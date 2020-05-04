using System;
using System.Collections.Generic;
using System.Threading;
namespace AarimousLD46 {
    public class Player {
        public Player () {
            inventory = new Card[invetorySize];

            setStartingInventory();

            invPropt = "";
        }

        private List<Effect> tempEff = new List<Effect>();
        private int invetorySize = 5;

        public Card[] inventory { get; set; }
        private string invPropt { get; set; }


        private void setStartingInventory(){


            for (int i = 0; i < invetorySize; i++){
                int cardNum = Game.Random.Next(0,Game.CurrentDeck.Count);
                this.inventory[i] = Game.CurrentDeck[cardNum];
                Game.CurrentDeck.RemoveAt(cardNum);
            }
        }

        public Card selectInventoryItem () {
            List<ConsoleKey> validKeys = new List<ConsoleKey> ();
            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo ();
            Card cardSelected = null;
            validKeys = getInentoryItems ();
            if (validKeys.Count == 0) {
                Game.View.writeScrollBuffer (Game.Util.INVETRORY_IS_EMPTY);

            } else {

                Game.View.writeScrollBuffer (Game.Util.USE_A_TREATMENT );

                Game.View.writeScrollBuffer (invPropt);

                keyPressed = getInput (validKeys);
                switch (keyPressed.Key) {
                    case ConsoleKey.D1:
                        cardSelected = this.inventory[0];
                        this.inventory[0] = null;
                        break;
                    case ConsoleKey.D2:
                        cardSelected = this.inventory[1];
                        this.inventory[1] = null;
                        break;
                    case ConsoleKey.D3:
                        cardSelected = this.inventory[2];
                        this.inventory[2] = null;
                        break;
                    case ConsoleKey.D4:
                        cardSelected = this.inventory[3];
                        this.inventory[3] = null;
                        break;
                    case ConsoleKey.D5:
                        cardSelected = this.inventory[4];
                        inventory[4] = null;
                        break;
                }

                if(cardSelected == null){
                     Game.View.writeScrollBuffer("ERROR: Card Was Null In useInventory");
                }
                else{
                    foreach(Effect e in cardSelected.Effects){
                         Game.View.writeScrollBuffer(e.getEffectStr());
                    }
                }
                
            }
            return cardSelected;
        }

        public Card pickCardToAddInv (List<Card> newCards) {
            
            int num = 1;

            List<ConsoleKey> validKeys = new List<ConsoleKey> ();
            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo ();

            Game.View.writeScrollBuffer(Game.Util.AVALABLE_TREATMENTS);
            foreach (Card c in newCards) {

                Game.View.writeScrollBuffer (num + " : " + c.toStr());

                switch (num) {
                    case 1:
                        validKeys.Add (ConsoleKey.D1);
                        break;
                    case 2:
                        validKeys.Add (ConsoleKey.D2);
                        break;
                    case 3:
                        validKeys.Add (ConsoleKey.D3);
                        break;
                    case 4:
                        validKeys.Add (ConsoleKey.D4);
                        break;
                }
                num++;
            }
            Game.View.writeScrollBuffer(Game.Util.ADD_A_TREATMENT);
            keyPressed = getInput (validKeys);
            Card addCard = null;
            switch (keyPressed.Key) {
                case ConsoleKey.D1:
                    addCard = newCards[0];
                    break;
                case ConsoleKey.D2:
                    addCard = newCards[1];
                    break;
                case ConsoleKey.D3:
                    addCard = newCards[2];
                    break;
                case ConsoleKey.D4:
                    addCard = newCards[3];
                    break;
            }
            for(int i = 0; i < inventory.Length; i++){
                if(inventory[i] == null){
                    inventory[i] = addCard;
                    break;
                }
            } 
            
            return addCard;
        }

        public void endTurn () {
            List<ConsoleKey> validKeys = new List<ConsoleKey> ();
            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo ();
            Game.View.writeScrollBuffer (Game.Util.END_TURN_TEXT);

            //Console.SetCursorPosition (2, Console.CursorTop);
            //validKeys = new List<ConsoleKey> { ConsoleKey.Y };
            Console.CursorVisible = true;
            keyPressed = getInput (validKeys);
            Console.CursorVisible = false;
            
            return;

        }

        private List<ConsoleKey> getInentoryItems () {

            String str = "Click ";
            List<ConsoleKey> retrunLst = new List<ConsoleKey> ();

            for (int i = 0; i < inventory.Length; i++) {
                if (inventory[i] != null) {
                    str += " " + (i + 1) + " ";

                    switch (i) {
                        case 0:
                            retrunLst.Add (ConsoleKey.D1);
                            break;
                        case 1:
                            retrunLst.Add (ConsoleKey.D2);
                            break;
                        case 2:
                            retrunLst.Add (ConsoleKey.D3);
                            break;
                        case 3:
                            retrunLst.Add (ConsoleKey.D4);
                            break;
                        case 4:
                            retrunLst.Add (ConsoleKey.D5);
                            break;
                    }
                }
            }

            invPropt = str;
            return retrunLst;
        }

        //if you pass this a List with no items it will assume and key can be pressed
        private ConsoleKeyInfo getInput (List<ConsoleKey> validKeys) {
            //Console.CursorVisible = true;
            ConsoleKeyInfo cki = new ConsoleKeyInfo ();
            while (true) {

                if (Console.KeyAvailable == true) {
                    cki = Console.ReadKey (true);
                    
                    if (validKeys.Count == 0 ||validKeys.Contains (cki.Key)) {
                        break;
                    }
                }
            }
            //Console.CursorVisible = false;
            return cki;
        }

    }

}