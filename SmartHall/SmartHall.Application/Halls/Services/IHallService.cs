using ErrorOr;
using SmartHall.Contracts.Halls.CreateHall;
using SmartHall.Contracts.Halls.GetFreeHall;
using SmartHall.Contracts.Halls.RemoveHall;
using SmartHall.Contracts.Halls.ReserveHall;
using SmartHall.Contracts.Halls.SearchFreeHall;
using SmartHall.Contracts.Halls.UpdateHall;

namespace SmartHall.Application.Halls.Services
{
	public interface IHallService
	{
		public Task<ErrorOr<CreateHallResponse>> CreateHall(CreateHallRequest request, CancellationToken cancellationToken);

		public Task<ErrorOr<UpdateHallResponse>> UpdateHall(UpdateHallRequest request, CancellationToken cancellationToken);

		public Task<ErrorOr<RemoveHallResponse>> RemoveHall(RemoveHallRequest request, CancellationToken cancellationToken);

		public Task<ErrorOr<ReserveHallResponse>> ReserveHall(ReserveHallRequest request, CancellationToken cancellationToken);

		public Task<ErrorOr<SearchFreeHallResponse>> SearchFreeHall(SearchFreeHallRequest request, CancellationToken cancellationToken);
	}
}
