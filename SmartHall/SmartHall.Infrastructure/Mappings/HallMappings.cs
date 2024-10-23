using AutoMapper;
using SmartHall.BLL.Halls.HallAggregate.Entities.Reservation;
using SmartHall.BLL.Halls.HallAggregate.ValueObjects;
using SmartHall.Common.Halls.Models.HallAggregate;
using SmartHall.Common.Halls.Models.HallAggregate.Entities.HallEquipment;
using SmartHall.Common.Halls.Models.HallAggregate.Entities.Reservation.ValueObjects;
using SmartHall.Common.Shared.ValueObjects;
using SmartHall.DAL.Sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.DAL.Sql.Mappings
{
    public sealed class HallMappings : Profile
    {
        public HallMappings() 
        {
            CreateMap<HallEntity, Hall>()
                .ConstructUsing(e => new Hall(e.Id, e.Name, Capacity.Create(e.HallEquipment.Count), Cost.Create(e.BaseCost), e.HallEquipment.Select(he => new HallEquipment(he.Id, he.Name, Cost.Create(he.Cost), he.Hall.Id)).ToList(), e.Reservations.Select(r => new Reservation(r.Id, ReservationPeriod.Create(r.StartDate, r.Duration), e.Id)).ToList()));

            CreateMap<Hall, HallEntity>()
                .ForMember(e => e.Id, opt => opt.MapFrom(h => h.Id))
                .ForMember(e => e.Name, opt => opt.MapFrom(h => h.Name))
                .ForMember(e => e.HallEquipment, opt => opt.MapFrom(h => h.AvailableEquipment))
                .ForMember(e => e.Reservations, opt => opt.MapFrom(h => h.Reservations))
                .ForMember(e => e.BaseCost, opt => opt.MapFrom(h => h.BaseCost));
        }
    }
}
