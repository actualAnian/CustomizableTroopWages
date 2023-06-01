using TaleWorlds.CampaignSystem;

namespace CustomizableTroopWages
{
    //this class defines how many days have to pass before you pay troop wages 
    // takes after CampaignBehaviorBase, in order to have SyncData method
    public class CustomizableWagesPayment : CampaignBehaviorBase
    {
        public override void RegisterEvents() { }

        private static bool _dailyTickPlayerState;

        public static int daysSinceWage;

        public CustomizableWagesPayment()
        {
            daysSinceWage = 0;
            _dailyTickPlayerState = false;
        }
        public static void SetDailyTickPlayerState(Clan clan)
        {
            if (clan == Clan.PlayerClan)
                _dailyTickPlayerState = true;
        }
        public static int AddDay(Clan current_clan)
        {

            if (!(current_clan == Clan.PlayerClan))
                return 0;

            int _tempDaysSinceWage = daysSinceWage + 1;
            if (_tempDaysSinceWage >= Settings.Instance.DaysBetweenWagePayment)
                _tempDaysSinceWage = 0;

            if (!_dailyTickPlayerState)
            {
                return _tempDaysSinceWage;
            }
            _dailyTickPlayerState = false;
            daysSinceWage = _tempDaysSinceWage;
            return daysSinceWage;
        }
        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("customizable_wages_payment", ref daysSinceWage);
        }
    }
}
