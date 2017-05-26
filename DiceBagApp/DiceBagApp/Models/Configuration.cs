using SQLite;

namespace DiceBagApp.Models
{
    class Configuration
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int LastRoon { get; set; }
        public int LastBag { get; set; }
        public string Color { get; set; }

    }
}
