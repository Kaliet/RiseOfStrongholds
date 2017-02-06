using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class ConstantClass
    {
        //TODO: Move all relevant constants to Configuration File

        /*mapping tables*/
        public static MappingClass<CharacterClass> MAPPING_TABLE_FOR_ALL_CHARS;
        public static MappingClass<BlockClass> MAPPING_TABLE_FOR_ALL_BLOCKS;
        public static MappingClass<TerrainClass> MAPPING_TABLE_FOR_ALL_TERRAINS;
        public static MappingClass<BuildingClass> MAPPING_TABLE_FOR_ALL_BUILDINGS;
        public static MappingClass<RoomClass> MAPPING_TABLE_FOR_ALL_ROOMS;

        /*enums*/
        public enum DEBUG_LEVELS { OFF, LOW, HIGH };

        /*character enums*/
        public enum CHARACTER_ACTIONS { IDLE, EAT, SLEEP , WALK, SEARCH};
        public enum CHARACTER_SLEEP_STATUS { AWAKE, SLEEPY }; //awake = can perform actions, sleepy = must sleep otherwise energy decreases to 0
        public enum CHARACTER_HUNGER_STATUS { FULL, HUNGRY }; //hungry = top priority is to find food ; otherwise HP decreases

        /*biological constants*/
        public static int MINIMUM_NUMBER_OF_SLEEP_HOURS = 2;
        public static int HOURS_BETWEEN_EATING = 8;
        public static int HOURS_BETWEEN_SLEEPING = 20;

        /*energy costs + regeneration*/
        public static int ENERGY_COST_FOR_EATING = -1;//how much energy is deducted for eating                
        public static int ENERGY_COST_WHEN_HUNGRY = -2; //how much energy is deducted when hungry
        public static int ENERGY_COST_WHEN_SLEEPY = -5; //how much energy is deducted when sleepy
        public static int ENERGY_ADD_WHEN_SLEEP = 5; //how much energy is added for each tick
        public static int ENERGY_COST_FOR_WALKING = -1; //how much energy is deducted for walking 1 block

        /*action priorities*/
        public static int ACTION_EAT_PRIORITY = 10;
        public static int ACTION_SLEEP_PRIORITY = 1;
        public static int ACTION_WALK_PRIORITY = 50;
        public static int ACTION_SEARCH_PRIORITY = 50;
        public static int ACTION_NO_PRIORITY = 99999;
        public static int VARIABLE_FOR_ACTION_NONE = -1;

        /*LOGGER*/
        public static LoggerClass LOGGER; //logger for debugging purposes
        public static string DEBUG_LOG_DIRECTORY = @"..\..\Debug\";
        public static string DEBUG_LOG_FILENAME = "debug.log";
        public static string GAME_LOG_FILENAME = "gamelog.log";
        public static string QUEUE_LOG_FILENAME = "queuelog.log";
        public static string MAP_LOG_FILENAME = "maplog.log";

        /*DEBUG LEVEL (default off*/
        public static DEBUG_LEVELS DEBUG_LOG_LEVEL;

        /*GAME TIME*/
        public static GameTimeClass gameTime;
        public static int MINUTES_IN_ONE_HOUR = 60;
        public static int HOURS_IN_ONE_DAY = 24;
        public static int DAYS_IN_ONE_MONTH = 30;
        public static int MONTHS_IN_ONE_YEAR = 12;

        public static int GAME_SPEED = 60; //number of game minutes to 1 real life second

        /*RANDOM NUMBER GENERATOR*/
        public static RandomClass RANDOMIZER = new RandomClass();

        /*TERRAIN TYPES*/
        public enum TERRAIN_TYPE {GRASS, DESERT, HILL, DIRT};
        public static int TERRAIN_FATIGUE_FOR_GRASS = 0;
        public static int TERRAIN_FATIGUE_FOR_DIRT = 0;
        public static int TERRAIN_FATIGUE_FOR_HILL = -5;
        public static int TERRAIN_FATIGUE_FOR_DESERT = -10;

        /*BLOCK EXITS*/
        public enum EXITS { NORTH, SOUTH, EAST, WEST};

        /*BUILDING TYPE*/
        public enum BUILDING { HUT, FARM, WALL, STORAGE};
    }
}

/*TEMPLATE FOR NEW CLASSES*/

//public CharacterClass()
//{
/*DEBUG HIGH*/
//    if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t-><class_name>()"); };

/*DEBUG HIGH*/
//    if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t<-<class_name>()"); };
//}


/*
Type	    Represents	                                                    Range	                                                    Default Value
bool	    Boolean value	                                                True or False	                                            False
byte	    8-bit unsigned integer	                                        0 to 255	                                                0
char	    16-bit Unicode character	                                    U +0000 to U +ffff	                                        '\0'
decimal	    128-bit precise decimal values with 28-29 significant digits	(-7.9 x 1028 to 7.9 x 1028) / 100 to 28	                    0.0M
double	    64-bit double-precision floating point type	                    (+/-)5.0 x 10-324 to (+/-)1.7 x 10308	                    0.0D
float	    32-bit single-precision floating point type	                    -3.4 x 1038 to + 3.4 x 1038	                                0.0F
int	        32-bit signed integer type	                                    -2,147,483,648 to 2,147,483,647	                            0
long	    64-bit signed integer type	                                    -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807	    0L
sbyte	    8-bit signed integer type	                                    -128 to 127	                                                0
short	    16-bit signed integer type	                                    -32,768 to 32,767	                                        0
uint	    32-bit unsigned integer type	                                0 to 4,294,967,295	                                        0
ulong	    64-bit unsigned integer type	                                0 to 18,446,744,073,709,551,615                         	0
ushort	    16-bit unsigned integer type	                                0 to 65,535	                                                0
*/