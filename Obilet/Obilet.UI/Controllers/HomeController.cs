using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Obilet.UI.Models.BusModels;
using Obilet.UI.Models.SessionModels;
using Obilet.UI.Interfaces;
using Obilet.UI.Models;
using System.Diagnostics;
using UAParser;
using System.Net;
using System.Reflection.Metadata;

namespace Obilet.UI.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        private readonly ILogger<HomeController> _logger;

        private readonly IBusService _busService;
        private readonly ISessionService _sessionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        string sessionCookie;
        const string cookieKey = "session";
        #endregion

        #region Ctor
        public HomeController(ILogger<HomeController> logger, IBusService busService, ISessionService sessionService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _busService = busService;
            _sessionService = sessionService;
            _httpContextAccessor = httpContextAccessor;
            CreateSession();
        }
        #endregion

        #region Methods
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(sessionCookie))
                sessionCookie = GetCookie(cookieKey);

            var deviceSession = JsonConvert.DeserializeObject<DeviceSession>(sessionCookie);

            var busLocationData = _busService.GetBusLocations(deviceSession);

            var busLocationList = new List<SelectListItem>();
            var i = 0;
            foreach (var location in busLocationData)
            {
                busLocationList.Add(new SelectListItem { Text = location.Name, Value = location.Id.ToString() });
                i++;
                if (i > 10)
                    break;
            }

            ViewBag.BusLocationList = new SelectList(busLocationList, "Value", "Text");
            return View();
        }

        public ActionResult FindJourney(int origin, int destination, DateTime departureDate)
        {
            var data = new BusJourneyData() { OriginId = origin, DestinationId = destination, DepartureDate = departureDate.ToString("yyyy-MM-dd") };

            var deviceSession = JsonConvert.DeserializeObject<DeviceSession>(sessionCookie);
            var busJourneyList = _busService.GetBusJourneys(data, deviceSession);

            busJourneyList = busJourneyList.OrderBy(o => o.Journey.Departure).ToList();

            return View("JourneyIndex", busJourneyList);
        }

        private void CreateSession()
        {

            sessionCookie = GetCookie(cookieKey);

            if (string.IsNullOrEmpty(sessionCookie))
            {
                var session = JsonConvert.SerializeObject(_sessionService.GetSession());

                AddCookie(session);
            }            
        }

        private void AddCookie(string value)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);

            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieKey, value, cookieOptions);

            sessionCookie = value;
        }

        private string? GetCookie(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }
        #endregion
    }
}