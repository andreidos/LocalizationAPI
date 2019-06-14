using Localization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Localization.DataStores
{
   public class LocationsDataStore
   {

      public static LocationsDataStore Current { get; } = new LocationsDataStore();
      public List<LocationDto> Locations { get; set; }

      public LocationsDataStore()
      {
         Locations = new List<LocationDto>()
         {
            new LocationDto()
            {
               Id= Guid.NewGuid(),
               BoardId = "DUMMY",
               IPAddress = "0.0.0.0",
               Longitude = 25.6052528,
               Latitude = 45.6534745,
               Status = EStatus.Awaiting,
               Time = DateTime.Now.ToString("dddd, dd MMMM yyyy")
            },
            new LocationDto()
            {
               Id= Guid.NewGuid(),
               BoardId = "DUMMY 2",
               IPAddress = "0.0.0.0",
               Longitude = 25.6052528,
               Latitude = 45.6534745,
               Status = EStatus.Solved
            }
         };
      }
   }
}
