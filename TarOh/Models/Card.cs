using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [DisplayName("Upload Image")]
        public string ImagePath { get; set; }

        public string UpDefinition { get; set; }

        public string DownDefinition { get; set; }

        public int CardTypeId { get; set; }

        public int DeckId { get; set; }

        public bool CardDirection { get; set; }

        public CardType CardType { get; set; }

        public List<CardComment> CardComments {get; set;}

        public List <SavedSpread> SavedSpreads { get; set; }

    }
}
