using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopDiceRoller
{
    [Table("RollItem")]
    public class RollItem
    {
        [PrimaryKey, AutoIncrement]
        public int RollID { get; set; }
        public string RollName { get; set; }
        public string RollDice { get; set; }
        [AutoIncrement]
        public int RollPosition { get; set; }
        public bool CanCrit { get; set; }
        public bool CanSave { get; set; }

        [OneToMany]
        public List<HasLevels> Levels { get; set; }

        [ForeignKey(typeof(Folder))]
        public int FolderID { get; set; }
    }
}
