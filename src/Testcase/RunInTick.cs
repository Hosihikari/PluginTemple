using Hosihikari.Minecraft.Extension;
using Hosihikari.Minecraft.Extension.Async;

namespace Hosihikari.UnitTest.Testcase;

internal class RunInTick : TestItem
{
    public override void Start()
    {
        _ = Task.Run(async () =>
        {
            await Task.Delay(1000).ConfigureAwait(false);
            await RunInTickVoid.StartAsync(() =>
            {
                if (!LevelTick.IsInTickThread)
                {
                    MarkFailed("Not in tick thread.");
                }
            });
            const int test = 2333;
            await Task.Delay(1000).ConfigureAwait(false);
            var result = await RunInTick<int>.StartAsync(() =>
            {
                if (!LevelTick.IsInTickThread)
                {
                    MarkFailed("Not in tick thread.");
                }
                return test;
            });
            await Task.Delay(1000).ConfigureAwait(false);
            if (test != result)
            {
                MarkFailed("Result is not correct.");
            }
            if (IsSuccess is null)
            {
                MarkSuccess();
            }
        });
    }
}
