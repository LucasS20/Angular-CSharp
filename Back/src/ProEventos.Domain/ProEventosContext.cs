using Microsoft.EntityFrameworkCore;

namespace ProEventos.Domain;

public class ProEventosContext : DbContext
{
    public DbSet<Event> Eventos { get; set; }
    public DbSet<Lot> Lots { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<EventSpeaker> EventSpeakers { get; set; }
    public DbSet<SocialMedia> SocialMedias { get; set; }
    public DbSet<Event> Events { get; set; }

    public ProEventosContext(DbContextOptions<ProEventosContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventSpeaker>()
            .HasKey(pe => new { pe.EventId, pe.SpeakerId });
    }
}