
using Xamarin.Forms;

namespace simpleWeatherApp.Core.Base.VIews
{
    public partial class BasePage : ContentPage
    {
        #region Protected Fields

        protected readonly BaseViewModel ViewModel;

        #endregion

        #region Constructor

        public BasePage(BaseViewModel viewModel)
        {
            ViewModel = viewModel;
            BindingContext = viewModel;

            InitializeComponent();
        }

        #endregion
    }
}
