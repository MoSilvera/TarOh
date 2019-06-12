using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models
{
    public class OrdinalComment
    {
        [Key]
        public int OrdinalCommentId { get; set; }

        public int CardId { get; set; }

        public string UserId { get; set; }

        public string Comment { get; set; }

        public ApplicationUser User { get; set; }

        public Card Card { get; set; }
    }
}
