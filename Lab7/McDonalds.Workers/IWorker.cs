using McDonalds.Emitters;

namespace McDonalds.Workers;

public enum WorkerLevel 
{
    Beginner = 0,
    Intermediate = 1,
    Professional = 2
}

public delegate Task WorkIsDone(IWorker worker, IOrder order);
public interface IWorker
{
    public int ID { get; set; }
    public WorkerLevel Level { get; }
    public bool IsFree { get; set; }
    public event WorkIsDone WorkIsDone;
    public async Task WorkAsync(IOrder emitter) { }
}
