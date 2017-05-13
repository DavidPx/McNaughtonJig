using JigGenerator.UI.Options;
using Svg;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

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
        
        private SvgDocument GenerateDocument()
        {
            // Load the options
            var options = GatherOptions();

            // Pass them to the drawing class which returns the SvgDocument
            var manager = new DrawingManager();

            return manager.CreateDocument(options);
        }

        private void SaveOptions_Click(object sender, RoutedEventArgs e)
        {
            var options = GatherOptions();

            var optionsManager = new OptionsManager();
            optionsManager.SaveOptions(options);
        }

        private void SaveOptionsTo_Click(object sender, RoutedEventArgs e)
        {

            var filePrompt = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = "xml",
                InitialDirectory = GetDefaultOptionsDirectory(),
                AddExtension = true
            };

            if (filePrompt.ShowDialog().GetValueOrDefault(false))
            {
                var options = GatherOptions();
                var optionsManager = new OptionsManager();
                var selectedFile = new FileInfo(filePrompt.FileName);
                optionsManager.SaveOptions(options, selectedFile);

                Properties.Settings.Default["DefaultOptionsDirectory"] = selectedFile.DirectoryName;
                Properties.Settings.Default.Save();
            }

        }

        private static string GetDefaultOptionsDirectory()
        {
            var defaultOptionsDirectory = Properties.Settings.Default["DefaultOptionsDirectory"] as string;

            if (string.IsNullOrWhiteSpace(defaultOptionsDirectory))
                defaultOptionsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return defaultOptionsDirectory;
        }

        private static string GetDefaultDrawingDirectory()
        {
            var defaultDrawingDirectory = Properties.Settings.Default["DefaultDrawingPath"] as string;

            if (string.IsNullOrWhiteSpace(defaultDrawingDirectory))
                defaultDrawingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return defaultDrawingDirectory;
        }

        private void LoadOptions_Click(object sender, RoutedEventArgs e)
        {
            var optionsManager = new OptionsManager();
            var options = optionsManager.LoadOptions();

            if (options == null) return;

            // TODO set all the fields using the options object
        }

        private JigOptions GatherOptions()
        {
            return new JigOptions();
        }

        private void UseOptions(JigOptions options)
        {

        }

        private void LoadOptionsFrom_Click(object sender, RoutedEventArgs e)
        {
            var filePrompt = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = "xml",
                CheckFileExists = true,
                InitialDirectory = GetDefaultOptionsDirectory(),
                Multiselect = false
            };

            if (filePrompt.ShowDialog().GetValueOrDefault(false))
            {
                var selectedFile = new FileInfo(filePrompt.FileName);

                var optionsManager = new OptionsManager();
                var options = optionsManager.LoadOptions(selectedFile);

                UseOptions(options);
            }
        }

        private void templateSelectButton_Click(object sender, RoutedEventArgs e)
        {
            var filePrompt = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = "svg",
                CheckFileExists = true,
                InitialDirectory = GetDefaultOptionsDirectory(),
                Multiselect = false
            };

            if (filePrompt.ShowDialog().GetValueOrDefault(false))
            {
                templateFilePath.Text = filePrompt.FileName;
            }
        }

        private void emptyFileSelectButton_Click(object sender, RoutedEventArgs e)
        {
            var filePrompt = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = "svg",
                InitialDirectory = GetDefaultDrawingDirectory(),
                AddExtension = true
            };

            if (filePrompt.ShowDialog().GetValueOrDefault(false))
            {
                emptyFilePath.Text = filePrompt.FileName;
            }
        }
        
        private void emptyFileGenerate_Click(object sender, RoutedEventArgs e)
        {
            // Prompt the user for the file if needed
            if (string.IsNullOrWhiteSpace(emptyFilePath.Text))
            {
                emptyFileSelectButton_Click(sender, e);

                // still not set? cancel
                if (string.IsNullOrWhiteSpace(emptyFilePath.Text)) return;
            }

            var doc = GenerateDocument();

            // Save it to a file
            File.WriteAllText(emptyFilePath.Text, doc.GetXML());
        }

        private void saveToTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            // Prompt and cancel if not set
            if (string.IsNullOrWhiteSpace(templateFilePath.Text))
            {
                templateSelectButton_Click(sender, e);

                if (string.IsNullOrWhiteSpace(templateFilePath.Text)) return;
            }

            if (string.IsNullOrWhiteSpace(templateLayerId.Text))
            {
                templateLayerId.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                return;
            }

            templateLayerId.BorderBrush = null;

            try
            {
                var templatedoc = SvgDocument.Open(templateFilePath.Text);

                var targetLayer = templatedoc.GetElementById<SvgGroup>(templateLayerId.Text);

                if (targetLayer != null)
                {
                    var doc = GenerateDocument();

                    foreach (var child in doc.Children)
                    {
                        targetLayer.Children.Add(child);
                    }

                    File.WriteAllText(templateFilePath.Text, templatedoc.GetXML());

                    statusText.Text = "File saved!";
                }
                else
                {
                    statusText.Text = $"Layer {templateLayerId.Text} not found in document";
                }


            }
            catch (Exception ex)
            {

                statusText.Text = ex.Message;
            }     

        }
    }
}
