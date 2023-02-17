using Application.DTOs.Car;
using Application.DTOs.Common;
using Application.DTOs.Suggestion;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Offre
{
    public class OffreDto : BaseEntityDto
    {
        public string? Description { get; set; }
        public string? Name { get; set; }
        public float? price { get; set; }
        public bool? Available { get; set; } = true;
        public int CarId { get; set; }
        public string? sellerId { get; set; }
        public virtual IList<CarDto>? cars { get; private set; }
        public virtual IList<SuggestionDto>? Suggestions { get; private set; }
    }
}
