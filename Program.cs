using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sentinel
{
    class Program
    {
        static void Main(string[] args)
        {
            var watchDirectories = ConfigurationManager.AppSettings["folders"].Split('|').ToList();

            watchDirectories.ForEach(path => CreateWatcher(path));

            Console.WriteLine("Watching...");
            Console.ReadLine();
        }

        static void CreateWatcher(string path)
        {
            var watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            // watcher.NotifyFilter = NotifyFilters.

            watcher.Created += watcher_Created;
            watcher.Changed += watcher_Changed;
            watcher.Deleted += watcher_Deleted;
            watcher.Renamed += watcher_Renamed;
        }

        static void WriteLogLine(FileSystemEventArgs e)
        {
            Console.WriteLine("{0,10}     {1,-10}", e.ChangeType.ToString().ToUpper(), e.FullPath);
        }

        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            WriteLogLine(e);
        }

        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            WriteLogLine(e);
        }

        static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            WriteLogLine(e);
        }

        static void watcher_Renamed(object sender, FileSystemEventArgs e)
        {
            WriteLogLine(e);
        }
    }
}
