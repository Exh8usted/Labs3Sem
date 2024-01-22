namespace McDonalds.AdditionalClasses;

public static class IDGenerator
{
    private static int _nextID = 0;
    public static int GetNextID()
    {
        return _nextID++;
    }
}
