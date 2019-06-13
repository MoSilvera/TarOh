using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models
{
    public class SavedSpread
    {
        [Key]
        public int SavedSpreadId { get; set; }

        public int SpreadId { get; set; }

        public int CardId { get; set; }

        public int OrdinalId { get; set; }

        public bool CardDirection { get; set; }

        public OrdinalPosition OrdinalPosition { get; set; }

        public Card Card { get; set; }
    }
}
