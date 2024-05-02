using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for WordListView.xaml
    /// </summary>
    public partial class WordListView : Window
    {
        public WordListView()
        {
            InitializeComponent();

            PopulateList();
        }

        public void PopulateList()
        {
            wordListViewTextBlock.Text = string.Join("\r", WordList.shortList);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            wordListViewTextBlock.Text = "";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Do nothing ATM
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateList();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult results = MessageBox.Show("Are you sure you want to update the word list?", "Update word list", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);

            if (results == MessageBoxResult.Cancel || results == MessageBoxResult.No)
            {
                return;
            }
            else if (results == MessageBoxResult.OK || results == MessageBoxResult.Yes)
            {
                UpdateWordlist();
                MessageBox.Show("Wordlist has been updated!", "Succcess!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateWordlist()
        {
            string input = wordListViewTextBlock.Text;
            
            // Sanitise textbox to remove any extra whitespace elements before formatting (excluding \r as this is used for split method below)
            input = Regex.Replace(input, @"\t|\n", "");

            // Split input by \r character, then trim \r character from all entries
            List<string> x = input.Split('\r').Select(p => p.Trim('\r')).ToList();

            // Update reference shortlist
            WordList.shortList = x;

            // Update registry wordlist for retention
            RegHandler.WriteKey("Wordlist", RegHandler.WordlistHandler.ToRegistryValue(x));
        }
    }
}
