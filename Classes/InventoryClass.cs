using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class InventoryClass<T> //T can be an object of objectclass
    {
        /*VARIABLES*/
        private List<T> m_InventoryList;        

        /*GET & SET*/
        public List<T> getInventoryList() { return m_InventoryList; }        

        /*CONSTRUCTORS*/
        public InventoryClass(int maxCap)
        {
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->InventoryClass()"); };

            m_InventoryList = new List<T>();
            m_InventoryList.Capacity = maxCap;

            /*DEBUG HIGH*/ (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-InventoryClass()"); };
        }
    }
}
