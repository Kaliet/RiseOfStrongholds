using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using RiseOfStrongholds.Classes;

namespace RiseOfStrongholds
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConstantClass.DEBUG_LOG_LEVEL = ConstantClass.DEBUG_LEVELS.OFF;

                /*Defining LOGGER*/
                ConstantClass.LOGGER = new LoggerClass();
                ConstantClass.LOGGER.createNewFiles();



                TestingClass testcase = new TestingClass(); //for making tests

                /*GAMETIME*/
                ConstantClass.gameTime = new GameTimeClass();
                Thread startGameTimeThread = new Thread(new ThreadStart(ConstantClass.gameTime.startGameTime));
                startGameTimeThread.Start();

                /*PROGRAM START*/
                ConstantClass.LOGGER.writeToDebugLog("Starting program...");
                //---------------

                //testcase.runGameTimeTests();
                //testcase.runCharacterTests();
                //testcase.runRandomGeneratorTests();
                //testcase.runMappingTableTests();

                /*INTIALIZING ALL MAPPING TABLES*/
                ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS = new MappingClass<BlockClass>();
                ConstantClass.MAPPING_TABLE_FOR_ALL_CHARS = new MappingClass<CharacterClass>();
                ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS = new MappingClass<TerrainClass>();
                ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS = new MappingClass<BuildingClass>();
                ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS = new MappingClass<RoomClass>();
                ConstantClass.MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS = new MappingPairClass<List<GuidPairClass>>();

                /*FIRST GENERATE THE WORLD - EXAMPLE*/

                TerrainClass dirtTerrain = new TerrainClass(ConstantClass.TERRAIN_TYPE.DIRT);
                TerrainClass grassTerrain = new TerrainClass(ConstantClass.TERRAIN_TYPE.GRASS);
                TerrainClass forestTerrain = new TerrainClass(ConstantClass.TERRAIN_TYPE.FOREST);
                TerrainClass hillTerrain = new TerrainClass(ConstantClass.TERRAIN_TYPE.HILL);
                RegionClass region1 = new RegionClass();

                //RoomClass[] rooms = new RoomClass[4];
                //for (int i = 0; i < 4; i++)
                //{
                //    rooms[i] = new RoomClass(2);
                //    rooms[i].initializeRoom(grassTerrain);
                //    rooms[i].linkAllBlocksTogetherHorizontally();
                //    rooms[i].linkAllBlocksTogetherVertically();
                //    ConstantClass.LOGGER.writeToMapLog("room" + i + " ID = " + rooms[i].getUniqueRoomID() + "\n");
                //    region1.addRoom(rooms[i].getUniqueRoomID());
                //}

                //region1.linkTwoRoomsWithExit(rooms[1], ConstantClass.EXITS.NORTH, rooms[3], 1); //-> endless loop in backtracking            
                //for (int i = 0; i < 3; i++)
                //{
                //    region1.linkTwoRoomsWithExit(rooms[i], ConstantClass.EXITS.SOUTH, rooms[i + 1], 1);
                //}
                int numOfRooms = 3;
                int sizeRoom = 10;
                RoomClass[] rooms = new RoomClass[numOfRooms];
                for (int i = 0; i < numOfRooms; i++)
                {
                    rooms[i] = new RoomClass(sizeRoom);
                    if ((i % 2) == 0) { rooms[i].initializeRoom(dirtTerrain); }
                    else { rooms[i].initializeRoom(dirtTerrain); }
                    rooms[i].linkAllBlocksTogetherHorizontally();
                    rooms[i].linkAllBlocksTogetherVertically();                   
                    region1.addRoom(rooms[i].getUniqueRoomID());                    
                }

                //rooms[numOfRooms - 1].getRoom()[1,0].setTerrainType(grassTerrain.getUniqueTerrainID());
                //rooms[0].getRoom()[1, 1].setTerrainType(grassTerrain.getUniqueTerrainID());
                List<GenericObjectClass> list = new List<Classes.GenericObjectClass>();
                list.Add(new GenericObjectClass("FOOD", 1));
                rooms[0].getRoom()[1, 1].setTerrainType(grassTerrain.getUniqueTerrainID());
                rooms[0].getRoom()[1, 1].DEBUG_addInventory(list);
                rooms[0].getRoom()[2, 2].setTerrainType(grassTerrain.getUniqueTerrainID());
                rooms[0].getRoom()[2, 2].DEBUG_addInventory(list);

                rooms[1].getRoom()[0, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
                rooms[1].getRoom()[0, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
                rooms[1].getRoom()[1, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
                rooms[1].getRoom()[1, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
                rooms[1].getRoom()[3, 3].setTerrainType(grassTerrain.getUniqueTerrainID());
                rooms[1].getRoom()[3, 3].DEBUG_addInventory(list);
                rooms[1].getRoom()[3, 2].setTerrainType(grassTerrain.getUniqueTerrainID());
                rooms[1].getRoom()[3, 2].DEBUG_addInventory(list);

                rooms[numOfRooms -1].getRoom()[0, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
                //rooms[1].getRoom()[0, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
                rooms[numOfRooms -1].getRoom()[0, 2].constructNewBuilding(ConstantClass.BUILDING.WALL);
                //rooms[1].getRoom()[0, 3].constructNewBuilding(ConstantClass.BUILDING.WALL);
                //rooms[1].getRoom()[1, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
                rooms[numOfRooms -1].getRoom()[2, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
                rooms[numOfRooms -1].getRoom()[2, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
                //rooms[1].getRoom()[2, 2].constructNewBuilding(ConstantClass.BUILDING.WALL);                
                //rooms[1].getRoom()[3, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
                //rooms[1].getRoom()[3, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
                //rooms[1].getRoom()[3, 2].constructNewBuilding(ConstantClass.BUILDING.WALL);
                //rooms[1].getRoom()[3, 3].constructNewBuilding(ConstantClass.BUILDING.WALL);
                //rooms[1].getRoom()[2, 3].constructNewBuilding(ConstantClass.BUILDING.WALL);
                //rooms[1].getRoom()[1, 3].constructNewBuilding(ConstantClass.BUILDING.WALL);

                for (int i = 0; i < numOfRooms - 1; i++)
                {
                    region1.linkTwoRoomsWithExit(rooms[i], ConstantClass.EXITS.SOUTH, rooms[i + 1], 1);
                }

                //ConstantClass.LOGGER.writeToMapLog(region1.printRoomLinks());
                ConstantClass.LOGGER.writeToMapLog(region1.printAllRoomsInRegion());


                ///*SECOND GENERATE THE CHARACTERS IN THE WORLD*/
                testcase.runRoomTestWithMultipleChars(rooms[1], region1, rooms[numOfRooms - 1]);
                //testcase.runRoomTestWithMultipleChars(room2, region1);            

            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);                
            }
            //---------------
            /*PROGRAM END*/

        }
    }
}


/* GIT COMMANDS
 * 
 *  
    fetch:
    git -c diff.mnemonicprefix=false -c core.quotepath=false fetch origin

    pull:
    git -c diff.mnemonicprefix=false -c core.quotepath=false fetch origin
    git -c diff.mnemonicprefix=false -c core.quotepath=false pull origin master
    git -c diff.mnemonicprefix=false -c core.quotepath=false submodule update --init --recursive



 * 
 */
