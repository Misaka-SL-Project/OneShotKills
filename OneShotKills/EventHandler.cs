namespace OneShotKills;

using CustomPlayerEffects;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Exiled.API.Enums;
using PlayerRoles.PlayableScps.Scp106;
using PluginAPI.Events;

public class EventHandler
{
    public void OnHurt(HurtingEventArgs ev)
    {
        if(ev.Attacker == null || ev.Player == null) return;

        if(ev.Attacker.Role.Type == RoleTypeId.Scp049 && Plugin.Instance.Config.DoctorOneShots)
        {
            ev.Amount = 50000;
        }
        else if(ev.Attacker.Role.Type == RoleTypeId.Scp106 && Plugin.Instance.Config.LarryOneShots)
        {
            ev.Amount = 50000;
        }
    }
}
