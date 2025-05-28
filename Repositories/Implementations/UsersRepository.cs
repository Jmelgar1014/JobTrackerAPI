using System;
using AutoMapper;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;
using JobTrackerApi.Repositories.Interfaces;

namespace JobTrackerApi.Repositories.Implementations;

public class UsersRepository(AppDbContext dbContext, IMapper mapper) : IUsersRepository
{

  private readonly AppDbContext _dbContext = dbContext;

  private readonly IMapper _mapper = mapper;
  public List<Users> GetUserNames()
  {
    List<Users> items = _dbContext.Users.ToList();

    return items;
  }


  public void AddToDb(InsertUserDto user)
  {
    Users newUser = _mapper.Map<Users>(user);

    _dbContext.Users.Add(newUser);

    _dbContext.SaveChanges();
  }
}
