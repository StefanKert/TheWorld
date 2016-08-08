using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, string username, Stop newStop)
        {
            var trip = GetTripByName(tripName, username);
            newStop.Order = trip.Stops.Max(s => s.Order) + 1;
            trip.Stops.Add(newStop);
            _context.Stops.Add(newStop);
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting all the trips from the database.");
            return _context.Trips.ToList();
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            return _context.Trips.Include(x => x.Stops).OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Trip> GetAllTripsWithStops(string name)
        {
            return _context.Trips.Include(x => x.Stops).OrderBy(x => x.Name).Where(x => x.UserName == name).ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips.Include(x => x.Stops).FirstOrDefault(x => x.Name == tripName);
        }

        public Trip GetTripByName(string tripName, string username)
        {
            return _context.Trips.Include(x => x.Stops).FirstOrDefault(x => x.Name == tripName && x.UserName == username);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
