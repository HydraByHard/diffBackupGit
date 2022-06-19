using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data.SqlClient;

namespace DifferentialBackup
{
    public partial class Form1 : Form
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)//метод, вызываемый таймером
        {
            int cnt = 0;
            string DiskName;
            DiskName = @"C:\";
            long Other = 0;
            List<long> LongsA = new List<long>();
            List<string> StringsA = new List<string>();

            DirectoryInfo dir_info = new DirectoryInfo(@"C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup");//путь до папки SQL Server с бэкапом
            List<FileInfo> file_list = new List<FileInfo>();
            file_list.AddRange(dir_info.GetFiles("*", SearchOption.AllDirectories));

            foreach (FileInfo FileA in file_list)
            {
                Other += FileA.Length;//суммарный объем файлов бэкапа
            }

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (DiskName == d.Name.ToString())
                {
                    LongsA.Add(d.TotalFreeSpace);
                    StringsA.Add("Свободное пространство");
                    LongsA.Add(d.TotalSize - d.TotalFreeSpace - Other);
                    StringsA.Add("Другие файлы");
                }
            }

            foreach (FileInfo FileA in file_list)//заполнение pie chart файлами бэкапа
            {
                if (FileA.Name.Contains("MyDB_Differntial"))
                    cnt++;
                Other += FileA.Length;
                LongsA.Add(FileA.Length);
                if (FileA.Name.Length > 23)
                    StringsA.Add(FileA.Name.Substring(5).Substring(0, FileA.Name.Substring(5).Length - 18));
                else
                    StringsA.Add(FileA.Name.Substring(5));
            }
            if (cnt == 0)//если нет разностных бэкапов, очистить список разностных бэкапов и обновить файл с ними
            {
                Data.pathListPART.Clear();
                Data.pathNamePART = "NULL";
                Data.saveData();
            }
            RoundChart.Series[0].Points.DataBindXY(StringsA, LongsA);
            RoundChart.Legends[0].Enabled = true;
            RoundChart.ChartAreas[0].Area3DStyle.Enable3D = true;
            RoundChart.Series[0].IsValueShownAsLabel = false;
            RoundChart.ChartAreas[0].BackColor = Color.Transparent;

            if (comboBox1.Items.Count != Data.pathListPART.Count)
            {
                comboBox1.Items.Clear();
                foreach (string str in Data.pathListPART)
                {
                    comboBox1.Items.Add(str.Substring(75));
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Data.updateData();
            Program.TimerEventProcessor(this, null);
            myTimer.Interval = 10000;//2 sec
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Start();
            TimerEventProcessor(this, null);
        }
        

        private void btnFBup_Click(object sender, EventArgs e)
        {
            Data.DoFullBackup(Data.databaseName);
            this.ActiveControl = null;
        }

        private void btnPBup_Click(object sender, EventArgs e)
        {
            Data.DoPartitionBackup(Data.databaseName);
            this.ActiveControl = null;
        }

        private void btnLOG_Click(object sender, EventArgs e)
        {
            Data.DoLOG(Data.databaseName);
            this.ActiveControl = null;
        }
        private void brnRe_Click(object sender, EventArgs e)
        {
            try
            {
                Data.DoRecover(Data.databaseName);
            }
            catch (System.Data.SqlClient.SqlException text)
            {
                if (text.Message.Contains(@"C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\MyDB_log.trn"))
                    MessageBox.Show("Нет журнала транзакций", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    if (text.Message.Contains("BACKUP LOG WITH NORECOVERY"))
                    MessageBox.Show("База данных уже существует.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.ActiveControl = null;
        }


        public void button1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//пользователь выбирает разностный бэкап
        {
            Object selectedItem = comboBox1.SelectedItem;
            Data.pathNamePART = selectedItem.ToString();
            foreach (string str in Data.pathListPART)
            {
                if (str.EndsWith(Data.pathNamePART))
                    Data.pathNamePART = str;
            }
            this.ActiveControl = null;
        }

        private void brnRst_Click(object sender, EventArgs e)
        {
            if (Data.pathNamePART != "NULL")
            {
                Data.DoRollback(Data.databaseName);
                this.ActiveControl = null;
            }
            else
            {
                MessageBox.Show("Разностная копия не выбрана", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = null;
            }
        }
    }
}
