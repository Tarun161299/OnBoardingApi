using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CommonModels = OnBoardingSystem.Common.Models;
namespace OnBoardingSystem.Data.EF.Models;

public partial class OBSDBContext : DbContext
{
    public virtual DbSet<CommonModels.MDStatus> MDStatus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new NotImplementedException("Hard coded connection strings are not allowed, use appsettings.json to configure database connection.");
        }
    }

    //modelBuilder.Entity<AbsCustomModels.MDStatus>(entity => { entity.HasNoKey(); });
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        //auditInfoMapper.MapAuditColumns(ChangeTracker.Entries());
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        //auditInfoMapper.MapAuditColumns(ChangeTracker.Entries());
        return base.SaveChanges();
    }
}