using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using Ninject;

namespace WatermarkingService
{
    public class NinjectServiceHost:ServiceHost
    {
        public NinjectServiceHost(IKernel Kernel, Type ServiceType, params Uri[] BaseAddresses) 
            :base(ServiceType, BaseAddresses)
        {
            if (Kernel == null)
            {
                throw new ArgumentNullException("Kernel is null.");
            }

            foreach(var cd in ImplementedContracts.Values)
            {
                cd.ContractBehaviors.Add(new NinjectInstanceProvider(Kernel));
            }
        }
    }
}   