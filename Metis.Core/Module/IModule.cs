using System.Threading;
using System.Threading.Tasks;

namespace Metis.Core.Module
{
    /// <summary>
    ///     A module for use in Metis.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        ///     Human-readable name for this module.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Runs the module.
        ///     This operation can potentially run indefinitely; a module may execute a series of commands and then exit, or may run until instructed to stop (see <paramref name="cancellationToken"/>).
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while executing the module.</param>
        /// <returns>A task that represents the asynchronous operation state.</returns>
        public Task RunAsync(CancellationToken cancellationToken);
    }
}
