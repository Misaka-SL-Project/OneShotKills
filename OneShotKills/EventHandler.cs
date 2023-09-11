namespace OneShotKills;

using CustomPlayerEffects;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

public class EventHandler
{
     public void OnHurt(HurtingEventArgs ev)
     {
          if (ev.Attacker == null || ev.Player == null) return;

          if (ev.Attacker.Role.Type == RoleTypeId.Scp049 && Plugin.Instance.Config.DoctorOneShots)
          {
               ev.Amount = 50000;
          }
          else if (ev.Attacker.Role.Type == RoleTypeId.Scp106)
          {
               if(Plugin.Instance.Config.LarryOneShots) 
                    ev.Amount = 50000;
               else if(Plugin.Instance.Config.EnableOldLarry)
                    ev.Player.EnableEffect<PocketCorroding>(0f, false);
          }
     }
}