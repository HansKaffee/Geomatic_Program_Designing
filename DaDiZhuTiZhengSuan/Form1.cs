using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaDiZhuTiZhengSuan
{
    public partial class Form1 : Form
    {
        DataCenter dc = new DataCenter();
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FileCenter.OpenFile(ref dc, ref dataGridView1);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FileCenter.SaveFile(ref dc, ref dataGridView1, ref richTextBox1);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            double BianLv;
            BianLv = Algorithm.CalBianLv(dc.BianLvDaoShu);
            double b = Algorithm.Calb(dc.ChangBanZhou, BianLv);
            double e2 = Algorithm.Cale2(dc.ChangBanZhou, b);
            double ep2 = Algorithm.Calep2(e2);

            string report1;
            report1 = "1,椭球长半轴a," + dc.ChangBanZhou + "\n";
            report1 += "2,扁率倒数1/f," + dc.BianLvDaoShu + "\n";
            report1 += "3,扁率f," + Math.Round(BianLv,6) + "\n";
            report1 += "4,椭球短半轴b," + Math.Round(b,6) + "\n";
            report1 += "5,第一偏心率平方e2," + Math.Round(e2,6) + "\n";
            report1 += "6,第二偏心率平方e'2," + Math.Round(ep2,6) + "\n";


            string report;

            report = Algorithm.Caldadi(dc.Points[2].latitude, dc.Points[2].longitude, dc.Points[2].angleA1, dc.Points[2].lengthS, e2, ep2, dc.ChangBanZhou, b);
            richTextBox1.Text = "--------------------计算结果--------------------\n";
            richTextBox1.Text += report1;
            richTextBox1.Text += report;
        }
    }
}
