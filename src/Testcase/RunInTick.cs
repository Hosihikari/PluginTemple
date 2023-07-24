namespace Hosihikari.PluginUnitTest.Testcase;

internal class RunInTick : TestItem
{
    public override void Start()
    {
        _ = Task.Run(async () =>
        {
            await Task.Delay(1000).ConfigureAwait(false);
            await Minecraft.Async.RunInTickVoid.StartAsync(() =>
            {
                if (!Minecraft.LevelTick.IsInTickThread)
                {
                    MarkFailed("Not in tick thread.");
                }
            });
            const int test = 2333;
            await Task.Delay(1000).ConfigureAwait(false);
            var result = await Minecraft.Async.RunInTick<int>.StartAsync(() =>
            {
                if (!Minecraft.LevelTick.IsInTickThread)
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
