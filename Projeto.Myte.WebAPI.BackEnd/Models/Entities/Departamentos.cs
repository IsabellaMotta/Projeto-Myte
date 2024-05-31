using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto.Myte.WebAPI.BackEnd.Models.Entities
{
	public class Departamentos
	{
		[Key]
		public int IdDepartamento { get; set; }
		public string? NomeDepartamento { get; set; }
		public int IdUsuario { get; set; }

		//[ForeignKey("IdUsuario")]
		[JsonIgnore] 
        public ICollection<Usuarios>? Usuarios { get; set; } // Coleção de usuários
    }
}
