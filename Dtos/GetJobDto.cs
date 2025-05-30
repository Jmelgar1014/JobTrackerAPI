using System;

namespace JobTrackerApi.Dtos;

public class GetJobDto
{
  public int Id { get; set; }
  public required string JobTitle { get; set; }

  public required string Company { get; set; }

  public required string Salary { get; set; }

  // public int UserId { get; set; }

  // public required Users User { get; set; }

  public required string Status { get; set; }

  public DateTime AppliedAt { get; set; }
}
