using System.ComponentModel.DataAnnotations;

namespace projeto.DTO
{
    public class FornecedorDTO
    {
        [Required(ErrorMessage = "Necessário informar nome do fornecedor")]
        [MinLength(2, ErrorMessage = "O nome do fornecedor deve ter no minimo 2 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Necessário informar CNPJ do fornecedor")]
        [StringLength(14, ErrorMessage = "O CNPJ do fornecedor deve ter 14 caracteres", MinimumLength = 14)]
        public string CNPJ { get; set; }
    }
}