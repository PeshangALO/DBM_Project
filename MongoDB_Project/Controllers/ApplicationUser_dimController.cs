using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;

namespace MongoDB_Project.Controllers
{
    public class ApplicationUser_dimController : Controller
    {
        // private readonly DatabaseContext_ADB1 _context;

        private IMongoCollection<ApplicationUser_dim> _applicationUser_dim;

        public ApplicationUser_dimController(IMongoClient client)
        {
            var database = client.GetDatabase("IoT");
            _applicationUser_dim = database.GetCollection<ApplicationUser_dim>("A");
            // _context = context;
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

        // [HttpGet("1")]
        // public async Task<IActionResult> Juice1(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice2(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice3(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice4(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice5(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice6(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice7(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice8(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice9(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice10(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice11(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

        // [HttpGet("2")]
        // public async Task<IActionResult> Juice12(){
        //     using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

        //     await connection.OpenAsync();

        //     var MAC = new List<String>();
        //     var Datetime = new List<DateTime>();

        //     using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
        //     using var reader = await command.ExecuteReaderAsync();
        //     while (await reader.ReadAsync())
        //     {
        //         MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
        //     }
        //     return View(MAC);
        // }

    }
}
