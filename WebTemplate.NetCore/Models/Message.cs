using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTemplate.NetCore.Enums;

namespace WebTemplate.NetCore.Models
{
    public class Message
    {
        public EnumMessageSeverity MessageSeverity { get; set; }
        public string Content { get; set; }
    }
}
