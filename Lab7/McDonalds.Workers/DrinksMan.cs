namespace McDonalds.Workers;

using McDonalds.Emitters;
using System.Threading.Tasks;
using McDonalds.AdditionalClasses;

public class DrinksMan : IWorker
{
    public Config Config { get; set; }
    public int ID { get; set; }
    public WorkerLevel Level { get; set; }
    public bool IsFree { get; set; }
    public event WorkIsDone WorkIsDone;
    public DrinksMan(Config config)
    {
        Config = config;
        ID = IDGenerator.GetNextID();
        Level = (WorkerLevel) ProbabilityCalculator.GetRandomValueInList(Config.drinksManLevelProbs);
        IsFree = true;
    }
    public async Task WorkAsync(IOrder emitter)
    {
        Logger.EventHandledCount++;

        if (!emitter.LockedBy[Locker.DrinksMan] && IsFree && emitter.OrderStage == OrderStage.Cooking 
        // && (emitter.Components[OrderComponent.Cola] != 0 || emitter.Components[OrderComponent.Coffee] != 0)
        && !(emitter.ComponentsChecklist[OrderComponent.Cola] && emitter.ComponentsChecklist[OrderComponent.Coffee])){
            emitter.LockedBy[Locker.DrinksMan] = true;
            IsFree = false;
            Logger.PrintOperationMessage($"Чел на напитках {ID} разливает для заказа OR{emitter.ID}");

            await Task.Delay(CalcWorkTime(emitter.Components));
            
            Logger.PrintOperationMessage($"Чел на напитках {ID} разлил напитки для заказа OR{emitter.ID}");

            emitter.ComponentsChecklist[OrderComponent.Cola] = true;
            emitter.ComponentsChecklist[OrderComponent.Coffee] = true;
            emitter.LockedBy[Locker.DrinksMan] = false;

            await WorkIsDone.Invoke(this, emitter);
            Logger.EventGenerationCount++;
        }
    }
    private int CalcWorkTime(Dictionary<OrderComponent, int> components)
    {
        int time = (components[OrderComponent.Cola] + components[OrderComponent.Coffee]) * Config.drinksManMsMultiplier;
        return Level switch
        {
            WorkerLevel.Beginner => Config.beginnerWorkTime * time,
            WorkerLevel.Intermediate => Config.intermediateWorkTime * time,
            WorkerLevel.Professional => Config.professionalWorkTime * time,
            _ => time
        };
    }
}