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

namespace Assign4_HangmanGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //current word
        string currentWord; //current word. Needs to read of a file.
        int linenumber;

        int mistakes; //number of wrong guessed
        int tries;
        char[] GuessedLetters; //create a array for the guessed letters
        string letter;
        char[] wordArray; //create a array for the current word.
        String wrongGues;
        string file;

        bool check;

        private static List<string> WordList = new List<string>();


        public MainWindow()
        {

            InitializeComponent();
            txtWord.Visibility = Visibility.Visible;
            btnClean.Visibility = Visibility.Visible;
            btnGuess.Visibility = Visibility.Visible;
            btnStart.Visibility = Visibility.Visible;
            lbGuessTitle.Visibility = Visibility.Visible;
            lblTryDesc.Visibility = Visibility.Visible;
            lblTryInt.Visibility = Visibility.Visible;
            lblWrongTries.Visibility = Visibility.Visible;
            lbWord.Visibility = Visibility.Visible;
            lbWrongGuessed.Visibility = Visibility.Visible;
            lbWrongTitle.Visibility = Visibility.Visible;
            lbText1.Visibility = Visibility.Visible;
            lbTitle.Visibility = Visibility.Visible;

            linenumber = 1;

            CleanFields();

            file = DirectoryExist();
            if (check)
            {
                ReadFromTextFile(file);
            }
        }
        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            //word array is equal current word

            if (currentWord.ToUpper().Contains(letter.ToUpper()) )
            {
                for (int i = 0; i < wordArray.Length; i++)
                {
                    char x = wordArray[i];

                    if (letter.ToUpper() == x.ToString().ToUpper())
                    {
                        GuessedLetters[i] = x;
                    }



                }
                lbWord.Content = new string(GuessedLetters).ToUpper();

                string compare1 = new string(wordArray);
                string compare2 = new string(GuessedLetters);

                if ((compare1.CompareTo(compare2)) == 0)
                {
                    MessageBox.Show("You win. Your guessed word is " + compare2 + ". Press Start to start again!");

                    CleanFields();
                }
            }
            else {
                mistakes++;
                tries--;

                lblWrongTries.Content = mistakes;
                lblTryInt.Content = tries;

                wrongGues = wrongGues + "" + letter;

                lbWrongGuessed.Content = wrongGues.ToUpper();

                    if (mistakes >= 3) { 
                        MessageBox.Show("Game Over!");
                        MessageBox.Show("The correct word is " + currentWord);
                        MessageBox.Show("Press Start to start again!");
                    
                         CleanFields();
                    } 
                }
            txtWord.Clear();

        }
        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to clean the word?",
            "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CleanFields();
            }
            else
            {
                // Do not close the window  
            }


        }

        private void CleanFields()
        {
            lblTryInt.Content = "";
            lbWord.Content = "";
            lblWrongTries.Content = "";
            txtWord.Clear();
            lblSizeWord.Content = "";
            lbWrongGuessed.Content = "";

            btnClean.IsEnabled = false;
            btnGuess.IsEnabled = false;
            txtWord.IsEnabled = false;
            btnStart.IsEnabled = true;

            mistakes = 0;
            tries = 0;
            letter = "";
            wrongGues = "";
        }

        private void TextWord_LostFocus(object sender, RoutedEventArgs e)
        {
            string myTempString = new string(GuessedLetters);

            int n;

            if (!((txtWord.Text.Length == 1) && (txtWord.Text != null) && (!int.TryParse(txtWord.Text, out n))))
            {
                MessageBox.Show("Invalid Value!");
                txtWord.Clear();
                letter = "";
            }
            else if (myTempString.Contains(txtWord.Text))
            { 
                 MessageBox.Show("Letter has already been informed. Try another letter!");
                 txtWord.Clear();
                 letter = "";
             }
            else
            {
                letter = txtWord.Text;
            }

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            linenumber++;

            if (check)
            {
                //Pick up random word
                Random rndWord = new Random();
                int num = rndWord.Next(0, WordList.Count);
                currentWord = WordList[num].ToUpper();

                wordArray = new Char[currentWord.Length];
                GuessedLetters = new Char[currentWord.Length];

                CleanFields();

                mistakes = 0;
                tries = 3;
                lblTryInt.Content = tries;
                lblSizeWord.Content = wordArray.Length;

                wordArray = currentWord.ToCharArray();

               for (int i=0; i < wordArray.Length; i++)
                {
                    GuessedLetters[i] = '*';
                }

                GuessedLetters[0] = wordArray[0]; 

                lbWord.Content = new string(GuessedLetters);

                btnClean.IsEnabled = true;
                btnGuess.IsEnabled = true;
                txtWord.IsEnabled = true;

                btnStart.IsEnabled = false;
            }
            else
            {
                CleanFields();
            }

        }
        private String DirectoryExist()
        {
            check = true;

            string curFile = @".\data\strings.txt";

            if (!File.Exists(curFile))
            {
                check = false;
                MessageBox.Show(curFile + " does not exist");
            }

            return curFile;
        }
        private void ReadFromTextFile(string fileName)
        {

       // for (int i = 0; i < linenumber; i++) 
       // {
                FileStream fs = null;
                try
                {
                    fs = File.Open(fileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    //string aLine = sr.ReadLine();
                    //string[] arrLine = aLine.Split(' ');

                        while (!sr.EndOfStream)
                        {
                            WordList.Add(sr.ReadLine());
                        }
                                             
                 sr.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        //}
        }


    }

    

}