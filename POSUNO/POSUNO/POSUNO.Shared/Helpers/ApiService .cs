using Newtonsoft.Json;
using POSUNO.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace POSUNO.Helpers
{
    public class ApiService 
    {

        public static async Task<Response> LoginAsing(LoginRequest model)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);//se debe descargar
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json"); //descargar net.http
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                 string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {

                    //BaseAddress= new Uri("https://localhost:44387/")
                   BaseAddress = new Uri(url)
                };

                HttpResponseMessage response = await client.PostAsync("api/Account/Login", content);

                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        isSuccess = false,
                        Message = result,
                    };
                }

                User user = JsonConvert.DeserializeObject<User>(result);

                return new Response
                {
                    isSuccess = true,
                    Result = user,
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    isSuccess = false,
                    Message = ex.Message
                };
            }
        }




    }
}
