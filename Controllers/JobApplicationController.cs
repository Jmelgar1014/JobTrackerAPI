using System.Security.Claims;
using JobTrackerApi.Dtos;
using JobTrackerApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobTrackerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IApplicationsRepository _applicationsRepository;
        public JobApplicationController(IApplicationsRepository applicationsRepository)
        {
            _applicationsRepository = applicationsRepository;
        }

        [HttpPost]
        public ActionResult InsertJob([FromBody] InsertJobDto job)
        {
            _applicationsRepository.AddJob(job);
            // In your controller or middleware
            Console.WriteLine($"Raw Auth Header: {Request.Headers["Authorization"]}");

            return Ok("success");
        }

        [HttpGet]
        public List<GetJobDto> GetAllJobs()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("sub")?.Value;
            
            return _applicationsRepository.GetAllJobs(userId);  
        }

        [HttpPut("{Id}")]
        public ActionResult UpdateStatus(int Id, [FromBody] string status)
        {
            _applicationsRepository.UpdateApplication(Id, status);

            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteApplication(int Id)
        {
            _applicationsRepository.RemoveApplication(Id);
            return Ok("Successfully Removed");
        }
    
    }
}
