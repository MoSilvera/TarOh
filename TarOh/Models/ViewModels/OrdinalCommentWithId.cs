using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarOh.Models.ViewModels
{
    public class OrdinalCommentWithId
    {
        public List<OrdinalComment> OrdinalComments { get; set; }
        public int? OrdinalId { get; set; }
    }
}
