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

        [HttpPost("cadastrar")]
        public IActionResult CadastrarUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            if (usuario == null) BadRequest("Usuário inválido");
            foreach(var item in _context.Usuarios)
            {
                if(usuario.Login==item.Login)
                {
                    return BadRequest("Usuário já existe");
                }
            }
            usuario.DataCriacao = DateTime.Now;
            usuario.SenhaHash();
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

        [HttpGet("listar")]
        public IEnumerable<Usuario> ListarUsuarios()
        {
            var usuarios =  _context.Usuarios;
            return usuarios;
        }

        [HttpGet("listar/{login}")]
        public IActionResult RecuperarPorLogin(string login)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u=>u.Login == login);
            if(usuario==null) return NotFound();
            var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDto);
        }

        [HttpPut("alterarStatus/{login}")]
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
