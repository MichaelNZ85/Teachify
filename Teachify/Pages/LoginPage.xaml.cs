using System;
using System.Collections.Generic;
using Teachify.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Teachify.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnLogin_Clicked(System.Object sender, System.EventArgs e)
        {

            ApiService apiService = new ApiService();
            var response = await apiService.GetToken(EntEmail.Text, EntPassword.Text);
            if (string.IsNullOrEmpty(response.access_token))
            {
                await DisplayAlert("Error", "Error logging in", "OK");
                Application.Current.MainPage = new LoginPage();
            }
            else
            {
                Preferences.Set("useremail", EntEmail.Text);
                Preferences.Set("password", EntPassword.Text);
                Preferences.Set("accesstoken", response.access_token);
                Application.Current.MainPage = new MasterPage();
            }
        }

        void BtnShowPwd_Clicked(System.Object sender, System.EventArgs e)
        {
            EntPassword.IsPassword = false;
        }

        void TapSignUp_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignupPage());
        }
    }
}
