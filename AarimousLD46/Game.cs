using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace AarimousLD46 {
    public class Game {

        public Game () {

            //setup game varibales
            Random = new Random ();
            Util = new Utilities ();
            currentTurn = 1;
            setDecks ();

            CurrentDeck = startDeck;
            //get view read
            View = new View (Console.WindowHeight, Console.WindowWidth, '=');
            ScrollBuffer = new Queue<String> ();

            //get game entities
            setPatient (50, 100, 140, 110, 98, 85);
            Player = new Player ();

            //draw view
            View.Update (_patient, Player);

            //and awayyyy we gooo
            //View.writeScrollBuffer ("Welome to the game");

        }

        //Basic classes we need game wide
        public static Random Random;
        public static Utilities Util;
        public static View View;

        //game objects
        public static Queue<String> ScrollBuffer { get; set; }
        public static Player Player { get; set; }
        private Patient _patient { get; set; }
        public static Dictionary<VitalType, int[]> scriptMap { get; set; }

        public static int totalTurns = 24;
        public static int currentTurn { get; set; }

        private int numCardsToOffer = 3;
        public static List<Card> CurrentDeck { get; set; }

        private static List<Card> startDeck;
        private static List<Card> midDeck;
        private static List<Card> endDeck;

        List<Card> cardsToOffer = new List<Card> ();

        public void run () {

            while (true) {
                string dig = "";
                if(Game.currentTurn < 10){
                    dig = "0";
                }
                else{
                    dig ="";
                }
                View.writeScrollBuffer ("Time " + dig + currentTurn + ":00");

                //Thread.Sleep(Game.Util.GLOBAL_SLEEP);

                Card cardPicked = Player.selectInventoryItem ();
                _patient.applyCard (cardPicked);
                _patient.setDeltaHealth ();
                View.Update (this._patient, Player);

                cardsToOffer = updateCardToDraft ();
                Card cardToAdd = Player.pickCardToAddInv (cardsToOffer);
                CurrentDeck.Remove (cardToAdd);

                View.Update (this._patient, Player);
                Player.endTurn ();
                View.Update (this._patient, Player);

                this._patient.update ();
                currentTurn++;

                //handle setting the turn
                //updating the current deck
                if (endTurnCleanup ()) {
                    break;
                }

                View.Update (this._patient, Player);

            }

        }

        private Boolean endTurnCleanup () {
            //todo if size of deck is less than the size of card to offer
            //we need to add more cards to the deck OR move to the next phase deck

            if (_patient.MajorVitalMap[VitalType.Health].CurrentValue == 0) {
                gameOver ();
                return true;
            } else if (currentTurn > totalTurns) {
                gameWin ();
                return true;
            }

            if (CurrentDeck.Count < numCardsToOffer) {
                View.writeScrollBuffer ("=============Moving to Mid Deck ===============");
                CurrentDeck = midDeck;
            }

            return false;
        }

        private void gameOver () {
            Console.Clear ();
            Console.WriteLine ("Time of death.. " + currentTurn + ":00");
            Console.WriteLine ("The patient has died.. head home and get ready for the next shift..");
        }
        private void gameWin () {
            Console.Clear ();
            Console.WriteLine ("Your 24 hour shift is over and the patient is still alive.");
            Console.WriteLine ("Congratulations doctor!");
            Console.WriteLine ("=========================");
            scoreGame ();
            Console.WriteLine ("=========================");
        }

        private void scoreGame () {
            Console.WriteLine ("TODO: SCORE GAME");
        }

        private void setPatient (int startHealth,
            int startHeartRate,
            int startSystolic,
            int startDiastolic,
            int startTemp,
            int start02) {

            this._patient = new Patient (startHealth,
                Random.Next (-10, 10) + startHeartRate,
                Random.Next (-10, 10) + startSystolic,
                Random.Next (-10, 10) + startDiastolic,
                Random.Next (-3, 3) + startTemp,
                Random.Next (-5, 5) + start02);

        }

        private List<Card> updateCardToDraft () {

            List<Card> returnCards = new List<Card> ();
            int randCardIndex = 0;
            for (int i = 0; i < this.numCardsToOffer; i++) {
                randCardIndex = Random.Next (0, CurrentDeck.Count);
                returnCards.Add (CurrentDeck[randCardIndex]);

                CurrentDeck.RemoveAt (randCardIndex);
            }

            return returnCards;

        }

        //not used
        /*
        private void setScript () {

            scriptMap = new Dictionary<VitalType, int[]> ();
            //TODO finish scripts
            scriptMap.Add (VitalType.HeartRate, new int[] { 5, -4, -10, 2, -5, -2, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            scriptMap.Add (VitalType.SystolicBP, new int[] { 2, 2, 3, 4, 3, 10, -10, -10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            scriptMap.Add (VitalType.DiastolicBP, new int[] { 2, 2, 3, 4, -5, -6, -2, -5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            scriptMap.Add (VitalType.Temperature, new int[] {-2, 1, 1, 1, 1, -1, 2, 1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            scriptMap.Add (VitalType.O2Level, new int[] {-1, 0, -2, 1, -4, -3, -2, 1, -10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        }
        */

        private void setDecks () {

            startDeck = setStartingCardDeck ();
            midDeck = setMidDeck ();

        }

        private List<Card> setStartingCardDeck () {

            List<Card> deck = new List<Card> ();

            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.HeartRate, new Queue<int> (new [] { 7, 0, 0, 0 }), "") }));
            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.HeartRate, new Queue<int> (new [] {-5, 0, 0, 0 }), "") }));
            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.SystolicBP, new Queue<int> (new [] { 7, 0, 0, 0 }), "") }));
            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.SystolicBP, new Queue<int> (new [] {-6, 0, 0, 0 }), "") }));
            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.DiastolicBP, new Queue<int> (new [] { 3, 0, 0, 0 }), "") }));
            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.DiastolicBP, new Queue<int> (new [] { 4, 0, 0, 0 }), "") }));
            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.O2Level, new Queue<int> (new [] { 3, 0, 0, 0 }), "") }));
            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.O2Level, new Queue<int> (new [] { 2, 0, 0, 0 }), "") }));
            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.Temperature, new Queue<int> (new [] {-1, 0, 0, 0 }), "") }));
            deck.Add (new Card (Util.INCREASE_NOW_HEARTRATE_BASIC.getEffectStr (), new List<Effect> { new Effect (VitalType.Temperature, new Queue<int> (new [] { 1, 0, 0, 0 }), "") }));

            return deck;

        }

        private List<Card> setMidDeck () {

            List<Card> deck = new List<Card> ();

            List<int> midRanges = new List<int> { 8, 12, 11, 5, 2 };

            int vitalType = 0;
            for (int i = 0; i < 100; i++) {
                vitalType = Random.Next (0, 5);
                switch (vitalType) {
                    case 0:
                        deck.Add (new Card ("", new List<Effect> { new Effect (VitalType.HeartRate, new Queue<int> (new [] { getDelta(-midRanges[0], midRanges[0]) }), "") }));
                        break;
                    case 1:
                        deck.Add (new Card ("", new List<Effect> { new Effect (VitalType.SystolicBP, new Queue<int> (new [] { getDelta (-midRanges[1], midRanges[1]) }), "") }));
                        break;
                    case 2:
                        deck.Add (new Card ("", new List<Effect> { new Effect (VitalType.DiastolicBP, new Queue<int> (new [] { getDelta (-midRanges[2], midRanges[2]) }), "") }));
                        break;
                    case 3:
                        deck.Add (new Card ("", new List<Effect> { new Effect (VitalType.O2Level, new Queue<int> (new [] { getDelta (-midRanges[3], midRanges[3]) }), "") }));
                        break;
                    case 4:
                        deck.Add (new Card ("", new List<Effect> { new Effect (VitalType.Temperature, new Queue<int> (new [] { getDelta (-midRanges[4], midRanges[4]) }), "") }));
                        break;
                    default:
                        break;
                }
            }
            return deck;
        }
        
        //pevent treatmenats with a zero effect
        private int getDelta (int min, int max) {
            int delta = 0;
            while (true) {
                delta =  Random.Next (min, max);
                if (delta != 0) {
                    break;
                }
            }
            return delta;
        }


    }
}