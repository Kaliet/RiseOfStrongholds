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
        public int getInventorySize() { return m_InventoryList.Count; }        

        /*CONSTRUCTORS*/
        public InventoryClass(int maxCap)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_InventoryList = new List<T>();
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

        public bool existsInInventory(object item) //returns true if item exists in inventory, otherwise false
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                foreach (object obj in m_InventoryList)
                {
                    if (typeof(GenericObjectClass).IsAssignableFrom(typeof(T))) //check if T is of class generic object class
                    {
                        if (((GenericObjectClass)obj) == ((GenericObjectClass)item))
                        {
                            return true;
                        }
                    }
                }

                if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH                
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }
            return false;
        }

        public bool addItemToInventory(object item, int quantity)
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {

                if (m_InventoryList.Count >= m_InventoryList.Capacity) return false; //returns false , unable to add another Item

                if (item is T) //check if item type matches inventory type
                {
                    bool found = false;
                    //if item exists in inventory, then update item
                    //  genericObject.incrementValue(quantity);    
                    foreach (object obj in m_InventoryList)
                    {
                        if (typeof(GenericObjectClass).IsAssignableFrom(typeof(T))) //check if T is of class generic object class
                        {
                            if (((GenericObjectClass)obj) == ((GenericObjectClass)item)) //if item is inside the inventory
                            {
                                ((GenericObjectClass)obj).incrementValue(quantity);
                                found = true;
                            }
                            else
                            {
                                //not supported other types yet
                            }
                        }
                    }
                    if (!found) { m_InventoryList.Add((T)item); }
                }
                else //if item type is wrong type, then cannot insert to inventory
                {
                    ConstantClass.LOGGER.writeToInventoryLog("Unable to insert item to inventory. Wrong type.");
                }
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

            if (m_InventoryList.Count == 1) return true;
            return false;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public int returnQuantityOfItemBasedOnIndex(int index) //returns quantity of the single item in the inventory
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (isEmpty()) return -1;
            if (m_InventoryList.Count < index) return -1;

            if (typeof(GenericObjectClass).IsAssignableFrom(typeof(T))) //check if T is of class generic object class
            {
                GenericObjectClass obj = m_InventoryList[index];
            }

            return -1;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public bool removeItemFromInventory(object item, int quantity)
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {

                if (m_InventoryList.Count <= 0) return false; //returns false , unable to remove any items

                if (item is T) //check if item type matches inventory type
                {
                    bool found = false;
                    //if item exists in inventory, then update item
                    //  genericObject.incrementValue(quantity);    
                    foreach (object obj in m_InventoryList)
                    {
                        if (typeof(GenericObjectClass).IsAssignableFrom(typeof(T))) //check if T is of class generic object class
                        {
                            if (((GenericObjectClass)obj) == ((GenericObjectClass)item)) //if item is inside the inventory
                            {
                                ((GenericObjectClass)obj).decrementValue(quantity);
                                found = true;
                            }
                            else
                            {
                                //not supported other types yet
                            }
                        }
                    }
                    if (!found) { m_InventoryList.Remove((T)item); }
                }
                else //if item type is wrong type, then cannot insert to inventory
                {
                    ConstantClass.LOGGER.writeToInventoryLog("Unable to insert item to inventory. Wrong type.");
                }
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

            foreach (object obj in m_InventoryList)
            {
                if (typeof(GenericObjectClass).IsAssignableFrom(typeof(T))) //check if T is of class generic object class
                {
                    output += "\t\t\t\t\t *" + ((GenericObjectClass)obj).ToString() + "\n";
                }
            }

            //output += "------------------------------------------------------";
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            return output;
        }

    }
}
