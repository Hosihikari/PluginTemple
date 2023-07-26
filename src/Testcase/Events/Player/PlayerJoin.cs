namespace Hosihikari.UnitTest.Testcase.Events.Player;

internal class PlayerJoin : TestItem
{
    public override void Start()
    {
        Minecraft.Extension.Events.Events.PlayerJoin.Before += PlayerJoin_Before;
    }

    private void PlayerJoin_Before(
        object? sender,
        Minecraft.Extension.Events.Implements.Player.JoinEventArgs e
    )
    {
        Minecraft.Extension.Events.Events.PlayerJoin.Before -= PlayerJoin_Before;
        try
        {
            WriteLine(e.ServerPlayer.Name + " Joined");
            MarkSuccess();
        }
        catch (Exception exception)
        {
            MarkFailed(exception.ToString());
        }
    }
}
