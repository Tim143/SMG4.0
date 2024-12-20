using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProfilePhotoController : ControllerBase
    {
        public EmployeeProfilePhotoController()
        {
            
        }

        [HttpGet("photo")]
        public async Task GetPhoto()
        {

        }

        [HttpPost("photo")]
        public async Task UploadPhoto()
        {

        }

        [HttpDelete("photo")]
        public async Task DeletePhoto()
        {

        }
    }
}
