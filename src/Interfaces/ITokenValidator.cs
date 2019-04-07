using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Interfaces
{
    public interface ITokenValidator
    {
        /// <summary>
        /// Validate a bearer token 
        /// </summary>
        /// <param name="bearerToken">The bearer token</param>
        /// <returns>True is autorized</returns>
        bool Validate(string bearerToken);
       
        /// <summary>
        /// Validates a bearer token located in the Authorize header
        /// </summary>
        /// <param name="headers">the Http Response Headers</param>
        /// <returns>true is autorized</returns>
        bool Validate(IHeaderDictionary headers, string headerName);
    }
}
