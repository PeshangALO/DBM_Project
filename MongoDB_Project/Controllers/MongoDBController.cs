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

        private IMongoCollection<BsonDocument> collection1;
         private IMongoCollection<BsonDocument> collection2;

        public MongoDBController(IMongoClient client)
        {
            var database = client.GetDatabase("ADB1");
            collection1 = database.GetCollection<BsonDocument>("DeviceOwnership_dim");
            collection2 = database.GetCollection<BsonDocument>("radreply_dim");
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
                        .Add("Total of ", "$COUNT(radreply_dim\u1390username)")
                        .Add("_id", 0))
                  };

            using (var cursor = await collection1.AggregateAsync(pipeline, options))
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

        [HttpGet("2")]
        public async Task<ActionResult> Query2()
        {
           
            List<BsonDocument> result = new List<BsonDocument>();
             var options = new AggregateOptions() {
                AllowDiskUse = true
            };
            PipelineDefinition<BsonDocument, BsonDocument> pipeline = new BsonDocument[]
            {
                new BsonDocument("$project", new BsonDocument()
                        .Add("_id", 0)
                        .Add("radreply_dim", "$$ROOT")), 
                new BsonDocument("$lookup", new BsonDocument()
                        .Add("localField", "radreply_dim.non_existing_field")
                        .Add("from", "DeviceOwnership_dim")
                        .Add("foreignField", "non_existing_field")
                        .Add("as", "DeviceOwnership_dim")), 
                new BsonDocument("$unwind", new BsonDocument()
                        .Add("path", "$DeviceOwnership_dim")
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
                                                        .Add("Vlan4")
                                                )
                                        )
                                )
                                .Add(new BsonDocument()
                                        .Add("$expr", new BsonDocument()
                                                .Add("$eq", new BsonArray()
                                                        .Add("$DeviceOwnership_dim.State")
                                                        .Add(new BsonInt64(2L))
                                                )
                                        )
                                )
                        )), 
                new BsonDocument("$project", new BsonDocument()
                        .Add("OwnerEmail  ", "$DeviceOwnership_dim.OwnerEmail")
                        .Add("  MAC  ", "$DeviceOwnership_dim.MAC")
                        .Add("  Vlan  ", "$radreply_dim.value" )
                        )
            };
            
            using (var cursor = await collection2.AggregateAsync(pipeline, options))
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

        [HttpGet("3")]
        public async Task<ActionResult> Query3()
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
                        .Add("from", "radacct_dim")
                        .Add("foreignField", "non_existing_field")
                        .Add("as", "radacct_dim")), 
                new BsonDocument("$unwind", new BsonDocument()
                        .Add("path", "$radacct_dim")
                        .Add("preserveNullAndEmptyArrays", new BsonBoolean(false))), 
                new BsonDocument("$match", new BsonDocument()
                        .Add("$expr", new BsonDocument()
                                .Add("$eq", new BsonArray()
                                        .Add("$DeviceOwnership_dim.MAC")
                                        .Add("$radacct_dim.username")
                                )
                        )), 
                new BsonDocument("$sort", new BsonDocument()
                        .Add("radacct_dim.acctstarttime", 1)), 
                new BsonDocument("$project", new BsonDocument()
                        .Add("radacct_dim.acctstarttime", "$radacct_dim.acctstarttime")
                        .Add("DeviceOwnership_dim.MAC", "$DeviceOwnership_dim.MAC")
                        .Add("_id", 0)), 
                new BsonDocument("$limit", 100)
            };
            
            using (var cursor = await collection1.AggregateAsync(pipeline, options))
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

        [HttpGet("4")]
        public async Task<ActionResult> Query4()
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
                        .Add("from", "radacct_dim")
                        .Add("foreignField", "non_existing_field")
                        .Add("as", "radacct_dim")), 
                new BsonDocument("$unwind", new BsonDocument()
                        .Add("path", "$radacct_dim")
                        .Add("preserveNullAndEmptyArrays", new BsonBoolean(false))), 
                new BsonDocument("$match", new BsonDocument()
                        .Add("$and", new BsonArray()
                                .Add(new BsonDocument()
                                        .Add("$expr", new BsonDocument()
                                                .Add("$gt", new BsonArray()
                                                        .Add("$radacct_dim.acctstarttime")
                                                        .Add("2021-06-17 13:10:11")
                                                )
                                        )
                                )
                                .Add(new BsonDocument()
                                        .Add("$expr", new BsonDocument()
                                                .Add("$eq", new BsonArray()
                                                        .Add("$radacct_dim.username")
                                                        .Add("$DeviceOwnership_dim.MAC")
                                                )
                                        )
                                )
                        )), 
                new BsonDocument("$project", new BsonDocument()
                        .Add("radacct_dim.username", "$radacct_dim.username")
                        .Add("radacct_dim.acctstarttime", "$radacct_dim.acctstarttime")
                        .Add("DeviceOwnership_dim.MAC", "$DeviceOwnership_dim.MAC")
                        .Add("_id", 0))
            };
            
            using (var cursor = await collection1.AggregateAsync(pipeline, options))
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

