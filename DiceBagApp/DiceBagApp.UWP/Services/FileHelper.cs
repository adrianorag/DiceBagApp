using System.IO;
using Xamarin.Forms;
using Windows.Storage;
using DiceBagApp.UWP.Services;
using DiceBagApp.Services;

[assembly: Dependency(typeof(FileHelper))]
namespace DiceBagApp.UWP.Services
{
    class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
