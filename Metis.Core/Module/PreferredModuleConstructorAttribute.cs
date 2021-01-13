using System;

namespace Metis.Core.Module
{
    /// <summary>
    ///     Specifies that a module's constructor should be used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public sealed class PreferredModuleConstructorAttribute : Attribute
    {
    }
}
