using System;
using System.Collections;
using System.Text;
using static StringBuilderManager;

namespace ConsoleApp22
{

    internal class Program
    {
        static void Main(string[] args)
        {
            IAction[] actions = { new Actioninput(), new PrintAction() };
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < actions.Length; i++)
            {
                stringBuilder.AppendLine($"{i + 1} - {actions[i].Description}");
            }
            while (true)
            {

                if (InputHelper.Input(stringBuilder.ToString(), 1, actions.Length, out int inputvalue))
                {
                    actions[inputvalue - 1].Run();
                }
                continue;
                string b1 = Console.ReadLine();
                int b=int.Parse(b1);
                if (b == 1)
                {

                }
                else if (b == 2 && stringBuilder.Length != 0)
                {

                }
                else if (b == 2 && stringBuilder.Length == 0)
                {
                    Console.WriteLine("Вы ничего не ввели!!!!");
                }
                if (b == 3)
                {
                    if (stringBuilder.Length == 0) Console.WriteLine("Там пусто!!!");
                    else
                    {
                        Clear();
                        string j = Console.ReadLine();
                        int.TryParse(j, out int k);
                        if (k == 1) stringBuilder.Clear();
                        else continue;
                    }
                }
                if (b == 4)
                {
                    if (stringBuilder.Length == 0) Console.WriteLine("Там пусто!!!");
                    else
                    {
                        Console.WriteLine(stringBuilder);
                        Repl1();
                        string e = Console.ReadLine();
                        Repl2();
                        string r = Console.ReadLine();
                        Console.WriteLine("Вы хотите заменить все " + e + " на " + r + "?");
                        Console.WriteLine("Подтвердить?\n 1 - Да\n2 - Нет");
                        string u = Console.ReadLine();
                        int.TryParse(u, out int l);
                        if (l == 1)
                        {
                            stringBuilder.Replace(e, r);
                            Console.WriteLine(stringBuilder);
                        }
                        else continue;
                    }
                }
                if (b == 5)
                {
                    Console.Clear();
                }
            }
            static void Menu()
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("|1 - Ввод текста        |");
                Console.WriteLine("|2 - Вывод текста       |");
                Console.WriteLine("|3 - Удалить весь текст |");
                Console.WriteLine("|4 - Заменить текст     |");
                Console.WriteLine("|5 - Отчистить экран    |");
                Console.WriteLine("-------------------------");

            }

            static void Clear()
            {
                Console.WriteLine("Вы уверены что хотите удалить весь текст?\n 1 - Да\n 2 - Нет");
            }
            static void Repl1()
            {
                Console.WriteLine("Какую строку вы хотите заменить?");
            }
            static void Repl2()
            {
                Console.WriteLine("На что вы хотите заменить ее?");
            }
        }
    }
}

public class Actioninput : IAction
{
    public string Description => "ввод текста";

    public void Run()
    {
        var modes = Get_PrintLineMode();
        if (InputHelper.Input("", (int)modes[0], (int)modes[modes.Count], out int intvalue))
        {
            Console.WriteLine("введите текст");
            StringBuilderManager.Instance.ADDContent(Console.ReadLine(), (LineMode)intvalue);
        }
    }
    static IList Get_PrintLineMode()
    {
        IList list = Enum.GetValues(typeof(LineMode));
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            object item = list[i];
            stringBuilder.Append($"{i + 1} - {list[i]}");
        }
        Console.WriteLine(stringBuilder);
        return list;
    }
}

public class PrintAction : IAction
{
    public string Description => "вывод информации";

    public void Run()
    {
        if (StringBuilderManager.Instance.ConteinsInfo())
        {
            StringBuilderManager.Instance.PrintInfo();
        }
    }
}




public class StringBuilderManager
{
    private readonly StringBuilder stringBuilder = new StringBuilder();
    public static StringBuilderManager Instance;
    static StringBuilderManager()
    {
        Instance = new StringBuilderManager();
    }
    private StringBuilderManager()
    {

    }
    public void ADDContent(string _text, LineMode lineMode)
    {
        switch (lineMode)
        {
            case LineMode.write:
                stringBuilder.Append(_text);
                break;
            case LineMode.Writeline:
                stringBuilder.Append($"\n{_text}");
                break;
            default:
                break;
        }
    }

    public void PrintInfo()
    {
        Console.WriteLine(stringBuilder);
    }

    public bool ConteinsInfo()
    {
        return stringBuilder.Length > 0;
    }

    public void ReplaceText(string a, string b)
    {
        stringBuilder.Replace(a, b);
    }

    public void ClearAllText()
    {
        stringBuilder.Clear();
    }
}

public static class InputHelper
{
    public static bool Input(string description, int min, int max, out int inputValue)
    {
        Console.WriteLine(description);
        bool result = false;
        if (int.TryParse(Console.ReadLine(), out inputValue))
        {
            if (inputValue >= min && inputValue <= max)
            {
                result = true;
            }
        }
        return result;
    }
}
public interface IAction
{
    public string Description { get; }
    public void Run();
}
public enum LineMode
{
    write = 1,
    Writeline
}