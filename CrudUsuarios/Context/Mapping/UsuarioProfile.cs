using AutoMapper;
using CrudUsuarios.Context.DTOs;
using CrudUsuarios.Models;

namespace CrudUsuarios.Context.Mapping
{
    public class UsuarioProfile:Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDTO,Usuario>();
            CreateMap<Usuario, UsuarioDTO>();
        }
    }
}
