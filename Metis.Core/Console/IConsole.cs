namespace Metis.Core.Console
{
    /// <summary>
    ///     Displays messages to the system's user.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        ///     Writes a line of text to the display.
        /// </summary>
        /// <param name="text">The text to display.</param>
        public void WriteLine(string text);
    }
}
