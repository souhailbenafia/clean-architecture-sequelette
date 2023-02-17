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
    public class Car : BaseEntity
    {
        public  string? Description { get; set; }
        public string? Model { get; set; }
        public CarMark? Mark { get; set; }
        public GearType? Gear { get; set; }
        public DateTime? Release { get; set; }
        public  int? Kilometrage { get; set; }
        public int? NombreDePlace { get; set; }
        public string? Color { get; set; }
        public CarburantType? Carburant { get; set; }
        public string? SellerId { get; set; }
        public virtual User? Seller { get;  set; }
        public virtual IList<Offre>? Offres { get; private set; }
    }
}
