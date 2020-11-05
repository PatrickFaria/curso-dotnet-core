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
        private readonly SmartContext context;
        public AlunoController(SmartContext context) {
            this.context = context;
        }

        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.context.Alunos);
        }

        // GET api/<AlunoController>/5
        [HttpGet("byId")]
        public IActionResult GetById(int id)
        {
            var aluno = this.context.Alunos.FirstOrDefault(aluno => aluno.Id == id);
            if(aluno == null)
            {
                return BadRequest("O Aluno não foi encontrado");
            }
            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = this.context.Alunos.FirstOrDefault(aluno => aluno.Nome.Contains(nome) && aluno.Sobrenome.Contains(sobrenome));
            if (aluno == null)
            {
                return BadRequest("O Aluno não foi encontrado");
            }
            return Ok(aluno);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            this.context.Add(aluno);
            this.context.SaveChanges();
            return Ok(aluno);
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = this.context.Alunos.AsNoTracking().FirstOrDefault(aluno => aluno.Id == id);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado");
            }
            this.context.Update(aluno);
            this.context.SaveChanges();
            return Ok(aluno);
        }

        // PATCH api/<AlunoController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = this.context.Alunos.FirstOrDefault(aluno => aluno.Id == id);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado");
            }
            this.context.Update(aluno);
            this.context.SaveChanges();
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
            this.context.Remove(aluno);
            this.context.SaveChanges();
            return Ok();
        }
    }
}
