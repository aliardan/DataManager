using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;

namespace DataManager
{
    class DataManager
    {
        public async Task UploadFileAsync(string fileName, string yandexDiskTarget)
        {
            IDiskApi diskApi = new DiskHttpApi("AgAAAABQNcSVAADLW8Rn_gQcNkrhlIhLGyzFvWM");
            var targetPath = Path.TrimEndingDirectorySeparator(yandexDiskTarget) + "/"+ Path.GetFileName(fileName);

            await diskApi.Files.UploadFileAsync(targetPath, false, fileName, CancellationToken.None);
        }

        public async Task UploadDirectoryAsync(string directoryName, string yandexDiskTarget,
            IProgress<UploadProgressData> progress = null)
        {
            if (progress != null)
            {
                var files = Directory.GetFiles(directoryName);

                Dictionary<string, bool> fileUploadStatuses = new Dictionary<string, bool>(files.Length);
                foreach (var file in files)
                {
                    fileUploadStatuses.Add(file, false);
                }

                var uploadingTasks = files.Select(async filename =>
                {
                    await UploadFileAsync(filename, yandexDiskTarget);
                    fileUploadStatuses[filename] = true;
                    progress.Report(new UploadProgressData(new Dictionary<string, bool>(fileUploadStatuses)));
                });
                
                await Task.WhenAll(uploadingTasks);
            }
            else
            {
                var files = Directory.GetFiles(directoryName);
                var uploadingTasks = files.Select(async x => { await this.UploadFileAsync(x, yandexDiskTarget); });

                await Task.WhenAll(uploadingTasks);
            }
        }
    }
}
