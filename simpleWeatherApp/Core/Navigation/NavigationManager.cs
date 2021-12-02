using simpleWeatherApp.Core.DependecyInjection;

namespace simpleWeatherApp.Core.Navigation
{
    public class NavigationManager
    {
        #region Constructors

        private NavigationManager() { }

        static NavigationManager()
        {
            Instance = DependencyManager.Instance.ServiceLocator.GetInstance<INavigationService>();
        }

        #endregion

        #region Instance

        public static INavigationService Instance { get; private set; }

        #endregion
    }
}
