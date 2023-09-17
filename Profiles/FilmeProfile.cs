using AutoMapper;
using FilmeAPI.Data.DTO.Create;
using FilmeAPI.Data.DTO.Read;
using FilmeAPI.Data.DTO.Update;
using FilmeAPI.Models;

namespace FilmeAPI.Profiles;

public class FilmeProfile:Profile
{
    // realiza automaticamente a criação de objetos entre Filme e context
    public FilmeProfile() {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>(); 
        CreateMap<Filme, UpdateFilmeDto>();
        CreateMap<Filme, ReadFilmeDto>();
    }
}
