using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AshTechEngine
{
    public static class FileSystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async Task WriteTextLocalStorage(string filePath, string text)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile File = await storageFolder.CreateFileAsync(filePath, CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(File, text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<string> ReadTextLocalStorage(string filePath)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile File = await storageFolder.CreateFileAsync(filePath, CreationCollisionOption.ReplaceExisting);
            string text = await Windows.Storage.FileIO.ReadTextAsync(File);
            return text;
        }
    }
}