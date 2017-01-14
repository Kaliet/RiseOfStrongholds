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
        public List<T> getQueue() { return m_queue; }

        /*CONSTRUCTOR*/
        public QueueClass()
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->QueueClass()"); };

            m_queue = new List<T>();

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-QueueClass()"); };
        }

        /*METHODS*/
        public string printQueue() //prints everything in queue
        {
            //return string should be [0] - info, [1] - info, etc.            
            string output = "";
            int index = 0;
            foreach (T element in m_queue)
            {
                output = "[" + index + "/" + (m_queue.Count - 1) + "] - " + element.ToString() + " ";
            }
            return output;
        }
    }
}
