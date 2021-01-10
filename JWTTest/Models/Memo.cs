using System;
using System.Collections.Generic;

#nullable disable

namespace MemoApp.Models
{
    public partial class Memo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserLoginId { get; set; }

        public virtual UserLogin UserLogin { get; set; }
    }
}
