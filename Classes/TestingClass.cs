﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class TestingClass
    {
        public TestingClass() { }

        /*GAMETIME TEST CASES*/
        public void runGameTimeTests ()
        {

            /*TEST CASE*/
          
            /*checking getTotalMinutes()*/
            //ConstantClass.LOGGER.writeToLog(ConstantClass.gameTime.ToString());
            //ConstantClass.LOGGER.writeToLog("Total minutes = " + ConstantClass.gameTime.getTotalMins());
            //ConstantClass.gameTime.set_years(1);
            //ConstantClass.LOGGER.writeToLog(ConstantClass.gameTime.ToString());
            //ConstantClass.LOGGER.writeToLog("Total minutes = " + ConstantClass.gameTime.getTotalMins());

            /*Ensure 1440+ mins are added to days correctly.*/
            //GameTimeClass tempTime = new GameTimeClass(1, 0, 1, 5, 1);
            //while (ConstantClass.gameTime != tempTime)
            //{
            //    ConstantClass.LOGGER.writeToLog(ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_mins(1440);
            //    ConstantClass.LOGGER.writeToLog("Added 50 minutes:\t" + ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_mins(1600);
            //    ConstantClass.LOGGER.writeToLog("Added 100 minutes:\t" + ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_mins(2000);
            //    ConstantClass.LOGGER.writeToLog("Added 240 minutes:\t" + ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_mins(2880);
            //    ConstantClass.LOGGER.writeToLog("Added 245 minutes:\t" + ConstantClass.gameTime.ToString());
            //}

            /*Ensure 60+ mins are added to hours correctly.*/
            //GameTimeClass tempTime = new GameTimeClass(1, 0, 1, 5, 1);
            //while (ConstantClass.gameTime != tempTime)
            //{
            //    ConstantClass.LOGGER.writeToLog(ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_mins(50);
            //    ConstantClass.LOGGER.writeToLog("Added 50 minutes:\t" + ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_mins(100);
            //    ConstantClass.LOGGER.writeToLog("Added 100 minutes:\t" + ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_mins(240);
            //    ConstantClass.LOGGER.writeToLog("Added 240 minutes:\t" + ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_mins(245);
            //    ConstantClass.LOGGER.writeToLog("Added 245 minutes:\t" + ConstantClass.gameTime.ToString());
            //}  
        }

        /*CHARACTER TEST CASES*/
        public void runCharacterTests()
        {
            int i = 0;
            CharacterClass[] people = new CharacterClass[10];

            while (i < 10)
            {
                people[i] = new CharacterClass();                
                ConstantClass.LOGGER.writeToLog("Person " + i + ": Birth date is " + people[i].getBirthDate().ToString());
                i++;
                ConstantClass.gameTime.set_days(1);
            }
        }
    }
}