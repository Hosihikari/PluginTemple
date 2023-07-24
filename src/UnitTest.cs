using System.Reflection;
using System.Runtime.CompilerServices;
using Hosihikari.Loader;
using Hosihikari.PluginUnitTest;

[assembly: EntryPoint<UnitTest>]

namespace Hosihikari.PluginUnitTest;

public class UnitTest : IEntryPoint
{
    public void Initialize(AssemblyPlugin plugin)
    {
        WriteLine(" Searching All Testcase.");
        AddAllItemToList();
        WriteLine(" Searching All Testcase Success.");
    }

    internal List<TestItem> TaskList = new();

    private void WriteLine(string str)
    {
        Console.WriteLine(nameof(UnitTest) + " >> " + str);
    }

    private void UpdateStatistical()
    {
        var success = TaskList.Count(x => x.IsSuccess is true);
        var failed = TaskList.Count(x => x.IsSuccess is false);
        var total = TaskList.Count;
        var waiting = TaskList.Count(x => x.IsSuccess is null);
        WriteLine($"Total: {total} Success: {success} Failed: {failed} Waiting: {waiting}");
    }

    private void AddAllItemToList([CallerFilePath] string sourceFile = "")
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();
        var trimLength =
            sourceFile.IndexOf(nameof(UnitTest), StringComparison.Ordinal) + "Testcase\\".Length;

        foreach (var type in types)
        {
            if (
                type.IsSubclassOf(typeof(TestItem))
                && Activator.CreateInstance(type) is TestItem item
            )
            {
                item.OnWriteError += (s, file, line) =>
                    WriteLine($"[Error] {s}\n\t{file[trimLength..]} {line}");
                item.OnWriteLine += (s, file, line) =>
                    WriteLine($"[Info] {s}\n\t{file[trimLength..]} {line}");
                item.OnSuccess += (file, line) =>
                {
                    WriteLine($"[Success] {file[trimLength..]} {line}");
                    UpdateStatistical();
                };
                item.OnFailed += (s, file, line) =>
                {
                    WriteLine($"[Failed] {s}\n\t{file[trimLength..]} {line}");
                    UpdateStatistical();
                };
                item.Start();
                TaskList.Add(item);
                WriteLine(" Add Testcase: " + type.Name);
            }
        }
    }
}
