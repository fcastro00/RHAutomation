namespace FolderAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            var watchFolder = @"C:\temp\Watch";
            var destinationRootFolder = @"C:\temp\Dest";

            var fileWatcher = new FileWatcher(watchFolder, destinationRootFolder);
            fileWatcher.Start();

            Console.WriteLine("Watching for files in {0}.", watchFolder);
            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();

            fileWatcher.Stop();
        }
    }
}