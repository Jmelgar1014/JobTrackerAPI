using System;

namespace JobTrackerApi.Dtos;

public class InsertUserDto
{
  public required string UserName { get; set; }
  
  public required string Email { get; set; }

  public DateTime CreatedAt { get; set; }
}
