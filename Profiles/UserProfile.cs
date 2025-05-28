using System;
using AutoMapper;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;

namespace JobTrackerApi.Profiles;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<InsertUserDto, Users>();
  }
}
