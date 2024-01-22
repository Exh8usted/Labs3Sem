namespace McDonalds;

using McDonalds.AdditionalClasses;
using McDonalds.Emitters;
using McDonalds.Workers;

public class Engine : IEngine
{
    public static Config Config { get; set; }
    public List<IOrder> Emitters { get; set; }
    public List<IWorker> Workers { get; set; }

    public event OrderWork DoOrder;
    public static event OrderStart? OrderCreated;
    public static event OrderAccept? OrderAccepted;
    public static event OrderEnd? OrderDone;
    public Engine(Config config)
    {
        Config = config;

        Emitters = new List<IOrder>();
        Workers = new List<IWorker>();

        GenerateWorkers();

        OrderCreated += Logger.HandleOrderStart;
        OrderAccepted += Logger.HandleOrderAccept;
        OrderDone += Logger.HandleOrderEnd;
    }
    private void GenerateWorkers()
    {
        for (int i = 0; i < Config.stuffAmount[0]; i++)
        {
            IWorker newCashier = new Cashier(Config);
            Workers.Add(newCashier);
        }
        for (int i = 0; i < Config.stuffAmount[1]; i++)
        {
            IWorker newCook = new Cook(Config);
            Workers.Add(newCook);
        }
        for (int i = 0; i < Config.stuffAmount[2]; i++)
        {
            IWorker newDeepFryer = new DeepFryer(Config);
            Workers.Add(newDeepFryer);
        }
        for (int i = 0; i < Config.stuffAmount[3]; i++)
        {
            IWorker newDrinksMan = new DrinksMan(Config);
            Workers.Add(newDrinksMan);
        }
        for (int i = 0; i < Config.stuffAmount[4]; i++)
        {
            IWorker newOrderCollector = new OrderCollector(Config);
            Workers.Add(newOrderCollector);
        }

        foreach(IWorker w in Workers)
        {
            DoOrder += w.WorkAsync;
            w.WorkIsDone += HandleOrder;
        }
    }
    public void GenerateNewOrder()
    {
        IOrder order = new Order(Config);
        Emitters.Add(order);

        OrderCreated.Invoke(order.ID, order.GetComponentsListInString());
        Logger.EventGenerationCount++;

        DoOrder.Invoke(order);
        Logger.EventGenerationCount++;
    }
    public async Task Run(int workSeconds)
    {
        Logger.PrintOperationMessage($"-----------Смена от {DateTime.Now}-----------\n\n");
        GenerateNewOrder();
        for (int i = 0; i < workSeconds; i++)
        {
            if (ProbabilityCalculator.CalcProbablility(Config.generateNewOrderProb))
            {
                GenerateNewOrder();
            }
            await Task.Delay(1000);
        }
        Logger.FinalQueues = CountUnhandledOrders();
    }
    public async Task HandleOrder(IWorker worker, IOrder order)
    {
        
        switch (order.OrderStage)
        {
            case OrderStage.Formation:
            HandleAcceptedOrder(order);
            break;

            case OrderStage.Cooking:
            HandleCookedOrder(order);
            break;

            case OrderStage.Ready:
            HandleReadyOrder(order);
            break;
        }

        worker.IsFree = true;
        Logger.EventHandledCount++;

        foreach (IOrder o in Emitters)
        {
            if (o.OrderStage != OrderStage.Done)
            {
                await DoOrder.Invoke(o);
                Logger.EventGenerationCount++;
            }
        }

        Logger.AddQueuesStat(CountUnhandledOrders());
    }
    private static void HandleAcceptedOrder(IOrder order)
    {
        order.OrderStage = OrderStage.Cooking;
        OrderAccepted.Invoke(order.ID);
        Logger.EventGenerationCount++;
    }
    private static void HandleCookedOrder(IOrder order)
    {
        if (!order.ComponentsChecklist.ContainsValue(false))
        {
            order.OrderStage = OrderStage.Ready;
        }
    }
    private static void HandleReadyOrder(IOrder order)
    {
        if (ProbabilityCalculator.CalcProbablility(Config.returnOrderProb))
        {
            HandleReturn(order);
            order.OrderStage = OrderStage.Cooking;
        }
        else
        {
            order.OrderStage = OrderStage.Done;
            OrderDone.Invoke(order.ID);
        }
        Logger.EventGenerationCount++;
    }
    private static void HandleReturn(IOrder order)
    {
        foreach(var component in order.Components)
        {
            if (component.Value != 0)
            {
                order.ComponentsChecklist[component.Key] = false;
                Logger.PrintOperationMessage($"---В заказ OR{order.ID} не доложили {component.Key}. Возврат на готовку---");
                Logger.FailedOrdersCount++;
                break;
            }
        }
    }
    private List<int> CountUnhandledOrders()
    {
        List<int> count = new List<int>() { 0, 0, 0 };
        foreach (var order in Emitters)
        {
            if (order.OrderStage != OrderStage.Done && !order.LockedBy.Values.Contains(true))
            {
                switch(order.OrderStage)
                {
                    case OrderStage.Formation:
                    count[0]++;
                    break;

                    case OrderStage.Cooking:
                    count[1]++;
                    break;

                    case OrderStage.Ready:
                    count[2]++;
                    break;
                }
            }
        }
        return count;
    }
}
