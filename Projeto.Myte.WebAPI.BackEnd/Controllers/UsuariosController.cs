using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto.Myte.WebAPI.BackEnd.Models;
using Projeto.Myte.WebAPI.BackEnd.Models.Entities;


namespace Projeto.Myte.WebAPI.BackEnd.Controllers
//camada intermediária que lida com as requisições HTTP e
//interage com a camada de dados (banco de dados) para realizar
//operações de CRUD (Create, Read, Update, Delete) sobre os registros de usuários. 
{
    // Controlador de API que lida com operações CRUD (Create, Read, Update, Delete) para registros de usuários
    [Route("api/[controller]")]
	[ApiController]
	public class UsuariosController : ControllerBase
	{
		private MyteDbContext _myteDbContext;

        // Construtor que recebe o contexto do banco de dados
		// via injeção de dependência
        public UsuariosController(MyteDbContext myteDbContext)
		{
			_myteDbContext = myteDbContext;
		}

		//tarefa assícrona com uso da requisição HttpGet
		[HttpGet]
        // Método para obter todos os usuários
        [Route("GetAll")] //api/UsuarioController/GetAll
		public async Task<ActionResult> Get()
		{
            // Busca todos os usuários no banco de dados de forma assíncrona
            //criar prop para receber de forma assícrona todos os registros recuperados da base
            var listaUsuarios = await _myteDbContext.Usuarios.ToListAsync();
			return Ok(listaUsuarios);
		}

		//Metodo para buscar registro único por id
		[HttpGet]
		[Route("GetOne/{id}")] //api/UsuarioController/GetOne/id
		public async Task<ActionResult> GetOne(int id)
		{
            // Busca o usuário pelo ID de forma assíncrona
            var registroUsuario = await _myteDbContext.Usuarios.FindAsync(id);
			if (registroUsuario == null)
			{
                // Retorna 404 (Not Found) se o usuário não for encontrado
                return NotFound();
			}
			return Ok(registroUsuario); // Retorna o usuário encontrado com status 200 (OK)
        }

		//adicionar um novo registro
		[HttpPost]
		[Route("AddRegister")]
		public async Task<ActionResult> Post(Usuarios registroUsuario)
		{
            // Adiciona o novo usuário ao banco de dados
            _myteDbContext.Usuarios.Add(registroUsuario);

			//salva as informações no banco de dados de forma assícrona
			await _myteDbContext.SaveChangesAsync();

            // Retorna o novo usuário com status 200 (OK)
            return Ok(registroUsuario);
		}

        // Método para atualizar um usuário existente
        [HttpPut]
		[Route("UpdateRegister/{id}")]
		public async Task<ActionResult> Put([FromRoute] int id, Usuarios novoRegistro)
		{
            // Busca o usuário pelo ID de forma assíncrona
            var buscandoUser = await _myteDbContext.Usuarios.FindAsync(id);

			if(buscandoUser == null)
			{
				return NotFound();
			}

            // Atualiza os campos do usuário existente com os novos dados
            buscandoUser.NomeUsuario = novoRegistro.NomeUsuario;
			buscandoUser.DataNascimento = novoRegistro.DataNascimento;
			buscandoUser.EmailUsuario = novoRegistro.EmailUsuario;

            // Salva as alterações no banco de dados de forma assíncrona
            await _myteDbContext.SaveChangesAsync();

            // Retorna o usuário atualizado com status 200 (OK)
            return Ok(buscandoUser);
		}

        // Método para excluir um usuário
        [HttpDelete]
		[Route("DeleteRegister")]
		public async Task<ActionResult> Delete(int id)
		{
            // Busca o usuário pelo ID de forma assíncrona
            var deleteUser = await _myteDbContext.Usuarios.FindAsync(id);

			if (deleteUser == null)
			{
				return NotFound();

			}
            // Remove o usuário do banco de dados
            _myteDbContext.Remove(deleteUser);
            // Salva as alterações no banco de dados de forma assíncrona
            await _myteDbContext.SaveChangesAsync();

			return Ok();
		}


	}
}
