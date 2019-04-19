using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MMM_Bracket.API.Domain.Models;

namespace MMM_Bracket.API.Persistence.Contexts
{
  public partial class mmm_bracketContext : DbContext
  {
    public mmm_bracketContext()
    {
    }

    public mmm_bracketContext(DbContextOptions<mmm_bracketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animals> Animals { get; set; }
    public virtual DbSet<Battles> Battles { get; set; }
    public virtual DbSet<BracketPicks> BracketPicks { get; set; }
    public virtual DbSet<Categories> Categories { get; set; }
    public virtual DbSet<Participants> Participants { get; set; }
    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        optionsBuilder.UseNpgsql("Host=localhost;Database=mmm_bracket;Username=Christopher;");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

      modelBuilder.Entity<Animals>(entity =>
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

      modelBuilder.Entity<Battles>(entity =>
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

      modelBuilder.Entity<Categories>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Description).HasColumnName("description");

        entity.Property(e => e.Name)
                  .IsRequired()
                  .HasColumnName("name");

        entity.Property(e => e.Year).HasColumnName("year");
      });

      modelBuilder.Entity<Participants>(entity =>
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

      modelBuilder.Entity<Users>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Email)
                  .IsRequired()
                  .HasColumnName("email");

        entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasColumnName("first_name");

        entity.Property(e => e.IsAdmin)
                  .IsRequired()
                  .HasColumnName("isAdmin");

        entity.Property(e => e.Password)
                  .IsRequired()
                  .HasColumnName("password");

        entity.Property(e => e.Username)
                  .IsRequired()
                  .HasColumnName("username");
      });

      modelBuilder.HasSequence("bracket_picks_seq");
    }
  }
}
