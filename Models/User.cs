namespace ExpenseApi.Models;

public class User
{
    public int Id {get; set;}
    public string Username {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty;
    public DateTime CreatedDateAt {get; set;} = DateTime.Now;
    public DateTime LastLoginDateAt {get; set;} = DateTime.Now;
}

public class UserD
{
    public int Id { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedDateAt { get; set; }  = DateTime.Now;
    public DateTime LastLoginDateAt { get; set; }  = DateTime.Now;
}