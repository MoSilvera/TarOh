using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models
{
    public class Spread
    {
        [Key]
        public int SpreadId { get; set; }

        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ReadingDate { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set;  }


        public List<SavedSpread> SavedSpreads { get; set; }

    }
}
