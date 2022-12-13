using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.LinQuick;

namespace CustomizableTroopWages.WagesPaymentPatches
{
    [HarmonyPatch(typeof(DefaultClanFinanceModel), "CalculateClanExpensesInternal")]
    internal class CalculateClanExpensesInternalPatch
    {

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilgenerator)
        {
            var call_AddExpenseFromLeaderParty = AccessTools.Method(typeof(DefaultClanFinanceModel), "AddExpenseFromLeaderParty");
            var method_get_IsUnderMercenaryService = AccessTools.Method(typeof(Clan), "get_IsUnderMercenaryService");

            Label jumpLabel = ilgenerator.DefineLabel();

            int insertion = 0;
            List<CodeInstruction> codes = instructions.ToListQ<CodeInstruction>();
            for (int index = 0; index < codes.Count; index++)
            {
                if (codes[index].opcode == OpCodes.Ldarg_0 && codes[index + 1].opcode == OpCodes.Ldarg_1 && codes[index + 2].opcode == OpCodes.Ldarg_2 && codes[index + 3].opcode == OpCodes.Ldarg_3 && codes[index + 4].operand == (object)call_AddExpenseFromLeaderParty)
                {

                    insertion = index;
                }

                if (codes[index].opcode == OpCodes.Call && codes[index + 1].opcode == OpCodes.Ldarg_1 && codes[index + 2].operand == (object)method_get_IsUnderMercenaryService)
                {
                    codes[index + 1].labels.Add(jumpLabel);
                }
            }
            List<CodeInstruction> stack = new List<CodeInstruction>
                {

                new CodeInstruction(OpCodes.Ldarg_1, null),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableWagesPayment), nameof(CustomizableWagesPayment.AddDay))),
                new CodeInstruction(OpCodes.Brtrue_S, jumpLabel)
                };
            codes.InsertRange(insertion, stack);
            return codes.AsEnumerable<CodeInstruction>();
        }
    }

    [HarmonyPatch(typeof(ClanVariablesCampaignBehavior), "DailyTickClan")]
    internal class DailyTickClanPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            yield return new CodeInstruction(OpCodes.Ldarg_1, null);
            yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableWagesPayment), nameof(CustomizableWagesPayment.SetDailyTickPlayerState)));

            foreach (var instruction in instructions)
            {
                yield return instruction;
            }
        }
    }
}
