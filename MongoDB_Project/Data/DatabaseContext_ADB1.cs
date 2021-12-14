using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MongoDB_Project
{
    public partial class DatabaseContext_ADB1 : DbContext
    {
        public DatabaseContext_ADB1()
        {
        }

        public DatabaseContext_ADB1(DbContextOptions<DatabaseContext_ADB1> options)
            : base(options)
        {
        }

        public virtual DbSet<Radacct_dim> Radacct { get; set; }
        public virtual DbSet<Radreply_dim> Radreply { get; set; }
        public virtual DbSet<ApplicationUser_dim> ApplicationUser { get; set; }
        public virtual DbSet<DeviceOwnership_dim> DeviceOwnership { get; set; }
        public virtual DbSet<Vlan_dim> Vlan { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}