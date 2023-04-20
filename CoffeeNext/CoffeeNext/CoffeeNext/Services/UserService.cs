using CoffeeNext.Helper;
using CoffeeNext.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CoffeeNext.Services
{
    public class UserService
    {
        static string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android ?
                                            "http://10.0.2.2:5000/" : "http://localhost:5000";

        //static string BaseUrl = "https://www.fijishopper.somee.com/";

        //https://localhost:7276/api/coffee

        //static string BaseUrl = "https://motzcoffee.azurewebsites.net/";
       // static string BaseUrl = "http://10.0.2.2:5000/";

        static HttpClient client;
        private static int _refreshTokenEntered = 0;
        public string BearerToken => Preferences.Get("BearerToken", string.Empty);
        public string RefreshToken => Preferences.Get("RefreshToken", string.Empty);
        static UserService()
        {
            try
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(BaseUrl)
                    
                };
                client.Timeout = TimeSpan.FromSeconds(10);
            }
            catch
            {
                Console.WriteLine("Failed to create api endpoint");
            }
        }

        public static async Task<IEnumerable<Person>> GetUsers()
        {
            var json = await client.GetStringAsync("api/UserManager/GetAll");
            var coffees = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
            return coffees;
        }

        public static async Task<ResponseStatus> CreateUser(Person user)
        {
            Console.WriteLine("Gender value is : " + user.Gender);
            Console.WriteLine("Country value is : " + user.SelectedCountry);
            if (string.IsNullOrEmpty(user.SelectedCountry)) user.Gender = "O";

            // DateTime dt = !string.IsNullOrEmpty(user.DOB) ?  DateTime.Parse(user.DOB) : new DateTime();
            // string formatedDOB = dt.ToString("dd-MM-yyyy");
            string formatedDOB = string.Empty;
            if (!string.IsNullOrEmpty(user.DOB))
            {
                DateTime dt = DateTime.Parse(user.DOB);
                formatedDOB = dt.ToString("dd-MM-yyyy");
            }
            var newUser = new Person
            {

                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                DOB = formatedDOB,//"2016-11-30", //DateTime.Parse(user.DOB).ToString("dd-MM-yyyy"),
                Password = user.Password,
                Gender = user.Gender,
                City = user.City,                 
                SelectedCountry = user.SelectedCountry,
                State = user.State,
                ResidentialAddress = user.ResidentialAddress,
                ZipCode = user.ZipCode, 
            };
            var json = JsonConvert.SerializeObject(newUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");           
            try{
                var response = await client.PostAsync("api/UserManager/CreateUser", content);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Failed to create user "+ response);
                    return new ResponseStatus()
                    {
                        Message = "Failed to create record, application error !" + response.Content, 
                    };                    
                }
                Settings.Username = user.Email;
                Debug.WriteLine("Setting the user name  " + Settings.Username);
                return new ResponseStatus()
                {
                    Status = true,
                    Message = "Record has been added successfully !"

                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to add record : review error logs : "+ ex);
                
                return new ResponseStatus()
                {
                   
                    Message = "Failed to add record, application error !"

                };
            }
        }

        public static async Task<HttpResponse<object>> LoginUser(LoginModel loginModel)
        {
            

            //HttpResponseMessage response = new HttpResponseMessage();
            var userCredenatils = new LoginModel
            {                
                Email = loginModel.Email,
                Password = loginModel.Password         
            };            
            var json = JsonConvert.SerializeObject(userCredenatils);           
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {               
                HttpResponseMessage response = await client.PostAsync("api/UserManager/Login", content);

                HttpResponse<object> httpRes = new HttpResponse<object>();

                if (response.IsSuccessStatusCode)   
                {
                   var authResults = new AuthResult();
                    string content_res = await response.Content.ReadAsStringAsync();
                    httpRes = JsonConvert.DeserializeObject<HttpResponse<object>>(content_res);
                    httpRes.DataList.ForEach(data => {
                        
                        authResults = JsonConvert.DeserializeObject<AuthResult>(data.ToString()); 
                    });
                    //Preferences.Set("BearerToken", authResults.Token);                                   
                    await SecureStorage.SetAsync("token", authResults.Token);
                    await SecureStorage.SetAsync("refreshToken", authResults.Token);
                    await SecureStorage.SetAsync("userId", authResults.UserId.ToString());           
                }                          

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Debug.WriteLine("HttpStatusCode.Unauthorized........................" + response.StatusCode);
                    return new HttpResponse<object>()
                    {
                        Message = "Failed to login user, invalid logic credentials !",
                        ErrorCode = 401,
                    };
                }

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    Debug.WriteLine("HttpStatusCode.InternalServerError........................" + response.StatusCode.GetHashCode());
                    return new HttpResponse<object>()
                    {
                        Message = "Failed to login user, application error !",
                        ErrorCode = 500,
                    };
                }

                if (!response.IsSuccessStatusCode) {

                    Debug.WriteLine("!response.IsSuccessStatusCode......................." + response.IsSuccessStatusCode.GetHashCode());
                    return new HttpResponse<object>()
                    {
                        Message = "Failed to login user, application error !"
                    }; 
                }
                
                return new HttpResponse<object>()
                {
                    IsSuccess = true,
                    Message = "Logged in successfully !"
                };
            }
            catch (Exception ex)
            {                   
                if(ex.GetType().ToString() == "System.OperationCanceledException")
                {
                    Debug.WriteLine("Failed to login. API request Timeout. Review error logs " + ex);
                    return new HttpResponse<object>()
                    {
                        Message = "System error, contact application admin !",
                        ErrorCode = 500,
                    };

                }
                Debug.WriteLine("Failed to login. API failed to respond correctly, review error logs ! " + ex);
                return new HttpResponse<object>()
                {
                    Message = "System error, contact application admin !",
                    ErrorCode = 500,
                };               
            }

        }

       

     /* public async Task<Person> GetPersonAsync(int id)
        {

        }*/

       

    }//end of class
}
