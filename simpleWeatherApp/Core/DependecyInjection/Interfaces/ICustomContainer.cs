
using System;
using System.Collections.Generic;

namespace simpleWeatherApp.Core.DependecyInjection.Interfaces
{
    public interface ICustomContainer
    {
        void RegisterInstance<TInterface>(TInterface instance);

        void RegisterDependency<TFrom, TTo>() where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(string name) where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(LifetimeCycle lifetimeManager) where TTo : TFrom;

        void RegisterDependency<TFrom, TTo>(string name, LifetimeCycle lifetimeManager) where TTo : TFrom;

        void RegisterDependency<TFrom, TTo, TToBeInjected>(string name, string typeIdentifier = null)
            where TTo : TFrom;

    }
}
