using System.Text;

Console.OutputEncoding = Encoding.UTF8;

string reportPath = Path.Combine(AppContext.BaseDirectory, "sales_report.txt");

Console.WriteLine("Начинаем загрузку отчёта о продажах...");

string report = await DownloadReportAsync();
await File.WriteAllTextAsync(reportPath, report, Encoding.UTF8);

Console.WriteLine("Загрузка завершена.");
Console.WriteLine($"Отчёт сохранён в файл: {reportPath}");
Console.WriteLine("\n=== Содержимое отчёта ===");
Console.WriteLine(report);

// Имитация запроса к серверу: ждём 2 секунды и возвращаем данные
static async Task<string> DownloadReportAsync()
{
    Console.WriteLine("Запрос к серверу отправлен, ожидаем ответ...");
    await Task.Delay(2000);

    var sb = new StringBuilder();
    sb.AppendLine($"Отчёт о продажах от {DateTime.Now:dd.MM.yyyy HH:mm}");
    sb.AppendLine("Товар;Продано, шт.;Выручка, руб.");
    sb.AppendLine("Ноутбук;12;780000");
    sb.AppendLine("Смартфон;35;1225000");
    sb.AppendLine("Наушники;58;290000");
    sb.Append("Итого;105;2295000");
    return sb.ToString();
}
