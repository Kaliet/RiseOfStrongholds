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
        private BlockClass[,] m_Room;
        private int m_size;

        /*GET & SET*/
        public BlockClass[,] getRoom() { return m_Room; }
        public int getSize() { return m_size; }
        public Guid getUniqueRoomID() { return m_room_id; }        
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

            try
            {
                for (int i = 0; i < m_size; i++)
                    for (int j = 0; j < m_size; j++)
                    {
                        m_Room[i, j] = new BlockClass(new PositionClass(i, j), terrainType.getUniqueTerrainID());
                        m_Room[i, j].setRoom(m_room_id); //links the block to the room id                   

                        ConstantClass.gameTime.GameTicked += m_Room[i, j].OnGameTicked;
                    }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void linkAllBlocksTogetherHorizontally() //links all the blocks horizontally
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
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
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void linkAllBlocksTogetherVertically() //links all the blocks vertically
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
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
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void linkTwoBlocksWithExit (BlockClass block1 , ConstantClass.EXITS exitFromBlock1,  BlockClass block2) //link two rooms based on exit from room1 perspective
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
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
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public string printRoom(bool withExits, string charID)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string output = "";
            string building = "";

            for (int i = 0; i < m_size; i++) 
            {                
                if (charID != "" && charID != null) { output = ""; }

                for (int j = 0; j < m_size; j++)
                {
                    output += "[\t";

                    if (m_Room[i, j].getBuildingID() != Guid.Empty) //there is a building here.
                    {
                        switch (ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable()[m_Room[i, j].getBuildingID()].getType())
                        {
                            case ConstantClass.BUILDING.WALL: //if building is a WALL
                                building = "W";
                                break;
                        }
                    }
                    output += building.ToString();                   
                    if (withExits)
                    {
                        if (m_Room[i, j].existsNorthExit()) { output += "N"; }
                        if (m_Room[i, j].existsSouthExit()) { output += "S"; }
                        if (m_Room[i, j].existsWestExit()) { output += "W"; }
                        if (m_Room[i, j].existsEastExit()) { output += "E"; }
                    }
                    if (!m_Room[i,j].isOccupantListEmpty()) //block is not empty, has occupants
                    {
                        output += m_Room[i, j].printOccupantList();
                    }           
                    else if (building == "") { output += " "; } //no occupants and no buildings
                    output += "\t]";
                    building = "";
                }
                if (charID == "") { output += "\n"; }
                else if (charID != null)
                {
                    ConstantClass.LOGGER.writeToCharLog("MAP|" + output, charID);
                }                
            }
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            return output;            
        }

        public BlockClass[,] getBlocksWithinRadius(Guid centerBlock, int radius) //returns back 2d block array given centerblock id and radius length
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            int startRowIndex, endRowIndex, startColumnIndex, endColumnIndex, xCenterBlock, yCenterBlock;
            int resultSize = 2 * radius + 1;
            if (resultSize >= m_size) { resultSize = m_size; }

            BlockClass[,] result = new BlockClass[resultSize, resultSize]; xxxx debug case where start point is on the side and radius is big. it should show limited area.

            //1. checks if center block is a block in room            
            if (centerBlock == Guid.Empty) return result;
            //2. checks if radius is valid number
            if (radius <= 0) return result;
            
            //3. check if centerBlock is in room
            if (ConstantClass.GET_ROOMID_BASED_BLOCKID(centerBlock) != m_room_id) return result;

            xCenterBlock = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[centerBlock].getPosition().getPositionX();
            yCenterBlock = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[centerBlock].getPosition().getPositionY();
            
            if (xCenterBlock - radius <= 0) { startRowIndex = 0; } //if we are on the top edge 
            else { startRowIndex = xCenterBlock - radius; }

            if (xCenterBlock + radius >= m_size - 1) { endRowIndex = m_size - 1; } //if we are on the bottom edge
            else { endRowIndex = xCenterBlock + radius; }

            if (yCenterBlock - radius <= 0) { startColumnIndex = 0; } //if we are the left edge
            else { startColumnIndex = yCenterBlock - radius; }

            if (yCenterBlock + radius >= m_size - 1) { endColumnIndex = m_size - 1; } //if we are on the right edge
            else { endColumnIndex = yCenterBlock + radius; }

            int counter = startColumnIndex;

            for (int i = 0; i < resultSize; i++)  //fill out result 2d array
            {
                for (int j = 0; j < resultSize; j++)
                {
                    result[i, j] = new BlockClass(ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[m_room_id].getRoom()[startRowIndex, counter]);
                    counter++;
                }
                startRowIndex++;
                counter = startColumnIndex;
            }

            return result;
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
