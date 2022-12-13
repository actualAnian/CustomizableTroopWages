using MCM.Abstractions.Base;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.FluentBuilder;
using MCM.Common;
using System.Collections.Generic;
using System;

namespace CustomizableTroopWages
{
    internal class Settings
    {
        private static Settings _instance;
        private FluentGlobalSettings globalSettings;

        public void Dispose()
        {
        }
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }

        public float troopTier0WageMultiplier { get; set; } = 1f;
        public float troopTier1WageMultiplier { get; set; } = 1f;
        public float troopTier2WageMultiplier { get; set; } = 1f;
        public float troopTier3WageMultiplier { get; set; } = 1f;
        public float troopTier4WageMultiplier { get; set; } = 1f;
        public float troopTier5WageMultiplier { get; set; } = 1f;
        public float troopTier6WageMultiplier { get; set; } = 1f;
        public float troopTier7WageMultiplier { get; set; } = 1f;


        public float troopIsNobleOnFootMultiplier { get; set; } = 1f;
        public float troopIsNobleMountedMultiplier { get; set; } = 1f;
        public float troopIsStandardMountedMultiplier { get; set; } = 1f;
        public float companionWageMultiplier { get; set; } = 1f;

        public int troopTier0RecruitCost { get; set; } = 10;
        public int troopTier1RecruitCost { get; set; } = 20;
        public int troopTier2RecruitCost { get; set; } = 50;
        public int troopTier3RecruitCost { get; set; } = 100;
        public int troopTier4RecruitCost { get; set; } = 200;
        public int troopTier5RecruitCost { get; set; } = 400;
        public int troopTier6RecruitCost { get; set; } = 600;
        public int troopTier7RecruitCost { get; set; } = 1000;
        public int troopTierRestRecruitCost { get; set; } = 1500;


        public int troopTier5AndLessHorseCost { get; set; } = 150;
        public int troopTier6AndMoreHorseCost { get; set; } = 500;

        public float mercenaryTroopRecruitMultiplier { get; set; } = 2f;
        public float banditTroopRecruitMultiplier { get; set; } = 2f;
        public float caravanGuardTroopRecruitMultiplier { get; set; } = 2f;


        public int daysBetweenWagePayment { get; set; } = 1;

        public void registerSettings()
        {
            int order = 0;
            var builder = BaseSettingsBuilder.Create("cus_wages", "Customizable Wages")!
    .SetFormat("xml")
    .SetFolderName(SubModule.ModuleFolderName)
    .SetSubFolder(SubModule.ModName)
        //.CreatePreset("def", "Default", presetBuilder => presetBuilder
        //        .SetPropertyValue("troopTier0WageMultiplier", 1)
        //        .SetPropertyValue("troopTier1WageMultiplier", 1)
        //        .SetPropertyValue("troopTier2WageMultiplier", 1)
        //        .SetPropertyValue("troopTier3WageMultiplier", 1)
        //        .SetPropertyValue("troopTier4WageMultiplier", 1)
        //        .SetPropertyValue("troopTier5WageMultiplier", 1)
        //        .SetPropertyValue("troopTier6WageMultiplier", 1)
        //        .SetPropertyValue("troopTier7WageMultiplier", 1)
        //        .SetPropertyValue("companionWageMultiplier", 1)
        //        .SetPropertyValue("troopIsNobleOnFootMultiplier", 1)
        //        .SetPropertyValue("troopIsNobleMountedMultiplier", 1)
        //        .SetPropertyValue("troopIsStandardMountedMultiplier", 1)
        //        .SetPropertyValue("troopTier0RecruitCost", 10)
        //        .SetPropertyValue("troopTier1RecruitCost", 20)
        //        .SetPropertyValue("troopTier2RecruitCost", 50)
        //        .SetPropertyValue("troopTier3RecruitCost", 100)
        //        .SetPropertyValue("troopTier4RecruitCost", 200)
        //        .SetPropertyValue("troopTier5RecruitCost", 400)
        //        .SetPropertyValue("troopTier6RecruitCost", 500)
        //        .SetPropertyValue("troopTier7RecruitCost", 1000)
        //        .SetPropertyValue("troopTierRestRecruitCost", 1500)
        //        .SetPropertyValue("troopTier5AndLessHorseCost", 150)
        //        .SetPropertyValue("troopTier6AndMoreHorseCost", 500)
        //        .SetPropertyValue("mercenaryTroopRecruitMultiplier", 2)
        //        .SetPropertyValue("banditTroopRecruitMultiplier", 2)
        //        .SetPropertyValue("caravanGuardTroopRecruitMultiplier", 2)
        //        .SetPropertyValue("daysBetweenWagePayment", 1))

        .CreateGroup("Wage Multiplier", groupBuilder => groupBuilder
            .SetGroupOrder(order++)
        .AddFloatingInteger("troopTier0WageMultiplier", "Tier 0 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => troopTier0WageMultiplier, o => troopTier0WageMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the wage of the troop. Native cost is 1")
            .SetOrder(order++))
        .AddFloatingInteger("troopTier1WageMultiplier", "Tier 1 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => troopTier1WageMultiplier, o => troopTier1WageMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the wage of the troop. Native cost is 2")
            .SetOrder(order++))
        .AddFloatingInteger("troopTier2WageMultiplier", "Tier 2 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => troopTier2WageMultiplier, o => troopTier2WageMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the wage of the troop. Native cost is 3")
            .SetOrder(order++))
        .AddFloatingInteger("troopTier3WageMultiplier", "Tier 3 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => troopTier3WageMultiplier, o => troopTier3WageMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the wage of the troop. Native cost is 5")
            .SetOrder(order++))
        .AddFloatingInteger("troopTier4WageMultiplier", "Tier 4 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => troopTier4WageMultiplier, o => troopTier4WageMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the wage of the troop. Native cost is 8")
            .SetOrder(order++))
        .AddFloatingInteger("troopTier5WageMultiplier", "Tier 5 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => troopTier5WageMultiplier, o => troopTier5WageMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the wage of the troop. Native cost is 12")
            .SetOrder(order++))
        .AddFloatingInteger("troopTier6WageMultiplier", "Tier 6 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => troopTier6WageMultiplier, o => troopTier6WageMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the wage of the troop. Native cost is 17")
            .SetOrder(order++))
        .AddFloatingInteger("troopTier7WageMultiplier", "Tier 7 and higher troop wage multiplier", 0, 1000, new ProxyRef<float>(() => troopTier7WageMultiplier, o => troopTier7WageMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the wage of the troop. Native cost is 23")
            .SetOrder(order++)))

        .CreateGroup("Special Wage Multiplier", groupBuilder => groupBuilder
            .SetGroupOrder(order++)
        .AddFloatingInteger("companionWageMultiplier", "Companion wage multiplier", 0, 1000, new ProxyRef<float>(() => companionWageMultiplier, o => companionWageMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the wage of the companion. Native calculation is 2 + 2 * companion_level"))
        .AddFloatingInteger("troopIsNobleOnFootMultiplier", "Noble troop on foot multiplier", 0, 1000, new ProxyRef<float>(() => troopIsNobleOnFootMultiplier, o => troopIsNobleOnFootMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("additionally multiplies wages of a troop if they are noble and on foot, Native is 1"))
        .AddFloatingInteger("troopIsNobleMountedMultiplier", "Noble Mounted troop multiplier", 0, 1000, new ProxyRef<float>(() => troopIsNobleMountedMultiplier, o => troopIsNobleMountedMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("additionally multiplies wages of a troop if they are noble and mounted, Native is 1"))
        .AddFloatingInteger("troopIsStandardMountedMultiplier", "Standard Mounted troop multiplier", 0, 1000, new ProxyRef<float>(() => troopIsStandardMountedMultiplier, o => troopIsStandardMountedMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("additionally multiplies wages of a troop if they are NOT noble but they are mounted, Native is 1")))

        .CreateGroup("Recruit Cost (in denars)", groupBuilder => groupBuilder
            .SetGroupOrder(order++)
         .AddInteger("troopTier0RecruitCost", "Troop tier 0 recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier0RecruitCost, o => troopTier0RecruitCost = o), integerBuilder => integerBuilder
            .SetHintText("Native is 10")
            .SetOrder(order++))
         .AddInteger("troopTier1RecruitCost", "Troop tier 1 recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier1RecruitCost, o => troopTier1RecruitCost = o), integerBuilder => integerBuilder
            .SetHintText("Native is 20")
            .SetOrder(order++))
         .AddInteger("troopTier2RecruitCost", "Troop tier 2 recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier2RecruitCost, o => troopTier2RecruitCost = o), integerBuilder => integerBuilder
            .SetHintText("Native is 50")
            .SetOrder(order++))
         .AddInteger("troopTier3RecruitCost", "Troop tier 3 recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier3RecruitCost, o => troopTier3RecruitCost = o), integerBuilder => integerBuilder
            .SetHintText("Native is 100")
            .SetOrder(order++))
         .AddInteger("troopTier4RecruitCost", "Troop tier 4 recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier4RecruitCost, o => troopTier4RecruitCost = o), integerBuilder => integerBuilder
            .SetHintText("Native is 200")
            .SetOrder(order++))
         .AddInteger("troopTier5RecruitCost", "Troop tier 5 recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier5RecruitCost, o => troopTier5RecruitCost = o), integerBuilder => integerBuilder
            .SetHintText("Native is 400")
            .SetOrder(order++))
         .AddInteger("troopTier6RecruitCost", "Troop tier 6 recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier6RecruitCost, o => troopTier6RecruitCost = o), integerBuilder => integerBuilder
            .SetHintText("Native is 600")
            .SetOrder(order++))
         .AddInteger("troopTier7RecruitCost", "Troop tier 7 recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier7RecruitCost, o => troopTier7RecruitCost = o), integerBuilder => integerBuilder
            .SetHintText("Native is 1000")
            .SetOrder(order++))
         .AddInteger("troopTierRestRecruitCost", "Other category troop recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTierRestRecruitCost, o => troopTierRestRecruitCost = o), integerBuilder => integerBuilder
            .SetHintText("Native is 1500")
            .SetOrder(order++)))

         .CreateGroup("Recruitment Horses cost", groupBuilder => groupBuilder
            .SetGroupOrder(order++)
         .AddInteger("troopTier5AndLessHorseCost", "Troop tier 5 or less horse recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier5AndLessHorseCost, o => troopTier5AndLessHorseCost = o), integerBuilder => integerBuilder
            .SetHintText("How much of the recruitment cost should the horse add. Native is 150")
            .SetOrder(order++))
         .AddInteger("troopTier6AndMoreHorseCost", "Other category troop horse recruitment cost", 1, 10000, new ProxyRef<int>(() => troopTier6AndMoreHorseCost, o => troopTier6AndMoreHorseCost = o), integerBuilder => integerBuilder
            .SetHintText("How much of the recruitment cost should the horse add. Native is 500")
            .SetOrder(order++)))

        .CreateGroup("Special Troop Types Recruitment Multiplier", groupBuilder => groupBuilder
            .SetGroupOrder(order++)
        .AddFloatingInteger("mercenaryTroopRecruitMultiplier", "Mercenary troop recruitment multiplier", 0, 1000, new ProxyRef<float>(() => mercenaryTroopRecruitMultiplier, o => mercenaryTroopRecruitMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the recruitment cost of the mercenary troops. Native is 2")
            .SetOrder(order++))
        .AddFloatingInteger("banditTroopRecruitMultiplier", "Bandit troop recruitment multiplier", 0, 1000, new ProxyRef<float>(() => banditTroopRecruitMultiplier, o => banditTroopRecruitMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the recruitment cost of the bandit troops. Native is 2")
            .SetOrder(order++))
        .AddFloatingInteger("caravanGuardTroopRecruitMultiplier", "Caravan guard troop recruitment multiplier", 0, 1000, new ProxyRef<float>(() => caravanGuardTroopRecruitMultiplier, o => caravanGuardTroopRecruitMultiplier = o), floatingBuilder => floatingBuilder
            .SetHintText("Multiplies and rounds up the recruitment cost of the Caravan guard troops. Native is 2")
            .SetOrder(order++)))

        .CreateGroup("Time Before Next Wage Payment", groupBuilder => groupBuilder
            .SetGroupOrder(order++)
         .AddInteger("daysBetweenWagePayment", "Days before next wage payment", 1, 100, new ProxyRef<int>(() => daysBetweenWagePayment, o => daysBetweenWagePayment = o), integerBuilder => integerBuilder
            .SetHintText("How many days should pass before next clan wage payment. Native is 1")));

            globalSettings = builder.BuildAsGlobal();
            globalSettings.Register();
        }
    }
}