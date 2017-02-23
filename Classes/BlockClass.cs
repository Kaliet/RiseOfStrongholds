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
        private Guid m_building_id;
        private List<Guid> m_list_of_occupants;
        private InventoryClass m_inventory_list;

        /*GET & SET*/
        public Guid getUniqueBlockID() { return m_unique_block_id; }        
        public GameTimeClass getCreateDate() { return m_createDate; }
        public Guid getTerrainID() { return m_terrain_id; }
        public Guid getRoomID() { return m_room_id; }
        public Guid getBuildingID () { return m_building_id; } //returns id of building that is built on it.
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
        public void setRoom(Guid room){ m_room_id = room; }
        public void setBuildingID(Guid buildingID) { m_building_id = buildingID; }
        public void setAllExits(Guid n, Guid s, Guid w, Guid e)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            Guid room2 = Guid.Empty;            
            GuidPairClass pair;
            

            try
            {
                if (ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.isGuidAKey(m_room_id)) //checks if room has a shared exit
                {
                    room2 = ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.returnSecondGuidKey(m_room_id);
                }
                pair = new GuidPairClass(m_room_id, room2);


                if (n == Guid.Empty) //if we are blocking off the north exit
                {
                    if (m_NorthExit != Guid.Empty)//if there is a block in the North, then that block's south exit is blocked
                    {
                        ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_NorthExit].m_SouthExit = Guid.Empty;
                    }
                    removeBlockFromSharedList(pair);
                    m_NorthExit = n;
                }
                if (s == Guid.Empty) //if we are blocking off the south exit
                {
                    if (m_SouthExit != Guid.Empty) { ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_SouthExit].m_NorthExit = Guid.Empty; } //if there is a block in the South, then that block's north exit is blocked
                    removeBlockFromSharedList(pair);
                    m_SouthExit = s;
                }
                if (e == Guid.Empty) //if we are blocking off the east exit
                {
                    if (m_EastExit != Guid.Empty) { ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_EastExit].m_WestExit = Guid.Empty; } //if there is a block in the East, then that block's west exit is blocked
                    removeBlockFromSharedList(pair);
                    m_EastExit = e;
                }
                if (w == Guid.Empty) //if we are blocking off the west exit
                {
                    if (m_WestExit != Guid.Empty) { ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_WestExit].m_EastExit = Guid.Empty; } //if there is a block in the West, then that block's east exit is blocked
                    removeBlockFromSharedList(pair);
                    m_WestExit = w;
                }
            }
            catch (Exception ex)
            {
                ConstantClass.LOGGER.writeToCrashLog(ex);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
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
            m_inventory_list = new InventoryClass(ConstantClass.INVENTORY_BLOCK_MAX_CAP);

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

        public void constructNewBuilding(ConstantClass.BUILDING type) //constructs building on this block
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                switch (type)
                {
                    case ConstantClass.BUILDING.WALL: //constructing new WALL on the block
                        {
                            BuildingClass newBuilding = new BuildingClass(type, m_unique_block_id);
                            setAllExits(Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty); //blocks all exits
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public void removeBlockFromSharedList(GuidPairClass roomPair) //removes this block from shared list of mapping table
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {

                List<GuidPairClass> listofSharedExits = new List<GuidPairClass>();
                Guid block2 = Guid.Empty;

                //if block is part of shared exits between room, then need remove it from the table of shared exits.
                if (ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable().ContainsKey(roomPair))
                {
                    listofSharedExits = ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable()[roomPair]; //list of shared exits

                    //1. foreach shared exit, check if block is shared exit , if so then remove it from table
                    for (int i = 0; i < listofSharedExits.Count; i++)
                    {
                        if (listofSharedExits[i].isGuidOneofthePairs(this.m_unique_block_id))
                        {
                            block2 = listofSharedExits[i].returnSecondGuidPair(this.m_unique_block_id);
                            ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable()[roomPair].Remove(new GuidPairClass(this.m_unique_block_id, block2));
                        }
                    }

                    //remove from mirrored entry in mapping table
                    roomPair.swapGuidInsidePair();
                    ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS.getMappingTable()[roomPair].Remove(new GuidPairClass(block2, m_unique_block_id));
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public bool existsResourceInInventory()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (m_inventory_list.getInventorySize() > 0) return true;
            else return false;
        }

        public ResourceObjectClass reduceBlockInventory(int rate) //rate = reduction rate
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            ResourceObjectClass resource = null;

            try
            {
                if (m_inventory_list.getInventorySize() <= 0) return null;
                
                else if (m_inventory_list.returnQuantityOfItemBasedOnIndex(0) <= rate) { rate = m_inventory_list.returnQuantityOfItemBasedOnIndex(0); }//if rate is higher than number of items in inventory, then retrieve all the inventory                            

                resource = new ResourceObjectClass(ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS.getMappingTable()[m_terrain_id].getResourceType(), rate); 
                
                m_inventory_list.removeItemFromInventory(resource,rate); //resource deducted from block inventory
            }
            catch(Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return resource;
        }

        private void updateBlockInventory() //update block inventory status 
        {            
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                //resources generated this turn are added to the block inventory            
                int rate = ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS.getMappingTable()[m_terrain_id].getResourceGenerateRate();
                ResourceObjectClass resource = new ResourceObjectClass(ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS.getMappingTable()[m_terrain_id].getResourceType());

                if (rate > 0) //if resource generate some value
                {
                    if (m_inventory_list.addItemToInventory(resource, rate))
                    {
                        //ConstantClass.LOGGER.writeToInventoryLog("Resource " + resource.ToString() + " added to inventory in block ID " + m_unique_block_id + "\n");
                    }
                    else
                    {
                        ConstantClass.LOGGER.writeToInventoryLog("Failed to add resource " + resource.ToString() + " to inventory in block ID " + m_unique_block_id + "\n");
                    }
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*EVENT HANDLER*/
        public void OnGameTicked(object source, EventArgs args)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            //update block
            updateBlockInventory();
            if (m_inventory_list.getInventorySize() > 0) { ConstantClass.LOGGER.writeToInventoryLog("\nBlock ID: " + m_unique_block_id + " " + m_inventory_list.printInventoryList()); }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
