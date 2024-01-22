using System.Globalization;

namespace Demographic;

/// <summary>
/// Класс исключений для различных обработок в процессе работы движка
/// </summary>
public class EngineException : Exception
{
    public EngineException(string message) : base(message) { }
}
