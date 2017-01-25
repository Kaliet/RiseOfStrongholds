using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class RoomClass //room consisting of x*x blocks
    {
        /*VARIABLES*/
        private BlockClass[,] m_Room;
        private int m_size;

        /*GET & SET*/
        public BlockClass[,] getRoom() { return m_Room; }
        public int getSize() { return m_size; }

        /*CONSTRUCTORS*/
        public RoomClass(int size)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (size <= 0) throw new Exception("Invalid room size");
            else
            {
                m_size = size;
                m_Room = new BlockClass[size, size];
            }

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
            PositionClass pos = new PositionClass(0, 0);
            
            //initialize all blocks in 2d array
            for (int i = 0; i < m_size ; i++)
                for (int j = 0; j < m_size ; j++)
                {
                    pos.setPositionX(i);
                    pos.setPositionY(j);
                    m_Room[i, j] = new BlockClass(pos, terrainType.getUniqueTerrainID());
                }

            //links all rooms together horizontally
            if (m_size == 1) { }//no need to link 1x1 room
            else if (m_size >= 2) //at least 2 x 2
            {
                for (int i = 0; i < m_size ; i++) //for each row                    0,1,2
                    for (int j = 0; j <= m_size - 2; j++) //for each column         0,1
                    {
                        linkTwoBlocksWithExit(m_Room[i, j], m_Room[i, j + 1], ConstantClass.EXITS.EAST);
                    }
            }
        }

        public void linkTwoBlocksWithExit (BlockClass room1 , BlockClass room2, ConstantClass.EXITS exitFromRoom1) //link two rooms based on exit from room1 perspective
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
           
            switch (exitFromRoom1)
            {                
                case ConstantClass.EXITS.NORTH: //room2 is located north of room1
                    room1.setExit(room2.getUniqueBlockID(), ConstantClass.EXITS.NORTH); //room1 north exit = room2 id
                    room2.setExit(room1.getUniqueBlockID(), ConstantClass.EXITS.SOUTH); //room2 south exit = room1 id
                    break;
                case ConstantClass.EXITS.SOUTH: //room2 is located south of room1
                    room1.setExit(room2.getUniqueBlockID(), ConstantClass.EXITS.SOUTH); //room1 south exit = room2 id
                    room2.setExit(room1.getUniqueBlockID(), ConstantClass.EXITS.NORTH); //room2 norht exit = room1 id
                    break;
                case ConstantClass.EXITS.WEST: //room2 is located west of room1
                    room1.setExit(room2.getUniqueBlockID(), ConstantClass.EXITS.WEST); //room1 west exit = room2 id
                    room2.setExit(room1.getUniqueBlockID(), ConstantClass.EXITS.EAST); //room2 east exit = room1 id
                    break;
                case ConstantClass.EXITS.EAST: //room2 is located west of room1
                    room1.setExit(room2.getUniqueBlockID(), ConstantClass.EXITS.EAST); //room1 east exit = room2 id
                    room2.setExit(room1.getUniqueBlockID(), ConstantClass.EXITS.WEST); //room2 west exit = room1 id
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
                    output += "x";
                    if (m_Room[i,j].) //check if exit then mark in string drawing
                }
                output += "\n";
            }

            return output;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
