using NUnit.Framework;
using LinearAlgebra;
using System;
using System.Security.Cryptography.X509Certificates;

namespace UnitTesting;

[TestFixture]
public class Tests
{
    private MathVector _vectorA;
    private MathVector _vectorB;
    private MathVector _vectorC;
    private MathVector _vectorD;
    private MathVector _vectorE;

    [SetUp]
    public void Setup()
    {
        _vectorA = new MathVector(3, new double[] { 1, 3, 4 });
        _vectorB = new MathVector(3, new double[] { 2, 5, 7 });
        _vectorC = new MathVector(2, new double[] { 3, 4 });
        _vectorD = new MathVector(2, new double[] { 6, 0 });
        _vectorE = new MathVector(2, new double[] { 3, 4 });
    }

    [Test]
    public void CreateVectorTest()
    {
        MathVector correctVector1 = new MathVector(1, new double[] { 1 });
        MathVector correctVector2 = new MathVector(5, new double[] { 1, 2, 3.2, 4.4, 5 });
        Assert.Pass();
    }

    [Test]
    public void CloneVectorTest()
    {
        MathVector correctVector1 = new MathVector(5, new double[] { 1, 2.1, 3, 4.5, 5 });
        MathVector clonedVector2 = new MathVector(correctVector1);
        Assert.Pass();
    }

    [Test]
    public void CreateVectorWithInvalidDimensionsTest()
    {
        Assert.Throws<Exception>(() => new MathVector(0, new double[] { 1, 2 }));
        Assert.Throws<Exception>(() => new MathVector(-1, new double[] { 1, 2 }));
        Assert.Pass();
    }

    [Test]
    public void CreateVectorWithInvalidCoordinatesAmountTest()
    {
        Assert.Throws<Exception>(() => new MathVector(1, new double[] { 1, 2 }));
        Assert.Throws<Exception>(() => new MathVector(1, new double[] { }));
        Assert.Pass();
    }

    [Test]
    public void GetVectorDimensionsTest()
    {
        int dimensions = _vectorA.Dimensions;
        Assert.AreEqual(3, dimensions);
        Assert.Pass();
    }

    [Test]
    public void CoordinatesGetterPositiveTest()
    {
        double coordinate1 = _vectorA[0];
        Assert.AreEqual(1, coordinate1);
        Assert.Pass();
    }

    [Test]
    public void CoordinatesGetterNegativeTest()
    {
        double coordinate;
        Assert.Throws<IndexOutOfRangeException>(() => coordinate = _vectorA[-1]);
        Assert.Pass();
    }

    [Test]
    public void CoordinatesSetterPositiveTest()
    {
        _vectorA[0]++;
        Assert.AreEqual(2, _vectorA[0]);
        Assert.Pass();
    }

    [Test]
    public void GetVectorLength()
    {
        Assert.AreEqual(5, _vectorC.Length);
        Assert.Pass();
    }

    [Test]
    public void SumNumberTest()
    {
        IMathVector resultVector = _vectorA.SumNumber(5);
        Assert.AreEqual(6, resultVector[0]);
        Assert.AreEqual(8, resultVector[1]);
        Assert.AreEqual(9, resultVector[2]);
        Assert.Pass();
    }

    [Test]
    public void SumVectorPositiveTest()
    {
        IMathVector resultVector = _vectorA.Sum(_vectorB);
        Assert.AreEqual(3, resultVector[0]);
        Assert.AreEqual(8, resultVector[1]);
        Assert.AreEqual(11, resultVector[2]);
        Assert.Pass();
    }

    [Test]
    public void SumVectorNegativeTest()
    {
        Assert.Throws<ArgumentException>(() => _vectorA.Sum(_vectorC));
        Assert.Pass();
    }

    [Test]
    public void MultiplyNumberTest()
    {
        IMathVector resultVector = _vectorA.MultiplyNumber(2);
        Assert.AreEqual(2, resultVector[0]);
        Assert.AreEqual(6, resultVector[1]);
        Assert.AreEqual(8, resultVector[2]);
        Assert.Pass();
    }

    [Test]
    public void MultiplyVectorPositiveTest()
    {
        IMathVector resultVector = _vectorA.Multiply(_vectorB);
        Assert.AreEqual(2, resultVector[0]);
        Assert.AreEqual(15, resultVector[1]);
        Assert.AreEqual(28, resultVector[2]);
        Assert.Pass();
    }
    
    [Test]
    public void MultiplyVectorNegativeTest()
    {
        Assert.Throws<ArgumentException>(() => _vectorA.Multiply(_vectorC));
        Assert.Pass();
    }

    [Test]
    public void ScalarMultiplyPositiveTest()
    {
        double result = _vectorA.ScalarMultiply(_vectorB);
        Assert.AreEqual(45, result);
        Assert.Pass();
    }
    
    [Test]
    public void ScalarMultiplyNegativeTest()
    {
        Assert.Throws<ArgumentException>(() => _vectorA.ScalarMultiply(_vectorC));
        Assert.Pass();
    }

    [Test]
    public void CalcDistancePositiveTest()
    {
        double result = _vectorC.CalcDistance(_vectorD);
        Assert.AreEqual(result, 5);
        Assert.Pass();
    }

    [Test]
    public void CalcDistanceNegativeTest()
    {
        Assert.Throws<ArgumentException>(() => _vectorC.CalcDistance(_vectorA));
        Assert.Pass();
    }

    [Test]
    public void SumNumberOperatorTest()
    {
        IMathVector resultVector = _vectorA + 5;
        Assert.AreEqual(6, resultVector[0]);
        Assert.AreEqual(8, resultVector[1]);
        Assert.AreEqual(9, resultVector[2]);
        Assert.Pass();
    }

    [Test]
    public void SumVectorOperatorTest()
    {
        IMathVector resultVector = _vectorA + _vectorB;
        Assert.AreEqual(3, resultVector[0]);
        Assert.AreEqual(8, resultVector[1]);
        Assert.AreEqual(11, resultVector[2]);
        Assert.Pass();
    }

    [Test]
    public void MultiplyNumberOperatorTest()
    {
        IMathVector resultVector = _vectorA * 2;
        Assert.AreEqual(2, resultVector[0]);
        Assert.AreEqual(6, resultVector[1]);
        Assert.AreEqual(8, resultVector[2]);
        Assert.Pass();
    }

    [Test]
    public void MultiplyVectorOperatorTest()
    {
        IMathVector resultVector = _vectorA * _vectorB;
        Assert.AreEqual(2, resultVector[0]);
        Assert.AreEqual(15, resultVector[1]);
        Assert.AreEqual(28, resultVector[2]);
        Assert.Pass();
    }

    [Test]
    public void SubtractionNumberOperatorTest()
    {
        IMathVector resultVector = _vectorA - 5;
        Assert.AreEqual(-4, resultVector[0]);
        Assert.AreEqual(-2, resultVector[1]);
        Assert.AreEqual(-1, resultVector[2]);
        Assert.Pass();
    }

    [Test]
    public void SubtractionVectorOperatorPositiveTest()
    {
        IMathVector resultVector = _vectorA - _vectorB;
        Assert.AreEqual(-1, resultVector[0]);
        Assert.AreEqual(-2, resultVector[1]);
        Assert.AreEqual(-3, resultVector[2]);
        Assert.Pass();
    }

    [Test]
    public void SubtractionVectorOperatorNegativeTest()
    {
        IMathVector resultVector;
        Assert.Throws<IndexOutOfRangeException>(() => resultVector = _vectorA - _vectorC);
        Assert.Pass();
    }

    [Test]
    public void DivisionNumberOperatorTest()
    {
        IMathVector resultVector = _vectorC / 2;
        Assert.AreEqual(1.5, resultVector[0]);
        Assert.AreEqual(2, resultVector[1]);
        Assert.Pass();
    }

    [Test]
    public void DivisionVectorOperatorPositiveTest()
    {
        IMathVector resultVector = _vectorC / _vectorE;
        Assert.AreEqual(1, resultVector[0]);
        Assert.AreEqual(1, resultVector[1]);
        Assert.Pass();
    }
    [Test]
    public void DivisionVectorOperatorNegativeTest()
    {
        IMathVector resultVector;
        Assert.Throws<IndexOutOfRangeException>(() => resultVector = _vectorA / _vectorC);
        Assert.Pass();
    }

    public void DivisionByZeroTest()
    {
        IMathVector resultVector;
        Assert.Throws<DivideByZeroException>(() => resultVector = _vectorA / 0);
        Assert.Pass();
    }

    [Test]
    public void ScalarMultiplyOperatorTest()
    {
        double result = _vectorA % _vectorB;
        Assert.AreEqual(45, result);
        Assert.Pass();
    }
}