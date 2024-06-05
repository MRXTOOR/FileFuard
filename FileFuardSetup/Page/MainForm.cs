using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using FileFuardSetup.Page;

namespace FileFuardSetup
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FunctionForm functionForm = new FunctionForm();
            this.Hide(); 
            functionForm.ShowDialog();
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            AboutTheProgram aboutTheProgram = new AboutTheProgram();
            aboutTheProgram.ShowDialog();
            this.Hide();
        }
     
   

        private async void button3_Click(object sender, EventArgs e)
        {
            using (var progressForm = new ProgressForm())
            {
                progressForm.Show();

                // Запустить таймер на 10 секунд
                Timer timer = new Timer();
                timer.Interval = 1000; // 1 секунда
                int counter = 0;
                timer.Tick += async (s, args) =>
                {
                    counter++;
                    progressForm.UpdateProgress(counter * 10, "Установка обновлений...");

                    if (counter == 10)
                    {
                        // Остановить таймер
                        timer.Stop();

                        // Скрыть форму прогресса
                        progressForm.Close();

                        // Показать сообщение о завершении обновлений
                        MessageBox.Show("Все обновления установлены!");

                        
                        
                    }
                };
                timer.Start();

                await Task.Delay(10000); // Ждем 10 секунд перед остановкой таймера
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
