using Hosihikari.Minecraft.Extension;

namespace Hosihikari.UnitTest.Testcase;

internal class LevelTickTest : TestItem
{
    public override void Start()
    {
        LevelTick.PostTick(() =>
        {
            if (LevelTick.IsInTickThread)
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
