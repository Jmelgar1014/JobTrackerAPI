using JobTrackerApi.Dtos;
using JobTrackerApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

            return Ok();
        }

        [HttpGet]
        public List<InsertJobDto> GetJobs()
        {
            return _applicationsRepository.GetJobs();

        }

        [HttpPut("{Id}")]
        public ActionResult UpdateStatus(int Id, [FromBody] string status)
        {
            _applicationsRepository.UpdateApplication(Id, status);

            return Ok();
        }



    }
}
