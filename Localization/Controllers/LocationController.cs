using System;
using System.Collections.Generic;
using Localization.Models;
using Localization.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Localization.Controllers
{
   [EnableCors("CorsPolicy")]
   [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : Controller
    {
      private readonly LocationService locationService;

      public LocationsController(LocationService _locationService)
      {
         locationService = _locationService;
      }

      public ActionResult<List<LocationMongoDTO>> Get() =>
            locationService.Get();

      [HttpGet("{id}", Name = "GetLocation")]
      public ActionResult<LocationMongoDTO> Get(string id)
      {
         var location = locationService.Get(id);

         if (location == null) {
            return NotFound();
         }

         return location;
      }

      [HttpGet("board/{boardId}")]
      public ActionResult<LocationMongoDTO> GetAlertStatus(string boardId)
      {
         var location = locationService.GetByBoard(boardId);

         if (location == null) {
            return NotFound();
         }

         return location;
      }

      [HttpPatch("update/{boardid}")]
      public IActionResult UpdateStatus(string boardId, [FromQuery] string status, [FromQuery] string helperid)
      {
         EStatus eStatus;
         if(Enum.TryParse(status, out eStatus))
         {
            ModelState.AddModelError("Status", "Status is not valid");
         }

         var locationFromStore = locationService.GetByBoard(boardId);

         if (locationFromStore == null)
         {
            return NotFound();
         }

         locationFromStore.Status = eStatus;
         locationFromStore.HelperId = helperid;

         locationService.Update(locationFromStore.Id, locationFromStore);

         return Ok(locationFromStore);
      }

      [HttpPost("add")]
      public ActionResult<LocationMongoDTO> Create([FromBody] LocationForCreationDto location)
      {
         var locationToAdd = new LocationMongoDTO() {
            BoardId = location.BoardId,
            Longitude = location.Longitude,
            Latitude = location.Latitude,
            IPAddress = location.IPAddress,
            Time = DateTime.Now.ToString("dd MMMM yyyy - HH:mm"),
            AdditionalData = location.AdditionalData,
            Status = EStatus.Awaiting
         };

         locationService.Create(locationToAdd);

         return CreatedAtRoute("GetLocation", new { id = locationToAdd.Id.ToString() }, locationToAdd);
      }

      [HttpDelete("{boardId}")]
      public IActionResult Delete(string boardId)
      {
         var location = locationService.GetByBoard(boardId);

         if (location == null) {
            return NotFound();
         }

         locationService.Remove(location.Id);

         return Ok();
      }

   }
}