using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace Optimizer.GameMode
{
    public static class GameMode
    {
        public static void TrunOn() {
            XboxGamingMode(1);
            OptimizationsForWindowedGames(1);
        }



        private static void XboxGamingMode(int val)
        {
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\GameBar", "AutoGameModeEnabled", val, RegistryValueKind.DWord);
        }
        private static void OptimizationsForWindowedGames(int val)
        {
            //DirectXUserGlobalSettings
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\DirectX\\UserGpuPreferences", "DirectXUserGlobalSettings", $"SwapEffectUpgradeEnable={val.ToString()};", RegistryValueKind.String);
        }
    }
}
