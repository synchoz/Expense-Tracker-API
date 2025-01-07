using System.ComponentModel.DataAnnotations;

namespace ExpenseApi.Models;

public class Expense
{
    public int Id {get; set;}
    [StringLength(100, MinimumLength = 1)]
    public string Username {get; set;} = String.Empty;
    [StringLength(100, MinimumLength = 0)]
    public string Name {get; set; } = String.Empty;
    [StringLength(50, MinimumLength = 0)]
    public string Type{get; set;} = String.Empty;
    [StringLength(500, MinimumLength = 0)]
    public string Note {get; set;} = String.Empty;
    public decimal MoneySpent {get; set;}
    public DateTime CreatedDateAt {get; set;} = DateTime.Now;
    public DateTime LastUpdatedAt {get; set;} = DateTime.Now;
}