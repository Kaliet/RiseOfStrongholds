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

        /*GET & SET*/
        public Guid getUniqueBlockID() { return m_unique_block_id; }        
        public GameTimeClass getCreateDate() { return m_createDate; }
        public Guid getTerrainID() { return m_terrain_id; }
        public PositionClass getPosition() { return m_position; }
        public BlockStatsClass getStats() { return m_stats; }
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
        public void setExits(Guid n, Guid s, Guid w, Guid e)
        {
            m_NorthExit = n;
            m_SouthExit = s;
            m_EastExit = e;
            m_WestExit = w;
        }

        /*CONSTRUCTORS*/
        public BlockClass(PositionClass position, Guid terrainUniqueID)
        {
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->BlockClass()"); };
            
            m_createDate = new GameTimeClass(ConstantClass.gameTime);
            m_unique_block_id = Guid.NewGuid();

            m_position = position;           
            m_terrain_id = terrainUniqueID;
            setExits(Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty);
            
            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable().Add(m_unique_block_id, this);

            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-BlockClass()"); };
        }

        /*METHODS*/
        public string printAllAvailableExits()
        {
            string output = "";

            if (m_NorthExit == Guid.Empty && m_SouthExit == Guid.Empty && m_WestExit == Guid.Empty && m_EastExit == Guid.Empty) output += "None";
            else
            {
                if (m_NorthExit != Guid.Empty) { output += "North,"; }
                if (m_SouthExit != Guid.Empty) { output += "South,"; }
                if (m_WestExit != Guid.Empty) { output += "West,"; }
                if (m_EastExit != Guid.Empty) { output += "East"; }
            }
            return output;

        }
    }
}
