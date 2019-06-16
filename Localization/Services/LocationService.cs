using Localization.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Localization.Services
{
   public class LocationService
   {
      private readonly IMongoCollection<LocationMongoDTO> locationsCollection;

      public LocationService(ILocationDatabaseSettings settings)
      {
         Console.WriteLine(settings.ConnectionString);
         var client = new MongoClient(settings.ConnectionString);
         var database = client.GetDatabase(settings.DatabaseName);

         locationsCollection = database.GetCollection<LocationMongoDTO>(settings.LocationsCollectionName);
      }

      public List<LocationMongoDTO> Get() =>
          locationsCollection.Find(location => true).ToList();

      public LocationMongoDTO Get(string id) =>
          locationsCollection.Find(location => location.Id == id).FirstOrDefault();

      public LocationMongoDTO GetByBoard(string boardId) =>
          locationsCollection.Find(location => location.BoardId == boardId && location.Status != EStatus.Solved).FirstOrDefault();

      public LocationMongoDTO Create(LocationMongoDTO location)
      {
         locationsCollection.InsertOne(location);
         return location;
      }

      public void Update(string id, LocationMongoDTO locationToUpdate) =>
          locationsCollection.ReplaceOne(location=> location.Id == id, locationToUpdate);

      public void Remove(string id) =>
          locationsCollection.DeleteOne(location => location.Id == id);
   }
}
