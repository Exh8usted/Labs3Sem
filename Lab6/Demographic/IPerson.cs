namespace Demographic;

public enum Gender
{
    Male,
    Female
};

public delegate void ChildBirth();
public delegate void Death(Person DiedPerson, int Year);
public interface IPerson
{
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public bool IsAlive { get; set; }
    public event ChildBirth ChildWasBorn;
    
}
