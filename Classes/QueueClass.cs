using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class QueueClass<T>
    {
        /*VARIABLES*/
        private List<T> m_queue;

        /*GET & SET*/
        public List<T> getQueue()  { return m_queue; }

        /*CONSTRUCTOR*/
        public QueueClass()
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_queue = new List<T>();

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        public void printQueue(string charID) //prints everything in queue
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            //return string should be [0] - info, [1] - info, etc.                        
            int index = 0;            

            foreach (T element in m_queue)
            {
                ConstantClass.LOGGER.writeToCharLog("Action|[" + (index + 1) + "/" + m_queue.Count + "]|" + element.ToString(), charID);
                index++;
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public string printQueue(QueueClass<ActionClass> actionQueue) //prints everything in queue
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            //return string should be [0] - info, [1] - info, etc.            
            string output = "";
            int index = 0;

            actionQueue.m_queue =  actionQueue.getQueue().OrderBy(obj => obj.getPriority()).ToList();

            foreach (ActionClass element in actionQueue.getQueue())
            {
                output += "\n\t\t\t\t\t[" + (index + 1) + "/" + actionQueue.getQueue().Count + "] - " + element.ToString();
                index++;
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return output;
        }
    }
}
