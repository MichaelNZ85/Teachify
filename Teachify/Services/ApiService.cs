using System;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Teachify.Models;
using Teachify.Helpers;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Teachify.Services
{
    public class ApiService
    {
        //string baseUrl = "https://teachify-michael.azurewebsites.net/api/";
        string rootUrl = "https://8406f7fbf92f.ngrok.io";
        string baseUrl = "https://8406f7fbf92f.ngrok.io/api/v1/";

        //string accessToken = "hjM7yzxSGYu9LSNRIgyXPoAt0N3qz6F6UFsOLHYgQp9vLeIC1_tzaBGiAc3ELeu55vXPZI_J6LIjHpL3f9_uP7pouyNDmrvITzIl14YpcOwEbN8XlW0RorpTUj4g4pXe4HOz37TCi4WJX_sskfPqw13GwACn1SZANM_lD6FpLcCKgitURSoxDStCpQPdhiWH_PiMC - abLRjwZnAXkzQRIODvFRFvrmob4yiLiFpPlsFqy52rFsbTEF4HLzT3aAFZ3Rv8J6KAilS9uSnvHKDLTaoK22WiBbPNAVbAGYbk08ys4L - fGmiGPbhNEUgPN5KYZvWrg6u4EDc40WmNp7YQAOJKz8 - qVY2KEtzT0OGndhyiQYFQW8rURUnm_fujX1OI645x6ObaVOooaD1PRfQG3QUSBNbN_P2iho_j5i2oWdc7BwKBQxpTAV844GUYJabAiJy5WHCvUScVqSOFiQUlRd8X_wIKcNNs3CBRFJ2V95Q";

        public async Task<bool> RegisterUser(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword

            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent($"email={email}&password={password}&confirm_password={confirmPassword}", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await httpClient.PostAsync(baseUrl + "signup", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<TokenResponse> GetToken(string email, string password)
        {
            var httpClient = new HttpClient();
            var content = new StringContent($"grant_type=password&email={email}&password={password}",Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await httpClient.PostAsync(baseUrl + "sessions", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TokenResponse>(jsonResult);
            return result; 
        }

        public async Task<bool> PasswordRecovery(string email)
        {
            var httpClient = new HttpClient();
            var recoverPasswordModel = new PasswordRecoveryModel()
            {
                Email = email
            };
            var json = JsonConvert.SerializeObject(recoverPasswordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(baseUrl + "Users/PasswordRecovery", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var httpClient = new HttpClient();
            var changePasswordModel = new ChangePasswordModel()
            {
                OldPassword = oldPassword, NewPassword = newPassword, ConfirmPassword = confirmPassword
            };
            var json = JsonConvert.SerializeObject(changePasswordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken",""));
            var response = await httpClient.PostAsync(baseUrl + "Account/ChangePassword", content);
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> BecomeAnInstructor(Instructor instructor)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(instructor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.PostAsync(baseUrl + "instructors", content);
            
            return response.StatusCode == HttpStatusCode.Created;
        }

        public async Task<List<Instructor>> GetInstructors()
        {
            var httpClient = new HttpClient();
            Console.WriteLine("Access token: " + Preferences.Get("accesstoken", ""));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.GetStringAsync(baseUrl + "instructors");
            JObject jsonResponse = JObject.Parse(response);
            IList<JToken> results = jsonResponse["data"].Children().ToList();
            List<Instructor> instructorz = new List<Instructor>();
            foreach (JToken result in results)
            {
                var instructorAttributes = result["attributes"];
                InstructorData instructorData = result.ToObject<InstructorData>();
                instructorData.instructorAttributes = instructorAttributes.ToObject<InstructorAttributes>();
                instructorz.Add(CreateInstructorFromData(instructorData));
            }

            return instructorz;
            //return true;
        }

        public async Task<Instructor> GetInstructor(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.GetStringAsync(baseUrl + "instructors/" + id);
            JObject jsonResponse = JObject.Parse(response);
            JToken result = jsonResponse["data"];
            var instructorAttributes = result["attributes"];
            InstructorData instructorData = result.ToObject<InstructorData>();
            instructorData.instructorAttributes = instructorAttributes.ToObject<InstructorAttributes>();
            Instructor instructor = CreateInstructorFromData(instructorData);
            return instructor;
        }

        public async Task<List<Instructor>> SearchInstructors(string subject, string gender, string city)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.GetStringAsync(baseUrl + $"instructors?subject={subject}&gender={gender}&city={city}");
            return JsonConvert.DeserializeObject<List<Instructor>>(response);
        }

        public async Task<List<City>> GetCities()
        {
            var httpClient = new HttpClient();       
            var response = await httpClient.GetStringAsync(baseUrl + "cities");
            return JsonConvert.DeserializeObject<List<City>>(response);
        }

        public async Task<List<Course>> GetCourses()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(baseUrl + "courses");
            return JsonConvert.DeserializeObject<List<Course>>(response);
        }

        private Instructor CreateInstructorFromData(InstructorData instructorData)
        {
            Instructor instructor = new Instructor
            {
                Id = Convert.ToInt32(instructorData.Id),
                Name = instructorData.instructorAttributes.Name,
                Language = instructorData.instructorAttributes.Language,
                Nationality = instructorData.instructorAttributes.Nationality,
                Gender = instructorData.instructorAttributes.Gender,
                Phone = instructorData.instructorAttributes.Phone,
                Email = instructorData.instructorAttributes.Email,
                Education = instructorData.instructorAttributes.Education,
                OneLineTitle = instructorData.instructorAttributes.OneLineTitle,
                Description = instructorData.instructorAttributes.Description,
                Experience = instructorData.instructorAttributes.Experience,
                HourlyRate = instructorData.instructorAttributes.HourlyRate,
                CourseDomain = instructorData.instructorAttributes.CourseDomain,
                City = instructorData.instructorAttributes.City,
                ImagePath = instructorData.instructorAttributes.ImageUrl.Replace("http://localhost:3000/", rootUrl)
            };

            return instructor;

        }


    }
}
