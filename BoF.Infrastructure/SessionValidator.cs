using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RFIS.Web
{
    public interface ISessionValidator
    {
        SessionToken ValidateSession();
    }
    public class SessionValidator : ISessionValidator
    {
        private readonly HttpContextBase httpContextBase;

        public SessionValidator(HttpContextBase httpContextBase)
        {
            this.httpContextBase = httpContextBase;
        }

        public SessionToken ValidateSession()
        {
            // Do some validation here
            var sessionToken = new SessionToken
            {
                IpAddress = IPAddress.Parse(httpContextBase.Request.UserHostAddress),
                IsValid = true
            };

            return sessionToken;
        }
    }
}