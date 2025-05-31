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

    _dbContext.Applications.Add(app);

    _dbContext.SaveChanges();
  }

  public List<InsertJobDto> GetJobs()
  {
    var items = _dbContext.Applications.ToList();
    List<InsertJobDto> jobs = _mapper.Map<List<InsertJobDto>>(items);

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

}
