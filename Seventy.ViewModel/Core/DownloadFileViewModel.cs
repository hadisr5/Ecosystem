using System.IO;

namespace Seventy.ViewModel.Core
{
   public class DownloadFileViewModel
    {
        public DownloadFileViewModel()
        {
            Result = false;
            Message = "file not found";
        }
        public bool Result { get; set; }
        public string Message { get; set; }
        public string MimeType { get; set; }
        public FileStream Stream { get; set; }
    }
}
