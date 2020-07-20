using System;
using System.Collections.Generic;
using Teachify.Services;
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
            if(string.IsNullOrEmpty(response.access_token))
            {
                await DisplayAlert("Error", "Somethign went wrong", "OK");
            }
            else
            {
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
