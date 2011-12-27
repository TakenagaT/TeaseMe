using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TeaseMe
{
    public class TeaseLibrary
    {
        public const string SupportedScriptVersion = "v0.1";

        private readonly string applicationDirectory;

        public TeaseLibrary(string applicationDirectory)
        {
            this.applicationDirectory = applicationDirectory;
        }

        public string TeasesFolder
        {
            get { return Path.Combine(applicationDirectory, ConfigurationManager.AppSettings["TeasesFolder"]); }
        }


        public Tease EmptyTease()
        {
            var tease = new Tease
            {
                ScriptDirectory = applicationDirectory
            };
            tease.Pages.Add(new TeasePage
            {
                Id = "start",
                Text = "Welcome. Please open a tease.",
                Audio = new TeaseMedia { Id = ConfigurationManager.AppSettings["WelcomeAudio"] },
                Image = new TeaseMedia { Id = ConfigurationManager.AppSettings["WelcomeImage"] }
            });
            return tease;
        }


        public Tease LoadTease(string scriptFileName)
        {
            Tease result = null;
            string fileContents = File.ReadAllText(scriptFileName);
            if (fileContents.StartsWith("<?xml"))
            {
                var xmldoc = new ConfigXmlDocument();
                xmldoc.LoadXml(fileContents);

                if (xmldoc.DocumentElement != null)
                {
                    if (xmldoc.DocumentElement.LocalName == "Tease")
                    {
                        // Current file format.
                        if (xmldoc.DocumentElement.SelectSingleNode(String.Format("/Tease[@scriptVersion='{0}']", SupportedScriptVersion)) != null)
                        {
                            using (var reader = new StreamReader(scriptFileName))
                            {
                                result = new XmlSerializer(typeof(Tease)).Deserialize(reader) as Tease;
                            }
                        }
                    }
                    if (xmldoc.DocumentElement.LocalName == "pages")
                    {
                        // Previous file format.
                        var converter = new OldScriptConverter(fileContents);
                        if (converter.CanConvert())
                        {
                            result = converter.ConvertToTease();

                            var sourceFileName = new FileInfo(scriptFileName);
                            var destFileName = sourceFileName.Name.BeforeLast(sourceFileName.Extension) + "-v0_0_5" + sourceFileName.Extension;

                            string message = "The tease you selected is made for a previous version of this application. "
                                            + "Do you want to convert it to the current file format?\n"
                                            + "\n"
                                            + "Your old file will be renamed to " + destFileName;
                            if (DialogResult.Yes == MessageBox.Show(message, "Save tease in new file format?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                File.Move(scriptFileName, Path.Combine(sourceFileName.DirectoryName, destFileName));
                                SaveTease(result, sourceFileName.FullName);
                            }
                        }
                    }
                }
            }

            if (result != null)
            {
                result.ScriptDirectory = new FileInfo(scriptFileName).DirectoryName;
                Environment.CurrentDirectory = result.ScriptDirectory;
            }

            return result;
        }

        public void SaveTease(Tease tease, string scriptFileName)
        {
            File.WriteAllText(scriptFileName, new TeaseSerializer().ConvertToXmlString(tease));
        }
    }
}
