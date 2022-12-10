using AutoMapper;
using CrudUsuarios.Context;
using CrudUsuarios.Context.DTOs;
using CrudUsuarios.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioDbContext _context;
        private readonly IMapper _mapper;
        public UsuariosController(UsuarioDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            if (usuario == null) BadRequest("Usuário inválido");
            usuario.DataCriacao = DateTime.Now;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarPorLogin), new { login = usuario.Login }, usuario);
        }

        [HttpPut("atualizar/{login}")]
        public IActionResult AtualizarUsuario(string login, [FromBody] UsuarioDTO usuarioDto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Login == login);
            if (usuario == null) return NotFound();
            usuario.DataAtualizacao = DateTime.Now;
            _mapper.Map(usuarioDto,usuario);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpGet]
        public IEnumerable<Usuario> ListarUsuarios()
        {
            var usuarios =  _context.Usuarios;
            return usuarios;
        }

        [HttpGet("{login}")]
        public IActionResult RecuperarPorLogin(string login)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u=>u.Login == login);
            if(usuario==null) return NotFound();
            var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDto);
        }

        [HttpPut("{login}")]
        public IActionResult AlterarStatus(string login)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u=>u.Login==login);
            if(usuario == null) return NotFound();
            usuario.Ativo = !usuario.Ativo;
            var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
            _context.SaveChanges();
            return Ok(usuarioDto);
        }
    }
}
