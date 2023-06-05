using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;

namespace CustomizableTroopWages
{
    public class CustomizableTroopUpgradeCost 
    {
        public static float MultiplyUpgradeCost(PartyBase party, CharacterObject upgradeTarget)
        {
            if (!(party.Owner != null && party.Owner == Hero.MainHero)) return 1;

            if(upgradeTarget.IsNoble())
            {
                return Settings.Instance.NobleTroopUpgradeMult;
            }
            else
            {
                return Settings.Instance.StandardTroopUpgradeMult;
            }
        }
    }
}