using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //ALUNOS
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
        Aluno[] GetAllAlunos(bool includeProfessor);
        Task<Aluno> GetAlunoByIdAsync(int alunoId, bool includeProfessor);
        Aluno GetAlunoById(int alunoId, bool includeProfessor);
        
        //PROFESSORES
        Task<Professor[]> GetAllProfessoresAsync(bool includeDisciplina);
        Professor[] GetAllProfessores(bool includeDisciplina);
        Task<Professor> GetProfessorByIdAsync(int professorId, bool includeDisciplina);
        Professor GetProfessorById(int professorId, bool includeDisciplina);

        //DISCIPLINAS
        Task<Disciplina[]> GetAllDisciplinasAsync(bool includeProfessor);
        Disciplina[] GetAllDisciplinas(bool includeProfessor);
        Task<Disciplina> GetDisciplinaByIdAsync(int disciplinaId, bool includeProfessor);
        Disciplina GetDisciplinaById(int disciplinaId, bool includeProfessor);

    }
}