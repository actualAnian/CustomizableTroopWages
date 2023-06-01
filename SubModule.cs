using CustomizableTroopWages.CostPatches;
using CustomizableTroopWages.WagePatches;
using HarmonyLib;
using MCM.Abstractions.Base.Global;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace CustomizableTroopWages
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly Harmony harmony = new("CustomizableTroopWagesPaymentTime");
        private bool manualPatchesHaveFired = false;
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            harmony.PatchAll();
        }
        // MCM Menu
        public static readonly string ModuleFolderName = "CustomizableTroop";
        public static readonly string ModName = "CustomizableTroopWages";
        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }
        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            base.InitializeGameStarter(game, starterObject);
            if (starterObject is CampaignGameStarter starter)
            {
                starter.AddBehavior(new CustomizableWagesPayment());
            }
        }
        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            if(!manualPatchesHaveFired)
            {
                manualPatchesHaveFired = true;
                RunManualPatches();
            }
        }

        private void RunManualPatches()
        {
            // all of these patches are called when campaign is loaded, because otherwise you can not debug the mod
            var originalGarChange = AccessTools.Method("DefaultSettlementGarrisonModel:CalculateGarrisonChangeInternal");
            var originalTotalWage = AccessTools.Method("DefaultPartyWageModel:GetTotalWage");
            var originalGetCharWage = AccessTools.Method("DefaultPartyWageModel:GetCharacterWage");
            var originalGetRecCost = AccessTools.Method("DefaultPartyWageModel:GetTroopRecruitmentCost");
            //var originalTakeFromGar = AccessTools.Method("DefaultSettlementGarrisonModel:FindNumberOfTroopsToTakeFromGarrison");
            //var originalLeaLeaveToGar = AccessTools.Method("DefaultSettlementGarrisonModel:FindNumberOfTroopsToLeaveToGarrison");
            harmony.Patch(originalGarChange, transpiler: new HarmonyMethod(typeof(TroopWagesPatch), nameof(TroopWagesPatch.CalculateGarrisonChangeInternalPatch)));
            harmony.Patch(originalTotalWage, transpiler: new HarmonyMethod(typeof(TroopWagesPatch), nameof(TroopWagesPatch.GetTotalWagePatch)));
            harmony.Patch(originalGetCharWage, transpiler: new HarmonyMethod(typeof(TroopWagesPatch), nameof(TroopWagesPatch.GetCharacterWagePatch)));
            harmony.Patch(originalGetRecCost, transpiler: new HarmonyMethod(typeof(TroopCostPatch), nameof(TroopCostPatch.GetTroopRecruitmentCostPatch)));
            //harmony.Patch(originalTakeFromGar, transpiler: new HarmonyMethod(typeof(TroopWagesPatch), nameof(TroopWagesPatch.FindNumberOfTroopsToTakeFromGarrisonPatch)));
            //harmony.Patch(originalLeaLeaveToGar, transpiler: new HarmonyMethod(typeof(TroopWagesPatch), nameof(TroopWagesPatch.FindNumberOfTroopsToLeaveToGarrisonPatch)));
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            MCM.Abstractions.FluentBuilder.ISettingsBuilder settingsBuilder = Settings.Instance.RegisterSettings();
            FluentGlobalSettings settings = settingsBuilder.BuildAsGlobal();
            settings.Register();
        }
    }
}