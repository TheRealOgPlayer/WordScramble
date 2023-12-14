/*
Made By: therealogplayer
Made On: 08/23/2023 
Updated On: 12/11/2023
Update Done On: 12/14/2023
*/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace HelloWord
{
    class Program
    {
        public static bool DebugMode = false;
        public static bool TextRestrictions = true;
        public static bool wordLimit = true;
        public static bool wordRepeat = false;
        public static bool wordKeepsForm = true;


        public static void EndProgram(long endTime)
        {
            //Gets The End Time To Tell The User

            Console.WriteLine("This Program Took: " + endTime + "ms To Complete\nWant to run again(Y)(N)");
            string userInput = Console.ReadLine().ToUpper();

            if (userInput == "N")
            {
                Environment.Exit(1);
            }
            else
            {
                startProgram();
            }

        }

        static void openMenu()
        {
            //Menu for the program
            try
            {
                //Clears console and then gets the user input
                if (!DebugMode) Console.Clear();
                Console.Write("\t\nInstructions:\n\n1.Type the # you want to toggle and press enter\n2.Type `E` when you want to return to the program.\n3.Type `H` after for a brief explanation. This will not chnage the option\t\n\nOptions:\n\n0: Debug Mode: {0}\n1: Text Restrictions: {1}\n2: Word Limit {2}\n3: Word Repeat {3}\n4: Words Keeps Form: {4}\n",
                    DebugMode ? "Enabled" : "Disabled", TextRestrictions ? "Enabled" : "Disabled", wordLimit ? "Enabled" : "Disabled", wordRepeat ? "Enabled" : "Disabled", wordKeepsForm ? "Enabled" : "Disabled");
                string userInput = Console.ReadLine().ToUpper();

                //Changes the bool to true or false based on what user toggled
                switch (Convert.ToString(userInput[0]))
                {
                    case "0":
                        //Check if the user wanted to change option or get the help for the option.
                        if (userInput.Length > 1 && Convert.ToString(userInput[1]) == "H")
                        {
                            Console.Write("Debug Mode gives the user more information if the program runs into an error.\nIt will also prevent the console from clearing\nPress Any Key To Continue\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            DebugMode = DebugMode ? false : true;
                        }
                        break;
                    case "1":
                        if (userInput.Length > 1 && Convert.ToString(userInput[1]) == "H")
                        {
                            Console.Write("If Text Restrictions are enabled then the user cannot type words with resricted letters IE ';' '+'.\nPress Any Key To Continue\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            TextRestrictions = TextRestrictions ? false : true;
                        }
                        break;
                    case "2":
                        if (userInput.Length > 1 && Convert.ToString(userInput[1]) == "H")
                        {
                            Console.Write("Word Limit is the max length a word can be(10) if diabled then you can have as big a word you want.\nPress Any Key To Continue\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            wordLimit = wordLimit ? false : true;
                        }
                        break;
                    case "3":
                        if (userInput.Length > 1 && Convert.ToString(userInput[1]) == "H")
                        {
                            Console.Write("Word Repeat if enabled will allow the program to repeat a word multiple times.\nPress Any Key To Continue\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            wordRepeat = wordRepeat ? false : true;
                        }
                        break;
                    case "4":
                        if (userInput.Length > 1 && Convert.ToString(userInput[1]) == "H")
                        {
                            Console.Write("Word Keep Form if enabled will allow the program to not keep the word form. IE say the word is 'Hello' \nWith Enabled:'Hlelo'\nWith Disabled:'llllo'\nPress Any Key To Continue\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            wordKeepsForm = wordKeepsForm ? false : true;
                        }
                        break;
                    case "E":
                        startProgram();
                        break;
                }

                openMenu();
            }
            catch (Exception e)
            {
                //Catch all in case something fails.
                if (!DebugMode) Console.WriteLine("Error of some kind\nPress Any Key To Return.");
                if (DebugMode) Console.WriteLine("Error of some kind\n\nDebug Info: \n{0}\n\nPress Any Key To Return.", e);
                Console.ReadKey();
                openMenu();
            }
        }
        static void startProgram()
        {
            try
            {
                if (!DebugMode) Console.Clear();
                //Gets The Word That The User Wants To Scramble
                Console.WriteLine("(M) for menu\nWhat Word Should I Scramble: \n");

                //Variables The Script Use
                string unModWord = Console.ReadLine();
                string Word = unModWord;
                string[] guessedWords = new string[0];
                //Sees if program should or should not open the menu.
                if (Word[0].ToString().ToUpper() == "M" && Word.Length == 1)
                {
                    openMenu();
                    return;
                }

                string forstringA = "";
                int counter = 1;

                long StartTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                List<string> afterScrampleList = new List<string>();
                List<string> formStringList = new List<string>();

                //Checks To Make Sure Word Provided Is Valid
                if (TextRestrictions)
                {
                    string[] IlligalChar = { "(", ")", ";", "'", "+", "-", "*", "\n", "/", "||", "{", "}", "[", "]" };
                    for (int i = 0; i < IlligalChar.Length; i++)
                    {
                        if (Word.Contains(IlligalChar[i]) == true)
                        {
                            Console.WriteLine("The Word You Have Selected, " + Word + " Is Not In The Range Of 2-10 Characters\n");
                            EndProgram(0);
                        }
                    }
                }
                //Word Limit
                if (((Word.Length >= 10 || Word.Length <= 1) && wordLimit))
                {
                    Console.WriteLine("The Word You Have Selected, " + Word + " Is Not In The Range Of 2-10 Characters\n");
                    EndProgram(0);
                }

                //Turns Word Into An Array
                for (int i = 0; i < Word.Length; i++)
                {
                    char x = Word[i];
                    afterScrampleList.Add(x.ToString());
                }

                string[] afterScrample = afterScrampleList.ToArray(); //Turns the list of letters back into an array

                if (!DebugMode) Console.Clear(); //Clear The Console So It Looks Perty
                Console.WriteLine("The Word I Started From Was: " + Word + "\n");

                //Logic For Scrambaling the word
                do
                {
                    Thread.Sleep(1);
                    bool guessedWordNotTheSame = true;
                    Random rnd = new Random();
                    if (wordKeepsForm)
                    {
                        //Scrambles the word with the correct numbers of letters
                        int[] numberOfNumbersNeeded = new int[afterScrample.Length];

                        for (int y = 0; y < numberOfNumbersNeeded.Length; y++)
                        {
                            int RandomNumber = rnd.Next(afterScrample.Length);


                            if (numberOfNumbersNeeded[RandomNumber] == 0)
                            {
                                numberOfNumbersNeeded[RandomNumber] = y;
                            }
                            else
                            {
                                y--;
                            }
                        }
                        for (int a = 0; a < numberOfNumbersNeeded.Length; a++)
                        {
                            formStringList.Add(afterScrample[numberOfNumbersNeeded[a]]);
                        }
                    }
                    else
                    {
                        //Scrambles the word with the incorrect number of letters. 
                        for (int i = 0; i < Word.Length; i++)
                        {
                            int RandomNumber = rnd.Next(afterScrample.Length);
                            formStringList.Add(afterScrample[RandomNumber]);
                        }
                    }


                    string[] formString = formStringList.ToArray(); //Turns the list of letters back into an array

                    forstringA = string.Join("", formString); //Turns Array Into String To Compair

                    if (!wordRepeat)
                    {
                        //Checks if word has already been guess
                        for (int i = 0; i < guessedWords.Length; i++)
                        {
                            if (guessedWords[i] == forstringA)
                            {
                                //Console.WriteLine("Oi {0} | {1} | {2}", guessedWords[i], guessedWords.Length,i);
                                guessedWordNotTheSame = false;
                                if (i != 0) break;
                            }
                        }

                        if (guessedWordNotTheSame)
                        {
                            guessedWords = guessedWords.Append(forstringA).ToArray();
                            Console.WriteLine(forstringA + " " + counter++ + " \n");

                        }
                        else if (string.Join("", forstringA) == Word)
                        {
                            Console.WriteLine(forstringA + " " + counter++ + " \n");
                        }
                    }
                    else
                    {
                        //Does not check if word has been guessed
                        Console.WriteLine(forstringA + " " + counter++ + " \n");
                    }

                    formStringList.Clear(); //Clears Array For Next Guess
                } while (string.Join("", forstringA) != Word); //Checks To See If Word Matched.

                EndProgram(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - StartTime); //Kills Program Once Word Is Found
            }
            catch (Exception e)
            {
                //Catch all error.
                if (!DebugMode) Console.WriteLine("Error of some kind\nPress Any Key To Return.");
                if (DebugMode) Console.WriteLine("Error of some kind\n\nDebug Info: \n{0}\n\nPress Any Key To Return.", e);
                Console.ReadKey();
                startProgram();
            }

        }

        static void Main(string[] args)
        {
            startProgram();
        }
    }
}