using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V1.Dtos
{
    public class DisciplinaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        //public int PrerequisitoId { get; set; }
        //public Disciplina Prerequisito { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
        //public int AlunoId { get; set; }
        //public Professor Aluno { get; set; }

        //public int CursoId { get; set; }
        //public Curso Curso { get; set; }
    }
}