using LinearAlgebra;

namespace VectorTest;

public static class InputFunctions
{
    public static int DimensionsInput()
    {
        Console.WriteLine("Введите размерность вектора (размерность - int, > 0): ");
        double Dimensions = double.Parse(Console.ReadLine());
        if (Dimensions <= 0)
        {
            throw new CustomExceptions($"Размерность не может быть меньше 0 (размерность = {Dimensions})");
        }
        else if (Dimensions != Convert.ToInt32(Dimensions))
        {
            throw new CustomExceptions($"Размерность не может быть дробным числом (размерность = {Dimensions})");
        }
        return Convert.ToInt32(Dimensions);
    }
    public static MathVector VectorInput(int Dimensions)
    {
        double[] coordinates = new double[Dimensions];
        Console.WriteLine("\nВведите координаты вектора в соответствии с размерностью (координата - double), разделитель - перенос строки: ");
        for (int i = 0; i < Dimensions; i++)
        {
            coordinates[i] = double.Parse(Console.ReadLine());
        }
        return new MathVector(Dimensions, coordinates);
    }
}
