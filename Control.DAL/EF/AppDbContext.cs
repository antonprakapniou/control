﻿namespace Control.DAL.EF;

public sealed class AppDbContext : DbContext
{
    #region DbSet

    public DbSet<Category> Categories { get; set; }
    public DbSet<Measuring> Measurings { get; set; }
    public DbSet<Nomination> Nominations { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Period> Periods { get; set; }
    public DbSet<Position> Positions { get; set; }
    //public DbSet<Master> Masters { get; set; }

    #endregion

    #region Ctor

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    #endregion

    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new MeasuringModelConfiguration())
            .ApplyConfiguration(new NominationModelConfiguration())
            .ApplyConfiguration(new OperationModelConfiguration())
            .ApplyConfiguration(new OwnerModelConfiguration())
            .ApplyConfiguration(new PeriodModelConfiguration())
            .ApplyConfiguration(new PositionModelConfiguration())
            .ApplyConfiguration(new CategoryModelConfiguration());
    }


    #endregion
}