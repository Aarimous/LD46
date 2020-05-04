using System;
using System.Collections.Generic;

namespace AarimousLD46 {
    public class Card {
        public Card (string name, List<Effect> effects) {
            this.Name = name;
            this.Effects = effects;
            //this.Effects.Add(new Effect(VitalType.HeartRate,new Queue<int>(new[] { 50 , 0, -50, -50 }),"this is a test"));
        }

        public string Name;
        public List<Effect> Effects;

        public String toStr () {

            String outstr = "";
            int i = 0;
            foreach (Effect e in Effects) {
                if (i > 0) {
                    outstr += " , ";
                }
                outstr += e.getEffectStr ();
                i ++;
            }

            return outstr;
        }

    }

}