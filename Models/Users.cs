using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTrackerApi.Models;


public class Users
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  [MaxLength(50)]
  public required string UserName { get; set; }

  [MaxLength(100)]
  public required string Email { get; set; }

  public DateTime CreatedAt { get; set; }
}
