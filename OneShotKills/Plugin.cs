namespace OneShotKills;

using Exiled.API.Enums;
using Exiled.API.Features;


public class Plugin : Plugin<Config>
{
    public override string Name => "OneShotKills";
    public override string Prefix => Name;
    public override string Author => "@misfiy";

    public static Plugin Instance;
    
    private EventHandler handler;

    public override void OnEnabled()
    {
        Instance = this;
        handler = new();

        Exiled.Events.Handlers.Player.Hurting += handler.OnHurt;

        base.OnEnabled();
    }
    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.Hurting -= handler.OnHurt;

        handler = null!;
        Instance = null!;
        base.OnDisabled();
    }
}