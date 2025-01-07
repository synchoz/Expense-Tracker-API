namespace ExpenseApi.Models;

public class Expense
{
    public int Id {get; set;}
    public string Username {get; set;} = String.Empty;
    public string Name {get; set; } = String.Empty;
    public string Type{get; set;} = String.Empty;
    public string Note {get; set;} = String.Empty;
    public decimal MoneySpent {get; set;}
    public DateTime CreatedDateAt {get; set;} = DateTime.Now;
    public DateTime LastUpdatedAt {get; set;} = DateTime.Now;
}