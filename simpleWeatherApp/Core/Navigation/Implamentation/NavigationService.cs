using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using simpleWeatherApp.Core.Base;
using simpleWeatherApp.Core.DependecyInjection;
using simpleWeatherApp.Models.Enum;
using Xamarin.Forms;

namespace simpleWeatherApp.Core.Navigation
{
    public class NavigationService : INavigationService
    {
        #region Constructors

        public NavigationService(INavigation navigation)
        {
            FormsNavigation = navigation;
            ModalStack = navigation.ModalStack;
            NavigationStack = navigation.NavigationStack;
        }

        #endregion

        #region Properties

        public IReadOnlyList<Page> ModalStack { get; private set; }

        public IReadOnlyList<Page> NavigationStack { get; private set; }

        public INavigation FormsNavigation { get; private set; }

        #endregion

        #region Public Methods

        public void InsertPageBefore(Page page, Page before)
        {
            FormsNavigation.InsertPageBefore(page, before);
        }

        public Task<Page> PopAsync()
        {
            return FormsNavigation.PopAsync();
        }

        public Task<Page> PopAsync(bool animated)
        {
            return FormsNavigation.PopAsync(animated);
        }

        public Task<Page> PopModalAsync()
        {
            return FormsNavigation.PopModalAsync();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return FormsNavigation.PopModalAsync(animated);
        }

        public Task PopToRootAsync()
        {
            return FormsNavigation.PopToRootAsync();
        }

        public Task PopToRootAsync(bool animated)
        {
            return FormsNavigation.PopToRootAsync(animated);
        }

        public Task PushAsync(Page page)
        {
            return FormsNavigation.PushAsync(page);
        }

        public Task PushAsync(Page page, bool animated)
        {
            return FormsNavigation.PushAsync(page, animated);
        }

        public Task PushModalAsync(Page page)
        {
            return FormsNavigation.PushModalAsync(page);
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            return FormsNavigation.PushModalAsync(page, animated);
        }
      
        public void RemovePage(Page page)
        {
            FormsNavigation.RemovePage(page);
        }

        public Task NavigateTo(ViewId viewId, bool animated = false)
        {
            var page = DependencyManager.Instance.ServiceLocator.GetInstance<Page>(viewId.ToString());

            return FormsNavigation.PushAsync(page, animated);
        }

        #endregion
    }
}
