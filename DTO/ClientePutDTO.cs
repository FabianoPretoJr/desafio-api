using System.ComponentModel.DataAnnotations;

namespace projeto.DTO
{
    public class ClientePutDTO
    {
        [MinLength(2, ErrorMessage = "O nome do cliente deve ter no minimo 2 caracteres")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "E-mail do cliente em formato inválido")]
        public string Email { get; set; }

        [StringLength(12, ErrorMessage = "A senha do cliente deve ter de 6 até 12 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [StringLength(11, ErrorMessage = "O documento do cliente deve ter 11 caracteres", MinimumLength = 11)]
        public string Documento { get; set; }
    }
}