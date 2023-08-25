/*
Made By therealogplayer
Made On 08/23/2023 
*/

using System;
using System.Collections.Generic;
using System.Threading;

namespace HelloWord
{
    class Program
    {
        public static void EndProgram(long endTime) 
        {
            //Gets The End Time To Tell The User
            
            Console.WriteLine("This Program Took: " + endTime + "ms To Complete\nPress Enter To Close The Window...");
            Console.ReadLine();
            Environment.Exit(1);

        }
        static void Main(string[] args)
        {

            //Gets The Word That The User Wants To Scramble
            Console.WriteLine("What Word Should I Scramble: \n");

            //Variables The Script Use
            string Word = Console.ReadLine();
            string forstringA = "";
            string[] IlligalChar = { "(", ")",";","'","+","-","*","\n","/","||","{", "}","[","]" };

            int counter = 1;

            long StartTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            List<string> afterScrampleList = new List<string>();
            List<string> formStringList = new List<string>();

            //Checks To Make Sure Word Provided Is Valid
            for (int i = 0; i < IlligalChar.Length; i++)
            {
                if (Word.Length >= 10 || Word.Length <= 1 || Word.Contains(IlligalChar[i]) == true)
                {
                    Console.WriteLine("The Word You Have Selected, " + Word + " Is Not In The Range Of 2-10 Characters\n" + "Or\n" + "The Word Contains An Illeagl Character(s)" + "\nFirst Illegal Character Found: " + IlligalChar[i]);
                    EndProgram(0);
                }
            }

            //Turns Word Into An Array
            for(int i = 0; i < Word.Length; i++)
            {
                char x = Word[i];
                afterScrampleList.Add(x.ToString());
                Console.WriteLine(afterScrampleList);
            }
            String[] afterScrample = afterScrampleList.ToArray(); //Turns the list of letters back into an array

            Console.Clear(); //Clear The Console So It Looks Perty
            Console.Beep(); //Beep
            Console.WriteLine("The Word I Started From Was: " + Word + "\n");

            //Logic For Scrambaing the word
            do
            {
                Thread.Sleep(1);
                Random rnd = new Random();
                for (int i = 0; i < Word.Length; i++)
                {
                    int RandomNumber = rnd.Next(afterScrample.Length);
                    formStringList.Add(afterScrample[RandomNumber]); //Makes a random word with the letter pervided
                }

                string[] formString = formStringList.ToArray(); //Turns the list of letters back into an array

                forstringA = string.Join("", formString); //Turns Array Into String To Compair

                Console.WriteLine(forstringA + " " + counter + " \n");

                formStringList.Clear(); //Clears Array For Next Guess
                counter++;

            }while (string.Join("", forstringA) != Word); //Checks To See If Word Matched.

            EndProgram(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - StartTime); //Kills Program Once Word Is Found

        }
    }
}
