using System;

namespace Roads.Common.Integrations
{
    public interface IRepositoryBase : IDisposable
    {
        /// <summary>
        /// Saves all changes to DataContext
        /// </summary>
        void Save();
    }
}
