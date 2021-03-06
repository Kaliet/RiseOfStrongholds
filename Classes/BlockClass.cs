﻿using System;
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

        public BlockClass(BlockClass target) //copy constructor
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            this.m_unique_block_id = target.m_unique_block_id;
            this.m_NorthExit = target.m_NorthExit;
            this.m_SouthExit = target.m_SouthExit;
            this.m_EastExit = target.m_EastExit;
            this.m_WestExit = target.m_WestExit;
            this.m_createDate = new GameTimeClass(target.m_createDate);
            this.m_terrain_id = target.m_terrain_id;
            this.m_position = new PositionClass(target.m_position);
            this.m_stats = new BlockStatsClass(target.m_stats);
            this.m_room_id = target.m_room_id;
            this.m_building_id = target.m_building_id;
            this.m_inventory_list = new InventoryClass(target.m_inventory_list);

            this.m_list_of_occupants = new List<Guid>();
            foreach (Guid id in target.m_list_of_occupants)
            {
                this.m_list_of_occupants.Add(id);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public BlockClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            this.m_unique_block_id = Guid.Empty;
            this.m_NorthExit = Guid.Empty;
            this.m_SouthExit = Guid.Empty;
            this.m_EastExit = Guid.Empty;
            this.m_WestExit = Guid.Empty;
            this.m_createDate = new GameTimeClass();
            this.m_terrain_id = Guid.Empty;
            this.m_position = new PositionClass();
            this.m_stats = new BlockStatsClass();
            this.m_room_id = Guid.Empty;
            this.m_building_id = Guid.Empty;
            this.m_inventory_list = new InventoryClass();

            this.m_list_of_occupants = new List<Guid>();            

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        
            /*exists*/
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
        public bool existsResourceInInventory()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (!m_inventory_list.isEmpty()) return true;
            else return false;
        }
        public bool isOccupantListEmpty()
        {
            return (m_list_of_occupants.Count == 0);
        }
        public bool existsBuilding() { return m_building_id != Guid.Empty; }

            /*inventory related*/
        public ResourceObjectClass reduceBlockInventory(int rate) //rate = reduction rate
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            ResourceObjectClass resource = null;

            try
            {
                if (m_inventory_list.getInventorySize() <= 0) return null;

                else if (m_inventory_list.returnQuantityOfItemBasedOnIndex(0) <= rate) { rate = m_inventory_list.returnQuantityOfItemBasedOnIndex(0); }//if rate is higher than number of items in inventory, then retrieve all the inventory                            

                resource = new ResourceObjectClass(ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS.getMappingTable()[m_terrain_id].getResourceType(), rate);

                m_inventory_list.deductQuantityOfItem(resource); //resource deducted from block inventory
            }
            catch (Exception e)
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
                ResourceObjectClass resource = new ResourceObjectClass(ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS.getMappingTable()[m_terrain_id].getResourceType(), rate);

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

            /*constructions*/
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

            /*updating occupants in block*/
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
        public void addCharacterToBlockOccupants(Guid character)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (m_list_of_occupants.Count > 0) throw new BlockOccupiedException(); //check if block is occupied
            else
            {
                try
                {
                    m_list_of_occupants.Add(character);
                }
                catch (Exception ex)
                {
                    ConstantClass.LOGGER.writeToCrashLog(ex);
                }
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
        public void removeCharacterFromBlockOccupants(Guid character)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                m_list_of_occupants.Remove(character);
            }
            catch (Exception ex)
            {
                ConstantClass.LOGGER.writeToCrashLog(ex);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
        public List<Guid> returnListOfUnoccupiedAdjBlocks(Guid charBlockID) //returns list of adjacent block IDs that are unoccupied, if none then return null
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            List<Guid> list = new List<Guid>();

            try
            {
                int roomSize = ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[m_room_id].getSize();
                BlockClass[,] room = ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[m_room_id].getRoom();
                int x, y;
                bool top, right, left, down;

                x = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[charBlockID].getPosition().getPositionX();
                y = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[charBlockID].getPosition().getPositionY();
                top = true;
                right = true;
                left = true;
                down = true;

                //checks boundaries
                if (x==0) { top = false; }
                if (x == roomSize - 1) { down = false; }
                if (y==0) { left = false; }
                if (y==roomSize-1) { right = false; }

                //check buildings
                if (x > 0 && room[x - 1,y].getBuildingID() != Guid.Empty) { top = false; }
                if (y < roomSize - 1 && room[x, y + 1].getBuildingID() != Guid.Empty) { right = false; }
                if (y > 0 && room[x, y - 1].getBuildingID() != Guid.Empty) { left = false; }
                if (x < roomSize - 1 && room[x + 1, y].getBuildingID() != Guid.Empty) { down = false; }

                if (top) { list.Add(room[x - 1, y].getUniqueBlockID()); }
                if (right) { list.Add(room[x, y + 1].getUniqueBlockID()); }
                if (left) { list.Add(room[x, y - 1].getUniqueBlockID()); }
                if (down) { list.Add(room[x + 1, y].getUniqueBlockID()); }
                
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            return list;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }        
        public Guid[] retrieveOccupantsList () //returns array of occupants of the block -> to avoid defining getOccupantList()
        {
            Guid[] result = new Guid[m_list_of_occupants.Count];
            m_list_of_occupants.CopyTo(result, 0);

            return result;
        }

            /*printing*/
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
        public string printOccupantList()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string output = "";

            foreach (Guid id in m_list_of_occupants)// go through list and print the occupants
            {
                output += id.ToString().Substring(0, 1);
            }

            return output;

        }
        public string printInventory()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string output = "";
            output = m_inventory_list.printUnformattedInventoryList();
            return output;
        }

        public void DEBUG_addInventory(List<GenericObjectClass> value)
        {
            m_inventory_list = new InventoryClass(value);
        }

        /*EVENT HANDLER*/
        public void OnGameTicked(object source, EventArgs args)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            //update block
            updateBlockInventory();
            if (m_inventory_list.getInventorySize() > 0)
            {
                ConstantClass.LOGGER.writeToInventoryLog("\nBlock ID: " + m_unique_block_id + "\n" + m_inventory_list.printInventoryList());                
            }            

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
