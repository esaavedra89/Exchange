
using Xamarin.Forms;

[assembly: Dependency(typeof(Exchange.iOS.Implementations.Config))]
namespace Exchange.iOS.Implementations
{
    using Exchange.Interfaces;
    using SQLite.Net.Interop;
    using System;

    public class Config : IConfig
    {
        string directoryDB;
        ISQLitePlatform platform;


        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryDB = System.IO.Path.Combine(directory, "..", "Library");
                }
                return directoryDB;
            }
        }

        //donde esta la libreria
        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                }

                return platform;
            }
        }
    }
}