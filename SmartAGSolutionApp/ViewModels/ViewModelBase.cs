using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SmartAGSolutionApp.Data;
using SmartAGSolutionApp.Model;

namespace SmartAGSolutionApp.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        private string title;

        public ViewModelBase(INavigationService navigationService, IDataProvider dataProvider)
        {
            NavigationService = navigationService;
            DataProvider = dataProvider;
        }

        #region Properties

        protected INavigationService NavigationService { get; private set; }
        protected IDataProvider DataProvider { get; private set; }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        #endregion
    }
}
