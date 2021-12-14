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
    public class MongoDBController : Controller
    {

        private IMongoCollection<BsonDocument> _vlan;

        public MongoDBController(IMongoClient client)
        {
            var database = client.GetDatabase("ADB1");
            _vlan = database.GetCollection<BsonDocument>("DeviceOwnership_dim");
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
           
            List<BsonDocument> result = new List<BsonDocument>();
             var options = new AggregateOptions() {
                AllowDiskUse = true
            };
            PipelineDefinition<BsonDocument, BsonDocument> pipeline = new BsonDocument[]
                  {
                new BsonDocument("$project", new BsonDocument()
                        .Add("_id", 0)
                        .Add("DeviceOwnership_dim", "$$ROOT")),
                new BsonDocument("$lookup", new BsonDocument()
                        .Add("localField", "DeviceOwnership_dim.non_existing_field")
                        .Add("from", "radreply_dim")
                        .Add("foreignField", "non_existing_field")
                        .Add("as", "radreply_dim")),
                new BsonDocument("$unwind", new BsonDocument()
                        .Add("path", "$radreply_dim")
                        .Add("preserveNullAndEmptyArrays", new BsonBoolean(false))),
                new BsonDocument("$match", new BsonDocument()
                        .Add("$and", new BsonArray()
                                .Add(new BsonDocument()
                                        .Add("$expr", new BsonDocument()
                                                .Add("$eq", new BsonArray()
                                                        .Add("$DeviceOwnership_dim.MAC")
                                                        .Add("$radreply_dim.username")
                                                )
                                        )
                                )
                                .Add(new BsonDocument()
                                        .Add("$expr", new BsonDocument()
                                                .Add("$eq", new BsonArray()
                                                        .Add("$radreply_dim.value")
                                                        .Add("Vlan3")
                                                )
                                        )
                                )
                                .Add(new BsonDocument()
                                        .Add("$expr", new BsonDocument()
                                                .Add("$ne", new BsonArray()
                                                        .Add("$DeviceOwnership_dim.state")
                                                        .Add(new BsonInt64(1L))
                                                )
                                        )
                                )
                        )),
                new BsonDocument("$group", new BsonDocument()
                        .Add("_id", new BsonDocument())
                        .Add("COUNT(radreply_dim\u1390username)", new BsonDocument()
                                .Add("$sum", 1)
                        )),
                new BsonDocument("$project", new BsonDocument()
                        .Add("Number of non-active devices ", "$COUNT(radreply_dim\u1390username)")
                        .Add("_id", 0))
                  };

            using (var cursor = await _vlan.AggregateAsync(pipeline, options))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        result.Add(document);
                    }
                }
            }
            return View(result);
        }

    }
}

