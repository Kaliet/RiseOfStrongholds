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
        private QueueActionClass m_action_queue; //queue of actions the character is doing
        private Guid m_block_id; //in which block the character resides in        
        private InventoryClass m_inventory; //characters inventory 
        private MemoryClass m_memoryBank; //character's memory to store vital information (short and long term)
        
        /*GET & SET*/
        public string getName() { return m_character_name; }
        public CharacterStatsClass getStats() { return m_stats; }
        public GameTimeClass getBirthDate() { return m_birthDate; }  
        public Guid getUniqueCharacterID () { return m_unique_character_id; } 
        public Guid getBlockID() { return m_block_id; }
        public MemoryClass DEBUG_getMemory() { return m_memoryBank; }

        /*CONSTRUCTORS*/
        public CharacterClass(Guid blockID)
        {            
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_character_name = "";

            m_stats = new CharacterStatsClass();
            m_stats.initializeHP(50);
            m_stats.initializeEnergy(20);
            
            //m_stats.initializeHungerRate(0, ConstantClass.RANDOMIZER.produceInt(1, ConstantClass.HOURS_BETWEEN_EATING * ConstantClass.HOURS_IN_ONE_DAY));
            //m_stats.initializeSleepRate(0, ConstantClass.RANDOMIZER.produceInt(1, ConstantClass.HOURS_BETWEEN_SLEEPING * ConstantClass.HOURS_IN_ONE_DAY));
            m_stats.initializeHungerRate(0, (int)(ConstantClass.HOURS_BETWEEN_EATING * ConstantClass.MINUTES_IN_ONE_HOUR * (ConstantClass.SECONDS_IN_ONE_MINUTE / ConstantClass.GAME_SPEED*1.0))); //total number of game minutes worth of HOURS_BETWEEn_EATING. 
            m_stats.initializeSleepRate(0, (int)(ConstantClass.HOURS_BETWEEN_SLEEPING * ConstantClass.MINUTES_IN_ONE_HOUR * (ConstantClass.SECONDS_IN_ONE_MINUTE / ConstantClass.GAME_SPEED * 1.0)));
            m_stats.setHungerStatus(ConstantClass.CHARACTER_SATIETY_STATUS.FULL);
            m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE);

            m_birthDate = new GameTimeClass(ConstantClass.gameTime);            
            m_unique_character_id = Guid.NewGuid(); //unique id for character            
            m_action_queue = new QueueActionClass();
            m_inventory = new InventoryClass(ConstantClass.INVENTORY_MAX_CHAR_CAP);
            m_memoryBank = new Classes.MemoryClass(ConstantClass.CHARACTER_MEMORY_INITIAL_SIZE);

            m_block_id = blockID;

            ConstantClass.MAPPING_TABLE_FOR_ALL_CHARS.getMappingTable().Add(m_unique_character_id, this);
            try
            {
                ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].addCharacterToBlockOccupants(m_unique_character_id);//adds character id as part of block list of occupants
            }
            catch (BlockOccupiedException ex)
            {
                ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " cannot be placed in block " + m_block_id + " since it is occupied. Exiting character creation.");
                throw new CharacterNotCreatedException();
            }

            ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " was created and currently standing on block " + m_block_id);

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

            try
            {
                /*DEBUG PRINTING*/
                ConstantClass.LOGGER.writeToQueueLog(outputPersonGUID() + " = " + m_action_queue.printQueue());//print queue
                                                                                                                                  //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is in block " + m_block_id.ToString().Substring(0, 2) + " position(" + 
                                                                                                                                  //            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getPosition().getPositionX() + "," +
                                                                                                                                  //            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getPosition().getPositionY() + "). Exits: " + ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].printAllAvailableExits());
                //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is on block " + this.m_block_id + " in room ID " + ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id));

                //checks if character is alive
                if (m_stats.getLifeStatus() == ConstantClass.CHARACTER_LIFE_STATUS.ALIVE)
                {
                    m_stats.modifyHungerRate(ConstantClass.GAME_SPEED); //hunger increases based on game speed (1 sec = how many game time mins)
                    m_stats.modifySleepRate(ConstantClass.GAME_SPEED); //sleepiness increases based on game speed (1 sec = how many game time mins)

                    if (m_action_queue.getQueue().Count > 0) //there are still actions left in the queue
                    {
                        //perform highest priority action in action list                
                        index = returnIndexOfActionWithHighestIndex();
                        if (index < 0) throw new Exception("Queue empty.");

                        //-------------------ACTION: EAT ---------------------//

                        if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.EAT) // EAT
                        {
                            if (m_action_queue.getQueue()[index].getVarForAction() > 0)
                            {                                
                                ResourceObjectClass foodItem = new ResourceObjectClass(ConstantClass.RESOURCE_TYPE.FOOD, ConstantClass.QUANTITY_TO_DEDUCT_PER_MEAL);

                                if (m_inventory.existsInInventory(foodItem)) //if there is food in inventory, then proceed to eat
                                {
                                    m_inventory.deductQuantityOfItem(foodItem);
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is EATING ");
                                    m_action_queue.getQueue()[index].modifyVarForAction(-1 * ConstantClass.GAME_SPEED);
                                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_FOR_EATING);
                                }
                                else //need to find food
                                {
                                    //gathers food at same block                                    
                                    m_action_queue.addAction(new ActionClass(ConstantClass.CHARACTER_ACTIONS.GATHER, m_action_queue.getQueue()[index].getPriority() - 1, 0, Guid.Empty));
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " has no food! Going to gather food.");
                                }
                            }
                            else
                            {
                                m_stats.initializeHungerRate(0, m_stats.getHungerRate().getMaxValue());
                                m_stats.setHungerStatus(ConstantClass.CHARACTER_SATIETY_STATUS.FULL);
                                m_stats.modifyHP(ConstantClass.CHARACTER_HP_REGEN_EATING);
                                ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " has finished EATING.");
                                //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " has regenerated HP (" + ConstantClass.CHARACTER_HP_REGEN_EATING + ") from eating.");
                                m_action_queue.removeAction(index); //EAT action completed - removed from queue
                            }
                        }

                        //-------------------ACTION: SLEEP ---------------------//

                        else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.SLEEP) // SLEEP
                        {                            
                            if (m_action_queue.getQueue()[index].getVarForAction() > 0)//wait until MINIMUM_NUMBER_OF_SLEEP_HOURS 
                            {
                                //wait - character is sleeping
                                ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is SLEEPING.");
                                m_action_queue.getQueue()[index].modifyVarForAction(-1 * ConstantClass.GAME_SPEED);
                                m_stats.modifyEnergy(ConstantClass.ENERGY_ADD_WHEN_SLEEP);
                            }
                            else //sleeping is over, reinitialize sleep rate and set status to AWAKE
                            {
                                m_memoryBank.updateShortLongTermTransitions(outputPersonGUID()); //during sleep, character memories are moved between short/long term
                                m_stats.initializeSleepRate(0, m_stats.getSleepRate().getMaxValue());
                                m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE);
                                m_stats.fillHPtoMax();
                                m_stats.fillEnergytoMax();
                                ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is AWAKE.");
                                m_action_queue.removeAction(index); //SLEEP action completed - removed from queue
                            }
                        }

                        //-------------------ACTION: REST ---------------------//

                        else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.REST) // TIRED
                        {                            
                            if (m_action_queue.getQueue()[index].getVarForAction() > 0 && !m_stats.isEnergyAtMax())//wait until rest period is over or energy reaches max
                            {
                                //wait - character is resting                            
                                ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is RESTING.");
                                m_stats.modifyEnergy(ConstantClass.ENERGY_ADD_WHEN_SLEEP);
                                m_action_queue.getQueue()[index].modifyVarForAction(-1 * ConstantClass.GAME_SPEED);

                            }
                            else //rest is over, set status to AWAKE
                            {
                                ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is RESTED.");
                                m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE);
                                m_action_queue.removeAction(index); //REST action completed - removed from queue
                            }
                        }

                        //-------------------ACTION: WALK ---------------------//

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
                                Guid nextStep = possibleExitsToWalk[exitNumber % possibleExitsToWalk.Count]; //character possible next step 

                                if (ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[nextStep].isOccupantListEmpty()) //checks if next step is occupied
                                {

                                    ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].removeCharacterFromBlockOccupants(m_unique_character_id);//remove character id from previuos block list of occupants                            
                                    ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[nextStep].addCharacterToBlockOccupants(m_unique_character_id);//adds character id as part of block list of occupants                            
                                    m_block_id = nextStep;
                                    deductEnergyBasedOnTerrain();

                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is going to walk " + printDirection(allExits, m_block_id) + " into block " + m_block_id.ToString().Substring(0, 2) + ".");
                                }
                                else
                                {
                                    //character waits one turn
                                }
                            }
                            m_action_queue.removeAction(index); //action completed, remove from index
                        }

                        //-------------------ACTION: FIND_BLOCK / FIND_CHAR ---------------------//

                        else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK ||
                                 m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.FIND_CHAR) //SEARCHES FOR SPECIFIC BLOCK or CHAR
                        {
                            Guid targetBlockID = Guid.Empty;
                            Guid targetCharID = Guid.Empty;

                            if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.FIND_CHAR) //if find char, then get targetBlockID of the char
                            {
                                targetCharID = m_action_queue.getQueue()[index].getGuidForAction(); //ID of target char to be searched (by this.character)    
                                Guid targetCharBlockID = ConstantClass.MAPPING_TABLE_FOR_ALL_CHARS.getMappingTable()[targetCharID].getBlockID();

                                //need to find unoccupid targetBlockID next to targetCharID                                                        
                                List<Guid> unoccupiedBlocksNextToChar = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[targetCharBlockID].returnListOfUnoccupiedAdjBlocks(targetCharBlockID);

                                if (unoccupiedBlocksNextToChar.Count == 0) //if no avaiable adjacent blocks next to char, then wait one turn
                                {
                                    //wait one turn
                                }
                                else //choose first block from list??
                                {
                                    targetBlockID = unoccupiedBlocksNextToChar.First();//ID of block where target char is residing
                                }
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
                                    m_action_queue.removeAction(index); //action completed, remove from index                            
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " has found " + targetBlockID);
                                    updateAction(); //take next action since character move action completed beginning of this round.
                                }
                                else if (ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[targetBlockID].getBuildingID() != Guid.Empty &&
                                         ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[targetBlockID].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //wall
                                {
                                    m_action_queue.removeAction(index); //action completed, remove from index
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " cannot walk to  " + targetBlockID + " due to building.");
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
                                                Guid nextStep = pair.returnSecondGuidPair(m_block_id); //character possible next step 

                                                if (ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[nextStep].isOccupantListEmpty())
                                                {
                                                    //next action is to move to next room
                                                    ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].removeCharacterFromBlockOccupants(m_unique_character_id);//remove character id from previuos block list of occupants                                                                                                                                    
                                                    ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[nextStep].addCharacterToBlockOccupants(m_unique_character_id);//adds character id as part of block list of occupants                                            
                                                    m_block_id = nextStep;
                                                    deductEnergyBasedOnTerrain();
                                                    crossedRoom = true;

                                                    //if character newly arrived block is still not target block id and last action item in queue is to reach target block id, then we do not remove it.
                                                    //needed for scenario where character starts at shared block and has only 1 action item in queue to reach to target block id in different room. without below, the last action will be removed and character will not move.
                                                    if (m_block_id != targetBlockID && m_action_queue.getQueue().Count == 1 && m_action_queue.getQueue()[0].getGuidForAction() == targetBlockID) { }
                                                    else { m_action_queue.removeAction(index); }//action completed, remove from index

                                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " crossed room to block " + m_block_id + " in room ID " + ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id));

                                                    break;
                                                }
                                                else
                                                {
                                                    //character waits for one turn
                                                }
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

                                            m_action_queue.addAction(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, currentPriority - 2, ConstantClass.VARIABLE_FOR_ACTION_NONE, chosenBlockOnCharRoom));
                                            m_action_queue.addAction(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, currentPriority - 1, ConstantClass.VARIABLE_FOR_ACTION_NONE, chosenBlockOnAdjRoom));
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
                                    Guid nextStep = searchPath.returnNextBlockGuidToMove();

                                    //check if nextstep is occupied, if so char waits one turn . if not, char moves.
                                    if (ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[nextStep].isOccupantListEmpty())
                                    {
                                        ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].removeCharacterFromBlockOccupants(m_unique_character_id);//remove character id from previuos block list of occupants                                                                                                                        
                                        ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[nextStep].addCharacterToBlockOccupants(m_unique_character_id);//adds character id as part of block list of occupants                                            
                                        m_block_id = nextStep;
                                        ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " moves to block " + m_block_id);
                                        deductEnergyBasedOnTerrain();
                                        //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is finding " + targetBlockID + " with priority " + (m_action_queue.getQueue()[index].getPriority() - 1));
                                    }
                                    else
                                    {
                                        //character waits one turn
                                    }
                                }
                            }
                            else
                            {
                                ConstantClass.LOGGER.writeToGameLog(targetBlockID + " is not a blockID. Removing action.");
                                m_action_queue.removeAction(index); //action completed, remove from index
                                updateAction(); //take next action since character move action completed beginning of this round.
                            }
                        }

                        //-------------------ACTION: GATHER ---------------------//

                        else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.GATHER) //action to gather resources
                        {
                            Guid targetBlockWithResource = m_action_queue.getQueue()[index].getGuidForAction();                            
                            //0.1 if targetblockwithResource is Guid.empty
                            if (targetBlockWithResource == Guid.Empty)
                            {
                                //0.2 then check in memory if there are any historic references
                                MemoryBitClass memory = new MemoryBitClass(Guid.Empty, ConstantClass.CHARACTER_ACTIONS.GATHER, new GameTimeClass(), 0);
                                MemoryBitClass retrievedMem = m_memoryBank.retrieveMemoryBitWithActionOnlyIfExistsInMemory(memory, ConstantClass.MEMORY.BOTH);
                                if (!retrievedMem.isEmpty)
                                {
                                    //0.3 if yes, then go to that block
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " tries to recall from memory last gathering location.");
                                    targetBlockWithResource = retrievedMem.getIDOfSomething();                                    
                                }
                                else
                                {
                                    //TODO: 0.4 if no, then perform ACTION.SCAN for resources
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " scans around for food.");
                                }
                            }
                            
                            int currentPriority = m_action_queue.getQueue()[index].getPriority();

                            if (targetBlockWithResource != Guid.Empty) //there is a targetblock that has resource
                            {
                                //1. check if there are resources available in block inventory
                                if (!ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[targetBlockWithResource].existsResourceInInventory())
                                {
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " cannot gather resources since block " + m_block_id + " has no resources.");

                                    MemoryBitClass memory = new MemoryBitClass(targetBlockWithResource, ConstantClass.CHARACTER_ACTIONS.GATHER, new GameTimeClass(), 0);
                                    m_memoryBank.removeMemoryFromShortLongTerm(memory, outputPersonGUID());

                                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_FOR_GATHERING);
                                    m_action_queue.removeAction(index); //action completed, remove from index
                                }
                                //2. check if character inventory is not full
                                else if (m_inventory.getInventorySize() == ConstantClass.INVENTORY_MAX_CHAR_CAP)
                                {
                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " cannot gather resources since character inventory is full.");
                                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_FOR_GATHERING);
                                    m_action_queue.removeAction(index); //action completed, remove from index
                                }
                                //3. check if character is on target block, if not then go find block
                                else if (m_block_id != targetBlockWithResource)
                                {
                                    m_action_queue.addAction(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, currentPriority - 1, ConstantClass.VARIABLE_FOR_ACTION_NONE, targetBlockWithResource));
                                }
                                //4. if all okay, deduct block inventory to character based on character's gather skill rate
                                //5. deduct energy
                                //6. remove action from queue
                                else
                                {
                                    ResourceObjectClass resourceGathered = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].reduceBlockInventory(ConstantClass.CHAR_SKILLS_GATHER_RATE);

                                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is gathering " + resourceGathered.getQuantity() + " resource(s) from block " + m_block_id + ".");
                                    m_inventory.addItemToInventory(resourceGathered, resourceGathered.getQuantity());
                                    m_memoryBank.addMemoryToShortTerm(new MemoryBitClass(m_block_id, ConstantClass.CHARACTER_ACTIONS.GATHER, ConstantClass.gameTime, ConstantClass.CHARACTER_MEMORY_GATHER_PRIORITY));

                                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_FOR_GATHERING);
                                    m_action_queue.removeAction(index); //action completed, remove from index
                                }
                            }
                        }

                        //-------------------ACTION: IDLE ---------------------//

                        else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.IDLE) //action to gather resources
                        {
                            m_action_queue.removeAction(index); //action completed, remove from index
                        }

                        //-------------------ACTION: SCAN ---------------------//

                        else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.SCAN) //action to scan
                        {
                            call getBlocksWithinRadius() and save whatever character scans into memory
                        }
                        //-------------------ACTION: XXXXX ---------------------//

                        //add new actions here              
                        //m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS
                    }
                    else
                    {
                        deductEnergyBasedOnTerrain();
                    }
                    OnActionUpdated(); //raise event
                }
                else //character is dead
                {
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is dead.");
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        private void updateCharacterStats() //update charater stats
        {
            double famishThres = (ConstantClass.CHARACTER_SATIETY_THRESHOLDS[(int)ConstantClass.CHARACTER_SATIETY_STATUS.FAMISHED] / 100.0) * m_stats.getHungerRate().getMaxValue();
            double starvingThres = (ConstantClass.CHARACTER_SATIETY_THRESHOLDS[(int)ConstantClass.CHARACTER_SATIETY_STATUS.STARVING] / 100.0) *m_stats.getHungerRate().getMaxValue();
            double hungryThres = (ConstantClass.CHARACTER_SATIETY_THRESHOLDS[(int)ConstantClass.CHARACTER_SATIETY_STATUS.HUNGRY] / 100.0) *m_stats.getHungerRate().getMaxValue();
            double fullThres = (ConstantClass.CHARACTER_SATIETY_THRESHOLDS[(int)ConstantClass.CHARACTER_SATIETY_STATUS.FULL] / 100.0) *m_stats.getHungerRate().getMaxValue();


            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (m_stats.getLifeStatus() == ConstantClass.CHARACTER_LIFE_STATUS.ALIVE)
            {
                /*UPDATE BIOLOGICAL DETERIORATION*/
                if (m_stats.getHungerRate().getCurrentValue() >= famishThres) //current = max --> hunger state
                {
                    //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is FAMISHED.");
                    m_stats.setHungerStatus(ConstantClass.CHARACTER_SATIETY_STATUS.FAMISHED);
                }
                else if (m_stats.getHungerRate().getCurrentValue() >= starvingThres)
                {
                    //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is STARVING.");
                    m_stats.setHungerStatus(ConstantClass.CHARACTER_SATIETY_STATUS.STARVING);
                }
                else if (m_stats.getHungerRate().getCurrentValue() >= hungryThres)
                {
                    //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is HUNGRY.");
                    m_stats.setHungerStatus(ConstantClass.CHARACTER_SATIETY_STATUS.HUNGRY);
                }
                else if (m_stats.getHungerRate().getCurrentValue() >= fullThres)
                {
                    //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is FULL.");
                    m_stats.setHungerStatus(ConstantClass.CHARACTER_SATIETY_STATUS.FULL);
                }

                if (m_stats.getSleepRate().getCurrentValue() == m_stats.getSleepRate().getMaxValue())
                {
                    //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is SLEEPY.");
                    m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY);
                }
                else if (m_stats.isEnergyAtMax())
                {
                    //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is AWAKE.");
                    m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE);
                }
                if (m_stats.getEnergy().getCurrentValue() == 0)
                {
                    //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is TIRED.");
                    m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.TIRED);
                }

                /*DECISIONS BASED ON BIOLOGICAL NEEDS*/
                switch (m_stats.getHungerStatus())
                {
                    case ConstantClass.CHARACTER_SATIETY_STATUS.HUNGRY:
                        m_action_queue.addAction(new ActionClass(ConstantClass.CHARACTER_ACTIONS.EAT, ConstantClass.ACTION_EAT_PRIORITY, ConstantClass.HOURS_FOR_EATING * ConstantClass.GAME_SPEED, Guid.Empty));
                        m_stats.modifyHP(ConstantClass.CHARACTER_SATIETY_PENALITIES[(int)ConstantClass.CHARACTER_SATIETY_STATUS.HUNGRY]);
                        //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " HP has been deducted (" + ConstantClass.CHARACTER_SATIETY_PENALITIES[(int)ConstantClass.CHARACTER_SATIETY_STATUS.HUNGRY] + ") due to HUNGRY status.");
                        break;
                    case ConstantClass.CHARACTER_SATIETY_STATUS.STARVING:
                        m_action_queue.addAction(new ActionClass(ConstantClass.CHARACTER_ACTIONS.EAT, ConstantClass.ACTION_EAT_PRIORITY, ConstantClass.HOURS_FOR_EATING * ConstantClass.GAME_SPEED, Guid.Empty));
                        m_stats.modifyHP(ConstantClass.CHARACTER_SATIETY_PENALITIES[(int)ConstantClass.CHARACTER_SATIETY_STATUS.STARVING]);
                        //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " HP has been deducted (" + ConstantClass.CHARACTER_SATIETY_PENALITIES[(int)ConstantClass.CHARACTER_SATIETY_STATUS.STARVING] + ") due to STARVING status.");
                        break;
                    case ConstantClass.CHARACTER_SATIETY_STATUS.FAMISHED:
                        m_action_queue.addAction(new ActionClass(ConstantClass.CHARACTER_ACTIONS.EAT, ConstantClass.ACTION_EAT_PRIORITY, ConstantClass.HOURS_FOR_EATING * ConstantClass.GAME_SPEED, Guid.Empty));
                        m_stats.modifyHP(ConstantClass.CHARACTER_SATIETY_PENALITIES[(int)ConstantClass.CHARACTER_SATIETY_STATUS.FAMISHED]);
                        //ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " HP has been deducted (" + ConstantClass.CHARACTER_SATIETY_PENALITIES[(int)ConstantClass.CHARACTER_SATIETY_STATUS.FAMISHED] + ") due to FAMISHED status.");
                        break;
                }

                if (m_stats.getSleepStatus() == ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY)
                {
                    m_action_queue.addAction(new ActionClass(ConstantClass.CHARACTER_ACTIONS.SLEEP, ConstantClass.ACTION_SLEEP_PRIORITY, ConstantClass.MINIMUM_NUMBER_OF_SLEEP_HOURS * ConstantClass.MINUTES_IN_ONE_HOUR, Guid.Empty));
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_WHEN_SLEEPY); //if sleepy , start deducting energy                    
                }
                if (m_stats.getSleepStatus() == ConstantClass.CHARACTER_SLEEP_STATUS.TIRED)
                {
                    m_action_queue.addAction(new ActionClass(ConstantClass.CHARACTER_ACTIONS.REST, ConstantClass.ACTION_REST_PRIORITY, ConstantClass.ENERGY_ADD_WHEN_REST * ConstantClass.MINUTES_IN_ONE_HOUR, Guid.Empty));
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_WHEN_TIRED); //if tired , start deducting energy
                }
            }
            else 
            {
                //character is dead - no actions
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        private Guid backTrackToAdjacentRoom (Guid endRoomID, Guid startRoomID, List<Guid> backtrackedRooms) //recursively backtracks from startRoomID to targetRoomID. Return adjacent room ID of targetRoomID, else return Guid.Emptyo
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {

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
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }
            return Guid.Empty;
        }

        public void FOR_DEBUG_addActionInQueue(ActionClass action)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            
            m_action_queue.addAction(action); //searches

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        private void deductEnergyBasedOnTerrain()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            int additionalTerrainFatigue = ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS.getMappingTable()[ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getTerrainID()].getFatigueCost();
            m_stats.modifyEnergy(ConstantClass.ENERGY_COST_FOR_WALKING + additionalTerrainFatigue); //for now start walking

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        private string printCharacter()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string output = "";

            /* Character    [Guid]
             * Birth Date   [date]
             * Block ID     [Guid]
             * Room ID      [Guid]
             * Action:
             *      - action1
             *      - action2
             * Stats:
             *      - Hunger status:    [status]
             *      - Hunger rate:      [rate]
             *      - Sleep status:     [status]
             *      - Sleep rate:       [rate]
             *      - Energy:           [energy]
             * Inventory:
             *      - item1 x quantity1
             *      - item2 x quantity2
             */

            //output += ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id)].printRoom(false,m_unique_character_id.ToString());
            /*output += m_unique_character_id+"|"+"Character ID|" + m_unique_character_id + "\n" +
                      m_unique_character_id + "|" + "Birth date|" + m_birthDate + "\n" +
                      m_unique_character_id + "|" + "Block ID:|" + m_block_id + "\n" +
                      m_unique_character_id + "|" + "Room ID:|" + ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id) + "\n" +
                      m_unique_character_id + "|" + "Action:\n" + m_action_queue.printQueue(m_unique_character_id.ToString()) + "\n" +
                      m_stats.printStats(m_unique_character_id.ToString()) + "\n" +
                      m_inventory.printInventoryList() + "\n";*/
            ConstantClass.LOGGER.writeToCharLog("Character ID| " + m_unique_character_id, m_unique_character_id.ToString());
            ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id)].printRoom(false, m_unique_character_id.ToString());
            ConstantClass.LOGGER.writeToCharLog("Birth date|" + m_birthDate, m_unique_character_id.ToString());
            ConstantClass.LOGGER.writeToCharLog("Death date|" + m_stats.getDeathDate(), m_unique_character_id.ToString());
            ConstantClass.LOGGER.writeToCharLog("Block ID| " + m_block_id, m_unique_character_id.ToString());
            ConstantClass.LOGGER.writeToCharLog("Room ID|" + ConstantClass.GET_ROOMID_BASED_BLOCKID(m_block_id), m_unique_character_id.ToString());
            m_action_queue.printQueue(m_unique_character_id.ToString());
            m_stats.printStats(m_unique_character_id.ToString());
            m_inventory.printInventoryList(m_unique_character_id.ToString());
            m_memoryBank.printMemory(m_unique_character_id.ToString());

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH 

            return output;
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
            updateCharacterStats(); //update character stats (hunger, sleep, etc) per tick
            m_memoryBank.updateMemoryExpirationsAndPriority(outputPersonGUID()); //updates memory expirations
            
            ConstantClass.LOGGER.writeToCharLog(printCharacter(), m_unique_character_id.ToString());
            //ConstantClass.LOGGER.writeToGameLog("----------------------------------------------------------------------------");

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
