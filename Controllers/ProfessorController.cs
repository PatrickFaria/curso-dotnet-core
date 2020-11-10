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
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext context;
        private readonly IRepository repo;

        public ProfessorController(SmartContext context, IRepository repo)
        {
            this.context = context;
            this.repo = repo;
        }
        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.context.Professores);
        }

        // GET api/<ProfessorController>/5
        [HttpGet("byId")]
        public IActionResult Get(int id)
        {
            var professor = this.context.Professores.FirstOrDefault(professor => professor.Id == id);
            if(professor == null)
            {
                return BadRequest("Professor não encontrado");
            }
            return Ok(professor);
        }

        // POST api/<ProfessorController>
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            this.repo.Add(professor);
            if (this.repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não Cadastrado");
        }

        // PUT api/<ProfessorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = this.context.Professores.AsNoTracking().FirstOrDefault(professor => professor.Id == id);
            if (prof == null)
            {
                return BadRequest("Professor não encontrado");
            }
            this.repo.Update(professor);
            if (this.repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não Atualizado");
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = this.context.Professores.AsNoTracking().FirstOrDefault(professor => professor.Id == id);
            if (prof == null)
            {
                return BadRequest("Professor não encontrado");
            }
            this.repo.Delete(prof);
            if (this.repo.SaveChanges())
            {
                return Ok("Professor deletado");
            }
            return BadRequest("Professor não deletado");
        }
    }
}
