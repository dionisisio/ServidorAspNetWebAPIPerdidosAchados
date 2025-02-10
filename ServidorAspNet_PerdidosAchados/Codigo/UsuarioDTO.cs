using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServidorAsp.Codigo
{
    [Table("tbusuario")]
    public class UsuarioDTO
    {
        [Key]  // Define a chave primária
        public int pkusuario { get; set; }
        public string? nomeusuario { get; set; }
        public string? nomeacessousuario { get; set; }
        public string? palavrapasseusuario { get; set; }
        public string? numerotelefoneusuario { get; set; }
        public string? enderecousuario { get; set; }
    }
}
