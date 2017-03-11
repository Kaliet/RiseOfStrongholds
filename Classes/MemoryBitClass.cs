using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class MemoryBitClass
    {
        /*VARIABLES*/
        private Guid m_IDOfSomething; //remembers ID of something
        private ConstantClass.CHARACTER_ACTIONS m_ActionToDo; //action related to ID
        private GameTimeClass m_dateOfLastOccurrence; //when did event occur
        private int m_numberOfTimesOccurred; //how many times occurred , more times --> goes to long term memory
        private GameTimeClass m_dateMemoryWillBeLost; //date when memory will be lost if occurrence does not increase with the period

        /*GET & SET*/
        public GameTimeClass getDateMemoryWillBeLost() { return m_dateMemoryWillBeLost; }
        
        /*CONSTRUCTOR*/
        public MemoryBitClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public MemoryBitClass(Guid ID, ConstantClass.CHARACTER_ACTIONS action, GameTimeClass date)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_IDOfSomething = ID;
            m_ActionToDo = action;
            m_dateOfLastOccurrence = date;
            m_numberOfTimesOccurred = 1;

            m_dateMemoryWillBeLost = new GameTimeClass(m_dateOfLastOccurrence);
            m_dateMemoryWillBeLost.set_days(ConstantClass.CHARACTER_MEMORY_EXPIRATION_DAYS_DURATION);


            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        public string printMemoryBit()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return "Date Occurred|" + m_dateOfLastOccurrence.ToString() + "|ActionDone|" + m_ActionToDo.ToString() + "|ID|" + m_IDOfSomething.ToString() + "|NumberOccurrence|" + m_numberOfTimesOccurred;
        }

        public void updateMemoryBit(GameTimeClass newDateOfOccurence) //updates the memory last date of occurence
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_dateOfLastOccurrence = newDateOfOccurence;
            m_dateMemoryWillBeLost = m_dateOfLastOccurrence;
            m_dateMemoryWillBeLost.set_days(ConstantClass.CHARACTER_MEMORY_EXPIRATION_DAYS_DURATION);
            m_numberOfTimesOccurred++;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH 
        }

        /*OVERRIDE*/
        public override bool Equals(object obj)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (obj == null || GetType() != obj.GetType())
                return false;

            MemoryBitClass memory = obj as MemoryBitClass;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (memory.m_ActionToDo == this.m_ActionToDo &&
                    memory.m_IDOfSomething == this.m_IDOfSomething);                    
        }
    }
}
