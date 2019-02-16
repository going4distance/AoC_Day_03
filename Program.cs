using System;
using System.IO;

namespace AdventOfCode_Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = 1000;
            int columns = 1000;
            char[,] fabric = new char[rows, columns];

            int startL = 0; int startT = 0; int width = 0; int height = 0; int idNumber = 0;
            string path = @"C:\Users\computer\source\repos\AdventOfCode-Day03\Advent_Calendar_-_day_3_input.txt";
            
            //  ========  Done Initialization  ==================
            string readText = File.ReadAllText(path);
            string[] allFileLines = readText.Split('\n');
            //  ========  Begin Matrix Fill  ==================
            for (int f = 0; f < allFileLines.Length; f++) {
                ParseLine(ref idNumber, ref startL, ref startT, ref width, ref height, allFileLines[f]);
                // marks the fabric based on the current line's pattern
                for (int x = startT; x<startT+height; x++)
                {
                    for(int y = startL; y < startL+width; y++)
                    {
                            if(fabric[x,y] == '\0')
                            {   // if space is not yet in use.
                                fabric[x, y] = '1';
                            }
                            else { // if space used by another cut
                            fabric[x, y] = '#';
                            }
                    }
                }
            }   // End for loop
            // ================  Done filling matrix  ========================
            //  Count # in fabric
            int overLapCount = 0;
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (fabric[x, y] == '#')
                    {
                        overLapCount++;
                    }
                }
            }
            Console.WriteLine("There are {0} #'s present in the fabric \n", overLapCount);

//  =========================   BEGIN PART 2  =======================================================
            bool NoOverlap;
            for (int f = 0; f < allFileLines.Length; f++){
                ParseLine(ref idNumber, ref startL, ref startT, ref width, ref height, allFileLines[f]);
                NoOverlap = true;
                for (int x = startT; x < startT + height; x++)
                {
                    for (int y = startL; y < startL+width; y++)
                    { 
                            if (fabric[x, y] == '#')
                            {   // if space is used by multiple claims
                                NoOverlap = false;
                                break;
                            }
                    }
                }
                if (NoOverlap)
                {   
                    Console.WriteLine("Found an uncontested claim.  ID#: {0}", idNumber);
                    break;  // no need to search the rest of the file, because we assume(per the directions) only a single claim can be correct.
                }
            }   // End for loop
            Console.WriteLine("\nPress ENTER to quit.");
            Console.ReadLine();
        }   // END of MAIN




        /// <summary>
        /// Splices the data read from a file, into its appropriate variables. 
        /// </summary>
        static bool ParseLine(ref int _ID, ref int _Left, ref int _Top, ref int _Wide, ref int _Height, string currLine = "#123 @ 3,2: 5x4")
        {
            string[] separatingChars = { "#", ",", "@", ":", "x", " " };
            string[] numData = currLine.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);
                
            // Try to Parse the current Line.
            int[] myStorage = new int[5];
            if (numData.Length == 5){
                bool success = true;
                for(int m = 0; m < 5; m++) { 
                    success = Int32.TryParse(numData[m], out myStorage[m]);
                    if (!success){  // unable to convert one result to an int type.
                        return (false);
                    }
                }   // all numbers successfully converted to int
                _ID = myStorage[0];
                _Left = myStorage[1];
                _Top = myStorage[2];
                _Wide = myStorage[3];
                _Height = myStorage[4];
            }
            else{   // Data is wrong size
                return (false);
            }
            return (true);
        }   // END parseLine ------------------
    }
}
