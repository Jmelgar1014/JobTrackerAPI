using System;

namespace JobTrackerApi.Dtos;

public class GetJobDto
{
  public int Id { get; set; }
  public required string JobTitle { get; set; }

  public required string Company { get; set; }

  public required string Salary { get; set; }

  public required string UserId { get; set; }

  // public required Users User { get; set; }

  public required string Status { get; set; }

  public required string AppliedAt { get; set; }
}
