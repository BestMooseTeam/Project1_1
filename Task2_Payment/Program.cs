using System.Text;

Console.OutputEncoding = Encoding.UTF8;

// Имитация интернет-магазина: покупатель выбирает способ оплаты
decimal orderTotal = 3490m;

var methods = new Dictionary<string, IPaymentMethod>
{
    ["1"] = new CreditCard("1234 5678 9012 3456"),
    ["2"] = new PayPal("user@example.com"),
    ["3"] = new BankTransfer("40817810000000000001")
};

Console.WriteLine($"Сумма заказа: {orderTotal:F2} руб.");
Console.WriteLine("Выберите способ оплаты:");
Console.WriteLine("1 — Банковская карта");
Console.WriteLine("2 — PayPal");
Console.WriteLine("3 — Банковский перевод");
Console.Write("Ваш выбор: ");

string? choice = Console.ReadLine()?.Trim();
if (choice is not null && methods.TryGetValue(choice, out var method))
{
    method.Pay(orderTotal);
}
else
{
    Console.WriteLine("Неизвестный способ оплаты.");
}

// Демонстрация всех способов через общий интерфейс
Console.WriteLine("\n=== Демонстрация всех способов оплаты ===");
foreach (var m in methods.Values)
{
    m.Pay(orderTotal);
}

interface IPaymentMethod
{
    void Pay(decimal amount);
}

class CreditCard : IPaymentMethod
{
    private readonly string _cardNumber;

    public CreditCard(string cardNumber) => _cardNumber = cardNumber;

    // Показываем только последние 4 цифры карты
    public void Pay(decimal amount) =>
        Console.WriteLine($"Оплата картой **** {_cardNumber[^4..]} на сумму {amount:F2} руб. выполнена.");
}

class PayPal : IPaymentMethod
{
    private readonly string _email;

    public PayPal(string email) => _email = email;

    public void Pay(decimal amount) =>
        Console.WriteLine($"Оплата через PayPal ({_email}) на сумму {amount:F2} руб. выполнена.");
}

class BankTransfer : IPaymentMethod
{
    private readonly string _accountNumber;

    public BankTransfer(string accountNumber) => _accountNumber = accountNumber;

    public void Pay(decimal amount) =>
        Console.WriteLine($"Оплата банковским переводом на счёт {_accountNumber} на сумму {amount:F2} руб. выполнена.");
}
