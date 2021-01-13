using System.Threading.Tasks;

namespace Metis.Core.Interaction
{
    /// <summary>
    ///     Handles sending input to the process.
    /// </summary>
    public interface IInputHandler
    {
        /// <summary>
        ///     Gets a value indicating whether the handler is able to send inputs.
        /// </summary>
        bool CanSend { get; }

        /// <summary>
        ///     Does not accept a cancellation token as this operation is not expected to take long enough.
        /// </summary>
        /// <param name="character">The character to send to the process.</param>
        /// <returns>A task that represents the asynchronous operation state.</returns>
        Task SendKeyAsync(char character);
    }
}
