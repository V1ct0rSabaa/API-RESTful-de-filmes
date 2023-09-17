using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Models;

public class Filme
{
    [Key] //primary
    [Required]
    public int id { get; set; }

    [Required(ErrorMessage = "O título do filme deve ser informado")]
    public string titulo { get; set; }

    [Required(ErrorMessage = "O gênero do filme deve ser informado")]
    [StringLength(50, ErrorMessage = "O gênero do filme não pode exceder 50 caracteres")]
    public string genero { get; set; }

    [Required(ErrorMessage = "A duração do filme deve ser informada")]
    [Range(70, 240, ErrorMessage = "o filme deve durar de 70 até 240 minutos")]
    public int duracaoMinutos { get; set; }
}
