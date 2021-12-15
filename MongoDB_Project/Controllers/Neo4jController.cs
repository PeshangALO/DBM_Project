using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using Neo4jClient;

namespace MongoDB_Project.Controllers
{
    public class Neo4jController : Controller
    {
        private readonly IDriver _driver;

        public Neo4jController()
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "juice"));
        }


        [HttpGet("1")]
        public async Task<IActionResult> Neo1()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH (n:ApplicationUser) RETURN n.Email");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("2")]
        public async Task<IActionResult> Neo2()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH (n:ApplicationUser) RETURN n.Email");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("3")]
        public async Task<IActionResult> Neo3()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH (n:ApplicationUser) RETURN n.Email");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("41")]
        public async Task<IActionResult> Neo4()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH(d:DeviceOwnership) WHERE d.Vlan = 'Vlan4' and d.State = '1' RETURN d.OwnerEmail");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("5")]
        public async Task<IActionResult> Neo5()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH (n:ApplicationUser) RETURN n.Email");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("66")]
        public async Task<IActionResult> Neo6()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();
            IAsyncSession session = _driver.AsyncSession();
            
            cursor = await session.RunAsync(@"MATCH(v:VLAN)-[:CONNECTS]->(d:DeviceOwnership) where d.State ='1' return count(d), v.VlanAlias");
            email = cursor.ToListAsync(record => 
            record[0].As<string>()).Result;
            
            IResultCursor cursor2;
            var email2 = new List<string>();
            IAsyncSession session2 = _driver.AsyncSession();
            
            cursor2 = await session2.RunAsync(@"MATCH(v:VLAN)-[:CONNECTS]->(d:DeviceOwnership) where d.State ='1' return v.VlanAlias, count(d)");
            email2 = cursor2.ToListAsync(record => 
            record[0].As<string>()).Result;
            
            var email3 = new List<string>();
            for (int i = 0; i < email.Count; i++)
            {
                email3.Add(email2[i] + " | " + email[i]);
            }

            return View(email3);
        }

        [HttpGet("77")]
        public async Task<IActionResult> Neo7()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH(v:VLAN)-[:CONNECTS]->(d:DeviceOwnership) where d.State ='2' or d.State = '0' return count(d), v.VlanAlias");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("8")]
        public async Task<IActionResult> Neo8()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH (n:ApplicationUser) RETURN n.Email");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("9")]
        public async Task<IActionResult> Neo9()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH (n:ApplicationUser) RETURN n.Email");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("10")]
        public async Task<IActionResult> Neo10()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH (n:ApplicationUser) RETURN n.Email");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("11")]
        public async Task<IActionResult> Neo11()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH (n:ApplicationUser) RETURN n.Email");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }

        [HttpGet("12")]
        public async Task<IActionResult> Neo12()
        {
            // var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "juice");
            // graphClient.Connect();

            IResultCursor cursor;
            var email = new List<string>();

            IAsyncSession session = _driver.AsyncSession();
            try {
                cursor = await session.RunAsync(@"MATCH (n:ApplicationUser) RETURN n.Email");
                email = cursor.ToListAsync(record => 
                record[0].As<string>()).Result;
            }
            finally
            { 
                await session.CloseAsync();
            }


            return View(email);
        }





    }
}
