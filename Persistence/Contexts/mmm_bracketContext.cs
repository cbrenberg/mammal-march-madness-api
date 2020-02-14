using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Models.Configuration;


namespace MMM_Bracket.API.Persistence.Contexts
{
  public class mmm_bracketContext : DbContext
  {

    private readonly DatabaseSecrets _databaseSecrets;
    public mmm_bracketContext()
    {
    }

    public mmm_bracketContext(DbContextOptions<mmm_bracketContext> options, IOptions<DatabaseSecrets> databaseSecrets)
        : base(options)
    {
      _databaseSecrets = databaseSecrets.Value ?? throw new ArgumentException(nameof(databaseSecrets));
    }

    public virtual DbSet<Animal> Animals { get; set; }
    public virtual DbSet<Battle> Battles { get; set; }
    public virtual DbSet<BracketPicks> BracketPicks { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Participant> Participants { get; set; }
    public virtual DbSet<User> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //  if (!optionsBuilder.IsConfigured)
    //  {
    //    optionsBuilder.UseNpgsql(_databaseSecrets.ConnectionString);
    //  }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

      modelBuilder.Entity<Animal>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.CategoryId).HasColumnName("category_id");

        entity.Property(e => e.InitialSeed).HasColumnName("initial_seed");

        entity.Property(e => e.Name)
                  .IsRequired()
                  .HasColumnName("name");

        entity.HasOne(d => d.Category)
                  .WithMany(p => p.Animals)
                  .HasForeignKey(d => d.CategoryId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("Animals_fk0");
      });

      modelBuilder.Entity<Battle>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Location)
                  .IsRequired()
                  .HasColumnName("location");

        entity.Property(e => e.RoundNumber).HasColumnName("round_number");

        entity.Property(e => e.Timestamp)
                  .HasColumnName("timestamp")
                  .HasColumnType("timestamp with time zone");
      });

      modelBuilder.Entity<BracketPicks>(entity =>
      {
        entity.ToTable("Bracket_Picks");

        entity.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasDefaultValueSql("nextval('bracket_picks_seq'::regclass)");

        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.Property(e => e.WinnerId).HasColumnName("winner_id");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.BracketPicks)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("Bracket_Picks_fk0");

        entity.HasOne(d => d.Winner)
                  .WithMany(p => p.BracketPicks)
                  .HasForeignKey(d => d.WinnerId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("Bracket_Picks_fk1");
      });

      modelBuilder.Entity<Category>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Description).HasColumnName("description");

        entity.Property(e => e.Name)
                  .IsRequired()
                  .HasColumnName("name");

        entity.Property(e => e.Year).HasColumnName("year");
      });

      modelBuilder.Entity<Participant>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.AnimalId).HasColumnName("animal_id");

        entity.Property(e => e.BattleId).HasColumnName("battle_id");

        entity.Property(e => e.IsWinner).HasColumnName("is_winner");

        entity.HasOne(d => d.Animal)
                  .WithMany(p => p.Participants)
                  .HasForeignKey(d => d.AnimalId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("Participants_fk0");

        entity.HasOne(d => d.Battle)
                  .WithMany(p => p.Participants)
                  .HasForeignKey(d => d.BattleId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("Participants_fk1");
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Email)
                  .IsRequired()
                  .HasColumnName("email");

        entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasDefaultValue("Pardner")
                  .HasColumnName("first_name");

        entity.Property(e => e.IsAdmin)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("is_admin");

        entity.Property(e => e.Password)
                  .IsRequired()
                  .HasColumnName("password");

        entity.Property(e => e.Username)
                  .IsRequired()
                  .HasColumnName("username");

        entity.Property(e => e.RefreshToken)
                  .HasColumnName("refresh_token");
      });

      modelBuilder.HasSequence("bracket_picks_seq");
    }
  }
}
