using DMSSWE.CLOUD.SECURITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using IGENWINAPP.Models;
using DMSSWE;


namespace IGENWINAPP
{
    public partial class Login : Form
    {
        private SecurityClient oSecurityClient = new SecurityClient();
        BuisnessModel ObuisnessModel = new BuisnessModel();
        List<Security202008> security202008s = new List<Security202008>();

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtuserName.Text.Trim().ToLower();
                string password = txtPassword.Text.Trim();

                

                if(validateUser(username, password) == true)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid User");
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validateUser(string username, string password)
        {
            bool result =false;

            security202008s = ObuisnessModel.SelectUsers();

            Security202008 user = new Security202008();

            string pass = DMSSWE.CryptoUtil.Encrypt(password, username);

            if( ObuisnessModel.checkUserExists(username, pass) == true)
            {
                user = security202008s.Where(x => x.column85.Equals(username) && x.column88.Equals(pass)).FirstOrDefault();
            }

            if (user.column85 != null)
            {
                LoginInfo.userID = user.column85;
                result = true;
            }
                

            return result;
        }
        
       
    }
}
