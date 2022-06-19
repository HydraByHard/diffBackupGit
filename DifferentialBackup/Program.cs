using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data.SqlClient;

namespace DifferentialBackup
{
    
    class Data
    {
        public static List<string> pathListPART = new List<string>();
        public static string databaseName = "volkovaa";
        public static string pathNamePART = "NULL";
        public static int Counter = 0;
        static string connectionString = @"Data Source=DESKTOP-TMI7NDV\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
        public static void updateData()//метод, загружающий список разностных бэкапов после перезапуска программы
        {
            DirectoryInfo dir_info = new DirectoryInfo(@"C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup");
            List<FileInfo> file_list = new List<FileInfo>();
            file_list.AddRange(dir_info.GetFiles("*", SearchOption.AllDirectories));
            int cnt=0;
            foreach (FileInfo FileA in file_list)
            {
                if (FileA.Name.Contains("MyDB_Differntial"))
                    cnt++;
            }
            if(cnt!=0)
            {
                string line;
                try
                {
                    StreamReader sr = new StreamReader("save.txt");
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        pathListPART.Add(line);
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Ошибка загрузки данных" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static void saveData()//метод, сохраняющий список разностных копий в папке бэкапа
        {
            try
            {
                FileStream fs = File.Create("save.txt");
                fs.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка создания хранилища данных" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                StreamWriter sw = new StreamWriter("save.txt");
                foreach (string str in pathListPART)
                    sw.WriteLine(str);
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка сохранения данных" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void clearDir(string str)//очистка директории
        {
            DirectoryInfo dirInfo = new DirectoryInfo(str);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }
        public static void DoFullBackup(string name)//метод вызова процедуры полного бэкапа
        {
            clearDir(@"C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup");
            Data.pathListPART.Clear();
            Data.pathNamePART = "NULL";
            saveData();
            if (Data.Counter != 0)
                Data.Counter = 1;
            string sqlExpression = "CopyFULL";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = name
                };
                command.Parameters.Add(nameParam);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void DoPartitionBackup(string name)//метод вызова процедуры разностного бэкапа
        {
            if (pathListPART.Count != 0)//проверка, не превышает ли разностная копия половину объема полной копии. Если превышает, создается полная копия
            {
                FileInfo fileFULL = new FileInfo(@"C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\MyDB_Full.bak");
                float sizeFULL = fileFULL.Length;
                FileInfo filePART = new FileInfo(pathListPART.Last());
                float sizePART = filePART.Length;
                if (sizePART / sizeFULL >= 0.5)
                    DoFullBackup(databaseName);
            }
            string sqlExpression = "CopyDIF";
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            pathListPART.Add(@"C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\MyDB_Differntial "+ dateTime.ToString().Replace(':', '.') +".bak");//в названии разностного бэкапа указывается время создания
            saveData();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter{ParameterName = "@name",Value = name};
                command.Parameters.Add(nameParam);
                SqlParameter pathParam = new SqlParameter { ParameterName = "@path", Value = pathListPART.Last() };
                command.Parameters.Add(pathParam);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void DoLOG(string name)//метод вызова процедуры копирования журналов транзакций
        {
            string sqlExpression = "CopyLOG";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = name
                };
                command.Parameters.Add(nameParam);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void DoRecover(string name)//метод вызова процедуры восстановления БД
        {
            string sqlExpression = "Recover";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter { ParameterName = "@name", Value = name };
                command.Parameters.Add(nameParam);
                SqlParameter pathParam = new SqlParameter { ParameterName = "@path", Value = pathListPART.Last() };
                command.Parameters.Add(pathParam);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void DoRollback(string name)//метод вызова процедуры отката БД к разностной копии
        {
            string sqlExpression = "RollbackP";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter { ParameterName = "@name", Value = name };
                command.Parameters.Add(nameParam);
                SqlParameter pathParam = new SqlParameter { ParameterName = "@path", Value = pathNamePART };
                command.Parameters.Add(pathParam);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
    static class Program
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        public static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)//метод, вызываемый таймером
        {
            if (Data.Counter == 0)//каждые 7 дней полный бэкап
            {
                Data.pathListPART.Clear();
                Data.saveData();
                Data.clearDir(@"C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup");
                Data.DoFullBackup(Data.databaseName);
            }
            if ((Data.Counter == 0) || (Data.Counter % 24 == 0))//каждые 24 часа разностный бэкап
                Data.DoPartitionBackup(Data.databaseName);
            if ((Data.Counter == 0) || (Data.Counter % 1 == 0))//каждый час журнал транзакций
                Data.DoLOG(Data.databaseName);
            Data.Counter++;
            if (Data.Counter >= 168)
                Data.Counter = 0;
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            myTimer.Interval = 3600000;//1 hrs
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Start();
            Application.Run(new Form1());
        }

    }
}
