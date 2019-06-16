using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Localization.Models
{
   public class LocationMongoDTO
   {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }
      public string BoardId { get; set; }
      public string IPAddress { get; set; }
      public double Longitude { get; set; }
      public double Latitude { get; set; }
      public EStatus Status { get; set; }
      public string HelperId { get; set; }
      public String Time { get; set; }
      public string AdditionalData { get; set; }
   }

   public enum EStatus
   {
      Awaiting,
      OnRoute,
      Solved
   }
}
