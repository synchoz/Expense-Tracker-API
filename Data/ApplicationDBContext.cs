using ExpenseApi.Models;
using Microsoft.EntityFrameworkCore;
namespace ExpenseApi.Models;


public class ExpenseContext : DbContext
{
    public ExpenseContext(DbContextOptions<ExpenseContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; } = null!;
    //need to do later a relation 1(User) to many(Expense) from User to Expense
    public DbSet<Expense> Expenses { get; set; } = null!;
}