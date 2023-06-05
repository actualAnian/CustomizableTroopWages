using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;

namespace CustomizableTroopWages.CostPatches
{
    internal class UpgradeCostPatch
    {
        [HarmonyDebug]
        [HarmonyPatch(typeof(DefaultPartyTroopUpgradeModel), "GetGoldCostForUpgrade")]
        internal class TroopUpgradeCostPatch
        {
            internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                bool found = false;
                foreach (var instruction in instructions)
                {
                    if(instruction.opcode == OpCodes.Conv_I4)
                    {
                        if (found) throw new Exception("incorrect harmony patch in Customizable Troop Wages, UpgradeCostPatch");
                        found = true;
//                        yield return new CodeInstruction(OpCodes.Conv_R4, null);
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Ldarg_3);
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopUpgradeCost), nameof(CustomizableTroopUpgradeCost.MultiplyUpgradeCost)));
                        yield return new CodeInstruction(OpCodes.Mul, null);
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MathF), nameof(MathF.Ceiling), new Type[] { typeof(float) }));
                    }
                    yield return instruction;
                }
            }
        }
    }

}
