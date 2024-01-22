using VectorTest;

class VectorDemo
{
    static void Main(string[] args)
    {
        TestBuisnessLogic.BuisnessState CurrentState = TestBuisnessLogic.BuisnessState.Input;
        while (CurrentState != TestBuisnessLogic.BuisnessState.Exit)
        {
            TestBuisnessLogic.BuisnessController(ref CurrentState);
        }
    }
}