using Application.DTOs.Common;
using Application.DTOs.Offre;
using Application.DTOs.User;
using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Car
{
    public class CarDto  
    {
        public int Id { get; set; }
        public  string? Description { get; set; }
        public string? Model { get; set; }
        public CarMark? Mark { get; set; }
        public GearType? Gear { get; set; }
        public DateTime? Release { get; set; }
        public  int? Kilometrage { get; set; }
        public int? NombreDePlace { get; set; }
        public string? Color { get; set; }
        public CarburantType Carburant { get; set; }
        public string? SellerId { get; set; }

        public virtual IList<OffreDto>? offres { get;set; }
    }
}
