using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{      
    public class CharacterClass
    {
        /* VARIABLES */
        private string m_character_name; //name
        private CharacterStatsClass m_stats; //stats
        private GameTimeClass m_birthDate; //date of birth
        private Guid m_unique_character_id; //character's unique id
        private QueueClass<ActionClass> m_action_queue; //queue of actions the character is doing
        private Guid m_block_id; //in which block the character resides in        
        
        /*GET & SET*/
        public string getName() { return m_character_name; }
        public CharacterStatsClass getStats() { return m_stats; }
        public GameTimeClass getBirthDate() { return m_birthDate; }  
        public Guid getUniqueCharacterID () { return m_unique_character_id; } 
        public Guid getBlockID() { return m_block_id; }

        /*CONSTRUCTORS*/
        public CharacterClass(Guid blockID)
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_character_name = "";

            m_stats = new CharacterStatsClass();
            m_stats.initializeHP(10);
            m_stats.initializeEnergy(20);
            //m_stats.initializeHungerRate(0, ConstantClass.RANDOMIZER.produceInt(1, ConstantClass.HOURS_BETWEEN_EATING * ConstantClass.HOURS_IN_ONE_DAY));
            //m_stats.initializeSleepRate(0, ConstantClass.RANDOMIZER.produceInt(1, ConstantClass.HOURS_BETWEEN_SLEEPING * ConstantClass.HOURS_IN_ONE_DAY));
            m_stats.initializeHungerRate(0, 2000);
            m_stats.initializeSleepRate(0, 2000);
            m_stats.setHungerStatus(ConstantClass.CHARACTER_HUNGER_STATUS.FULL);
            m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE);

            m_birthDate = new GameTimeClass(ConstantClass.gameTime);
            m_unique_character_id = Guid.NewGuid(); //unique id for character            
            m_action_queue = new QueueClass<ActionClass>();

            m_block_id = blockID;

            ConstantClass.MAPPING_TABLE_FOR_ALL_CHARS.getMappingTable().Add(m_unique_character_id, this);

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/       
        public int returnIndexOfActionWithHighestIndex()//returns the FIRST index from m_action_this_turn that has the highest priority action
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            int highestPriority = ConstantClass.ACTION_NO_PRIORITY; //0 is highest, then 1,2,3...is lower, no priority = 99999
            int index = -1;

            if (m_action_queue.getQueue().Count <= 0) { return -1; }
            else if (m_action_queue.getQueue().Count == 1) { return 0; }
            else //list has at least 2 elements
            {
                int count = 0;
                foreach (ActionClass action in m_action_queue.getQueue())
                {
                    if (action.getPriority() < highestPriority)
                    {
                        highestPriority = action.getPriority();  //found higher index
                        index = count;
                    }
                    count++;
                }
                return index;
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public string outputPersonGUID()
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return "Person " + m_unique_character_id.ToString().Substring(4, 2) + "(" + m_stats.getEnergy().getCurrentValue() + "/" + m_stats.getEnergy().getMaxValue() + ")";

        }

        public string printDirection(Guid[] allexits, Guid exitWalked)
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string direction = "";
            bool found = false;
            int i = 0;

            while (!found && i<allexits.Count())
            {
                if (allexits[i] == exitWalked) { found = true; }
                else { i++; }
            }

            if (found)
            {
                switch (i)
                {
                    case (int)ConstantClass.EXITS.NORTH:
                        direction = "North";
                        break;
                    case (int)ConstantClass.EXITS.SOUTH:
                        direction = "South";
                        break;
                    case (int)ConstantClass.EXITS.EAST:
                        direction = "East";
                        break;
                    case (int)ConstantClass.EXITS.WEST:
                        direction = "West";
                        break;
                }
            }
            else direction = "ERROR";

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return direction;
        }


        public void updateAction() //character decides what to do now and performs the action
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            int index = -1;

            /*DEBUG PRINTING*/
            ConstantClass.LOGGER.writeToQueueLog(outputPersonGUID() + " = " + m_action_queue.printQueue());//print queue
            ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is in block " + m_block_id.ToString().Substring(0, 2) + " position(" + 
                ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getPosition().getPositionX() + "," +
                ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getPosition().getPositionY() + "). Exits: " + ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].printAllAvailableExits());

            m_stats.modifyHungerRate(ConstantClass.GAME_SPEED); //hunger increases based on game speed (1 sec = how many game time mins)
            m_stats.modifySleepRate(ConstantClass.GAME_SPEED); //sleepiness increases based on game speed (1 sec = how many game time mins)

            if (m_action_queue.getQueue().Count > 0) //there are still actions left in the queue
            {
                //perform highest priority action in action list                
                index = returnIndexOfActionWithHighestIndex();
                if (index < 0) throw new Exception("Queue empty.");
                if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.EAT) // EAT
                {
                    //TODO: character eats something or goes to find something to eat
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is EATING");
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_FOR_EATING);
                    m_stats.initializeHungerRate(0, m_stats.getHungerRate().getMaxValue());
                    m_stats.setHungerStatus(ConstantClass.CHARACTER_HUNGER_STATUS.FULL);
                    //TODO: add HP from eating.
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is FULL");
                    m_action_queue.getQueue().RemoveAt(index); //EAT action completed - removed from queue
                }
                else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.SLEEP) // SLEEP
                {
                    //TODO: character goes to sleep until he replenishes his energy                    
                    if (m_action_queue.getQueue()[index].getVarForAction() > 0)//wait until MINIMUM_NUMBER_OF_SLEEP_HOURS 
                    {
                        //wait - character is sleeping
                        ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is SLEEPING");
                        m_action_queue.getQueue()[index].modifyVarForAction(-1*ConstantClass.GAME_SPEED);
                        m_stats.modifyEnergy(ConstantClass.ENERGY_ADD_WHEN_SLEEP);
                    }
                    else //sleeping is over, reinitialize sleep rate and set status to AWAKE
                    {
                        m_stats.initializeSleepRate(0, m_stats.getSleepRate().getMaxValue());
                        m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE);
                        ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is AWAKE");
                        m_action_queue.getQueue().RemoveAt(index); //SLEEP action completed - removed from queue
                    }
                }
                else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.WALK) // WALKS
                {
                    //TODO: character wants to walk to a random exit (before AI introduction)
                    Guid[] allExits = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getAllExits(); //get all exits from character's residing block
                    List<Guid> possibleExitsToWalk = new List<Guid>();

                    foreach (Guid id in allExits) //go through all exits and check which ones are exitable
                    {
                        if (id != Guid.Empty) { possibleExitsToWalk.Add(id); }
                    }

                    if (possibleExitsToWalk.Count == 0) { ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " cannot walk out of the current block."); }
                    else //character walks out of one of the exits
                    {
                        int exitNumber = ConstantClass.RANDOMIZER.produceInt(1, 100); //randomizing which exit to take                        
                                                
                        m_block_id = possibleExitsToWalk[exitNumber % possibleExitsToWalk.Count];

                        ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " walks " + printDirection(allExits, m_block_id) + " into block " + m_block_id.ToString().Substring(0, 2) + ".");
                    }
                    m_action_queue.getQueue().RemoveAt(index); //remove from index
                }
                
            }
            else 
            {
                /*UPDATE BIOLOGICAL DETERIORATION*/
                if (m_stats.getHungerRate().getCurrentValue() == m_stats.getHungerRate().getMaxValue()) //current = max --> hunger state
                {
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is HUNGRY");
                    m_stats.setHungerStatus(ConstantClass.CHARACTER_HUNGER_STATUS.HUNGRY);
                }
                if (m_stats.getSleepRate().getCurrentValue() == m_stats.getSleepRate().getMaxValue() || (m_stats.getEnergy().getCurrentValue() == 0))
                {
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is TIRED and SLEEPY");
                    m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY);
                }

                /*DECISIONS BASED ON BIOLOGICAL NEEDS*/
                if (m_stats.getHungerStatus() == ConstantClass.CHARACTER_HUNGER_STATUS.HUNGRY)
                {
                    m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.EAT,ConstantClass.ACTION_EAT_PRIORITY,ConstantClass.VARIABLE_FOR_ACTION_NONE));
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_WHEN_HUNGRY); //if hungry , start deducting energy
                }
                if (m_stats.getSleepStatus() == ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY)
                {
                    m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.SLEEP,ConstantClass.ACTION_SLEEP_PRIORITY,ConstantClass.MINIMUM_NUMBER_OF_SLEEP_HOURS*ConstantClass.MINUTES_IN_ONE_HOUR));
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_WHEN_SLEEPY); //if sleepy , start deducting energy
                }

                m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.WALK, ConstantClass.ACTION_WALK_PRIORITY, ConstantClass.VARIABLE_FOR_ACTION_NONE));
                int additionalTerrainFatigue = ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS.getMappingTable()[ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getTerrainID()].getFatigueCost();

                m_stats.modifyEnergy(ConstantClass.ENERGY_COST_FOR_WALKING + additionalTerrainFatigue); //for now start walking
                
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*EVENTS HANDLER*/
        public void OnGameTicked (object source, EventArgs args)
        {
            updateAction(); //update action for every game tick
        }
    }
}
