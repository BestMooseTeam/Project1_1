using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var savings = new SavingsAccount("Иван Петров", 10000m, minimumBalance: 1000m);
var checking = new CheckingAccount("Анна Смирнова", 5000m, serviceFee: 50m);

Console.WriteLine("=== Сберегательный счёт ===");
savings.PrintBalance();
savings.Deposit(2000m);
savings.Withdraw(5000m);
savings.Withdraw(6500m); // не пройдёт: остаток станет меньше минимального
savings.PrintBalance();

Console.WriteLine("\n=== Расчётный счёт ===");
checking.PrintBalance();
checking.Deposit(1000m);
checking.Withdraw(2000m); // спишется 2000 + 50 комиссии
checking.Withdraw(4000m); // не пройдёт: не хватает с учётом комиссии
checking.PrintBalance();

/// <summary>
/// Базовый банковский счёт. Пополнение общее, снятие каждый тип счёта реализует сам.
/// </summary>
abstract class BankAccount
{
    public string Owner { get; }
    public decimal Balance { get; protected set; }

    protected BankAccount(string owner, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("Владелец не может быть пустым.", nameof(owner));
        if (initialBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(initialBalance), "Начальный баланс не может быть отрицательным.");

        Owner = owner;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Ошибка: сумма пополнения должна быть больше нуля.");
            return;
        }

        Balance += amount;
        Console.WriteLine($"Пополнение на {amount:F2} руб. Баланс: {Balance:F2} руб.");
    }

    public abstract void Withdraw(decimal amount);

    public void PrintBalance() =>
        Console.WriteLine($"Владелец: {Owner}, баланс: {Balance:F2} руб.");
}

/// <summary>
/// Сберегательный счёт: при снятии на счёте должен остаться минимальный остаток.
/// </summary>
class SavingsAccount : BankAccount
{
    public decimal MinimumBalance { get; }

    public SavingsAccount(string owner, decimal initialBalance, decimal minimumBalance)
        : base(owner, initialBalance)
    {
        MinimumBalance = minimumBalance;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Ошибка: сумма снятия должна быть больше нуля.");
            return;
        }

        if (Balance - amount < MinimumBalance)
        {
            Console.WriteLine($"Отказ: после снятия {amount:F2} руб. остаток будет меньше минимального ({MinimumBalance:F2} руб.).");
            return;
        }

        Balance -= amount;
        Console.WriteLine($"Снято {amount:F2} руб. Баланс: {Balance:F2} руб.");
    }
}

/// <summary>
/// Расчётный счёт: за каждое снятие списывается плата за обслуживание.
/// </summary>
class CheckingAccount : BankAccount
{
    public decimal ServiceFee { get; }

    public CheckingAccount(string owner, decimal initialBalance, decimal serviceFee)
        : base(owner, initialBalance)
    {
        ServiceFee = serviceFee;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Ошибка: сумма снятия должна быть больше нуля.");
            return;
        }

        decimal total = amount + ServiceFee;
        if (total > Balance)
        {
            Console.WriteLine($"Отказ: недостаточно средств для снятия {amount:F2} руб. с учётом комиссии {ServiceFee:F2} руб.");
            return;
        }

        Balance -= total;
        Console.WriteLine($"Снято {amount:F2} руб. + комиссия {ServiceFee:F2} руб. Баланс: {Balance:F2} руб.");
    }
}
