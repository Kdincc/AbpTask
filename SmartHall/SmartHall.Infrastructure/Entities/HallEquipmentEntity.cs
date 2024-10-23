using SmartHall.Common.Halls.Models.HallAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.DAL.Sql.Entities
{
    public sealed class HallEquipmentEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public Hall Hall { get; set; }
    }
}
