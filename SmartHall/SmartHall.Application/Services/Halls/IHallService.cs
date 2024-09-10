﻿using ErrorOr;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.RemoveHall;
using SmartHall.Contracts.Halls.ReserveHall;
using SmartHall.Contracts.Halls.UpdateHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Services.Halls
{
    public interface IHallService
    {
        public Task<ErrorOr<CreateHallResponse>> CreateHall(CreateHallRequest request, CancellationToken cancellationToken);

        public Task<ErrorOr<UpdateHallResponse>> UpdateHall(UpdateHallRequest request, CancellationToken cancellationToken);

        public Task<ErrorOr<RemoveHallResponse>> RemoveHall(RemoveHallRequest request, CancellationToken cancellationToken);

        public Task<ErrorOr<ReserveHallResponse>> ReserveHall(ReserveHallRequest request, CancellationToken cancellationToken);
    }
}
