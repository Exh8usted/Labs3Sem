using Demographic.FileOperations;

namespace Demographic;

public class Engine : IEngine
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public int InitialPeopleAmount { get; set; }
    public int MalePopulation
    {
        get
        {
            int malePopulation = 0;
            foreach (Person m in People)
            {
                if (m.IsAlive && m.Gender == Gender.Male)
                {
                    malePopulation++;
                }
            }
            return malePopulation;
        }
    }
    public int FemalePopulation
    {
        get
        {
            int femalePopulation = 0;
            foreach (Person m in People)
            {
                if (m.IsAlive && m.Gender == Gender.Female)
                {
                    femalePopulation++;
                }
            }
            return femalePopulation;
        }
    }
    public List<Person> People { get; set; }
    public List<string> Population { get; set; }
    public List<int> MaleAges { get; set; }
    public List<int> FemaleAges { get; set; }
    public List<AgeData> Ages { get; set; }
    public List<DeathData> Deaths { get; set; }
    public event YearTick? YearPassed;

    /// <summary>
    /// Базовый конструктор движка
    /// </summary>
    /// <param name="StartYear">Год начала симуляции</param>
    /// <param name="EndYear">Год конца симуляции</param>
    /// <param name="AgeData">Данные по возрасту популяции</param>
    /// <param name="DeathData">Данные по вероятности смерти в зависимости от возраста и пола</param>
    /// <param name="InitialPeopleAmount">Размер начальной популяции</param>
    public Engine(List<AgeData> AgeData, List<DeathData> DeathData, int StartYear = 1970, int EndYear = 2021, int InitialPeopleAmount = 130)
    {
        this.StartYear = StartYear;
        this.EndYear = EndYear;
        Ages = AgeData;
        Deaths = DeathData;
        this.InitialPeopleAmount = InitialPeopleAmount * 1000;
        Population = new List<string>()
        {
            "Male,Female,All"
        };
        MaleAges = new List<int> {0, 0, 0, 0};
        FemaleAges = new List<int> {0, 0, 0, 0};
        GeneratePeople();
    }

    /// <summary>
    /// Метод для генерации начальной популяции
    /// </summary>
    public void GeneratePeople()
    {
        People = new List<Person>(InitialPeopleAmount);
        
        for (int i = 0; i < Convert.ToInt32(InitialPeopleAmount/2); i++)
        {
            Person newMale = new Person(Ages, Gender.Male, this);
            YearPassed += newMale.HandleYearPassed;
            People.Add(newMale);

            Person newFemale = new Person(Ages, Gender.Female, this);
            YearPassed += newFemale.HandleYearPassed;
            People.Add(newFemale);
        }
    }

    /// <summary>
    /// Основная функция симуляции при заданных значениях
    /// </summary>
    public void Simulate()
    {
        for (int currentYear = StartYear; currentYear < EndYear; currentYear++)
        {
            YearPassed.Invoke(Deaths, currentYear);
            Population.Add($"{MalePopulation}, {FemalePopulation}, {FemalePopulation + MalePopulation}");
        }
        CalculateDataForAgeFile();
    }

    /// <summary>
    /// Для обработки события рождения нового ребенка
    /// </summary>
    public void HandleChildBirth()
    {
        Person newPerson = new Person(this);
        YearPassed += newPerson.HandleYearPassed;
        People.Add(newPerson);
    }

    /// <summary>
    /// Для обработки события смерти человека
    /// </summary>
    /// <param name="DiedPerson">Умерший человек</param>
    /// <param name="Year">Год смерти (в промежутке заданных лет)</param>
    public void HandleDeath(Person DiedPerson, int Year)
    {
        DiedPerson.IsAlive = false;
        DiedPerson.DeathYear = Year;
        YearPassed -= DiedPerson.HandleYearPassed;
        DiedPerson.ChildWasBorn -= HandleChildBirth;
        DiedPerson.Die -= HandleDeath;
    }

    /// <summary>
    /// Функция для формирования данных о возрастном распределении населения
    /// </summary>
    public void CalculateDataForAgeFile()
    {
        foreach (Person person in People)
        {
            if (person.Gender == Gender.Male)
            {
                if (person.Age <= 18)
                {
                    MaleAges[0]++;
                }
                else if (person.Age <= 45)
                {
                    MaleAges[1]++;
                }
                else if (person.Age <= 60)
                {
                    MaleAges[2]++;
                }
                else if (person.Age <= 100)
                {
                    MaleAges[3]++;
                }
            }
            else
            {
                if (person.Age <= 18)
                {
                    FemaleAges[0]++;
                }
                else if (person.Age <= 45)
                {
                    FemaleAges[1]++;
                }
                else if (person.Age <= 60)
                {
                    FemaleAges[2]++;
                }
                else if (person.Age <= 100)
                {
                    FemaleAges[3]++;
                }
            }
        }
    }

    /// <summary>
    /// Вывод содержимого в массиве людей (техническая функция, при основных расчетах не используется)
    /// </summary>
    public void Print()
    {
        int i = 0;
        foreach(Person p in People)
        {
            i++;
            Console.Write($"{i}: ");
            p.Print();
        }
    }
}
