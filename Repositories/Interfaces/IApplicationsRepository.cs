using System;
using System.Reflection;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;

namespace JobTrackerApi.Repositories.Interfaces;

public interface IApplicationsRepository
{
  void AddJob(InsertJobDto job);


  void UpdateApplication(int Id, string status);

  void RemoveApplication(int Id);

  List<GetJobDto> GetAllJobs(string Id);
}
