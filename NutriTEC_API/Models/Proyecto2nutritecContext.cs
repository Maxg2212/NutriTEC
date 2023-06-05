using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models;

public partial class Proyecto2nutritecContext : DbContext
{
    public Proyecto2nutritecContext()
    {
    }

    public Proyecto2nutritecContext(DbContextOptions<Proyecto2nutritecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminRecipe> AdminRecipes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientNutritionist> ClientNutritionists { get; set; }

    public virtual DbSet<DailyConsumption> DailyConsumptions { get; set; }

    public virtual DbSet<EatingPlan> EatingPlans { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceClient> InvoiceClients { get; set; }

    public virtual DbSet<Nutritionist> Nutritionists { get; set; }

    public virtual DbSet<NutritionistPlan> NutritionistPlans { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<ProductDish> ProductDishes { get; set; }

    public virtual DbSet<ProductPlan> ProductPlans { get; set; }

    public virtual DbSet<ProductRecipe> ProductRecipes { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=nutritec-posgresql-db.postgres.database.azure.com;Database=proyecto2nutritec;Username=dlurena24;Password=Max12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("admin_pkey");

            entity.ToTable("admin");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
        });

        modelBuilder.Entity<AdminRecipe>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("admin_recipe");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.RecipeId)
                .HasMaxLength(100)
                .HasColumnName("recipe_id");

            entity.HasOne(d => d.EmailNavigation).WithMany()
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ar_recipe");

            entity.HasOne(d => d.Recipe).WithMany()
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ar_admin");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.ClientId)
                .HasMaxLength(100)
                .HasColumnName("client_id");
            entity.Property(e => e.Bdate).HasColumnName("bdate");
            entity.Property(e => e.Bmi)
                .HasMaxLength(100)
                .HasColumnName("bmi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FatPercentage)
                .HasMaxLength(100)
                .HasColumnName("fat_percentage");
            entity.Property(e => e.HipSize)
                .HasMaxLength(100)
                .HasColumnName("hip_size");
            entity.Property(e => e.LastMonthMeas)
                .HasMaxLength(100)
                .HasColumnName("last_month_meas");
            entity.Property(e => e.Lname1)
                .HasMaxLength(100)
                .HasColumnName("lname1");
            entity.Property(e => e.Lname2)
                .HasMaxLength(100)
                .HasColumnName("lname2");
            entity.Property(e => e.MuslcePercentage)
                .HasMaxLength(100)
                .HasColumnName("muslce_percentage");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.NeckSize)
                .HasMaxLength(100)
                .HasColumnName("neck_size");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.SecondName)
                .HasMaxLength(100)
                .HasColumnName("second_name");
            entity.Property(e => e.WaistSize)
                .HasMaxLength(100)
                .HasColumnName("waist_size");
            entity.Property(e => e.Weight)
                .HasMaxLength(100)
                .HasColumnName("weight");
        });

        modelBuilder.Entity<ClientNutritionist>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("client_nutritionist");

            entity.Property(e => e.ClientId)
                .HasMaxLength(100)
                .HasColumnName("client_id");
            entity.Property(e => e.NutritionistId)
                .HasMaxLength(100)
                .HasColumnName("nutritionist_id");

            entity.HasOne(d => d.Client).WithMany()
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cn_client");

            entity.HasOne(d => d.Nutritionist).WithMany()
                .HasForeignKey(d => d.NutritionistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cn_nutritionist");
        });

        modelBuilder.Entity<DailyConsumption>(entity =>
        {
            entity.HasKey(e => new { e.Barcode, e.ClientId }).HasName("daily_consumption_pkey");

            entity.ToTable("daily_consumption");

            entity.Property(e => e.Barcode)
                .HasMaxLength(100)
                .HasColumnName("barcode");
            entity.Property(e => e.ClientId)
                .HasMaxLength(100)
                .HasColumnName("client_id");
            entity.Property(e => e.Datec).HasColumnName("datec");
            entity.Property(e => e.EatingTime)
                .HasMaxLength(100)
                .HasColumnName("eating_time");

            entity.HasOne(d => d.BarcodeNavigation).WithMany(p => p.DailyConsumptions)
                .HasForeignKey(d => d.Barcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dc_product");

            entity.HasOne(d => d.Client).WithMany(p => p.DailyConsumptions)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dc_client");
        });

        modelBuilder.Entity<EatingPlan>(entity =>
        {
            entity.HasKey(e => e.EatplanId).HasName("eating_plan_pkey");

            entity.ToTable("eating_plan");

            entity.Property(e => e.EatplanId)
                .HasMaxLength(100)
                .HasColumnName("eatplan_id");
            entity.Property(e => e.EatingSchedule)
                .HasMaxLength(100)
                .HasColumnName("eating_schedule");
            entity.Property(e => e.EndingPeriod)
                .HasMaxLength(100)
                .HasColumnName("ending_period");
            entity.Property(e => e.NutritionistName)
                .HasMaxLength(100)
                .HasColumnName("nutritionist_name");
            entity.Property(e => e.Quantity)
                .HasMaxLength(100)
                .HasColumnName("quantity");
            entity.Property(e => e.StartPeriod)
                .HasMaxLength(100)
                .HasColumnName("start_period");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("invoice_pkey");

            entity.ToTable("invoice");

            entity.Property(e => e.InvoiceId)
                .HasMaxLength(100)
                .HasColumnName("invoice_id");
            entity.Property(e => e.InDescription)
                .HasMaxLength(100)
                .HasColumnName("in_description");
            entity.Property(e => e.InvoiceDate).HasColumnName("invoice_date");
            entity.Property(e => e.PayAmount)
                .HasMaxLength(100)
                .HasColumnName("pay_amount");
        });

        modelBuilder.Entity<InvoiceClient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("invoice_client");

            entity.Property(e => e.IdClient)
                .HasMaxLength(100)
                .HasColumnName("id_client");
            entity.Property(e => e.InvoiceIdc)
                .HasMaxLength(100)
                .HasColumnName("invoice_idc");

            entity.HasOne(d => d.IdClientNavigation).WithMany()
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ic_client");

            entity.HasOne(d => d.InvoiceIdcNavigation).WithMany()
                .HasForeignKey(d => d.InvoiceIdc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ic_invoice");
        });

        modelBuilder.Entity<Nutritionist>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("nutritionist_pkey");

            entity.ToTable("nutritionist");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .HasColumnName("employee_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Bdate)
                .HasMaxLength(100)
                .HasColumnName("bdate");
            entity.Property(e => e.Bmi)
                .HasMaxLength(100)
                .HasColumnName("bmi");
            entity.Property(e => e.CreditCard)
                .HasMaxLength(100)
                .HasColumnName("credit_card");
            entity.Property(e => e.Discount)
                .HasPrecision(5, 2)
                .HasColumnName("discount");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Lname1)
                .HasMaxLength(100)
                .HasColumnName("lname1");
            entity.Property(e => e.Lname2)
                .HasMaxLength(100)
                .HasColumnName("lname2");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.NutritionistCode)
                .HasMaxLength(100)
                .HasColumnName("nutritionist_code");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(100)
                .HasColumnName("payment_type");
            entity.Property(e => e.ProfilePic)
                .HasMaxLength(1000)
                .HasColumnName("profile_pic");
            entity.Property(e => e.SecondName)
                .HasMaxLength(100)
                .HasColumnName("second_name");
            entity.Property(e => e.Weight)
                .HasMaxLength(100)
                .HasColumnName("weight");

            entity.HasOne(d => d.PaymentTypeNavigation).WithMany(p => p.Nutritionists)
                .HasForeignKey(d => d.PaymentType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pt");
        });

        modelBuilder.Entity<NutritionistPlan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("nutritionist_plan");

            entity.Property(e => e.EatplanId)
                .HasMaxLength(100)
                .HasColumnName("eatplan_id");
            entity.Property(e => e.NutritionistId)
                .HasMaxLength(100)
                .HasColumnName("nutritionist_id");

            entity.HasOne(d => d.Eatplan).WithMany()
                .HasForeignKey(d => d.EatplanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_np_plan");

            entity.HasOne(d => d.Nutritionist).WithMany()
                .HasForeignKey(d => d.NutritionistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_np_product");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PtypeId).HasName("payment_type_pkey");

            entity.ToTable("payment_type");

            entity.Property(e => e.PtypeId)
                .HasMaxLength(100)
                .HasColumnName("ptype_id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ProductDish>(entity =>
        {
            entity.HasKey(e => e.Barcode).HasName("product_dish_pkey");

            entity.ToTable("product_dish");

            entity.Property(e => e.Barcode)
                .HasMaxLength(100)
                .HasColumnName("barcode");
            entity.Property(e => e.Calcium)
                .HasMaxLength(100)
                .HasColumnName("calcium");
            entity.Property(e => e.Carbs)
                .HasMaxLength(100)
                .HasColumnName("carbs");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Energy)
                .HasMaxLength(100)
                .HasColumnName("energy");
            entity.Property(e => e.Fat)
                .HasMaxLength(100)
                .HasColumnName("fat");
            entity.Property(e => e.Iron)
                .HasMaxLength(100)
                .HasColumnName("iron");
            entity.Property(e => e.PortionSize)
                .HasMaxLength(100)
                .HasColumnName("portion_size");
            entity.Property(e => e.Protein)
                .HasMaxLength(100)
                .HasColumnName("protein");
            entity.Property(e => e.Sodium)
                .HasMaxLength(100)
                .HasColumnName("sodium");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Vitamins)
                .HasMaxLength(100)
                .HasColumnName("vitamins");
        });

        modelBuilder.Entity<ProductPlan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("product_plan");

            entity.Property(e => e.Barcode)
                .HasMaxLength(100)
                .HasColumnName("barcode");
            entity.Property(e => e.EatplanId)
                .HasMaxLength(100)
                .HasColumnName("eatplan_id");

            entity.HasOne(d => d.BarcodeNavigation).WithMany()
                .HasForeignKey(d => d.Barcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pp_product");

            entity.HasOne(d => d.Eatplan).WithMany()
                .HasForeignKey(d => d.EatplanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pp_plan");
        });

        modelBuilder.Entity<ProductRecipe>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("product_recipe");

            entity.Property(e => e.Barcode)
                .HasMaxLength(100)
                .HasColumnName("barcode");
            entity.Property(e => e.RecipeId)
                .HasMaxLength(100)
                .HasColumnName("recipe_id");

            entity.HasOne(d => d.BarcodeNavigation).WithMany()
                .HasForeignKey(d => d.Barcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pr_product");

            entity.HasOne(d => d.Recipe).WithMany()
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pr_recipe");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("recipe_pkey");

            entity.ToTable("recipe");

            entity.Property(e => e.RecipeId)
                .HasMaxLength(100)
                .HasColumnName("recipe_id");
            entity.Property(e => e.Calories)
                .HasMaxLength(100)
                .HasColumnName("calories");
            entity.Property(e => e.Ingredients)
                .HasMaxLength(100)
                .HasColumnName("ingredients");
            entity.Property(e => e.Portions)
                .HasMaxLength(100)
                .HasColumnName("portions");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
