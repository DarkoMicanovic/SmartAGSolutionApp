using Prism.Commands;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using SmartAGSolutionApp.Model;
using System;
using System.Linq;
using Xamarin.Forms;

namespace SmartAGSolutionApp.ViewModels
{
    public class GreenhouseProfilesAddPageViewModel : ViewModelBase, INavigationAware
    {
        private INavigationService navigationService;
        private IDataProvider dataProvider;
        private bool canEdit;
        private string buttonText;
        private string newName;
        private string id;

        public GreenhouseProfilesAddPageViewModel(INavigationService navigationService, IDataProvider dataProvider)
            : base(navigationService, dataProvider)
        {
            this.navigationService = navigationService;
            this.dataProvider = dataProvider;

            this.Title = this.ButtonText = "Add Greenhouse profile";
            this.CanEdit = true;
            this.id = string.Empty;
            AddGreenhouseProfileCommand = new DelegateCommand(() => AddGreenhouseProfile());
        }

        #region Properties

        public DelegateCommand AddGreenhouseProfileCommand { get; set; }

        public bool CanEdit
        {
            get { return this.canEdit; }
            set { SetProperty(ref this.canEdit, value); }
        }

        public string NewName
        {
            get { return this.newName; }
            set { SetProperty(ref this.newName, value); }
        }

        public string ButtonText
        {
            get { return this.buttonText; }
            set { SetProperty(ref this.buttonText, value); }
        }

        public string NewPhoneNumber { get; set; }

        public string NewDescription { get; set; }

        #endregion

        #region INavigationAware Members

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("CanEdit"))
            {
                this.id = parameters.GetValue<string>("ID");
                this.CanEdit = parameters.GetValue<bool>("CanEdit");
                this.ButtonText = this.Title = parameters.GetValue<string>("ButtonText");
                Greenhouse greenhouse = this.dataProvider.GetGreenhouseCollection().First(g => g.ID == Guid.Parse(this.id));
                this.NewName = greenhouse.Name;
                this.NewPhoneNumber = greenhouse.PhoneNumber;
                this.NewDescription = greenhouse.Description;
            }
        }

        #endregion

        private void AddGreenhouseProfile()
        {
            if (string.IsNullOrEmpty(this.NewPhoneNumber) || string.IsNullOrEmpty(this.NewName))
                Application.Current.MainPage.DisplayAlert("Warning", "Name or phone number field cannot be emtpy", "Ok");
            else if (this.canEdit)
                this.dataProvider.AddGreenhouse(new Greenhouse(this.NewPhoneNumber, this.NewName, this.NewDescription));
            else
            {
                Guid greenhouseID  = this.dataProvider.FindGreenhouse(Guid.Parse(this.id));
                this.dataProvider.ApplyModify(new Greenhouse(this.NewPhoneNumber, this.NewName, this.NewDescription) { ID = greenhouseID });
            }

            this.navigationService.GoBackAsync();
        }
    }
}
