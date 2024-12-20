using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Entity
{
    public class AuthenticationTokenEntity
    {
        public long Id { get; set; }
        public string Token { get; set; } = default!;
        public long EmployeeId { get; set; }
        public EmployeeEntity? Employee { get; set; }
    }
}
