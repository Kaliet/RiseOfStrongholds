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
        //public void runCharacterTests()
        //{
        //    int i = 0;
        //    CharacterClass[] people = new CharacterClass[10];

        //    while (i++ < 10)
        //    {
        //        people[i] = new CharacterClass();                
        //        //ConstantClass.LOGGER.writeToDebugLog("Person " + i + ": Unique ID is " + people[i].getUniqueCharacterID().ToString());                                
        //    }
        //}

        /*RANDOM TESTS CASES*/
        public void runRandomGeneratorTests()
        {
            for (int i = 1; i <= 100; i++)
            {
                ConstantClass.LOGGER.writeToDebugLog("Iteration " + i + ":\t\t" + ConstantClass.RANDOMIZER.produceInt(1, 100));
            }            
        }

        /*MAPPING TABLE CASES*/
        //public void runMappingTableTests()
        //{
        //    MappingClass<CharacterClass> characterMapping = new MappingClass<CharacterClass>();
        //    int i = 0;
        //    CharacterClass[] people = new CharacterClass[10];

        //    while (i < 10)
        //    {
        //        people[i] = new CharacterClass();
        //        characterMapping.getMappingTable().Add(people[i].getUniqueCharacterID(), people[i]);
        //        i++;
        //    }

        //    CharacterClass x = characterMapping.getMappingTable()[people[6].getUniqueCharacterID()];
        //}

        /*SIMULATED ROOM TEST WITH MULTIPLE CHARACTERS*/
        public void runRoomTestWithMultipleChars(RoomClass room, RegionClass region, RoomClass room2)
        {
            List<CharacterClass> people = new List<CharacterClass>();

            try
            {
                for (int i = 0; i < 1; i++)
                {
                    try
                    {
                        people.Add(new CharacterClass(room.getRoom()[i, 0].getUniqueBlockID()));
                    }
                    catch (CharacterNotCreatedException ex)
                    {
                        continue;
                    }
                    ConstantClass.gameTime.GameTicked += people[i].OnGameTicked;
                    people[i].ActionUpdated += room.OnActionUpdated;
                    people[i].ActionUpdated += region.OnActionUpdated;
                }
                //CharacterClass person2 = new CharacterClass(room.getRoom()[0, 2].getUniqueBlockID());
                //ConstantClass.gameTime.GameTicked += person2.OnGameTicked;
                //person2.ActionUpdated += room.OnActionUpdated;
                //person2.ActionUpdated += region.OnActionUpdated;            
                //person2.FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, ConstantClass.ACTION_SEARCH_PRIORITY, 0, room.getRoom()[1,0].getUniqueBlockID()));
                //person2.FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_CHAR, ConstantClass.ACTION_SEARCH_PRIORITY, 0, people[0].getUniqueCharacterID()));

                //people.Add(person2);

                //people[0].FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, ConstantClass.ACTION_SEARCH_PRIORITY, 0, room.getRoom()[4,4].getUniqueBlockID()));
                //people[0].FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, ConstantClass.ACTION_SEARCH_PRIORITY, 0, room.getRoom()[0, 1].getUniqueBlockID()));
                //people[0].FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.GATHER, ConstantClass.ACTION_GATHER_PRIORITY, 0, room.getRoom()[0, 0].getUniqueBlockID()));
                //people[0].FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, ConstantClass.ACTION_GATHER_PRIORITY, 0, room.getRoom()[0, 0].getUniqueBlockID()));
                //people[0].FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_BLOCK, ConstantClass.ACTION_GATHER_PRIORITY, 0, room2.getRoom()[2, 2].getUniqueBlockID()));
                //people[1].FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.GATHER, ConstantClass.ACTION_GATHER_PRIORITY, 0, room2.getRoom()[1, 0].getUniqueBlockID()));
                //people[1].FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_CHAR, ConstantClass.ACTION_SEARCH_PRIORITY, 0, people[2].getUniqueCharacterID()));
                //people[1].FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.FIND_CHAR, ConstantClass.ACTION_SEARCH_PRIORITY, 0, people[0].getUniqueCharacterID()));
                //people[2].FOR_DEBUG_addActionInQueue(new ActionClass(ConstantClass.CHARACTER_ACTIONS.WALK, ConstantClass.ACTION_WALK_PRIORITY, 0, Guid.Empty));

                MemoryBitClass bit = new MemoryBitClass(room.getRoom()[0, 0].getUniqueBlockID(), ConstantClass.CHARACTER_ACTIONS.GATHER, ConstantClass.gameTime);
                people[0].DEBUG_getMemory().addMemoryToShortTerm(bit);
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }
        }
    }
}
