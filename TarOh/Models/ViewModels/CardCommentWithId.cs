using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models.ViewModels
{
    public class CardCommentWithId
    {
        public List<CardComment> CardComments { get; set; }
        public int? CardId { get; set; }

        public Card Card { get; set; }

        public CardType CardType { get; set; }
    }
}
