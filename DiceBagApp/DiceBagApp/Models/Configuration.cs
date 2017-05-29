using SQLite;

namespace DiceBagApp.Models
{
    class Configuration
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int RoonSelected { get; set; }
        public int BagSelected { get; set; }
        
    }
}
