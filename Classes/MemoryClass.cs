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
        private List<MemoryBitClass> m_MemoryForBlocksVisited; //memory for storing blocks visited
        private List<MemoryBitClass> m_MemoryForBlocksWithResources; //memory for storing blocks that have resources
        private List<MemoryBitClass> m_ShortTermMemory; //short term memory
        private List<MemoryBitClass> m_LongTermMemory; //long term memory
        private int m_MemorySize;


        /*GET & SET*/
        
        /*CONSTRUCTOR*/
        public MemoryClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_ShortTermMemory = new List<MemoryBitClass>();
            m_LongTermMemory = new List<MemoryBitClass>();
            m_MemoryForBlocksVisited = new List<Classes.MemoryBitClass>();
            m_MemoryForBlocksWithResources = new List<Classes.MemoryBitClass>();
            m_MemorySize = 0;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public MemoryClass(int size)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_MemorySize = size;
            m_ShortTermMemory = new List<MemoryBitClass>(size);
            m_LongTermMemory = new List<MemoryBitClass>(size);
            m_MemoryForBlocksVisited = new List<MemoryBitClass>(size);
            m_MemoryForBlocksWithResources = new List<Classes.MemoryBitClass>(size);

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/

        public bool isMemoryFull (ConstantClass.MEMORY type)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (type == ConstantClass.MEMORY.LONG) { return m_LongTermMemory.Count == m_LongTermMemory.Capacity; }
            else if (type == ConstantClass.MEMORY.SHORT) { return m_ShortTermMemory.Count == m_ShortTermMemory.Capacity; }
            else if (type == ConstantClass.MEMORY.BLOCKS) { return m_MemoryForBlocksVisited.Count == m_MemoryForBlocksVisited.Capacity; }
            else if (type == ConstantClass.MEMORY.RESOURCES) { return m_MemoryForBlocksWithResources.Count == m_MemoryForBlocksWithResources.Capacity;)}
            else
            {
                throw new Exception("Invalid memory access.");
            }
        }

        public MemoryBitClass retrieveMemoryBitIfExistsInMemory (MemoryBitClass memoryBit, ConstantClass.MEMORY type) //checks if memorybit exists (action+id) in short/long term memory
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            List<MemoryBitClass> list = new List<MemoryBitClass>();

            try
            {
                if (type == ConstantClass.MEMORY.LONG || type == ConstantClass.MEMORY.SHORT_AND_LONG)
                {
                    foreach (MemoryBitClass bit in m_LongTermMemory)
                    {
                        if (bit == memoryBit) //memory found
                        {
                            return bit;
                        }
                    }
                }
                if (type == ConstantClass.MEMORY.SHORT || type == ConstantClass.MEMORY.SHORT_AND_LONG)
                {
                    foreach (MemoryBitClass bit in m_ShortTermMemory)
                    {
                        if (bit == memoryBit) //memory found
                        {
                            return bit;
                        }
                    }
                }
                if (type == ConstantClass.MEMORY.BLOCKS)
                {
                    foreach (MemoryBitClass bit in m_MemoryForBlocksVisited)
                    {
                        if (bit == memoryBit) //memory found
                        {
                            return bit;
                        }
                    }
                }
                if(type == ConstantClass.MEMORY.BLOCKS)
                {
                    foreach (MemoryBitClass bit in m_MemoryForBlocksVisited)
                    {
                        if (bit == memoryBit) //memory found
                        {
                            return bit;
                        }
                    }
                }              
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (new MemoryBitClass());
        }

        public MemoryBitClass retrieveMemoryBitWithActionOnlyIfExistsInMemory(MemoryBitClass memoryBit, ConstantClass.MEMORY type) //checks if memorybit exists in short/long term memory
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            try
            {
                if (type == ConstantClass.MEMORY.LONG || type == ConstantClass.MEMORY.SHORT_AND_LONG)
                {
                    foreach (MemoryBitClass bit in m_LongTermMemory)
                    {
                        if (bit.ifActionIsSame(memoryBit)) //memory found
                        {
                            return bit;
                        }
                    }
                }
                if (type == ConstantClass.MEMORY.SHORT || type == ConstantClass.MEMORY.SHORT_AND_LONG)
                {
                    foreach (MemoryBitClass bit in m_ShortTermMemory)
                    {
                        if (bit.ifActionIsSame(memoryBit)) //memory found
                        {
                            return bit;
                        }
                    }
                }
                if (type == ConstantClass.MEMORY.BLOCKS)
                {
                    foreach (MemoryBitClass bit in m_MemoryForBlocksVisited)
                    {
                        if (bit.ifActionIsSame(memoryBit)) //memory found
                        {
                            return bit;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (new MemoryBitClass());
        }

        public void updateShortLongTermTransitions(string charID) //goes over short memory and transfer relevant memory bits to long term memory
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            List<int> indexToMove = new List<int>();
            int index = 0;

            try
            {
                //1. go over short term memory and check how important is memory bit
                foreach (MemoryBitClass bit in m_ShortTermMemory)
                {
                    //2. if priority of memory bit is greater than CHARACTER_MEMORY_SHORT_TO_LONG_TERM_THRESHOLD 
                    if (bit.isMemoryPriorityOverThreshold())
                    {
                        indexToMove.Add(index);
                    }
                    index++;
                }
                //then move to long term
                foreach (int number in indexToMove)
                {
                    MemoryBitClass toAddToLongTerm = m_ShortTermMemory[number];
                    GameTimeClass newDateMemoryLost = new GameTimeClass(toAddToLongTerm.getDateOfLastOccurence());
                    newDateMemoryLost.set_days(ConstantClass.CHARACTER_MEMORY_SHORT_TERM_EXPIRATION_DAYS_DURATION);
                    toAddToLongTerm.setDateOfMemoryWillBeLost(newDateMemoryLost);
                    m_LongTermMemory.Add(toAddToLongTerm);                    
                    ConstantClass.LOGGER.writeToGameLog(charID + " memory was engraved.");
                }
                foreach(int number in indexToMove.OrderByDescending(v => v))
                {                   
                    m_ShortTermMemory.RemoveAt(number);                        
                }
                index = 0;
                indexToMove.Clear();

                //3. same but go over long term memory and check if less than threshold. 
                foreach (MemoryBitClass bit in m_LongTermMemory)
                {                    
                    if (!bit.isMemoryPriorityOverThreshold())
                    {
                        indexToMove.Add(index);
                    }
                    index++;
                }
                //move to short term if lower than CHARACTER_MEMORY_SHORT_TO_LONG_TERM_THRESHOLD
                foreach (int number in indexToMove)
                {
                    m_ShortTermMemory.Add(m_LongTermMemory[number]);                    
                    ConstantClass.LOGGER.writeToGameLog(charID + " memory became more blurry.");
                }
                foreach (int number in indexToMove.OrderByDescending(v => v))
                {
                    m_LongTermMemory.RemoveAt(number);
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        private void refreshMemory(MemoryBitClass memoryBit, ConstantClass.MEMORY type) //memory bit exists in short/long term, updates the number of occurrence and last occurred date
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                //1. goes through short/long term memory list and finds object
                //2. update object number of occurrence and date of last ocurred
                if (type == ConstantClass.MEMORY.LONG || type == ConstantClass.MEMORY.SHORT_AND_LONG)
                {
                    foreach (MemoryBitClass bit in m_LongTermMemory)
                    {
                        if (bit == memoryBit)
                        {
                            bit.updateMemoryBitForShortLongTerm(ConstantClass.gameTime);
                        }
                    }
                }

                if (type == ConstantClass.MEMORY.SHORT || type == ConstantClass.MEMORY.SHORT_AND_LONG)
                {
                    foreach (MemoryBitClass bit in m_ShortTermMemory)
                    {
                        if (bit == memoryBit)
                        {
                            bit.updateMemoryBitForShortLongTerm(ConstantClass.gameTime);
                        }
                    }
                }
                if (type == ConstantClass.MEMORY.BLOCKS)
                {
                    foreach (MemoryBitClass bit in m_MemoryForBlocksVisited)
                    {
                        if (bit == memoryBit)
                        {
                            bit.updateMemoryBitForBlocksVisited(ConstantClass.gameTime);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        private void deleteMemory(MemoryBitClass memoryBit, ConstantClass.MEMORY type) //delete memory Bit from short/long term
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            bool found = false;
            int index = 0;
            try
            {
                //1. goes through short/long term memory list and finds object                
                //for long
                if (type == ConstantClass.MEMORY.LONG || type == ConstantClass.MEMORY.SHORT_AND_LONG)
                {
                    foreach (MemoryBitClass bit in m_LongTermMemory)
                    {
                        if (bit == memoryBit)
                        {
                            found = true;
                            break;
                        }
                        else index++;
                    }
                }
                if (found) m_LongTermMemory.RemoveAt(index);
                //for short
                found = false;
                index = 0;

                if (type == ConstantClass.MEMORY.SHORT || type == ConstantClass.MEMORY.SHORT_AND_LONG)
                {
                    foreach (MemoryBitClass bit in m_ShortTermMemory)
                    {
                        if (bit == memoryBit)
                        {
                            found = true;
                            break;
                        }
                        else index++;
                    }
                }
                if (found) m_ShortTermMemory.RemoveAt(index);
                //for blocks
                found = false;
                index = 0;

                if (type == ConstantClass.MEMORY.BLOCKS)
                {
                    foreach (MemoryBitClass bit in m_MemoryForBlocksVisited)
                    {
                        if (bit == memoryBit)
                        {
                            found = true;
                            break;
                        }
                        else index++;
                    }
                }
                if (found) m_MemoryForBlocksVisited.RemoveAt(index);
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void addMemoryToBlocksVisited(MemoryBitClass memoryBit, string charID) //adds a memory bit to blocks visited memory.
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                //1. check if memory is already in short/long term
                MemoryBitClass bit = retrieveMemoryBitIfExistsInMemory(memoryBit, ConstantClass.MEMORY.BLOCKS);

                if (!bit.isEmpty)
                {
                    //2. if yes, then update existing memory: date of occurrence and number of occurrence
                    refreshMemory(memoryBit, ConstantClass.MEMORY.BLOCKS);
                    ConstantClass.LOGGER.writeToGameLog(charID + " remembers something.");
                }
                //3. if no, then add new memory to short term
                else
                {
                    //3.1 check if short memory is full
                    if (isMemoryFull(ConstantClass.MEMORY.BLOCKS))
                    {
                        //3.2 if full, remove oldest memory and add newest memory
                        GameTimeClass earliestDateOfMemory = m_MemoryForBlocksVisited.Min(obj => obj.getDateMemoryWillBeLost());
                        MemoryBitClass oldestMemory = m_MemoryForBlocksVisited.First(obj => obj.getDateMemoryWillBeLost() == earliestDateOfMemory);
                        m_MemoryForBlocksVisited.Remove(oldestMemory);
                        m_MemoryForBlocksVisited.Add(memoryBit);
                        ConstantClass.LOGGER.writeToGameLog(charID + " forgets some place he had been.");
                    }
                    else
                    {
                        //3.3 if not full, add memory
                        memoryBit.updateMemoryBitForBlocksVisited(ConstantClass.gameTime);
                        m_MemoryForBlocksVisited.Add(memoryBit);
                        ConstantClass.LOGGER.writeToGameLog(charID + " memorizes the way.");
                    }
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void addMemoryToShortTerm(MemoryBitClass memoryBit, string charID) //adds a memory bit to short memory. cannot add directly to long term memory
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH            
            try
            {
                //1. check if memory is already in short/long term
                MemoryBitClass bit = retrieveMemoryBitIfExistsInMemory(memoryBit, ConstantClass.MEMORY.SHORT_AND_LONG);

                if (!bit.isEmpty)
                {
                    //2. if yes, then update existing memory: date of occurrence and number of occurrence
                    refreshMemory(memoryBit, ConstantClass.MEMORY.SHORT_AND_LONG);
                    ConstantClass.LOGGER.writeToGameLog(charID + " remembers something.");
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
                        ConstantClass.LOGGER.writeToGameLog(charID + " forgets something irrelevant.");
                    }
                    else
                    {
                        //3.3 if not full, add memory
                        m_ShortTermMemory.Add(memoryBit);
                        ConstantClass.LOGGER.writeToGameLog(charID + " memorizes something unique.");
                    }
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void removeMemoryFromBlocksVisited(MemoryBitClass memoryBit, string charID)//removes memory bit from blocks visited
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            //1. check if memory is already in short/long term
            try
            {
                MemoryBitClass bit = retrieveMemoryBitIfExistsInMemory(memoryBit, ConstantClass.MEMORY.BLOCKS);
                if (!bit.isEmpty)
                {
                    //2. if yes, then remove existing memory: date of occurrence and number of occurrence 
                    deleteMemory(memoryBit, ConstantClass.MEMORY.BLOCKS);
                    ConstantClass.LOGGER.writeToGameLog(charID + " forgets some place he had been.");
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void removeMemoryFromShortLongTerm(MemoryBitClass memoryBit, string charID)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            //1. check if memory is already in short/long term
            try
            {
                MemoryBitClass bit = retrieveMemoryBitIfExistsInMemory(memoryBit, ConstantClass.MEMORY.SHORT_AND_LONG);
                if (!bit.isEmpty)
                {
                    //2. if yes, then remove existing memory: date of occurrence and number of occurrence 
                    deleteMemory(memoryBit, ConstantClass.MEMORY.SHORT_AND_LONG);
                    ConstantClass.LOGGER.writeToGameLog(charID + " forgets something irrelevant.");
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void updateMemoryExpirationsAndPriority(string charID) 
        //method to update memory per tick.
        //1. checks expiration         
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            List<int> indexToRemove = new List<int>();
            int index = 0;
            GameTimeClass delta;
            bool newDayFlag = false;

            try
            {
                if (ConstantClass.gameTime.get_hour() == 0) { newDayFlag = true; }
                //for short
                foreach (MemoryBitClass bit in m_ShortTermMemory)
                {
                    //if memory bit is expired, mark them to remove
                    if (bit.isMemoryBitExpired())
                    {
                        indexToRemove.Add(index);
                    }
                    if (newDayFlag)
                    {
                        delta = bit.returnDeltaLastOccuredDate();
                        bit.reducePriorityBy(delta.get_day()); //reduces priority based on number of days passed the last occurrence date.                        
                    }
                    index++;
                }
                //remove those memories
                foreach (int number in indexToRemove)
                {
                    m_ShortTermMemory.RemoveAt(number);
                    ConstantClass.LOGGER.writeToGameLog(charID + " lost a memory.");
                }
                //for long
                index = 0;
                indexToRemove.Clear();

                foreach (MemoryBitClass bit in m_LongTermMemory)
                {
                    //if memory bit is expired, then remove it from memory
                    if (bit.isMemoryBitExpired())
                    {
                        indexToRemove.Add(index);
                    }
                    if (newDayFlag)
                    {
                        delta = bit.returnDeltaLastOccuredDate();
                        bit.reducePriorityBy(delta.get_day()); //reduces priority based on number of days passed the last occurrence date.
                    }
                    index++;
                }
                //remove those memories
                foreach (int number in indexToRemove)
                {
                    m_LongTermMemory.RemoveAt(number);
                    ConstantClass.LOGGER.writeToGameLog(charID + " lost a memory..");
                }
                //for blocks
                index = 0;
                indexToRemove.Clear();

                foreach (MemoryBitClass bit in m_MemoryForBlocksVisited)
                {
                    //if memory bit is expired, then remove it from memory
                    if (bit.isMemoryBitExpired())
                    {
                        indexToRemove.Add(index);
                    }
                    if (newDayFlag)
                    {
                        delta = bit.returnDeltaLastOccuredDate();
                        bit.reducePriorityBy(delta.get_day()); //reduces priority based on number of days passed the last occurrence date.
                    }
                    index++;
                }
                //remove those memories
                foreach (int number in indexToRemove)
                {
                    m_MemoryForBlocksVisited.RemoveAt(number);
                    ConstantClass.LOGGER.writeToGameLog(charID + " lost a memory...");
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void printMemory(string charID)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                ConstantClass.LOGGER.writeToCharLog("Short Term Memory|Size|" + m_ShortTermMemory.Capacity, charID);
                foreach (MemoryBitClass shortMem in m_ShortTermMemory)
                {
                    ConstantClass.LOGGER.writeToCharLog("Short Term Memory|" + shortMem.printMemoryBit(), charID);
                }

                ConstantClass.LOGGER.writeToCharLog("Long Term Memory|Size|" + m_LongTermMemory.Capacity, charID);
                foreach (MemoryBitClass longMem in m_LongTermMemory)
                {
                    ConstantClass.LOGGER.writeToCharLog("Long Term Memory|" + longMem.printMemoryBit(), charID);
                }

                ConstantClass.LOGGER.writeToCharLog("Block Visited Memory|Size|" + m_MemoryForBlocksVisited.Capacity, charID);
                foreach (MemoryBitClass blockMem in m_MemoryForBlocksVisited)
                {
                    ConstantClass.LOGGER.writeToCharLog("Block Visited Memory|" + blockMem.printMemoryBit(), charID);
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public Guid chooseUnvisitedBlockFromMemory(string charID) //returns an unvisited block ID, goes over all short/term memory blocks , remove all visited blocks from there and choose randomly one from the remaining. if none, return Guid.empty
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            List<Guid> listofPotentialBlocks = new List<Guid>();
            Guid blockChosen = Guid.Empty;

            try
            {
                //1. gather all FIND_BLOCK ids from short and long term memory            
                foreach (MemoryBitClass bit in m_ShortTermMemory)
                {
                    if (bit.isActionSameAs(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK))
                    {
                        listofPotentialBlocks.Add(bit.getIDOfSomething());
                    }
                }
                foreach (MemoryBitClass bit in m_LongTermMemory)
                {
                    if (bit.isActionSameAs(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK))
                    {
                        listofPotentialBlocks.Add(bit.getIDOfSomething());
                    }
                }

                //2. remove all ids that are in visited blocks memory
                foreach (MemoryBitClass bit in m_MemoryForBlocksVisited)
                {
                    Guid id = bit.getIDOfSomething();

                    if (listofPotentialBlocks.Contains(id)) //if visited block is in potential blocks
                    {
                        int index = listofPotentialBlocks.IndexOf(id);
                        listofPotentialBlocks.RemoveAt(index);
                    }
                }

                //3. check if list is empty, if so return Guid.empty
                if (listofPotentialBlocks.Count == 0) blockChosen = Guid.Empty;
                else //4. if not empty, choose randomly one Guid from list.
                {
                    int randomIndex = ConstantClass.RANDOMIZER.produceInt(0, listofPotentialBlocks.Count);
                    blockChosen = listofPotentialBlocks[randomIndex];
                    ConstantClass.LOGGER.writeToGameLog(charID + " chooses randomly to go to " + blockChosen.ToString());
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return blockChosen;
        }
    }
}
