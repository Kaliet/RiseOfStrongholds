﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class GameTimeClass
    {
        /*VARIABLES*/
        private int game_mins;
        private int game_hours;
        private int game_day;
        private int game_month;
        private int game_year;

        /*GETS & SETS*/
        public void set_years(int value)
        {
            game_year += value;
        }
        public void set_months(int value)
        {
            if (game_month + value > ConstantClass.MONTHS_IN_ONE_YEAR) // if months + value > 12
            {
                set_years((game_month + value) / ConstantClass.MONTHS_IN_ONE_YEAR);
                game_month = (game_month + value) % ConstantClass.MONTHS_IN_ONE_YEAR;
            }
            else game_month += value;
        }
        public void set_days(int value)
        {
            if (game_day + value > ConstantClass.DAYS_IN_ONE_MONTH) //if days + value > 30
            {
                set_months((game_day + value) / ConstantClass.DAYS_IN_ONE_MONTH);
                game_day = (game_day + value) % ConstantClass.DAYS_IN_ONE_MONTH;
            }
            else game_day += value;
        }
        public void set_hours(int value)
        {
            if (game_hours + value >= ConstantClass.HOURS_IN_ONE_DAY) //if hours + value >= 24
            {
                set_days((game_hours + value) / ConstantClass.HOURS_IN_ONE_DAY);
                game_hours = (game_hours + value) % ConstantClass.HOURS_IN_ONE_DAY;
            }
            else game_hours += value;
        }
        public void set_mins(int value)
        {
            if (game_mins + value >= ConstantClass.MINUTES_IN_ONE_HOUR) //if mins + values >= 60
            {
                set_hours((game_mins + value) / ConstantClass.MINUTES_IN_ONE_HOUR);
                game_mins = (game_mins + value) % ConstantClass.MINUTES_IN_ONE_HOUR;
            }
            else game_mins += value;
        }
        

        public int get_min() { return game_mins; }
        public int get_hour() { return game_hours; }
        public int get_day() { return game_day; }
        public int get_month() { return game_month; }
        public int get_year() { return game_year; }

        public int getTotalMins ()
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t->getTotalMins()"); };

            return (game_mins +
                    game_hours * ConstantClass.MINUTES_IN_ONE_HOUR +
                    game_day * ConstantClass.HOURS_IN_ONE_DAY * ConstantClass.MINUTES_IN_ONE_HOUR +
                    game_month * ConstantClass.DAYS_IN_ONE_MONTH * ConstantClass.HOURS_IN_ONE_DAY * ConstantClass.MINUTES_IN_ONE_HOUR +
                    game_year * ConstantClass.MONTHS_IN_ONE_YEAR * ConstantClass.DAYS_IN_ONE_MONTH * ConstantClass.HOURS_IN_ONE_DAY * ConstantClass.MINUTES_IN_ONE_HOUR);

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t<-getTotalMins()"); };
        }

        /*CONSTRUCTORS*/
        public GameTimeClass()
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t->GameTimeClass()"); };

            game_hours = 8;
            game_mins = 0;
            game_day = 1;
            game_month = 1;
            game_year = 1;

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t<-GameTimeClass()"); };
        }

        public GameTimeClass(GameTimeClass input)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t->GameTimeClass()"); };

            game_mins = input.get_min();
            game_hours = input.get_hour();
            game_day = input.get_day();
            game_month = input.get_month();
            game_year = input.get_year();

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t<-GameTimeClass()"); };
        }

        public GameTimeClass(int min,int hour, int day, int month, int year)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t->GameTimeClass()"); };
                       
            game_mins = min;
            game_hours = hour;
            game_day = day;
            game_month = month;
            game_year = year;

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t<-GameTimeClass()"); };
        }
      
        /*OVERRIDE & OVERLOAD*/
        public override string ToString()
        {
            string min, hour, day, month, year;

            if (0 <= game_mins && game_mins <= 9) { min = "0" + game_mins.ToString(); } else min = game_mins.ToString();
            if (0 <= game_hours && game_hours <= 9) { hour = "0" + game_hours.ToString(); } else hour = game_hours.ToString();
            if (0 <= game_day && game_day <= 9) { day = "0" + game_day.ToString(); } else day = game_day.ToString();
            if (0 <= game_month && game_month <= 9) { month = "0" + game_month.ToString(); } else month = game_month.ToString();
            year = game_year.ToString();

            //TODO: Pad and format the dates and time
            return day  + "/" + month + "/" + year + "\t" + hour + ":" + min ;
        }

        public static bool operator == (GameTimeClass a, GameTimeClass b)
        {
            if (a.get_min() == b.get_min() &&
                a.get_hour() == b.get_hour() &&
                a.get_day() == b.get_day() &&
                a.get_month() == b.get_month() &&
                a.get_year() == b.get_year())
            {
                return true;
            }
            else return false;
        }

        public static bool operator !=(GameTimeClass a, GameTimeClass b)
        {
            return !(a == b);
        }

        public static bool operator >=(GameTimeClass a, GameTimeClass b)
        {
            return (a.getTotalMins() >= b.getTotalMins());
        }

        public static bool operator <=(GameTimeClass a, GameTimeClass b)
        {
            return (a.getTotalMins() <= b.getTotalMins());
        }

        public static bool operator >(GameTimeClass a, GameTimeClass b)
        {
            return (a.getTotalMins() > b.getTotalMins());
        }

        public static bool operator <(GameTimeClass a, GameTimeClass b)
        {
            return (a.getTotalMins() < b.getTotalMins());
        }

        /*GAMETIME MODIFICATIONS*/
        public void updateGameTimeBasedOnElapsedTimeSpan (TimeSpan elapsedTime)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t->updateGameTimeBasedOnElapsedTimeSpan()"); };

            set_mins((int)elapsedTime.TotalSeconds * ConstantClass.GAME_SPEED);

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t<-updateGameTimeBasedOnElapsedTimeSpan()"); };
            
        }
    }
}