using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DevEx.Azure.AdventureWorks
{
    public class Common
    {
        public const string AppTitle = "AdventureWorks";
        
        public static string GetMSGraphToken()
        {
            string jsonResponse = null;
            string oauth2_token_url = string.Format(ConfigurationManager.AppSettings["ida:AADInstance"], ConfigurationManager.AppSettings["ida:Tenant"], "/oauth2/token?api-version=1.0");
            
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri("~/Home/Welcome.cshtml");
                var content = new System.Net.Http.FormUrlEncodedContent(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("resource", "https://graph.microsoft.com"),
                    new KeyValuePair<string, string>("client_id", ConfigurationManager.AppSettings["ida:ClientId"]),
                    new KeyValuePair<string, string>("client_secret", ConfigurationManager.AppSettings["ida:ClientSecret"])
                });
                
                var responseData = client.PostAsync(oauth2_token_url, content).Result;
                jsonResponse = responseData.Content.ReadAsStringAsync().Result;
            }
            
            return jsonResponse;
        }

    }
}
