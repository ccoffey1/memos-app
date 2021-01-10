using System.ComponentModel.DataAnnotations;

namespace MemoApp.Contracts
{
    public class MemoDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public int UserLoginId { get; set; }
    }
}
