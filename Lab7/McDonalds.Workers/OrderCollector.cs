namespace McDonalds.Workers;

using McDonalds.Emitters;
using System.Threading.Tasks;
using McDonalds.AdditionalClasses;

public class OrderCollector : IWorker
{
    public Config Config { get; set; }
    public int ID { get; set; }
    public WorkerLevel Level { get; set; }
    public bool IsFree { get; set; }
    public event WorkIsDone WorkIsDone;
    public OrderCollector(Config config)
    {
        Config = config;
        ID = IDGenerator.GetNextID();
        Level = (WorkerLevel) ProbabilityCalculator.GetRandomValueInList(Config.orderCollectorLevelProbs);
        IsFree = true;
    }
    public async Task WorkAsync(IOrder emitter)
    {
        Logger.EventHandledCount++;
        
        if (IsFree && !emitter.LockedBy[Locker.OrderCollector] && emitter.OrderStage == OrderStage.Ready){
            emitter.LockedBy[Locker.OrderCollector] = true;
            IsFree = false;
            
            Logger.PrintOperationMessage($"Чел на выдаче {ID} собирает заказ OR{emitter.ID}");

            await Task.Delay(CalcWorkTime(emitter.Components));
            
            Logger.PrintOperationMessage($"Чел на выдаче {ID} отдал заказ OR{emitter.ID}");

            emitter.LockedBy[Locker.OrderCollector] = false;

            await WorkIsDone.Invoke(this, emitter);
            Logger.EventGenerationCount++;
        }
    }
    private int CalcWorkTime(Dictionary<OrderComponent, int> components)
    {
        int time = 0;
        foreach (var amount in components.Values)
        {
            time += amount;
        }
        time *= Config.orderCollectorMsMultiplier;
        
        return Level switch
        {
            WorkerLevel.Beginner => Config.beginnerWorkTime * time,
            WorkerLevel.Intermediate => Config.intermediateWorkTime * time,
            WorkerLevel.Professional => Config.professionalWorkTime * time,
            _ => time
        };
    }
}
