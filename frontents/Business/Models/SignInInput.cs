using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class SignInInput
{
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Display(Name = "Password")]
    public string Password { get; set; }
    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
    
}