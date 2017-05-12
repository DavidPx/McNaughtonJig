using JigGenerator.UI.Options;
using Svg;
using System;
using System.IO;
using System.Windows;

namespace JigGenerator.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileInfo destination;

        public MainWindow()
        {
            InitializeComponent();

            var defaultDrawingPath = Properties.Settings.Default["DefaultDrawingPath"] as string;

            if (string.IsNullOrWhiteSpace(defaultDrawingPath))
            {
                defaultDrawingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Jig.svg");
            }

            destination = new FileInfo(defaultDrawingPath);
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
            var options = GatherOptions();

            var optionsManager = new OptionsManager();
            optionsManager.SaveOptions(options);
        }

        private void SaveOptionsTo_Click(object sender, RoutedEventArgs e)
        {
            var defaultOptionsDirectory = Properties.Settings.Default["DefaultOptionsDirectory"] as string;

            if (string.IsNullOrWhiteSpace(defaultOptionsDirectory))
                defaultOptionsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var filePrompt = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = "xml",
                InitialDirectory = defaultOptionsDirectory,
                AddExtension = true
            };

            if (filePrompt.ShowDialog().GetValueOrDefault(true))
            {
                var options = GatherOptions();
                var optionsManager = new OptionsManager();
                var selectedFile = new FileInfo(filePrompt.FileName);
                optionsManager.SaveOptions(options, selectedFile);

                Properties.Settings.Default["DefaultOptionsDirectory"] = selectedFile.DirectoryName;
                Properties.Settings.Default.Save();
            }
            
        }

        private void LoadOptions_Click(object sender, RoutedEventArgs e)
        {

        }

        private JigOptions GatherOptions()
        {
            return new JigOptions();
        }

        private void LoadOptionsFrom_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
