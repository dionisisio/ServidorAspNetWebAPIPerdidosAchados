using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServidorAsp.Codigo;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPerdidoAchadoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ItemPerdidoAchadoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("listaItensperdidosachado")]
        public async Task<ActionResult<IEnumerable<ItemPerdidoAchadoDTO>>> GetItensPerdidosAchados()
        {
            return await _context.ItensPerdidosAchados.ToListAsync();
        }

      
        [HttpGet("buscarpkitemperdidoachado")]
        public async Task<ActionResult<ItemPerdidoAchadoDTO>> ObterItemporPK(
            [FromQuery] int pkitemperdidoachado)
        {
            var item = await _context.ItensPerdidosAchados
                .FirstOrDefaultAsync(u => u.pkitemperdidoachado == pkitemperdidoachado);

            if (item == null)
                return NotFound($"Iten não encontrado");

            return Ok(item);
        }
       

       
        [HttpPost("adicionaritemperdidoachado")]
        public async Task<ActionResult<ItemPerdidoAchadoDTO>> CreateAdicionarItemUser(
            [FromBody] ItemPerdidoAchadoDTO dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos.");

            var novoitem = new ItemPerdidoAchadoDTO
            {

                nomeitemperdidoachado = dto.nomeitemperdidoachado,
                descricaoitemperdidoachado = dto.descricaoitemperdidoachado,
                localitemperdidoachado = dto.localitemperdidoachado,
                urlimagemitemperdidoachado = dto.urlimagemitemperdidoachado,
                verificaritemperdido = dto.verificaritemperdido,
                fkpessoaregistoitemperdidoachado = dto.fkpessoaregistoitemperdidoachado,
                verificarprocessoconcluidoitemperdido = dto.verificarprocessoconcluidoitemperdido,
                observacaoentregaitemperdidoachado = dto.observacaoentregaitemperdidoachado,
                dataitemperdidoachado = dto.dataitemperdidoachado
            };

            _context.ItensPerdidosAchados.Add(novoitem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterItemporPK), new { pkitemperdidoachado = dto.pkitemperdidoachado }, dto);
        }
        [HttpGet("buscarusuariopkitemperdidoachado")]
        public async Task<ActionResult<IEnumerable<object>>> ObterUsuarioItemporPK(
            [FromQuery] int pkitemperdidoachado)
        {
            var usuarioItens = await _context.Users
                .Join(_context.ItensPerdidosAchados,
                      usuario => usuario.pkusuario,
                      item => item.fkpessoaregistoitemperdidoachado,
                      (usuario, item) => new
                      {
                          nomeusuario = usuario.nomeusuario,
                          nomeacessousuario = usuario.nomeacessousuario,
                          palavrapasseusuario = usuario.palavrapasseusuario,
                          numerotelefoneusuario = usuario.numerotelefoneusuario,
                          enderecousuario = usuario.enderecousuario,

                          pkitemperdidoachado=item.pkitemperdidoachado,
                          nomeitemperdidoachado = item.nomeitemperdidoachado,
                          descricaoitemperdidoachado = item.descricaoitemperdidoachado,
                          localitemperdidoachado = item.localitemperdidoachado,
                          urlimagemitemperdidoachado = item.urlimagemitemperdidoachado,
                          verificaritemperdido = item.verificaritemperdido,
                          fkpessoaregistoitemperdidoachado = item.fkpessoaregistoitemperdidoachado,
                          verificarprocessoconcluidoitemperdido = item.verificarprocessoconcluidoitemperdido,
                          observacaoentregaitemperdidoachado = item.observacaoentregaitemperdidoachado,
                          dataitemperdidoachado = item.dataitemperdidoachado

                        
                      }).FirstOrDefaultAsync(u => u.pkitemperdidoachado == pkitemperdidoachado);

            return Ok(usuarioItens);
        }
        [HttpGet("listaitensperdidosachadosporusuario")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuarioItens()
        {
            var usuarioItens = await _context.Users
                .Join(_context.ItensPerdidosAchados,
                      usuario => usuario.pkusuario,
                      item => item.fkpessoaregistoitemperdidoachado,
                      (usuario, item) => new
                      {
                          nomeusuario = usuario.nomeusuario,
                          nomeacessousuario = usuario.nomeacessousuario,
                          palavrapasseusuario = usuario.palavrapasseusuario,
                          numerotelefoneusuario = usuario.numerotelefoneusuario,
                          enderecousuario = usuario.enderecousuario,
                          pkitemperdidoachado = item.pkitemperdidoachado,
                          nomeitemperdidoachado = item.nomeitemperdidoachado,
                          descricaoitemperdidoachado = item.descricaoitemperdidoachado,
                          localitemperdidoachado = item.localitemperdidoachado,
                          urlimagemitemperdidoachado = item.urlimagemitemperdidoachado,
                          verificaritemperdido = item.verificaritemperdido,
                          fkpessoaregistoitemperdidoachado = item.fkpessoaregistoitemperdidoachado,
                          verificarprocessoconcluidoitemperdido = item.verificarprocessoconcluidoitemperdido,
                          observacaoentregaitemperdidoachado = item.observacaoentregaitemperdidoachado,
                          dataitemperdidoachado = item.dataitemperdidoachado


                      }).Where(X => X.verificarprocessoconcluidoitemperdido == false)
                .ToListAsync();

            return Ok(usuarioItens);
        }

        [HttpPut("atualizaritem/{id}")]
        public async Task<ActionResult<ItemPerdidoAchadoDTO>> UpdateItem(int id, [FromBody] ItemPerdidoAchadoDTO dto)
        {
            if (dto == null)
                return BadRequest("Item inválido");

            var Item = await _context.ItensPerdidosAchados.FindAsync(id);
            if (Item == null)
                return NotFound($"Item com ID {id} não encontrado.");

            Item.nomeitemperdidoachado = dto.nomeitemperdidoachado;
            Item.descricaoitemperdidoachado = dto.descricaoitemperdidoachado;
            Item.localitemperdidoachado = dto.localitemperdidoachado;
            Item.urlimagemitemperdidoachado = dto.urlimagemitemperdidoachado;
            Item.verificaritemperdido = dto.verificaritemperdido;
            Item.fkpessoaregistoitemperdidoachado = dto.fkpessoaregistoitemperdidoachado;
            Item.verificarprocessoconcluidoitemperdido = dto.verificarprocessoconcluidoitemperdido;
            Item.observacaoentregaitemperdidoachado = dto.observacaoentregaitemperdidoachado;
            Item.dataitemperdidoachado = dto.dataitemperdidoachado;
                       

            await _context.SaveChangesAsync();
            return Ok(Item);
        }

        [HttpDelete("eliminarregisto/{id}")]
        public async Task<ActionResult> EliminarRegisto(int id)
        {
            var item = await _context.ItensPerdidosAchados.FindAsync(id);

            if (item == null)
                return NotFound($"Item com ID {id} não encontrado.");

            _context.ItensPerdidosAchados.Remove(item);
            await _context.SaveChangesAsync();

            return Ok($"Item com ID {id} foi removido com sucesso.");
        }


        private bool ItemExists(int id)
        {
            return _context.ItensPerdidosAchados.Any(e => e.pkitemperdidoachado == id);
        }
    }
}
