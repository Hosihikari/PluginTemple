namespace Hosihikari.UnitTest.Testcase.Events.Player;

internal class ChatEvent : TestItem
{
    public override void Start()
    {
        Minecraft.Extension.Events.Events.PlayerChat.Before += PlayerChat_Before;
    }

    private void PlayerChat_Before(
        object? sender,
        Minecraft.Extension.Events.Implements.Player.ChatEventArgs e
    )
    {
        Minecraft.Extension.Events.Events.PlayerChat.Before -= PlayerChat_Before;
        try
        {
            WriteLine(e.ServerPlayer.Name);
            MarkSuccess();
        }
        catch (Exception exception)
        {
            MarkFailed(exception.ToString());
        }
    }
}