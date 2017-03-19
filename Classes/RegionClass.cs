using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class RegionClass
    {
        //room [row,column]

        /*VARIABLES*/
        private Guid m_region_id;
        private List<Guid> m_list_of_rooms;

        /*SET AND GET*/
        public Guid getRegionID() { return m_region_id; }
        public List<Guid> getListOfRooms() { return m_list_of_rooms; }

        /*CONSTRUCTOR*/
        public RegionClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_list_of_rooms = new List<Guid>();
            m_region_id = Guid.NewGuid();

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }        

        /*METHODS*/
        public void addRoom(Guid roomID) //add a new room to the region
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_list_of_rooms.Add(roomID); //adds room to the list
            ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[roomID].setRegionID(m_region_id); //updates region id for the room

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void linkTwoRoomsWithExit(RoomClass room1, ConstantClass.EXITS exitFromRoom1, RoomClass room2, int numberOfSharedExits) //links 2 rooms based on defined number of exits (min = 1, max = smallest room size)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            //TODO: Must check if block is wall then it cannot go as common block                        

            int randomBlock;
            int room1Size, room2Size;

            List<int> blocks = new List<int>();

            try
            {

                for (int i = 0; i < Math.Min(room1.getSize(), room2.getSize()); i++)
                {
                    blocks.Add(i); //add all block indices from smallest room to list
                }

                //1. find common blocks between two rooms minus number of walls
                room1Size = room1.getSize();
                room2Size = room2.getSize();

                //go over room1 and check if there are walls. if so, exclude it from shared blocks list
                for (int i = 0; i < room1.getSize(); i++)
                {
                    if (exitFromRoom1 == ConstantClass.EXITS.NORTH) //room2 is north of room1 --> go over room1 [0,i] and room2[getSize-1,i]
                    {
                        if (room1.getRoom()[0, i].getBuildingID() != Guid.Empty && ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[room1.getRoom()[0, i].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //if block is a wall
                        {
                            if (blocks.Contains(i)) { blocks.Remove(i); }
                            room1Size--;
                        }
                    }
                    else if (exitFromRoom1 == ConstantClass.EXITS.SOUTH) //room2 is south of room1 --> go over room1[getSize-1,i] and room2[0,i]
                    {
                        if (room1.getRoom()[room1.getSize() - 1, i].getBuildingID() != Guid.Empty && ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[room1.getRoom()[room1.getSize() - 1, i].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //if block is a wall
                        {
                            if (blocks.Contains(i)) { blocks.Remove(i); }
                            room1Size--;
                        }
                    }
                    else if (exitFromRoom1 == ConstantClass.EXITS.WEST) //room2 is west of room1 --> go over room1[i,0] and room2[i,getSize-1]
                    {
                        if (room1.getRoom()[i, 0].getBuildingID() != Guid.Empty && ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[room1.getRoom()[i, 0].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //if block is a wall
                        {
                            if (blocks.Contains(i)) { blocks.Remove(i); }
                            room1Size--;
                        }
                    }
                    else if (exitFromRoom1 == ConstantClass.EXITS.EAST) //room2 is east of room1 --> go over room1[i,getSize-1] and room2[i,0]
                    {
                        if (room1.getRoom()[i, room1.getSize() - 1].getBuildingID() != Guid.Empty && ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[room1.getRoom()[i, room1.getSize() - 1].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //if block is a wall
                        {
                            if (blocks.Contains(i)) { blocks.Remove(i); }
                            room1Size--;
                        }
                    }
                }

                for (int i = 0; i < room2.getSize(); i++) //go over room2 and check if there are walls. if so, exclude it from blocks list
                {
                    if (exitFromRoom1 == ConstantClass.EXITS.NORTH) //room2 is north of room1 --> go over room1 [0,i] and room2[getSize-1,i]
                    {
                        if (room2.getRoom()[room2.getSize() - 1, i].getBuildingID() != Guid.Empty && ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[room2.getRoom()[room2.getSize() - 1, i].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //if block is a wall
                        {
                            if (blocks.Contains(i)) { blocks.Remove(i); }
                            room2Size--;
                        }
                    }
                    else if (exitFromRoom1 == ConstantClass.EXITS.SOUTH) //room2 is south of room1 --> go over room1[getSize-1,i] and room2[0,i]
                    {
                        if (room2.getRoom()[0, i].getBuildingID() != Guid.Empty && ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[room2.getRoom()[0, i].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //if block is a wall
                        {
                            if (blocks.Contains(i)) { blocks.Remove(i); }
                            room2Size--;
                        }
                    }
                    else if (exitFromRoom1 == ConstantClass.EXITS.WEST) //room2 is west of room1 --> go over room1[i,0] and room2[i,getSize-1]
                    {
                        if (room2.getRoom()[i, room2.getSize() - 1].getBuildingID() != Guid.Empty && ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[room2.getRoom()[i, room2.getSize() - 1].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //if block is a wall
                        {
                            if (blocks.Contains(i)) { blocks.Remove(i); }
                            room2Size--;
                        }
                    }
                    else if (exitFromRoom1 == ConstantClass.EXITS.EAST) //room2 is east of room1 --> go over room1[i,getSize-1] and room2[i,0]
                    {
                        if (room2.getRoom()[i, 0].getBuildingID() != Guid.Empty && ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[room2.getRoom()[i, 0].getBuildingID()].getType() == ConstantClass.BUILDING.WALL) //if block is a wall
                        {
                            if (blocks.Contains(i)) { blocks.Remove(i); }
                            room2Size--;
                        }
                    }
                }

                int totalCommonBlocks = Math.Min(room1Size, room2Size); //total # of common blocks is the smallest room size            

                if (totalCommonBlocks == 0 || blocks.Count == 0) { return; } //if there are no common blocks or block list is empty, then return without doing anything
                if (totalCommonBlocks < numberOfSharedExits) { numberOfSharedExits = totalCommonBlocks; } //cannot define more shared exits can what is possible

                GuidPairClass blockPair = null;
                List<GuidPairClass> listOfSharedBlocksRoom1ToRoom2 = new List<GuidPairClass>();
                List<GuidPairClass> listOfSharedBlocksRoom2ToRoom1 = new List<GuidPairClass>();
                GuidPairClass room1Room2Pairs = new GuidPairClass(room1.getUniqueRoomID(), room2.getUniqueRoomID());
                GuidPairClass room2Room1Pairs = new GuidPairClass(room2.getUniqueRoomID(), room1.getUniqueRoomID());

                if (numberOfSharedExits >= 1 && numberOfSharedExits <= totalCommonBlocks) //number of shared exits must be greater than 1 and less than equal to smallest room size
                {
                    //2. randomize and choose the blocks to become exit                
                    for (int i = 0; i < numberOfSharedExits; i++)
                    {
                        randomBlock = blocks[ConstantClass.RANDOMIZER.produceInt(1, 100) % blocks.Count];//randomizing common blocks to pick up # exits    
                        blocks.Remove(randomBlock); //removes index that was picked. blocks list will consists of indices not picked

                        switch (exitFromRoom1)
                        {
                            case (ConstantClass.EXITS.NORTH): //room2 is north of room1
                                {
                                    room1.getRoom()[0, randomBlock].setExit(room2.getRoom()[room2.getSize() - 1, randomBlock].getUniqueBlockID(), ConstantClass.EXITS.NORTH); //room1 block's north exit = room2 block id
                                    room2.getRoom()[room2.getSize() - 1, randomBlock].setExit(room1.getRoom()[0, randomBlock].getUniqueBlockID(), ConstantClass.EXITS.SOUTH); //room2 block's south exit = room1 block id

                                    //room1 to room2
                                    blockPair = new GuidPairClass(room1.getRoom()[0, randomBlock].getUniqueBlockID(), room2.getRoom()[room2.getSize() - 1, randomBlock].getUniqueBlockID());
                                    listOfSharedBlocksRoom1ToRoom2.Add(blockPair);

                                    //room2 to room1
                                    blockPair = new GuidPairClass(room2.getRoom()[room2.getSize() - 1, randomBlock].getUniqueBlockID(), room1.getRoom()[0, randomBlock].getUniqueBlockID());
                                    listOfSharedBlocksRoom2ToRoom1.Add(blockPair);

                                    break;
                                }
                            case (ConstantClass.EXITS.SOUTH): //room2 is south of room1
                                {
                                    room1.getRoom()[room1.getSize() - 1, randomBlock].setExit(room2.getRoom()[0, randomBlock].getUniqueBlockID(), ConstantClass.EXITS.SOUTH); //room1 block's south exit = room2 block id
                                    room2.getRoom()[0, randomBlock].setExit(room1.getRoom()[room1.getSize() - 1, randomBlock].getUniqueBlockID(), ConstantClass.EXITS.NORTH); //room2 block's north exit = room1 block id

                                    //room1 to room2
                                    blockPair = new GuidPairClass(room1.getRoom()[room1.getSize() - 1, randomBlock].getUniqueBlockID(), room2.getRoom()[0, randomBlock].getUniqueBlockID());
                                    listOfSharedBlocksRoom1ToRoom2.Add(blockPair);

                                    //room2 to room1
                                    blockPair = new GuidPairClass(room2.getRoom()[0, randomBlock].getUniqueBlockID(), room1.getRoom()[room1.getSize() - 1, randomBlock].getUniqueBlockID());
                                    listOfSharedBlocksRoom2ToRoom1.Add(blockPair);

                                    break;
                                }
                            case (ConstantClass.EXITS.WEST): //room2 is west of room1
                                {
                                    room1.getRoom()[randomBlock, 0].setExit(room2.getRoom()[randomBlock, room2.getSize() - 1].getUniqueBlockID(), ConstantClass.EXITS.WEST); //room1 block's west exit = room2 block id
                                    room2.getRoom()[randomBlock, room2.getSize() - 1].setExit(room1.getRoom()[randomBlock, 0].getUniqueBlockID(), ConstantClass.EXITS.EAST); //room2 block's east exit = room1 block id

                                    //room1 to room2
                                    blockPair = new GuidPairClass(room1.getRoom()[randomBlock, 0].getUniqueBlockID(), room2.getRoom()[randomBlock, room2.getSize() - 1].getUniqueBlockID());
                                    listOfSharedBlocksRoom1ToRoom2.Add(blockPair);

                                    //room2 to room1
                                    blockPair = new GuidPairClass(room2.getRoom()[randomBlock, room2.getSize() - 1].getUniqueBlockID(), room1.getRoom()[randomBlock, 0].getUniqueBlockID());
                                    listOfSharedBlocksRoom2ToRoom1.Add(blockPair);

                                    break;
                                }
                            case (ConstantClass.EXITS.EAST): //room2 is east of room1
                                {
                                    room1.getRoom()[randomBlock, room1.getSize() - 1].setExit(room2.getRoom()[randomBlock, 0].getUniqueBlockID(), ConstantClass.EXITS.EAST); //room1 block's east exit = room2 block id
                                    room2.getRoom()[randomBlock, 0].setExit(room1.getRoom()[randomBlock, room1.getSize() - 1].getUniqueBlockID(), ConstantClass.EXITS.WEST); //room2 block's west exit = room1 block id

                                    //room1 to room2
                                    blockPair = new GuidPairClass(room1.getRoom()[randomBlock, room1.getSize() - 1].getUniqueBlockID(), room2.getRoom()[randomBlock, 0].getUniqueBlockID());
                                    listOfSharedBlocksRoom1ToRoom2.Add(blockPair);

                                    //room2 to room1
                                    blockPair = new GuidPairClass(room2.getRoom()[randomBlock, 0].getUniqueBlockID(), room1.getRoom()[randomBlock, room1.getSize() - 1].getUniqueBlockID());
                                    listOfSharedBlocksRoom2ToRoom1.Add(blockPair);

                                    break;
                                }
                        } //end switch                    

                    }//for loop

                    ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable().Add(room1Room2Pairs, listOfSharedBlocksRoom1ToRoom2);
                    ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable().Add(room2Room1Pairs, listOfSharedBlocksRoom2ToRoom1);
                }
                else
                {
                    throw new Exception("Invalid numberOfsharedExits");
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public string printRoomLinks()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string output = "";
            Dictionary<GuidPairClass, List<GuidPairClass>> sharedBlocks = ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable();

            foreach (KeyValuePair<GuidPairClass,List<GuidPairClass>> pair in sharedBlocks)
            {
                foreach (GuidPairClass guidpair in pair.Value)
                {
                    //output += "room id " + pair.Key.m_guid1 + " block " + guidpair.m_guid1 + "is linked to \nroom id " + pair.Key.m_guid2 + " block " + guidpair.m_guid2 + "\n";
                    output += "[" + ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[guidpair.m_guid1].getPosition().getPositionX() + "," + ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[guidpair.m_guid1].getPosition().getPositionY() + "]\t\t\t" + pair.Key.m_guid1 + "\t\t\t" + guidpair.m_guid1 + "\n";
                    output += "[" + ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[guidpair.m_guid2].getPosition().getPositionX() + "," + ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[guidpair.m_guid2].getPosition().getPositionY() + "]\t\t\t" + pair.Key.m_guid2 + "\t\t\t" + guidpair.m_guid2 + "\n";
                }                
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return output;
        }
        
        public string printAllRoomsInRegion()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string output = "";

            foreach (Guid room in m_list_of_rooms)
            {
                output += "Room ID:\t\t" + room + "\n";
                output += ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[room].printEntireRoom(true,null) + "\n";
            }

            return output;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }        

        /*EVENT HANLDER*/
        public void OnActionUpdated(object source, EventArgs args)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            Console.Clear();

            foreach (Guid id in m_list_of_rooms)
            {
                Console.WriteLine(ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[id].printEntireRoom(false,""));
            }

            Console.WriteLine(ConstantClass.gameTime.ToString());

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

    }
}
