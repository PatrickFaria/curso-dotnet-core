using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository repo;
        public AlunoController(IRepository repo) {
            this.repo = repo;
        }

        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.repo.GetAllAlunos(true));
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = this.repo.GetAlunoById(id);
            if(aluno == null)
            {
                return BadRequest("O Aluno não foi encontrado");
            }
            return Ok(aluno);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            this.repo.Add(aluno);
            if (this.repo.SaveChanges())
            {
                return Ok(aluno);
            }
             return BadRequest("Aluno não Cadastrado");
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = this.repo.GetAlunoById(id);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado");
            }
            this.repo.Upadate(aluno);
            if (this.repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Professor não Atualizado");
        }

        // PATCH api/<AlunoController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = this.repo.GetAlunoById(id);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado");
            }
            this.repo.Upadate(aluno);
            this.repo.SaveChanges();
            return Ok(aluno);
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = this.context.Alunos.FirstOrDefault(aluno => aluno.Id == id);
            if(aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }
            this.repo.Delete(aluno);
            if (this.repo.SaveChanges())
            {
                return Ok("Aluno deletado");
            }
            return BadRequest("Aluno não deletado");
        }
    }
}
