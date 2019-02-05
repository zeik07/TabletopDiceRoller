using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TabletopDiceRoller
{
    public class RollItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; }
    }
}
