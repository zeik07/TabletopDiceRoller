using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TabletopDiceRoller
{
    public class HasLevels
    {
        [PrimaryKey, AutoIncrement]
        public int LevelID { get; set; }
        public string BaseRoll { get; set; }
        public int BaseLevel { get; set; }
        public string AddToRoll { get; set; }
        public int PerLevel { get; set; }
        public int LevelCap { get; set; }

        [ForeignKey(typeof(RollItem))]
        public int RollID { get; set; }
    }
}
