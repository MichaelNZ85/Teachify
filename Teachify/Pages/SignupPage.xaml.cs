using System;
using System.Collections.Generic;
using Teachify.Services;
using Xamarin.Forms;

namespace Teachify.Pages
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnSignup_Clicked(System.Object sender, System.EventArgs e)
        {
            ApiService apiService = new ApiService();
            bool response = await apiService.RegisterUser(EntEmail.Text, EntPassword.Text, EntConfirmPassword.Text);
            if (!response)
            {
                await DisplayAlert("Error", "Something went wrong", "Cancel");
            }
            else
            {
                await DisplayAlert("Success", "Account created successfully", "OK");
                await Navigation.PopToRootAsync();
            }
        }
    }
}
