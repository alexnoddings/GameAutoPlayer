using System;

namespace Metis.Core.Settings
{
    /// <summary>
    ///     Denotes that a property represents a setting which should be persisted.
    ///     Only useful on properties in sub-classes of <see cref="Metis.Core.Settings.ModuleSettings" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SavedSettingAttribute : Attribute
    {
    }
}
