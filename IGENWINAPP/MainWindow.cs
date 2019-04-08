using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using IGENWINAPP.Models;
using System.Reflection;

namespace IGENWINAPP
{
    public partial class MainWindow : Form
    {
        BuisnessModel buisnessModel = new BuisnessModel();
        List<CustomMenuItem> customMenuItems = new List<CustomMenuItem>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;

            MenuStrip MnuStrip = new MenuStrip();
            ToolStripMenuItem MnuStripItem = new ToolStripMenuItem();

            this.Controls.Add(MnuStrip);

            customMenuItems = buisnessModel.customMenuItems(LoginInfo.userID);

            List<CustomMenuItem> MenuItem = customMenuItems.GroupBy(o => new { o.CategoryId}).Select(o => o.FirstOrDefault()).ToList();

            foreach (CustomMenuItem obj in MenuItem)
            {
                MnuStripItem = new ToolStripMenuItem(obj.CategoryName);

                SubMenu(MnuStripItem, obj.CategoryId);

                MnuStrip.Items.Add(MnuStripItem);
            }

            this.MainMenuStrip = MnuStrip;
        }

        public void SubMenu(ToolStripMenuItem mnu , int CategoryId )
        {
            List<CustomMenuItem> SubMenuItem = customMenuItems.Where(x => x.CategoryId == CategoryId).ToList();

            foreach(CustomMenuItem obj in SubMenuItem)
            {
                ToolStripMenuItem SSmneu = new ToolStripMenuItem(obj.ReportName, null,new EventHandler(child_Click));
                mnu.DropDownItems.Add(SSmneu);
            }
        }


        private void child_Click(object sender, EventArgs e)
        {
            List<CustomMenuItem> Contect = customMenuItems.Where(x => x.ReportName == sender.ToString()).ToList();

            string formName = sender.ToString().Replace(" ", string.Empty);

            Assembly frmAssembly = Assembly.LoadFile(Application.ExecutablePath);

            foreach (Type type in frmAssembly.GetTypes())

            {
                //MessageBox.Show(type.Name);

                if (type.BaseType == typeof(Form))

                {
                    if (type.Name == formName)

                    {
                        Form frmShow = (Form)frmAssembly.CreateInstance(type.ToString());

                        // then we  close all of the child Forms with  simple below code

                        foreach (Form form in this.MdiChildren)

                        {
                            form.Close();
                        }

                        frmShow.MdiParent = this;

                        frmShow.WindowState = FormWindowState.Maximized;
                        

                        frmShow.Show();
                    }
                }
            }
        }

        
    }
}
