using Prism.Commands;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace SmartAGSolutionApp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IDataProvider dataProvider;

        public SettingsPageViewModel(INavigationService navigationService, IDataProvider dataProvider)
            : base(navigationService, dataProvider)
        {
            this.navigationService = navigationService;
            this.dataProvider = dataProvider;
            this.Title = "Settings";
            this.ClearCacheCommand = new DelegateCommand(() => this.ClearCache());
        }

        #region Properties

        public DelegateCommand ClearCacheCommand { get; set; }

        #endregion

        private async void ClearCache()
        {
            bool confirmed = await Application.Current.MainPage.DisplayAlert(
                "Warning", "This operation will delete all profiles and history of measurements. Are you sure you want to continue?",
                "Yes", "No");

            if (confirmed)
            {
                this.dataProvider.ClearCache();
                await this.navigationService.GoBackAsync();
            }
        }
    }
}
