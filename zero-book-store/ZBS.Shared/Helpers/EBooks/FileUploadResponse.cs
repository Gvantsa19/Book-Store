using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Helpers.EBooks
{
    public class FileUploadResponse
    {
        public string ErrorMessage { get; set; }
        public List<FileUploadResponseData> Data { get; set; }
    }
}
