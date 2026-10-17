using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = new UTF8Encoding(false);

// Журнал лежит рядом с exe-файлом программы
string logPath = Path.Combine(AppContext.BaseDirectory, "log.txt");

Console.Write("Введите имя пользователя: ");
string? userName = Console.ReadLine()?.Trim();
if (string.IsNullOrEmpty(userName))
    userName = "Гость";

// При каждом запуске добавляем новое событие в конец файла
string entry = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} — пользователь «{userName}» вошёл в систему";
try
{
    File.AppendAllText(logPath, entry + Environment.NewLine, Encoding.UTF8);
    Console.WriteLine("Событие записано в журнал.");

    // После входа читаем и выводим весь журнал
    Console.WriteLine($"\n=== Журнал событий ({logPath}) ===");
    foreach (string line in File.ReadAllLines(logPath, Encoding.UTF8))
    {
        Console.WriteLine(line);
    }
}
catch (IOException ex)
{
    Console.WriteLine($"Ошибка работы с файлом: {ex.Message}");
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("Ошибка: нет доступа к файлу журнала.");
}
