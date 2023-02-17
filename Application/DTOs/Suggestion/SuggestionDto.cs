using Application.DTOs.Common;
using Domain.Entities.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Rent;

namespace Application.DTOs.Suggestion
{
    public class SuggestionDto : BaseEntityDto
    {

        public string? CustomerId { get; set; }
        public int OffreId { get; set; }
        public SuggestionStatus staus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual RentDto Rent { get; private set; }
    }
}
