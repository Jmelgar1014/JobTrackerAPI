using System;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApi.Models;

public class AppDbContext : DbContext
{

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {

  }

  public DbSet<Users> Users { get; set; }
  
  public DbSet<Applications> Applications { get; set; }

}
