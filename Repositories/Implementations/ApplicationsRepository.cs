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
}
