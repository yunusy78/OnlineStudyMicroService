using System.ComponentModel.DataAnnotations;

namespace Frontents.Business.Dtos.Auth;

public class UsersDto
{
    public string UserId { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email{ get; set; }
    [Required]
    public string City { get; set; }
}