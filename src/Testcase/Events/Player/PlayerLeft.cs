namespace Hosihikari.UnitTest.Testcase.Events.Player;

internal class PlayerLeft : TestItem
{
    public override void Start()
    {
        Minecraft.Extension.Events.Events.PlayerLeft.Before += PlayerLeft_Before;
    }

    private void PlayerLeft_Before(
        object? sender,
        Minecraft.Extension.Events.Implements.Player.LeftEventArgs e
    )
    {
        Minecraft.Extension.Events.Events.PlayerLeft.Before -= PlayerLeft_Before;
        try
        {
            WriteLine(e.ServerPlayer.Name + "Left");
            MarkSuccess();
        }
        catch (Exception exception)
        {
            MarkFailed(exception.ToString());
        }
    }
}