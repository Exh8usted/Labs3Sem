namespace McDonalds.Workers;

using McDonalds.Emitters;
using System.Threading.Tasks;
using McDonalds.AdditionalClasses;

public class DeepFryer : IWorker
{
    public Config Config { get; set; }
    public int ID { get; set; }
    public WorkerLevel Level { get; set; }
    public bool IsFree { get; set; }
    public event WorkIsDone WorkIsDone;
    public DeepFryer(Config config)
    {
        Config = config;
        ID = IDGenerator.GetNextID();
        Level = (WorkerLevel) ProbabilityCalculator.GetRandomValueInList(Config.deepFryerLevelProbs);
        IsFree = true;
    }
    public async Task WorkAsync(IOrder emitter)
    {
        Logger.EventHandledCount++;

        if (!emitter.LockedBy[Locker.DeepFryer] && IsFree && emitter.OrderStage == OrderStage.Cooking
        // && (emitter.Components[OrderComponent.Fries] != 0 || emitter.Components[OrderComponent.Nuggets] != 0)
        && !(emitter.ComponentsChecklist[OrderComponent.Fries] && emitter.ComponentsChecklist[OrderComponent.Nuggets])){
            emitter.LockedBy[Locker.DeepFryer] = true;
            IsFree = false;
            Logger.PrintOperationMessage($"Чел на фритюре {ID} готовит стартеры для заказа OR{emitter.ID}");

            await Task.Delay(CalcWorkTime(emitter.Components));
            
            Logger.PrintOperationMessage($"Чел на фритюре {ID} приготовил стартеры для заказа OR{emitter.ID}");

            emitter.ComponentsChecklist[OrderComponent.Fries] = true;
            emitter.ComponentsChecklist[OrderComponent.Nuggets] = true;
            emitter.LockedBy[Locker.DeepFryer] = false;

            await WorkIsDone.Invoke(this, emitter);
            Logger.EventGenerationCount++;
        }
    }
    private int CalcWorkTime(Dictionary<OrderComponent, int> components)
    {
        int time = (components[OrderComponent.Fries] + components[OrderComponent.Nuggets]) * Config.deepFryerMsMultiplier;
        return Level switch
        {
            WorkerLevel.Beginner => Config.beginnerWorkTime * time,
            WorkerLevel.Intermediate => Config.intermediateWorkTime * time,
            WorkerLevel.Professional => Config.professionalWorkTime * time,
            _ => time
        };
    }
}