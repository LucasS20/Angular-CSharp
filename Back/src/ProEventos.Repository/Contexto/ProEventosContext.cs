using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contexto;

public class ProEventosContext : DbContext
{
    
    public DbSet<Event> Events { get; set; }
    public DbSet<Lot> Lots { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<EventSpeaker> EventSpeakers { get; set; }
    public DbSet<SocialMedia> SocialMedias { get; set; }


    public ProEventosContext(DbContextOptions<ProEventosContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventSpeaker>()
            .HasKey(pe => new { pe.EventId, pe.SpeakerId });
        modelBuilder.Entity<Event>()
            .HasMany(e => e.SocialMedias)
            .WithOne(sm => sm.Event)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Speaker>()
            .HasMany(s => s.SocialMedias)
            .WithOne(sm => sm.Speaker)
            .OnDelete(DeleteBehavior.Cascade);
    }
}