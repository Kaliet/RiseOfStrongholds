using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class MemoryClass
    {
        /*VARIABLES*/
        private List<MemoryBitClass> m_ShortTermMemory;
        private List<MemoryBitClass> m_LongTermMemory;
        private int m_MemorySize;


        /*GET & SET*/
        
        /*CONSTRUCTOR*/
        public MemoryClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_ShortTermMemory = new List<Classes.MemoryBitClass>();
            m_LongTermMemory = new List<MemoryBitClass>();
            m_MemorySize = 0;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public MemoryClass(int size)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_MemorySize = size;
            m_ShortTermMemory = new List<MemoryBitClass>(size);
            m_LongTermMemory = new List<MemoryBitClass>(size);

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/

       public bool isMemoryFull (ConstantClass.MEMORY type)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (type == ConstantClass.MEMORY.LONG) { return m_LongTermMemory.Count == m_LongTermMemory.Capacity; }
            else if (type == ConstantClass.MEMORY.SHORT) { return m_ShortTermMemory.Count == m_ShortTermMemory.Capacity; }
            else
            {
                throw new Exception("Invalid memory access.");
            }
        }

        public bool ifExistsInMemory (MemoryBitClass memoryBit, ConstantClass.MEMORY type) //checks if memorybit exists in short/long term memory
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (type == ConstantClass.MEMORY.LONG || type == ConstantClass.MEMORY.BOTH)
            {
                foreach (MemoryBitClass bit in m_LongTermMemory)
                {
                    if (bit == memoryBit) //memory found
                    {
                        return true;
                    }
                }
            }
            if (type == ConstantClass.MEMORY.SHORT || type == ConstantClass.MEMORY.BOTH)
            {
                foreach (MemoryBitClass bit in m_ShortTermMemory)
                {
                    if (bit == memoryBit) //memory found
                    {
                        return true;
                    }
                }
            }           

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return false;
        }   

        public void moveShortTermToLongTerm(MemoryBitClass bit) //moves short memory bit to long term memory bit
        {
            //TODO:
        }

        public void updateMemory(MemoryBitClass memoryBit, ConstantClass.MEMORY type) //memory bit exists in short/long term, updates the number of occurrence and last occurred date
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            //1. goes through short/long term memory list and finds object
            //2. update object number of occurrence and date of last ocurred
            if (type == ConstantClass.MEMORY.LONG || type == ConstantClass.MEMORY.BOTH)
            {
                foreach (MemoryBitClass bit in m_LongTermMemory)
                {
                    if (bit == memoryBit)
                    {
                        bit.updateMemoryBit(ConstantClass.gameTime);
                    }
                }
            }

            if (type == ConstantClass.MEMORY.SHORT || type == ConstantClass.MEMORY.BOTH)
            {
                foreach (MemoryBitClass bit in m_ShortTermMemory)
                {
                    if (bit == memoryBit)
                    {
                        bit.updateMemoryBit(ConstantClass.gameTime);
                    }
                }
            }


            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void addMemoryToShortTerm(MemoryBitClass memoryBit) //adds a memory bit to short memory. cannot add directly to long term memory
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH            

            //1. check if memory is already in short/long term
            if (ifExistsInMemory(memoryBit, ConstantClass.MEMORY.BOTH))
            {
                //2. if yes, then update existing memory: date of occurrence and number of occurrence
                updateMemory(memoryBit, ConstantClass.MEMORY.BOTH);
            }
            //3. if no, then add new memory to short term
            else
            {                
                //3.1 check if short memory is full
                if (isMemoryFull(ConstantClass.MEMORY.SHORT))
                {
                    //3.2 if full, remove oldest memory and add newest memory
                    GameTimeClass earliestDateOfMemory = m_ShortTermMemory.Min(obj => obj.getDateMemoryWillBeLost());
                    MemoryBitClass oldestMemory = m_ShortTermMemory.First(obj => obj.getDateMemoryWillBeLost() == earliestDateOfMemory);
                    m_ShortTermMemory.Remove(oldestMemory);
                    m_ShortTermMemory.Add(memoryBit);
                }
                else
                {
                    //3.3 if not full, add memory
                    m_ShortTermMemory.Add(memoryBit);
                }
            }
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }


        public void printMemory(string charID)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            ConstantClass.LOGGER.writeToCharLog("Short Term Memory Size|" + m_ShortTermMemory.Capacity, charID);
            foreach (MemoryBitClass shortMem in m_ShortTermMemory)
            {
                ConstantClass.LOGGER.writeToCharLog("Short Term Memory|" + shortMem.printMemoryBit(), charID);
            }

            ConstantClass.LOGGER.writeToCharLog("Long Term Memory Size|" + m_LongTermMemory.Capacity, charID);
            foreach (MemoryBitClass longMem in m_LongTermMemory)
            {
                ConstantClass.LOGGER.writeToCharLog("Long Term Memory|" + longMem.printMemoryBit(), charID);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*EVENTS HANDLER*/
        public void OnGameTicked(object source, EventArgs args) =-----------to do and test here
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            //TODO: checks all memory and erased expired memorybits

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
