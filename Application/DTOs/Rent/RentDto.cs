using Application.DTOs.Common;
using Application.DTOs.Suggestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Rent
{
    public class RentDto :BaseEntityDto
    {
        public int SuggestionId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRentFinished { get; set; }
        public virtual SuggestionDto? Suggestion { get; private set; }
    }
}
