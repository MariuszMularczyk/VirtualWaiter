using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace VirtualWaiter.Utils
{
    public class StorageHelper
    {
        private string storagePath;

        public StorageHelper(string _storagePath)
        {
            storagePath = _storagePath;
        }

        public string SaveFile(HttpPostedFileBase file, string storageSubdirectory, string newFileName)
        {
            string directoryPath;
            string fullPath;
            GetDirectoryAndFilePath(storageSubdirectory, newFileName, out directoryPath, out fullPath);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            file.SaveAs(fullPath);
            return fullPath;
        }

        private void GetDirectoryAndFilePath(string storageSubdirectory, string newFileName, out string directoryPath, out string fullPath)
        {


            directoryPath = Path.Combine(storagePath, storageSubdirectory);
            fullPath = Path.Combine(storagePath, storageSubdirectory, newFileName);
        }

        public string SaveFile(Stream stream, string storageSubdirectory, string newFileName)
        {

            string directoryPath;
            string fullPath;
            GetDirectoryAndFilePath(storageSubdirectory, newFileName, out directoryPath, out fullPath);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
            return fullPath;
        }

        public void RemoveFile(string name, string storageSubdirectory)
        {
            string fullPath = Path.Combine(storagePath, storageSubdirectory, name);
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public static string RemoveNotAllowedCharacters(string str)
        {
            return str.Replace(@"\", "_")
                .Replace("/", "_")
                .Replace(":", "_")
                .Replace("*", "_")
                .Replace("?", "_")
                .Replace("?", "_")
                .Replace("<", "_")
                .Replace(">", "_")
                .Replace("|", "_")
                .Replace("#", "_")
                .Replace("%", "_")
                .Replace("&", "_")
                .Replace("{", "_")
                .Replace("}", "_")
                .Replace("$", "_")
                .Replace("!", "_")
                .Replace("'", "_")
                .Replace("\"", "_")
                .Replace("@", "_")
                .Replace("+", "_")
                .Replace("=", "_");
        }
    }
}
