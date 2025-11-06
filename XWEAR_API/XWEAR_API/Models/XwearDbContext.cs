using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace XWEAR_API.Models;

public partial class XwearDbContext : DbContext
{
    public XwearDbContext()
    {
    }

    public XwearDbContext(DbContextOptions<XwearDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<GoodSize> GoodSizes { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionsDetail> TransactionsDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=xwear_db", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.0.1-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PRIMARY");

            entity.ToTable("brands");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(70)
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.UserEmail, e.GoodArticle })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("cart");

            entity.HasIndex(e => e.GoodArticle, "fk_cart_good_idx");

            entity.Property(e => e.UserEmail)
                .HasMaxLength(120)
                .HasColumnName("user_email");
            entity.Property(e => e.GoodArticle)
                .HasMaxLength(8)
                .HasColumnName("good_article");
            entity.Property(e => e.GoodCount).HasColumnName("good_count");

            entity.HasOne(d => d.GoodArticleNavigation).WithMany(p => p.Carts)
                .HasPrincipalKey(p => p.GoodArticle)
                .HasForeignKey(d => d.GoodArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cart_good");

            entity.HasOne(d => d.UserEmailNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cart_user");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(70)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => new { e.GoodArticle, e.ModelId, e.CategoryId, e.BrandId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

            entity.ToTable("goods");

            entity.HasIndex(e => e.BrandId, "fk_brand_good_idx");

            entity.HasIndex(e => e.CategoryId, "fk_category_good_idx");

            entity.HasIndex(e => e.ModelId, "fk_model_idx");

            entity.HasIndex(e => e.GoodArticle, "good_article_UNIQUE").IsUnique();

            entity.Property(e => e.GoodArticle)
                .HasMaxLength(8)
                .HasColumnName("good_article");
            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");

            entity.HasOne(d => d.Brand).WithMany(p => p.Goods)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_brand_good");

            entity.HasOne(d => d.Category).WithMany(p => p.Goods)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_category_good");

            entity.HasOne(d => d.Model).WithMany(p => p.Goods)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_model");
        });

        modelBuilder.Entity<GoodSize>(entity =>
        {
            entity.HasKey(e => new { e.GoodArticle, e.Size })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("good_size");

            entity.Property(e => e.GoodArticle)
                .HasMaxLength(8)
                .HasColumnName("good_article");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.GoodArticleNavigation).WithMany(p => p.GoodSizes)
                .HasPrincipalKey(p => p.GoodArticle)
                .HasForeignKey(d => d.GoodArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_good_size");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => new { e.GoodArticle, e.Patrh })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("images");

            entity.HasIndex(e => e.GoodArticle, "good_id_UNIQUE").IsUnique();

            entity.Property(e => e.GoodArticle)
                .HasMaxLength(8)
                .HasColumnName("good_article");
            entity.Property(e => e.Patrh)
                .HasMaxLength(100)
                .HasColumnName("patrh");
            entity.Property(e => e.Main).HasColumnName("main");

            entity.HasOne(d => d.GoodArticleNavigation).WithOne(p => p.Image)
                .HasPrincipalKey<Good>(p => p.GoodArticle)
                .HasForeignKey<Image>(d => d.GoodArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_good_image");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PRIMARY");

            entity.ToTable("models");

            entity.Property(e => e.ModelId).HasColumnName("model_id");
            entity.Property(e => e.ModelName)
                .HasMaxLength(70)
                .HasColumnName("model_name");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => new { e.TransactionId, e.UserEmail })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("transactions");

            entity.HasIndex(e => e.UserEmail, "fk_user_transaction_idx");

            entity.HasIndex(e => e.TransactionId, "transaction_id_UNIQUE").IsUnique();

            entity.Property(e => e.TransactionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("transaction_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(120)
                .HasColumnName("user_email");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(70)
                .HasColumnName("company_name");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");
            entity.Property(e => e.TransactionTotalAmount).HasColumnName("transaction_total_amount");
            entity.Property(e => e.TransactionsStatus)
                .HasColumnType("enum('Не оплачен','В обработке','Оплачено')")
                .HasColumnName("transactions_status");
            entity.Property(e => e.UserCity)
                .HasMaxLength(70)
                .HasColumnName("user_city");
            entity.Property(e => e.UserCountry)
                .HasMaxLength(70)
                .HasColumnName("user_country");
            entity.Property(e => e.UserIndex)
                .HasMaxLength(6)
                .HasColumnName("user_index");
            entity.Property(e => e.UserName)
                .HasMaxLength(70)
                .HasColumnName("user_name");
            entity.Property(e => e.UserNumHouse)
                .HasMaxLength(5)
                .HasColumnName("user_num_house");
            entity.Property(e => e.UserState)
                .HasMaxLength(70)
                .HasColumnName("user_state");
            entity.Property(e => e.UserStreet)
                .HasMaxLength(70)
                .HasColumnName("user_street");
            entity.Property(e => e.UserSurname)
                .HasMaxLength(70)
                .HasColumnName("user_surname");

            entity.HasOne(d => d.UserEmailNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_transaction");
        });

        modelBuilder.Entity<TransactionsDetail>(entity =>
        {
            entity.HasKey(e => new { e.TransactionId, e.GoodArticle })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("transactions_details");

            entity.HasIndex(e => e.GoodArticle, "fk_trans_good_idx");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.GoodArticle)
                .HasMaxLength(8)
                .HasColumnName("good_article");
            entity.Property(e => e.CountGood).HasColumnName("count_good");
            entity.Property(e => e.GoodAmount).HasColumnName("good_amount");

            entity.HasOne(d => d.GoodArticleNavigation).WithMany(p => p.TransactionsDetails)
                .HasPrincipalKey(p => p.GoodArticle)
                .HasForeignKey(d => d.GoodArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trans_good");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TransactionsDetails)
                .HasPrincipalKey(p => p.TransactionId)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trans_trans");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("email");
            entity.Property(e => e.Nickname)
                .HasMaxLength(70)
                .HasColumnName("nickname");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(18)
                .HasColumnName("phone");

            entity.HasMany(d => d.GoodArticles).WithMany(p => p.UserEmails)
                .UsingEntity<Dictionary<string, object>>(
                    "FavouritesGood",
                    r => r.HasOne<Good>().WithMany()
                        .HasPrincipalKey("GoodArticle")
                        .HasForeignKey("GoodArticle")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_good_fav"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_user_fav"),
                    j =>
                    {
                        j.HasKey("UserEmail", "GoodArticle")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("favourites_good");
                        j.HasIndex(new[] { "GoodArticle" }, "fk_good_fav_idx");
                        j.IndexerProperty<string>("UserEmail")
                            .HasMaxLength(70)
                            .HasColumnName("user_email");
                        j.IndexerProperty<string>("GoodArticle")
                            .HasMaxLength(8)
                            .HasColumnName("good_article");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
