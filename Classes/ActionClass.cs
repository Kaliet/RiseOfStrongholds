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
        private Guid m_guid_for_action; //guid variable for some actions

        /*GET & SET*/
        public ConstantClass.CHARACTER_ACTIONS getAction() { return m_action; }
        public int getPriority() { return m_priority; }
        public int getVarForAction() { return m_var_for_action; }
        public Guid getGuidForAction() { return m_guid_for_action; }

        public void setAction(ConstantClass.CHARACTER_ACTIONS action) { m_action = action; }
        public void setPriority(int value) { m_priority = value; }
        public void setVarForAction (int value) { m_var_for_action = value; }
        public void setGuidForAction(Guid value) { m_guid_for_action = value; }
        public void modifyVarForAction (int value) { m_var_for_action += value; }

        /*CONSTRUCTORS*/
        public ActionClass()
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public ActionClass(ConstantClass.CHARACTER_ACTIONS action)
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_action = action;
            m_priority = -1; //undefined
            m_var_for_action = -1; //undefined
            m_guid_for_action = Guid.Empty;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public ActionClass(ConstantClass.CHARACTER_ACTIONS action, int priority, int varForAction, Guid guidForAction)
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_action = action;
            m_priority = priority;
            m_var_for_action = varForAction;
            m_guid_for_action = guidForAction;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*OVERRIDES*/
        public override string ToString()
        {
            return "A|" + m_action.ToString() + "|P|" + m_priority + "|V|" + m_var_for_action + "|G|" + m_guid_for_action;
        }
    }
}
