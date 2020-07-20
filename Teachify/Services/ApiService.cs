using System;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Teachify.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;

namespace Teachify.Services
{
    public class ApiService
    {
        string baseUrl = "https://teachify-michael.azurewebsites.net/api/";

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
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(baseUrl + "Account/Register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<TokenResponse> GetToken(string email, string password)
        {
            var httpClient = new HttpClient();
            var content = new StringContent($"grant_type=password&username={email}&password={password}",Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await httpClient.PostAsync(baseUrl + "Token", content);
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
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "Big purrrrrs");
            var response = await httpClient.PostAsync(baseUrl + "Account/ChangePassword", content);
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> BecomeAnInstructor(Instructor instructor)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(instructor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "Big purrrrrs");
            var response = await httpClient.PostAsync(baseUrl + "instructors", content);
            
            return response.StatusCode == HttpStatusCode.Created;
        }

        public async Task<List<Instructor>> GetInstructors()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "Big purrrrrs");
            var response = await httpClient.GetStringAsync(baseUrl + "instructors");
            return JsonConvert.DeserializeObject<List<Instructor>>(response);
        }

        public async Task<Instructor> GetInstructor(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "Big purrrrrs");
            var response = await httpClient.GetStringAsync(baseUrl + "instructors/" + id);
            return JsonConvert.DeserializeObject<Instructor>(response);
        }

        public async Task<List<Instructor>> SearchInstructors(string subject, string gender, string city)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "Big purrrrrs");
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



    }
}
