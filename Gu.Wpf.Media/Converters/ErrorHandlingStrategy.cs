namespace Gu.Wpf.Media
{
    /// <summary>
    /// Specifies how conversion errors are handled at runtime.
    /// </summary>
    public enum ErrorHandlingStrategy
    {
        /// <summary>
        /// Throw an exception
        /// </summary>
        Throw,

        /// <summary>
        /// Return Binding.DoNothing
        /// </summary>
        SilentFailure
    }
}