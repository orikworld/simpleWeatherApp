namespace simpleWeatherApp.Core.DependecyInjection
{
    public enum LifetimeCycle
    {
        NewInstance = 0,
        SingletonInstance = 1,
        PerThreadInstance = 2,
    }
}
