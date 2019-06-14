using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Localization.Models
{
   public class LocationDto
   {
      [Key]
      public Guid Id { get; set; }
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
