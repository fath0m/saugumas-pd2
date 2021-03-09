using Microsoft.Win32;
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

namespace AesWpf
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

        private bool IsFormFilledIn()
        {
            return !String.IsNullOrEmpty(TextInput.Text) && !String.IsNullOrEmpty(KeyInput.Text);
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFormFilledIn())
            {
                MessageBox.Show("Please fill out the input text and key", "Error occured");
                return;
            }

            AES aes;

            try
            {
                aes = new AES(KeyInput.Text, (bool) CBCRadioButton.IsChecked ? "CBC" : "ECB");
            } catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error occured");
                return;
            }

            var encrypted = "";

            try
            {
                encrypted = aes.Encrypt(TextInput.Text);
            } catch
            {
                MessageBox.Show("Unable to encrypt the message", "Error occured");
            }
                     
            OutputInput.Text = encrypted;
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFormFilledIn())
            {
                MessageBox.Show("Please fill out the input text and key", "Error occured");
                return;
            }

            AES aes;

            try
            {
                aes = new AES(KeyInput.Text, (bool)CBCRadioButton.IsChecked ? "CBC" : "ECB");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error occured");
                return;
            }

            var decrypted = "";

            try
            {
                decrypted = aes.Decrypt(TextInput.Text);
            } catch
            {
                MessageBox.Show("Unable to decrypt the message", "Error occured");
            }
 
            OutputInput.Text = decrypted;
        }

        private void LoadInputButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*",
                Title = "Select a text file"
            };

            if (dialog.ShowDialog() == true)
            {
                var fileName = dialog.FileName;
                TextInput.Text = File.ReadAllText(fileName);

                MessageBox.Show("File was successfuly loaded", "Success");
            }
        }

        private void SaveOutputButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(OutputInput.Text))
            {
                MessageBox.Show("There is nothing to save", "Error occured");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, OutputInput.Text);
                MessageBox.Show("File was successfuly saved", "Success");
            }
        }
    }
}
