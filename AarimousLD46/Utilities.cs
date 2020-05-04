using System;
using System.Collections.Generic;

namespace AarimousLD46 {
    public class Utilities {
        public Utilities () {

        }

        public int GLOBAL_SLEEP = 500;

        //Radom trun texts
        public string TREAT_PHASE_TEXT = "Treat Patient Phase Started";
        public string GET_MORE_TREAT_TEXT = "Add More Treatments to Invetory";
        public string USE_A_TREATMENT = "Use a Treatment From Your Inventory";
        public string AVALABLE_TREATMENTS = "Here Are the Available Treatments";
        public string ADD_A_TREATMENT = "Add a Treatmnet by Clicking the Corisponding Number Key";
        public string INVETRORY_IS_EMPTY = "Inentory Is Empty, Moving To Next Phase";
        public string END_TURN_TEXT = "Press Any Key to End End Turn..";

        //UI Texts
        public const string BLOOD_TOXICITY_NAME = "Blood Toxicity";
        public ConsoleColor BLOOD_TOXICITY_COLOR = ConsoleColor.DarkGreen;
        public const string HEALTH_POINTS_NAME = "Current Health";
        public ConsoleColor HEALTH_POINTS_COLOR = ConsoleColor.Red;
        public string VITALS_TEXT = "Minor Vitals: Normal Ranges (MIN,MAX)";

        //Test effects
        public Effect TEST_EFFECT_HEARTRATE_NONE = new Effect (VitalType.HeartRate,
            new Queue<int> (new [] { 0, 0, 0, 0 }),
            "This card does nothing to HB");

        //Heart Rate
        public const string HEART_BEAT_NAME = "Heart Beat";
        public ConsoleColor HEART_BEAT_COLOR = ConsoleColor.Blue;
        public Effect INCREASE_NOW_HEARTRATE_BASIC = new  Effect (VitalType.HeartRate,
            new Queue<int> (new [] { 5, 0, 0, 0 }),
            "Increased the Heartrate by 5");
        public Effect DECREASE_NOW_HEARTRATE_BASIC = new Effect (VitalType.HeartRate,
            new Queue<int> (new [] {-5, 0, 0, 0 }),
            "Decreased the Heartrate by 5");

        //Tempurature
        public const string TEMPURATURE_NAME = "Tempurature";
        public ConsoleColor TEMPURATURE_COLOR = ConsoleColor.DarkYellow;
        public Effect INCREASE_NOW_TEMP_BASIC = new Effect (VitalType.Temperature,
            new Queue<int> (new [] { 1, 0, 0, 0 }),
            "Increased the Tempurature by 1");
        public Effect DECREASE_NOW_TEMP_BASIC = new Effect (VitalType.Temperature,
            new Queue<int> (new [] {-1, 0, 0, 0 }),
            "Decreased the Tempurature by 1");

        //Systilic BP
        public const string SYSTOLIC_NAME = "Systolic";
        public ConsoleColor SYSTOLIC_COLOR = ConsoleColor.Cyan;
        public Effect INCREASE_NOW_SYSTOLIC_BASIC = new Effect (VitalType.SystolicBP,
            new Queue<int> (new [] { 5, 0, 0, 0 }),
            "Increase the Systolic BP by 5");
        public Effect DECREASE_NOW_SYSTOLIC_BASIC = new Effect (VitalType.SystolicBP,
            new Queue<int> (new [] {-5, 0, 0, 0 }),
            "Decrease the Systolic BP by 5");

        //Diastolic BP
        public const string DIASTOLIC_NAME = "Diastolic";
        public ConsoleColor DIASTOLIC_COLOR = ConsoleColor.DarkCyan;
        public Effect INCREASE_NOW_DIASTOLIC_BASIC = new Effect (VitalType.DiastolicBP,
            new Queue<int> (new [] { 5, 0, 0, 0 }),
            "Increase the Diastolic BP by 5");
        public Effect DECREASE_NOW_DIASTOLIC_BASIC = new Effect (VitalType.DiastolicBP,
            new Queue<int> (new [] {-5, 0, 0, 0 }),
            "Decrease the Diastolic BP by 5");

        //BP
        public const string BLOOD_PRESSURE = "Blood Pressure";

        //02
        public const string O2_NAME = "O2 Level";
        public ConsoleColor O2_COLOR = ConsoleColor.Magenta;
        public Effect INCREASE_NOW_O2_BASIC = new Effect (VitalType.O2Level,
            new Queue<int> (new [] { 3, 0, 0, 0 }),
            "Vital Update: [" + O2_NAME +"]");
        public Effect DECREASE_NOW_O2_BASIC = new Effect (VitalType.O2Level,
            new Queue<int> (new [] {-3, 0, 0, 0 }),
            "Vital Update: [" + O2_NAME +"]");

        //Useful methods

        public String getVitalName (VitalType vt) {
            switch (vt) {
                case VitalType.Health:
                    return HEALTH_POINTS_NAME;
                case VitalType.BloodToxicity:
                    return BLOOD_TOXICITY_NAME;
                case VitalType.DiastolicBP:
                    return DIASTOLIC_NAME;
                case VitalType.SystolicBP:
                    return SYSTOLIC_NAME;
                case VitalType.O2Level:
                    return O2_NAME;
                case VitalType.Temperature:
                    return TEMPURATURE_NAME;
                case VitalType.HeartRate:
                    return HEART_BEAT_NAME;
                default:
                    return "Update Util.getVitalName to have new vitals ;)";
            }
        }

        public ConsoleColor getVitalColor (VitalType vt) {
            switch (vt) {
                case VitalType.Health:
                    return HEALTH_POINTS_COLOR;
                case VitalType.BloodToxicity:
                    return BLOOD_TOXICITY_COLOR;
                case VitalType.DiastolicBP:
                    return DIASTOLIC_COLOR;
                case VitalType.SystolicBP:
                    return SYSTOLIC_COLOR;
                case VitalType.O2Level:
                    return O2_COLOR;
                case VitalType.Temperature:
                    return TEMPURATURE_COLOR;
                case VitalType.HeartRate:
                    return HEART_BEAT_COLOR;
                default:
                    throw new Exception ("No vital found in Utilities.getVitalColor(VitalType vt)");
            }
        }

        public ConsoleColor getVitalColor (String str) {
            switch (str) {
                case HEALTH_POINTS_NAME:
                    return HEALTH_POINTS_COLOR;
                case BLOOD_TOXICITY_NAME:
                    return BLOOD_TOXICITY_COLOR;
                case DIASTOLIC_NAME:
                    return DIASTOLIC_COLOR;
                case SYSTOLIC_NAME:
                    return SYSTOLIC_COLOR;
                case O2_NAME:
                    return O2_COLOR;
                case TEMPURATURE_NAME:
                    return TEMPURATURE_COLOR;
                case HEART_BEAT_NAME:
                    return HEART_BEAT_COLOR;
                default:
                    throw new Exception ("No vital found in Utilities.getVitalColor(String str)");
            }
        }

        public Boolean isVitalString (String str) {
            switch (str) {
                case HEALTH_POINTS_NAME:
                case BLOOD_TOXICITY_NAME:
                case DIASTOLIC_NAME:
                case SYSTOLIC_NAME:
                case O2_NAME:
                case TEMPURATURE_NAME:
                case HEART_BEAT_NAME:
                    return true;
                default:
                    return false;
            }
        }

    }
}