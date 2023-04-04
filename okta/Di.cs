using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.Wpf
{
    public static class Di
    {
        private static StandardKernel kernel;

        public static T Get<T>(Action<StandardKernel>? kernelInitializer = null)
        {
            if (kernelInitializer == null)
            {
                kernelInitializer = App.InitializeDi;
            }
            if (kernel == null)
            {
                kernel = new StandardKernel();
                kernelInitializer(kernel);
            }
            return kernel.Get<T>();
        }
    }
}
