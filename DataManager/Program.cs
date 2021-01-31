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

            var files = Directory.GetFiles(args[0]);
            var uploadingTasks = files.Select(async x =>
            {
                await dataManager.UploadFileAsync(x, args[1]);
            });
            Console.WriteLine("Files are being uploaded");
            await Task.WhenAll(uploadingTasks);
            Console.WriteLine("All files have been uploaded");
        }
    }
}
