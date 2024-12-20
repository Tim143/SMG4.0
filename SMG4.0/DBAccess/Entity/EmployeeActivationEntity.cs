using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.Entity
{
    public class EmployeeActivationEntity
    {
        public long EmployeeId { get; set; }
        public string? Code { get; set; }
        public DateOnly? ActivationDate { get; set; }
    }
}
