namespace McDonalds.Workers;

using McDonalds.Emitters;
using System.Threading.Tasks;
using McDonalds.AdditionalClasses;

public class Cashier : IWorker
{
    public Config Config { get; set; }
    public int ID { get; set; }
    public WorkerLevel Level { get; set; }
    public bool IsFree { get; set; }
    
    public event WorkIsDone WorkIsDone;

    public Cashier(Config config)
    {
        Config = config;
        ID = IDGenerator.GetNextID();
        Level = (WorkerLevel) ProbabilityCalculator.GetRandomValueInList(Config.cashierLevelProbs);
        IsFree = true;
    }

    public async Task WorkAsync(IOrder emitter)
    {
        Logger.EventHandledCount++;
        
        if (IsFree && !emitter.LockedBy[Locker.Cashier] && emitter.OrderStage == OrderStage.Formation){
            emitter.LockedBy[Locker.Cashier] = true;
            IsFree = false;
            
            Logger.PrintOperationMessage($"Кассир {ID} принимает заказ OR{emitter.ID}");

            await Task.Delay(CalcWorkTime(emitter.Components));
            
            Logger.PrintOperationMessage($"Кассир {ID} принял заказ OR{emitter.ID}");

            emitter.LockedBy[Locker.Cashier] = false;

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
        time *= Config.cashierMsMultiplier;
        
        return Level switch
        {
            WorkerLevel.Beginner => Config.beginnerWorkTime * time,
            WorkerLevel.Intermediate => Config.intermediateWorkTime * time,
            WorkerLevel.Professional => Config.professionalWorkTime * time,
            _ => time
        };
    }
}
