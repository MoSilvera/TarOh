using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models
{
    public class SpreadComment
    {
        [Key]
        public int SpreadCommentId { get; set; }

        public int SpreadId { get; set; }

        public string UserId { get; set; }

        public string Comment { get; set; }

        public ApplicationUser User { get; set; }

        public Spread Spread { get; set; }
    }
}
