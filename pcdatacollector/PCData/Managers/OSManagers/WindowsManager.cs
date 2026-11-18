using System;
using System.Collections.Generic;
using System.Text;
namespace pcdatacollector.PCData.Managers.OSManagers
{
    public static class WindowsManager
    {
        public static string? GetComputerName()
        {
            return RegistryManager.GetReg<string>("HKEY_CURRENT_USER\\Volatile Environment", "USERDOMAIN");
        }
        public static string? GetUsername()
        {
            return RegistryManager.GetReg<string>("HKEY_CURRENT_USER\\Volatile Environment", "USERNAME");
        }
        public static string? GetHomeDrive()
        {
            return RegistryManager.GetReg<string>("HKEY_CURRENT_USER\\Volatile Environment", "HOMEDRIVE");
        }
    }
}
