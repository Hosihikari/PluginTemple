namespace PluginUnitTest.Testcase;

internal class LevelTickTest : TestItem
{
    public override void Start()
    {
        Minecraft.LevelTick.PostTick(() =>
        {
            if (Minecraft.LevelTick.IsInTickThread)
            {
                MarkSuccess();
            }
            else
            {
                MarkFailed("Not in tick thread.");
            }
        });
    }
}
