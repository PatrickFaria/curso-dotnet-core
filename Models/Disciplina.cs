using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Models
{
    public class Disciplina
    {
        public Disciplina()
        {

        }
        public Disciplina(int id, string nome, int professorId)
        {
            Id = id;
            Nome = nome;
            ProfesssorId = professorId;
        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int ProfesssorId { get; private set; }
        public Professor professor { get; private set; }

        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}
