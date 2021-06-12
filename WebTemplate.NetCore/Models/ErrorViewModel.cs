using System;

namespace WebTemplate.NetCore.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public Exception Exception { get; set; }

        public string PathRequest { get; set; }
    }
}
