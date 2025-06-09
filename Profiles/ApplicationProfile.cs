using System;
using AutoMapper;
using JobTrackerApi.Dtos;
using JobTrackerApi.Models;

namespace JobTrackerApi.Profiles;

public class ApplicationProfile : Profile
{
  public ApplicationProfile()
  {
    CreateMap<InsertJobDto, Applications>();
    CreateMap<Applications, InsertJobDto>();
    CreateMap<Applications, GetJobDto>();
    CreateMap<DeleteJobDto, Applications>();
  }
}
