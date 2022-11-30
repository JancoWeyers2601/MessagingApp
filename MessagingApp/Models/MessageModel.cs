using System;
using System.Collections.Generic;

namespace MessagingApp.Models
{
    public partial class MessageModel
    {
        public int PkTblMessage { get; set; }
        public int? FkTblConversation { get; set; }
        public string? FkTblUser { get; set; }
        public string? TblMessageHeader { get; set; }
        public string? TblMessageBody { get; set; }
        public DateTime? tbl_Message_Time { get; set; }
    }
}
