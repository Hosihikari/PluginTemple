namespace PluginUnitTest.Testcase;

internal class LevelTickTest : TestItem
{
    public override void Start()
    {
        Minecraft.LevelTick.PostTick(() =>
        {
            MarkSuccess();
        });
    }
}
