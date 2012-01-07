using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace TeaseMe.Common
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [XmlRoot(ElementName = "Tease")]
    public class Tease
    {
        public const string CurrentScriptVersion = "v0.1";


        [XmlAttribute("scriptVersion")]
        public string ScriptVersion { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Url")]
        public string Url { get; set; }

        [XmlElement("Author")]
        public Author Author { get; set; }

        [XmlElement("MediaDirectory")]
        public string MediaDirectory { get; set; }

        [XmlArrayItem("Page")]
        public List<TeasePage> Pages { get; set; }


        [XmlIgnore]
        public string ScriptDirectory { get; set; }

        [XmlIgnore]
        public string FullMediaDirectoryPath
        {
            get
            {
                if (!String.IsNullOrEmpty(MediaDirectory))
                {
                    return MediaDirectory.StartsWith("http://") ? MediaDirectory : new DirectoryInfo(MediaDirectory).FullName;
                }
                if (!String.IsNullOrEmpty(ScriptDirectory))
                {
                    return new DirectoryInfo(ScriptDirectory).FullName;
                }
                return Environment.CurrentDirectory;
            }
        }


        [XmlIgnore]
        public List<string> Flags { get; set; }

        private TeasePage currentPage;

        [XmlIgnore]
        public TeasePage CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    OnCurrentPageChanged();
                }
            }
        }

        public event EventHandler<TeasePageEventArgs> CurrentPageChanged;

        protected void OnCurrentPageChanged()
        {
            if (CurrentPageChanged != null)
            {
                CurrentPageChanged(this, new TeasePageEventArgs { Page = currentPage });
            }
        }

        private readonly Random random = new Random(DateTime.Now.Millisecond);

        public Tease()
        {
            ScriptVersion = CurrentScriptVersion;
            Author = new Author();
            Pages = new List<TeasePage>();
            Flags = new List<string>();
        }

        public void Start()
        {
            Pages.ForEach(x => x.Tease = this);
            NavigateToPage("start");
        }


        public override string ToString()
        {
            return String.Format("Title:{0}", Title);
        }


        public void SetFlags(string flagNames)
        {
            var flags = flagNames.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var flagName in flags.Where(flagName => !Flags.Contains(flagName)))
            {
                Flags.Add(flagName);
            }
        }

        public void UnsetFlags(string flagNames)
        {
            var flags = flagNames.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var flagName in flags.Where(flagName => Flags.Contains(flagName)))
            {
                Flags.Remove(flagName);
            }
        }

        public void NavigateToPage(string pageId)
        {
            CurrentPage = Pages.Find(page => page.Id.Equals(pageId, StringComparison.InvariantCultureIgnoreCase));
        }


        public string GetFileName(TeaseMedia media)
        {
            if (media.Id.StartsWith("http://"))
            {
                return media.Id;
            }
            if (FullMediaDirectoryPath.StartsWith("http://"))
            {
                return FullMediaDirectoryPath + media.Id;
            }
            var matchingFiles = new DirectoryInfo(FullMediaDirectoryPath).GetFiles(media.Id);
            if (matchingFiles.Count() > 0)
            {
                return matchingFiles[random.Next(matchingFiles.Length)].FullName;
            }
            return null;
        }



        public int GetInteger(string text)
        {
            var match = Regex.Match(text, @"\((?<min>\d+)\.\.(?<max>\d+)\)");
            if (match.Success)
            {
                int min = Convert.ToInt32(match.Groups["min"].Value);
                int max = Convert.ToInt32(match.Groups["max"].Value);
                return random.Next(min, max + 1);
            }
            return Convert.ToInt32(text);
        }



        public void ExecuteTeaseAction(TeaseAction action)
        {
            // By default, the current page flag will be set when navigating away.
            SetFlags(currentPage.Id);

            if (!String.IsNullOrEmpty(currentPage.SetFlags))
            {
                SetFlags(currentPage.SetFlags);
            }
            if (!String.IsNullOrEmpty(currentPage.UnsetFlags))
            {
                UnsetFlags(currentPage.UnsetFlags);
            }

            if (action != null)
            {
                if (!String.IsNullOrEmpty(action.SetFlags))
                {
                    SetFlags(action.SetFlags);
                }
                if (!String.IsNullOrEmpty(action.UnsetFlags))
                {
                    UnsetFlags(action.UnsetFlags);
                }

                var target = GetTarget(action);

                NavigateToPage(target);
            }
        }

        private bool AllowedToShowPage(string pageId)
        {
            var page = Pages.FirstOrDefault(x => x.Id.Equals(pageId));
            if (page != null)
            {
                if (!String.IsNullOrEmpty(page.IfSetCondition))
                {
                    return Flags.Contains(page.IfSetCondition);
                }
                if (!String.IsNullOrEmpty(page.IfNotSetCondition))
                {
                    return !Flags.Contains(page.IfNotSetCondition);
                }
                return !Flags.Contains(page.Id);
            }
            return false;
        }

        private string GetTarget(TeaseAction action)
        {
            var possibilities = new List<string>();

            var match = Regex.Match(action.Target, @"(?<pre>.*)\((?<min>\d+)\.\.(?<max>\d+)\)(?<post>.*)");
            if (match.Success)
            {
                int min = Convert.ToInt32(match.Groups["min"].Value);
                int max = Convert.ToInt32(match.Groups["max"].Value);
                for (var i = min; i < max; i++)
                {
                    string pageId = String.Format("{0}{1}{2}", match.Groups["pre"].Value, i, match.Groups["post"].Value);
                    if (Pages.Exists(p => p.Id.Equals(pageId)) && AllowedToShowPage(pageId))
                    {
                        possibilities.Add(pageId);
                    }
                }
            }
            else
            {
                string pageId = action.Target;
                if (Pages.Exists(p => p.Id.Equals(pageId)) && AllowedToShowPage(pageId))
                {
                    possibilities.Add(pageId);
                }
            }
            if (possibilities.Count == 0)
            {
                throw new ArgumentException(String.Format("Page '{0}' does not exist or is already marked as seen.\n\nFor the author of this tease:\nUse the set/unset commands to to correct this situation.", action.Target));
            }
            return possibilities[random.Next(possibilities.Count)];
        }
    }
}