using Microsoft.EntityFrameworkCore;
using Projeto.Myte.WebAPI.BackEnd.Models.Entities;
namespace Projeto.Myte.WebAPI.BackEnd.Models
{
	//DbContext é uma classe nativa
	public class MyteDbContext : DbContext
	{
		public MyteDbContext(DbContextOptions<MyteDbContext> options) : base(options) { }


		//fazer referencia as entities definidas na aplicação
		public DbSet<Entities.Usuarios> Usuarios { get; set; }
		public DbSet<Entities.Departamentos> Departamentos { get; set; }

		//OnModelCreating ajuda a fazer o mapeamento da relação entre as tabelas
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			modelBuilder.Entity<Usuarios>(entity =>
			{
				entity.HasKey(e => e.IdUsuario);
			
			});
			
			modelBuilder.Entity<Departamentos>(entity =>
			{
				entity.HasKey(c => c.IdDepartamento);
				entity.HasMany(c => c.Usuarios)
					  .WithOne(e => e.Departamento) //a classe Usuarios não possui uma propriedade chamada Departamento
                      .HasForeignKey(c => c.IdUsuario);
			});
		}
	}
}
