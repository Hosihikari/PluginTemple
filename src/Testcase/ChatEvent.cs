namespace Hosihikari.UnitTest.Testcase;

internal class ChatEvent : TestItem
{
    public override void Start()
    {
        Minecraft.Extension.Events.Events.Chat.Before += Chat_Before;
    }

    private void Chat_Before(
        object? sender,
        Minecraft.Extension.Events.Implements.Player.ChatEventArgs e
    )
    {
        try
        {
            WriteLine(e.Player.Name);
            MarkSuccess();
        }
        catch (Exception exception)
        {
            MarkFailed(exception.ToString());
        }
    }
}
