namespace McDonalds.AdditionalClasses;


public class Config
{
    /// <summary>
    /// Время работы движка (sec)
    /// </summary>
    public int workSeconds { get; set; }

    /// <summary>
    /// Вероятность генерации нового заказа в текущую секунду
    /// </summary>
    public double generateNewOrderProb { get; set; }
    
    public double returnOrderProb { get; set; }

    /// <summary>
    /// Число сотрудников каждого типа:
    /// [0]: Кассир
    /// [1]: Повар
    /// [2]: Чел на фритюре
    /// [3]: Чел на напитках
    /// [4]: Сборщик заказов
    /// </summary>
    public List<int> stuffAmount { get; set; }

    // Путь к логу
    public string logFilePath { get; set; }

    // Вероятности генерации определенного компонента в заказе
    public double cheeseburgerProb { get; set; }
    public double bigSpecialProb { get; set; }
    public double friesProb { get; set; }
    public double nuggetsProb { get; set; }
    public double colaProb { get; set; }
    public double coffeeProb { get; set; }

    // Число попыток сгенерировать продукт (максимальное число компонента в 1 заказе)
    public int cheeseburgerAmount { get; set; }
    public int bigSpecialAmount { get; set; }
    public int friesAmount { get; set; }
    public int nuggetsAmount { get; set; }
    public int colaAmount { get; set; }
    public int coffeeAmount { get; set; }

    // Доп множитель по времени для разных уровней сотрудников
    public int beginnerWorkTime { get; set; }
    public int intermediateWorkTime { get; set; }
    public int professionalWorkTime { get; set; }

    // Основной множитель времени для работников (ms)
    public int cashierMsMultiplier { get; set; }
    public int cookMsMultiplier { get; set; }
    public int deepFryerMsMultiplier { get; set; }
    public int drinksManMsMultiplier { get; set; }
    public int orderCollectorMsMultiplier { get; set; }

    // Распределение вероятности для определения уровня сотрудника при его создании
    public List<double> cashierLevelProbs { get; set; }
    public List<double> cookLevelProbs { get; set; }
    public List<double> deepFryerLevelProbs { get; set; }
    public List<double> drinksManLevelProbs { get; set; }
    public List<double> orderCollectorLevelProbs { get; set; }
}