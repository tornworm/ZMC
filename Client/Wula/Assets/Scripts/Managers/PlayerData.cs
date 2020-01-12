using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    public class PlayerData
    {
        private static PlayerData instance;
        private PlayerData() {
            actorList = new List<Actor>();
        }
        public static PlayerData Instance
        {
            get{
                if (instance==null)
                {
                    instance = new PlayerData();                   
                }
                return instance;
            }
        }
        public List<Actor> actorList;
        public int UserID { get; set; }
        public string Name { get; set; }
        public string gender { get; set; }
        public int roleId { get; set; }
        public int Lv { get; set; }
    }
}
