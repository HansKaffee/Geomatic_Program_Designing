using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace DaDiZhuTiZhengSuan
{
    class FileCenter
    {
        public static void OpenFile(ref DataCenter dc, ref DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开数据文件";
            ofd.Filter = "文本文件|*.txt";
            int line = 0;
            try
            {

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(ofd.FileName);
                    string[] strs;
                    strs = sr.ReadLine().Trim().Split(',');
                    dc.ChangBanZhou = double.Parse(strs[0]);
                    dc.BianLvDaoShu = double.Parse(strs[1]);
                    sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        dataGridView.Rows.Add();

                        strs = sr.ReadLine().Trim().Split(',');
                        Point point = new Point(strs[0], double.Parse(strs[1]), double.Parse(strs[2]), strs[3], double.Parse(strs[4]), double.Parse(strs[5]));
                        for (int i = 0; i < strs.Length; i++)
                        {
                            dataGridView.Rows[line].Cells[i].Value = strs[i];
                        }

                        dc.Points.Add(point);

                        line++;

                    }
                    MessageBox.Show("文件导入成功！");
                }
            }
            catch
            {
                MessageBox.Show("文件打开失败！");
            }
        }

        public static void SaveFile(ref DataCenter dc,ref DataGridView dataGridView,ref RichTextBox richTextBox)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存结果文件";
            sfd.FileName = "result.txt";
            sfd.Filter = "文本文件|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
            }
            MessageBox.Show("保存成功！");
        }
    }
}
