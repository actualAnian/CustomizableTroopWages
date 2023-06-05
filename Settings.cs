using MCM.Abstractions.Base;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.FluentBuilder;
using MCM.Common;

namespace CustomizableTroopWages
{
    internal class Settings
    {
        private static Settings? _instance;
        
        public void Dispose()
        {
        }
        public static Settings Instance
        {
            get
            {
                _instance ??= new Settings();
                return _instance;
            }
        }

        public float TroopTier0WageMultiplier { get; set; } = 1f;
        public float TroopTier1WageMultiplier { get; set; } = 1f;
        public float TroopTier2WageMultiplier { get; set; } = 1f;
        public float TroopTier3WageMultiplier { get; set; } = 1f;
        public float TroopTier4WageMultiplier { get; set; } = 1f;
        public float TroopTier5WageMultiplier { get; set; } = 1f;
        public float TroopTier6WageMultiplier { get; set; } = 1f;
        public float TroopTier7WageMultiplier { get; set; } = 1f;


        public float TroopIsNobleOnFootMultiplier { get; set; } = 1f;
        public float TroopIsNobleMountedMultiplier { get; set; } = 1f;
        public float TroopIsStandardMountedMultiplier { get; set; } = 1f;
        public float CompanionWageMultiplier { get; set; } = 1f;

        public int TroopTier0RecruitCost { get; set; } = 10;
        public int TroopTier1RecruitCost { get; set; } = 20;
        public int TroopTier2RecruitCost { get; set; } = 50;
        public int TroopTier3RecruitCost { get; set; } = 100;
        public int TroopTier4RecruitCost { get; set; } = 200;
        public int TroopTier5RecruitCost { get; set; } = 400;
        public int TroopTier6RecruitCost { get; set; } = 600;
        public int TroopTier7RecruitCost { get; set; } = 1000;
        public int TroopTierRestRecruitCost { get; set; } = 1500;


        public int TroopTier5AndLessHorseCost { get; set; } = 150;
        public int TroopTier6AndMoreHorseCost { get; set; } = 500;

        public float MercenaryTroopRecruitMultiplier { get; set; } = 2f;
        public float BanditTroopRecruitMultiplier { get; set; } = 2f;
        public float CaravanGuardTroopRecruitMultiplier { get; set; } = 2f;


        public int DaysBetweenWagePayment { get; set; } = 1;

        public float StandardTroopUpgradeMult { get; set; } = 1f;
        public float NobleTroopUpgradeMult { get; set; } = 1f;



        public ISettingsBuilder RegisterSettings()
        {
            int order = 0;
            var builder = BaseSettingsBuilder.Create("customizable_troop_wages", "CustomizableTroopWages")!
                .SetFormat("json2")

                .SetFolderName(nameof(CustomizableTroopWages))
                .SetSubFolder("CustomizableTroopWages")
                .CreateGroup("{=ctw_wage_multiplier}Wage Multiplier", BuildWageMultiplierSettings)
                .CreateGroup("{=ctw_special_wage_multiplier}Special Wage Multiplier", BuildSpecialWageMultiplierSettings)
                .CreateGroup("{=ctw_recruit_cost}Recruit Cost (in denars)", BuildRecruitCostSettings)
                .CreateGroup("{=ctw_recruitment_horses_cost}Recruitment Horses Cost", BuildHorsesCostSettings)
                .CreateGroup("{=ctw_sp_troop_recruitment_multi}Special Troop Types Recruitment Multiplier", BuildSpecialTroopRecruitmentSettings)
                .CreateGroup("{=ctw_time_before_next_wage_payment}Time Before Next Wage Payment", BuildTimeBeforeNextWagePaymentSettings)
                .CreateGroup("{=ctw_upgrade_cost_mult}Upgrade Troop Multiplier", BuildUpgradeCostMultiplierSettings)

                .CreatePreset(BaseSettings.DefaultPresetId, BaseSettings.DefaultPresetName, builder => BuildDefaultPreset(builder, new()));

            return builder;

            static void BuildDefaultPreset(ISettingsPresetBuilder builder, object value)
                => builder
                    .SetPropertyValue("troopTier0WageMultiplier", Settings.Instance.TroopTier0WageMultiplier)
                    .SetPropertyValue("troopTier1WageMultiplier", Settings.Instance.TroopTier1WageMultiplier)
                    .SetPropertyValue("troopTier2WageMultiplier", Settings.Instance.TroopTier2WageMultiplier)
                    .SetPropertyValue("troopTier3WageMultiplier", Settings.Instance.TroopTier3WageMultiplier)
                    .SetPropertyValue("troopTier4WageMultiplier", Settings.Instance.TroopTier4WageMultiplier)
                    .SetPropertyValue("troopTier5WageMultiplier", Settings.Instance.TroopTier5WageMultiplier)
                    .SetPropertyValue("troopTier6WageMultiplier", Settings.Instance.TroopTier6WageMultiplier)
                    .SetPropertyValue("troopTier7WageMultiplier", Settings.Instance.TroopTier7WageMultiplier)

                    .SetPropertyValue("companionWageMultiplier", Settings.Instance.CompanionWageMultiplier)
                    .SetPropertyValue("troopIsNobleOnFootMultiplier", Settings.Instance.TroopIsNobleOnFootMultiplier)
                    .SetPropertyValue("troopIsNobleMountedMultiplier", Settings.Instance.TroopIsNobleMountedMultiplier)
                    .SetPropertyValue("troopIsStandardMountedMultiplier", Settings.Instance.TroopIsStandardMountedMultiplier)

                    .SetPropertyValue("troopTier0RecruitCost", Settings.Instance.TroopTier0RecruitCost)
                    .SetPropertyValue("troopTier1RecruitCost", Settings.Instance.TroopTier1RecruitCost)
                    .SetPropertyValue("troopTier2RecruitCost", Settings.Instance.TroopTier2RecruitCost)
                    .SetPropertyValue("troopTier3RecruitCost", Settings.Instance.TroopTier3RecruitCost)
                    .SetPropertyValue("troopTier4RecruitCost", Settings.Instance.TroopTier4RecruitCost)
                    .SetPropertyValue("troopTier5RecruitCost", Settings.Instance.TroopTier5RecruitCost)
                    .SetPropertyValue("troopTier6RecruitCost", Settings.Instance.TroopTier6RecruitCost)
                    .SetPropertyValue("troopTier7RecruitCost", Settings.Instance.TroopTier7RecruitCost)
                    .SetPropertyValue("troopTierRestRecruitCost", Settings.Instance.TroopTierRestRecruitCost)



                    .SetPropertyValue("troopTier5AndLessHorseCost", Settings.Instance.TroopTier5AndLessHorseCost)
                    .SetPropertyValue("troopTier6AndMoreHorseCost", Settings.Instance.TroopTier6AndMoreHorseCost)

                    .SetPropertyValue("mercenaryTroopRecruitMultiplier", Settings.Instance.MercenaryTroopRecruitMultiplier)
                    .SetPropertyValue("banditTroopRecruitMultiplier", Settings.Instance.BanditTroopRecruitMultiplier)
                    .SetPropertyValue("caravanGuardTroopRecruitMultiplier", Settings.Instance.CaravanGuardTroopRecruitMultiplier)

                    .SetPropertyValue("daysBetweenWagePayment", Settings.Instance.DaysBetweenWagePayment)

                    .SetPropertyValue("StandardTroopUpgradeMult", Settings.Instance.StandardTroopUpgradeMult)
                    .SetPropertyValue("NobleTroopUpgradeMult", Settings.Instance.NobleTroopUpgradeMult);

            void BuildWageMultiplierSettings(ISettingsPropertyGroupBuilder builder)
                => builder

                .AddFloatingInteger("troopTier0WageMultiplier", "{=ctw_tier0_wage_mult}Tier 0 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => TroopTier0WageMultiplier, o => TroopTier0WageMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_tier0_wage_mult_desc}Multiplies and rounds up the wage of the troop. Native cost is 1")
                    .SetOrder(order++))
                .AddFloatingInteger("troopTier1WageMultiplier", "{=ctw_tier1_wage_mult}Tier 1 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => TroopTier1WageMultiplier, o => TroopTier1WageMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_tier1_wage_mult_desc}Multiplies and rounds up the wage of the troop. Native cost is 2")
                    .SetOrder(order++))
                .AddFloatingInteger("troopTier2WageMultiplier", "{=ctw_tier2_wage_mult}Tier 2 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => TroopTier2WageMultiplier, o => TroopTier2WageMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_tier2_wage_mult_desc}Multiplies and rounds up the wage of the troop. Native cost is 3")
                    .SetOrder(order++))
                .AddFloatingInteger("troopTier3WageMultiplier", "{=ctw_tier3_wage_mult}Tier 3 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => TroopTier3WageMultiplier, o => TroopTier3WageMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_tier3_wage_mult_desc}Multiplies and rounds up the wage of the troop. Native cost is 5")
                    .SetOrder(order++))
                .AddFloatingInteger("troopTier4WageMultiplier", "{=ctw_tier4_wage_mult}Tier 4 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => TroopTier4WageMultiplier, o => TroopTier4WageMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_tier4_wage_mult_desc}Multiplies and rounds up the wage of the troop. Native cost is 8")
                    .SetOrder(order++))
                .AddFloatingInteger("troopTier5WageMultiplier", "{=ctw_tier5_wage_mult}Tier 5 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => TroopTier5WageMultiplier, o => TroopTier5WageMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_tier5_wage_mult_desc}Multiplies and rounds up the wage of the troop. Native cost is 12")
                    .SetOrder(order++))
                .AddFloatingInteger("troopTier6WageMultiplier", "{=ctw_tier6_wage_mult}Tier 6 troop wage multiplier", 0, 1000, new ProxyRef<float>(() => TroopTier6WageMultiplier, o => TroopTier6WageMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_tier6_wage_mult_desc} and rounds up the wage of the troop. Native cost is 17")
                    .SetOrder(order++))
                .AddFloatingInteger("troopTier7WageMultiplier", "{=ctw_tier7_wage_mult}Tier 7 and higher troop wage multiplier", 0, 1000, new ProxyRef<float>(() => TroopTier7WageMultiplier, o => TroopTier7WageMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_tier7_wage_mult_desc}Multiplies and rounds up the wage of the troop. Native cost is 23")
                    .SetOrder(order++))
                .SetGroupOrder(1);
            void BuildSpecialWageMultiplierSettings(ISettingsPropertyGroupBuilder builder)
                => builder
                .AddFloatingInteger("companionWageMultiplier", "{=ctw_comp_wage_mult}Companion wage multiplier", 0, 1000, new ProxyRef<float>(() => CompanionWageMultiplier, o => CompanionWageMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_comp_wage_mult_desc}Multiplies and rounds up the wage of the companion. Native calculation is 2 + 2 * companion_level"))
                .AddFloatingInteger("troopIsNobleOnFootMultiplier", "{=ctw_noble_on_foot_wage_mult}Noble troop on foot multiplier", 0, 1000, new ProxyRef<float>(() => TroopIsNobleOnFootMultiplier, o => TroopIsNobleOnFootMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_noble_on_foot_wage_mult_desc}Additionally multiplies wages of a troop if they are noble and on foot, Native is 1"))
                .AddFloatingInteger("troopIsNobleMountedMultiplier", "{=ctw_noble_mounted_mult}Noble Mounted troop multiplier", 0, 1000, new ProxyRef<float>(() => TroopIsNobleMountedMultiplier, o => TroopIsNobleMountedMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_noble_mounted_mult_desc}Additionally multiplies wages of a troop if they are noble and mounted, Native is 1"))
                .AddFloatingInteger("troopIsStandardMountedMultiplier", "{=ctw_standard_mounted_wage_mult}Standard Mounted troop multiplier", 0, 1000, new ProxyRef<float>(() => TroopIsStandardMountedMultiplier, o => TroopIsStandardMountedMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_standard_mounted_wage_mult_desc}Additionally multiplies wages of a troop if they are NOT noble but they are mounted, Native is 1"))
                .SetGroupOrder(2);
            void BuildRecruitCostSettings(ISettingsPropertyGroupBuilder builder)
                => builder
                 .AddInteger("troopTier0RecruitCost", "{=ctw_tier0_rec_cost}Troop tier 0 recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier0RecruitCost, o => TroopTier0RecruitCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier0_rec_cost_desc}Native is 10")
                    .SetOrder(order++))
                 .AddInteger("troopTier1RecruitCost", "{=ctw_tier1_rec_cost}Troop tier 1 recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier1RecruitCost, o => TroopTier1RecruitCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier1_rec_cost_desc}Native is 20")
                    .SetOrder(order++))
                 .AddInteger("troopTier2RecruitCost", "{=ctw_tier2_rec_cost}Troop tier 2 recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier2RecruitCost, o => TroopTier2RecruitCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier2_rec_cost_desc}Native is 50")
                    .SetOrder(order++))
                 .AddInteger("troopTier3RecruitCost", "{=ctw_tier3_rec_cost}Troop tier 3 recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier3RecruitCost, o => TroopTier3RecruitCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier3_rec_cost_desc}Native is 100")
                    .SetOrder(order++))
                 .AddInteger("troopTier4RecruitCost", "{=ctw_tier4_rec_cost}Troop tier 4 recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier4RecruitCost, o => TroopTier4RecruitCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier4_rec_cost_desc}Native is 200")
                    .SetOrder(order++))
                 .AddInteger("troopTier5RecruitCost", "{=ctw_tier5_rec_cost}Troop tier 5 recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier5RecruitCost, o => TroopTier5RecruitCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier5_rec_cost_desc}Native is 400")
                    .SetOrder(order++))
                 .AddInteger("troopTier6RecruitCost", "{=ctw_tier6_rec_cost}Troop tier 6 recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier6RecruitCost, o => TroopTier6RecruitCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier6_rec_cost_desc}Native is 600")
                    .SetOrder(order++))
                 .AddInteger("troopTier7RecruitCost", "{=ctw_tier7_rec_cost}Troop tier 7 recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier7RecruitCost, o => TroopTier7RecruitCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier7_rec_cost_desc}Native is 1000")
                    .SetOrder(order++))
                 .AddInteger("troopTierRestRecruitCost", "{=ctw_other_rec_cost}Other category troop recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTierRestRecruitCost, o => TroopTierRestRecruitCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_other_rec_cost_desc}Native is 1500")
                    .SetOrder(order++))
                 .SetGroupOrder(3);
            void BuildHorsesCostSettings(ISettingsPropertyGroupBuilder builder)
                => builder
                 .AddInteger("troopTier5AndLessHorseCost", "{=ctw_tier5_less_horse_rec_cost}Troop tier 5 or less horse recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier5AndLessHorseCost, o => TroopTier5AndLessHorseCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier5_less_horse_rec_cost_desc}How much of the recruitment cost should the horse add. Native is 150")
                    .SetOrder(order++))
                 .AddInteger("troopTier6AndMoreHorseCost", "{=ctw_tier6_more_horse_rec_cost}Other category troop horse recruitment cost", 1, 10000, new ProxyRef<int>(() => TroopTier6AndMoreHorseCost, o => TroopTier6AndMoreHorseCost = o), integerBuilder => integerBuilder
                    .SetHintText("{=ctw_tier6_more_horse_rec_cost_desc}How much of the recruitment cost should the horse add. Native is 500")
                    .SetOrder(order++))
                 .SetGroupOrder(4);

            void BuildSpecialTroopRecruitmentSettings(ISettingsPropertyGroupBuilder builder)
                => builder
                .AddFloatingInteger("mercenaryTroopRecruitMultiplier", "{=ctw_mercenary_rec_mult}Mercenary troop recruitment multiplier", 0, 1000, new ProxyRef<float>(() => MercenaryTroopRecruitMultiplier, o => MercenaryTroopRecruitMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_mercenary_rec_mult_desc}Multiplies and rounds up the recruitment cost of the mercenary troops. Native is 2")
                    .SetOrder(order++))
                .AddFloatingInteger("banditTroopRecruitMultiplier", "{=ctw_bandit_rec_mult}Bandit troop recruitment multiplier", 0, 1000, new ProxyRef<float>(() => BanditTroopRecruitMultiplier, o => BanditTroopRecruitMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_bandit_rec_desc}Multiplies and rounds up the recruitment cost of the bandit troops. Native is 2")
                    .SetOrder(order++))
                .AddFloatingInteger("caravanGuardTroopRecruitMultiplier", "{=ctw_caravan_rec_mult}Caravan guard troop recruitment multiplier", 0, 1000, new ProxyRef<float>(() => CaravanGuardTroopRecruitMultiplier, o => CaravanGuardTroopRecruitMultiplier = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_caravan_rec_mult_desc}Multiplies and rounds up the recruitment cost of the Caravan guard troops. Native is 2")
                    .SetOrder(order++))
                .SetGroupOrder(5);

            void BuildTimeBeforeNextWagePaymentSettings(ISettingsPropertyGroupBuilder builder)
                => builder
                 .AddInteger("daysBetweenWagePayment", "{=days_before_wage_payment}Days before next wage payment", 1, 100, new ProxyRef<int>(() => DaysBetweenWagePayment, o => DaysBetweenWagePayment = o), integerBuilder => integerBuilder
                    .SetHintText("{=days_before_wage_payment_desc}How many days should pass before next clan wage payment. Native is 1"))
                 .SetGroupOrder(6);

            void BuildUpgradeCostMultiplierSettings(ISettingsPropertyGroupBuilder builder)
                => builder
                .AddFloatingInteger("StandardTroopUpgradeMult", "{=ctw_standard_upgrade_mult}Standard troop upgrade multiplier", 0, 1000, new ProxyRef<float>(() => StandardTroopUpgradeMult, o => StandardTroopUpgradeMult = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_standard_upgrade_mult_desc}Multiplies and rounds up cost of the troop upgrade if next tier troop is standard"))
                .AddFloatingInteger("NobleTroopUpgradeMult", "{=ctw_noble_upgrade_mult}Noble troop upgrade multiplier", 0, 1000, new ProxyRef<float>(() => NobleTroopUpgradeMult, o => NobleTroopUpgradeMult = o), floatingBuilder => floatingBuilder
                    .SetHintText("{=ctw_noble_upgrade_mult_desc}Multiplies and rounds up cost of the troop upgrade if next tier troop is noble"))
                .SetGroupOrder(7);
        }
    }
}