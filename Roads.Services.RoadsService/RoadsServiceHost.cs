using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Roads.Common.Integrations;

namespace Roads.Services.RoadsService
{
    /// <summary>
    /// 
    /// </summary>
    public class RoadsServiceHost:ServiceHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsServiceHost"/> class.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="languagesManager">The languages manager.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        /// <exception cref="System.ArgumentNullException">
        /// resourceManager
        /// or
        /// languagesManager
        /// </exception>
        public RoadsServiceHost(IResourceManager resourceManager,ILanguagesManager languagesManager,
            Type serviceType, params Uri[] baseAddresses )
            : base(serviceType, baseAddresses)
        {
            if (resourceManager == null)
            {
                throw new ArgumentNullException("resourceManager");
            }

            if (languagesManager == null)
            {
                throw new ArgumentNullException("languagesManager");
            }

            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new RoadsInstanceProvider(resourceManager, languagesManager));
            }
        }
    }
}
