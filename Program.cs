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

            /*VARIABLES*/
            ConstantClass.gameTime = new GameTimeClass();                                   
            ConstantClass.DEBUG_LOG_LEVEL = ConstantClass.DEBUG_LEVELS.OFF;

            /*Defining LOGGER*/
            ConstantClass.LOGGER = new LoggerClass();
            ConstantClass.LOGGER.createNewDebugFile();

            /*PROGRAM START*/ConstantClass.LOGGER.writeToLog("Starting program...");
            //---------------

            //testcase.runGameTimeTests();
            //testcase.runCharacterTests();

            //---------------
            /*PROGRAM END*/
            ConstantClass.LOGGER.writeToLog("Ending program...");
        }
    }
}
