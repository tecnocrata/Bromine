using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Bromine.Core
{
    public class PageFactory
    {
        private static IUnityContainer myContainer = new UnityContainer();
        private static PageFactory instance;

        public static PageFactory Instance
        {
            get { return instance ?? (instance = new PageFactory()); }
        }

        public void Register(Type managerType, BrowserType type)
        {
            myContainer.RegisterType(typeof(IAutomationManager), managerType, new ContainerControlledLifetimeManager(), new InjectionConstructor(type));
        }

        public T CreatePage<T>() where T : BasePage
        {
            return myContainer.Resolve<T>();
        }
    }
  
}
