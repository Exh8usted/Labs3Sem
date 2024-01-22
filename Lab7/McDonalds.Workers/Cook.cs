namespace McDonalds.Workers;

using McDonalds.Emitters;
using System.Threading.Tasks;
using McDonalds.AdditionalClasses;

public class Cook : IWorker
{
    public Config Config { get; set; }
    public int ID { get; set; }
    public WorkerLevel Level { get; set; }
    public bool IsFree { get; set; }
    public event WorkIsDone WorkIsDone;
    public Cook(Config config)
    {
        Config = config;
        ID = IDGenerator.GetNextID();
        Level = (WorkerLevel) ProbabilityCalculator.GetRandomValueInList(Config.cookLevelProbs);
        IsFree = true;
    }
    public async Task WorkAsync(IOrder emitter)
    {
        Logger.EventHandledCount++;

        if (!emitter.LockedBy[Locker.Cook] && IsFree && emitter.OrderStage == OrderStage.Cooking
        && !(emitter.ComponentsChecklist[OrderComponent.Cheeseburger] && emitter.ComponentsChecklist[OrderComponent.BigSpecial])){
            emitter.LockedBy[Locker.Cook] = true;
            IsFree = false;

            Logger.PrintOperationMessage($"Повар {ID} готовит заказ OR{emitter.ID}");

            await Task.Delay(CalcWorkTime(emitter.Components));
            
            Logger.PrintOperationMessage($"Повар {ID} приготовил заказ OR{emitter.ID}");

            emitter.ComponentsChecklist[OrderComponent.BigSpecial] = true;
            emitter.ComponentsChecklist[OrderComponent.Cheeseburger] = true;
            emitter.LockedBy[Locker.Cook] = false;

            await WorkIsDone.Invoke(this, emitter);
            Logger.EventGenerationCount++;
        }
    }
    private int CalcWorkTime(Dictionary<OrderComponent, int> components)
    {
        int time = (components[OrderComponent.Cheeseburger] + components[OrderComponent.BigSpecial]) * Config.cookMsMultiplier;
        return Level switch
        {
            WorkerLevel.Beginner => Config.beginnerWorkTime * time,
            WorkerLevel.Intermediate => Config.intermediateWorkTime * time,
            WorkerLevel.Professional => Config.professionalWorkTime * time,
            _ => time
        };
    }
}
