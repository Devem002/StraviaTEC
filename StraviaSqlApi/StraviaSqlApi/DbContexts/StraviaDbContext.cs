using Microsoft.EntityFrameworkCore;
using StraviaSqlApi.Entities;
using StraviaSqlApi.Utilities;

namespace StraviaSqlApi.DbContexts;

public class StraviaDbContext : DbContext
{
    public StraviaDbContext(DbContextOptions<StraviaDbContext> options)
        : base(options)
    {
        
    }
    public virtual DbSet<ActCarrera> ACT_CARRERA { set; get; }
    
    public virtual DbSet<Actividad> ACTIVIDAD { set; get; }
    
    public virtual DbSet<ActReto> ACT_RETO { set; get; }
    
    public virtual DbSet<Amigos> AMIGOS { set; get; }
    
    public virtual DbSet<Atleta> ATLETA { set; get; }
    
    public virtual DbSet<Carrera> CARRERA { set; get; }
    
    public virtual DbSet<Categoria> CATEGORIA { set; get; }
    
    public virtual DbSet<ClasificacionActividad> CLASIFICACION_ACTIVIDAD { set; get; }
    
    public virtual DbSet<CuentaBanco> CUENTA_BANCO { set; get; }
    
    public virtual DbSet<FondoAltura> FONDO_ALTURA { set; get; }
    
    public virtual DbSet<Grupos> GRUPOS { set; get; }
    
    public virtual DbSet<Inscrito> INSCRITO { set; get; }
    
    public virtual DbSet<Integrantes> INTEGRANTES { set; get; }
    
    public virtual DbSet<Nacionalidad> NACIONALIDAD { set; get; }
    
    public virtual DbSet<PatrocinaCarrera> PATROCINA_CARRERA { set; get; }
    
    public virtual DbSet<Patrocinador> PATROCINADOR { set; get; }
    
    public virtual DbSet<PatrocinaReto> PATROCINA_RETO { set; get; }
    
    public virtual DbSet<PrivCarrera> PRIV_CARRERA { set; get; }
    
    public virtual DbSet<PrivReto> PRIV_RETO { set; get; }
    
    public virtual DbSet<Retos> RETOS { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Conversion DateTime a TimeOnly
        modelBuilder
            .Entity<Actividad>()
            .Property(p => p.Duracion)
            .HasConversion(
                v => new DateTime(v.Ticks),
                v => new TimeOnly(v.TimeOfDay.Ticks));
        
        modelBuilder
            .Entity<Actividad>()
            .Property(p => p.Hora)
            .HasConversion(
                v => new DateTime(v.Ticks),
                v => new TimeOnly(v.TimeOfDay.Ticks));
        
        modelBuilder.Entity<Actividad>()
            .Property(a => a.Fecha)
            .HasConversion(new DateOnlyConverter());
        
        modelBuilder.Entity<Atleta>()
            .Property(a => a.Fecha_nacimiento)
            .HasConversion(new DateOnlyConverter());
        
        modelBuilder.Entity<Atleta>().
            HasOne(d => d.NacionalidadNav).WithMany(p => p.Atleta)
            .HasForeignKey(d => d.Nacionalidad)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("ATLETA_NACIONALIDAD_FK");
        
        modelBuilder.Entity<Carrera>()
            .Property(a => a.Fecha)
            .HasConversion(new DateOnlyConverter());
        
        modelBuilder
            .Entity<Carrera>()
            .Property(p => p.Hora)
            .HasConversion(
                v => new DateTime(v.Ticks),
                v => new TimeOnly(v.TimeOfDay.Ticks));

        modelBuilder.Entity<Retos>()
            .Property(a => a.Fecha_inicio)
            .HasConversion(new DateOnlyConverter());
        
        modelBuilder.Entity<Retos>()
            .Property(a => a.Fecha_fin)
            .HasConversion(new DateOnlyConverter());
        
       /* modelBuilder.Entity<Amigos>()
            .HasOne(e => e.User)
            .WithMany(e => e.AmigosList)
            .HasForeignKey(e => e.Usuario);
        */
    }
}