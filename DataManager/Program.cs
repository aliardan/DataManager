using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataManager
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Wrong arguments count");
            }

            DataManager dataManager = new DataManager();
            Progress<UploadProgressData> progress = new Progress<UploadProgressData>();

            Console.WriteLine("Files are being uploaded");
            
            progress.ProgressChanged += ProgressOnProgressChanged;
            await dataManager.UploadDirectoryAsync(args[0], args[1], progress);

            Console.WriteLine("All files have been uploaded");
        }

        private static void ProgressOnProgressChanged(object? sender, UploadProgressData e)
        {
            foreach (var fileStatus in e.FileUploadStatuses)
            {
                var statusstring = fileStatus.Value == true ? "Uploaded" : "Is being uploaded";
                Console.WriteLine($"{fileStatus.Key} [{statusstring}]");
            }
        }
    }
}
