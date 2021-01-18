using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        
        public Repository(SmartContext context)
        {
            _context = context;
        }

        // Métodos HTML

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        // Métodos ALUNOS

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            //query = query.AsNoTracking().OrderBy(a => a.Nome);

            if(!string.IsNullOrEmpty(pageParams.Nome)){
                query = query.Where(aluno => aluno.Nome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()) ||
                                             aluno.Sobrenome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()));
            }

            if (pageParams.Matricula > 0){
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);
            }

            if (pageParams.Ativo != null){
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));
            }

            switch (pageParams.OrderBy){
                case "id":{
                    query = query.AsNoTracking().OrderBy(a => a.Id);
                    break;
                }
                    
                case "nome":{
                    query = query.AsNoTracking().OrderBy(a => a.Nome);
                    break;
                }

                case "matricula":{
                    query = query.AsNoTracking().OrderBy(a => a.Matricula);
                    break;
                } 
            }

            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         //.OrderBy(a => a.Id)
                         .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }

        public async Task<Aluno> GetAlunoByIdAsync(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         //.OrderBy(a => a.Id)
                         .Where(aluno => aluno.Id == alunoId);

            return await query.FirstOrDefaultAsync();
        }

        // Métodos PROFESSORES
        public Professor[] GetAllProfessores(bool includeDisciplina = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeDisciplina){
                query = query.Include(d => d.Disciplina)
                             .ThenInclude(a => a.AlunosDisciplinas)
                             .ThenInclude(al => al.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return query.ToArray();
        }

        public async Task<Professor[]> GetAllProfessoresAsync(bool includeDisciplina = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeDisciplina){
                query = query.Include(d => d.Disciplina)
                             .ThenInclude(a => a.AlunosDisciplinas)
                             .ThenInclude(al => al.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public Professor GetProfessorById(int professorId, bool includeDisciplina = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeDisciplina){
                query = query.Include(d => d.Disciplina);
            }

            query = query.AsNoTracking()
                         .Where(prof => prof.Id == professorId);

            return query.FirstOrDefault();
        }

        public async Task<Professor> GetProfessorByIdAsync(int professorId, bool includeDisciplina = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeDisciplina){
                query = query.Include(d => d.Disciplina);
            }

            query = query.AsNoTracking()
                         .Where(prof => prof.Id == professorId);

            return await query.FirstOrDefaultAsync();
        }

        // Métodos DISCIPLINAS
        public Disciplina[] GetAllDisciplinas(bool includeProfessor = false)
        {
            IQueryable<Disciplina> query = _context.Disciplinas;

            // if(includeAlunos){
            //     query = query.Include(d => d.AlunosDisciplinas)
            //                  .ThenInclude(ad => ad.Aluno);
            // }

            if(includeProfessor){
                query = query.Include(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(d => d.Id);

            return query.ToArray();
        }

        public async Task<Disciplina[]> GetAllDisciplinasAsync(bool includeProfessor = false)
        {
            IQueryable<Disciplina> query = _context.Disciplinas;

            // if(includeAlunos){
            //     query = query.Include(d => d.AlunosDisciplinas)
            //                  .ThenInclude(ad => ad.Aluno);
            // }

            if(includeProfessor){
                query = query.Include(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(d => d.Id);

            return await query.ToArrayAsync();
        }

        public Disciplina GetDisciplinaById(int disciplinaId, bool includeProfessor)
        {
            IQueryable<Disciplina> query = _context.Disciplinas;

            if(includeProfessor){
                query = query.Include(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .Where(disc => disc.Id == disciplinaId);
                         
            return query.FirstOrDefault();
        }

        public async Task<Disciplina> GetDisciplinaByIdAsync(int disciplinaId, bool includeProfessor)
        {
            IQueryable<Disciplina> query = _context.Disciplinas;

            if(includeProfessor){
                query = query.Include(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .Where(disc => disc.Id == disciplinaId);
                         
            return await query.FirstOrDefaultAsync();
        }
    }
}