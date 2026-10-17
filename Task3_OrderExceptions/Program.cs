using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = new UTF8Encoding(false);

Console.WriteLine("=== Оформление заказа ===");

try
{
    int quantity = ReadPositiveInt("Введите количество товара: ");
    decimal price = ReadPositiveDecimal("Введите цену за единицу: ");

    decimal total = quantity * price;
    Console.WriteLine($"Итоговая сумма заказа: {total:F2} руб.");
}
catch (FormatException)
{
    Console.WriteLine("Ошибка: нужно ввести число.");
}
catch (OverflowException)
{
    Console.WriteLine("Ошибка: число слишком большое.");
}
catch (InvalidOrderException ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}

static int ReadPositiveInt(string prompt)
{
    Console.Write(prompt);
    string input = Console.ReadLine() ?? "";

    // int.Parse бросает FormatException для нечислового ввода
    int value = int.Parse(input);
    if (value <= 0)
        throw new InvalidOrderException("количество должно быть больше нуля.");
    return value;
}

static decimal ReadPositiveDecimal(string prompt)
{
    Console.Write(prompt);
    // Разрешаем и запятую, и точку как разделитель
    string input = (Console.ReadLine() ?? "").Replace('.', ',');

    decimal value = decimal.Parse(input, new System.Globalization.CultureInfo("ru-RU"));
    if (value <= 0)
        throw new InvalidOrderException("цена должна быть больше нуля.");
    return value;
}

// Собственное исключение для некорректных данных заказа
class InvalidOrderException : Exception
{
    public InvalidOrderException(string message) : base(message) { }
}
