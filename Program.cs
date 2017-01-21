using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RiseOfStrongholds.Classes;

namespace RiseOfStrongholds
{
    class Program
    {
        static void Main(string[] args)
        {
            TestingClass testcase = new TestingClass(); //for making tests

            // WELCOME!//            
            // WORK//

            /*VARIABLES*/
            long lastTick = DateTime.Now.Ticks;
            long currentTick;
            long elapsedTick;
            TimeSpan elapsedSpan;
            ConstantClass.gameTime = new GameTimeClass();                                   
            ConstantClass.DEBUG_LOG_LEVEL = ConstantClass.DEBUG_LEVELS.OFF;

            /*Defining LOGGER*/
            ConstantClass.LOGGER = new LoggerClass();
            ConstantClass.LOGGER.createNewFiles();

            /*PROGRAM START*/ConstantClass.LOGGER.writeToDebugLog("Starting program...");
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
            TerrainClass grassTerrain = new TerrainClass(ConstantClass.TERRAIN_TYPE.GRASS);
            BlockClass block1 = new BlockClass(new PositionClass(0, 0), grassTerrain.getUniqueTerrainID());
            BlockClass block2 = new BlockClass(new PositionClass(0, 1), grassTerrain.getUniqueTerrainID());
            block1.setExits(Guid.Empty, Guid.Empty, Guid.Empty, block2.getUniqueBlockID());
            block2.setExits(Guid.Empty, Guid.Empty, block1.getUniqueBlockID(), Guid.Empty);            

            /*SECOND GENERATE THE CHARACTERS IN THE WORLD*/
            CharacterClass person = new CharacterClass(block1.getUniqueBlockID());
            CharacterClass person2 = new CharacterClass(block2.getUniqueBlockID());

            while (true) //game loop
            {
                currentTick = DateTime.Now.Ticks; //checks time now.
                elapsedTick = currentTick - lastTick; //calculates how much time elapsed

                if (elapsedTick < TimeSpan.TicksPerSecond)
                {
                    //do nothing and check again until 1 second passes
                }
                else //more than 1 second has passed
                {
                    /*UPDATE GAME TIME*/
                    elapsedSpan = new TimeSpan(elapsedTick);
                    ConstantClass.gameTime.updateGameTimeBasedOnElapsedTimeSpan(elapsedSpan); //gameTime is updated based on real time seconds elapse
                    ConstantClass.LOGGER.writeToDebugLog(ConstantClass.gameTime.ToString());
                    lastTick = currentTick;
                    /*-----------------*/

                    //<-game code goes here.
                    person.updateAction(); //person does something each second
                    //person2.updateAction(); //person does something each second
                    //ConstantClass.LOGGER.writeToGameLog("Person hunger state: "+person.getStats().printHungerStatus() +"\t|\tsleep state: "+ person.getStats().printSleepStatus());
                }
            }


            //---------------
            /*PROGRAM END*/

        }
    }
}
