using AutoMapper;
using ErrorOr;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace SmartHall.Service.Mappings
{
    public class ErrorMapper : Profile
    {
        public ErrorMapper()
        {
            CreateMap<ValidationFailure, Error>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.ErrorCode))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ErrorMessage));

            CreateMap<IdentityError, Error>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
