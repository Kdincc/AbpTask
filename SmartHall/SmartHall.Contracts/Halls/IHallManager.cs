using ErrorOr;
using SmartHall.Common.Halls.Models.CreateHall;
using SmartHall.Common.Halls.Models.RemoveHall;
using SmartHall.Common.Halls.Models.ReserveHall;
using SmartHall.Common.Halls.Models.SearchFreeHall;
using SmartHall.Common.Halls.Models.UpdateHall;

namespace SmartHall.Common.Halls
{
    /// <summary>
    /// Service for hall operations
    /// </summary>
    public interface IHallManager
    {
        public Task<ErrorOr<CreateHallResponse>> CreateHall(CreateHallRequest request, CancellationToken cancellationToken);

        public Task<ErrorOr<UpdateHallResponse>> UpdateHall(UpdateHallRequest request, CancellationToken cancellationToken);

        public Task<ErrorOr<RemoveHallResponse>> RemoveHall(RemoveHallRequest request, CancellationToken cancellationToken);

        public Task<ErrorOr<ReserveHallResponse>> ReserveHall(ReserveHallRequest request, CancellationToken cancellationToken);

        public Task<SearchFreeHallResponse> SearchFreeHall(SearchFreeHallRequest request, CancellationToken cancellationToken);
    }
}
