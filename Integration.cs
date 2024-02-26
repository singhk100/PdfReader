using Newtonsoft.Json;
using PdfReader.Interface;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace PdfReader
{
    public class Integration 
    {
        private object client;


        public async Task<string> CallApi(string jsonData)
        {
            string apiUrl = "http://localhost:5035/api";
            using (HttpClient client = new HttpClient())
            {
                try
                    {
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    string result = "";

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }
                    else
                    {
                        result = "API called failed";
                        Console.WriteLine("API called failed");
                    }
                    return result;
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error: {e}");
                    throw e;
                }
            }
        }
    }
}
