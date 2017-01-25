using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class BlockClass
    {
        /* VARIABLES */
        private Guid m_unique_block_id;
        private Guid m_NorthExit;
        private Guid m_SouthExit;
        private Guid m_EastExit;
        private Guid m_WestExit;
        private GameTimeClass m_createDate;
        private Guid m_terrain_id;
        private PositionClass m_position;
        private BlockStatsClass m_stats;
        private Guid m_room_id;
        private List<Guid> m_list_of_occupants;        

        /*GET & SET*/
        public Guid getUniqueBlockID() { return m_unique_block_id; }        
        public GameTimeClass getCreateDate() { return m_createDate; }
        public Guid getTerrainID() { return m_terrain_id; }
        public Guid getRoomID() { return m_room_id; }
        public PositionClass getPosition() { return m_position; }
        public BlockStatsClass getStats() { return m_stats; }
        public List<Guid> getListOfOccupants() { return m_list_of_occupants; }
        public Guid[] getAllExits() //return array of all exits
        {
            Guid[] exits = new Guid[4];
            exits[(int)ConstantClass.EXITS.NORTH] = m_NorthExit;
            exits[(int)ConstantClass.EXITS.SOUTH] = m_SouthExit;
            exits[(int)ConstantClass.EXITS.EAST] = m_EastExit;
            exits[(int)ConstantClass.EXITS.WEST] = m_WestExit;

            return exits;
        }
        
        public void setTerrainType(Guid terrain) { m_terrain_id = terrain; }
        public void setRoom(Guid room) { m_room_id = room; }
        public void setAllExits(Guid n, Guid s, Guid w, Guid e)
        {
            m_NorthExit = n;
            m_SouthExit = s;
            m_EastExit = e;
            m_WestExit = w;
        }

        public void setExit(Guid neighborBlockID, ConstantClass.EXITS exit)//add exit without changing the exits of others.
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            switch (exit)
            {
                case ConstantClass.EXITS.NORTH:
                    m_NorthExit = neighborBlockID;
                    break;
                case ConstantClass.EXITS.SOUTH:
                    m_SouthExit = neighborBlockID;
                    break;
                case ConstantClass.EXITS.WEST:
                    m_WestExit = neighborBlockID;
                    break;
                case ConstantClass.EXITS.EAST:
                    m_EastExit = neighborBlockID;
                    break;
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*CONSTRUCTORS*/
        public BlockClass(PositionClass position, Guid terrainUniqueID)
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_createDate = new GameTimeClass(ConstantClass.gameTime);
            m_unique_block_id = Guid.NewGuid();

            m_position = position;           
            m_terrain_id = terrainUniqueID;
            setAllExits(Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty);
            m_list_of_occupants = new List<Guid>();
            m_stats = new BlockStatsClass();

            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable().Add(m_unique_block_id, this); //maps to mapping table

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        public string printAllAvailableExits()
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            string output = "";

            if (m_NorthExit == Guid.Empty && m_SouthExit == Guid.Empty && m_WestExit == Guid.Empty && m_EastExit == Guid.Empty) output += "None";
            else
            {
                if (m_NorthExit != Guid.Empty) { output += "North,"; }
                if (m_SouthExit != Guid.Empty) { output += "South,"; }
                if (m_WestExit != Guid.Empty) { output += "West,"; }
                if (m_EastExit != Guid.Empty) { output += "East"; }
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return output;
        }

        public bool existsNorthExit() //return true if there is exit to north, return false if no exit
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            return !(m_NorthExit == Guid.Empty);                        
        }

        public bool existsSouthExit() //return true if there is exit to south, return false if no exit
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            return !(m_SouthExit == Guid.Empty);            
        }

        public bool existsWestExit() //return true if there is exit to west, return false if no exit
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            return !(m_WestExit == Guid.Empty);            
        }

        public bool existsEastExit() //return true if there is exit to east, return false if no exit
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            return !(m_EastExit == Guid.Empty);            
        }
    }
}
