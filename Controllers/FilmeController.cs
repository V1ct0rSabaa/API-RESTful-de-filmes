using AutoMapper;
using FilmeAPI.Data.DTO.Create;
using FilmeAPI.Data.DTO.Read;
using FilmeAPI.Data.DTO.Update;
using FilmeAPI.Models;
using FilmesApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace FilmeAPI.Controllers;

[ApiController]
[Route("controller")]
public class FilmeController : ControllerBase 
{
    private FilmeContext _context;
    private IMapper _mapper; //objeto do automapper

    public FilmeController(FilmeContext context) { _context = context; }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult adicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        var filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(recuperaFilmePorId),
            new { id = filme.id }, filme);
    }

    /// <summary>
    /// Retorna uma lista de filmes que pode ser utilizada com um iterator
    /// </summary>
    /// <param name="skip">Quantidade de elementos para pular na query</param>
    /// <param name="take">quantidade de elementos que serão mostrados</param>
    /// <returns>IEnumerable</returns>
    [HttpGet]
    public IEnumerable<ReadFilmeDto> recuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadFilmeDto>>
            (_context.Filmes.Skip(skip).Take(take));
         
        // skip e take são uma estratégia de paginação de conteudo web,
        // respectivamente pulando os primeiros elementos da lista e determinando quantos vão aparecer
    }

    /// <summary>
    /// Retorna o Filme, caso o id esteja correto 
    /// </summary>
    /// <param name="id">valor inteiro correspondente a PK do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Filme recuperado com sucesso</response>
    /// <response code="404">Filme não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult recuperaFilmePorId(int id)
    {
      Filme? filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);
        if(filme == null) return NotFound();
        ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    /// <summary>
    /// Altera os dados de um filme
    /// </summary>
    /// <param name="id"></param>
    /// <param name="filmeDto">Objeto com os dados atualizados</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Filme atualizado com sucesso</response>
    /// <response code="404">Filme não foi encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult atualizaFilmePorId(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.id == id);
        if (filme == null) return NotFound();

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Apaga um filme do banco de dados, procurando pelo id(PK)
    /// </summary>
    /// <param name="id">valor inteiro correspondente a PK do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Filme deletado com sucesso</response>
    /// <response code="404">Filme não foi encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult deletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(
             filme => filme.id == id);
        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
