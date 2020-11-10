using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext context;
        public Repository(SmartContext context)
        {
            this.context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            this.context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        public Aluno[] getAllAlunos(bool includeDisciplina)
        {
            IQueryable<Aluno> query = this.context.Alunos;
            if (includeDisciplina)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.professor);
            }
            query = query.AsNoTracking().OrderBy(aluno => aluno.Id);
            return query.ToArray();
        }

        public Aluno[] getAllAlunosByDisciplinaId()
        {
            throw new NotImplementedException();
        }

        public Professor[] getAllProfessores()
        {
            throw new NotImplementedException();
        }

        public Professor[] getAllProfessoresByDisciplinaId()
        {
            throw new NotImplementedException();
        }

        public Aluno[] getAlunoById()
        {
            throw new NotImplementedException();
        }

        public Professor[] getProfessorById()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return this.context.SaveChanges() > 0;
        }

        public void Upadate<T>(T entity) where T : class
        {
            this.context.Update(entity);
        }
    }
}
