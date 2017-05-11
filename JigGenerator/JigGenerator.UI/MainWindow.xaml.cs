using JigGenerator.UI.Options;
using Svg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JigGenerator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateDrawing(object sender, RoutedEventArgs e)
        {
            // Load the options
            var options = GatherOptions();

            // Pass them to the drawing class which returns the SvgDocument
            var manager = new DrawingManager();
            var doc = manager.CreateDocument(options);

            // Save it to a file
            File.WriteAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Jig.svg"), doc.GetXML());
        }

        private void SaveOptions_Click(object sender, RoutedEventArgs e)
        {
            var dest = new DirectoryInfo(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "JigGenerator"));

            if (!dest.Exists)
                dest.Create();

            // Load all the controls into the options object
            var options = GatherOptions();

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(RootOptions));

            using (var writer = new StreamWriter(System.IO.Path.Combine(dest.FullName, "options.xml"), false))
            {
                serializer.Serialize(writer, options);
            }
            
        }

        private void SaveOptionsTo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadOptions_Click(object sender, RoutedEventArgs e)
        {

        }

        private RootOptions GatherOptions()
        {
            return new RootOptions();
        }
    }
}
