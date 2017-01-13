using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using Ninject;
using WatermarkingLib;

namespace WatermarkingService
{
    public class NinjectServiceHostFactory:ServiceHostFactory
    {
        private readonly IKernel kernel;

        public NinjectServiceHostFactory() {
            kernel = new StandardKernel();
            kernel.Bind<IWaterMark>().To<WatermarkingUtility>();
            kernel.Bind<IWaterMarkService>().To<WaterMarkService>();
        }

        protected override System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new NinjectServiceHost(kernel,
                                    serviceType, 
                                    baseAddresses);
        }


    }
}