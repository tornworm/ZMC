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
        private PlayerData() { }
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

        public int UserID { get; set; }
        public int Name { get; set; }
    }
}
