using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class RegionClass
    {
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
            
            //1. find common blocks between two rooms
            int totalCommonBlocks = Math.Min(room1.getSize(), room2.getSize()); //total # of common blocks is the smallest room size            
            int randomBlock;

            List<int> blocks = new List<int>();

            for (int i = 0; i < totalCommonBlocks; i++)
            {
                blocks.Add(i); //add all block indices to list
            }

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
                                break;
                            }
                        case (ConstantClass.EXITS.SOUTH): //room2 is south of room1
                            {
                                room1.getRoom()[room1.getSize() - 1, randomBlock].setExit(room2.getRoom()[0, randomBlock].getUniqueBlockID(), ConstantClass.EXITS.SOUTH); //room1 block's south exit = room2 block id
                                room2.getRoom()[0, randomBlock].setExit(room1.getRoom()[room1.getSize() - 1, randomBlock].getUniqueBlockID(), ConstantClass.EXITS.NORTH); //room2 block's north exit = room1 block id
                                break;
                            }
                        case (ConstantClass.EXITS.WEST): //room2 is west of room1
                            {
                                room1.getRoom()[randomBlock, 0].setExit(room2.getRoom()[randomBlock, room2.getSize() - 1].getUniqueBlockID(), ConstantClass.EXITS.WEST); //room1 block's west exit = room2 block id
                                room2.getRoom()[randomBlock, room2.getSize() - 1].setExit(room1.getRoom()[randomBlock, 0].getUniqueBlockID(), ConstantClass.EXITS.EAST); //room2 block's east exit = room1 block id
                                break;
                            }
                        case (ConstantClass.EXITS.EAST): //room2 is east of room1
                            {
                                room1.getRoom()[randomBlock, room1.getSize() - 1].setExit(room2.getRoom()[randomBlock, 0].getUniqueBlockID(), ConstantClass.EXITS.EAST); //room1 block's east exit = room2 block id
                                room2.getRoom()[randomBlock, 0].setExit(room1.getRoom()[randomBlock, room1.getSize() - 1].getUniqueBlockID(), ConstantClass.EXITS.WEST); //room2 block's west exit = room1 block id
                                break;
                            }
                    }
                }
            }
            else
            {
                throw new Exception("Invalid numberOfsharedExits");
            }
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*EVENT HANLDER*/
        public void OnActionUpdated(object source, EventArgs args)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            Console.Clear();

            foreach (Guid id in m_list_of_rooms)
            {
                Console.WriteLine(ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[id].printRoom());
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

    }
}
