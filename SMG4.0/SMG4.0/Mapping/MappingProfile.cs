using AutoMapper;
using DBAccess.Entity;
using SMG4._0.Models.DTO;
using SMG4._0.Models.Response;
using SMG4._0.Models.SubModels;

namespace SMG4._0.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeEntity,EmployeeDTO>().ReverseMap();
            CreateMap<AuthenticationTokenEntity, RefreshTokenModel>().ReverseMap();
            CreateMap<EmployeeDTO, EmployeeProfileResponseModel>().ReverseMap();
        }
    }
}
