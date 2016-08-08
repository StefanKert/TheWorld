using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        void AddTrip(Trip newTrip);

        Task<bool> SaveChangesAsync();
        
        Trip GetTripByName(string tripName);
        void AddStop(string tripName, string username, Stop newStop);
        IEnumerable<Trip> GetAllTripsWithStops();
        IEnumerable<Trip> GetAllTripsWithStops(string name);
        Trip GetTripByName(string tripName, string username);
    }
}