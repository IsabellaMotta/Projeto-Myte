using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto.Myte.WebAPI.BackEnd.Models;
using Projeto.Myte.WebAPI.BackEnd.Models.Entities;

namespace Projeto.Myte.WebAPI.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private MyteDbContext _myteDbContext;

        public DepartamentosController(MyteDbContext myteContext)
        {
            _myteDbContext = myteContext;
        }


        //tarefa assícrona com uso da requisição HttpGet
        [HttpGet]
        [Route("GetAll")] //api/UsuarioController/GetAll
        public async Task<ActionResult> Get()
        {
            //criar prop para receber de forma assícrona todos os registros recuperados da base
            var listaDepartamentos = await _myteDbContext.Departamentos.ToListAsync();
            return Ok(listaDepartamentos);
        }

        //buscar registro único por id
        [HttpGet]
        [Route("GetOne/{id}")] //api/UsuarioController/GetOne/id
        public async Task<ActionResult> GetOne(int id)
        {
            var registroDepartamento = await _myteDbContext.Departamentos.FindAsync(id);
            if (registroDepartamento == null)
            {
                return NotFound();
            }
            return Ok(registroDepartamento);
        }

        //adicionar um registro
        [HttpPost]
        [Route("AddRegister")]
        public async Task<ActionResult> Post(Departamentos registroDepartamento)
        {
            _myteDbContext.Departamentos.Add(registroDepartamento);
            //inserir registro de forma assícrona
            await _myteDbContext.SaveChangesAsync();
            return Ok(registroDepartamento);
        }

        [HttpPut]
        [Route("UpdateRegister/{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, Departamentos novoRegistro)
        {
            var buscandoDepto = await _myteDbContext.Departamentos.FindAsync(id);

            if (buscandoDepto == null)
            {
                return NotFound();
            }

            buscandoDepto.IdDepartamento = novoRegistro.IdDepartamento;
            buscandoDepto.NomeDepartamento = novoRegistro.NomeDepartamento;



            await _myteDbContext.SaveChangesAsync();
            return Ok(buscandoDepto);
        }


        [HttpDelete]
        [Route("DeleteRegister")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteDepto = await _myteDbContext.Departamentos.FindAsync(id);

            if (deleteDepto == null)
            {
                return NotFound();

            }
            _myteDbContext.Remove(deleteDepto);
            await _myteDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
