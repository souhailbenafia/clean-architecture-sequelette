using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Voiture : BaseEntity
    {
        public  string? Description { get; set; }
        public string[]? ImagesPath { get; set; }

        public string? Model { get; set; }
        public string? Gear { get; set; }
        public DateTime? MiseEnCirculation { get; set; }
        public  int? Kilometrage { get; set; }

        public int? NombreDePlace { get; set; }
    }
}
