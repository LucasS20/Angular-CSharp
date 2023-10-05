using Microsoft.EntityFrameworkCore;
using Proeventos.Models;

namespace Proeventos.Data;

public class DataContext : DbContext
{
    public DbSet<Evento> Eventos { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options){}
}