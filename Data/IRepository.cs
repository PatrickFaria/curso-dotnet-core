﻿using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class; 
        void Upadate<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        Aluno[] GetAllAlunos (bool includeProfessor);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);
        Professor[] GetAllProfessores(bool includeAluno);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAluno = false);
        Professor GetProfessorById(int professorId, bool includeAluno = false);
    }
}