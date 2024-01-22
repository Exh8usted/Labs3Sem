namespace McDonalds.Exec;

using McDonalds;
using McDonalds.AdditionalClasses;
using McDonalds.FileOperations;

class Program
{
    static void Main(string[] args)
    {
        Config config = FileManager.ReadConfig();
        Console.WriteLine(config.returnOrderProb);
        Start(config).Wait();
        Logger.PrintOrderStats();
        FileManager.CreateLogFile(config.logFilePath);
        
    }
    public static async Task Start(Config config)
    {
        Engine engine = new Engine(config);

        await engine.Run(config.workSeconds);
    }
}