using System.ComponentModel.DataAnnotations;

namespace TrabalhoApi;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome deve ter entre 3 e 120 caracteres.")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 120 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Estoque não pode ser negativo.")]
    public int Estoque { get; set; }
}