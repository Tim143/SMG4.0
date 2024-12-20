using AutoMapper;
using DBAccess.DBAccess;
using DBAccess.Entity;
using Microsoft.EntityFrameworkCore;
using SMG4._0.Models.DTO;
using System.Linq;

namespace SMG4._0.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDTO> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<EmployeeDTO> GetByIdAsync(long employeeId, CancellationToken cancellationToken);
        Task<string> GetEmployeeActivationCode(long id, CancellationToken cancellationToken);
        Task UpdateEmployeeActivationCode(long id, CancellationToken cancellationToken);
        Task UpdateEmployeeAsync(EmployeeDTO userDTO, CancellationToken cancellationToken);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public EmployeeRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<EmployeeDTO> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            EmployeeEntity employee = await dbContext.Employees.Where(x => x.Email == email).FirstOrDefaultAsync(cancellationToken);

            if(employee != null)
            {
                return mapper.Map<EmployeeDTO>(employee);
            }

            return new();
        }

        public async Task<EmployeeDTO> GetByIdAsync(long employeeId, CancellationToken cancellationToken)
        {
            EmployeeEntity employee = await dbContext.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync(cancellationToken);

            if (employee != null)
            {
                return mapper.Map<EmployeeDTO>(employee);
            }

            return new();
        }

        public async Task<string> GetEmployeeActivationCode(long employeeId, CancellationToken cancellationToken)
        {
            return await dbContext.EmployeeActivations
                .Where(x => x.EmployeeId == employeeId && x.ActivationDate != null)
                .Select(x => x.Code)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateEmployeeAsync(EmployeeDTO employeeDTO, CancellationToken cancellationToken)
        {
            EmployeeEntity employee = await dbContext.Employees.Where(x => x.Id == employeeDTO.Id).FirstOrDefaultAsync(cancellationToken);

            if(employee != null)
            {
                employee.PasswordHash = employeeDTO.PasswordHash;
                employee.PasswordSalt = employeeDTO.PasswordSalt;

                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateEmployeeActivationCode(long id, CancellationToken cancellationToken)
        {
            EmployeeActivationEntity activationEntity = await dbContext.EmployeeActivations.Where(x => x.EmployeeId == id).FirstOrDefaultAsync(cancellationToken);

            if (activationEntity != null)
            {
                activationEntity.ActivationDate = DateOnly.FromDateTime(DateTime.Now);
                activationEntity.Code = HashCode.Combine(DateTime.Now, activationEntity.Code).ToString();
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
