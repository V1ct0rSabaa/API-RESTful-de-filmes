namespace FilmeAPI.Data.DTO.Read;

public class ReadFilmeDto
{
    public string titulo { get; set; }
    public string genero { get; set; }
    public int duracaoMinutos { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
}
