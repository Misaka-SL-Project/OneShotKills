namespace OneShotKills;

using Exiled.API.Features;
using HarmonyLib;

public class Plugin : Plugin<Config>
{
     public override string Name => "OneShotKills";
     public override string Prefix => Name;
     public override string Author => "@misfiy";
     public override Version Version => new(1, 0, 2);
     public override Version RequiredExiledVersion => new(8, 2, 1);

     public static Plugin Instance;

     //private EventHandler handler;
     private Harmony? harmony;

     public override void OnEnabled()
     {
          Instance = this;
          //handler = new();

          harmony = new("OneShotKills");
          harmony.PatchAll();

          //Exiled.Events.Handlers.Player.Hurting += handler.OnHurt;

          base.OnEnabled();
     }
     public override void OnDisabled()
     {
          //Exiled.Events.Handlers.Player.Hurting -= handler.OnHurt;

          harmony?.UnpatchAll("OneShotKills");
          harmony = null;

          //handler = null!;
          Instance = null!;
          base.OnDisabled();
     }
}