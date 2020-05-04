using System;
using System.Collections.Generic;

namespace AarimousLD46
{
    public class Effect
    {
        public Effect(VitalType vt, Queue<int> changePerTurn, string flavorText){
            this.VitalType = vt;
            this.ChangePerTurn = changePerTurn;
            this.FlavorText = flavorText;
        }

        public VitalType VitalType;
        public Queue<int> ChangePerTurn;
        //todo set max value?
        public String FlavorText;

        public String getEffectStr(){
            if(ChangePerTurn.Count == 0){
                return "";
            }

            string changeStr = "";
            if(ChangePerTurn.Peek() > 0){
                changeStr = "+" + ChangePerTurn.Peek();
            }
            else if(ChangePerTurn.Peek() < 0){
                changeStr = "" + ChangePerTurn.Peek();
            }
            else{
                changeStr = "won't change";
            }
            return "Vital Update : [" + Game.Util.getVitalName(VitalType) + "] " + changeStr;
        }

    }


}