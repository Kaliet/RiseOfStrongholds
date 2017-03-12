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
        private int m_priorityOfMemoryBit; //how important is the memory bit
        public bool isEmpty;

        /*GET & SET*/
        public GameTimeClass getDateMemoryWillBeLost() { return m_dateMemoryWillBeLost; }
        public Guid getIDOfSomething() { return m_IDOfSomething; }

        /*CONSTRUCTOR*/
        public MemoryBitClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            isEmpty = true;
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public MemoryBitClass(Guid ID, ConstantClass.CHARACTER_ACTIONS action, GameTimeClass dateActionPerformed, int priority)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_IDOfSomething = ID;
            m_ActionToDo = action;
            m_dateOfLastOccurrence = new GameTimeClass(dateActionPerformed);
            m_priorityOfMemoryBit = priority;
            m_numberOfTimesOccurred = 1;
            isEmpty = false;

            m_dateMemoryWillBeLost = new GameTimeClass(m_dateOfLastOccurrence);
            m_dateMemoryWillBeLost.set_days(ConstantClass.CHARACTER_MEMORY_SHORT_TERM_EXPIRATION_DAYS_DURATION);


            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        public string printMemoryBit()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return "DO|" + m_dateOfLastOccurrence.ToString() + "|ACT|" + m_ActionToDo.ToString() + "|ID|" + m_IDOfSomething.ToString() + "|NO.|" + m_numberOfTimesOccurred + "|LOST|" + m_dateMemoryWillBeLost.ToString() + "|PRI|" + m_priorityOfMemoryBit;
        }

        public bool isMemoryBitExpired() //returns if memory bit is expired or not
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (ConstantClass.gameTime >= m_dateMemoryWillBeLost);
        }

        public bool isMemoryPriorityOverThreshold()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (m_priorityOfMemoryBit >= ConstantClass.CHARACTER_MEMORY_SHORT_TO_LONG_TERM_THRESHOLD);
        }

        public void updateMemoryBit(GameTimeClass newDateOfOccurence) //updates the memory last date of occurence
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_dateOfLastOccurrence = new GameTimeClass(newDateOfOccurence);
            m_dateMemoryWillBeLost = new GameTimeClass(m_dateOfLastOccurrence);
            m_dateMemoryWillBeLost.set_days(ConstantClass.CHARACTER_MEMORY_SHORT_TERM_EXPIRATION_DAYS_DURATION);
            m_numberOfTimesOccurred++;
            m_priorityOfMemoryBit++;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH 
        }

        public void reducePriorityBy(int value)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_priorityOfMemoryBit -= value;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public GameTimeClass returnDeltaLastOccuredDate() //return in Gametime format
        {
            return (new GameTimeClass(ConstantClass.gameTime - m_dateOfLastOccurrence));
        }

        public bool ifActionIsSame (MemoryBitClass obj)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (this.m_ActionToDo == obj.m_ActionToDo);
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

        public static bool operator ==(MemoryBitClass a, MemoryBitClass b)
        {
            return (a.m_ActionToDo == b.m_ActionToDo &&
                    a.m_IDOfSomething == b.m_IDOfSomething);
        }

        public static bool operator !=(MemoryBitClass a, MemoryBitClass b)
        {
            return !(a == b);
        }
    }
}
