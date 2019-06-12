using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models
{
    public class OrdinalPosition
    {
        [Key]
        public int OrdinalPositionId { get; set; }

        public string Name { get; set; }

        public string Definition { get; set; }

        public List<OrdinalComment> OrdinalComments {get; set;}
    }
}
