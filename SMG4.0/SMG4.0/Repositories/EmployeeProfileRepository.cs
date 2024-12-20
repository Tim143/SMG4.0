using AutoMapper;
using DBAccess.DBAccess;
using DBAccess.Entity;
using Microsoft.EntityFrameworkCore;
using SMG4._0.Models.DTO;

namespace SMG4._0.Repositories
{
    public interface IEmployeeProfileRepository
    {
        Task<EmployeeDTO> GetEmployeeProfile(long  id, CancellationToken cancellationToken);
    }

    public class EmployeeProfileRepository : IEmployeeProfileRepository
    {
        private readonly AppDbContext dBContext;
        private readonly IMapper mapper;

        public EmployeeProfileRepository(AppDbContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
        }

        public async Task<EmployeeDTO> GetEmployeeProfile(long id, CancellationToken cancellationToken)
        {
            EmployeeEntity employeeEntity = await dBContext.Employees.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync(cancellationToken);

            if (employeeEntity != null)
            {
                return mapper.Map<EmployeeDTO>(employeeEntity);
            }

            return new();
        }
    }
}
