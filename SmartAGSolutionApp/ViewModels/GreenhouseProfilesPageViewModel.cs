using Prism.Commands;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using SmartAGSolutionApp.Model;
using System;
using System.Collections.ObjectModel;

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

            Title = "Select greenhouse profile";
            this.GreenhouseCollection = this.dataProvider.GetGreenhouseCollection();
            this.AddGreenhouseProfilesCommand = new DelegateCommand(() => AddGreenhouse());
            this.DeleteSelectedGreenHouseProfileCommand = new DelegateCommand(() => DeleteSelectedGreenHouseProfile());

        }

        #region Properties
        
        public DelegateCommand AddGreenhouseProfilesCommand { get; set; }

        public DelegateCommand DeleteSelectedGreenHouseProfileCommand { get; set; }

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

        #region INavigationAware Members

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            this.SelectedItem = dataProvider.ActiveGreenhouse;
        }

        #endregion

        private void AddGreenhouse()
        {
            this.navigationService.NavigateAsync("GreenhouseProfilesAddPage");
            
        }

        private void DeleteSelectedGreenHouseProfile()
        {
            this.dataProvider.RemoveGreenhouse(this.SelectedItem);
        }
    }
}
