using System.Diagnostics;
using System.Runtime;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace CustomizableTroopWages
{
    //this class defines how much does the recruitment cost
    public class CustomizableTroopCost
    {

        public static int ChooseTroopCost(CharacterObject troop, Hero buyerHero, bool withoutItemCost = false)
        {
            int _recruitCost = 1;

            if (troop.Level <= 1)
            {
                _recruitCost = Settings.Instance.troopTier0RecruitCost;
            }
            else if (troop.Level <= 6)
            {
                _recruitCost = Settings.Instance.troopTier1RecruitCost;
            }
            else if (troop.Level <= 11)
            {
                _recruitCost = Settings.Instance.troopTier2RecruitCost;
            }
            else if (troop.Level <= 16)
            {
                _recruitCost = Settings.Instance.troopTier3RecruitCost;
            }
            else if (troop.Level <= 21)
            {
                _recruitCost = Settings.Instance.troopTier4RecruitCost;
            }
            else if (troop.Level <= 26)
            {
                _recruitCost = Settings.Instance.troopTier5RecruitCost;
            }
            else if (troop.Level <= 31)
            {
                _recruitCost = Settings.Instance.troopTier6RecruitCost;
            }
            else if (troop.Level <= 36)
            {
                _recruitCost = Settings.Instance.troopTier7RecruitCost;
            }
            else
            {
                _recruitCost = Settings.Instance.troopTierRestRecruitCost;
            }
            if (troop.Equipment.Horse.Item != null && !withoutItemCost)
            {
                if (troop.Level < 26)
                {
                    _recruitCost += Settings.Instance.troopTier5AndLessHorseCost;
                }
                else
                {
                    _recruitCost += Settings.Instance.troopTier6AndMoreHorseCost;
                }
            }
            if (troop.Occupation == Occupation.Mercenary)
                _recruitCost = MathF.Round((float)_recruitCost * Settings.Instance.mercenaryTroopRecruitMultiplier);

            if (troop.Occupation == Occupation.Gangster)
                _recruitCost = MathF.Round((float)_recruitCost * Settings.Instance.banditTroopRecruitMultiplier);

            if (troop.Occupation == Occupation.CaravanGuard)
                _recruitCost = MathF.Round((float)_recruitCost * Settings.Instance.caravanGuardTroopRecruitMultiplier);

            return _recruitCost;
        }
        public static bool IsPlayerClan(Hero hero)
        {
            if (hero != null && hero.Clan == Clan.PlayerClan)
                return true;
            return false;
        }
    }
}