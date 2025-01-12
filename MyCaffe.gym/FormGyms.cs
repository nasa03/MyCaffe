﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCaffe.gym
{
    /// <summary>
    /// The FormGyms dialog displays the available gyms.
    /// </summary>
    public partial class FormGyms : Form
    {
        GymCollection m_col;
        IXMyCaffeGym m_selectedGym;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="col">Specifies the GymCollection of gyms to display.</param>
        public FormGyms(GymCollection col = null)
        {
            if (col == null)
            {
                col = new GymCollection();
                col.Load();
            }

            m_col = col;
            InitializeComponent();
        }

        /// <summary>
        /// Returns the selected Gym.
        /// </summary>
        public IXMyCaffeGym SelectedGym
        {
            get { return m_selectedGym; }
        }

        private void FormGyms_Load(object sender, EventArgs e)
        {
            foreach (IXMyCaffeGym igym in m_col)
            {
                ListViewItem lvi = new ListViewItem(igym.Name);
                lvi.Tag = igym;

                lstItems.Items.Add(lvi);
            }
        }

        private void timerUI_Tick(object sender, EventArgs e)
        {
            if (lstItems.SelectedItems.Count == 0)
                btnOpen.Enabled = false;
            else
                btnOpen.Enabled = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            m_selectedGym = lstItems.SelectedItems[0].Tag as IXMyCaffeGym;
        }

        private void lstItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hti = lstItems.HitTest(e.Location);
            if (hti == null)
                return;

            m_selectedGym = hti.Item.Tag as IXMyCaffeGym;
            DialogResult = DialogResult.OK;
        }
    }
}
