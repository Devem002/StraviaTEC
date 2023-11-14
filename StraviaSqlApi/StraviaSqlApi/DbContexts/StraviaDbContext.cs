using Microsoft.EntityFrameworkCore;
using StraviaSqlApi.Entities;

namespace StraviaSqlApi.DbContexts;

public class StraviaDbContext : DbContext
{
    public StraviaDbContext(DbContextOptions<StraviaDbContext> options)
        : base(options)
    {
        
    }
    public virtual DbSet<ActCarrera> Act_carrera { set; get; }
    
    public virtual DbSet<Actividad> Actividad { set; get; }
    
    public virtual DbSet<ActReto> Act_reto { set; get; }
    
    public virtual DbSet<Amigos> Amigos { set; get; }
    
    public virtual DbSet<Atleta> Atleta { set; get; }
    
    public virtual DbSet<Carrera> Carrera { set; get; }
    
    public virtual DbSet<Categoria> Categoria { set; get; }
    
    public virtual DbSet<ClasificacionActividad> Clasificacion_actividad { set; get; }
    
    public virtual DbSet<CuentaBanco> Cuenta_banco { set; get; }
    
    public virtual DbSet<FondoAltura> Fondo_altura { set; get; }
    
    public virtual DbSet<Grupos> Grupos { set; get; }
    
    public virtual DbSet<Inscrito> Inscrito { set; get; }
    
    public virtual DbSet<Integrantes> Integrantes { set; get; }
    
    public virtual DbSet<Nacionalidad> Nacionalidad { set; get; }
    
    public virtual DbSet<PatrocinaCarrera> Patrocina_carrera { set; get; }
    
    public virtual DbSet<Patrocinador> Patrocinador { set; get; }
    
    public virtual DbSet<PatrocinaReto> Patrocina_reto { set; get; }
    
    public virtual DbSet<PrivCarrera> Priv_carrera { set; get; }
    
    public virtual DbSet<PrivReto> Priv_reto { set; get; }
    
    public virtual DbSet<Retos> Retos { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Atleta>()
            .HasMany(e => e.AmigosList)
            .WithMany();

        modelBuilder.Entity<Amigos>()
            .HasOne(e => e.User)
            .WithMany(e => e.AmigosList)
            .HasForeignKey(e => e.Usuario);
        
        base.OnModelCreating(modelBuilder);
        
        
    }
}