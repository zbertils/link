using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using Utilities;

namespace OBD2
{
    public class ParameterIdentificationCollection : List<ParameterIdentification>
    {
        /// <summary>
        /// Loads the Parameter Identification entries from the given file.
        /// </summary>
        /// <param name="filePathAndName"> The fully qualified file path and name. </param>
        /// <returns> The list of loaded ParameterIdentification objects. </returns>
        public void LoadPIDs(string filePathAndName)
        {
            if (!System.IO.File.Exists(filePathAndName))
            {
                Diagnostics.DiagnosticLogger.Log("Could not find given pids file " + filePathAndName);
            }

            // load the pids in from the file
            List<ParameterIdentification> pids = new List<ParameterIdentification>();
            using (StreamReader streamReader = new StreamReader(filePathAndName))
            {
                DataContractSerializer xmlSerializer = new DataContractSerializer(typeof(List<ParameterIdentification>));
                try
                {
                    pids = xmlSerializer.ReadObject(streamReader.BaseStream) as List<ParameterIdentification>;
                }
                catch (Exception e)
                {
                    Diagnostics.DiagnosticLogger.Log("Could not read objects", e);
                }
            }

            if (pids != null)
            {
                this.AddRange(pids);
            }
        }

        /// <summary>
        /// Loads the Parameter Identification entries from the given file.
        /// </summary>
        /// <param name="filePathAndName"> The fully qualified file path and name. </param>
        /// <returns> The list of loaded ParameterIdentification objects. </returns>
        public static List<IniFileEntry> LoadDtcDescriptions(string filePathAndName)
        {
            if (!System.IO.File.Exists(filePathAndName))
            {
                Diagnostics.DiagnosticLogger.Log("Could not find given dtc file " + filePathAndName);
                return new List<IniFileEntry>();
            }

            List<IniFileEntry> descriptions = new List<IniFileEntry>();
            Utilities.IniFile file = new Utilities.IniFile(filePathAndName);
            file.Read();

            // load all trouble code descriptions, this will get
            // all codes regardless of what type they belong to
            for (int sectionIndex = 0; sectionIndex < file.Sections.Count; sectionIndex++)
			{
                IniFileSection section = file.Sections.ElementAt(sectionIndex).Value;
                for (int entryIndex = 0; entryIndex < section.Entries.Count; entryIndex++)
                {
                    descriptions.Add(section.Entries.ElementAt(entryIndex).Value);                    
                }
            }

            return descriptions;
        }

    }
}
