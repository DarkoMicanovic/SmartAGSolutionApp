using Prism.Commands;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using SmartAGSolutionApp.Model;
using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.Helpers;

namespace SmartAGSolutionApp.ViewModels
{
    class GreenhouseProfilesPageViewModel : ViewModelBase, INavigationAware
    {
        private INavigationService navigationService;
        private IDataProvider dataProvider;
        private Greenhouse activeGreenhouse;

        public GreenhouseProfilesPageViewModel(INavigationService navigationService, IDataProvider dataProvider)
            : base(navigationService, dataProvider)
        {
            this.navigationService = navigationService;
            this.dataProvider = dataProvider;

            Title = LocalizationResourceManager.Current.GetValue("Select_greenhouse_profile");
            this.GreenhouseCollection = this.dataProvider.GetGreenhouseCollection();
            this.AddGreenhouseProfilesCommand = new DelegateCommand(() => this.AddGreenhouse());
            this.DeleteSelectedGreenhouseProfileCommand = new DelegateCommand<string>((greenhouseName) => this.DeleteSelectedGreenhouseProfile(greenhouseName));
            this.ModifySelectedGreenhouseProfileCommand = new DelegateCommand<string>((id) => this.ModifySelectedGreenhouseProfile(id));
        }

        #region Properties
        
        public DelegateCommand AddGreenhouseProfilesCommand { get; set; }

        public DelegateCommand<string> DeleteSelectedGreenhouseProfileCommand { get; set; }

        public DelegateCommand<string> ModifySelectedGreenhouseProfileCommand { get; set; }

        public ObservableCollection<Greenhouse> GreenhouseCollection { get; set; }

        public Greenhouse SelectedItem
        {
            get { return this.activeGreenhouse; }
            set
            {
                SetProperty(ref this.activeGreenhouse, value);
                this.dataProvider.SetActiveGreenhouse(this.activeGreenhouse);
            }
        }

        #endregion

        private void ReloadGreenhouseCollection()
        {
            this.GreenhouseCollection = this.dataProvider.GetGreenhouseCollection();
        }

        #region INavigationAware Members

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            this.SelectedItem = this.dataProvider.ActiveGreenhouse;

            NavigationMode navigationMode = parameters.GetNavigationMode();
            if (navigationMode == NavigationMode.Back)
                this.ReloadGreenhouseCollection();

        }

        #endregion

        private void AddGreenhouse()
        {
            this.navigationService.NavigateAsync("GreenhouseProfilesAddPage");
        }

        private void DeleteSelectedGreenhouseProfile(string greenhouseName)
        {
            this.dataProvider.RemoveGreenhouse(greenhouseName);
            this.navigationService.GoBackAsync();
        }

        private void ModifySelectedGreenhouseProfile(string id)
        {
            var parameters = new NavigationParameters();
            parameters.Add("CanEdit", false);
            parameters.Add("ID", id);
            this.navigationService.NavigateAsync("GreenhouseProfilesAddPage", parameters);
        }
    }
}
