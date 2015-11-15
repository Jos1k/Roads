using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Roads.Common.Integrations;
using Roads.Common.Managers;

namespace Roads.Services.RoadsService
{
    /// <summary>
    /// Service Host Factory implementation to pass parameters into ctor for WCF Service
    /// </summary>
    public class RoadsServiceHostFactory: ServiceHostFactory
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
        /// Initializes a new instance of the <see cref="RoadsServiceHostFactory"/> class.
        /// </summary>
        public RoadsServiceHostFactory()
        {
            this.resourceManager = new ResourceManager();
            this.languagesManager = new LanguagesManager();
        }

        /// <summary>
        /// Creates a <see cref="T:System.ServiceModel.ServiceHost" /> for a specified type of service with a specific base address.
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host.</param>
        /// <param name="baseAddresses">The <see cref="T:System.Array" /> of type <see cref="T:System.Uri" /> that contains the base addresses for the service hosted.</param>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.ServiceHost" /> for the type of service specified with a specific base address.
        /// </returns>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return  new RoadsServiceHost(this.resourceManager, this.languagesManager, serviceType, baseAddresses);
        }
    }
}
