using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class GameTimeClass
    {
        /*VARIABLES*/
        private byte game_mins;
        private byte game_hours;
        private byte game_day;
        private byte game_month;
        private short game_year;

        /*GETS & SETS*/
        public void set_years(short value)
        {
            game_year += value;
        }

        public void set_months(byte value)
        {
            if (game_month + value > ConstantClass.MONTHS_IN_ONE_YEAR) // if months + value > 12
            {
                set_years(+1);
                game_month = (byte)(game_month + value - ConstantClass.MONTHS_IN_ONE_YEAR);
            }
            else game_month += value;
        }

        public void set_days(byte value)
        {
            if (game_day + value > ConstantClass.DAYS_IN_ONE_MONTH) //if days + value > 30
            {
                set_months(+1);
                game_day = (byte)(game_day + value - ConstantClass.DAYS_IN_ONE_MONTH);
            }
            else game_day += value;
        }

        public void set_hours(byte value)
        {
            if (game_hours + value >= ConstantClass.HOURS_IN_ONE_DAY) //if hours + value >= 24
            {
                set_days(+1);
                game_hours = (byte)(game_hours + value - ConstantClass.HOURS_IN_ONE_DAY);
            }
            else game_hours += value;
        }

        public void set_mins(byte value)
        {
            if (game_mins + value >= ConstantClass.MINUTES_IN_ONE_HOUR) //if mins + values >= 60
            {
                set_hours(+1);
                game_mins = (byte)(game_mins + value - ConstantClass.MINUTES_IN_ONE_HOUR);
            }
            else game_mins += value;
        }

        public byte get_min() { return game_mins; }
        public byte get_hour() { return game_hours; }
        public byte get_day() { return game_day; }
        public byte get_month() { return game_month; }
        public short get_year() { return game_year; }


        /*CONSTRUCTORS*/
        public GameTimeClass()
        {
            game_hours = 8;
            game_mins = 0;
            game_day = 1;
            game_month = 1;
            game_year = 1;
        }

        public GameTimeClass(GameTimeClass input)
        {
            game_mins = input.get_min();
            game_hours = input.get_hour();
            game_day = input.get_day();
            game_month = input.get_month();
            game_year = input.get_year();
        }

        public GameTimeClass(byte min,byte hour, byte day, byte month, short year)
        {
            game_mins = min;
            game_hours = hour;
            game_day = day;
            game_month = month;
            game_year = year;
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
    }
}
