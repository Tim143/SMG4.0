using AutoMapper;
using DBAccess.DBAccess;
using DBAccess.Entity;
using Microsoft.EntityFrameworkCore;
using SMG4._0.Models.SubModels;

namespace SMG4._0.Repositories
{
    public interface ITokenRepository
    {
        Task CreateAsync(RefreshTokenModel token, CancellationToken cancellationToken);
        Task<RefreshTokenModel> GetRefreshTokenAsync(string token, CancellationToken cancellationToken);
        Task<RefreshTokenModel> GetRefreshTokenAsync(long employeeId, CancellationToken cancellationToken);
        Task DeleteRefreshTokenAsync(long id, CancellationToken cancellationToken);
    }

    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public TokenRepository(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task CreateAsync(RefreshTokenModel token, CancellationToken cancellationToken)
        {
            var refreshToken = new AuthenticationTokenEntity()
            {
                Token = token.Token,
                EmployeeId = token.EmployeeId
            };

            await dbContext.AuthenticationTokens.AddAsync(refreshToken, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRefreshTokenAsync(long tokenId, CancellationToken cancellationToken)
        {
            var refreshToken = await dbContext.AuthenticationTokens.Where(tkn => tkn.Id == tokenId).FirstOrDefaultAsync(cancellationToken);

            if (refreshToken != null)
            {
                dbContext.AuthenticationTokens.Remove(refreshToken);

                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<RefreshTokenModel> GetRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            var refreshToken = await dbContext.AuthenticationTokens.Where(tkn => tkn.Token == token).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<RefreshTokenModel>(refreshToken);
        }

        public async Task<RefreshTokenModel> GetRefreshTokenAsync(long employeeId, CancellationToken cancellationToken)
        {
            var refreshToken = await dbContext.AuthenticationTokens.Where(tkn => tkn.EmployeeId == employeeId).FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<RefreshTokenModel>(refreshToken);
        }
    }
}
