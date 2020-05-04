using System;

namespace AarimousLD46 {
    public class Vital {
        public Vital (VitalType type, int startingValue, int min, int max, int normalMin, int normalMax, ConsoleColor forgroundColor, string unit) {
            this.type = type;
            this.min = min;
            this.max = max;
            this.normalMax = normalMax;
            this.normalMin = normalMin;
            this.textColor = forgroundColor;
            this.unit = unit;
            //set last for reasons
            setCurrentValue(startingValue);
            //this.currentValue = startingValue;
            turnChangeStatus = "";
        }

        public VitalType type { get; set; }
        public string turnChangeStatus {get;set;}
        public string unit {get;set;}
        public int CurrentValue {
            get{
                return currentValue;
            }
            set {
                if (value > max) {
                    currentValue = max;
                } else if (value < min) {
                    currentValue = min;
                } else {
                    currentValue = value;
                }
            }

        }

        private int currentValue;
        public int min { get; set; }
        public int max { get; set; }
        public int normalMax { get; set; }
        public int normalMin { get; set; }
        public ConsoleColor textColor { get; set; }
        public int delta1 { get; set; }

        private void setCurrentValue(int value){
        if (value > max) {
                currentValue = max;
            } else if (value < min) {
                currentValue = min;
            } else {
                currentValue = value;
            }
        }


        //todo change this based on a scripts
        //maybe allow users to play pre-scripted or random game
        public void update (int min, int max) {
            this.delta1 = 0;

            int delta = Game.Random.Next (min, max);

            currentValue += delta;


                if(delta > 0){
                    turnChangeStatus = " Up by " + delta;
                }
                else if (delta < 0){
                    turnChangeStatus = " Down by " + delta;
                }
                else{
                    turnChangeStatus = "";
                }
                
            }
        }

    }

