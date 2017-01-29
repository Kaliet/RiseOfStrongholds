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

        public void linkTwoRoomsWithExit (RoomClass room1, ConstantClass.EXITS exitFromRoom1, RoomClass room2) //links 2 rooms based on defined exit 
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            int totalCommonBlocks = Math.Min(room1.getSize(), room2.getSize()); //total # of common blocks is the smallest room size
            int randomBlock = ConstantClass.RANDOMIZER.produceInt(1, 100) % totalCommonBlocks; //randomize a block

            //1. find common blocks between two rooms
            //2. randomize and choose one of the blocks to become exit

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
