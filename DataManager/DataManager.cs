using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
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
            var targtPath = Path.TrimEndingDirectorySeparator(yandexDiskTarget) + "/"+ Path.GetFileName(fileName);

            await diskApi.Files.UploadFileAsync(targtPath, false, fileName, CancellationToken.None);
        }

        public async Task UploadDirectoryAsync(string directoryName, string yandexDiskTarget)
        {
            var files = Directory.GetFiles(directoryName);
            var uploadingTasks = files.Select(async x =>
            {
                await this.UploadFileAsync(x, yandexDiskTarget);
            });
            
            await Task.WhenAll(uploadingTasks);
        }
    }
}
