using JobTrackerApi.Dtos;
using JobTrackerApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobTrackerApi.Controllers
{
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
        public void InsertJob([FromBody]InsertJobDto job)
        {
            _applicationsRepository.AddJob(job);
        }

        
    }
}
