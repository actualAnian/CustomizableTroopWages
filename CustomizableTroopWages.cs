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

        public static float ChooseMultiplier(CharacterObject character)
        {
            float multiplier = 1;

            if (!_isPlayerRelated)
                return multiplier;

            multiplier = character.Tier switch
            {
                0 => Settings.Instance.TroopTier0WageMultiplier,
                1 => Settings.Instance.TroopTier1WageMultiplier,
                2 => Settings.Instance.TroopTier2WageMultiplier,
                3 => Settings.Instance.TroopTier3WageMultiplier,
                4 => Settings.Instance.TroopTier4WageMultiplier,
                5 => Settings.Instance.TroopTier5WageMultiplier,
                6 => Settings.Instance.TroopTier6WageMultiplier,
                _ => Settings.Instance.TroopTier7WageMultiplier,
            };
            return character.GetTroopType() switch
            {
                Helper.TroopType.StandardOnFoot => multiplier,
                Helper.TroopType.StandardMounted => multiplier * Settings.Instance.TroopIsStandardMountedMultiplier,
                Helper.TroopType.NobleOnFoot => multiplier * Settings.Instance.TroopIsNobleOnFootMultiplier,
                Helper.TroopType.NobleMounted => multiplier * Settings.Instance.TroopIsNobleMountedMultiplier,
                _ => multiplier,
            };
        }
        public static double AddCompanionMultiplier(CharacterObject character)
        {
            if (character.HeroObject != null && character.HeroObject.Clan == Clan.PlayerClan)
                return Settings.Instance.CompanionWageMultiplier;
            return 1;
        }
    }
}