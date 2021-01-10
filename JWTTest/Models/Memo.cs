using System;
using System.Collections.Generic;

#nullable disable

namespace MemoApp.Models
{
    public partial class Memo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Content { get; set; }
        public int Userloginid { get; set; }

        public virtual UserLogin UserLogin { get; set; }
    }
}
