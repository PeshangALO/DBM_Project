using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationUser_dimController : Controller
    {

        private IMongoCollection<ApplicationUser_dim> _applicationUser_dim;

        public ApplicationUser_dimController(IMongoClient client)
        {
            var database = client.GetDatabase("IoT");
            _applicationUser_dim = database.GetCollection<ApplicationUser_dim>("A");
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
          
            IMongoClient client = new MongoClient("mongodb+srv://Peshang:juice@cluster0.g2i94.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            IMongoDatabase database = client.GetDatabase("IoT");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("DeviceOwnership");
            
            // Created with Studio 3T, the IDE for MongoDB - https://studio3t.com/
            
            var options = new AggregateOptions() {
                AllowDiskUse = true
            };
            
            PipelineDefinition<BsonDocument, BsonDocument> pipeline = new BsonDocument[]
            {
                new BsonDocument("$group", new BsonDocument()
                        .Add("_id", new BsonDocument()
                                .Add("DeviceOwnership\u1390OwnerEmail", "$DeviceOwnership.OwnerEmail")
                        )
                        .Add("COUNT(DeviceOwnership\u1390DeviceName)", new BsonDocument()
                                .Add("$sum", 1)
                        )), 
                new BsonDocument("$project", new BsonDocument()
                        .Add("DeviceOwnership.OwnerEmail", "$_id.DeviceOwnership\u1390OwnerEmail")
                        .Add("COUNT(DeviceOwnership\u1390DeviceName)", "$COUNT(DeviceOwnership\u1390DeviceName)")
                        .Add("_id", 0))
            };
            
            using (var cursor = await collection.AggregateAsync(pipeline, options))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        Console.WriteLine(document.ToJson());
                        
                    }
                }
            }
            return View();
           
        }
    }
}
