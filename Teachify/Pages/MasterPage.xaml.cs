using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Teachify.Pages
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        void TapHome_Tapped(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        void TapChangePassword_Tapped(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new ChangePasswordPage());
            IsPresented = false;
        }

        void TapBecomeInstructor_Tapped(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new BecomeInstructorPage());
            IsPresented = false;
        }

        void TapFaq_Tapped(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new FaqPage());
            IsPresented = false;
        }

        void TapLogout_Tapped(System.Object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new BecomeInstructorPage());
            IsPresented = false;
        }
    }
}