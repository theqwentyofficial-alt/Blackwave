using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace pcdatacollector.PCData.Managers.OSManagers
{
    public static class FileManager
    {
        /// <summary>
        /// Lists the directories in the current working directory.
        /// </summary>
        /// <returns>A list of directory paths in the current working directory.</returns>
        public static List<string> GetDirectories()
        {
            List<string> list = new List<string>();
            string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());

            for (int i = 0; i < dirs.Length; i++)
            {
                list.Add(dirs[i]);
            }
            return list;
        }

        public static List<string> GetDirectories(string path)
        {
            List<string> list = new List<string>();
            string[] dirs = Directory.GetDirectories(path);

            for (int i = 0; i < dirs.Length; i++)
            {
                list.Add(dirs[i]);
            }
            return list;
        }
        public static void SetCurrDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }
        public static string GetCurrDirectory()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}
