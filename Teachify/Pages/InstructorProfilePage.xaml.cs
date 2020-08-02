using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teachify.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teachify.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructorProfilePage : ContentPage
    {
        private string phoneNumber;
        private string emailAddress;
        public InstructorProfilePage(int id)
        {
            InitializeComponent();
            GetInstructorProfile(id);
            BusyIndicator.IsRunning = false;
        }

        public async void GetInstructorProfile(int id)
        {
            ApiService apiService = new ApiService();
            var instructor = await apiService.GetInstructor(id);
            ImgProfile.Source = instructor.ImagePath;
            LblCity.Text = instructor.City;
            LblLanguage.Text = instructor.Language;
            LblNationality.Text = instructor.Nationality;
            LblExperience.Text = instructor.Experience;
            LblSpecialization.Text = instructor.CourseDomain;
            LblName.Text = instructor.Name;
            LblOneLineDescription.Text = instructor.OneLineTitle;
            LblHourlyRate.Text = instructor.HourlyRate;
            LblDescription.Text = instructor.Description;
            phoneNumber = instructor.Phone;
            emailAddress = instructor.Email;
        }

        private void BtnCall_Clicked(object sender, EventArgs e)
        {
            PhoneDialer.Open(phoneNumber);
        }

        private void BtnSms_Clicked(object sender, EventArgs e)
        {
            var message = new SmsMessage("", phoneNumber);
            Sms.ComposeAsync(message);
        }

        private void BtnEmail_Clicked(object sender, EventArgs e)
        {
            var message = new EmailMessage("", "", emailAddress);
            Email.ComposeAsync(message);
        }
    }
}