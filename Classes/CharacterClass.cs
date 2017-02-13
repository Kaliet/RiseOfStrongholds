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
            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getListOfOccupants().Add(m_unique_character_id); //adds character id as part of block list of occupants

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/       
        public int returnIndexOfActionWithHighestIndex()//returns the FIRST index from m_action_this_turn that has the highest priority action
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            int highestPriority = ConstantClass.ACTION_NO_PRIORITY; //0 is highest, then 1,2,3...is lower, no priority = 99999
            int index = -1;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

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

            
        }

        public string outputPersonGUID()
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return "Person " + m_unique_character_id.ToString().Substring(0, 2) + "(" + m_stats.getEnergy().getCurrentValue() + "/" + m_stats.getEnergy().getMaxValue() + ")";

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
            ConstantClass.LOGGER.writeToQueueLog(outputPersonGUID() + " = " + m_action_queue.printQueue(this.m_action_queue));//print queue
            //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is in block " + m_block_id.ToString().Substring(0, 2) + " position(" + 
            //            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getPosition().getPositionX() + "," +
            //            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getPosition().getPositionY() + "). Exits: " + ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].printAllAvailableExits());
            ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is on block " + this.m_block_id + " in room ID " + ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id));            

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

                        ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getListOfOccupants().Remove(m_unique_character_id);//remove character id from previuos block list of occupants
                        m_block_id = possibleExitsToWalk[exitNumber % possibleExitsToWalk.Count]; //character moves to a different block - new id defined
                        ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getListOfOccupants().Add(m_unique_character_id); //adds character id as part of block list of occupants
                        deductEnergyBasedOnTerrain();

                        ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is going to walk " + printDirection(allExits, m_block_id) + " into block " + m_block_id.ToString().Substring(0, 2) + ".");
                    }
                    m_action_queue.getQueue().RemoveAt(index); //action completed, remove from index
                }
                else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK ||
                         m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.FIND_CHAR) //SEARCHES FOR SPECIFIC BLOCK or CHAR
                {
                    Guid targetBlockID = Guid.Empty;
                    Guid targetCharID = Guid.Empty;                                       

                    if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.FIND_CHAR) //if find char, then get targetBlockID of the char
                    {
                        targetCharID = m_action_queue.getQueue()[index].getGuidForAction(); //ID of target char to be searched (by this.character)
                        targetBlockID = ConstantClass.MAPPING_TABLE_FOR_ALL_CHARS.getMappingTable()[targetCharID].getBlockID(); //ID of block where target char is residing
                    }
                    else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK)
                    {
                        targetBlockID = m_action_queue.getQueue()[index].getGuidForAction(); //ID of block this.character is searching
                    }

                    Guid targetBlockRoomID = ConstantClass.GET_ROOMID_BASED_BLOCKID(targetBlockID); //roomID where this.character wants to get
                    Guid currentCharRoomID = ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id); //current roomID where this.character resides

                    if (ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable().ContainsKey(targetBlockID)) //checks if targetblockID is a block ID
                    {
                        if (targetBlockID == m_block_id) //arrived
                        {
                            m_action_queue.getQueue().RemoveAt(index); //action completed, remove from index                            
                            ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " has found " + targetBlockID);
                            updateAction(); //take next action since character move action completed beginning of this round.
                        }
                        else if (ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[targetBlockID].getBuildingID() != Guid.Empty && 
                                 ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[targetBlockID].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //wall
                        {
                            m_action_queue.getQueue().RemoveAt(index); //action completed, remove from index
                            ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " cannot walk to  " + targetBlockID+ " due to building.");
                            updateAction(); //take next action since character move action completed beginning of this round.
                        }
                        else if (targetBlockRoomID != currentCharRoomID) //target block is in another room than the character
                        {
                            List<GuidPairClass> listOfSharedBlocks = new List<GuidPairClass>();
                            List<Guid> backtrackedRooms = new List<Guid>();
                            Guid adjRoomIDOfCurrentRoomID = backTrackToAdjacentRoom(targetBlockRoomID, currentCharRoomID, backtrackedRooms);
                            GuidPairClass roomsPair = new GuidPairClass(adjRoomIDOfCurrentRoomID, currentCharRoomID);
                            Guid chosenBlockOnAdjRoom = Guid.Empty;
                            Guid chosenBlockOnCharRoom = Guid.Empty;
                            bool crossedRoom = false;

                            if (ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable().ContainsKey(roomsPair))
                            {
                                listOfSharedBlocks = ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable()[roomsPair];
                            }

                            //1. go over all shared blocks and check if character is standing on shared block between the rooms
                            if (listOfSharedBlocks.Count > 0) //check list is not empty
                            {
                                foreach (GuidPairClass pair in listOfSharedBlocks)
                                {
                                    if (pair.isGuidOneofthePairs(m_block_id)) //character is standing on a shared block between the rooms
                                    {
                                        //next action is to move to next room
                                        ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getListOfOccupants().Remove(m_unique_character_id);//remove character id from previuos block list of occupants
                                        m_block_id = pair.returnSecondGuidPair(m_block_id); //character moves to a different block - new id defined                                    
                                        ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getListOfOccupants().Add(m_unique_character_id); //adds character id as part of block list of occupants
                                        deductEnergyBasedOnTerrain();
                                        crossedRoom = true;

                                        //if character newly arrived block is still not target block id and last action item in queue is to reach target block id, then we do not remove it.
                                        //needed for scenario where character starts at shared block and has only 1 action item in queue to reach to target block id in different room. without below, the last action will be removed and character will not move.
                                        if (m_block_id != targetBlockID && m_action_queue.getQueue().Count == 1 && m_action_queue.getQueue()[0].getGuidForAction() == targetBlockID) { } 
                                        else { m_action_queue.getQueue().RemoveAt(index); }//action completed, remove from index

                                        ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " crossed room to block " + m_block_id + " in room ID " + ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id));

                                        break;
                                    }
                                }
                                if (!crossedRoom) //character is not standing on a shared block between the rooms
                                {
                                    //2. choose randomly among the available shared blocks to walk into the other room
                                    int exitNumber = ConstantClass.RANDOMIZER.produceInt(1, 100); //randomizing which block to take

                                    if (listOfSharedBlocks.Count > 0)
                                    {
                                        chosenBlockOnCharRoom = listOfSharedBlocks[exitNumber % listOfSharedBlocks.Count].m_guid2;
                                        chosenBlockOnAdjRoom = listOfSharedBlocks[exitNumber % listOfSharedBlocks.Count].m_guid1;
                                    }
                                    //3. add to queue new action character to find shared exit block id with higher priority than current action
                                    int currentPriority = this.m_action_queue.getQueue()[index].getPriority();

                                    m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, currentPriority - 2, ConstantClass.VARIABLE_FOR_ACTION_NONE, chosenBlockOnCharRoom));
                                    m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, currentPriority - 1, ConstantClass.VARIABLE_FOR_ACTION_NONE, chosenBlockOnAdjRoom));
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is looking for block " + targetBlockID + " in room ID " + targetBlockRoomID + "(" + (m_action_queue.getQueue()[index].getPriority()) + ")");
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " needs to find " + chosenBlockOnCharRoom + " in room ID " + ConstantClass.GET_ROOMID_BASED_BLOCKID(chosenBlockOnCharRoom) + "(" + (currentPriority - 2) + ")");
                                    ConstantClass.LOGGER.writeToGameLog("and cross to block " + chosenBlockOnAdjRoom + " in room ID " + ConstantClass.GET_ROOMID_BASED_BLOCKID(chosenBlockOnAdjRoom) + "(" + (currentPriority - 1) + ")");

                                    updateAction(); //take next action since character move action completed beginning of this round.
                                }
                            }
                        }
                        else //block is in the same room as character
                        {
                            PathFindingClass searchPath = new PathFindingClass(m_block_id, targetBlockID);

                            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getListOfOccupants().Remove(m_unique_character_id);//remove character id from previuos block list of occupants
                            m_block_id = searchPath.returnNextBlockGuidToMove();
                            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getListOfOccupants().Add(m_unique_character_id); //adds character id as part of block list of occupants
                            ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " moves to block " + m_block_id);
                            deductEnergyBasedOnTerrain();
                            ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is finding " + targetBlockID + " with priority " + (m_action_queue.getQueue()[index].getPriority() - 1));
                        }
                    }
                    else
                    {
                        ConstantClass.LOGGER.writeToGameLog(targetBlockID + " is not a blockID. Removing action.");
                        m_action_queue.getQueue().RemoveAt(index); //action completed, remove from index
                        updateAction(); //take next action since character move action completed beginning of this round.
                    }
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
                    m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.EAT, ConstantClass.ACTION_EAT_PRIORITY, ConstantClass.VARIABLE_FOR_ACTION_NONE, Guid.Empty));
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_WHEN_HUNGRY); //if hungry , start deducting energy
                }
                if (m_stats.getSleepStatus() == ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY)
                {
                    m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.SLEEP, ConstantClass.ACTION_SLEEP_PRIORITY, ConstantClass.MINIMUM_NUMBER_OF_SLEEP_HOURS * ConstantClass.MINUTES_IN_ONE_HOUR, Guid.Empty));
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_WHEN_SLEEPY); //if sleepy , start deducting energy
                }

                /*TEST BLOCK FOR WALK ACTION*/
                //m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.WALK, ConstantClass.ACTION_WALK_PRIORITY, ConstantClass.VARIABLE_FOR_ACTION_NONE,m_block_id)); //walks
                /*-*/

                /*TEST BLOCK FOR SEARCH ACTION*/
                //Guid roomID = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getRoomID();
                //int roomSize = ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[roomID].getSize();
                //Guid targetBlockID = ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[roomID].getRoom()[1, 0].getUniqueBlockID();                
                //m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.SEARCH, ConstantClass.ACTION_SEARCH_PRIORITY, ConstantClass.VARIABLE_FOR_ACTION_NONE, targetBlockID)); //searches
                /*-*/

                deductEnergyBasedOnTerrain();

            }
            OnActionUpdated(); //raise event

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        private Guid backTrackToAdjacentRoom (Guid endRoomID, Guid startRoomID, List<Guid> backtrackedRooms) //recursively backtracks from startRoomID to targetRoomID. Return adjacent room ID of targetRoomID, else return Guid.Empty
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is backtracking from room ID " + startRoomID + " to get to target room ID " + endRoomID);

            GuidPairClass pair = new GuidPairClass(endRoomID, startRoomID);            

            if (startRoomID == Guid.Empty || endRoomID == Guid.Empty) { return Guid.Empty; }

            //1. if startRoomID and endRoomID have shared exit  && char room is the startroom then return endRoomID
            if (ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable().ContainsKey(pair) && ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id) == startRoomID)
            {
                if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
                ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " backtracked to room ID " + endRoomID);
                return endRoomID;
            }
            //2. else search in mapping table if endRoomID has a pair (aka neighbor room with exit)
            else
            {
                Guid endRoomGuidPair = ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.returnNonVisitedGuidPair(endRoomID, backtrackedRooms);

                //3.    if found pair , backTrackToAdjacentRoom (neighborrRoomID)
                if (ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.isGuidAKey(endRoomID)) //if endRoomID has a neighbor = has entry in mapping table and it was not visited
                {

                    if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
                    backtrackedRooms.Add(endRoomID); 
                    return backTrackToAdjacentRoom(endRoomGuidPair, startRoomID, backtrackedRooms); //call recursively this function with neighborID
                }
                //4.    if not found pair, return Guid.Empty (aka no adjancent room next to startRoomID)
                else
                {
                    if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " cannot reach to room ID " + endRoomID);
                    return Guid.Empty; //end room has no neighbor -> impossible to arrive there -> return empty Guid.
                }
            }                                                      
            
        }

        public void FOR_DEBUG_addActionInQueue(ActionClass action)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            
            m_action_queue.getQueue().Add(action); //searches

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        private void deductEnergyBasedOnTerrain()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            int additionalTerrainFatigue = ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS.getMappingTable()[ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getTerrainID()].getFatigueCost();
            m_stats.modifyEnergy(ConstantClass.ENERGY_COST_FOR_WALKING + additionalTerrainFatigue); //for now start walking

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*EVENTS HANDLER*/
        public event EventHandler ActionUpdated;

        protected virtual void OnActionUpdated()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (ActionUpdated != null)
            {
                ActionUpdated(this, EventArgs.Empty);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void OnGameTicked (object source, EventArgs args)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            updateAction(); //update action for every game tick 
            ConstantClass.LOGGER.writeToGameLog("----------------------------------------------------------------------------");

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
