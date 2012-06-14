using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TeaseMe.Common
{
    public class TeaseLibrary
    {
        public const string SupportedScriptVersion = "v0.1";

        private readonly string applicationDirectory;

        public TeaseLibrary(string applicationDirectory)
        {
            this.applicationDirectory = applicationDirectory;
            if (!Directory.Exists(TeasesFolder))
            {
                Directory.CreateDirectory(TeasesFolder);
            }
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
            var welcomePage = new TeasePage();
            welcomePage.Id = "start";
            welcomePage.Text = "Welcome. Please open a tease.";
            welcomePage.AudioList.Add(new TeaseMedia { Id = ConfigurationManager.AppSettings["WelcomeAudio"] });
            welcomePage.ImageList.Add(new TeaseMedia { Id = ConfigurationManager.AppSettings["WelcomeImage"] });
            tease.Pages.Add(welcomePage);
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
