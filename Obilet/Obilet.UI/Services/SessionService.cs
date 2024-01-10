using Obilet.UI.Models.SessionModels;
using System;
using System.Net;
using System.Web;
using Obilet.UI.Interfaces;
using UAParser;

namespace Obilet.UI.Services
{
    public class SessionService : ISessionService
    {
        #region Fields
        private IIntegrationService _integrationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor
        public SessionService(IIntegrationService integrationService, IHttpContextAccessor httpContextAccessor)
        {
            _integrationService = integrationService;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Methods
        public DeviceSession GetSession()
        {
            try
            {
                var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
                var uaParser = Parser.GetDefault();
                ClientInfo client = uaParser.Parse(userAgent);
                var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                var sessionRequest = new SessionRequest()
                {
                    Type = 1,
                    Connection = new Connection { IpAddress = ipAddress, Port = "5117" },
                    Browser = new Browser { Name = client.UserAgent.Family, Version = $"{client.UserAgent.Major}.{client.UserAgent.Minor}" }
                };

                var sessionResponse = _integrationService.GetSession(sessionRequest).Result;
                if (sessionResponse.Status != "Success")
                    throw new Exception($"Error on GetSession. Details:{sessionResponse.Message}");

                var data = sessionResponse.Data;
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}
