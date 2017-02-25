using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class InventoryClass 
    {
        /*VARIABLES*/
        private List<GenericObjectClass> m_InventoryList;        

        /*GET & SET*/
        public int getInventorySize() { return m_InventoryList.Count; }        

        /*CONSTRUCTORS*/
        public InventoryClass(int maxCap)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_InventoryList = new List<GenericObjectClass>();
            m_InventoryList.Capacity = maxCap;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        public bool isEmpty()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (m_InventoryList.Count == 0);
        }

        public bool existsInInventory(GenericObjectClass item) //returns true if item exists in inventory, otherwise false
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                foreach (GenericObjectClass obj in m_InventoryList)
                {
                    if (obj == item) return true;
                }

                if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH                
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }
            return false;
        }

        public bool addItemToInventory(GenericObjectClass item, int quantity)
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {

                if (m_InventoryList.Count >= m_InventoryList.Capacity) return false; //returns false , unable to add another Item

                
                bool found = false;
                //if item exists in inventory, then update item
                //  genericObject.incrementValue(quantity);    
                foreach (GenericObjectClass obj in m_InventoryList)
                {                  
                    if (obj == item) //if item is inside the inventory
                    {
                        obj.incrementValue(quantity);
                        found = true;
                    }
                }
                if (!found) { m_InventoryList.Add(item); }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            
            return true;
        }

        public bool checkIfOnlyOneItemType() //checks if there is only 1 item in the inventory , relevant for block inventories that have resources only
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (m_InventoryList.Count == 1) return true;
            return false;
        }

        public int returnQuantityOfItemBasedOnIndex(int index) //returns quantity of the single item in the inventory
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (isEmpty()) return -1;
            if (m_InventoryList.Count < index) return -1;

            return (m_InventoryList[index].getQuantity());            
        }

        public bool removeItemFromInventory(GenericObjectClass item, int quantity)
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                if (m_InventoryList.Count <= 0) return false; //returns false , unable to remove any items

                bool found = false;
                //if item exists in inventory, then update item
                //  genericObject.incrementValue(quantity);    
                foreach (GenericObjectClass obj in m_InventoryList)
                {                    
                    if (obj == item) //if item is inside the inventory
                    {                        
                        obj.decrementValue(quantity);
                        found = true;
                    }                        
                }
                if (!found) { m_InventoryList.Remove(item); }                
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return true;
        }

        public string printInventoryList()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string output = "";

            output += "Inventory Status:\n";

            foreach (GenericObjectClass obj in m_InventoryList)
            {
                output += "\t\t\t\t\t *" + (obj.ToString() + "\n");                
            }

            //output += "------------------------------------------------------\n";
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            return output;
        }

    }
}
