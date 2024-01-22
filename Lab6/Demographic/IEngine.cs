namespace Demographic;
using Demographic.FileOperations;

public delegate void YearTick(List<DeathData> DeathProbability, int Year);
public interface IEngine
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public int InitialPeopleAmount { get; set; }
    public List<Person> People { get; set; }
    public event YearTick? YearPassed;
    public void Simulate();
}
