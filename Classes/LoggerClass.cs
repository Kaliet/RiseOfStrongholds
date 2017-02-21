using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class LoggerClass
    {        
        public LoggerClass() { }

        public void createNewFiles ()
        {
            try
            {
                System.IO.File.WriteAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.DEBUG_LOG_FILENAME, "");
                System.IO.File.WriteAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.GAME_LOG_FILENAME, "");
                System.IO.File.WriteAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.QUEUE_LOG_FILENAME, "");
                System.IO.File.WriteAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.MAP_LOG_FILENAME, "");
                System.IO.File.WriteAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.INVENTORY_LOG_FILENAME, "");
                System.IO.File.WriteAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.CRASH_LOG_FILENAME, "");
            }                  
            catch (DirectoryNotFoundException e)
            {
                System.IO.Directory.CreateDirectory(ConstantClass.DEBUG_LOG_DIRECTORY);
                System.IO.File.WriteAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.DEBUG_LOG_FILENAME, "");
            }
            catch (Exception e)
            {                
                Console.WriteLine(e.ToString());
            }            
        }

        public string getRealDateTime()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int hour = DateTime.Now.Hour;
            int mins = DateTime.Now.Minute;
            int secs = DateTime.Now.Second;
            int milisec = DateTime.Now.Millisecond;

            string datetime = "";
            datetime += year;
            datetime += "/";
            datetime += (month < 10) ? "0" + month.ToString() : month.ToString();
            datetime += "/";
            datetime += (day < 10) ? "0" + day.ToString() : day.ToString();
            datetime += " ";
            datetime += (hour < 10) ? "0" + hour.ToString() : hour.ToString();
            datetime += ":";
            datetime += (mins < 10) ? "0" + mins.ToString() : mins.ToString();
            datetime += ":";
            datetime += (secs < 10) ? "0" + secs.ToString() : secs.ToString();
            datetime += ":";
            datetime += milisec;

            return datetime;
        }

        public void writeToDebugLog(string text)
        {
            string datetime = getRealDateTime();

            text = datetime + "\t\t - \t" + text + "\n";
            try
            {
                System.IO.File.AppendAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.DEBUG_LOG_FILENAME, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void writeToGameLog(string text)
        {
            string datetime = getRealDateTime();

            //text = ConstantClass.gameTime.ToString() + "\t\t - \t" + text + "\n";
            text = datetime + "\t\t - \t" + text + "\n";

            try
            {
                System.IO.File.AppendAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.GAME_LOG_FILENAME, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void writeToQueueLog(string text)
        {
            text = ConstantClass.gameTime.ToString() + "\t\t - \t" + text + "\n";
            try
            {
                System.IO.File.AppendAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.QUEUE_LOG_FILENAME, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void writeToMapLog(string text)
        {            
            try
            {
                System.IO.File.AppendAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.MAP_LOG_FILENAME, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void writeToInventoryLog(string text)
        {
            text = ConstantClass.gameTime.ToString() + "\t\t - \t" + text + "\n";

            try
            {
                System.IO.File.AppendAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.INVENTORY_LOG_FILENAME, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void writeToCrashLog(string text)
        {
            string datetime = getRealDateTime();

            text = datetime + "\t\t - \t" + text + "\n";

            try
            {
                System.IO.File.AppendAllText(ConstantClass.DEBUG_LOG_DIRECTORY + ConstantClass.CRASH_LOG_FILENAME, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void writeToCrashLog (Exception e)
        {
            writeToCrashLog("EXCEPTION caught!\n");
            writeToCrashLog("Source:\t\t\t" + e.TargetSite.ToString());
            writeToCrashLog("Message:\t\t\t" + e.Message);
            writeToCrashLog("StackTrace:\t\t" + e.StackTrace.ToString());
        }
    }
}
