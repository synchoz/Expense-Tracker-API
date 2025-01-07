using System.ComponentModel.DataAnnotations;

namespace ExpenseApi.Models;

public class User
{
    public int Id {get; set;}
    [StringLength(100, MinimumLength = 1)]
    public string Username {get; set;} = string.Empty;
    [StringLength(100, MinimumLength = 0)]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
    public string Email {get; set;} = string.Empty;
    [StringLength(100, MinimumLength = 1)]
    public string PasswordHash {get; set;} = string.Empty;
    public DateTime CreatedDateAt {get; set;} = DateTime.Now;
    public DateTime LastLoginDateAt {get; set;} = DateTime.Now;
}

public class UserD
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedDateAt { get; set; }  = DateTime.Now;
    public DateTime LastLoginDateAt { get; set; }  = DateTime.Now;
}