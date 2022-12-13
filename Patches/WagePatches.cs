using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment;
using TaleWorlds.CampaignSystem.ViewModelCollection.Party;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;

namespace CustomizableTroopWages.WagePatches
{
    internal class TroopWagesPatch
    {

        // companion wage patch
        [HarmonyPatch(typeof(CharacterObject), "TroopWage", MethodType.Getter)]
        internal class TroopWagePatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var instruction in instructions)
                {
                    yield return instruction;

                    if (instruction.opcode == OpCodes.Add)
                    {
                        yield return new CodeInstruction(OpCodes.Conv_R4, null);
                        yield return new CodeInstruction(OpCodes.Ldarg_0, null);
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.AddCompanionMultiplier)));
                        yield return new CodeInstruction(OpCodes.Mul, null);
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MathF), nameof(MathF.Ceiling), new Type[] { typeof(float) }));
                    }

                }
            }
        }

        // the patches below area meant to set a state, so the GetCharacterWage method knows if it's related to player-clan or ai

        //[HarmonyPatch(typeof(ClanVariablesCampaignBehavior), "UpdateClanSettlementsPaymentLimit")]
        //internal class UpdateClanSettlementsPaymentLimitPatch
        //{
        //    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        //    {
        //        bool found = false;

        //        var method_get_wage_default = AccessTools.Method(typeof(DefaultPartyWageModel), nameof(DefaultPartyWageModel.GetCharacterWage));
        //        var method_get_wage = AccessTools.Method(typeof(PartyWageModel), nameof(PartyWageModel.GetCharacterWage));
        //        foreach (var instruction in instructions)
        //        {
        //            if (instruction.operand == (object)method_get_wage_default || instruction.operand == (object)method_get_wage)
        //            {

        //                if (found)
        //                    throw new ArgumentException("Found multiple GetCharacterWage in Patching method");
        //                yield return new CodeInstruction(OpCodes.Ldarg_1, null);
        //                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.IsPlayerRelated), new Type[] { typeof(Clan) }));
        //                found = true;
        //            }
        //            yield return instruction;
        //        }
        //    }
        //}

        [HarmonyPatch(typeof(PartyUpgraderCampaignBehavior), "GetPossibleUpgradeTargets")]
        internal class GetPossibleUpgradeTargetsPatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                int number_get_wage_called = 0; // used to decided whether to pass character or characterObject variable
                var method_get_wage = AccessTools.Method(typeof(PartyWageModel), nameof(PartyWageModel.GetCharacterWage));
                yield return new CodeInstruction(OpCodes.Ldarg_1, null);
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.IsPlayerRelated), new Type[] { typeof(PartyBase) }));
                foreach (var instruction in instructions)
                {
                    if (instruction.opcode == OpCodes.Ret)
                    {
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetdontSwitchPlayerRelatedFalse)));
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedFalse)));
                    }
                    yield return instruction;
                }
            }
        }
        [HarmonyPatch(typeof(RecruitmentCampaignBehavior), "RecruitVolunteersFromNotable")]
        internal class RecruitVolunteersFromNotablePatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                bool found = false;
                var method_get_wage_default = AccessTools.Method(typeof(DefaultPartyWageModel), nameof(DefaultPartyWageModel.GetCharacterWage));
                var method_get_wage = AccessTools.Method(typeof(PartyWageModel), nameof(PartyWageModel.GetCharacterWage));

                yield return new CodeInstruction(OpCodes.Ldarg_1, null);
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.IsPlayerRelated), new Type[] { typeof(MobileParty) }));

                foreach (var instruction in instructions)
                {
                    if (instruction.opcode == OpCodes.Ret)
                    {
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedFalse)));

                    }
                    yield return instruction;
                }
            }
        }


        //[HarmonyPatch(typeof(DefaultPartyDesertionModel), "GetNumberOfDeserters")]
        //internal class GetNumberOfDesertersPatch
        //{
        //    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        //    {
        //        bool found = false;
        //        //var method_get_average_wage = AccessTools.Property(typeof(DefaultPartyDesertionModel), nameof(DefaultPartyDesertionModel.get_AverageWage));
        //        var method_get_average_wage = AccessTools.Method(typeof(DefaultPartyDesertionModel), "get_AverageWage");
        //        foreach (var instruction in instructions)
        //        {
        //            if (instruction.operand == (object)method_get_average_wage)
        //            {
        //                if (found)
        //                    throw new ArgumentException("Found multiple GetCharacterWage in Patching method");
        //                yield return new CodeInstruction(OpCodes.Ldarg_1, null);
        //                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.IsPlayerRelated), new Type[] { typeof(MobileParty) }));
        //                found = true;
        //            }
        //            yield return instruction;
        //        }
        //    }
        //}

        //[HarmonyPatch(typeof(MobileParty), "LimitedPartySize", MethodType.Getter)]
        //internal class LimitedPartySizePatch
        //{
        //    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        //    {
        //        bool found = false;
        //        var method_get_wage_default = AccessTools.Method(typeof(DefaultPartyWageModel), nameof(DefaultPartyWageModel.GetCharacterWage));
        //        var method_get_wage = AccessTools.Method(typeof(PartyWageModel), nameof(PartyWageModel.GetCharacterWage));
        //        foreach (var instruction in instructions)
        //        {
        //            if (instruction.operand == (object)method_get_wage_default || instruction.operand == (object)method_get_wage)
        //            {
        //                if (found)
        //                    throw new ArgumentException("Found multiple GetCharacterWage in Patching method");
        //                yield return new CodeInstruction(OpCodes.Ldarg_0, null);
        //                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.IsPlayerRelated), new Type[] { typeof(MobileParty) }));
        //                found = true;
        //            }
        //            yield return instruction;
        //        }
        //    }
        //}
        // viewmodel patches, I am assuming that they must always be related to the player

        // used to calculate the changes to party expenses during recruitment
        [HarmonyPatch(typeof(RecruitVolunteerTroopVM), MethodType.Constructor, typeof(RecruitVolunteerVM), typeof(CharacterObject), typeof(int), typeof(Action<RecruitVolunteerTroopVM>), typeof(Action<RecruitVolunteerTroopVM>))]
        internal class RecruitVolunteerTroopVMPatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedTrue)));

                foreach (var instruction in instructions)
                {
                    if (instruction.opcode == OpCodes.Ret)
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedFalse)));

                    yield return instruction;
                }
            }
        }

        // used for party screen wage above the troop
        [HarmonyPatch(typeof(PartyVM), "SetSelectedCharacter")]
        internal class RefreshCurrentCharacterInformationPatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {

                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedTrue)));

                foreach (var instruction in instructions)
                {
                    if (instruction.opcode == OpCodes.Ret)
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedFalse)));

                    yield return instruction;
                }
            }
        }
        //used for information screen when hovering over a troop
        [HarmonyPatch(typeof(PropertyBasedTooltipVMExtensions), "UpdateTooltip", typeof(PropertyBasedTooltipVM), typeof(CharacterObject))]
        internal class UpdateTooltipPatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
            {

                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedTrue)));


                foreach (var instruction in instructions)
                {
                    if (instruction.opcode == OpCodes.Ret)
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedFalse)));

                    yield return instruction;
                }
            }
        }
        internal static IEnumerable<CodeInstruction> CalculateGarrisonChangeInternalPatch(IEnumerable<CodeInstruction> instructions)
        {
            yield return new CodeInstruction(OpCodes.Ldarg_0, null);
            yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.IsPlayerRelated), new Type[] { typeof(Settlement) }));

            foreach (var instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ret)
                {
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedFalse)));
                }

                yield return instruction;
            }
        }
        internal static IEnumerable<CodeInstruction> GetTotalWagePatch(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
        {
            yield return new CodeInstruction(OpCodes.Ldarg_1, null);
            yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.IsPlayerRelated), new Type[] { typeof(MobileParty) }));

            foreach (var instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ret)
                {
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.SetIsPlayerRelatedFalse)));
                }

                yield return instruction;
            }
        }
        internal static IEnumerable<CodeInstruction> GetCharacterWagePatch(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ret)
                {
                    yield return new CodeInstruction(OpCodes.Conv_R4, null);
                    yield return new CodeInstruction(OpCodes.Ldarg_1, null);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.ChooseMultiplier)));
                    yield return new CodeInstruction(OpCodes.Mul, null);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MathF), nameof(MathF.Ceiling), new Type[] { typeof(float) }));
                }
                yield return instruction;
            }
        }
        //internal static IEnumerable<CodeInstruction> FindNumberOfTroopsToLeaveToGarrisonPatch(IEnumerable<CodeInstruction> instructions)
        //{
        //    bool found = false;
        //    var method_get_wage_default = AccessTools.Method(typeof(DefaultPartyWageModel), nameof(DefaultPartyWageModel.GetCharacterWage));
        //    var method_get_wage = AccessTools.Method(typeof(PartyWageModel), nameof(PartyWageModel.GetCharacterWage));

        //    foreach (var instruction in instructions)
        //    {
        //        if (instruction.operand == (object)method_get_wage_default || instruction.operand == (object)method_get_wage)
        //        {
        //            if (found)
        //                throw new ArgumentException("Found multiple GetCharacterWage in Patching method");
        //            yield return new CodeInstruction(OpCodes.Ldarg_1, null);
        //            yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.IsPlayerRelated), new Type[] { typeof(MobileParty) }));
        //            found = true;
        //        }
        //        yield return instruction;
        //    }
        //}
        //internal static IEnumerable<CodeInstruction> FindNumberOfTroopsToTakeFromGarrisonPatch(IEnumerable<CodeInstruction> instructions)
        //{
        //    bool found = false;
        //    var method_get_wage_default = AccessTools.Method(typeof(DefaultPartyWageModel), nameof(DefaultPartyWageModel.GetCharacterWage));
        //    var method_get_wage = AccessTools.Method(typeof(PartyWageModel), nameof(PartyWageModel.GetCharacterWage));

        //    foreach (var instruction in instructions)
        //    {
        //        if (instruction.operand == (object)method_get_wage_default || instruction.operand == (object)method_get_wage)
        //        {
        //            if (found)
        //                throw new ArgumentException("Found multiple GetCharacterWage in Patching method");
        //            yield return new CodeInstruction(OpCodes.Ldarg_1, null);
        //            yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomizableTroopWages), nameof(CustomizableTroopWages.IsPlayerRelated), new Type[] { typeof(MobileParty) }));
        //            found = true;
        //        }
        //        yield return instruction;
        //    }
        //}
    }
}