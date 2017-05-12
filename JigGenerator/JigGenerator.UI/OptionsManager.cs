using JigGenerator.UI.Options;
using System;
using System.IO;

namespace JigGenerator.UI
{
    internal class OptionsManager
    {
        private FileInfo defaultFile;

        public OptionsManager()
        {
            defaultFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "JigGenerator", "options.xml"));
        }
        internal void SaveOptions(JigOptions options)
        {
            SaveOptions(options, defaultFile);
        }

        internal void SaveOptions(JigOptions options, FileInfo file)
        {
            var dest = file.Directory;

            if (!dest.Exists)
                dest.Create();
            
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(JigOptions));

            using (var writer = new StreamWriter(Path.Combine(file.FullName), false))
            {
                serializer.Serialize(writer, options);
            }
        }

        internal JigOptions LoadOptions()
        {
            return LoadOptions(defaultFile);
        }

        internal JigOptions LoadOptions(FileInfo file)
        {
            if (!file.Exists)
                throw new InvalidOperationException($"Options file '{file.FullName}' does not exist");

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(JigOptions));

            using (var reader = new StreamReader(file.FullName))
            {
                var options = serializer.Deserialize(reader) as JigOptions;

                if (options == null)
                    throw new InvalidOperationException($"Problem reading from options file '{file.FullName}'");

                return options;
            }
        }
    }
}
