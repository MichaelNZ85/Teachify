using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Teachify.Models;
using Teachify.Services;
using Xamarin.Forms;

namespace Teachify.Pages
{
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<Instructor> Instructors;
        public HomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ApiService apiService = new ApiService();
            var instructors = await apiService.GetInstructors();
            foreach (var instructor in instructors)
            {
                Instructors.Add(instructor);
            }
            LvInstructors.ItemsSource = Instructors;
        }
    }
}
