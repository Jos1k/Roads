using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using Roads.Common.Integrations;

namespace Roads.Services.RoadsService
{
    /// <summary>
    /// Instanse provider for Roads Service.
    /// Get instance of WCF Service with parameters.
    /// </summary>
    public class RoadsInstanceProvider : IInstanceProvider, IContractBehavior
    {

        /// <summary>
        /// The resource manager
        /// </summary>
        private readonly IResourceManager resourceManager;
        /// <summary>
        /// The languages manager
        /// </summary>
        private readonly ILanguagesManager languagesManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsInstanceProvider"/> class.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="languagesManager">The languages manager.</param>
        /// <exception cref="System.ArgumentNullException">
        /// resourceManager
        /// or
        /// languagesManager
        /// </exception>
        public RoadsInstanceProvider(IResourceManager resourceManager, ILanguagesManager languagesManager)
        {

            if (resourceManager == null)
            {
                throw new ArgumentNullException("resourceManager");
            }

            if (languagesManager == null)
            {
                throw new ArgumentNullException("languagesManager");
            }

            this.resourceManager = resourceManager;
            this.languagesManager = languagesManager;
        }

        #region IInstanceProvider Members
        /// <summary>
        /// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext" /> object.
        /// </summary>
        /// <param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext" /> object.</param>
        /// <param name="message">The message that triggered the creation of a service object.</param>
        /// <returns>
        /// The service object.
        /// </returns>
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.GetInstance(instanceContext);
        }

        /// <summary>
        /// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext" /> object.
        /// </summary>
        /// <param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext" /> object.</param>
        /// <returns>
        /// A user-defined service object.
        /// </returns>
        public object GetInstance(InstanceContext instanceContext)
        {
            return new RoadsService(this.resourceManager, this.languagesManager);
        }

        /// <summary>
        /// Called when an <see cref="T:System.ServiceModel.InstanceContext" /> object recycles a service object.
        /// </summary>
        /// <param name="instanceContext">The service's instance context.</param>
        /// <param name="instance">The service object to be recycled.</param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
        #endregion

        #region IContractBehavior Members
        /// <summary>
        /// Configures any binding elements to support the contract behavior.
        /// </summary>
        /// <param name="contractDescription">The contract description to modify.</param>
        /// <param name="endpoint">The endpoint to modify.</param>
        /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Implements a modification or extension of the client across a contract.
        /// </summary>
        /// <param name="contractDescription">The contract description for which the extension is intended.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="clientRuntime">The client runtime.</param>
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        /// <summary>
        /// Implements a modification or extension of the client across a contract.
        /// </summary>
        /// <param name="contractDescription">The contract description to be modified.</param>
        /// <param name="endpoint">The endpoint that exposes the contract.</param>
        /// <param name="dispatchRuntime">The dispatch runtime that controls service execution.</param>
        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        /// <summary>
        /// Implement to confirm that the contract and endpoint can support the contract behavior.
        /// </summary>
        /// <param name="contractDescription">The contract to validate.</param>
        /// <param name="endpoint">The endpoint to validate.</param>
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
        #endregion
    }
}
