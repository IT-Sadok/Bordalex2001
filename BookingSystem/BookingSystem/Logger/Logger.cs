namespace BookingSystem.Logger;

public class Logger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }

    public void LogWarning(string message) 
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[WARNING] {message}");
        Console.ResetColor();
    }

    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {message}");
        Console.ResetColor();
    }
}
