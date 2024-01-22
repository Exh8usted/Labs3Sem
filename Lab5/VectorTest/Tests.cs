using LinearAlgebra;

namespace VectorTest;

public static class Tests
{
    // Технические методы (для корректного отображения в консоли)
    static void ShowCoordinates(IMathVector vector)
    {
        foreach (double coord in vector)
        {
            Console.Write($"{coord} ");
        }
        Console.WriteLine();
    }

    // Тесты функционала
    public static void ConstructorTest()
    {
        Console.WriteLine("\n=========================\n\nТест 1: проверка конструктора класса.\n");
        
        IMathVector correctVector = new MathVector(3, new double[] {1, 2, 3});
        Console.WriteLine($"1.1. Правильная инициализация вектора: {correctVector.Dimensions}");

        try
        {
            Console.Write("1.2. Неправильная инициализация вектора (размерность != длине): ");
            IMathVector incorrectVector = new MathVector(3, new double[] {1, 2});
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        try
        {
            Console.Write("1.3. Неправильная инициализация вектора (длина массива с координатами = 0): ");
            IMathVector incorrectVector = new MathVector(0, new double[] {});
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void DimensionsTest(IMathVector vectorA)
    {
        Console.WriteLine("\n=========================\n\nТест 2: проверка работы геттера для размерности.\n");
        
        Console.WriteLine($"2. Размерность вектора: {vectorA.Dimensions}");
    }
    public static void IndexatorTest(IMathVector vectorA)
    {
        Console.WriteLine("\n=========================\n\nТест 3: проверка работы индексатора.\n");

        Console.WriteLine($"3.1. Правильное извлечение элемента: {vectorA[0]}");

        vectorA[0] += 5;
        Console.WriteLine($"3.2. Правильное изменение элемента (прибавили 5 к значению из 3.1.): {vectorA[0]}");
        // Возращаем значение в на прежнее
        vectorA[0] -= 5;

        try
        {
            Console.Write("3.3. Неправильное извлечение элемента: ");
            double tmp = vectorA[-1];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        try
        {
            Console.Write("3.4. Неправильное изменение элемента: ");
            vectorA[-1] += 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void LengthTest(IMathVector vectorA)
    {
        Console.WriteLine("\n=========================\n\nТест 4: проверка рассчета длины.\n");

        Console.WriteLine($"4. Длина вектора: {vectorA.Length}");
    }
    public static void SumNumberTest(IMathVector vectorA)
    {
        Console.WriteLine("\n=========================\n\nТест 5: проверка функции покомпонентного сложения с числом (число = 5).\n");

        vectorA = vectorA.SumNumber(5);
        Console.Write($"5. Сложение вектора с числом: ");
        ShowCoordinates(vectorA);
    }
    public static void MultiplyNumberTest(IMathVector vectorA)
    {
        Console.WriteLine("\n=========================\n\nТест 6: проверка функции покомпонентного умножения на число (число = 2).\n");

        vectorA = vectorA.MultiplyNumber(2);
        Console.Write($"6. Умножение вектора на число: ");
        ShowCoordinates(vectorA);
    }
    public static void SumTest(IMathVector vectorA, IMathVector vectorB, IMathVector vectorC)
    {
        Console.WriteLine("\n=========================\n\nТест 7: проверка функции покомпонентного сложения двух векторов.\n");

        vectorA = vectorA.Sum(vectorB);
        Console.Write($"7.1. Покомпонентное сложение двух векторов (позитивный): ");
        ShowCoordinates(vectorA);
        try
        {
            Console.Write($"7.2. Покомпонентное сложение двух векторов (негативный): ");
            vectorA = vectorA.Sum(vectorC);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void MultiplyTest(IMathVector vectorA, IMathVector vectorB, IMathVector vectorC)
    {
        Console.WriteLine("\n=========================\n\nТест 8: проверка функции покомпонентного умножения двух векторов.\n");

        vectorA = vectorA.Multiply(vectorB);
        Console.Write($"8.1. Покомпонентное умножение двух векторов (позитивный): ");
        ShowCoordinates(vectorA);
        try
        {
            Console.Write($"8.2. Покомпонентное умножение двух векторов (негативный): ");
            vectorA = vectorA.Multiply(vectorC);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void ScalarMultiplyTest(IMathVector vectorA, IMathVector vectorB, IMathVector vectorC)
    {
        Console.WriteLine("\n=========================\n\nТест 9: проверка функции скалярного умножения двух векторов.\n");

        Console.WriteLine($"9.1. Покомпонентное скалярное умножение двух векторов (позитивный): {vectorA.ScalarMultiply(vectorB)}");
        try
        {
            Console.Write($"9.2. Покомпонентное скалярное умножение двух векторов (негативный): ");
            vectorA.ScalarMultiply(vectorC);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void CalcDistanceTest(IMathVector vectorA, IMathVector vectorB, IMathVector vectorC)
    {
        Console.WriteLine("\n=========================\n\nТест 10: проверка функции нахождения Евклидового расстояния между двумя векторами.\n");

        Console.WriteLine($"10.1. Евклидово расстояние между двумя векторами (позитивный): {vectorA.CalcDistance(vectorB)}");
        try
        {
            Console.Write($"10.2. Евклидово расстояние между двумя векторами (негативный): ");
            vectorA.CalcDistance(vectorC);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void OperatorsTest(MathVector vectorA, MathVector vectorB)
    {
        Console.WriteLine("\n=========================\n\nТест 11: проверка перегруженных операторов.\n");

        IMathVector sumNumberVector = vectorA + 5;
        Console.Write($"11.1. Сложение вектора с числом (+): ");
        ShowCoordinates(sumNumberVector);
        
        IMathVector sumVector = vectorA + vectorB;
        Console.Write($"11.2. Покомпонентное сложение двух векторов (+): ");
        ShowCoordinates(sumVector);

        IMathVector multiplyNumberVector = vectorA * 2;
        Console.Write($"11.3. Умножение вектора на число (*): ");
        ShowCoordinates(multiplyNumberVector);

        IMathVector multiplyVector = vectorA * vectorB;
        Console.Write($"11.4. Покомпонентное умножение двух векторов (*): ");
        ShowCoordinates(multiplyVector);

        IMathVector subtrNumberVector = vectorA - 5;
        Console.Write($"11.5. Вычитание числа из вектора (-): ");
        ShowCoordinates(subtrNumberVector);
        
        IMathVector subtrVector = vectorA - vectorB;
        Console.Write($"11.6. Покомпонентное вычитание двух векторов (-): ");
        ShowCoordinates(subtrVector);

        IMathVector devideNumberVector = vectorA / 2;
        Console.Write($"11.7. Деление вектора на число (/): ");
        ShowCoordinates(devideNumberVector);

        IMathVector devideVector = vectorA / vectorB;
        Console.Write($"11.8. Покомпонентное деление двух векторов (/): ");
        ShowCoordinates(devideVector);

        try
        {
            Console.Write($"11.9. Покомпонентное деление вектора на число (/) (негативный): ");
            IMathVector devideZeroNumberVector = vectorA / 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        try
        {
            Console.Write($"11.10. Деление двух векторов (/) (негативный): ");
            vectorB[0] = 0;
            IMathVector devideZeroVector = vectorA / vectorB;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            vectorB[0] = 2;
        }

        Console.WriteLine($"11.11. Покомпонентное скалярное умножение двух векторов (%): {vectorA % vectorB}");
    }
}
