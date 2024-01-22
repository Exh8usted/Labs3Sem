namespace McDonalds.Emitters;

using McDonalds.AdditionalClasses;

public class Order : IOrder
{
    public Config Config { get; set; }
    public int ID { get; set; }
    public OrderStage OrderStage { get; set; }
    public Dictionary<OrderComponent, int> Components { get; set; }
    public Dictionary<OrderComponent, bool> ComponentsChecklist { get; set; }
    public Dictionary<Locker, bool>  LockedBy{ get; set; }
    public Order(Config config)
    {
        Config = config;
        ID = IDGenerator.GetNextID();
        OrderStage = OrderStage.Formation;
        LockedBy = new Dictionary<Locker, bool>()
        {
            {Locker.Cashier, false},
            {Locker.Cook, false},
            {Locker.DeepFryer, false},
            {Locker.DrinksMan, false},
            {Locker.OrderCollector, false}
        };
        GenerateComponents();
    }
    private void GenerateComponents()
    {
        ComponentsChecklist = new Dictionary<OrderComponent, bool>()
        {
            {OrderComponent.BigSpecial, true},
            {OrderComponent.Cheeseburger, true},
            {OrderComponent.Coffee, true},
            {OrderComponent.Cola, true},
            {OrderComponent.Fries, true},
            {OrderComponent.Nuggets, true},
        };
        Components = new Dictionary<OrderComponent, int>()
        {
            {OrderComponent.BigSpecial, 0},
            {OrderComponent.Cheeseburger, 0},
            {OrderComponent.Coffee, 0},
            {OrderComponent.Cola, 0},
            {OrderComponent.Fries, 0},
            {OrderComponent.Nuggets, 0},
        };
        bool OrderIsEmpty = true;

        for (int i = 0; i < Config.bigSpecialAmount; i++)
        {
            if (ProbabilityCalculator.CalcProbablility(Config.bigSpecialProb))
            {
                Components[OrderComponent.BigSpecial]++;
                ComponentsChecklist[OrderComponent.BigSpecial] = false;
                OrderIsEmpty = false;
            }
        }
        for (int i = 0; i < Config.cheeseburgerAmount; i++)
        {
            if (ProbabilityCalculator.CalcProbablility(Config.cheeseburgerProb))
            {
                Components[OrderComponent.Cheeseburger]++;
                ComponentsChecklist[OrderComponent.Cheeseburger] = false;
                OrderIsEmpty = false;
            }
        }
        for (int i = 0; i < Config.coffeeAmount; i++)
        {
            if (ProbabilityCalculator.CalcProbablility(Config.coffeeProb))
            {
                Components[OrderComponent.Coffee]++;
                ComponentsChecklist[OrderComponent.Coffee] = false;
                OrderIsEmpty = false;
            }
        }
        for (int i = 0; i < Config.colaAmount; i++)
        {
            if (ProbabilityCalculator.CalcProbablility(Config.colaProb))
            {
                Components[OrderComponent.Cola]++;
                ComponentsChecklist[OrderComponent.Cola] = false;
                OrderIsEmpty = false;
            }
        }
        for (int i = 0; i < Config.friesAmount; i++)
        {
            if (ProbabilityCalculator.CalcProbablility(Config.friesProb))
            {
                Components[OrderComponent.Fries]++;
                ComponentsChecklist[OrderComponent.Fries] = false;
                OrderIsEmpty = false;
            }
        }
        for (int i = 0; i < Config.nuggetsAmount; i++)
        {
            if (ProbabilityCalculator.CalcProbablility(Config.nuggetsProb))
            {
                Components[OrderComponent.Nuggets]++;
                ComponentsChecklist[OrderComponent.Nuggets] = false;
                OrderIsEmpty = false;
            }
        }
        if (OrderIsEmpty)
        {
            Components[OrderComponent.Coffee] = 1;
            ComponentsChecklist[OrderComponent.Coffee] = false;
        }
    }
    public Dictionary<string, int> GetComponentsListInString()
    {
        Dictionary<string, int> result = new Dictionary<string, int>();
        foreach (var component in Components)
        {
            result.Add(component.Key.ToString(), component.Value);
        }
        return result;
    }
}
