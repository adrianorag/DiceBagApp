using DiceBagApp.Services;
using DiceBagApp.Droid.Services;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(FileHelper))]
namespace DiceBagApp.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}