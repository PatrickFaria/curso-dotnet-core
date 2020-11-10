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

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = this.context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(aluno => aluno.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = this.context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(aluno => aluno.Id)
                .Where(ad => ad.AlunosDisciplinas.Any(d => d.DisciplinaId == disciplinaId));
            return query.ToArray();
        }

        public Professor[] GetAllProfessores(bool includeAluno)
        {
            IQueryable<Professor> query = this.context.Professores;
            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas).ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking().OrderBy(p => p.Id);
            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAluno = false)
        {
            IQueryable<Professor> query = this.context.Professores;
            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas).ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking().OrderBy(p => p.Id)
                .Where(aluno => aluno.Disciplinas.Any(d => d.Id == disciplinaId));
            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = this.context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina)
                    .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(aluno => aluno.Id)
                .Where(aluno => aluno.Id == alunoId);
            return query.FirstOrDefault();
        }

        public Professor GetProfessorById(int professorId, bool includeAluno = false)
        {
            IQueryable<Professor> query = this.context.Professores;
            if (includeAluno)
            {
                query = query.Include(p => p.Disciplinas).ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking().OrderBy(p => p.Id)
                .Where(prof => prof.Id == professorId);
            return query.FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return this.context.SaveChanges() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            this.context.Update(entity);
        }
    }
}
