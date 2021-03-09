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
                aes = new AES(KeyInput.Text);
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
                aes = new AES(KeyInput.Text);
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
    }
}
