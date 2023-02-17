

using Domain.Common;
using Domain.Entities.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rent :BaseEntity
    {
        public int SuggestionId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRentFinished { get; set; }
        public virtual Suggestion? Suggestion { get; private set; }
    }
}
