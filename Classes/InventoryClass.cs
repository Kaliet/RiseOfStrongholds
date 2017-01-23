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

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_InventoryList = new List<T>();
            m_InventoryList.Capacity = maxCap;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
