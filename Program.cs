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
            ConstantClass.DEBUG_LOG_LEVEL = ConstantClass.DEBUG_LEVELS.HIGH;

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
            BlockClass block1 = new BlockClass(new PositionClass(0, 0), grassTerrain.getUniqueTerrainID());
            BlockClass block2 = new BlockClass(new PositionClass(1, 0), hillTerrain.getUniqueTerrainID());
            BlockClass block3 = new BlockClass(new PositionClass(2, 0), grassTerrain.getUniqueTerrainID());
            BlockClass block4 = new BlockClass(new PositionClass(2, 1), hillTerrain.getUniqueTerrainID());
            BlockClass block5 = new BlockClass(new PositionClass(1, 1), hillTerrain.getUniqueTerrainID());
            block1.setExits(Guid.Empty, Guid.Empty, Guid.Empty, block2.getUniqueBlockID());
            block2.setExits(block5.getUniqueBlockID(), Guid.Empty, block1.getUniqueBlockID(), block3.getUniqueBlockID());
            block3.setExits(block4.getUniqueBlockID(), Guid.Empty, block2.getUniqueBlockID(), Guid.Empty);
            block4.setExits(Guid.Empty, block3.getUniqueBlockID(), block5.getUniqueBlockID(), Guid.Empty);
            block5.setExits(Guid.Empty, block2.getUniqueBlockID(), Guid.Empty, block4.getUniqueBlockID());

            /*SECOND GENERATE THE CHARACTERS IN THE WORLD*/
            CharacterClass person = new CharacterClass(block1.getUniqueBlockID());
            CharacterClass person2 = new CharacterClass(block2.getUniqueBlockID());

            ConstantClass.gameTime.GameTicked += person.OnGameTicked;            
            //---------------
            /*PROGRAM END*/

        }
    }
}
