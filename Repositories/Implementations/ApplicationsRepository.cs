using System;
using AutoMapper;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;
using JobTrackerApi.Repositories.Interfaces;

namespace JobTrackerApi.Repositories.Implementations;

public class ApplicationsRepository(AppDbContext dbContext, IMapper mapper) : IApplicationsRepository
{
  private readonly AppDbContext _dbContext = dbContext;

  private readonly IMapper _mapper = mapper;

  public void AddJob(InsertJobDto job)
  {
    Applications app = _mapper.Map<Applications>(job);

    app.AppliedAt = DateTime.SpecifyKind(app.AppliedAt, DateTimeKind.Utc);
    
    _dbContext.Applications.Add(app);

    _dbContext.SaveChanges();
  }

  public List<GetJobDto> GetAllJobs(string Id)
  {
    var items = _dbContext.Applications.Where(a => a.UserId == Id).OrderByDescending(a => a.AppliedAt).ToList();

    List<GetJobDto> jobs = _mapper.Map<List<GetJobDto>>(items);

    return jobs;
  }

  public void UpdateApplication(int Id, string status)
  {
    var app = _dbContext.Applications.Where(a => a.Id == Id).First();

    if (app == null)
    {
      throw new Exception($"Application with Id {Id}: not found ");
    }

    app.Status = status;

    _dbContext.SaveChanges();

  }

  public void RemoveApplication(int Id)
  {
    var application = _dbContext.Applications.Where(a => a.Id == Id).First();

    _dbContext.Applications.Remove(application);

    _dbContext.SaveChanges();
  }

}
