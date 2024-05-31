using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Myte.WebAPI.BackEnd.Models.Entities
{
	//Representação da TB dentro da aplicação
	public class Usuarios
	{
		[Key]
		public int IdUsuario {  get; set; }
		public string? NomeUsuario {get; set; }
		public string? DataNascimento { get; set; }
		public string? EmailUsuario { get; set; }

        // Chave estrangeira para Departamentos
        public int IdDepartamento { get; set; }

        // Propriedade de navegação para o relacionamento com Departamentos
        [ForeignKey("IdDepartamento")]
        public Departamentos? Departamento { get; set; }



    }
}
