

namespace Demographic.FileOperations;

public class FileManager : IFileManager
{
    public string AgeFilePath { get; set; }
    public string DeathRateFilePath { get; set; }
    public string AgeResultFilePath { get; set; }
    public string PopulationResultFile { get; set; }
    public static string AgeFileHeader = "Возраст, Количество_на_1000";
    public static string DeathRateFileHeader = "Начальный_возраст, Конечный_возраст, Вероятность_смерти_муж, Вероятность_смерти_жен";

    public FileManager(string AgeFilePath, string DeathRateFilePath, string AgeResultFilePath, string PopulationResultFile)
    {
        if (File.Exists(AgeFilePath) && File.Exists(DeathRateFilePath))
        {
            this.AgeFilePath = AgeFilePath;
            this.DeathRateFilePath = DeathRateFilePath;
            this.AgeResultFilePath = AgeResultFilePath;
            this.PopulationResultFile = PopulationResultFile;
        }
        else
        {
            throw new FileNotFoundException("Нет такого файла(ов)!");
        }
    }
    public void CreateAgesResultFile(List<int> MaleAges, List<int> FemaleAges)
    {
        List<string> ageDataInString = new List<string>(3)
        {
            "Gender,0-18,19-45,45-60,60-100",
            $"Male,{MaleAges[0]},{MaleAges[1]},{MaleAges[2]},{MaleAges[3]}",
            $"Female,{FemaleAges[0]},{FemaleAges[1]},{FemaleAges[2]},{FemaleAges[3]}"
        };

        File.WriteAllLines(AgeResultFilePath, ageDataInString);
    }
    public void CreatePopulationResultFile(List<string> PopulationInString)
    {
        File.WriteAllLines(PopulationResultFile, PopulationInString);
    }
    public List<AgeData> GetAgeData()
    {
        List<AgeData> data = new List<AgeData>();
        using (var reader = new StreamReader(AgeFilePath))
        {
            try
            {
                string header = reader.ReadLine();
                if (header != AgeFileHeader)
                {
                    throw new FileManagerExceptions("Некорректный хедер в файле с возрастами");
                }
            }
            catch(FileManagerExceptions ex)
            {
                throw ex;
            }
            catch
            {
                throw new FileManagerExceptions("В процессе считывания хедера из файла с возрастами возникла ошибка");
            }
            while (!reader.EndOfStream)
            {
                string currentLine = reader.ReadLine();
                try
                {
                    var valuesInLine = currentLine.Replace(" ", "").Replace("\n","").Replace("\r", "").Split(',');

                    if (int.TryParse(valuesInLine[0], out int age) & double.TryParse(valuesInLine[1].Replace(".",","), out double amount))
                    {
                        data.Add(new AgeData(age, amount));
                    }
                    else
                    {
                        throw new FileManagerExceptions("В файле с возрастами встретилось некорректное значение");
                    }
                }
                catch(FileManagerExceptions ex)
                {
                    throw new FileManagerExceptions("Ошибка в процессе получения данных из файла с возрастами");
                }
                
            }
        }
        return data;
    }

    public List<DeathData> GetDeathData()
    {
        List<DeathData> data = new List<DeathData>();
        using (var reader = new StreamReader(DeathRateFilePath))
        {
            try
            {
                string header = reader.ReadLine();
                if (header != DeathRateFileHeader)
                {
                    throw new FileManagerExceptions("Некорректный хедер в файле со статистикой смертей");
                }
            }
            catch(FileManagerExceptions ex)
            {
                throw ex;
            }
            catch
            {
                throw new FileManagerExceptions("В процессе считывания хедера из файла со статистикой смертей возникла ошибка");
            }
            while (!reader.EndOfStream)
            {
                string currentLine = reader.ReadLine();
                try
                {
                    var valuesInLine = currentLine.Replace(" ", "").Replace("\n","").Replace("\r", "").Split(',');

                    if (int.TryParse(valuesInLine[0], out int primaryAge) & int.TryParse(valuesInLine[1].Replace(".",","), out int finalAge)
                    & double.TryParse(valuesInLine[2].Replace(".",","), out double maleDeathProbability)
                    & double.TryParse(valuesInLine[3].Replace(".",","), out double femaleDeathProbability))
                    {
                        DeathData tmp = new DeathData(primaryAge, finalAge, maleDeathProbability, femaleDeathProbability);
                        data.Add(tmp);
                    }
                    else
                    {
                        throw new FileManagerExceptions("В файле со статистикой смертей встретилось некорректное значение");
                    }
                }
                catch(FileManagerExceptions ex)
                {
                    throw new FileManagerExceptions("Ошибка в процессе получения данных из файла со статистикой смертей");
                }
                
            }
        }
        return data;
    }

}
