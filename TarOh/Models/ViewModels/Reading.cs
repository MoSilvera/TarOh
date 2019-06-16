using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models.ViewModels
{
    public class Reading
    {
        public List<Card> Cards { get; set; }

        public List<OrdinalPosition> OrdinalPositions { get; set;}

        public List<CardComment> CardComments { get; set; }

        public List<OrdinalComment> OrdinalComments { get; set; }

        public int SpreadId { get; set; }

        
    }
}
