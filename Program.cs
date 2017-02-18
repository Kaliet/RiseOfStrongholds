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
            int numOfRooms = 2;
            int sizeRoom = 1;
            RoomClass[] rooms = new RoomClass[numOfRooms];
            for (int i = 0; i < numOfRooms; i++)
            {
                rooms[i] = new RoomClass(sizeRoom);
                if ((i % 2) == 0) { rooms[i].initializeRoom(grassTerrain); }
                else { rooms[i].initializeRoom(forestTerrain); }
                rooms[i].linkAllBlocksTogetherHorizontally();
                rooms[i].linkAllBlocksTogetherVertically();
                ConstantClass.LOGGER.writeToMapLog("room" + i + " ID = " + rooms[i].getUniqueRoomID() + "\n");
                region1.addRoom(rooms[i].getUniqueRoomID());
            }
            
            //for (int i = 0; i < 1; i++)
            //{
            //    region1.linkTwoRoomsWithExit(rooms[i], ConstantClass.EXITS.SOUTH, rooms[i + 1], 3);
            //}


            ConstantClass.LOGGER.writeToMapLog(region1.printRoomLinks());

            ///*SECOND GENERATE THE CHARACTERS IN THE WORLD*/
            testcase.runRoomTestWithMultipleChars(rooms[0], region1, rooms[sizeRoom - 1]);
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
