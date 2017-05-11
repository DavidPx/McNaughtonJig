﻿using JigGenerator.UI.Options;
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
            var options = new RootOptions();

            // Pass them to the drawing class which returns the SvgDocument
            var manager = new DrawingManager();
            var doc = manager.CreateDocument(options);

            // Save it to a file
            File.WriteAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Jig.svg"), doc.GetXML());
        }
    }
}
