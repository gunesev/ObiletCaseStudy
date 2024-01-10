using Obilet.UI.Models;
using Obilet.UI.Models.BusModels;
using Obilet.UI.Models.SessionModels;
using Obilet.UI.Interfaces;

namespace Obilet.UI.Services
{
    public class BusService : IBusService
    {
        #region Fields

        private IIntegrationService _integrationService;

        #endregion

        #region Ctor
        public BusService(IIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }
        #endregion


        #region Methods        
        public List<BusLocation> GetBusLocations(DeviceSession session)
        {
            try
            {
                var busLocationsRequest = new Request<string>() { DeviceSession = session };

                var busLocationsResponse = _integrationService.GetBusLocations(busLocationsRequest).Result;
                if (busLocationsResponse.Status != "Success")
                    throw new Exception($"Error on GetBusLocations. Details:{busLocationsResponse.Message}");

                var data = busLocationsResponse.Data;
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<BusJourney> GetBusJourneys(BusJourneyData busJourneyData, DeviceSession session)
        {
            try
            {
                var busJourneysRequest = new Request<BusJourneyData>() { DeviceSession = session, Data = busJourneyData };

                var busJourneysResponse = _integrationService.GetBusJourneys(busJourneysRequest).Result;
                if (busJourneysResponse.Status != "Success")
                    throw new Exception($"Error on GetBusJourneys. Details:{busJourneysResponse.Message}");

                var data = busJourneysResponse.Data;
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
