using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public static class FileWatcher
    {


        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Start()
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();

            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            // string _directory = @"..\..\..\FlatFiles\{0}\";

            var _path = @"E:\Data\FlooringMastery\{mode}\";
            
             watcher.Path = _path;
           

            watcher.NotifyFilter = NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Filter = "*.csv";

            watcher.Changed += new FileSystemEventHandler(OnChanged);

            watcher.EnableRaisingEvents = true;

        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Output.SendToConsole("File: " + e.FullPath + " " + e.ChangeType);
        }


    }
}
