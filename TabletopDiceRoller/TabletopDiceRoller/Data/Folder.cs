using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopDiceRoller
{
    public class Folder
    {
        [PrimaryKey, AutoIncrement]
        public int FolderID { get; set; }
        public string FolderName { get; set; }
        [AutoIncrement]
        public int FolderPosition { get; set; }

        [OneToMany]
        public List<RollItem> Rolls { get; set; }

        [ForeignKey(typeof(Profile))]
        public int ProfileID { get; set; }
    }
}
