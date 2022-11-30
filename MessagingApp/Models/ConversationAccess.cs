using System;
using System.Collections.Generic;

namespace MessagingApp.Models
{
    public partial class ConversationAccess
    {
        public int PkTblConversationAccess { get; set; }
        public int? FkTblUser { get; set; }
        public int? FkTblConversation { get; set; }
    }
}
