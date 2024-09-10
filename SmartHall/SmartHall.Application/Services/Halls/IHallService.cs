using SmartHall.Contracts.Halls.CreateHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Services.Halls
{
    public interface IHallService
    {
        public Task<CreateHallResponse> CreateHall(CreateHallRequest request);
    }
}
