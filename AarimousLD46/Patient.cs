using System;
using System.Collections.Generic;
namespace AarimousLD46 {
    public class Patient {
        public Patient (int startHealth,
            int startHeartRate,
            int startSystolic,
            int startDiastolic,
            int startTemp,
            int start02) {

            MinorVitalMap = new Dictionary<VitalType, Vital> ();
            MinorVitalMap.Add (VitalType.HeartRate, (new Vital (VitalType.HeartRate, startHeartRate, 0, 200, 60, 120, Game.Util.HEART_BEAT_COLOR, "BpM")));
            MinorVitalMap.Add (VitalType.SystolicBP, (new Vital (VitalType.SystolicBP, startSystolic, 0, 200, 90, 120, Game.Util.SYSTOLIC_COLOR, "")));
            MinorVitalMap.Add (VitalType.DiastolicBP, (new Vital (VitalType.DiastolicBP, startDiastolic, 0, 200, 60, 90, Game.Util.DIASTOLIC_COLOR, "")));
            MinorVitalMap.Add (VitalType.O2Level, (new Vital (VitalType.O2Level, start02, 0, 100, 90, 100, Game.Util.O2_COLOR, "%")));
            MinorVitalMap.Add (VitalType.Temperature, (new Vital (VitalType.Temperature, startTemp, 0, 200, 97, 99, Game.Util.TEMPURATURE_COLOR, " F")));

            MajorVitalMap = new Dictionary<VitalType, Vital> ();
            MajorVitalMap.Add (VitalType.Health, (new Vital (VitalType.Health,startHealth, 0, 100, 50, 100, Game.Util.HEALTH_POINTS_COLOR, "%")));
            MajorVitalMap.Add (VitalType.BloodToxicity, (new Vital (VitalType.BloodToxicity, 0, 0, 10, 0, 0, Game.Util.BLOOD_TOXICITY_COLOR, "%")));

            EffectsMap = new Dictionary<VitalType, List<Effect>> ();
            EffectsMap.Add (VitalType.BloodToxicity, new List<Effect> ());
            EffectsMap.Add (VitalType.DiastolicBP, new List<Effect> ());
            EffectsMap.Add (VitalType.Health, new List<Effect> ());
            EffectsMap.Add (VitalType.HeartRate, new List<Effect> ());
            EffectsMap.Add (VitalType.O2Level, new List<Effect> ());
            EffectsMap.Add (VitalType.SystolicBP, new List<Effect> ());
            EffectsMap.Add (VitalType.Temperature, new List<Effect> ());

            setDeltaHealth ();

        }

        //thesea are vitals which won't directly end the game
        public Dictionary<VitalType, Vital> MinorVitalMap { get; private set; }
        //these are the vitals which if they hit a limit will end the game
        public Dictionary<VitalType, Vital> MajorVitalMap { get; private set; }

        public int deltaHealth { get; set; }
        public Dictionary<VitalType, List<Effect>> EffectsMap { get; set; }

        public void update () {

            this.MajorVitalMap[VitalType.Health].CurrentValue += deltaHealth;
            updateVitals ();
            setDeltaHealth ();
            cleanUpEffects();
            
        }

        public void setDeltaHealth () {
            this.deltaHealth = 0;
            foreach (VitalType vt in this.MinorVitalMap.Keys) {
                Vital v = this.MinorVitalMap[vt];
                Vital health = this.MajorVitalMap[VitalType.Health];

                if (v.CurrentValue > v.normalMax || v.CurrentValue < v.normalMin) {
                    this.deltaHealth -= 1;
                } else {
                    this.deltaHealth += 1;
                }
                if(health.CurrentValue + deltaHealth > health.max){
                    deltaHealth = health.max - health.CurrentValue;
                }
            }

        }

        public void updateVitals () {

            foreach (VitalType vt in MinorVitalMap.Keys) {

                //apply all current effect to
                foreach (Effect e in EffectsMap[vt]) {
                    //should always be true, but we all make mistakes
                    if (e.VitalType == vt && e.ChangePerTurn.Count > 0) {
                        MinorVitalMap[vt].CurrentValue += e.ChangePerTurn.Dequeue ();
                    }
                }

                //gab the scripted amount for the minor vt
                //int valueChange = Game.scriptMap[vt][Game.currentTurn -1];
                
                //Do the normal updates (currently ranom, might be scripted in future)
                if(Game.currentTurn <= 8){
                    if(vt == VitalType.Temperature){
                        MinorVitalMap[vt].update(-1,1);
                    }
                    else{
                        MinorVitalMap[vt].update(-5,5);
                    }
                }
                else if (Game.currentTurn <= 12){
                                        if(vt == VitalType.Temperature){
                        MinorVitalMap[vt].update(-2,2);
                    }
                    else{
                        MinorVitalMap[vt].update(-10,10);
                    }
                }
                else{
                    if(vt == VitalType.Temperature){
                        MinorVitalMap[vt].update(-3,3);
                    }
                    else{
                        MinorVitalMap[vt].update(-20,20);
                    }
                }

                
            }
        }

        public void applyCard (Card card) {
            if (card != null) {
                foreach (Effect e in card.Effects) {
                    if (e.ChangePerTurn.Count > 0) {
                        applyVitalEffectNow (e.VitalType, (int) e.ChangePerTurn.Dequeue ());
                    }

                    EffectsMap[e.VitalType].Add (e);

                }
            }

        }

        public void applyVitalEffectNow (VitalType vt, int ammount) {

            foreach (VitalType minVt in this.MinorVitalMap.Keys) {
                if (vt == minVt) {
                    this.MinorVitalMap[vt].CurrentValue += ammount;
                    setDeltaHealth ();
                    return;
                }
            }

            foreach (VitalType majVt in this.MajorVitalMap.Keys) {
                if (vt == majVt) {
                    this.MajorVitalMap[vt].CurrentValue += ammount;
                    setDeltaHealth ();
                    return;
                }
            }
            //if we don't find the vitral in Minor/Major throw this soft error.
            throw new Exception ("Need to update the Enums map to add the new vital");
        }

    
        public void cleanUpEffects(){

             foreach (VitalType vt in MinorVitalMap.Keys) {
                
                List<Effect> effectsLst = new List<Effect>();

                foreach (Effect e in EffectsMap[vt]) {
                    if(e.ChangePerTurn.Count > 0){
                        effectsLst.Add(e);
                    }
                }
                EffectsMap[vt] = effectsLst;
             }


             foreach (VitalType vt in MajorVitalMap.Keys) {
                
                List<Effect> effectsLst = new List<Effect>();

                foreach (Effect e in EffectsMap[vt]) {
                    if(e.ChangePerTurn.Count > 0){
                        effectsLst.Add(e);
                    }
                }

                EffectsMap[vt] = effectsLst;
             }
        }
    }
}