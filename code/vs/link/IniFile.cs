using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utilities
{
    public sealed class IniFile
    {
        private char commentChar = ';';

        private string filePath = string.Empty;
        private Dictionary<string, IniFileSection> sections = new Dictionary<string, IniFileSection>();


        /// <summary>
        /// Returns the path to the file including the file name. This value is read only.
        /// </summary>
        public string FilePath { get { return filePath; } }


        /// <summary>
        /// Returns the sections read from the initialization file. This value is read only.
        /// </summary>
        public Dictionary<string, IniFileSection> Sections { get { return sections; } }


        /// <summary>
        /// The comments associated with the section.
        /// Each element in the list represents a new comment line.
        /// </summary>
        public List<string> Comments = new List<string>();


        /// <summary>
        /// Instantiates a new instance of the IniFile class.
        /// </summary>
        /// <param name="filePath"> The path and file name of the initialization file. </param>
        public IniFile(string filePath)
        {
            // catch any errors that occur with the input file name
            if (string.IsNullOrEmpty(filePath))
            {
                throw new FileNotFoundException("The file path and name \"" + filePath + "\" is not valid.");
            }

            this.filePath = filePath;
        }



        /// <summary>
        /// Reads the entire initialization file in to memory.
        /// </summary>
        /// <returns> True if the read was successful and false otherwise. </returns>
        public bool Read()
        {
            try
            {
                using (StreamReader iniFileReader = new StreamReader(filePath))
                {
                    IniFileSection currentSection = null;

                    while (!iniFileReader.EndOfStream)
                    {
                        string currentLine = iniFileReader.ReadLine();
                        currentLine.TrimStart(); // remove leading white spaces

                        // if the current line has no size then move on to the next line
                        if (currentLine.Length <= 0)
                        {
                            continue;
                        }

                        // if comment line, and no section encountered yet then save as a file comment
                        if (currentSection == null && currentLine[0] == commentChar)
                        {
                            Comments.Add(currentLine.Substring(1).Trim());
                        }

                        // if there is a known section and the current line is a comment then
                        // add the comment to the section, but strip the comment character first
                        else if (currentLine[0] == commentChar)
                        {
                            currentSection.Comments.Add(currentLine.Substring(1).Trim());
                        }

                        // check if the current line is the start of a new section
                        else if (currentLine[0] == '[')
                        {
                            string sectionName = currentLine.Replace("[", "").Replace("]", "").Trim();  // get the section name by removing [ ]
                            currentSection = new IniFileSection(sectionName);                           // create the new section
                            sections.Add(currentSection);                                               // add the current section to the dictionary
                        }

                        // if this is not a comment or a section then it has to be an entry,
                        // but do a check to make sure there is a section to put it in
                        if (currentSection != null)
                        {
                            string[] lineSplit = currentLine.Split('=');

                            // make sure there are at least two parts,
                            // first part being the key and second the value
                            if (lineSplit.Length >= 2)
                            {
                                string key = lineSplit[0].Trim();
                                string[] valueSplit = lineSplit[1].Split(commentChar);

                                if (valueSplit.Length >= 1)
                                {
                                    string value = valueSplit[0]; // comment has to come after the value
                                    IniFileEntry newEntry = new IniFileEntry(key, value.Trim());

                                    // if there is a comment and it is not empty or null then save it
                                    if (valueSplit.Length > 1 && !string.IsNullOrEmpty(valueSplit[1].Trim()))
                                    {
                                        newEntry.Comment = valueSplit[1].Trim();
                                    }

                                    // add the new entry to the current section
                                    currentSection.Entries.Add(newEntry);
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // if any errors happened return false
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Exception in IniFile: " + ex.ToString());
#endif
                return false;
            }

            return true;
        }


        /// <summary>
        /// Writes the stored initialization values to the file passed in the constructor.
        /// </summary>
        /// <returns> True if the write was successful and false otherwise. </returns>
        public bool Write()
        {
            try
            {
                using (StreamWriter iniFileWriter = new StreamWriter(filePath))
                {
                    iniFileWriter.Write(ToString());
                }
            }
            catch
            {
                // if any errors happened return false
                return false;
            }

            return true;
        }


        /// <summary>
        /// Writes the initialization file to a string.
        /// </summary>
        /// <returns> The initialization file string as it appears in the file. </returns>
        public override string ToString()
        {
            string iniString = "";

            // write all file comments at the top of the file
            foreach (string comment in Comments)
            {
                iniString += commentChar + " " + comment + Environment.NewLine;
            }

            // add a new line if there were comments, if there
            // were no comments then just write the section at top
            if (Comments.Count > 0)
            {
                iniString += Environment.NewLine;
            }

            // write each section
            foreach (IniFileSection section in Sections.Values)
            {
                iniString += "[" + section.Name + "]" + Environment.NewLine; // write the section name

                // write the comments for the section right after the section header
                foreach (string comment in section.Comments)
                {
                    iniString += commentChar + " " + comment + Environment.NewLine;
                }
                iniString += Environment.NewLine; // put a new line separating the comments, or header, from the key-value pairs

                foreach (IniFileEntry entry in section.Entries.Values)
                {
                    iniString += entry.Key + " = " + entry.Value;

                    if (!string.IsNullOrEmpty(entry.Comment))
                    {
                        iniString += " " + commentChar + " " + entry.Comment;
                    }
                    iniString += Environment.NewLine;
                }

                iniString += Environment.NewLine + Environment.NewLine;
            }

            return iniString;
        }

    }


    /// <summary>
    /// The class representing a single section in an initialization file.
    /// </summary>
    public class IniFileSection
    {
        private string name = string.Empty;


        /// <summary>
        /// Returns the name of the sections. This value is read only.
        /// </summary>
        public string Name { get { return name; } }


        /// <summary>
        /// The list of entries for this section.
        /// </summary>
        /// <remarks>
        /// The entries are public to give full read and write access without the need for accessor functions
        /// </remarks>
        public Dictionary<string, IniFileEntry> Entries = new Dictionary<string, IniFileEntry>();


        /// <summary>
        /// The comments associated with the section.
        /// Each element in the list represents a new comment line.
        /// </summary>
        public List<string> Comments = new List<string>();

        
        /// <summary>
        /// Instantiates a new instance of IniFileSection.
        /// </summary>
        /// <param name="name"> The name to give to the section. </param>
        public IniFileSection(string name)
        {
            this.name = name;
        }
    }


    /// <summary>
    /// The class representing a single entry in an initialization file.
    /// </summary>
    public class IniFileEntry
    {
        private string key = string.Empty;
        private string value = string.Empty;


        /// <summary>
        /// Returns the key for the entry. This value is read only.
        /// </summary>
        public string Key { get { return key; } }


        /// <summary>
        /// Returns the value for the entry. This value is read only.
        /// </summary>
        public string Value { get { return value; } }


        /// <summary>
        /// The comment associated with the entry.
        /// </summary>
        public string Comment = string.Empty;

        
        /// <summary>
        /// Instantiates a new instance of IniFileEntry.
        /// </summary>
        /// <param name="key"> The key for the entry. </param>
        /// <param name="value"> The value of the entry. </param>
        public IniFileEntry(string key, string value)
        {
            this.key = key;
            this.value = value;
        }


        /// <summary>
        /// Returns the entry value as the given type
        /// </summary>
        /// <typeparam name="T"> The type to return the entry value as. </typeparam>
        /// <param name="defaultValue"> The default value to return if the entry value could not be converted to the specified type. </param>
        /// <returns> The entry value as the specified type, or default value in the event the entry value could not be converted. </returns>
        public T ValueAs<T>(T defaultValue = default(T))
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(string) || typeof(T) == typeof(String))
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            else
            {
                Type valueType = Type.GetType(value);
                if (valueType != null)
                {
                    return (T)Activator.CreateInstance(valueType);
                }
                else
                {
                    return defaultValue;
                }
            }
        }
    }


    public static class IniFileExtensions
    {
        /// <summary>
        /// Adds the IniFileSection to the Dictionary object containing sections.
        /// </summary>
        /// <param name="dictionary"> The Dictionary object that will add the section. </param>
        /// <param name="section"> The section to add to the Dictionary object containing all sections. </param>
        public static void Add(this Dictionary<string, IniFileSection> dictionary, IniFileSection section)
        {
            dictionary.Add(section.Name, section);
        }


        /// <summary>
        /// Adds the IniFileSection to the Dictionary object containing entries.
        /// </summary>
        /// <param name="dictionary"> The Dictionary object that will add the entry. </param>
        /// <param name="entry"> The entry to add to the Dictionary object containing all entries. </param>
        public static void Add(this Dictionary<string, IniFileEntry> dictionary, IniFileEntry entry)
        {
            dictionary.Add(entry.Key, entry);
        }
    }
}
