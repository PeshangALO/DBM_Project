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
    public class MySqlController : Controller
    {

        [HttpGet("1")]
        public async Task<IActionResult> Juice1(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();
            var Datetime = new List<DateTime>();

            using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R where D.mac = R.username ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
            }
            return View(MAC);
        }

        [HttpGet("2")]
        public async Task<IActionResult> Juice2(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();
            var Datetime = new List<DateTime>();

            using var command = new MySqlCommand("select R.acctstarttime, D.MAC from deviceownership_dim D, radacct_dim R, applicationuser_dim A where D.mac = R.username and A.email = D.owneremail and A.groupmembership = 0 ORDER BY R.acctstarttime ASC LIMIT 100;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetDateTime(0).ToString()+"  |  "+reader.GetString(1));
            }
            return View(MAC);
        }

        [HttpGet("3")]
        public async Task<IActionResult> Juice3(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();

            using var command = new MySqlCommand("select R.username, R.acctstarttime from deviceownership_dim D, radacct_dim R where R.acctstarttime > '2021-06-17 13:10:11' and R.username = D.mac;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetDateTime(1).ToString()+"  |  "+reader.GetString(0));
            }
            return View(MAC);
        }

        [HttpGet("4")]
        public async Task<IActionResult> Juice4(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();
            var Datetime = new List<DateTime>();

            using var command = new MySqlCommand("select D.owneremail, D.vlan from deviceownership_dim D where D.vlan = 'Vlan2' and D.state = 1;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetString(1)+"  |  "+reader.GetString(0));
            }
            return View(MAC);
        }

        [HttpGet("5")]
        public async Task<IActionResult> Juice5(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");
            await connection.OpenAsync();
            var MAC = new List<String>();

            using var command = new MySqlCommand("select D.owneremail, D.mac, R.value from radreply_dim R, deviceownership_dim D where D.mac = R.username and R.value = 'Vlan4' and D.state = 2;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetString(2)+"  |  "+reader.GetString(1)+"  |  "+reader.GetString(0));
            }
            return View(MAC);
        }

        [HttpGet("6")]
        public async Task<IActionResult> Juice6(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();
            var Datetime = new List<DateTime>();

            using var command = new MySqlCommand("select count(R.username) as number_of_mac_addresses, R.value from radreply_dim R, deviceownership_dim D where D.mac = R.username and D.state = 1 group by R.value;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetString(1)+"  |  "+reader.GetInt64(0));
            }
            return View(MAC);
        }

        [HttpGet("7")]
        public async Task<IActionResult> Juice7(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();
            var Datetime = new List<DateTime>();

            using var command = new MySqlCommand("select count(R.username) as Number_of_non_active_devices from deviceownership_dim D, radreply_dim R where D.mac = R.username and R.value = 'Vlan3' and NOT D.state = 1", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetInt64(0)+"  |  ");
            }
            return View(MAC);
        }

        [HttpGet("8")]
        public async Task<IActionResult> Juice8(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();

            using var command = new MySqlCommand("select  R.username as MAC_Address, D.owneremail as Owner_Email from radacct_dim R, deviceownership_dim D where R.acctstarttime <= '2021-03-25 13:10:11' and (R.acctstoptime >= '2021-03-25 13:10:11' or R.acctstoptime = null) and R.username = D.mac;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetString(0)+"  |  "+reader.GetString(1));
            }
            return View(MAC);
        }

        [HttpGet("9")]
        public async Task<IActionResult> Juice9(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();
            var Datetime = new List<DateTime>();

            using var command = new MySqlCommand("select D.owneremail, count(D.devicename) as number_of_devices from deviceownership_dim D group by (D.OwnerEmail);", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetInt64(1)+"  |  "+reader.GetString(0));
            }
            return View(MAC);
        }

        [HttpGet("10")]
        public async Task<IActionResult> Juice10(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();

            using var command = new MySqlCommand("select avg(DATEDIFF(D.dateregistered,R.acctstarttime)) as average_time_from_auth_to_fist_connection from radacct_dim R, deviceownership_dim D where NOT D.state = 0;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetInt64(0)+"  |  ");
            }
            return View(MAC);
        }

        [HttpGet("11")]
        public async Task<IActionResult> Juice11(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();
            var Datetime = new List<DateTime>();

            using var command = new MySqlCommand("select (sum(CURRENT_TIMESTAMP() - R.acctstoptime)/count(D.MAC))/86400 as days_average_until_deleted from radacct_dim R, deviceownership_dim D where D.state = 2 and D.MAC = R.username;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetInt64(0)+"  |  ");
            }
            return View(MAC);
        }

        [HttpGet("12")]
        public async Task<IActionResult> Juice12(){
            using var connection = new MySqlConnection("Server=localhost;Database=ADB1;Uid=root;Password=juice");

            await connection.OpenAsync();

            var MAC = new List<String>();
            var Datetime = new List<DateTime>();

            using var command = new MySqlCommand("select (sum(R.acctinputoctets) / count(D.MAC)) as sumy from radacct_dim R, deviceownership_dim D, applicationuser_dim A where A.groupmembership = 0 and R.username = D.mac and A.email = D.owneremail;", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                MAC.Add("|  "+reader.GetInt64(0)+"  |  ");
            }
            return View(MAC);
        }

    }
}
