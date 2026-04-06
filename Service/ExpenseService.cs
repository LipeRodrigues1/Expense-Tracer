using System.Text.Json;

namespace Expense_Tracer;

public class ExpenseService
{
    private readonly string filepath = "expenses.json";

    public List<Expense> GetExpenses()
    {
        if (!File.Exists(filepath))
        {
            return new List<Expense>();
        }
        var json = File.ReadAllText(filepath);

        if (string.IsNullOrEmpty(json))
        {
            return new List<Expense>();
        }
        return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
    
    }

    public void SaveExpenses(List<Expense> expenses)
    {
        string json = JsonSerializer.Serialize(expenses);
        File.WriteAllText(filepath, json);
    }

    public void AddExpense(string description, decimal amount)
    {
        var expenses = GetExpenses();
        int newId = expenses.Count > 0 ? expenses.Max(e => e.Id) + 1 : 1;
        var newExpense = new Expense
        {
            Id = newId,
            Description = description,
            Amount = amount,
            Date = DateTime.Now
        };

        expenses.Add(newExpense);
        SaveExpenses(expenses);
        
    }

    public void ListExpense()
    {
        var expenses = GetExpenses();
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses found!");
            return;
        }

        foreach (var expense in expenses)
        {
            Console.WriteLine($"ID: {expense.Id}, Description: {expense.Description}, Amount: {expense.Amount:C}, Date: {expense.Date: yyyy-MM-dd}");
        }
    }

    public void DeleteExpense(int id)
    {
        var expenses = GetExpenses();
        var expense = expenses.FirstOrDefault(e => e.Id == id);

        if (expense == null)
        {
            Console.WriteLine("Expense don't exists!");
            return;
        }

        expenses.Remove(expense);
        SaveExpenses(expenses);
        Console.WriteLine("Expense removed successfully!");
    }

    public void Summary()
    {
        var expenses = GetExpenses();

        if(expenses == null)
        {
            Console.WriteLine("There is no expense!");
            return;
        }

        var total = expenses.Sum(e => e.Amount);
        Console.WriteLine($"Expenses: {total:C}");
    }

    public void Summary(int month)
    {
        var expenses = GetExpenses();
        if(expenses == null)
        {
            Console.WriteLine("There are no expenses");
            return;
        }
        var monthExpenses = expenses.Where(e => e.Date.Month == month);

        if (!monthExpenses.Any())
        {
            Console.WriteLine("There are no expenses this month!");
            return;
        }
        monthExpenses.Sum(e => e.Amount);
        Console.WriteLine($"Summary of the month: {month}: {monthExpenses:C}");
    }

}
