using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BruteForceAnything
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool passwordFound = false;

        private void button1_Click(object sender, EventArgs e)
        {

            passwordFound = false;

            string password = "";

            char[] possibleCharacters = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+=-~`{[}];'|\""?/>.<,".ToCharArray();

            string currentAttempt = "";

            while (!passwordFound)
            {
                currentAttempt = incrementString(currentAttempt, possibleCharacters);

                if (currentAttempt == password)
                {
                    passwordFound = true;
                }
                else
                {
                    passwordFound = false;
                    richTextBox1.Text += Environment.NewLine + "Attempting with password: " + currentAttempt;
                    System.Windows.Forms.Clipboard.SetText(currentAttempt);
                    SendKeys.Send("^{v}");
                    SendKeys.SendWait("{ENTER}");
                    SendKeys.Send("^{a}");
                    SendKeys.Send("{BACKSPACE}");

                }
            }
        }

        static string incrementString(string str, char[] possibleCharacters)
        {
            char[] characters = str.ToCharArray();

            bool incremented = false;

            for (int i = characters.Length - 1; i >= 0; i--)
            {
                if (characters[i] == possibleCharacters.Last())
                {
                    characters[i] = possibleCharacters.First();
                }
                else
                {
                    characters[i] = possibleCharacters[Array.IndexOf(possibleCharacters, characters[i]) + 1];

                    // Set the flag to true to stop the loop
                    incremented = true;
                    break;
                }
            }

            if (!incremented)
            {
                characters = (possibleCharacters.First() + new string(characters)).ToCharArray();
            }

            return new string(characters);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            passwordFound = true;
            MessageBox.Show("Bruteforce stopped.");
        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (richTextBox2.Text != string.Empty)
                {

                    passwordFound = true;
                    MessageBox.Show("Bruteforce works!");

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {

                if (passwordFound == true)
                {

                    MessageBox.Show("Bruteforce started.");
                    button1.PerformClick();

                }
                else
                {

                    passwordFound = true;
                    MessageBox.Show("Bruteforce stopped.");

                }

                e.SuppressKeyPress = true;
                e.Handled = true;

            }
        }
    }
}