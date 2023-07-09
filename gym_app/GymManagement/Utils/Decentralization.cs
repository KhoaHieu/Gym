using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Utils
{
    public class Decentralization
    {
        private static readonly Decentralization _instances = new Decentralization();
        private int _roleId;
        public int RoleId
        {
            get
            {
                return _roleId;
            }
            set 
            { 
                _roleId= value; 
            }
        }
        public Decentralization()
        {
            
        }
        public static Decentralization GetDecentralization()
        {
            return _instances;
        }
       
        
        
    }
}
