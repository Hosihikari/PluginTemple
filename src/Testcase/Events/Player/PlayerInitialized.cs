namespace Hosihikari.UnitTest.Testcase.Events.Player;

internal class PlayerInitialized : TestItem
{
    public override void Start()
    {
        Minecraft.Extension.Events.Events.PlayerInitialized.Before += PlayerInitialized_Before;
    }

    private void PlayerInitialized_Before(
        object? sender,
        Minecraft.Extension.Events.Implements.Player.InitializedEventArgs e
    )
    {
        Minecraft.Extension.Events.Events.PlayerInitialized.Before -= PlayerInitialized_Before;
        try
        {
            WriteLine(e.ServerPlayer.Name + " Initialized");
            MarkSuccess();
        }
        catch (Exception exception)
        {
            MarkFailed(exception.ToString());
        }
    }
}
