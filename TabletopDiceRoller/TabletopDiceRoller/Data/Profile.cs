using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TabletopDiceRoller
{
    public class Profile
    {
        [PrimaryKey, AutoIncrement]
        public int ProfileID { get; set; }
        public string ProfileName { get; set; }
        [AutoIncrement]
        public int ProfilePosition { get; set; }

        [OneToMany]
        public List<Folder> Folders { get; set; }
    }
}
