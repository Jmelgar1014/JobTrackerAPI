using System;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace JobTrackerApi.Repositories.Interfaces;

public interface IUsersRepository
{
  List<Users> GetUserNames();

  void AddToDb(InsertUserDto user);
}
