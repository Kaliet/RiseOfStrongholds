using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class ActionClass //actions to perform by characters
    {
        /*VARIABLES*/
        private ConstantClass.CHARACTER_ACTIONS m_action;
        private int m_priority; //0 = highest, 1, 2....lower

        /*GET & SET*/
        public ConstantClass.CHARACTER_ACTIONS getAction() { return m_action; }
        public int getPriority() { return m_priority; }

        public void setAction(ConstantClass.CHARACTER_ACTIONS action) { m_action = action; }
        public void setPriority(int value) { m_priority = value; }

        /*CONSTRUCTORS*/
        public ActionClass()
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->ActionClass()"); };
           
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-ActionClass()"); };
        }

        public ActionClass(ConstantClass.CHARACTER_ACTIONS action)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->ActionClass()"); };

            m_action = action;
            m_priority = -1; //undefined

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-ActionClass()"); };
        }

        public ActionClass(ConstantClass.CHARACTER_ACTIONS action, int priority)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->ActionClass()"); };

            m_action = action;
            m_priority = priority;

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-ActionClass()"); };
        }

        /*METHODS*/

    }
}
