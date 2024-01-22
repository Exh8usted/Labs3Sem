namespace McDonalds.FileOperations;

using McDonalds.AdditionalClasses;
using Newtonsoft.Json;

public static class FileManager
{
    public static void CreateLogFile(string filePath)
    {
        File.WriteAllText(filePath, Logger.log);
    }
    public static Config ReadConfig()
    {
        string jsonData = File.ReadAllText("./config.json");
        return JsonConvert.DeserializeObject<Config>(jsonData);
    }
}