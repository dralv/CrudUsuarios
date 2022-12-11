using System.ComponentModel.DataAnnotations;

namespace CrudUsuarios.Context.DTOs
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage ="Seu nome completo é obrigatório")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage ="Email é obrigatório")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }
        [MinLength(6, ErrorMessage = "A senha precisa ter mais de 6 caracteres")]
        public string Senha { get; set; }
        //public DateTime DataCriacao { get; set; }
        //public DateTime DataAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }
}
