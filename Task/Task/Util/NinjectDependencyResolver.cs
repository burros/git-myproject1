using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Services;

namespace Task.Util
{
    //пробував створити свій власний клас NinjectDependencyResolver,
    //але через те, що метод WithConstructorArgument потрібно використовувати в самому контролері
    //цей клас не зовсім доцільний для мого варіанту реалізації
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IWeatherManager>().To<WeatherManager>();
        }
    }
}