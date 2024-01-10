using Obilet.UI.Models.SessionModels;
using Obilet.UI.Models;
using Obilet.UI.Models.BusModels;

namespace Obilet.UI.Interfaces
{
    public interface IIntegrationService
    {
        Task<Response<DeviceSession>> GetSession(SessionRequest sessionRequest);
        Task<Response<List<BusLocation>>> GetBusLocations(Request<string> request);
        Task<Response<List<BusJourney>>> GetBusJourneys(Request<BusJourneyData> request);
    }
}
