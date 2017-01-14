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
        private int m_var_for_action;

        /*GET & SET*/
        public ConstantClass.CHARACTER_ACTIONS getAction() { return m_action; }
        public int getPriority() { return m_priority; }
        public int getVarForAction() { return m_var_for_action; }

        public void setAction(ConstantClass.CHARACTER_ACTIONS action) { m_action = action; }
        public void setPriority(int value) { m_priority = value; }
        public void setVarForAction (int value) { m_var_for_action = value; }
        public void modifyVarForAction (int value) { m_var_for_action += value; }

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
            m_var_for_action = -1; //undefined

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-ActionClass()"); };
        }

        public ActionClass(ConstantClass.CHARACTER_ACTIONS action, int priority, int varForAction)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->ActionClass()"); };

            m_action = action;
            m_priority = priority;
            m_var_for_action = varForAction;

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-ActionClass()"); };
        }

        /*OVERRIDES*/
        public override string ToString()
        {
            return "A: " + m_action.ToString() + "\tP: " + m_priority + "\tV: " + m_var_for_action;
        }
    }
}
