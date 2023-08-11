using ChallengeN5.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChallengeN5.Data
{
    public class PermissionDbContext : DbContext
    {
        public PermissionDbContext(DbContextOptions<PermissionDbContext> options) : base(options) { }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
    }
}
