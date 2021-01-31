using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataManager
{
    public class Program
    {
        public static int ConsoleLeftCursorPosition;
        public static int ConsoleTopCursorPosition;

        static ManualResetEventSlim mres = new ManualResetEventSlim(false);

        public static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Wrong arguments count");
            }

            DataManager dataManager = new DataManager();
            Progress<UploadProgressData> progress = new Progress<UploadProgressData>();

            Console.WriteLine("Files are being uploaded");

            // set the base position for reporting block
            ConsoleLeftCursorPosition = Console.CursorLeft;
            ConsoleTopCursorPosition = Console.CursorTop;
            progress.ProgressChanged += ProgressOnProgressChanged;
            await dataManager.UploadDirectoryAsync(args[0], args[1], progress);

            // wait till all event handlers done
            mres.Wait();

            Console.WriteLine("All files have been uploaded");
            Console.ReadKey();
        }

        // uploading progress changed event handler
        private static void ProgressOnProgressChanged(object? sender, UploadProgressData e)
        {
            // reset the cursor to the beggining of reporting block
            Console.SetCursorPosition(ConsoleLeftCursorPosition, ConsoleTopCursorPosition);
            foreach (var fileStatus in e.FileUploadStatuses)
            {
                var statusstring = fileStatus.Value == true ? "Uploaded" : "Is being uploaded";
                var line = $"{fileStatus.Key} [{statusstring}]";
                Console.WriteLine(line+ new string(' ', Console.WindowWidth - line.Length));
            }

            if (e.FileUploadStatuses.Values.All(isUploaded => isUploaded == true))
            {
                mres.Set();
            }
        }
    }
}
