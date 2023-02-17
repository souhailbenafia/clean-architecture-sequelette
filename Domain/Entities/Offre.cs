using Domain.Common;
using Domain.Entities.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Offre : BaseEntity
    {
        public string? Description { get; set; }
        public string? Name { get; set; }
        public float? price { get; set; }
        public bool? Available { get; set; } = true;
        public int CarId { get; set; }
        public virtual Car? Car { get; private set; }
        public virtual IList<Suggestion>? Suggestions { get; private set; }

    }
}
