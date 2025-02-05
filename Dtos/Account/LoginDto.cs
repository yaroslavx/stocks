using System.ComponentModel.DataAnnotations;

namespace stocks.Dtos.Account;

public class LoginDto
{
    [Required] 
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}