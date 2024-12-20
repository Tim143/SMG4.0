using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMG4._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProfilePhotoController : ControllerBase
    {
        public EmployeeProfilePhotoController()
        {
            
        }

        [HttpGet("photo")]
        public async Task GetPhoto(long id)
        {

        }

        [HttpPost("photo")]
        public async Task UploadPhoto(long id)
        {

        }

        [HttpDelete("photo")]
        public async Task DeletePhoto(long id)
        {

        }
    }
}
