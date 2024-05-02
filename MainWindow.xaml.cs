using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace EzPass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WordList.InitializeWordlist();

            //foreach (var entry in WordList.shortList)
            //    Console.WriteLine(entry);

            // Request new password on program startup
            RequestNewPassword();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Request new password when "Generate" button is clicked
            RequestNewPassword();
        }

        public void RequestNewPassword()
        {
            passwordOutputBox.Text = PasswordGenerator.New(2, (bool)numbersCheckbox.IsChecked, (bool)replaceCheckbox.IsChecked);

            UpdatePhoneticTextBox();

            /*
            string output = string.Empty;
            Random random = new Random();
            int rand = random.Next(1, 999999999);
            Debug.WriteLine(rand);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.dinopass.com/password/strong?_=1{rand}");
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                output = reader.ReadToEnd();
            }

            passwordOutputBox.Text = output;
            */
        }

        private void UpdatePhoneticTextBox()
        {
            phoneticTextBox.Text = ConvertoToPhonetic(passwordOutputBox.Text);
        }

        private string ConvertoToPhonetic(string input)
        {
            StringBuilder x = new StringBuilder();

            // Loop through each letter in the string and convert to phonetic outputs inside previously created string builder (x)
            foreach (char character in input)
            {
                if (char.IsLetter(character))
                {
                    // Single liner to do the above (currently without extra new line between two words)
                    _ = char.IsUpper(character) ? x.AppendLine(WordList.phoneticAlphabet[char.ToLower(character)].ToUpper()) : x.AppendLine(WordList.phoneticAlphabet[char.ToLower(character)]);
                }
                else
                {
                    // Append character if not a letter
                    x.AppendLine(character.ToString());
                }
            }

            return x.ToString().TrimEnd();
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(passwordOutputBox.Text);
            //MessageBox.Show($"Password was coppied to clipboard", "Copy Successful", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void passwordOutputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePhoneticTextBox();
        }

        private void bulkGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            BulkList x = new BulkList();
            x.Show();
        }

        private void OpenWordlist_Click(Object sender, RoutedEventArgs e)
        {
            WordListView x = new WordListView();
            x.Show();
        }

        public class NoteData
        {
            [JsonProperty("error")]
            public string error;

            [JsonProperty("link")]
            public string link;
        }

        private async void CreateSecurenoteButtonClick(object sender, RoutedEventArgs e)
        {
            // Wait for encoding to finish
            string text = HttpUtility.HtmlEncode(passwordOutputBox.Text);

            // Start submitting data async while continuing UI operations
            Task<string> req = Request.PostAsync("https://notes.evolvetech.com.au/create", text, 7, "0.0.0.0/0");

            // Await response from request previously mentioned
            var response = await req;

            if (response == null || response == string.Empty)   // Do nothing if the try statement for the post request returned null or an error
            {
                // The erroring for this will already be handled in Requests.cs
                RaiseError(string.Empty);
                return;
            }

            NoteData data;

            // Attempt to deserialize response
            try
            {
                data = JsonConvert.DeserializeObject<NoteData>(response);
            }
            catch (Exception ex)
            {
                RaiseError($"There was an error deseralizing the response. \n\n {ex.Message}");
                return;
            }

            // Check error object to see if something went wrong
            if (data.error != null)
            {
                RaiseError(data.error);
                return;
            }

            // Copy the link automatically to the clipboard
            Clipboard.SetText(data.link);

            MessageBox.Show($"SecureNotes link has been saved to your clipboard", "SecureNote Created Successfully", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
        }

        private void RaiseError(string text)
        {
            if (text != string.Empty)
                MessageBox.Show($"{text}", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        private void replaceCheckbox_Click(object sender, RoutedEventArgs e)
        {
            int indexPoint = passwordOutputBox.Text.IndexOf('.');
            string extra = "";
            string currentPass = passwordOutputBox.Text;
            if (indexPoint > 0)
            {
                extra = currentPass.Substring(indexPoint, passwordOutputBox.Text.Length - indexPoint);
                currentPass = passwordOutputBox.Text.Substring(0, indexPoint);
            }

            bool normal = true;

            foreach (char ch in currentPass)
            {
                if (WordList.replacementChars.ContainsValue(ch))
                {
                    normal = false;
                    break;
                }
            }

            if (normal)
                PasswordGenerator.ReplaceCharacters(ref currentPass);
            else
                PasswordGenerator.InvertReplaceCharacters(ref currentPass);

            passwordOutputBox.Text = currentPass + extra;

        }
    }
}
