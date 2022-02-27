using DesktopClient.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.other
{
    class NetworkManager
    {
        public string UserName { get; private set; }
        private string Token;
        private string ServerUrl = "http://localhost:82/";
        private static NetworkManager manager;
        private HttpClient client;
        public static NetworkManager getInstance() 
        {
            if (manager== null) manager = new NetworkManager();
            return manager;
        }
        private NetworkManager() 
        {
            client = new HttpClient();
        }
        public async Task<bool> LogIn(LogInModel model) 
        {
            HttpClient client = new HttpClient();
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            pairs.Add("Username", model.Username);
            pairs.Add("Password", model.Password);
            string json = JsonConvert.SerializeObject(pairs);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            string url = "http://localhost:82/api/login";
            HttpResponseMessage response = await client.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                if (token.Length<50)
                {
                    return false;
                }
                Token = token;
                UserName = model.Username;
                return true;
            }
            return false;
        }
    }
}
