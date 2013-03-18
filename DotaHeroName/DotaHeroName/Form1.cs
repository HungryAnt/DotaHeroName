using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotaHeroName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataProcesser _dp = new DataProcesser();

        private void Form1_Load(object sender, EventArgs e)
        {
            _dp = new DataProcesser();
            _dp.CreateInvertedIndex();
            textBox1.Text = _dp.DataIndicsLibrary.ToString();
        }

        private void textBoxYourName_TextChanged(object sender, EventArgs e)
        {
            listBoxAvailableHeroNames.Items.Clear();
            List<string> availableNames = _dp.FindSimilarResult(textBoxYourName.Text);

            if (availableNames == null
                || availableNames.Count == 0)
            {
                listBoxAvailableHeroNames.Items.Add("无匹配项");
            }
            else
            {
                listBoxAvailableHeroNames.Items.AddRange(availableNames.ToArray());
            }
        }
    }
}
