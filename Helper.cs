using System.Collections.Generic;
using TaleWorlds.CampaignSystem;

namespace CustomizableTroopWages
{
    public static class Helper
    {
        public enum TroopType
        {
            StandardOnFoot,
            StandardMounted,
            NobleOnFoot,
            NobleMounted
        }
        public static TroopType GetTroopType(this CharacterObject character)
        {
            if (character.IsNoble())
            {
                if (character.IsMounted)
                    return TroopType.NobleMounted;
                else
                    return TroopType.NobleOnFoot;
            }
            else
            {
                if (character.IsMounted)
                    return TroopType.StandardMounted;
                else
                    return TroopType.StandardOnFoot;
            }
        }
        public static bool IsNoble(this CharacterObject character)
        {
            if (!character.Culture.IsMainCulture)
                return false;

            Stack<CharacterObject> troops_stack = new();
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
    }
}