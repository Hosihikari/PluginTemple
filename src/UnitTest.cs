using System.Reflection;
using System.Runtime.CompilerServices;
using Hosihikari.Logging;
using Hosihikari.PluginManager;
using Hosihikari.UnitTest;

[assembly: EntryPoint<UnitTest>]

namespace Hosihikari.UnitTest;

public class UnitTest : IEntryPoint
{
    Logger Logger = new(nameof(UnitTest));

    public void Initialize(AssemblyPlugin plugin)
    {
        Logger.Debug("Searching All Testcase.");
        AddAllItemToList();
        Logger.Debug($"Searching All Testcase Success. Total {TaskList.Count}");
    }

    internal List<TestItem> TaskList = new();

    private void UpdateStatistical()
    {
        var success = TaskList.Count(x => x.IsSuccess is true);
        var failed = TaskList.Count(x => x.IsSuccess is false);
        var total = TaskList.Count;
        var waiting = TaskList.Count(x => x.IsSuccess is null);
        Logger.Information(
            $"Total: {total} Success: {success} Failed: {failed} Waiting: {waiting}"
        );
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
                    Logger.Error($"{s}\n\t{file[trimLength..]} {line}");
                item.OnWriteLine += (s, file, line) =>
                    Logger.Information($"{s}\n\t{file[trimLength..]} {line}");
                item.OnSuccess += (file, line) =>
                {
                    Logger.Information($"Success: {file[trimLength..]} {line}");
                    UpdateStatistical();
                };
                item.OnFailed += (s, file, line) =>
                {
                    Logger.Warning($"Success: {s}\n\t{file[trimLength..]} {line}");
                    UpdateStatistical();
                };
                item.Start();
                TaskList.Add(item);
                Logger.Debug(" Add Testcase: " + type.Name);
            }
        }
    }
}
