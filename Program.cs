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

            CharacterClass person = new CharacterClass();

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
                    ConstantClass.LOGGER.writeToGameLog("Person hunger state: "+person.getStats().printHungerStatus() +" | sleep state: "+ person.getStats().printSleepStatus());
                }
            }


            //---------------
            /*PROGRAM END*/

        }
    }
}
