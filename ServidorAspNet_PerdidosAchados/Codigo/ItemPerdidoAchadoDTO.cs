using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServidorAsp.Codigo
{
    [Table("tbitemperdidoachado")]
    public class ItemPerdidoAchadoDTO
    {
        [Key]
        public int pkitemperdidoachado { get; set; }  // Chave primária
        public string? nomeitemperdidoachado { get; set; }  // Nome do item perdido ou achado
        public string? descricaoitemperdidoachado { get; set; }  // Descrição do item
        public string? localitemperdidoachado { get; set; }  // Local do item
        public string? urlimagemitemperdidoachado { get; set; }  

        public string? dataitemperdidoachado { get; set; }  // Data do item perdido ou encontrado
        public bool? verificaritemperdido { get; set; }  // Status (Perdido ou Encontrado)
        public int? fkpessoaregistoitemperdidoachado { get; set; }  // FK para pessoa que registrou o item
        public string? observacaoentregaitemperdidoachado { get; set; }  // Observação sobre a entrega
        public bool? verificarprocessoconcluidoitemperdido { get; set; }  // Status (Perdido ou Encontrado)

    }
}
