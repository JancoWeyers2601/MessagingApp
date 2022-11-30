using System;
using System.Collections.Generic;

namespace MessagingApp.Models
{
    public partial class Conversation
    {
        public int PkTblConversation { get; set; }
        public string? TblConversationHeader { get; set; }
        public int? FkTblCreator { get; set; }
    }
}
