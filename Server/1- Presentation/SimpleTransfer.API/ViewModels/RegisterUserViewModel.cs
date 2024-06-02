using System.ComponentModel.DataAnnotations;

namespace SimpleTransfer.API.ViewModels;

public record RegisterUserViewModel
{
    [Required(ErrorMessage = "Nome do usuário é obrigatório")]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email é obrigatório")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Senha é obrigatória")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Documento é obrigatório")]
    public string Document { get; set; } = string.Empty;
}