using System;
using System.Linq;
using Localization.DataStores;
using Localization.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Localization.Controllers
{
   [EnableCors("CorsPolicy")]
   [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : Controller
    {
      LocationsDataStore locationsStore = LocationsDataStore.Current;

      public IActionResult GetLocations()
      {
         return new JsonResult(locationsStore.Locations);
      }

      [HttpGet("{id}")]
      public IActionResult GetLocation(Guid id)
      {
         var location = locationsStore.Locations.FirstOrDefault(l=>l.Id == id);
         if(location == null)
         {
            return NotFound();
         }

         return Ok(location);
      }

      [HttpPut("{id}/update")]
      public IActionResult UpdateStatus(Guid id, [FromQuery] string status, [FromQuery] string helperid)
      {
         EStatus eStatus;
         if(Enum.TryParse(status, out eStatus))
         {
            ModelState.AddModelError("Status", "Status is not valid");
         }

         var locationFromStore = locationsStore.Locations.FirstOrDefault(l=> l.Id == id);

         if (locationFromStore == null)
         {
            return NotFound();
         }

         locationFromStore.Status = eStatus;
         locationFromStore.HelperId = helperid;

         return Ok(locationFromStore);
      }

      [HttpPost("addheader")]
      public IActionResult AddLocationFromHeader([FromQuery]string board, [FromQuery]double? longitude=0, [FromQuery]double? latitude=0)
      {
         if (board.Length < 0)
         {
            return BadRequest();
         }

         var location = new LocationDto() {
            Id = Guid.NewGuid(),
            BoardId = board,
            Longitude = (double)longitude,
            Latitude = (double)latitude
         };

         locationsStore.Locations.Add(location);

         return Ok();
      }

      [HttpPost("add")]
      public IActionResult AddLocation([FromBody] LocationForCreationDto newlocation)
      {
         Guid id = Guid.NewGuid();
         if (newlocation.BoardId.Length < 0) {
            return BadRequest();
         }

         var locationToAdd = new LocationDto() {
            Id = id,
            BoardId = newlocation.BoardId,
            IPAddress = newlocation.IPAddress,
            Longitude = newlocation.Longitude,
            Latitude = newlocation.Latitude
         };

         locationsStore.Locations.Add(locationToAdd);

         return Ok("OK");
      }

   }
}