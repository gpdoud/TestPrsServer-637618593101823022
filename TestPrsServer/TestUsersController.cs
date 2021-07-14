using PrsServer6.Models;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using TestPrsServer.Models;

namespace TestPrsServer {

    public class TestUsersController {

        public Config config { get; set; }
        private static HttpClient http = new HttpClient();
        private static JsonSerializerOptions jsonOptions = new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        };

        public TestUsersController(Config config) {
            this.config = config;
        }

        public async Task TestUserLogin() {
            Console.WriteLine("TestUserLogin");
            var username = Program.ReadConsole("Username", "sa");
            var password = Program.ReadConsole("Password", "sa");
            var userJson = await http.GetAsync($"{config.fullUrl}/{username}/{password}").Result.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(userJson, jsonOptions);
            if (user.Username != username || user.Password != password) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("TestUserLogin test failed!");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine("*** passed ***");
            Console.WriteLine(user);
        }
        public async Task TestUserGetAll() {
            Console.WriteLine("TestUserGetAll ");
            var userJson = await http.GetAsync($"{config.fullUrl}").Result.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<User[]>(userJson, jsonOptions);
            Console.WriteLine("*** passed ***");
            foreach(var user in users) {
                Console.WriteLine(user);
            }
        }
        public async Task TestUserGetByPk() {
            Console.WriteLine("TestUserGetByPk ");
            var id = Convert.ToInt32(Program.ReadConsole("User id:", "1"));
            var userJson = await http.GetAsync($"{config.fullUrl}/{id}").Result.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<User>(userJson, jsonOptions);
            Console.WriteLine("*** passed ***");
            Console.WriteLine(user);
        }
        public async Task TestUserCreate() {
            Console.WriteLine("TestUserCreate ");
            var username = Program.ReadConsole("Username:", "user-DateTime.Now.Millisecond.ToString()");
            var user = new User() {
                Id = 0, Username = $"{username}", Password = "password",
                Firstname = "AA", Lastname = "AA", IsAdmin = false, IsReviewer = false
            };
            var userJson = JsonSerializer.Serialize<User>(user, jsonOptions);
            var content = new StringContent(userJson);
            var rowsAffected = await http.PostAsync($"{config.fullUrl}", content);
            Console.WriteLine("*** passed ***");
            Console.WriteLine(user);
        }
    }
}
