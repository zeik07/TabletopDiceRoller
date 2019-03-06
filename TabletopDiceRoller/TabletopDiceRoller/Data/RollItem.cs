using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopDiceRoller
{
    [Table("RollItems")]
    public class RollItem
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int RollID { get; set; }
        [AutoIncrement]
        public int RollPosition { get; set; }

        public string Profile { get; set; }
        public string Folder { get; set; }
        
        public string RollName { get; set; }
        public string RollDice { get; set; }
        
        public bool CanCrit { get; set; }
        public bool CanSave { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<HasLevels> Levels { get; set; }
    }
}
