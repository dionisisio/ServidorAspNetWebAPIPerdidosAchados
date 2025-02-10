using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServidorAsp.Codigo;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("listausuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // ✅ Ajustado para evitar conflito de rotas
        [HttpGet("buscarpornome")]
        public async Task<ActionResult<UsuarioDTO>> GetUserByUsername([FromQuery] string nomeacessousuario)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(u => u.nomeacessousuario == nomeacessousuario);

            if (usuario == null)
                return NotFound($"Usuário com nome de acesso '{nomeacessousuario}' não encontrado.");

            return Ok(usuario);
        }

        // ✅ Ajustado para evitar conflito de rotas
        [HttpGet("buscarpornomeesenha")]
        public async Task<ActionResult<UsuarioDTO>> GetUserByUsernamePassword(
            [FromQuery] string nomeacessousuario,
            [FromQuery] string palavrapasseusuario)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(u => u.nomeacessousuario == nomeacessousuario && u.palavrapasseusuario == palavrapasseusuario);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            return Ok(usuario);
        }

        // ✅ Corrigido para evitar erro ao adicionar usuários
        [HttpPost("adicionarusuario")]
        public async Task<ActionResult<UsuarioDTO>> CreateUser([FromBody] UsuarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");

            var novoUsuario = new UsuarioDTO
            {
                nomeusuario = dto.nomeusuario,
                nomeacessousuario = dto.nomeacessousuario,
                palavrapasseusuario = dto.palavrapasseusuario,
                numerotelefoneusuario = dto.numerotelefoneusuario,
                enderecousuario = dto.enderecousuario
            };

            _context.Users.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserByUsername), new { nomeacessousuario = dto.nomeacessousuario }, dto);
        }

        // ✅ Ajustado para evitar exceções no PUT
        [HttpPut("atualizarusuario/{id}")]
        public async Task<ActionResult<UsuarioDTO>> UpdateUser(int id, [FromBody] UsuarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Usuário inválido");

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            existingUser.nomeusuario = dto.nomeusuario;
            existingUser.nomeacessousuario = dto.nomeacessousuario;
            existingUser.palavrapasseusuario = dto.palavrapasseusuario;
            existingUser.numerotelefoneusuario = dto.numerotelefoneusuario;
            existingUser.enderecousuario = dto.enderecousuario;

            await _context.SaveChangesAsync();
            return Ok(existingUser);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.pkusuario == id);
        }
    }
}
