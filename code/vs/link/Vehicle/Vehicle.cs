using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utilities;

namespace OBD2.Vehicle
{
    public class Vehicle
    {
        /// <summary>
        /// The Vehicle Identification Number for the vehicle.
        /// </summary>
        public string VIN { get; protected set; }

        /// <summary>
        /// The manufacturer of the vehicle, as determined by the VIN.
        /// </summary>
        public string Manufacturer { get; protected set; }

        /// <summary>
        /// The year of the vehicle, as determined by the VIN.
        /// </summary>
        public int Year { get; protected set; }

        /// <summary>
        /// The model of the vehicle. This value may be supplied or left empty.
        /// </summary>
        public string Model { get; set; }

        private IniFile wmis;

        public Vehicle(string vin, IniFile wmis)
        {
            // set the private members first
            this.wmis = wmis;

            // this needs to be assigned first so other properties can be populated from it
            this.VIN = vin.ToUpper();

            // populate other properties that depend on the vin
            this.Manufacturer = GetManufacturer();
            this.Year = GetYear();
        }

        public override string ToString()
        {
            return
                "VIN: " + this.VIN                      + System.Environment.NewLine +
                "Year: " + this.Year.ToString()         + System.Environment.NewLine +
                "Make: " + this.Manufacturer                    + System.Environment.NewLine +
                "Model: " + this.Model ?? string.Empty  + System.Environment.NewLine;
        }

        /// <summary>
        /// Converts the 10th VIN digit into the manufacturing year.
        /// </summary>
        /// <returns> The year of manufacturer as denoted by the VIN. </returns>
        /// <remarks>
        /// This value starts at 1996 for valid 10th VIN digits.
        /// </remarks>
        private int GetYear()
        {
            if (!string.IsNullOrEmpty(this.VIN) &&
                this.VIN.Length >= 10)
            {
                switch (this.VIN[9])
                {
                    // OBD2 did not exist prior to 1996 so they could not be using an elm327 scanner to read the vin
                    case 'T': return 1996; //break;
                    case 'V': return 1997; //break;
                    case 'W': return 1998; //break;
                    case 'X': return 1999; //break;
                    case 'Y': return 2000; //break;
                    case '1': return 2001; //break;
                    case '2': return 2002; //break;
                    case '3': return 2003; //break;
                    case '4': return 2004; //break;
                    case '5': return 2005; //break;
                    case '6': return 2006; //break;
                    case '7': return 2007; //break;
                    case '8': return 2008; //break;
                    case '9': return 2009; //break;
                    case 'A': return 2010; //break;
                    case 'B': return 2011; //break;
                    case 'C': return 2012; //break;
                    case 'D': return 2013; //break;
                    case 'E': return 2014; //break;
                    case 'F': return 2015; //break;
                    case 'G': return 2016; //break;
                    case 'H': return 2017; //break;
                    case 'J': return 2018; //break;
                    case 'K': return 2019; //break;
                    default: return 0; //break;
                }
            }

            return 0;
        }

        /// <summary>
        /// Gets the manufacturer as denoted by the VIN.
        /// </summary>
        /// <returns> The manufacturer's long name. </returns>
        /// <remarks>
        /// This value is determined from the supplied IniFile <see cref="wmis"/> parameter from the constructor.
        /// </remarks>
        private string GetManufacturer()
        {
            if (!string.IsNullOrEmpty(this.VIN) &&
                this.VIN.Length >= 3)
            {
                string longwmi = this.VIN.Substring(0, 3);
                string shortwmi = this.VIN.Substring(0, 2);

                if (wmis.Sections["WMI"].Entries.ContainsKey(longwmi))
                {
                    return wmis.Sections["WMI"].Entries[longwmi].Value;
                }
                else if (wmis.Sections["WMI"].Entries.ContainsKey(shortwmi))
                {
                    return wmis.Sections["WMI"].Entries[shortwmi].Value;
                }
                else
                {
                    return "Unknown";
                }
            }

            return "Unknown (Invalid VIN)";
        }

    }
}
