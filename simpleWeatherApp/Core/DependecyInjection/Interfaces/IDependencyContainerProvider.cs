using CommonServiceLocator;

namespace simpleWeatherApp.Core.DependecyInjection.Interfaces
{
    public interface IDependencyContainerProvider : ICustomContainer, IServiceLocator
    {
        
    }
}
