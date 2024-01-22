using LinearAlgebra;

namespace VectorTest;

public static class TestBuisnessLogic
{
    static MathVector vectorA;
    static MathVector vectorB;
    static MathVector vectorC;
    public enum BuisnessState
    {
        Input,
        Test,
        Exit
    }
    public static void BuisnessController(ref BuisnessState CurrentState)
    {
        switch (CurrentState)
        {
            case BuisnessState.Input:
                try
                {
                    Console.WriteLine("\nВведите размерность и координаты вектора А:");
                    vectorA = InputFunctions.VectorInput(InputFunctions.DimensionsInput());

                    Console.WriteLine("\nВведите размерность и координаты вектора B (размерность должна совпадать с вектором А):");
                    vectorB = InputFunctions.VectorInput(InputFunctions.DimensionsInput());
                    if (vectorA.Dimensions != vectorB.Dimensions)
                    {
                        throw new CustomExceptions($"Размерности НЕ сопали ({vectorA.Dimensions} != {vectorB.Dimensions})");
                    }

                    Console.WriteLine("\nВведите размерность и координаты вектора С (размерность должна не совпадать с предыдущими):");
                    vectorC = InputFunctions.VectorInput(InputFunctions.DimensionsInput());
                    if (vectorA.Dimensions == vectorC.Dimensions)
                    {
                        throw new CustomExceptions($"Размерности совпали ({vectorA.Dimensions} = {vectorC.Dimensions})");
                    }

                    CurrentState = BuisnessState.Test;
                }
                catch (CustomExceptions ex)
                {
                    Console.WriteLine($"{ex.CustomMessage}\n");
                }
                catch (Exception)
                {
                    Console.WriteLine("Ошибка с форматом ввода (пустой ввод или не число)\n");
                }
                break;

            case BuisnessState.Test:
                Console.WriteLine("\nНесколько тестов с векторами:");
                Tests.ConstructorTest();
                Tests.DimensionsTest(vectorA);
                Tests.IndexatorTest(vectorA);
                Tests.LengthTest(vectorA);
                Tests.SumNumberTest(vectorA);
                Tests.MultiplyNumberTest(vectorA);
                Tests.SumTest(vectorA, vectorB, vectorC);
                Tests.MultiplyTest(vectorA, vectorB, vectorC);
                Tests.ScalarMultiplyTest(vectorA, vectorB, vectorC);
                Tests.CalcDistanceTest(vectorA, vectorB, vectorC);
                Tests.OperatorsTest(vectorA, vectorB);

                CurrentState = BuisnessState.Exit;
                break;

            case BuisnessState.Exit:
                Console.WriteLine("\nЗавершение программы...\n");
                break;
        }
    }
    
}