namespace Demographic.Exec;

using Demographic.FileOperations;

class Program
{
    static void Main(string[] args)
    {
        FileManager fileManager = new FileManager(args[0], args[1], args[2], args[3]);
        List<AgeData> data1 = fileManager.GetAgeData();
        List<DeathData> data2 = fileManager.GetDeathData();

        Engine engine = new Engine(data1, data2, int.Parse(args[4]), int.Parse(args[5]), int.Parse(args[6]));
        engine.Simulate();
        fileManager.CreateAgesResultFile(engine.MaleAges, engine.FemaleAges);
        fileManager.CreatePopulationResultFile(engine.Population);
        // try
        // {
        //     FileManager fileManager = new FileManager(args[0], args[1], args[2], args[3]);
        //     List<AgeData> data1 = fileManager.GetAgeData();
        //     List<DeathData> data2 = fileManager.GetDeathData();

        //     Engine engine = new Engine(data1, data2, int.Parse(args[4]), int.Parse(args[5]), int.Parse(args[6]));
        //     engine.Simulate();
        //     fileManager.CreateAgesResultFile(engine.MaleAges, engine.FemaleAges);
        //     fileManager.CreatePopulationResultFile(engine.Population);
        // }
        // catch(FileManagerExceptions ex)
        // {
        //     Console.WriteLine(ex.Message);
        // }
        // catch
        // {
        //     Console.WriteLine("Проблема с файлами");
        // }
        
    }
}