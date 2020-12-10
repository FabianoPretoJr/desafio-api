using System.ComponentModel.DataAnnotations;

namespace projeto.DTO
{
    public class CredenciaisDTO
    {
        [Required(ErrorMessage = "Necessário informar e-mail para login")]
        [EmailAddress(ErrorMessage = "E-mail está em formato inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Necessário informar senha para login")]
        [StringLength(12, ErrorMessage = "A senha deve ter de 6 até 12 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}