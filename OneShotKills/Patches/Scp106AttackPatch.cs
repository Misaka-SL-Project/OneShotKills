using HarmonyLib;
using System.Reflection.Emit;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;
using Mono.Collections.Generic;
using PlayerRoles.PlayableScps.Scp106;
using System.Reflection;
using CustomPlayerEffects;
using VoiceChat.Networking;
using PlayerRoles.Voice;
using PlayerRoles;
using VoiceChat;

namespace OneShotKills.Patches
{

     [HarmonyPatch(typeof(Scp106Attack), nameof(Scp106Attack.ServerShoot))]
     internal static class InstaTeleport
     {
          public static bool ShouldTP(bool oldState)
          {
               return Plugin.Instance.Config.EnableOldLarry ? true : oldState;
          }
          private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
          {
               List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

               int index = newInstructions.FindLastIndex(instruction =>
               instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == Method(typeof(PlayerEffectsController), nameof(PlayerEffectsController.GetEffect), null, new[] { typeof(Corroding) }));
               index += 4;

               Collection<CodeInstruction> collection = new()
               {
                    new(OpCodes.Call, Method(typeof(InstaTeleport), nameof(InstaTeleport.ShouldTP), new [] { typeof(bool) }))
               };
               newInstructions.InsertRange(index, collection);

               foreach (CodeInstruction instruction in newInstructions)
                    yield return instruction;

               ListPool<CodeInstruction>.Shared.Return(newInstructions);
          }
     }
}
