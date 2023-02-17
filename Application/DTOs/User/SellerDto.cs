using Application.DTOs.Car;
using Application.DTOs.Offre;
using Application.DTOs.Rent;
using Application.DTOs.Suggestion;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class SellerDto
    {
            public string SellerId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Genre { get; set; }
            public DateTime BirthDate { get; set; }
            public string Email { get; set; }

        public virtual IList<CarDto> Cars { get; private set; }

        public virtual IList<OffreDto> Offres { get; private set; }

        public virtual IList<RentDto> Rents { get; private set; }
        public virtual IList<SuggestionDto> Suggestions { get; private set; }
    }
}
