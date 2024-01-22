namespace McDonalds.Emitters;

public enum OrderStage 
{
    Formation = 0,
    Cooking = 1,
    Ready = 2,
    Done = 3
}

public enum Locker
{
    Cashier = 0,
    DrinksMan = 1,
    Cook = 2,
    DeepFryer = 3,
    OrderCollector = 4
}

public enum OrderComponent
{
    Cheeseburger = 0,
    BigSpecial = 1,
    Fries = 2,
    Nuggets = 3,
    Cola = 4,
    Coffee = 5
}

public interface IOrder
{
    public int ID { get; set; }
    public OrderStage OrderStage { get; set; }
    public Dictionary<OrderComponent, int> Components { get; set; }
    public Dictionary<OrderComponent, bool> ComponentsChecklist { get; set; }
    public Dictionary<Locker, bool> LockedBy { get; set; }
    public Dictionary<string, int> GetComponentsListInString();
}
