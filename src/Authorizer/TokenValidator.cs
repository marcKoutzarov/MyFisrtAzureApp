using Interfaces;
using Microsoft.AspNetCore.Http;

namespace Authorizer
{
    public class TokenValidator : ITokenValidator
    {
        public bool Validate(string bearerToken)
        {
            return CheckToken(bearerToken);
        }

        public bool Validate(IHeaderDictionary headers, string headerName)
        {
            string  bearerToken = headers[headerName];

            return CheckToken(bearerToken);
        }
      
        /// <summary>
        /// Checks if the token is valid
        /// </summary>
        /// <param name="bearerToken"></param>
        /// <returns></returns>
        private bool CheckToken(string bearerToken)
        {
            if (bearerToken?.Length < 5 || bearerToken?.Length==null) { return false;};
            
            return true;
        }

    }
}
