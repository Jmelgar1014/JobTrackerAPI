using System;
using AutoMapper;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;
using JobTrackerApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApi.Repositories.Implementations;

public class ApplicationsRepository(AppDbContext dbContext, IMapper mapper) : IApplicationsRepository
{
  private readonly AppDbContext _dbContext = dbContext;

  private readonly IMapper _mapper = mapper;

  public async Task<GetJobDto> AddJob(InsertJobDto job, string userId)
  {
    Applications app = _mapper.Map<Applications>(job);

    app.UserId = userId;

    app.AppliedAt = DateTime.SpecifyKind(app.AppliedAt, DateTimeKind.Utc);
    
    _dbContext.Applications.Add(app);

    await _dbContext.SaveChangesAsync();

    return _mapper.Map<GetJobDto>(app);
  }

  public async Task<ResponseMetaData<GetJobDto>>  GetAllJobs( string userId, int page, int pageSize, string? status)
  {
    var query = _dbContext.Applications.Where(a => a.UserId == userId);

    if(!string.IsNullOrEmpty(status))
    {
      query = query.Where(a => a.Status == status);
    }

    var items = await query.OrderByDescending(a => a.AppliedAt).ThenByDescending(a => a.Id).Skip((page -1) * pageSize).Take(pageSize).ToListAsync();

    List<GetJobDto> jobs = _mapper.Map<List<GetJobDto>>(items);

    int totalCount = await query.CountAsync();

    ResponseMetaData<GetJobDto> response = new ResponseMetaData<GetJobDto>{
      Items = jobs,
      Page = page,
      PageSize = pageSize,
      TotalCount = totalCount,
    };
    return response;
  }

  public async Task<GetJobDto> GetJob(string userId, int id)
  {
    var item = await _dbContext.Applications.Where(a => a.UserId == userId && a.Id == id).FirstAsync();

    GetJobDto job = _mapper.Map<GetJobDto>(item);

    return job;
  }

  public async Task<bool> UpdateStatus(int Id, string status, string userId)
  {
    var app = await _dbContext.Applications.FirstOrDefaultAsync(a => a.Id == Id && a.UserId == userId);

    if (app is null) return false;

    app.Status = status;

    await _dbContext.SaveChangesAsync();

    return true;

  }

  public async Task<bool> UpdateApplication(string userId, UpdateJobDto changes, int id)
  {
    var item = await _dbContext.Applications.FirstOrDefaultAsync(a => a.UserId == userId && a.Id == id);

    if(item == null) return false;

    _mapper.Map(changes, item);

    await _dbContext.SaveChangesAsync();

    return true;
  }

  public async Task<bool> RemoveApplication(int Id, string userId)
  {
    var app = await _dbContext.Applications.FirstOrDefaultAsync(a => a.Id == Id && a.UserId == userId);

    if(app is null) return false;

    _dbContext.Applications.Remove(app);

    await _dbContext.SaveChangesAsync();

    return true;
  }

}
