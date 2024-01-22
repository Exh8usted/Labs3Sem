using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace McDonalds.AdditionalClasses;

public static class Logger
{
    public static int OrdersCount = 0;
    public static int FailedOrdersCount = 0;
    public static int EventGenerationCount = 0;
    public static int EventHandledCount = 0;

    public static double CashierQueues = 0;
    public static int CashierQueuesCount = 0;
    public static double CookesQueues = 0;
    public static int CookesQueuesCount = 0;
    public static double OrderCollectorQueues = 0;
    public static int OrderCollectorQueuesCount = 0;
    public static List<int> FinalQueues = new ();

    public static TimeSpan queueAvgTime = TimeSpan.Zero;
    public static TimeSpan executionAvgTime = TimeSpan.Zero;

    public static Dictionary<int, List<DateTime>> OrderStats = new ();

    public static string log = "";

    public static void PrintOperationMessage(string msg)
    {
        WriteLineToConsoleAndLog($"[{DateTime.Now:hh:mm:ss.fff}]: {msg}");
    }

    public static void HandleOrderStart(int emitterID, Dictionary<string, int> components)
    {
        DateTime startTime = DateTime.Now;
        WriteLineToConsoleAndLog($"[{startTime:hh:mm:ss}]: !!! Поступил новый заказ под номером OR{emitterID} !!!");
        PrintOrderComponents(emitterID, components);
        OrderStats.Add(emitterID, new List<DateTime>() { startTime });
        OrdersCount++;
        EventHandledCount++;
    }

    public static void HandleOrderAccept(int emitterID)
    {
        DateTime endTime = DateTime.Now;
        OrderStats[emitterID].Add(endTime);
        EventHandledCount++;
    }

    public static void HandleOrderEnd(int emitterID)
    {
        DateTime endTime = DateTime.Now;
        OrderStats[emitterID].Add(endTime);
        EventHandledCount++;
    }

    public static void PrintOrderStats()
    {
        WriteLineToConsoleAndLog("\n----------------\nСтатистика по заказам:");
        foreach (var stat in OrderStats)
        {
            if (stat.Value.Count == 3)
            {
                WriteLineToConsoleAndLog($" - ID = OR{stat.Key} : {stat.Value[0]:hh:mm:ss.fff}-{stat.Value[2]:hh:mm:ss.fff}");
            }
            else
            {
                WriteLineToConsoleAndLog($" - ID = OR{stat.Key} : {stat.Value[0]:hh:mm:ss.fff} (не завершен)");
            }
        }

        CalcAvgTime();
        CalcAvgQueues();

        WriteLineToConsoleAndLog($"\nВсего заказов: {OrdersCount}");
        WriteLineToConsoleAndLog($"Неправильно собранных заказов: {FailedOrdersCount}");
        WriteLineToConsoleAndLog($"Среднее время выполнения заказа: {executionAvgTime}");
        WriteLineToConsoleAndLog($"Среднее время в очереди у кассы: {queueAvgTime}");
        WriteLineToConsoleAndLog($"Среднее кол-во заказов в очереди у кассиров: {CashierQueues}");
        WriteLineToConsoleAndLog($"Среднее кол-во заказов в очереди у различных поваров: {CookesQueues}");
        WriteLineToConsoleAndLog($"Среднее кол-во заказов в очереди у сборщиков заказов: {OrderCollectorQueues}");
        WriteLineToConsoleAndLog($"Кол-во заказов в очереди у кассиров на момент завершения смены: {FinalQueues[0]}");
        WriteLineToConsoleAndLog($"Кол-во заказов в очереди у различных поваров на момент завершения смены: {FinalQueues[1]}");
        WriteLineToConsoleAndLog($"Кол-во заказов в очереди у сборщиков заказов на момент завершения смены: {FinalQueues[2]}");
        WriteLineToConsoleAndLog($"Сгенерировано событий: {EventGenerationCount}");
        WriteLineToConsoleAndLog($"Обработано событий: {EventHandledCount}\n----------------");
    }

    public static void PrintOrderComponents(int emitterID, Dictionary<string, int> components)
    {
        WriteLineToConsoleAndLog($"\t\tСостав заказа OR{emitterID}:");
        foreach (var component in components)
        {
            if (component.Value != 0)
            {
                WriteLineToConsoleAndLog($"\t\t\t{component.Key}: {component.Value}");
            }
        }
    }

    public static void AddQueuesStat(List<int> queues)
    {
        CashierQueues += queues[0];
        CashierQueuesCount++;

        CookesQueues += queues[1];
        CookesQueuesCount++;

        OrderCollectorQueues += queues[2];
        OrderCollectorQueuesCount++;
    }

    private static void WriteLineToConsoleAndLog(string msg)
    {
        Console.WriteLine(msg);
        log += msg + "\n";
    }

    private static void CalcAvgTime()
    {
        int count = 0;
        foreach (var times in OrderStats.Values)
        {
            if (times.Count == 3)
            {
                queueAvgTime += times[1] - times[0];
                executionAvgTime += times[2] - times[0];
                count++;
            }
        }
        queueAvgTime /= count;
        executionAvgTime /= count;
    }

    private static void CalcAvgQueues()
    {
        CashierQueues /= CashierQueuesCount;
        CookesQueues /= CookesQueuesCount;
        OrderCollectorQueues /= OrderCollectorQueuesCount;
    }
}
