using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace ECard
{
    public partial class SelectDeviceFrom : Form
    {

        private CDDeviceType selectType;

        private int selectindex = 0;



        public CDDeviceType SelectDeviceType
        {
            get { return selectType; }
            set { selectType = value; }
        }

       
       
        public SelectDeviceFrom()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 初始化信息
        /// </summary>
        private void InitialInfo()
        {
            string[] devices=Enum.GetNames(typeof(CDDeviceType));
          
            ArrayList list = new ArrayList();

            foreach (string item in devices)
            {
                list.Add(item);
            }

            this.cbDeviceType.DataSource = list;

            this.cbDeviceType.Text = Enum.GetName(typeof(CDDeviceType), selectType);

        }

        private void SelectDeviceFrom_Load(object sender, EventArgs e)
        {
            InitialInfo();
        }

        private void btOK_Click(object sender, EventArgs e)
        {

            try
            {
                this.SelectDeviceType = (CDDeviceType)selectindex;
                this.DialogResult = DialogResult.Yes;

                this.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void cbDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectindex = this.cbDeviceType.SelectedIndex;

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
