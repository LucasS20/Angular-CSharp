using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace Proeventos.Data;

public class DataContext : DbContext
{
    public DbSet<Event> Eventos { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options){}
}