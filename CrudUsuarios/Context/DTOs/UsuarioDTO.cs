﻿namespace CrudUsuarios.Context.DTOs
{
    public class UsuarioDTO
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        //public DateTime DataCriacao { get; set; }
        //public DateTime DataAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }
}
