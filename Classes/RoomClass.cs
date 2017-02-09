using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    /* 
     *    [0,0] [0,1] [0,2] [0,size-1]
     *    [1,0]   .     .     .
     *    [2,0]   .     .     .
     *    [size-1,0]
     * 
     * 
     */
    public class RoomClass //room consisting of x*x blocks
    {
        /*VARIABLES*/
        private Guid m_room_id;
        private Guid m_region_id;
        private List<Guid> m_neighboring_rooms;
        private BlockClass[,] m_Room;
        private int m_size;

        /*GET & SET*/
        public BlockClass[,] getRoom() { return m_Room; }
        public int getSize() { return m_size; }
        public Guid getUniqueRoomID() { return m_room_id; }
        public List<Guid> getNeighboringRooms() { return m_neighboring_rooms; }
        public Guid getRegionID() { return m_region_id; }
        public void setRegionID(Guid regionID) { m_region_id = regionID; }

        /*CONSTRUCTORS*/
        public RoomClass(int size)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (size <= 0) throw new Exception("Invalid room size");
            else
            {
                m_size = size;
                m_Room = new BlockClass[size, size];
                m_room_id = Guid.NewGuid();
                m_region_id = Guid.Empty;
                m_neighboring_rooms = new List<Guid>();
            }

            ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable().Add(m_room_id, this);            
            
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        public void initializeRoom(TerrainClass terrainType) //initializes room of one type of Terrain , define all exits to link with adjacent cells as per 2d array. All external cells have open exits
        {
            /*ex. of size 2 room
             * 
             *        |         |
             *  <-> [0,0] <-> [0,1] <->
             *        |         |
             *  <-> [1,0] <-. [1,1] <->
             *        |         |
             * *       
             */
            //initialize all blocks in 2d array

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            for (int i = 0; i < m_size ; i++)
                for (int j = 0; j < m_size ; j++)
                {
                    m_Room[i, j] = new BlockClass(new PositionClass(i, j), terrainType.getUniqueTerrainID());
                    m_Room[i, j].setRoom(m_room_id); //links the block to the room id                   
                }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void linkAllBlocksTogetherHorizontally() //links all the blocks horizontally
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            //links all blocks together horizontally
            if (m_size == 1) { }//no need to link 1x1 room
            else if (m_size >= 2) //at least 2 x 2
            {
                for (int i = 0; i < m_size; i++) //for each row                    0,1,2
                    for (int j = 0; j <= m_size - 2; j++) //for each column         0,1
                    {
                        if (j < m_size - 1) { linkTwoBlocksWithExit(m_Room[i, j], ConstantClass.EXITS.EAST, m_Room[i, j + 1]); }//last column cannot have east exit
                    }
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void linkAllBlocksTogetherVertically() //links all the blocks vertically
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            //links all blocks together horizontally
            if (m_size == 1) { }//no need to link 1x1 room
            else if (m_size >= 2) //at least 2 x 2
            {
                for (int i = 0; i < m_size; i++) //for each row                    0,1,2
                    for (int j = 0; j <= m_size - 1; j++) //for each column         0,1
                    {
                        if (i > 0) { linkTwoBlocksWithExit(m_Room[i, j], ConstantClass.EXITS.NORTH, m_Room[i - 1, j]); } //first row cannot have north exit                        
                    }
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void linkTwoBlocksWithExit (BlockClass block1 , ConstantClass.EXITS exitFromBlock1,  BlockClass block2) //link two rooms based on exit from room1 perspective
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
           
            switch (exitFromBlock1)
            {                
                case ConstantClass.EXITS.NORTH: //room2 is located north of room1
                    block1.setExit(block2.getUniqueBlockID(), ConstantClass.EXITS.NORTH); //room1 north exit = room2 id
                    block2.setExit(block1.getUniqueBlockID(), ConstantClass.EXITS.SOUTH); //room2 south exit = room1 id
                    break;
                case ConstantClass.EXITS.SOUTH: //room2 is located south of room1
                    block1.setExit(block2.getUniqueBlockID(), ConstantClass.EXITS.SOUTH); //room1 south exit = room2 id
                    block2.setExit(block1.getUniqueBlockID(), ConstantClass.EXITS.NORTH); //room2 norht exit = room1 id
                    break;
                case ConstantClass.EXITS.WEST: //room2 is located west of room1
                    block1.setExit(block2.getUniqueBlockID(), ConstantClass.EXITS.WEST); //room1 west exit = room2 id
                    block2.setExit(block1.getUniqueBlockID(), ConstantClass.EXITS.EAST); //room2 east exit = room1 id
                    break;
                case ConstantClass.EXITS.EAST: //room2 is located west of room1
                    block1.setExit(block2.getUniqueBlockID(), ConstantClass.EXITS.EAST); //room1 east exit = room2 id
                    block2.setExit(block1.getUniqueBlockID(), ConstantClass.EXITS.WEST); //room2 west exit = room1 id
                    break;
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public string printRoom()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string output = "";
            
            
            for (int i = 0; i < m_size; i++) 
            {
                for (int j = 0; j < m_size; j++)
                {
                    output += "|\t\t";

                    if (m_Room[i, j].getBuildingID() != Guid.Empty) //there is a building here.
                    {
                        switch (ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[m_Room[i, j].getBuildingID()].getType())
                        {
                            case ConstantClass.BUILDING.WALL: //if building is a WALL
                                output += "WALL";
                                break;
                        }
                    }
                    //if (m_Room[i, j].existsNorthExit()) { output += "N"; }
                    //if (m_Room[i, j].existsSouthExit()) { output += "S"; }
                    //if (m_Room[i, j].existsWestExit()) { output += "W"; }
                    //if (m_Room[i, j].existsEastExit()) { output += "E"; }
                    if (m_Room[i,j].getListOfOccupants().Count > 0) //block is not empty, has occupants
                    {
                        foreach (Guid id in m_Room[i, j].getListOfOccupants())// go through list and print the occupants
                        {                            
                            output += id.ToString().Substring(0, 1); 
                        }
                    }           
                    output += "\t\t|";
                }
                output += "\n";
            }
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            return output;            
        }

        /*EVENT HANDLER*/
        public void OnActionUpdated(object source, EventArgs args)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            //ConstantClass.LOGGER.writeToMapLog(printRoom()); //sometimes the room is printed first before the character moves.
            //Console.Clear();
            //Console.WriteLine(printRoom());


            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
