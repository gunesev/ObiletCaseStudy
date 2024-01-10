using Obilet.UI.Models.BusModels;
using Obilet.UI.Models.SessionModels;
using System.Collections.Generic;

namespace Obilet.UI.Interfaces
{
    public interface IBusService
    {
        List<BusLocation> GetBusLocations(DeviceSession session);
        List<BusJourney> GetBusJourneys(BusJourneyData data, DeviceSession session);
    }
}
