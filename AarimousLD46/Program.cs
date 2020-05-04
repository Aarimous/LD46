using System;
using System.Threading;

namespace AarimousLD46
{
    class Program
    {

        static void Main (string[] args) {


            Console.SetWindowSize(100,40);

            while (true) {
                Console.CursorVisible = false;

                startingStory ();
                
                while (true) {
                    Console.Clear();
                    Game game = new Game ();

                    game.run ();

                    Console.WriteLine ("Play again? (Y/N)...");
                    while (true) {
                        ConsoleKeyInfo cki;
                        if (Console.KeyAvailable == true) {
                            cki = Console.ReadKey (true);

                            if (cki.Key == ConsoleKey.Y) {
                                Console.WriteLine ("Starting new shift...");
                                Thread.Sleep (500);

                                break;
                            } else if (cki.Key == ConsoleKey.N) {
                                return;
                            }
                        }
                        Thread.Sleep (5);
                    }
                }

            }

        }

        private static void startingStory () {

            int sleep = 3000;
            int sleepShort = 1000;
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep (sleepShort);
            Console.WriteLine ("============================================");
            Console.WriteLine ("==----------------------------------------==");
            Console.Write ("==          ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write ("Dr. ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write ("Take ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write ("Your ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write ("Pills");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine ("           ==");
            Console.WriteLine ("==----------------------------------------==");
            Console.WriteLine ("============================================");
            Thread.Sleep (sleepShort);
            Console.Write ("== An");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write (" Arb Walk ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine ("production by Andrew Olson ==");
            Console.WriteLine ("============================================");
            Thread.Sleep (sleepShort);
            Console.WriteLine ("  [Press Any Key to Continue Story]  ");
            Console.WriteLine (">> Your 24 hour shift is starting soon...");
            Console.ReadKey (true);
            Console.WriteLine (">> Better put on your scrubs and head to the ER");
            Thread.Sleep (sleepShort);
            Console.WriteLine (". . .");
            Console.ReadKey (true);
            Console.WriteLine (">> You enter the ER and see your superior Dr. Bigshot");
            Console.ReadKey (true);
            Console.WriteLine (">> You approach Dr. Bigshot and say “Hello”");
            Console.ReadKey (true);
            Console.WriteLine ("Intern, I don't have time for this.. listen up");
            Console.ReadKey (true);
            Console.WriteLine ("Since this is your first 24 hour shift, we're only giving you 1 patient");
            Console.ReadKey (true);
            Console.WriteLine (">> You are slightly offended, but overall relieved");
            Console.ReadKey (true);
            Console.WriteLine ("Don’t look so happy.. this patient has been a real SoB");
            Console.ReadKey (true);
            Console.WriteLine ("I’ve managed to get most of their vitals within the normal ranges..");
            Console.ReadKey (true);
            Console.WriteLine (">> You grab the chart.. start to ask a question, but Dr. BigShot cuts you off..");
            Console.ReadKey (true);
            Console.WriteLine ("This is my patient so I’ll determine the long term treatments");
            Console.ReadKey (true);
            Console.WriteLine ("Your ONLY job is to keep the patient alive while I'm in Vegas for the next 24 hours");
            Console.ReadKey (true);
            Console.WriteLine ("This is done by keeping the vitals in the normal ranges..");
            Console.ReadKey (true);
            Console.WriteLine (">> Without wait to hear your response, the doc heads for the exit");
            Console.ReadKey (true);
            Console.WriteLine ("Don’t call, just keep this patient alive . . .");
            Console.ReadKey (true);
            Console.WriteLine (">> The door slams shut and you are left holding the chart");
            Console.ReadKey (true);
            Console.WriteLine ("==========================================");
            Console.ReadKey (true);
            Console.WriteLine ("Each hour of your shift will consist of:");
            Thread.Sleep (sleepShort);
            Console.WriteLine ("   1. Reading the tests (changes in vitals from the last hour)");
            Thread.Sleep (sleepShort);
            Console.WriteLine ("   2. Using a treatment from your inventory");
            Thread.Sleep (sleepShort);
            Console.WriteLine ("   3. Adding a new treatment to your inventory");
            Console.ReadKey (true);
            Console.WriteLine ("Good luck, press any key to begin...");
            Console.ReadKey (true);
            Console.WriteLine ("Starting your shift...");
            Thread.Sleep (sleepShort);
        }
    }
}
