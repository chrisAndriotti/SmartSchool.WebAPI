using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V2.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V2.Controllers
{
    /// <summary>
    /// Versão 2.0
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public DisciplinaController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        /// <summary>
        /// Método responsável por retornar todas as disciplinas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var disciplinas = _repo.GetAllDisciplinas(false);

            return Ok(_mapper.Map<IEnumerable<DisciplinaDto>>(disciplinas));

        }

        /// <summary>
        /// Método responsável por retornar uma disciplina identificada pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var disciplina = _repo.GetDisciplinaById(id, false);
            if (disciplina == null) return BadRequest("Disciplina não encontrada!");

            var disciplinaDto = _mapper.Map<DisciplinaDto>(disciplina);

            return Ok(disciplinaDto);

        }

        // [HttpPost]
        // public IActionResult Post(DisciplinaDto disciplina){
        //     var disc = _mapper.Map<Disciplina>(disciplina);

        //     _repo.Add(disc);
        //     if (_repo.SaveChanges()){
        //         return Created($"/api/disciplina/{disciplina.Id}", _mapper.Map<DisciplinaDto>(disc));
        //     };

        //     return BadRequest("Disciplina não cadastrada!");
        // }
    }
}