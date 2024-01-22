using System.Collections;

namespace LinearAlgebra;

public class MathVector : IMathVector
{
    // Массив для координат (для реализации IEnumerable)
    private double[] coordinates;
    public IEnumerator GetEnumerator()
    {
        foreach (var coordinate in coordinates)
        {
            yield return coordinate;
        }
    }

    // Размерность вектора
    public int Dimensions { get; }

    // Конструктор (на вход размерность и координаты)
    public MathVector(int Dimensions, double[] coordinates)
    {
        if (coordinates.Length == 0)
        {
            throw new Exception("Длина массива с координатами не может быть равна 0");
        }
        if (Dimensions != coordinates.Length)
        {
            throw new Exception($"Длина массива с координатами не соответствует размерности вектора (размерность = {Dimensions}, длина = {coordinates.Length})");
        }
        this.Dimensions = Dimensions;
        this.coordinates = new double[Dimensions];
        for (int i = 0; i < Dimensions; i++)
        {
            this.coordinates[i] = coordinates[i];
        }
    }
    // Второй конструктор (клонирование вектора - для реализации иммутабельности)
    public MathVector(IMathVector vector)
    {
        Dimensions = vector.Dimensions;
        coordinates = new double[Dimensions];
        for (int i = 0; i < Dimensions; i++)
        {
            coordinates[i] = vector[i];
        }
    }

    // Индексатор
    public double this[int index]
    {
        get
        {
            if (index >= 0 && index < Dimensions)
            {
                return coordinates[index];
            }
            else
            {
                throw new IndexOutOfRangeException($"С индексом напутали: размерность = {Dimensions}, индекс = {index}");
            }
        }
        set
        {
            if (index >= 0 && index < Dimensions)
            {
                double[] newCoordinates = new double[Dimensions];
                for (int i = 0; i < Dimensions; i++)
                {
                    newCoordinates[i] = coordinates[i];
                }
                newCoordinates[index] = value;
                coordinates = newCoordinates;
            }
            else
            {
                throw new IndexOutOfRangeException($"С индексом напутали: размерность = {Dimensions}, индекс = {index}");
            }
        }
    }

    // Длина вектора (в виде свойства с геттером)
    public double Length
    {
        get
        {
            double res = 0;
            for (int i = 0; i < Dimensions; i++)
            {
                res += Math.Pow(this[i], 2);
            }

            return Math.Sqrt(res);
        }
    }

    // Перегруженные операторы
    public static IMathVector operator +(MathVector vector, double number) => vector.SumNumber(number);
    public static IMathVector operator +(MathVector vectorA, MathVector vectorB) => vectorA.Sum(vectorB);

    public static IMathVector operator -(MathVector vector, double number) => vector.SumNumber(-number);
    public static IMathVector operator -(MathVector vectorA, MathVector vectorB)
    {
        IMathVector newVector = new MathVector(vectorA);
        for (int i = 0; i < newVector.Dimensions; i++)
        {
            newVector[i] = vectorA[i] - vectorB[i];
        }
        return newVector;
    }

    public static IMathVector operator *(MathVector vector, double number) => vector.MultiplyNumber(number);
    public static IMathVector operator *(MathVector vectorA, MathVector vectorB) => vectorA.Multiply(vectorB);

    public static IMathVector operator /(MathVector vector, double number)
    {
        if (number == 0)
        {
            throw new Exception("Попытка поделить на 0");
        }

        IMathVector newVector = new MathVector(vector);
        for (int i = 0; i < newVector.Dimensions; i++)
        {
            newVector[i] = vector[i] / number;
        }
        return newVector;
    }
    public static IMathVector operator /(MathVector vectorA, MathVector vectorB)
    {
        IMathVector newVector = new MathVector(vectorA);
        for (int i = 0; i < newVector.Dimensions; i++)
        {
            if (vectorB[i] == 0)
            {
                throw new Exception($"В координатах второго вектора попался 0 (индекс = {i}), поэтому деление невозможно");
            }
            newVector[i] = vectorA[i] / vectorB[i];
        }
        return newVector;
    }

    public static double operator %(MathVector vectorA, MathVector vectorB) => vectorA.ScalarMultiply(vectorB);

    // Реализация методов
    public IMathVector SumNumber(double number)
    {
        IMathVector newVector = new MathVector(this);
        for (int i = 0; i < Dimensions; i++)
        {
            newVector[i] += number;
        }

        return newVector;
    }

    public IMathVector MultiplyNumber(double number)
    {
        IMathVector newVector = new MathVector(this);
        for (int i = 0; i < Dimensions; i++)
        {
            newVector[i] *= number;
        }

        return newVector;
    }

    public IMathVector Sum(IMathVector vector)
    {
        if (Dimensions != vector.Dimensions)
        {
            throw new ArgumentException($"Векторы должны быть с равными размерностями ({Dimensions} != {vector.Dimensions})");
        }

        IMathVector newVector = new MathVector(this);
        for (int i = 0; i < Dimensions; i++)
        {
            newVector[i] += vector[i];
        }

        return newVector;
    }

    public IMathVector Multiply(IMathVector vector)
    {
        if (Dimensions != vector.Dimensions)
        {
            throw new ArgumentException($"Векторы должны быть с равными размерностями ({Dimensions} != {vector.Dimensions})");
        }

        IMathVector newVector = new MathVector(this);
        for (int i = 0; i < Dimensions; i++)
        {
            newVector[i] *= vector[i];
        }

        return newVector;
    }

    public double ScalarMultiply(IMathVector vector)
    {
        if (Dimensions != vector.Dimensions)
        {
            throw new ArgumentException($"Векторы должны быть с равными размерностями ({Dimensions} != {vector.Dimensions})");
        }

        IMathVector newVector = new MathVector(this);
        double res = 0;
        for (int i = 0; i < Dimensions; i++)
        {
            res += newVector[i] * vector[i];
        }

        return res;
    }

    public double CalcDistance(IMathVector vector)
    {
        if (Dimensions != vector.Dimensions)
        {
            throw new ArgumentException($"Векторы должны быть с равными размерностями ({Dimensions} != {vector.Dimensions})");
        }

        IMathVector newVector = new MathVector(this);
        double res = 0;
        for (int i = 0; i < Dimensions; i++)
        {
            res += Math.Pow(newVector[i] - vector[i], 2);
        }

        return Math.Sqrt(res);
    }
}
