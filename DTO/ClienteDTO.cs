using System.ComponentModel.DataAnnotations;

namespace projeto.DTO
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "Necessário informar nome do cliente")]
        [MinLength(2, ErrorMessage = "O nome do cliente deve ter no minimo 2 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Necessário informar e-mail do cliente")]
        [EmailAddress(ErrorMessage = "E-mail do cliente em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Necessário informar senha do cliente")]
        [StringLength(12, ErrorMessage = "A senha do cliente deve ter de 6 até 12 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Necessário informar documento do cliente")]
        [StringLength(11, ErrorMessage = "O documento do cliente deve ter 11 caracteres", MinimumLength = 11)]
        public string Documento { get; set; }
    }
}