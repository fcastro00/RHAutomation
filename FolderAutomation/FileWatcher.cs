using System.Text.RegularExpressions;

namespace FolderAutomation
{
    public class FileWatcher
    {
        private readonly string _watchFolder;
        private readonly string _destinationRootFolder;
        private FileSystemWatcher _fileSystemWatcher;

        public FileWatcher(string watchFolder, string destinationRootFolder)
        {
            _watchFolder = watchFolder;
            _destinationRootFolder = destinationRootFolder;
            CleanDestinationFolder();
            InitializeWatcher();
        }

        private void CleanDestinationFolder()
        {
            if (Directory.Exists(_destinationRootFolder))
            {
                Directory.Delete(_destinationRootFolder, true);
            }

            Directory.CreateDirectory(_destinationRootFolder);
        }

        private void InitializeWatcher()
        {
            _fileSystemWatcher = new FileSystemWatcher(_watchFolder)
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
            };

            _fileSystemWatcher.Created += OnFileCreated;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                var fileName = Path.GetFileName(e.FullPath);
                var datePart = ExtractDateFromFileName(fileName);
                var destinationFolder = string.IsNullOrEmpty(datePart)
                    ? Path.Combine(_destinationRootFolder, "Review")
                    : Path.Combine(_destinationRootFolder, datePart);

                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }

                var destinationPath = Path.Combine(destinationFolder, fileName);
                File.Move(e.FullPath, destinationPath, true);
                Console.WriteLine($"Moved file {fileName} to {destinationPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }

        private string ExtractDateFromFileName(string fileName)
        {
            var datePart = Regex.Match(fileName, @"\d{8}").Value;
            return datePart;
        }

        public void Start()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
        }
    }
}
