using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartAGSolutionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void ImgbtnSerbianFlag_Clicked(object sender, EventArgs e)
        {
            this.fSerbianFlag.BackgroundColor = Color.Silver;
            this.fUSAFlag.BackgroundColor = Color.White;
        }

        private void ImgbtnUSAFlag_Clicked(object sender, EventArgs e)
        {
            this.fSerbianFlag.BackgroundColor = Color.White;
            this.fUSAFlag.BackgroundColor = Color.Silver;
        }
    }
}