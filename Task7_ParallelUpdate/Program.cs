using System.Diagnostics;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Обновляем данные главной страницы...");
var stopwatch = Stopwatch.StartNew();

// Запускаем все загрузки одновременно
Task<string> ratesTask = LoadCurrencyRatesAsync();
Task<string> newsTask = LoadNewsAsync();
Task<string> weatherTask = LoadWeatherAsync();

// Ждём завершения всех трёх задач
string[] results = await Task.WhenAll(ratesTask, newsTask, weatherTask);

stopwatch.Stop();

Console.WriteLine("\n=== Все данные обновлены ===");
foreach (string result in results)
{
    Console.WriteLine(result);
}

// Общее время ≈ самой долгой загрузке (3 с), а не сумме (1.5 + 2 + 3 = 6.5 с)
Console.WriteLine($"\nОбщее время: {stopwatch.Elapsed.TotalSeconds:F1} с");

static async Task<string> LoadCurrencyRatesAsync()
{
    Console.WriteLine("[Курсы валют] загрузка начата");
    await Task.Delay(1500);
    Console.WriteLine("[Курсы валют] загрузка завершена");
    return "Курсы валют: USD — 92,50 руб., EUR — 100,10 руб.";
}

static async Task<string> LoadNewsAsync()
{
    Console.WriteLine("[Новости] загрузка начата");
    await Task.Delay(2000);
    Console.WriteLine("[Новости] загрузка завершена");
    return "Новости: В городе открылся новый парк";
}

static async Task<string> LoadWeatherAsync()
{
    Console.WriteLine("[Погода] загрузка начата");
    await Task.Delay(3000);
    Console.WriteLine("[Погода] загрузка завершена");
    return "Погода: +12 °C, облачно";
}
