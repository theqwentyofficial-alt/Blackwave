using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace pcdatacollector.PCData.Managers.OSManagers
{
    public static class RegistryManager
    {
        public static ValueType? GetReg<ValueType>(string regPath, string valueName)
        {
            ValueType? data = (ValueType?)Registry.GetValue(regPath, valueName, null);
            return data;
        }
        /// <summary>
        /// Sets a registry value.
        /// </summary>
        /// <typeparam name="ValueType">The type of the value to store.</typeparam>
        /// <param name="regPath">The path of the registry key.</param>
        /// <param name="valueName">The name of the registry value.</param>
        /// <param name="value">The data to store in the registry value.</param>
        public static void SetReg<ValueType>(string? regPath, string? valueName, ValueType? value)
        {
            Registry.SetValue(regPath, valueName, value);
        }
    
    }
}
