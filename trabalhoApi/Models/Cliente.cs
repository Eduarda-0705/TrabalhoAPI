using System.ComponentModel.DataAnnotations;

namespace TrabalhoApi;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email em formato inválido.")]
    public string Email { get; set; } = string.Empty;

    [Range(18, 150, ErrorMessage = "Idade mínima é 18 anos.")]
    public int Idade { get; set; }
}