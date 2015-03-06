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

            watcher.Created += (sender, e) => WriteLogLine(e);
            watcher.Changed += (sender, e) => WriteLogLine(e);
            watcher.Deleted += (sender, e) => WriteLogLine(e);
            watcher.Renamed += (sender, e) => WriteLogLine(e);
        }

        static void WriteLogLine(FileSystemEventArgs e)
        {
            Console.WriteLine("{0,10}     {1,-10}", e.ChangeType.ToString().ToUpper(), e.FullPath);
        }
    }
}
