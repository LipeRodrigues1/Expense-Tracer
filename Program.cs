using Expense_Tracer;

var service = new ExpenseService();

while (true)
{
    Console.WriteLine("1 - List Expenses");
    Console.WriteLine("2 - Add Expense");
    Console.WriteLine("3 - Delete Expense");
    Console.WriteLine("4 - Summary");
    Console.WriteLine("5 - Summary by Month");
    Console.WriteLine("0 - Exit");

    Console.WriteLine("Chose an option: ");
    var option = Console.ReadLine();
    switch (option)
    {
        case "1":
        service.ListExpense();
        break;
        
        case "2":
        Console.WriteLine("Description: ");
        var descriptionText = Console.ReadLine();
        string[] parts = descriptionText.Split(' ');
        string description1 = parts[0];
        decimal amount = decimal.Parse(parts[1]);
        service.AddExpense(description1, amount);
        Console.WriteLine("Expense Added Successfully!");
        break;

        case "3":
        Console.WriteLine("Please provide the expense ID: ");
        if (!int.TryParse(Console.ReadLine(),out int id))
            {
                Console.WriteLine("Invalid ID");
                break;
            }
        service.DeleteExpense(id);
        break;

        case "4":
        service.Summary();
        break;

        case "5":
        Console.WriteLine("Enter month: ");
            if (!int.TryParse(Console.ReadLine(), out int month))
            {
                Console.WriteLine("Invalid month!");
                break;
            }

        service.Summary(month);
        break;

        case "0":
        Console.WriteLine("Finish!");
        return;

    }
}
