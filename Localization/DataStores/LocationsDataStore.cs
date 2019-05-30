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
               Longitude = 13.1234,
               Latitude = 13.0,
               Status = EStatus.Solved
            }
         };
      }
   }
}
