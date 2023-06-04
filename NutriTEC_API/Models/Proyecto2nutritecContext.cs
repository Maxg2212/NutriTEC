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

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Dailyconsumption> Dailyconsumptions { get; set; }

    public virtual DbSet<Eatingplan> Eatingplans { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Nutritionist> Nutritionists { get; set; }

    public virtual DbSet<Paymenttype> Paymenttypes { get; set; }

    public virtual DbSet<Productdish> Productdishes { get; set; }

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

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Clientid).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Clientid)
                .HasMaxLength(100)
                .HasColumnName("clientid");
            entity.Property(e => e.Bdate)
                .HasMaxLength(100)
                .HasColumnName("bdate");
            entity.Property(e => e.Bmi)
                .HasMaxLength(100)
                .HasColumnName("bmi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fatpercentage)
                .HasMaxLength(100)
                .HasColumnName("fatpercentage");
            entity.Property(e => e.Hipsize)
                .HasMaxLength(100)
                .HasColumnName("hipsize");
            entity.Property(e => e.Lastmonthmeas)
                .HasMaxLength(100)
                .HasColumnName("lastmonthmeas");
            entity.Property(e => e.Lname1)
                .HasMaxLength(100)
                .HasColumnName("lname1");
            entity.Property(e => e.Lname2)
                .HasMaxLength(100)
                .HasColumnName("lname2");
            entity.Property(e => e.Muslcepercentage)
                .HasMaxLength(100)
                .HasColumnName("muslcepercentage");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Necksize)
                .HasMaxLength(100)
                .HasColumnName("necksize");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Secondname)
                .HasMaxLength(100)
                .HasColumnName("secondname");
            entity.Property(e => e.Waistsize)
                .HasMaxLength(100)
                .HasColumnName("waistsize");
            entity.Property(e => e.Weight)
                .HasMaxLength(100)
                .HasColumnName("weight");

            entity.HasMany(d => d.Invoiceidcs).WithMany(p => p.Idclients)
                .UsingEntity<Dictionary<string, object>>(
                    "Invoiceclient",
                    r => r.HasOne<Invoice>().WithMany()
                        .HasForeignKey("Invoiceidc")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ic_invoice"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("Idclient")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ic_client"),
                    j =>
                    {
                        j.HasKey("Idclient", "Invoiceidc").HasName("invoiceclient_pkey");
                        j.ToTable("invoiceclient");
                        j.IndexerProperty<string>("Idclient")
                            .HasMaxLength(100)
                            .HasColumnName("idclient");
                        j.IndexerProperty<string>("Invoiceidc")
                            .HasMaxLength(100)
                            .HasColumnName("invoiceidc");
                    });

            entity.HasMany(d => d.Nutritionists).WithMany(p => p.Clients)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientNutritionist",
                    r => r.HasOne<Nutritionist>().WithMany()
                        .HasForeignKey("Nutritionistid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_cn_nutritionist"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("Clientid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_cn_client"),
                    j =>
                    {
                        j.HasKey("Clientid", "Nutritionistid").HasName("client_nutritionist_pkey");
                        j.ToTable("client_nutritionist");
                        j.IndexerProperty<string>("Clientid")
                            .HasMaxLength(100)
                            .HasColumnName("clientid");
                        j.IndexerProperty<string>("Nutritionistid")
                            .HasMaxLength(100)
                            .HasColumnName("nutritionistid");
                    });
        });

        modelBuilder.Entity<Dailyconsumption>(entity =>
        {
            entity.HasKey(e => new { e.Barcode, e.Clientid }).HasName("dailyconsumption_pkey");

            entity.ToTable("dailyconsumption");

            entity.Property(e => e.Barcode)
                .HasMaxLength(100)
                .HasColumnName("barcode");
            entity.Property(e => e.Clientid)
                .HasMaxLength(100)
                .HasColumnName("clientid");
            entity.Property(e => e.Datec).HasColumnName("datec");
            entity.Property(e => e.Eatingtime)
                .HasMaxLength(100)
                .HasColumnName("eatingtime");

            entity.HasOne(d => d.BarcodeNavigation).WithMany(p => p.Dailyconsumptions)
                .HasForeignKey(d => d.Barcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dc_product");

            entity.HasOne(d => d.Client).WithMany(p => p.Dailyconsumptions)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dc_client");
        });

        modelBuilder.Entity<Eatingplan>(entity =>
        {
            entity.HasKey(e => e.Eatplanid).HasName("eatingplan_pkey");

            entity.ToTable("eatingplan");

            entity.Property(e => e.Eatplanid)
                .HasMaxLength(100)
                .HasColumnName("eatplanid");
            entity.Property(e => e.Eatingschedule)
                .HasMaxLength(100)
                .HasColumnName("eatingschedule");
            entity.Property(e => e.Endingperiod)
                .HasMaxLength(100)
                .HasColumnName("endingperiod");
            entity.Property(e => e.Nutritionistname)
                .HasMaxLength(100)
                .HasColumnName("nutritionistname");
            entity.Property(e => e.Quantity)
                .HasMaxLength(100)
                .HasColumnName("quantity");
            entity.Property(e => e.Startperiod)
                .HasMaxLength(100)
                .HasColumnName("startperiod");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Invoiceid).HasName("invoice_pkey");

            entity.ToTable("invoice");

            entity.Property(e => e.Invoiceid)
                .HasMaxLength(100)
                .HasColumnName("invoiceid");
            entity.Property(e => e.Indescription)
                .HasMaxLength(100)
                .HasColumnName("indescription");
            entity.Property(e => e.Invoicedate).HasColumnName("invoicedate");
            entity.Property(e => e.Payamount)
                .HasMaxLength(100)
                .HasColumnName("payamount");
        });

        modelBuilder.Entity<Nutritionist>(entity =>
        {
            entity.HasKey(e => e.Employeeid).HasName("nutritionist_pkey");

            entity.ToTable("nutritionist");

            entity.Property(e => e.Employeeid)
                .HasMaxLength(100)
                .HasColumnName("employeeid");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Bdate)
                .HasMaxLength(100)
                .HasColumnName("bdate");
            entity.Property(e => e.Bmi)
                .HasMaxLength(100)
                .HasColumnName("bmi");
            entity.Property(e => e.Creditcard)
                .HasMaxLength(100)
                .HasColumnName("creditcard");
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
            entity.Property(e => e.Nutritionistcode)
                .HasMaxLength(100)
                .HasColumnName("nutritionistcode");
            entity.Property(e => e.Nutritionistid)
                .HasMaxLength(100)
                .HasColumnName("nutritionistid");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Paymenttype)
                .HasMaxLength(100)
                .HasColumnName("paymenttype");
            entity.Property(e => e.Profilepic)
                .HasMaxLength(1000)
                .HasColumnName("profilepic");
            entity.Property(e => e.Secondname)
                .HasMaxLength(100)
                .HasColumnName("secondname");
            entity.Property(e => e.Weight)
                .HasMaxLength(100)
                .HasColumnName("weight");

            entity.HasMany(d => d.Eatplans).WithMany(p => p.Nutritionists)
                .UsingEntity<Dictionary<string, object>>(
                    "Nutritionistplan",
                    r => r.HasOne<Eatingplan>().WithMany()
                        .HasForeignKey("Eatplanid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_np_plan"),
                    l => l.HasOne<Nutritionist>().WithMany()
                        .HasForeignKey("Nutritionistid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_np_product"),
                    j =>
                    {
                        j.HasKey("Nutritionistid", "Eatplanid").HasName("nutritionistplan_pkey");
                        j.ToTable("nutritionistplan");
                        j.IndexerProperty<string>("Nutritionistid")
                            .HasMaxLength(100)
                            .HasColumnName("nutritionistid");
                        j.IndexerProperty<string>("Eatplanid")
                            .HasMaxLength(100)
                            .HasColumnName("eatplanid");
                    });
        });

        modelBuilder.Entity<Paymenttype>(entity =>
        {
            entity.HasKey(e => new { e.Ptypeid, e.Description }).HasName("paymenttype_pkey");

            entity.ToTable("paymenttype");

            entity.Property(e => e.Ptypeid)
                .HasMaxLength(100)
                .HasColumnName("ptypeid");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");

            entity.HasOne(d => d.Ptype).WithMany(p => p.Paymenttypes)
                .HasForeignKey(d => d.Ptypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pt");
        });

        modelBuilder.Entity<Productdish>(entity =>
        {
            entity.HasKey(e => e.Barcode).HasName("productdish_pkey");

            entity.ToTable("productdish");

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
            entity.Property(e => e.Portionsize)
                .HasMaxLength(100)
                .HasColumnName("portionsize");
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

            entity.HasMany(d => d.Eatplans).WithMany(p => p.Barcodes)
                .UsingEntity<Dictionary<string, object>>(
                    "Productplan",
                    r => r.HasOne<Eatingplan>().WithMany()
                        .HasForeignKey("Eatplanid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_pp_plan"),
                    l => l.HasOne<Productdish>().WithMany()
                        .HasForeignKey("Barcode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_pp_product"),
                    j =>
                    {
                        j.HasKey("Barcode", "Eatplanid").HasName("productplan_pkey");
                        j.ToTable("productplan");
                        j.IndexerProperty<string>("Barcode")
                            .HasMaxLength(100)
                            .HasColumnName("barcode");
                        j.IndexerProperty<string>("Eatplanid")
                            .HasMaxLength(100)
                            .HasColumnName("eatplanid");
                    });

            entity.HasMany(d => d.Recipes).WithMany(p => p.Barcodes)
                .UsingEntity<Dictionary<string, object>>(
                    "Productrecipe",
                    r => r.HasOne<Recipe>().WithMany()
                        .HasForeignKey("Recipeid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_pr_recipe"),
                    l => l.HasOne<Productdish>().WithMany()
                        .HasForeignKey("Barcode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_pr_product"),
                    j =>
                    {
                        j.HasKey("Barcode", "Recipeid").HasName("productrecipe_pkey");
                        j.ToTable("productrecipe");
                        j.IndexerProperty<string>("Barcode")
                            .HasMaxLength(100)
                            .HasColumnName("barcode");
                        j.IndexerProperty<string>("Recipeid")
                            .HasMaxLength(100)
                            .HasColumnName("recipeid");
                    });
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Recipeid).HasName("recipe_pkey");

            entity.ToTable("recipe");

            entity.Property(e => e.Recipeid)
                .HasMaxLength(100)
                .HasColumnName("recipeid");
            entity.Property(e => e.Calories)
                .HasMaxLength(100)
                .HasColumnName("calories");
            entity.Property(e => e.Ingredients)
                .HasMaxLength(100)
                .HasColumnName("ingredients");
            entity.Property(e => e.Portions)
                .HasMaxLength(100)
                .HasColumnName("portions");

            entity.HasMany(d => d.Emails).WithMany(p => p.Recipes)
                .UsingEntity<Dictionary<string, object>>(
                    "Adminrecipe",
                    r => r.HasOne<Admin>().WithMany()
                        .HasForeignKey("Email")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ar_recipe"),
                    l => l.HasOne<Recipe>().WithMany()
                        .HasForeignKey("Recipeid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ar_admin"),
                    j =>
                    {
                        j.HasKey("Recipeid", "Email").HasName("adminrecipe_pkey");
                        j.ToTable("adminrecipe");
                        j.IndexerProperty<string>("Recipeid")
                            .HasMaxLength(100)
                            .HasColumnName("recipeid");
                        j.IndexerProperty<string>("Email")
                            .HasMaxLength(100)
                            .HasColumnName("email");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
