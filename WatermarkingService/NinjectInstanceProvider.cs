using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Wcf;
using Ninject.Activation.Caching;
using WatermarkingLib;
using Ninject.Web.Common;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WatermarkingService
{
    public class NinjectInstanceProvider:IInstanceProvider,IContractBehavior
    {
        private readonly IKernel _kernel;

        public NinjectInstanceProvider(IKernel Kernel)
        {
            _kernel=Kernel;
        }

        public object GetInstance(InstanceContext instanceContext, Message Message)
        {
            return GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext InstanceContext)
        {
            return _kernel.Get(InstanceContext.Host.Description.ServiceType);
        }

        public void ReleaseInstance(InstanceContext InstanceContext, object instance)
        {
            _kernel.Release(instance);
        }

        public void AddBindingParameters(ContractDescription ContractDescriptn, ServiceEndpoint EndPoint, BindingParameterCollection BindingParams)
        {         
        }

        public void ApplyClientBehavior(ContractDescription Description, ServiceEndpoint EndPoint, ClientRuntime RunTime)
        {         
        }

        public void ApplyDispatchBehavior(ContractDescription Description, ServiceEndpoint EndPoint, DispatchRuntime Runtime)
        {
            Runtime.InstanceProvider = this;
        }

        public void Validate(ContractDescription Description, ServiceEndpoint EndPoint)
        {             
        }      
    }
}