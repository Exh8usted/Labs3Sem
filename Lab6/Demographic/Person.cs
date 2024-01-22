using Demographic.FileOperations;

namespace Demographic;

public class Person : IPerson
{
    public static double ChanceOfMaleBirth = 0.45;
    public static double ChildBirthChance = 0.151;
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public bool IsAlive { get; set; }
    public int DeathYear { get; set; }
    public event ChildBirth ChildWasBorn;
    public event Death Die;
    
    /// <summary>
    /// Конструктор для рождения нового ребенка
    /// </summary>
    /// <param name="currentEngine">Движок, к которому привязан человек (для подписок на события)</param>
    public Person(Engine currentEngine)
    {
        if (ProbabilityCalculator.CalcProbablility(ChanceOfMaleBirth))
        {
            Gender = Gender.Male;
        }
        else
        {
            Gender = Gender.Female;
            ChildWasBorn += currentEngine.HandleChildBirth;
        }

        Age = 0;
        Die += currentEngine.HandleDeath;
        IsAlive = true;
    }

    /// <summary>
    /// Конструктор для создания готового человека (используется при генерации начальной популяции)
    /// </summary>
    /// <param name="AgeBirthRate">Данные по распределению возрастов</param>
    /// <param name="Gender">Пол человека</param>
    /// <param name="currentEngine">Движок, к которому привязан человек (для подписок на события)</param>
    public Person(List<AgeData> AgeBirthRate, Gender Gender, Engine currentEngine)
    {
        this.Gender = Gender;
        if(Gender == Gender.Female)
        {
            ChildWasBorn += currentEngine.HandleChildBirth;
        }

        double ageChance = ProbabilityCalculator.GetRandomDoubleValue();
        double probabilitySum = 0;
        Age = -1;
        for (int i = 0; i < AgeBirthRate.Count; i++)
        {
            probabilitySum += AgeBirthRate[i].amount/1000;
            if (ageChance < probabilitySum)
            {
                Age = AgeBirthRate[i].age;
                break;
            }
        }
        if (Age == -1)
        {
            Age = ProbabilityCalculator.GetRandomAge();
        }

        Die += currentEngine.HandleDeath;
        IsAlive = true;
    }

    /// <summary>
    /// Для обработки события прохождения года
    /// </summary>
    /// <param name="DeathProbability">Массив с распределением вероятностей смертей</param>
    /// <param name="Year">Текущий год</param>
    public void HandleYearPassed(List<DeathData> DeathProbability, int Year)
    {
        if (IsAlive)
        {
            Age++;
            if (Age == 100)
            {
                Die.Invoke(this, Year);
            }
            if (Gender == Gender.Male)
            {
                for (int i = 0; i < DeathProbability.Count; i++)
                {
                    if (Age >= DeathProbability[i].primaryAge && Age < DeathProbability[i].finalAge
                    && ProbabilityCalculator.CalcProbablility(DeathProbability[i].femaleDeathProbability))
                    {
                        Die.Invoke(this, Year);
                    }
                }
            }
            if (Gender == Gender.Female)
            {
                for (int i = 0; i < DeathProbability.Count; i++)
                {
                    if (Age >= DeathProbability[i].primaryAge && Age < DeathProbability[i].finalAge
                    && ProbabilityCalculator.CalcProbablility(DeathProbability[i].femaleDeathProbability))
                    {
                        Die.Invoke(this, Year);
                    }
                }
                if (IsAlive && Age >= 18 && Age <= 45 && ProbabilityCalculator.CalcProbablility(ChildBirthChance))
                {
                    ChildWasBorn.Invoke();
                }
            }
        }
    }

    /// <summary>
    /// Вывод информации о человеке (техническая функция)
    /// </summary>
    public void Print()
    {
        Console.WriteLine($"{Gender}, {Age}, {IsAlive}");
    }
}
