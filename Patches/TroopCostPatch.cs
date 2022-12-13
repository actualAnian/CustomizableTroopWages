using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.LinQuick;

namespace CustomizableTroopWages.CostPatches
{
    [HarmonyPatch(typeof(DefaultPartyWageModel), "GetTroopRecruitmentCost")]
    internal class TroopCostPatch
    {
        internal static IEnumerable<CodeInstruction> GetTroopRecruitmentCostPatch(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
        {
            var codes = instructions.ToListQ();
            var insertion = 0;
            var startVanillaRecruitjumpLabel = ilGenerator.DefineLabel();
            var afterVanillaRecruitjumpLabel = ilGenerator.DefineLabel();
            for (var index = 0; index < codes.Count; index++)
            {
                if (codes[index].opcode == OpCodes.Ldarg_1
                    && codes[index + 1].opcode == OpCodes.Callvirt
                    && codes[index + 2].opcode == OpCodes.Ldc_I4_1
                    && codes[index + 3].opcode == OpCodes.Bgt_S
                    && codes[index - 1].opcode == OpCodes.Mul)
                    insertion = index;

                if (codes[index].opcode == OpCodes.Ldc_I4_S
                    && codes[index + 1].opcode == OpCodes.Ldarg_1
                    && codes[index + 2].opcode == OpCodes.Callvirt
                    && codes[index + 3].opcode == OpCodes.Conv_R4)
                    codes[index].labels.Add(startVanillaRecruitjumpLabel);

                if (codes[index].opcode == OpCodes.Ldarg_2
                    && codes[index + 1].opcode == OpCodes.Brfalse
                    && codes[index + 2].opcode == OpCodes.Ldloca_S
                    && codes[index + 3].opcode == OpCodes.Ldc_R4)
                    codes[index].labels.Add(afterVanillaRecruitjumpLabel);
            }

            var instr_list = new List<CodeInstruction>
                {
                    new(OpCodes.Ldarg_2, null),
                    new(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopCost), nameof(CustomizableTroopCost.IsPlayerClan))),
                    new(OpCodes.Brfalse, startVanillaRecruitjumpLabel),
                    new(OpCodes.Ldarg_1, null),
                    new(OpCodes.Ldarg_2, null),
                    new(OpCodes.Ldarg_3, null),
                    new(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopCost), nameof(CustomizableTroopCost.ChooseTroopCost))),
                    new(OpCodes.Stloc_0, null),
                    new(OpCodes.Br, afterVanillaRecruitjumpLabel)
                };
            codes.InsertRange(insertion, instr_list);
            return codes.AsEnumerable();
        }
    }
}
