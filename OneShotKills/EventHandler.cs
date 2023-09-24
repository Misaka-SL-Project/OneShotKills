namespace OneShotKills;

using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features.DamageHandlers;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using PlayerStatsSystem;
using System.Diagnostics.Tracing;

public class EventHandler
{
     public void OnHurt(HurtingEventArgs ev)
     {
          if (ev.Attacker == null || ev.Player == null) return;

          if (ev.Attacker.Role.Type == RoleTypeId.Scp049 && Plugin.Instance.Config.DoctorOneShots)
          {
               Scp049DamageHandler scp049DamageHandler = new(ev.Attacker.ReferenceHub, 100000, Scp049DamageHandler.AttackType.Instakill);
               ev.Player.Kill(scp049DamageHandler);
          }
          else if (ev.Attacker.Role.Type == RoleTypeId.Scp106)
          {
               if(Plugin.Instance.Config.LarryOneShots)
               {
                    ev.Amount = 50000;
               }
               //else if(Plugin.Instance.Config.EnableOldLarry)
               //{
               //     ev.Player.EnableEffect<PocketCorroding>(0f, false);
               //}
          }
     }
}