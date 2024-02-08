using HarmonyLib;
using System.Reflection.Emit;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;
using Mono.Collections.Generic;
using PlayerRoles.PlayableScps.Scp106;
using System.Reflection;
using CustomPlayerEffects;
using Exiled.API.Features;
using PlayerRoles.PlayableScps.Scp049;

namespace OneShotKills.Patches
{

    [HarmonyPatch(typeof(Scp049AttackAbility), nameof(Scp049AttackAbility.ServerProcessCmd))]
    internal static class InstaKill
    {
        public static bool ShouldInstakill(bool oldState)
        {
            return Plugin.Instance.Config.Scp049InstaKill ? true : oldState;
        }
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindLastIndex(instruction =>
            instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == Method(typeof(PlayerEffectsController), nameof(PlayerEffectsController.GetEffect), null, new[] { typeof(CardiacArrest) }));
            index += 5;

            Collection<CodeInstruction> collection = new()
               {
                    new(OpCodes.Call, Method(typeof(InstaKill), nameof(InstaKill.ShouldInstakill), new [] { typeof(bool) }))
               };
            newInstructions.InsertRange(index, collection);

            foreach (CodeInstruction instruction in newInstructions)
                yield return instruction;

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
