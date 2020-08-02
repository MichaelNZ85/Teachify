using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using Teachify.Helpers;
using Teachify.Models;
using Teachify.Services;
using Xamarin.Forms;

namespace Teachify.Pages
{
    public partial class BecomeInstructorPage : ContentPage
    {
        private MediaFile file;
        public BecomeInstructorPage()
        {
            InitializeComponent();
        }

        private async void TapCamera_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera available", "Okey dokey");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Custom,
                CustomPhotoSize = 30,
                CompressionQuality = 60,
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "Okey dokey");

            ImgProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void BtnApply_Clicked(object sender, EventArgs e)
        {
            var imageArray = FileHelper.ReadFully(file.GetStream());
            file.Dispose();
            var instructor = new Instructor()
            {
                Name = EntName.Text,
                Language = EntLanguage.Text,
                Nationality = EntNationality.Text,
                Gender = PickerGender.Items[PickerGender.SelectedIndex],
                Phone = EntPhone.Text,
                Email = EntEmail.Text,
                Education = EntEducation.Text,
                Experience = PickerExperience.Items[PickerExperience.SelectedIndex],
                HourlyRate = PickerHourlyRate.Items[PickerHourlyRate.SelectedIndex],
                CourseDomain = PickerCourseDomain.Items[PickerCourseDomain.SelectedIndex],
                City = PickerCity.Items[PickerCity.SelectedIndex],
                OneLineTitle = EntOneLineTitle.Text,
                Description = EdtDescription.Text,
                ImageArray = imageArray,
                ImagePath = ""
            };
            ApiService service = new ApiService();
            var response = await service.BecomeAnInstructor(instructor);
            if (!response)
            {
                await DisplayAlert("ERROR", "Does not compute", "EXTERMINATE!");
            }
            else
            {
                await DisplayAlert("Congratulations", "You are now an instructor. You have also been transformed into a cat", "Chur bro!");
            }
        }
    }
}
