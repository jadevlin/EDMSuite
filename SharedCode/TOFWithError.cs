﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Data
{
    /// <summary>
    /// This class represents a time of flight curve with an associated error at every point.
    /// </summary>
    [Serializable]
    public class TOFWithError : TOF
    {
        [XmlArrayItem("e")]
        public double[] Errors;

        public TOFWithError() { }

        public TOFWithError(TOF t)
        {
            this.Data = t.Data;
            this.Errors = new double[Length];
            this.Calibration = t.Calibration;
            this.GateStartTime = t.GateStartTime;
            this.ClockPeriod = t.ClockPeriod;
        }

        // this method adds two TOFWithErrors together. It combines the errors using the
        // usual addition of errors formula.
        static public TOFWithError operator +(TOFWithError t1, TOFWithError t2)
        {
            if (t1.ClockPeriod == t2.ClockPeriod && t1.GateStartTime == t2.GateStartTime
               && t1.Length == t2.Length)
            {
                TOFWithError temp = new TOFWithError();
                temp.Calibration = t1.Calibration;
                temp.ClockPeriod = t1.ClockPeriod;
                temp.GateStartTime = t1.GateStartTime;

                temp.Data = new double[t1.Length];
                temp.Errors = new double[t1.Length];
                for (int i = 0; i < t1.Length; i++)
                {
                    temp.Data[i] = t1.Data[i] + t2.Data[i];
                    temp.Errors[i] = Math.Sqrt(Math.Pow(t1.Errors[i], 2) + Math.Pow(t2.Errors[i], 2));
                }
                return temp;
            }
            else return null;
        }

        // this gives the difference of two TOFWithErrors, respecting the errors.
        static public TOFWithError operator -(TOFWithError t1, TOFWithError t2)
        {
            TOFWithError temp = new TOFWithError();
            temp.Data = new double[t2.Data.Length];
            temp.Errors = new double[t2.Errors.Length];
            temp.GateStartTime = t1.GateStartTime;
            temp.ClockPeriod = t1.ClockPeriod;

            for (int i = 0; i < t2.Data.Length; i++)
            {
                temp.Data[i] = -t2.Data[i];
                temp.Errors[i] = t2.Errors[i];
            }
            return t1 + temp;
        }

    }
}