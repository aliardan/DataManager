using System;
using System.Collections.Generic;
using System.Text;

namespace DataManager
{
    class UploadProgressData
    {
        public UploadProgressData(Dictionary<string, bool> fileUploadStatuses)
        {
            FileUploadStatuses = fileUploadStatuses;
        }

        public Dictionary<string, bool> FileUploadStatuses { get; }
    }
}
