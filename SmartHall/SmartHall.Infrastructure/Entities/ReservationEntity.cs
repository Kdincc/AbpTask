using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.DAL.Sql.Entities
{
    public sealed class ReservationEntity
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public TimeSpan Duration { get; set; }

        public Guid HallId { get; set; }
    }
}
