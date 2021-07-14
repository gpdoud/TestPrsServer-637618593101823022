using System;
using System.Threading.Tasks;

using TestPrsServer.Models;

namespace TestPrsServer {

    class Program {
    
        static async Task Main(string[] args) {
            var config = GetConfig();
            var testUserCtrl = new TestUsersController(config);
            await testUserCtrl.TestUserLogin();
            await testUserCtrl.TestUserGetByPk();
            await testUserCtrl.TestUserCreate();
            await testUserCtrl.TestUserGetAll();
        }
        static Config GetConfig() {
            var config = new Config();
            config.Domain = ReadConsole("Domain?", "localhost");
            config.Port = Convert.ToInt32(ReadConsole("Port?", "5000"));
            return config;
        }
        public static string ReadConsole(string prompt, string def) {
            Console.Write($"{prompt} (default {def}): ");
            var ans = Console.ReadLine();
            return (ans.Trim().Length == 0) ? def : ans;
        }
    }
}
