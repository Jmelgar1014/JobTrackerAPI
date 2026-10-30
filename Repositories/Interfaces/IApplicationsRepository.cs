using System;
using JobTrackerApi.Dtos;

namespace JobTrackerApi.Repositories.Interfaces;

public interface IApplicationsRepository
{
  Task<GetJobDto> AddJob(InsertJobDto job, string userId);


  Task<bool> UpdateStatus(int Id, string status, string userId);

  Task<bool> RemoveApplication(int Id, string userId);

  Task<List<GetJobDto>> GetAllJobs( string userId);

  Task<GetJobDto> GetJob(string userId, int id);

  Task<bool> UpdateApplication(string userId, UpdateJobDto changes, int id );
}
