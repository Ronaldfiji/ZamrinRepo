using CoffeeNext.Helper;
using CoffeeNext.Models;
using CoffeeNext.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProjectService))]
namespace CoffeeNext.Services
{
    public class ProjectService : IProjectService
    {
        public async Task<IEnumerable<Projects>> GetProjectsAsync(string url)
        {
            
            HttpClient client = RestService.getHttpClient();
           

            IEnumerable<Projects> myProjectList = null;

            var pro = new List<Projects>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                // HttpResponse<object> ht = new HttpResponse<object>();
                ApiResponse hr = new ApiResponse();

                if (response.IsSuccessStatusCode)
                {
                     string content = await response.Content.ReadAsStringAsync();                    

                     hr = JsonConvert.DeserializeObject<ApiResponse>(content);

                     hr.result.DataList.ForEach(a => {
                         pro.Add(JsonConvert.DeserializeObject<Projects>(a.ToString()));
                     }); 
                }

                return pro;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server:  {ex}");
                throw ex;
            }



        }

        public async Task DeleteProject(string projectToDelete)
        {
            HttpClient client = RestService.getHttpClient();

            var response = await client.DeleteAsync(projectToDelete);
            if (!response.IsSuccessStatusCode)
            {

            }
        }

        /* public class RootObject<T>
         {
             public T Item { get; set; }
             public List<T> Items { get; set; }
             public string message { get; set; }
         }*/


    }
}
