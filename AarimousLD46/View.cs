using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace AarimousLD46 {
    public class View {
        public View (int height, int width, char borderChar) {
            this._height = height;
            this._width = width;
            this._borderChar = borderChar;
        }

        private int _height { get; set; }
        private int _width { get; set; }
        private char _borderChar { get; set; }
        private int _invRow = 13;
        private int _healthRow = 3;
        private int _toxRow = 4;
        private int _currentRow = 0;
        private int _sideCol = 2;

        public void Update (Patient patient, Player player) {
            this._currentRow = 0;
            Console.SetCursorPosition (0, 0);

            writeSideBorder ();
            writeBorderLine ();
            Console.SetCursorPosition (this._sideCol, this._currentRow);
            string dig = "";
            if(Game.currentTurn < 10){
                dig = "0";
            }
            else{
                dig ="";
            }
            Console.WriteLine ("Hour " + dig + Game.currentTurn +":00 of " + Game.totalTurns + ":00");
            this._currentRow++;
            writeBorderLine ();
            writeHealth (patient);
            this._currentRow++;
            //writeBloodTox (patient);
            //this._currentRow++;
            writeBorderLine ();

            writeVitals (patient);
            writeBorderLine ();

            updateInventory (player.inventory);
            
            writeBorderLine ();
            //testsAreIn (patient);

            //ready for inputs
            //writeScrollBuffer ();

            Console.SetCursorPosition (2, Console.WindowHeight - 2);
        }

        private void writeSideBorder () {

            Console.SetCursorPosition (0, 0);

            for (int i = 0; i < this._invRow -1; i++) {
                Console.WriteLine (_borderChar + new string (' ', _width - 2) + _borderChar);
            }

        }

        //line 3,4,5,6,7,8,9
        private void writeVitals (Patient pat) {

            //Vitals Title

            Console.SetCursorPosition (this._sideCol, this._currentRow);
            Console.WriteLine (Game.Util.VITALS_TEXT);
            this._currentRow++;
            writeBorderLine ();
            foreach (VitalType vt in pat.MinorVitalMap.Keys) {
                writeVital (pat.MinorVitalMap[vt], this._sideCol, this._currentRow, true);
                this._currentRow++;
            }
        }

        public void writeHealth (Patient pat) {
            Vital v = pat.MajorVitalMap[VitalType.Health];
            Console.SetCursorPosition (_sideCol, _healthRow);
            int strLength = 0;
            string str = "";
            string blanks = "";
            writeStrInColor (Game.Util.getVitalName(v.type), v.textColor);
            Console.Write (" ");
            if (v.CurrentValue > v.normalMax || v.CurrentValue < v.normalMin) {
                writeStrInColor (v.CurrentValue.ToString (), ConsoleColor.Red);
            } else {
                writeStrInColor (v.CurrentValue.ToString (), ConsoleColor.Green);
            }
            str += v.unit;
            if(pat.deltaHealth == 0){
                str = " won't change ";
                Console.Write(str);
            }
            else if(pat.deltaHealth > 0){
                str = " will increase by ";
                Console.Write(str);
                writeStrInColor(pat.deltaHealth.ToString(), ConsoleColor.Green);
            }
            else if(pat.deltaHealth < 0){
                str = " will decrease by ";
                Console.Write(str);
                writeStrInColor(pat.deltaHealth.ToString(), ConsoleColor.Red);
            }

            strLength += Game.Util.getVitalName(v.type).Length + v.CurrentValue.ToString ().Length + pat.deltaHealth.ToString().Length + str.Length;

            blanks += new string (' ', (this._width - 4 - strLength));
            Console.WriteLine (blanks);
            //writeVital (pat.HealthPoint, this._sideCol, this._healthRow, false);
        }

        public void writeBloodTox (Patient pat) {
                writeVital (pat.MajorVitalMap[VitalType.BloodToxicity], this._sideCol, this._toxRow, false);
        }

        public void updateInventory (Card[] inventory) {
            int row = this._invRow;
            int slot = 1;
             Console.SetCursorPosition (this._sideCol, row);
            Console.WriteLine("Invetory");
            row++;
            foreach (Card c in inventory) {
                Console.SetCursorPosition (this._sideCol, row);
                if (c != null) {

                   writeFromatedString(slot + " : " + c.toStr());
                } else {
                   writeFromatedString(slot + " : ");
                }

                slot++;
                row++;
            }
            _currentRow = row;
             
        }

        public void writeScrollBuffer (String str) {


                Game.ScrollBuffer.Enqueue(str);
            
            if (Game.ScrollBuffer != null && Game.ScrollBuffer.Count > 0) {
                String[] array = new String[Game.ScrollBuffer.Count];

                array = Game.ScrollBuffer.ToArray();

                int itemIdex = array.Length - 1;

                for (int row = Console.WindowHeight - 2; row >= this._currentRow; row--) {

                    if (itemIdex >= 0) {
                        String line =  array[itemIdex]; 
                            Console.SetCursorPosition (this._sideCol, row);
                            writeFromatedString(" > " + line);
                    }
                    itemIdex--;
                }
            }
            Thread.Sleep (Game.Util.GLOBAL_SLEEP);
            
        }


        // [vital name]
         private void writeFromatedString(String line){

            int writtenLen = 0;
            char[] delimiterChars = { '[', ']'};

            string[] words = line.Split(delimiterChars);
            foreach(string s in words){
                if(Game.Util.isVitalString(s)){
                    writeStrInColor(s, Game.Util.getVitalColor(s));
                }
                else{
                    Console.Write(s);
                }
                writtenLen += s.Length;
            }

            writeBlanksNewLine(writtenLen, 2);

         }

        private void writeVital (Vital v, int col, int row, bool mainVit) {
            Console.SetCursorPosition (col, row);
            int strLength = 0;
            string str = "";
            string blanks = "";
            writeStrInColor (Game.Util.getVitalName(v.type), v.textColor);
            Console.Write(" ");
            if (v.CurrentValue > v.normalMax || v.CurrentValue < v.normalMin) {
                writeStrInColor (v.CurrentValue.ToString (), ConsoleColor.Red);
            } else {
                writeStrInColor (v.CurrentValue.ToString (), ConsoleColor.Green);
            }
            string minMax = v.unit + " (" + v.normalMin + "," + v.normalMax + ") ";
            Console.Write (minMax);

            Console.Write (v.turnChangeStatus);

            /*
            if (mainVit) {
                if (v.delta1 != 0) {
                    str += " changed by " + v.delta1;
                }
                if (v.CurrentValue > v.normalMax) {
                    str += " WARNING > Normal Limits of " + v.normalMax;
                }
                if (v.CurrentValue < v.normalMin) {
                    str += " WARNING < Normal Limit of " + v.normalMin;
                }
            }
            */

            strLength += 1 + Game.Util.getVitalName(v.type).Length + v.CurrentValue.ToString ().Length + str.Length + minMax.Length + v.turnChangeStatus.Length ;

            blanks += new string (' ', (this._width - 3 - strLength));
            Console.WriteLine (str + blanks);

        }

        private void writeVitalUpdate (Vital v, int col, int row) {
            Console.SetCursorPosition (col, row);
            writeStrInColor (Game.Util.getVitalName(v.type), v.textColor);
            string str = ": has changed by " + v.delta1;
            if (v.CurrentValue > v.normalMax) {
                str += " WARNING > Noramal Limits of " + v.normalMax;
            }
            if (v.CurrentValue < v.normalMin) {
                str += " WARNING < Noramal Limit of " + v.normalMin;
            }

            string blanks = new string (' ', (this._width - 3 - Game.Util.getVitalName(v.type).Length - str.Length));
            str += blanks;
            Console.WriteLine (str);

        }

        /*
        private void writeEffect(Effect e){
            int writtenLen = 0;
            writtenLen += writeVitalNameInColor(e.VitalType);

            string changeStr = "";
            if( e.ChangePerTurn.Peek() > 0){
                changeStr = " +" + e.ChangePerTurn.Peek();
            }
            else if(e.ChangePerTurn.Peek() < 0){
                changeStr = " -" + e.ChangePerTurn.Peek();
            }
            else{
                changeStr = " did not change";
            }
            Console.Write(changeStr);
            writtenLen += changeStr.Length;

            writeBlanksNewLine( writtenLen ,5 );

        }
*/

        private int writeVitalNameInColor(VitalType vt){

                int writtenLen = 0;
                ConsoleColor color = Game.Util.getVitalColor(vt);
                String str = Game.Util.getVitalName(vt);
                writtenLen += str.Length;

                writeStrInColor(str, color);
                return writtenLen;
        }
        
        private void writeStrInColor (string text, ConsoleColor color) {

            Console.ForegroundColor = color;
            Console.Write (text);

            Console.ResetColor ();
        }

        private void writeBorderLine () {
            Console.SetCursorPosition (0, this._currentRow);
            Console.WriteLine (new string (_borderChar, _width));
            this._currentRow++;
        }

        private void writeBlanksNewLine(int strLenSoFar, int offset){

            string blanks = new string (' ', (this._width - strLenSoFar - offset));

            Console.WriteLine(blanks);

        }
    }
}