﻿using System;
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
            
            /*FIRST GENERATE THE WORLD - EXAMPLE*/  
            /*
             * 
             *                   block5 <-> block4
             *                      |         |
             *        block1 <-> block2 <-> block3
             * 
             */

            TerrainClass grassTerrain = new TerrainClass(ConstantClass.TERRAIN_TYPE.GRASS);
            TerrainClass hillTerrain = new TerrainClass(ConstantClass.TERRAIN_TYPE.HILL);
            RoomClass room1 = new RoomClass(4);
            room1.initializeRoom(grassTerrain);
            room1.linkAllBlocksTogetherHorizontally();
            room1.linkAllBlocksTogetherVertically();
            //room1.getRoom()[1, 0].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room1.getRoom()[1, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room1.getRoom()[1, 2].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room1.getRoom()[2, 1].constructNewBuilding(ConstantClass.BUILDING.WALL);
            room1.getRoom()[2, 2].constructNewBuilding(ConstantClass.BUILDING.WALL);
            ///*SECOND GENERATE THE CHARACTERS IN THE WORLD*/
            testcase.runRoomTestWithMultipleChars(room1);

            //---------------
            /*PROGRAM END*/

        }
    }
}
