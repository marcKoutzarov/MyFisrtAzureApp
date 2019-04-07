using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Implementation;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace MyFisrtAzureApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IApiManager _manager;
        ITokenValidator _tokenValidator;
        IConfiguration _Configuration;

        public ValuesController(IApiManager manager, ITokenValidator tokenValidator, IConfiguration configuration)
        {
            _manager = manager;
            _tokenValidator = tokenValidator;
            _Configuration = configuration;
        }

        /// <summary>
        /// Summary about this end point
        /// </summary>
        /// <param name="authHeader">bearer token</param>
        /// <returns>String</returns>
        /// <remarks>Some remarks about this endpoint</remarks>
        [HttpGet]
        [ProducesResponseType(404, Type = typeof(ApiResponse<ProblemDetails>))]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(500, Type = typeof(ApiResponse<ProblemDetails>))]
        [ProducesResponseType(401, Type = typeof(ApiResponse<ProblemDetails>))]
        public JsonResult Get([FromHeader(Name = "Authorize")] string authHeader)
        {
            if (!_tokenValidator.Validate(Request.Headers, "Authorize"))
            {
                Response.StatusCode = 401;
                return UnAuthorized("/api/values/{id}");
            };

            Response.StatusCode = 200;
            return new JsonResult(new ApiResponse<string[]>
            {
                Success = true,
                StatusCode = 200,
                Payload = _manager.GetValues()
            });
        }
 
        /// <summary>
        /// gets a single item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authHeader">Required bearer token</param>
        /// <returns>string</returns>
        /// <remarks>some remakrs to be made here</remarks>
        [ProducesResponseType(404, Type = typeof(ApiResponse<ProblemDetails>))]
        [ProducesResponseType(200, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(500, Type = typeof(ApiResponse<ProblemDetails>))]
        [ProducesResponseType(401, Type = typeof(ApiResponse<ProblemDetails>))]
        [HttpGet("{id}")]
        public JsonResult Get(int id, [FromHeader(Name = "Authorize")] string authHeader)
        {
            if (!_tokenValidator.Validate(Request.Headers, "Authorize")) {
                Response.StatusCode = 401;
                return UnAuthorized("/api/values/{id}");
            };

            JsonResult result;
            try
            {
                Response.StatusCode = 200;
                var model = _manager.GetValue(id);
                var response = new ApiResponse<string> { Success = true, StatusCode = 200, Payload = model };
                result = new JsonResult(response);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                result = ServerError("/api/values/{id}");
            };

            return result;
        }

        private JsonResult UnAuthorized(string instance)
        {

            var response = new ApiResponse<ProblemDetails>
            {
                Success = false,
                StatusCode = 401,
                Payload = new ProblemDetails
                {
                    Detail = "Access denied",
                    Type = "About:Blank",
                    Status = 401,
                    Title = "401 UnAuthorized",
                    Instance = instance
                }
            };
            return new JsonResult(response);
        }

        private JsonResult ServerError(string instance)
        {
            var response = new ApiResponse<ProblemDetails>
            {
                Success = false,
                StatusCode = 500,
                Payload = new ProblemDetails
                {
                    Detail = "Unexpected server error occured",
                    Type = "About:Blank",
                    Status = 500,
                    Title = "500 Server error",
                    Instance = instance
                }
            };
            return new JsonResult(response);
        }

        private JsonResult NotFound(string instance)
        {
            var response = new ApiResponse<ProblemDetails>
            {
                Success = false,
                StatusCode = 404,
                Payload = new ProblemDetails
                {
                    Detail = "Requested item was not found",
                    Type = "About:Blank",
                    Status = 404,
                    Title = "404 Not Found",
                    Instance = instance
                }
            };
            return new JsonResult(response);
        }
    }
}



