using System;
using System.Reflection;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Metis.Application.ViewModels
{
    internal class BaseViewModel : ViewModelBase
    {
        private const BindingFlags BindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        ///     Specifies that a property should be injected during construction.
        /// </summary>
        /// <remarks>Attribute is only useful on view models.</remarks>
        [AttributeUsage(AttributeTargets.Property)]
        protected sealed class AutoInjectAttribute : Attribute
        {
            /// <summary>
            ///     Whether this is required.
            ///     If required, an exception will be thrown if the service is not found.
            ///     If not required, null will be injected if the service is not found. 
            /// </summary>
            public bool IsRequired { get; }

            /// <summary>
            ///     The registered type of the service to be injected.
            /// </summary>
            /// <remarks>
            ///     Used when registering Xy\Service as IService but one class requires it as a XyzService.
            /// </remarks>
            public Type? RegisteredType { get; }

            public AutoInjectAttribute(bool isRequired = true, Type? registeredType = null)
            {
                IsRequired = isRequired;
                RegisteredType = registeredType;
            }
        }

        public BaseViewModel()
        {
            InjectDependencies();
        }

        private void InjectDependencies()
        {
            PropertyInfo[] properties = GetType().GetProperties(BindingAttr);
            foreach (PropertyInfo property in properties)
            {
                var attr = property.GetCustomAttribute<AutoInjectAttribute>();
                if (attr == null) continue;
                Type targetType = attr.RegisteredType ?? property.PropertyType;

                object? instance;
                if (attr.IsRequired)
                {
                    instance = SimpleIoc.Default.GetInstance(targetType);
                }
                else
                {
                    try
                    {
                        instance = SimpleIoc.Default.GetInstance(targetType);
                    }
                    catch (InvalidOperationException e) when (e.Message.Contains("Type not found in cache"))
                    {
                        instance = null;
                    }
                }
                property.SetValue(this, instance);
            }
        }
    }
}
