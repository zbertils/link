using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Diagnostics;

using OBD2.Cables;

namespace Extensions
{
    public static class Extensions
    {

        /// <summary>
        /// Determines if a value is exclusively between two other values.
        /// </summary>
        /// <typeparam name="T"> The type being compared. </typeparam>
        /// <param name="actual"> The actual value to determine if it is between two numbers. </param>
        /// <param name="lower"> The lower bound to compare <paramref name="actual"/> to. </param>
        /// <param name="upper"> The upper bound to compare <paramref name="actual"/> to. </param>
        /// <returns> True if <paramref name="actual"/> is between <paramref name="lower"/> and <paramref name="upper"/>. </returns>
        public static bool BetweenExclusive<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) > 0 && actual.CompareTo(upper) < 0;
        }


        /// <summary>
        /// Determines if a value is inclusively between two other values.
        /// </summary>
        /// <typeparam name="T"> The type being compared. </typeparam>
        /// <param name="actual"> The actual value to determine if it is between two numbers. </param>
        /// <param name="lower"> The lower bound to compare <paramref name="actual"/> to. </param>
        /// <param name="upper"> The upper bound to compare <paramref name="actual"/> to. </param>
        /// <returns> True if <paramref name="actual"/> is between or at <paramref name="lower"/> and <paramref name="upper"/>. </returns>
        public static bool BetweenInclusive<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
        }


        /// <summary>
        /// Converts a DateTime to its exact date and time including milliseconds.
        /// </summary>
        /// <param name="dateTime"> The date time to convert to an exact date and time. </param>
        /// <returns> The string containing the exact date and time including milliseconds. </returns>
        public static string ToExactDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToShortDateString() + " " +
                dateTime.Hour.ToString() + ":" +
                dateTime.Minute.ToString() + ":" +
                dateTime.Second.ToString() + "." +
                dateTime.Millisecond.ToString("000");
        }


        ///// <summary>
        ///// Automatically detects if a cable with the given device description is connected.
        ///// </summary>
        ///// <param name="serialPort"> The serial port instance to use when checking for a connection. </param>
        ///// <param name="deviceDescription"> The device description shown in Device Manager for the device. </param>
        ///// <returns> The COM port assigned to the device if found and null otherwise. </returns>
        //public static string AutoDetectCable(this Cable serialPort, string deviceDescription)
        //{
        //    using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_SerialPort"))
        //    {
        //        using (var collection = searcher.Get())
        //        {
        //            foreach (ManagementObject device in collection)
        //            {
        //                try
        //                {
        //                    string description = device["Description"].ToString();
        //                    string hardwareIds = device["DeviceID"].ToString();
        //                    if (description == deviceDescription)
        //                    {
        //                        return device["DeviceID"].ToString();
        //                    }
        //                }
        //                catch { }
        //            }
        //        }
        //    }

        //    return null;
        //}


        /// <summary>
        /// Replaces macros with their expanded values.
        /// </summary>
        /// <param name="originalString"> The original string containing macros. </param>
        /// <param name="macros"> The dictionary of macros (keys) and their expanded values. </param>
        /// <returns> The string with expanded macros that are contained in <paramref name="macros"/>. </returns>
        public static string InsertMacros(this string originalString, Dictionary<string, string> macros)
        {
            string finalString = originalString;
            string[] keys = macros.Keys.ToArray();

            // go through each key and replace the macro key with the macro value
            foreach (string key in keys)
            {
                finalString = finalString.Replace(key, macros[key]);
            }

            return finalString;
        }
    }
}
