using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace CSE105FinalProject
{
    // kullanıcı adını ve parolanın daha önce girilip girilmediğini kontrol etmede kaldık.
    public partial class MainWindow : Window
    {
        
        
       
        public MainWindow()
        {
            InitializeComponent();
            string foldername1 = "logs";
            if (!Directory.Exists(foldername1))
            {
                Directory.CreateDirectory(foldername1);
                
            }
        }
       
        

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            string inputUN = regUN.Text; //girilen kullanıcı adını aldım.
            inputUN = inputUN.Trim();//boşluklardan kurtulduk.
            inputUN = inputUN.ToLower();//küçük harfe çevirdik.
            //karakterleri ingilizceye çevirme:
            inputUN = publicFunctions.trToEn(inputUN);
            //izin verilmeyen karakterler
            List<string> lstNotAllowedChars = new List<string> { "%", "-", "$", "/", "\\", "(", ")", "[", "]", ",", "'","´","`","!","?","=","&","\"", ";" };
            for (int k = 0; k < lstNotAllowedChars.Count; k++)
            {
                if (inputUN.Contains(lstNotAllowedChars[k]))
                {
                    MessageBox.Show($"Your username contains an invalid character: ({lstNotAllowedChars[k]}) \n Please enter try another username.");
                    return;
                    
                }
            }
            

            if (inputUN.Length < 3)
            {
                MessageBox.Show("Your username must contain at least three characters.");
                return;
            }

            List<char> lstInvChar = new List<char>();
            foreach (var vr in System.IO.Path.GetInvalidFileNameChars()) //dosya oluşturmamızı engelleyen karakterleri bir listeye aldım.
            {
                lstInvChar.Add(vr);

            }
            bool fileNIC = false;
            string invalidChar="";

            foreach (var vr in System.IO.Path.GetInvalidFileNameChars())
            {
                if (inputUN.IndexOf(vr) != -1)
                {
                    fileNIC = true;
                    invalidChar = vr.ToString();
                    break;
                }
                
            }
            if (fileNIC)
            {
                MessageBox.Show($"Username contains an invalid character: ({invalidChar}) \n Please select another username.");
                return;
            }

            if (publicFunctions.checkUsername(inputUN))
            {
                MessageBox.Show("This username is already taken, please try another one.");
                return;
            }

            if (!publicFunctions.doesntStartWithANumber(inputUN))
            {
                MessageBox.Show("Username cannot start with a number.");
                return;
            }

            if (regPW1.Password.ToString() != regPW2.Password.ToString())
            {
                MessageBox.Show("Warning! Your passwords do not match!"); //parolaların eşleşip eşleşmediğini kontrol ettim.
                return;
            }
            string inputPW = regPW1.Password.ToString(); //girilen parolayı aldım.
            if (inputPW.Contains(":"))
            {
                MessageBox.Show("Your password cannot contain this(:) symbol. Please try another password.");
                return;
            }
            bool pwSecur = false; //parola güvenlik kontrolü
            if (inputPW.Length >= 8 && inputPW.Any(char.IsDigit) && inputPW.Any(char.IsUpper) && inputPW.Any(char.IsLower))
            {
                pwSecur = true;

            }
            if (pwSecur == false)
            {
                MessageBox.Show("Your password must be longer than 8 characters, it must contain a lowercase letter, an uppercase letter, and a number.");
                return;
            }
            
            string hashedPW = publicFunctions.ComputeSha256Hash(inputPW);
            File.AppendAllText("users.txt", inputUN + publicFunctions.crSeperator + hashedPW+"\r\n"); // seperator hala burada DİKKAT!
        }

        private void logButton_Click(object sender, RoutedEventArgs e)
        {
            bool girisbasari = false;
            lgUser.Text = lgUser.Text.ToLower().Trim();
            lgUser.Text = publicFunctions.trToEn(lgUser.Text);

            if (!publicFunctions.checkPw(lgUser.Text,lgPw.Password))
            {
                MessageBox.Show("Wrong username or password.");
                return;
            }
            else
            {
                MessageBox.Show("Welcome " + lgUser.Text + "! You can start using the calculator now!");
                Calculator.IsEnabled = true;
                girisbasari = true;
                
            }
            publicDegisken.loggedUserName = lgUser.Text;
            
            loggedUserLabel.Content = "Welcome, " + publicDegisken.loggedUserName +".";

            if (girisbasari)
            {
                File.AppendAllText($"logs\\{publicDegisken.loggedUserName}log.txt", "");
                Calculator.IsSelected = true;
                LogReg.IsEnabled = false;
                List<string> terseDoneceklst = new List<string>();
                foreach (var item in File.ReadAllLines($"logs\\{publicDegisken.loggedUserName}log.txt"))
                {
                    
                    terseDoneceklst.Add(item);
                    
                }
                foreach (var item in terseDoneceklst)
                {
                    loggedOpsLB.Items.Insert(0,item);
                }
            }

            
        }
        
        private void btncClick(object sender, RoutedEventArgs e)
        {
            operationText.Text = "";
        }

        private void btn0Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + "0";
            
        }

        private void btnNoktaClick(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + ".";
        }

        private void btnCommaClick(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + ",";
        }

        private void btnSpaceClick(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
           
            operationText.Text = operationText.Text + " ";
        }

        private void btnEsitClick(object sender, RoutedEventArgs e)
        {
            double dblResult;
            string lastoperation;
            
           
            if (!publicFunctions.operasyonKullanildi(operationText.Text) && operationText.Text=="")
            {
                MessageBox.Show("You did not write an operation!");
            }
            else if(!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                MessageBox.Show("You did not write an operation!");
            }
            else
            {
                try
                {
                    dblResult = Convert.ToDouble(new DataTable().Compute(operationText.Text, null));
                }
                catch (Exception E)
                {
                    MessageBox.Show("Please enter a valid operation.");
                    return;
                }
                var vrText = operationText.Text + " = " + dblResult.ToString("N2");
                File.AppendAllText($"logs\\{publicDegisken.loggedUserName}log.txt", vrText + "\r\n"); //history için dosya actık

                resultLstBox.Items.Insert(0, vrText);
                
                lastoperation = operationText.Text;
                operationText.Text = "";
            }
        }

        private void btnParantezAcClick(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + "(";
        }

        private void btnParantezKapaClick(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + ")";
        }

        private void btnDelClick(object sender, RoutedEventArgs e)
        {
            operationText.Text = operationText.Text.Remove(operationText.Text.Length - 1, 1);
        }

        private void btnArtiClick(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + "+";
        }

        private void btn7Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + "7";
        }

        private void btn8Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
           
            operationText.Text = operationText.Text + "8";
        }

        private void btn9Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
           
            operationText.Text = operationText.Text + "9";
        }

        private void btnEksiClick(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
           
            operationText.Text = operationText.Text + "-";
        }

        private void btn4Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + "4";
        }

        private void btn5Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + "5";
        }

        private void btn6Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + "6";
        }

        private void btnCarpimClick(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
           
            operationText.Text = operationText.Text + "*";
        }

        private void btn1Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
           
            operationText.Text = operationText.Text + "1";
        }

        private void btn2Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
           
            operationText.Text = operationText.Text + "2";
        }

        private void btn3Click(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
           
            operationText.Text = operationText.Text + "3";
        }

        private void btnBolumClick(object sender, RoutedEventArgs e)
        {
            if (!publicFunctions.operasyonKullanildi(operationText.Text))
            {
                operationText.Text = "";
            }
            
            operationText.Text = operationText.Text + "/";
        }

        private void OperationText_GotFocus(object sender, RoutedEventArgs e)
        {
            if(operationText.Text=="Please enter your operation here...")
            {
                operationText.Text = "";
            }
            
        }

        private void OperationText_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                double dblResult;
                string lastoperation;


                if (!publicFunctions.operasyonKullanildi(operationText.Text) && operationText.Text == "")
                {
                    MessageBox.Show("You did not write an operation!");
                }
                else if (!publicFunctions.operasyonKullanildi(operationText.Text))
                {
                    MessageBox.Show("You did not write an operation!");
                }
                else
                {
                    try
                    {
                        dblResult = Convert.ToDouble(new DataTable().Compute(operationText.Text, null));
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("Please enter a valid operation.");
                        return;
                    }
                    var vrText = operationText.Text + " = " + dblResult.ToString("N2");
                    File.AppendAllText($"logs\\{publicDegisken.loggedUserName}log.txt", vrText + "\r\n"); //history için dosya actık

                    resultLstBox.Items.Insert(0, vrText);

                    lastoperation = operationText.Text;
                    operationText.Text = "";
                }
            }
        }

        private void Btnlgout_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

       
    }
}
