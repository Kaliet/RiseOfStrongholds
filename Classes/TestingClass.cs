using System;
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

            /*Check 1 full day*/
            GameTimeClass tempTime = new GameTimeClass(1, 0, 2, 1, 1);
            while (ConstantClass.gameTime != tempTime)
            {
                ConstantClass.LOGGER.writeToLog(ConstantClass.gameTime.ToString());
                ConstantClass.gameTime.set_mins(1);
            }

            /*Check 1 full month*/
            //GameTimeClass tempTime = new GameTimeClass(0, 1, 1, 2, 1);
            //while (ConstantClass.gameTime != tempTime)
            //{
            //    ConstantClass.LOGGER.writeToLog(ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_hours(1);
            //}

            /*Check 1 full year*/
            //GameTimeClass tempTime = new GameTimeClass(0, 8, 1, 1, 2);
            //while (ConstantClass.gameTime != tempTime)
            //{
            //    ConstantClass.LOGGER.writeToLog(ConstantClass.gameTime.ToString());
            //    ConstantClass.gameTime.set_days(1);
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
