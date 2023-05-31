using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models;

public partial class NutriTecPsqlContext : DbContext
{
    public NutriTecPsqlContext()
    {
    }

    public NutriTecPsqlContext(DbContextOptions<NutriTecPsqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminRecipe> AdminRecipes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<DailyConsumption> DailyConsumptions { get; set; }

    public virtual DbSet<EatingPlan> EatingPlans { get; set; }

    public virtual DbSet<Nutritionist> Nutritionists { get; set; }

    public virtual DbSet<NutritionistPlan> NutritionistPlans { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<ProductDish> ProductDishes { get; set; }

    public virtual DbSet<ProductPlan> ProductPlans { get; set; }

    public virtual DbSet<ProductRecipe> ProductRecipes { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=nutritec-posgresql-db.postgres.database.azure.com;Database=NutriTec-psql;Username=dlurena24;Password=Max12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("Admin_pkey");

            entity.ToTable("Admin");
        });

        modelBuilder.Entity<AdminRecipe>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.Email }).HasName("AdminRecipe_pkey");

            entity.ToTable("AdminRecipe");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("Client_pkey");

            entity.ToTable("Client");

            entity.Property(e => e.Bmi).HasColumnName("BMI");
            entity.Property(e => e.Lname1).HasColumnName("LName1");
            entity.Property(e => e.Lname2).HasColumnName("LName2");
        });

        modelBuilder.Entity<DailyConsumption>(entity =>
        {
            entity.HasKey(e => new { e.Barcode, e.ClientId }).HasName("DailyConsumption_pkey");

            entity.ToTable("DailyConsumption");
        });

        modelBuilder.Entity<EatingPlan>(entity =>
        {
            entity.HasKey(e => e.EatPlanId).HasName("EatingPlan_pkey");

            entity.ToTable("EatingPlan");
        });

        modelBuilder.Entity<Nutritionist>(entity =>
        {
            entity.HasKey(e => new { e.NutritionistId, e.Email }).HasName("Nutritionist_pkey");

            entity.ToTable("Nutritionist");

            entity.Property(e => e.Bmi).HasColumnName("BMI");
            entity.Property(e => e.Lname1).HasColumnName("LName1");
            entity.Property(e => e.Lname2).HasColumnName("LName2");
        });

        modelBuilder.Entity<NutritionistPlan>(entity =>
        {
            entity.HasKey(e => new { e.NutritionistId, e.EatPlanId }).HasName("NutritionistPlan_pkey");

            entity.ToTable("NutritionistPlan");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PtypeId).HasName("PaymentType_pkey");

            entity.ToTable("PaymentType");

            entity.Property(e => e.PtypeId).HasColumnName("PTypeId");
        });

        modelBuilder.Entity<ProductDish>(entity =>
        {
            entity.HasKey(e => e.Barcode).HasName("Product-dish_pkey");

            entity.ToTable("Product-dish");
        });

        modelBuilder.Entity<ProductPlan>(entity =>
        {
            entity.HasKey(e => new { e.Barcode, e.EatPlanId }).HasName("ProductPlan_pkey");

            entity.ToTable("ProductPlan");
        });

        modelBuilder.Entity<ProductRecipe>(entity =>
        {
            entity.HasKey(e => new { e.Barcode, e.RecipeId }).HasName("ProductRecipe_pkey");

            entity.ToTable("ProductRecipe");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("Recipe_pkey");

            entity.ToTable("Recipe");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
