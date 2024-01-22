using McDonalds.Emitters;

namespace McDonalds;

public delegate Task OrderWork(IOrder emitter);
public delegate void OrderStart(int orderID, Dictionary<string, int> components);
public delegate void OrderAccept(int orderID);
public delegate void OrderEnd(int orderID);
public interface IEngine
{
}