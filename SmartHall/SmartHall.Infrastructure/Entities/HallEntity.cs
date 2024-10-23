using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.DAL.Sql.Entities
{
    public sealed class HallEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<HallEquipmentEntity> HallEquipment { get; set; }

        public decimal BaseCost { get; set; }

        public List<ReservationEntity> Reservations { get; set; }
    }
}
