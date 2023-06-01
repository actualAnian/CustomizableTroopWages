using System.Collections.Generic;
using System.Runtime;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace CustomizableTroopWages
{
    //this class defines how much you have to pay for troops
    public class CustomizableTroopWages
    {

        private static bool _isPlayerRelated = false;
        private static bool _dontSwitchPlayerRelated;

        internal static void SetdontSwitchPlayerRelatedFalse()
        {
            _dontSwitchPlayerRelated = false;
        }
        internal static void IsPlayerRelated(PartyBase party)
        {
            if (party.Owner != null && party.Owner.Clan == Clan.PlayerClan)
            {
                _isPlayerRelated = true;
                _dontSwitchPlayerRelated = true;
            }
        }
        internal static void IsPlayerRelated(MobileParty party)
        {
            if (party.Owner != null && party.Owner.Clan == Clan.PlayerClan)
                _isPlayerRelated = true;
        }
        internal static void IsPlayerRelated(Settlement settlement)
        {
            if (settlement.OwnerClan == Clan.PlayerClan)
                _isPlayerRelated = true;
        }
        public static void SetIsPlayerRelatedTrue()
        {
            _isPlayerRelated = true;
        }

        public static void SetIsPlayerRelatedFalse()
        {
            if (!_dontSwitchPlayerRelated)
                _isPlayerRelated = false;
        }
        private static bool IsNoble(CharacterObject character)
        {
            if (!character.Culture.IsMainCulture)
                return false;

            Stack<CharacterObject> troops_stack = new Stack<CharacterObject>();
            troops_stack.Push(character.Culture.EliteBasicTroop);
            while (troops_stack.Count != 0)
            {
                CharacterObject cur_troop = troops_stack.Pop();
                if (cur_troop.Tier < character.Tier)
                {
                    CharacterObject[] upgrade_array = cur_troop.UpgradeTargets;
                    foreach (var troop_char in upgrade_array)
                        troops_stack.Push(troop_char);

                }
                else if (cur_troop.Tier == character.Tier)
                    if (cur_troop.StringId == character.StringId)
                        return true;
            }
            return false;
        }

        public static float ChooseMultiplier(CharacterObject character)
        {
            float multiplier = 1;

            if (!_isPlayerRelated)
                return multiplier;

            switch (character.Tier)
            {
                case 0:
                    multiplier = Settings.Instance.TroopTier0WageMultiplier;
                    break;
                case 1:
                    multiplier = Settings.Instance.TroopTier1WageMultiplier;
                    break;
                case 2:
                    multiplier = Settings.Instance.TroopTier2WageMultiplier;
                    break;
                case 3:
                    multiplier = Settings.Instance.TroopTier3WageMultiplier;
                    break;
                case 4:
                    multiplier = Settings.Instance.TroopTier4WageMultiplier;
                    break;
                case 5:
                    multiplier = Settings.Instance.TroopTier5WageMultiplier;
                    break;
                case 6:
                    multiplier = Settings.Instance.TroopTier6WageMultiplier;
                    break;
                default:
                    multiplier = Settings.Instance.TroopTier7WageMultiplier;
                    break;
            }


            if (IsNoble(character))
            {
                if (character.IsMounted)
                    return multiplier * Settings.Instance.TroopIsNobleMountedMultiplier;
                else
                    return multiplier * Settings.Instance.TroopIsNobleOnFootMultiplier;
            }
            else
            {
                if (character.IsMounted)
                    return multiplier * Settings.Instance.TroopIsStandardMountedMultiplier;
            }
            return multiplier;
        }
        public static double AddCompanionMultiplier(CharacterObject character)
        {
            if (character.HeroObject != null && character.HeroObject.Clan == Clan.PlayerClan)
                return Settings.Instance.CompanionWageMultiplier;
            return 1;
        }
    }
}