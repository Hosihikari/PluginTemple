using Loader;

[assembly: EntryPoint<PluginUnitTest.UnitTest>]

namespace PluginUnitTest;

public class UnitTest : IPlugin
{
    public void Initialize(Plugin plugin)
    {
        Console.WriteLine(nameof(UnitTest) + " Loaded.");
        Minecraft.LevelTick.PostTick(() =>
        {
            Console.WriteLine(nameof(UnitTest) + " PostTick Success.");
        });
    }
}
