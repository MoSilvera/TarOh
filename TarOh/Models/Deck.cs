using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models
{
    public class Deck
    {
        [Key]
        public int DeckId { get; set; }

        
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]

        public bool Custom { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Card> CardsInDeck { get; set; }

    }
}
