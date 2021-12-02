using System;
using CommonServiceLocator;
using simpleWeatherApp.Core.DependecyInjection.Implementations;
using simpleWeatherApp.Core.DependecyInjection.Interfaces;

namespace simpleWeatherApp.Core.DependecyInjection
{
    public class DependencyManager
    {
        #region Private Fields

        private readonly IDependencyContainerProvider _singletonDependencyContainerProvider;

        #endregion

        #region Constructors

        private DependencyManager()
        {
            _singletonDependencyContainerProvider = new UnityCustomImplementation();
        }

        #endregion

        #region Properties

        public static DependencyManager Instance { get; } = new DependencyManager();

        public ICustomContainer Container => _singletonDependencyContainerProvider;

        public IServiceLocator ServiceLocator => _singletonDependencyContainerProvider;

        #endregion
    }
}
