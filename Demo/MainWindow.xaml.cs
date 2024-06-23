using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Net1702_221_ASM2_SE161142_NGUYENMINHTRIET;

namespace Demo
{
    public partial class MainWindow : Window
    {
        private string _filePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Json_Click(object sender, RoutedEventArgs e)
        {
            ToggleFormat.Content = "Json/Xml";
        }

        private void Xml_Click(object sender, RoutedEventArgs e)
        {
            ToggleFormat.Content = "Xml";
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|JSON files (*.json)|*.json"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                _filePath = openFileDialog.FileName;
                LoadFileData();
            }
        }

        private void LoadFileData()
        {
            if (string.IsNullOrEmpty(_filePath)) return;

            string fileContent = File.ReadAllText(_filePath);
            TextBoxOriginal.Text = fileContent;

            // Update toggle button based on file extension
            if (_filePath.EndsWith(".xml"))
            {
                ToggleFormat.IsChecked = false; // Ensure XML mode
                ToggleFormat.Content = "Xml";
            }
            else if (_filePath.EndsWith(".json"))
            {
                ToggleFormat.IsChecked = true; // Ensure JSON mode
                ToggleFormat.Content = "Json";
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_filePath)) return;

            string fileContent = File.ReadAllText(_filePath);
            if (ToggleFormat.IsChecked == true)
            {
                // Deserialize from JSON
                var deserializedData = JsonConvert.DeserializeObject<List<Schedule>>(fileContent);
                DataGridDeserialized.ItemsSource = deserializedData;
            }
            else
            {
                // Deserialize from XML
                var serializer = new XmlSerializer(typeof(List<Schedule>));
                using (var reader = new StringReader(fileContent))
                {
                    var deserializedData = (List<Schedule>)serializer.Deserialize(reader);
                    DataGridDeserialized.ItemsSource = deserializedData;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridDeserialized.ItemsSource == null) return;

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|JSON files (*.json)|*.json"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var data = DataGridDeserialized.ItemsSource as List<Schedule>;
                string serializedData = string.Empty;

                if (saveFileDialog.FileName.EndsWith(".xml"))
                {
                    // Serialize to XML
                    var serializer = new XmlSerializer(typeof(List<Schedule>));
                    using (var writer = new StringWriter())
                    {
                        serializer.Serialize(writer, data);
                        serializedData = writer.ToString();
                    }
                }
                else if (saveFileDialog.FileName.EndsWith(".json"))
                {
                    // Serialize to JSON
                    serializedData = JsonConvert.SerializeObject(data, Formatting.Indented);
                }

                File.WriteAllText(saveFileDialog.FileName, serializedData);
            }
        }
    }
}
