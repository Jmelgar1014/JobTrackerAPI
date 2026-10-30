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
    CreateMap<Applications, GetJobDto>();
    CreateMap<UpdateJobDto, Applications>().ForAllMembers(opts => opts.Condition((src,dest,srcMember) => srcMember != null));
  }
}
