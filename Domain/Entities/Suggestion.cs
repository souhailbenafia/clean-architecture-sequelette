using Domain.Common;
using Domain.Entities.Enums;
using Domain.Entities.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Suggestion : BaseEntity
    {
        public string? CustomerId { get; set; }
        public virtual User? Customer { get; private set; }
        public int OffreId { get; set; }
        public virtual Offre? Offre { get; private set; }
        public SuggestionStatus staus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Rent? Rent { get; private set; }
    }
}
