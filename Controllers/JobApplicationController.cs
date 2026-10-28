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

        private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value ?? throw new UnauthorizedAccessException();

        [HttpPost]
        public async Task<ActionResult<GetJobDto>> InsertJob([FromBody] InsertJobDto job)
        {
            var created = await _applicationsRepository.AddJob(job, GetUserId());
            // In your controller or middleware

            return CreatedAtAction(nameof(GetAllJobs),created);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetJobDto>>> GetAllJobs()
        {
            return await _applicationsRepository.GetAllJobs(GetUserId());
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<GetJobDto>> GetJob(int Id)
        {
            var job = await _applicationsRepository.GetJob(GetUserId(), Id);

            return job;
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateStatus(int Id, [FromBody] string status)
        {
            var updated = await _applicationsRepository.UpdateApplication(Id, status,GetUserId());

            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteApplication(int Id)
        {
            var deleted = await _applicationsRepository.RemoveApplication(Id,GetUserId());
            return deleted ? NoContent() : NotFound();
        }

    }
}
