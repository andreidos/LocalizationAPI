using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Localization.Models
{
   public class LocationForCreationDto
   {
      public string BoardId { get; set; }
      public string IPAddress { get; set; }
      public double Longitude { get; set; }
      public double Latitude { get; set; }
      public string AdditionalData { get; set; }
   }
}
