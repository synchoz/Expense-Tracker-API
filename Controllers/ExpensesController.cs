using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly ExpenseContext _context;

    public ExpensesController(ExpenseContext context)
    {
        _context = context;
    }


    [HttpGet("{username}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        string username)
    {
        Console.WriteLine(startDate);
        Console.WriteLine(endDate);
        ActionResult<IEnumerable<Expense>> expenses;
        if(startDate.Year > 2000 && endDate.Year > 2000)
        {                       
            expenses = await _context.Expenses
                        .Where(e => e.Username == username && 
                                e.CreatedDateAt >=  startDate &&
                                e.CreatedDateAt <= endDate 
                            )
                        .ToListAsync();
        } 
        else
        {
            expenses = await _context.Expenses
                        .Where(e => e.Username == username)
                        .ToListAsync();
        }

        if (expenses == null)
        {
            return NotFound("try something else...");
        }

        return expenses;
    }


    //PUT /api/Expenses/resource?username=johndoe&type=Groceries&name=Supermarket
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> PutExpense(
        [FromQuery] string username,
        [FromQuery] string type,
        [FromQuery] string name,
        [FromBody] Expense expenseData)
    {
        var expense = await _context.Expenses
                            .FirstOrDefaultAsync(e => e.Username == username && e.Type == type && e.Name == name);

        if (expense == null)
        {
            return NotFound();
        }

        expense.MoneySpent = expenseData.MoneySpent;
        expense.Note = expenseData.Note;
        expense.LastUpdatedAt = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }

        return Ok("Was succesfully updated");
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Expense>> PostTodoItem(Expense expenseData)
    {
        var expense = new Expense
        {
            Username = expenseData.Username,
            Type = expenseData.Type,
            Name = expenseData.Name,
            MoneySpent = expenseData.MoneySpent,
            Note = expenseData.Note,
            CreatedDateAt = DateTime.Now,
            LastUpdatedAt = DateTime.Now
        };

        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetExpense),
            new Expense
            {
                Id = expense.Id,
                Username = expense.Username,
                Name = expense.Name,
                Type = expense.Type,
                MoneySpent = expense.MoneySpent,
                Note = expense.Note,
                CreatedDateAt = expense.CreatedDateAt,
                LastUpdatedAt = expense.LastUpdatedAt
                });
    }

    // DELETE: api/Expenses/resource?username=johndoe&type=Groceries&name=Supermarket
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
        {
            return NotFound();
        }

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();

        return Ok("this was done succesfully");
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Expense>> GetExpense(int id)
    {
        var item = await _context.Expenses.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
}