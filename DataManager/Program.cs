using System;
using System.IO;
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
            var files = Directory.GetFiles(args[0]);
            Console.WriteLine("Files are being uploaded");
            int counter = 1;
            foreach (var file in files)
            {
                Console.WriteLine($"File {file} is being uploaded ({counter}/{files.Length})");
                await dataManager.UploadAsync(file, args[1]);
                counter++;
            }

            Console.WriteLine("All files have been uploaded");
        }
    }
}
