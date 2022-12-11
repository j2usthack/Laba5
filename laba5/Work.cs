using System.Text.RegularExpressions;

namespace AllStaff.laba5;
/// <summary>
/// Абстрактный класс
/// </summary>
public abstract class Work
{
    /// <summary>
    /// Абстрактное свойство всех строк
    /// </summary>
    protected abstract string[] Lines { get; }
    /// <summary>
    /// Абстрактное свойство Regex для переопределения в производных классах
    /// </summary>
    protected abstract Regex Regex { get; }
    /// <summary>
    /// Виртуальный метод для вывода результата. Можно переопределить
    /// </summary>
    public virtual void PrintFromFile()
    {
        foreach (string line in Lines)
        {
            if (Regex.IsMatch(line))
            {
                Console.WriteLine($"Под выражение подходит: {line}");
            }
            else
            {
                Console.WriteLine($"Под выражение не подходит: {line}");
            }
        }
    }
    public virtual void PrintFromConsole(string text)
    {
        if (Regex.IsMatch(text))
        {
            Console.WriteLine($"Под выражение подходит: {text}");
        }
        else
        {
            Console.WriteLine($"Под выражение не подходит: {text}");
        }
    }
}
public class Work1 : Work
{
    protected override string[] Lines => File.ReadAllLines($"!{nameof(Work1)}.txt");
    protected override Regex Regex => new(@"(a+\s?)+");
}
public class Work2 : Work
{
    protected override string[] Lines => File.ReadAllLines($"!{nameof(Work2)}.txt");
    protected override Regex Regex => new(@"[а-яА-Яa-zA-Z0-9]{5,}");
}
public class Work3 : Work
{
    protected override string[] Lines => File.ReadAllLines($"!{nameof(Work3)}.txt");
    protected override Regex Regex => new(@".+@.+");
}
//public class Work4a : Work
//{
//    protected override string[] Lines => File.ReadAllLines($"!{nameof(Work4a)}.txt");
//    protected override Regex Regex => new(@"^(ул\D\s)?(?<street>\S+)\s(д\D\s)?(?<house>\d{1,3}(\/\d{1,3})?)$");
//    public override void PrintFromFile()
//    {
//        foreach (string line in Lines)
//        {
//            Match match = Regex.Match(line);

//            if (match.Success)
//            {
//                Console.WriteLine($"{match.Groups["street"]} {match.Groups["house"]}");
//            }
//            else
//            {
//                Console.WriteLine("Не найдено");
//            }
//        }
//    }
//}
public class Work4a : Work
{
    protected override string[] Lines => File.ReadAllLines($"!{nameof(Work4a)}.txt");
    protected override Regex Regex => new(@"^(ул\D\s)?(?<street>\S+)\s(д\D\s)?(?<house>\d{1,3}(\/\d{1,3})?)$");
    public override void PrintFromFile()
    {
        foreach (string line in Lines)
        {
            Match match = Regex.Match(line);
            if (match.Success)
            {
                Console.WriteLine($"{match.Groups["street"]} {match.Groups["house"]}");
            }
            else
            {
                Console.WriteLine("Не найдено");
            }
        }
    }
    public override void PrintFromConsole(string text)
    {
        Match match = Regex.Match(text);
        if (match.Success)
        {
            Console.WriteLine($"{match.Groups["street"]} {match.Groups["house"]}");
        }
        else
        {
            Console.WriteLine("Не найдено");
        }
    }
}
public class AddWork2 : Work
{
    protected override string[] Lines => File.ReadAllLines($"!{nameof(AddWork2)}.txt");
    protected override Regex Regex => new(@"^(?<prot>(https?|ftp):\/\/)?(www\.)?(?<url>([a-zA-Z0-9_]+\.?){2,5})$");
    public override void PrintFromFile()
    {
        foreach (var line in Lines)
        {
            Match match = Regex.Match(line);
            if (match.Success)
            {
                string prot = string.IsNullOrEmpty(match.Groups["prot"].Value) ? "http://" : match.Groups["prot"].Value;
                string newHref = $"{prot}www.{match.Groups["url"].Value}";
                SaveHrefInFile($"New Hrefs {nameof(AddWork2)}.txt", newHref);
                Console.WriteLine(newHref);
            }
        }
    }
    public static void SaveHrefInFile(string filePath, string text) => File.AppendAllText(filePath, $"{text}\n");
}