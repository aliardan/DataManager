using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
        public async Task UploadAsync(string fileName, string yandexDiskTarget)
        {
            IDiskApi diskApi = new DiskHttpApi("AgAAAABQNcSVAADLW8Rn_gQcNkrhlIhLGyzFvWM");
            var targtPath = Path.TrimEndingDirectorySeparator(yandexDiskTarget) + "/"+ Path.GetFileName(fileName);

            await diskApi.Files.UploadFileAsync(targtPath, false, fileName, CancellationToken.None);
        }
    }
}
