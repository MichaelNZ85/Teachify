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
            //if(string.IsNullOrEmpty(response.access_token))
            //{
                await DisplayAlert("Error", "Using Test Data", "OK");
                // THE FOLLOWING LINES ARE TEST CODE UNTIL SIGNING IN WITH THE API WORKS. DELETE FOR PRODUCTION
                Preferences.Set("useremail","cat@dog.com");
                Preferences.Set("password","Meow55555!");
                Preferences.Set("accesstoken", "ICefJEUg18rEOv06IJmb0URwIzP2dJudvSKdUJ6aVtCFQYvmspc50eLk3AeRfXscN8m1x14RQQjLJBRHFspatdpKvA7KhIYW6H8dQ5VRT1mIsKR_HWpnpIxJmkyvfcymxH0XID1HSIJjCg7xeNEfmniNsx6ctukoa3zGVI5OXC5HDn5UswBxMcPuVbHPltkyvHuMvniKXC0o0f7_Swt231kh75rqfFGD2C0dVGlbLxjhaqdBgHGssCyW0d7yqRcf5iYzQNChJlS52XuF_JgU5Q0JJIYamIZg9uXNnYUb2amcfnno_j9oZg7kL9sjJyBRdD4_zfsYRakUgM0yzfE19o_fXE8G2IVHUEZ5NytFQfnQA1l2NjyP9-_KwulHm3vOP7IaioY-ahxRLQnJ1AgNG9poBebETSPJDP4DATSwiaGwnPOTcuKcWB6L7VVMC1Qcx91gynwgE8uJjyU7DeSXzaPkBpu25dy-W5fMoo5oUls");
                Application.Current.MainPage = new MasterPage();
            //}
            //else
            //{
            //    Preferences.Set("useremail", EntEmail.Text);
            //    Preferences.Set("password", EntPassword.Text);
            //    Preferences.Set("accesstoken", response.access_token);
            //    Application.Current.MainPage = new MasterPage();
            //}
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
