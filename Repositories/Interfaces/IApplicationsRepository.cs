using System;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;

namespace JobTrackerApi.Repositories.Interfaces;

public interface IApplicationsRepository
{
  Task<GetJobDto> AddJob(InsertJobDto job, string userId);


  Task<bool> UpdateStatus(int Id, string status, string userId);

  Task<bool> RemoveApplication(int Id, string userId);

  Task<ResponseMetaData<GetJobDto>> GetAllJobs( string userId, int page, int pageSize, string? status);

  Task<GetJobDto> GetJob(string userId, int id);

  Task<bool> UpdateApplication(string userId, UpdateJobDto changes, int id );
}
