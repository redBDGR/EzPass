using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EzPass
{
    /// <summary>
    /// Interaction logic for BulkList.xaml
    /// </summary>
    public partial class BulkList : Window
    {

        /// <summary>
        /// Automatically populates the BulkGenerate windows text box with new password data (10)
        /// </summary>
        public void GenerateNewList()
        {
            string displayText = "";
            Random number = new Random();

            for (int i = 0; i < 10; i++)
            {
                displayText += $"{PasswordGenerator.New(2, true, false, number.Next(10, 1000))}\n";
            }

            bulkGenerateTextBox.Text = displayText;
        }

        public BulkList()
        {
            InitializeComponent();

            // Init window with a fresh password list
            GenerateNewList();
        }

        /// <summary>
        /// Populate the BulkGenerate windows text box with new password data (10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bulkGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateNewList();
        }
    }
}
