using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Helpers.EBooks
{
    public class FileUploadResponseData
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string FileName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
