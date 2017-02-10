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

            TerrainClass grassTerrain = new TerrainClass(ConstantClass.TERRAIN_TYPE.GRASS);
            TerrainClass hillTerrain = new TerrainClass(ConstantClass.TERRAIN_TYPE.HILL);
            RegionClass region1 = new RegionClass();

            RoomClass room1 = new RoomClass(5);
            room1.initializeRoom(grassTerrain);
            room1.linkAllBlocksTogetherHorizontally();
            room1.linkAllBlocksTogetherVertically();

            room1.getRoom()[1, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room1.getRoom()[1, 2].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room1.getRoom()[1, 3].constructNewBuilding(ConstantClass.BUILDING.WALL);
            //room1.getRoom()[2, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room1.getRoom()[2, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
            //room1.getRoom()[2, 2].constructNewBuilding(ConstantClass.BUILDING.WALL);            
            room1.getRoom()[2, 3].constructNewBuilding(ConstantClass.BUILDING.WALL);            
            //room1.getRoom()[3, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room1.getRoom()[3, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
            //room1.getRoom()[3, 2].constructNewBuilding(ConstantClass.BUILDING.WALL);
            //room1.getRoom()[3, 3].constructNewBuilding(ConstantClass.BUILDING.WALL);            


            RoomClass room2 = new RoomClass(4);
            room2.initializeRoom(grassTerrain);
            room2.linkAllBlocksTogetherHorizontally();
            room2.linkAllBlocksTogetherVertically();
            room2.getRoom()[1, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room2.getRoom()[1, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room2.getRoom()[1, 2].constructNewBuilding(ConstantClass.BUILDING.WALL);

            //RoomClass room3 = new RoomClass(2);
            //room3.initializeRoom(grassTerrain);
            //room3.linkAllBlocksTogetherHorizontally();
            //room3.linkAllBlocksTogetherVertically();

            region1.addRoom(room1.getUniqueRoomID());
            region1.addRoom(room2.getUniqueRoomID());
            //region1.addRoom(room3.getUniqueRoomID());



            region1.linkTwoRoomsWithExit(room1, ConstantClass.EXITS.SOUTH, room2, 3);
            //region1.linkTwoRoomsWithExit(room2, ConstantClass.EXITS.NORTH, room3, 1);

            ///*SECOND GENERATE THE CHARACTERS IN THE WORLD*/
            testcase.runRoomTestWithMultipleChars(room2, region1, room1);
            //testcase.runRoomTestWithMultipleChars(room2, region1);            
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
