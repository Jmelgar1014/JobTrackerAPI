using System;
using System.Reflection;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;

namespace JobTrackerApi.Repositories.Interfaces;

public interface IApplicationsRepository
{
  void AddJob(InsertJobDto job);

  List<InsertJobDto> GetJobs();

  void UpdateApplication(int Id, string status);
}
