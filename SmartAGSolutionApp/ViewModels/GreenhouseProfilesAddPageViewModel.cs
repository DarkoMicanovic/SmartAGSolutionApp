using Prism.Commands;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using SmartAGSolutionApp.Model;
using Xamarin.Forms;

namespace SmartAGSolutionApp.ViewModels
{
    public class GreenhouseProfilesAddPageViewModel : ViewModelBase, INavigationAware
    {
        private INavigationService navigationService;
        private IDataProvider dataProvider;

        public GreenhouseProfilesAddPageViewModel(INavigationService navigationService, IDataProvider dataProvider)
            : base(navigationService, dataProvider)
        {
            this.navigationService = navigationService;
            this.dataProvider = dataProvider;

            Title = "Add Greenhouse profile";
            AddGreenhouseProfileCommand = new DelegateCommand(() => AddGreenhouseProfile());
        }

        #region Properties

        public DelegateCommand AddGreenhouseProfileCommand { get; set; }

        public string NewName { get; set; }

        public string NewPhoneNumber { get; set; }

        public string NewDescription { get; set; }

        #endregion

        #region INavigationAware Members

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        #endregion

        private void AddGreenhouseProfile()
        {
            if (string.IsNullOrWhiteSpace(NewPhoneNumber))
                Application.Current.MainPage.DisplayAlert("Warning", "Phone number field cannot be emtpy", "Ok");
            else

                dataProvider.AddGreenhouse(new Greenhouse(NewPhoneNumber, NewName, NewDescription));
            navigationService.GoBackAsync();
        }
    }
}
