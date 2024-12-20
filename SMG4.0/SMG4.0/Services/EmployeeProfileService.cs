using AutoMapper;
using SMG4._0.Models.Response;
using SMG4._0.Repositories;

namespace SMG4._0.Services
{
    public interface IEmployeeProfileService
    {
        public Task<EmployeeProfileResponseModel> GetProfile(long id, CancellationToken cancellationToken);
    }

    public class EmployeeProfileService : IEmployeeProfileService
    {
        private readonly IEmployeeProfileRepository employeeProfileRepository;
        private readonly IMapper mapper;

        public EmployeeProfileService(IEmployeeProfileRepository employeeProfileRepository, IMapper mapper)
        {
            this.employeeProfileRepository = employeeProfileRepository;
            this.mapper = mapper;
        }

        public async Task<EmployeeProfileResponseModel> GetProfile(long id, CancellationToken cancellationToken)
        {
            var employeeProfile = await employeeProfileRepository.GetEmployeeProfile(id, cancellationToken);

            if(employeeProfile != null)
            {
                return mapper.Map<EmployeeProfileResponseModel>(employeeProfile);
            }

            return new();
        }
    }
}