using Prism.Commands;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace SmartAGSolutionApp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase, INavigationAware
    {
        private INavigationService navigationService;
        private IDataProvider dataProvider;
        private Color serbianLanguageIsSelectedColor;
        private Color englishLanguageIsSelectedColor;

        public SettingsPageViewModel(INavigationService navigationService, IDataProvider dataProvider)
            : base(navigationService, dataProvider)
        {
            this.navigationService = navigationService;
            this.dataProvider = dataProvider;
            this.Title = string.Empty;
            this.ClearCacheCommand = new DelegateCommand(() => this.ClearCache());
            this.SerbianLanguageSelectedCommand = new DelegateCommand(() => SerbianLanguageSelected());
            this.EnglishLanguageSelectedCommand = new DelegateCommand(() => EnglishLanguageSelected());
        }

        #region Properties

        public DelegateCommand ClearCacheCommand { get; set; }

        public DelegateCommand SerbianLanguageSelectedCommand { get; set; }

        public DelegateCommand EnglishLanguageSelectedCommand { get; set; }

        public Color SerbianLanguageIsSelectedColor
        {
            get { return this.serbianLanguageIsSelectedColor; }
            set { SetProperty(ref this.serbianLanguageIsSelectedColor, value); }
        }

        public Color EnglishLanguageIsSelectedColor
        {
            get { return this.englishLanguageIsSelectedColor; }
            set { SetProperty(ref this.englishLanguageIsSelectedColor, value); }
        }

        public LanguageSelectionType SelectedLanguage
        {
            get { return this.dataProvider.SelectedLanguage; }
        }

        #endregion

        #region INavigationAware Members

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            this.SetUpSelectedLanguageColor();
        }

        #endregion

        private void SetUpSelectedLanguageColor()
        {
            LanguageSelectionType selectedLanguage = this.dataProvider.SelectedLanguage;
            switch (selectedLanguage)
            {
                case LanguageSelectionType.Serbian:
                    {
                        this.SerbianLanguageIsSelectedColor = Color.Silver;
                        this.EnglishLanguageIsSelectedColor = Color.White;
                        break;
                    }
                case LanguageSelectionType.English:
                    {
                        this.EnglishLanguageIsSelectedColor = Color.Silver;
                        this.SerbianLanguageIsSelectedColor = Color.White;
                        break;
                    }
            }
        }

        private async void ClearCache()
        {
            bool confirmed = await this.ClearCachedFilesDisplayWarning();

            if (confirmed)
            {
                this.dataProvider.ClearCache();
                await this.navigationService.GoBackAsync();
            }
        }

        private async Task<bool> ClearCachedFilesDisplayWarning()
        {
            string titleMessage = LocalizationResourceManager.Current.GetValue("Warning");
            string message = LocalizationResourceManager.Current.GetValue("This_operation_will_delete_all_profiles_and_history_of_measurements");
            string yesMessage = LocalizationResourceManager.Current.GetValue("Yes");
            string noMessage = LocalizationResourceManager.Current.GetValue("No");
            return await Application.Current.MainPage.DisplayAlert(titleMessage, message, yesMessage,noMessage);
        }

        private void SerbianLanguageSelected()
        {
            this.dataProvider.SelectedLanguage = LanguageSelectionType.Serbian;
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("sr");
        }

        private void EnglishLanguageSelected()
        {
            this.dataProvider.SelectedLanguage = LanguageSelectionType.English;
            LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("en");
        }
    }
}
