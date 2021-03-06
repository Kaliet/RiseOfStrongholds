﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class ConstantClass
    {
        //TODO: Move all relevant constants to Configuration Files

        /*mapping tables*/
        public static MappingClass<CharacterClass> MAPPING_TABLE_FOR_ALL_CHARS;
        public static MappingClass<BlockClass> MAPPING_TABLE_FOR_ALL_BLOCKS;
        public static MappingClass<TerrainClass> MAPPING_TABLE_FOR_ALL_TERRAINS;
        public static MappingClass<BuildingClass> MAPPING_TABLE_FOR_ALL_BUILDINGS;
        public static MappingClass<RoomClass> MAPPING_TABLE_FOR_ALL_ROOMS;
        public static MappingPairClass<List<GuidPairClass>> MAPPING_TABLE_FOR_SHARED_EXITS_BETWEEN_ROOMS; //<(room1,room2) -> list of (block1 of room1,block2 of room2) with shared exit

        /*memory*/
        /*memory priority = the higher the longer the memory will be kept in long term memory*/
        public static int CHARACTER_MEMORY_INITIAL_SIZE = 100;
        public static int CHARACTER_MEMORY_SHORT_TERM_EXPIRATION_DAYS_DURATION = 100; //for example 100 days
        public static int CHARACTER_MEMORY_LONG_TERM_EXPIRATION_DAYS_DURATION = 200; //for example 200 days
        public static int CHARACTER_MEMORY_BLOCKS_VISITED_EXPIRATION_DAYS_DURATION = 300; //for example 300 days
        public static int CHARACTER_MEMORY_GATHER_PRIORITY = 500;
        public static int CHARACTER_MEMORY_FIND_CHAR_PRIORITY = 20;
        public static int CHARACTER_MEMORY_SLEEP_PRIORITY = 50;
        public static int CHARACTER_MEMORY_SHORT_TO_LONG_TERM_THRESHOLD = 50;
        public enum MEMORY_TYPE { BLOCK, ROOM, CHARACTER}; //defines what type of memory to store i.e: remembers person, block , room        
        public enum MEMORY { LONG, SHORT, SHORT_AND_LONG, BLOCKS, RESOURCES}; //defines long or short , blocks memory has not end date only capacity based. if full, then it will replace an older block visited memory       

        /*enums*/
        public enum DEBUG_LEVELS { OFF, LOW, HIGH };

        /*character enums*/
        public enum CHARACTER_ACTIONS
        {
            IDLE,   //idle - not doing anything
            EAT,    //eat - eats something from inventory, if none will SCAN
            SLEEP , //sleep - sleep
            WALK,   //walk - walks around aimlessly
            FIND_BLOCK, //find_block - finds a specific block id with pathfinding skill
            FIND_BUILDING, //TODO: not implemented yet
            FIND_CHAR,  //find_char - finds a specific character
            GATHER, //gather - gather resources
            REST,   //rest - rest until energy is replenished
            SCAN    //scan - scans around and memorize it
        };
        public enum CHARACTER_SLEEP_STATUS { AWAKE, SLEEPY, TIRED}; //awake = can perform actions, sleepy = must sleep since char passed HOURS_BETWEEN_SLEEPING, tired = energy decreases to 0        
        public enum CHARACTER_LIFE_STATUS { ALIVE, DEAD }; //dead or alive
        public enum CHARACTER_SATIETY_STATUS { FULL, HUNGRY, STARVING, FAMISHED }; //different levels of appetite level

        /*SATIETY*/
        public static int CHARACTER_HP_REGEN_EATING = 10; //amount of hp regenerated after eating
        public static int[] CHARACTER_SATIETY_THRESHOLDS = { 0, 75, 90, 100 }; //thresholds for different satiety levels of max hunger rate (in percentile) - i.e: 100% of max hunger rate = FAMISHED, 
        public static int[] CHARACTER_SATIETY_PENALITIES = { 0, 0, 0, -1 }; //number of HP deducted per tick for each different satiety status        

        /*character skills*/
        public static int CHAR_SKILLS_GATHER_RATE = 1; //# of resources gathered per round
        public static int CHAR_SKILLS_SCAN_RADIUS = 1; //radius from where character is standing

        /*biological constants*/
        public static int MINIMUM_NUMBER_OF_SLEEP_HOURS = 8; //minimum hour of hours to sleep
        public static int HOURS_BETWEEN_EATING = 8; //how many hours in between until character gets hungry
        public static int HOURS_BETWEEN_SLEEPING = 20; //how many hours in between character gets sleepy
        public static int HOURS_FOR_EATING = 1; //need two hours to eat
        public static int QUANTITY_TO_DEDUCT_PER_MEAL = 1; //number of quantity of FOOD to deduct from inventory per ACTION.EAT

        /*energy costs + regeneration*/             
        public static int ENERGY_COST_WHEN_HUNGRY = -2; //how much energy is deducted when hungry
        public static int ENERGY_COST_WHEN_TIRED = -2; //how much energy is deducted when hungry
        public static int ENERGY_COST_WHEN_SLEEPY = -5; //how much energy is deducted when sleepy
        public static int ENERGY_ADD_WHEN_SLEEP = 100; //how much energy is added for each tick
        public static int ENERGY_ADD_WHEN_REST = 10; //how much energy is added for each tick

        public static int ENERGY_COST_FOR_EATING = -1;//how much energy is deducted for eating           
        public static int ENERGY_COST_FOR_WALKING = -1; //how much energy is deducted for walking 1 block
        public static int ENERGY_COST_FOR_GATHERING = -1; //how much energy is deducted for picking up

        /*action priorities*/        
        public static int ACTION_SLEEP_PRIORITY = 1;
        public static int ACTION_EAT_PRIORITY = 10;
        public static int ACTION_REST_PRIORITY = 10;
        public static int ACTION_GATHER_PRIORITY = 100;
        public static int ACTION_WALK_PRIORITY = 100;
        public static int ACTION_SEARCH_PRIORITY = 100;
        public static int ACTION_SCAN_PRIORITY = 100;
        public static int ACTION_NO_PRIORITY = 99999;
        public static int VARIABLE_FOR_ACTION_NONE = -1;

        /*LOGGER*/
        public static LoggerClass LOGGER; //logger for debugging purposes
        public static string DEBUG_LOG_DIRECTORY = @"..\..\Debug\";
        public static string DEBUG_LOG_FILENAME = "debug.log";
        public static string GAME_LOG_FILENAME = "gamelog.log";
        public static string QUEUE_LOG_FILENAME = "queuelog.log";
        public static string MAP_LOG_FILENAME = "maplog.log";
        public static string INVENTORY_LOG_FILENAME = "invlog.log";
        public static string CRASH_LOG_FILENAME = "crash.log";
        public static string CHAR_LOG_FILENAME = "char.log";

        /*DEBUG LEVEL (default off*/
        public static DEBUG_LEVELS DEBUG_LOG_LEVEL;

        /*GAME TIME*/
        public static GameTimeClass gameTime;
        public static int SECONDS_IN_ONE_MINUTE = 60;
        public static int MINUTES_IN_ONE_HOUR = 60;
        public static int HOURS_IN_ONE_DAY = 24;
        public static int DAYS_IN_ONE_MONTH = 30;
        public static int MONTHS_IN_ONE_YEAR = 12;

        public static int GAME_SPEED = 60; //number of game minutes to 1 real life second

        /*RANDOM NUMBER GENERATOR*/
        public static RandomClass RANDOMIZER = new RandomClass();

        /*TERRAIN TYPES*/
        public enum TERRAIN_TYPE {FOREST, GRASS, DESERT, HILL, DIRT};        
        public static int TERRAIN_FATIGUE_FOR_GRASS = 0;
        public static int TERRAIN_FATIGUE_FOR_DIRT = 0;
        public static int TERRAIN_FATIGUE_FOR_HILL = -5;
        public static int TERRAIN_FATIGUE_FOR_FOREST = -5;
        public static int TERRAIN_FATIGUE_FOR_DESERT = -10;

        /*RESOURCE TYPES*/
        public enum RESOURCE_TYPE { WOOD, FOOD, NONE };
        public static int RESOURCE_WOOD_GENERATE_RATE = 10; //number of wood generated per tick
        public static int RESOURCE_FOOD_GENERATE_RATE = 0; //number of food generated per tick

        /*INVENTORY*/
        public static int INVENTORY_BLOCK_MAX_CAP = 100; //maximum number of unique items in the inventory
        public static int INVENTORY_MAX_QUANTITY_PER_ITEM = 999; //maximum number per unique items in the inventory
        public static int INVENTORY_MAX_CHAR_CAP = 1000; //maximum number of unique items character can have

        /*BLOCK EXITS*/
        public enum EXITS { NORTH, SOUTH, EAST, WEST};

        /*BUILDING TYPE*/
        public enum BUILDING { HUT, FARM, WALL, STORAGE};

        /*PATHFINDING PENALTIES*/
        public static int WALL_PENALTY_TO_H_VALUE = 10;
                
        /*METHODS*/
        public static Guid GET_ROOMID_BASED_BLOCKID(Guid blockID)
        {
            if (blockID == Guid.Empty) { return Guid.Empty; }
            else { return ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[blockID].getRoomID(); }
        }
    }
}

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