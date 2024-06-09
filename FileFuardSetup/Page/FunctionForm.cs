using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using Microsoft.Win32;
using System.Windows;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.IO.Compression;
using System.Threading;
using File = System.IO.File;
using MessageBox = System.Windows.Forms.MessageBox;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using FileFuardSetup.DbContex;
using Path = System.IO.Path;
using System.Data.SqlClient;
using System.Timers;
using Volo.Abp.Data;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace FileFuardSetup
{
	public partial class FunctionForm : Form
	{
		private System.Timers.Timer timer1;


		public FunctionForm()
		{
			InitializeComponent();
			timer1 = new System.Timers.Timer();
			timer1.Elapsed += timer__Tick;
			timer1.Interval = 20000; // 20 секунд
			timer1.Start();

		}


		private void OpenFile_Click(object sender, EventArgs e)
		{
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Word Documents|*.docx|PDF Files|*.pdf"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                string fileExtension = Path.GetExtension(fileName).ToLower();

                if (fileExtension == ".docx" || fileExtension == ".pdf")
                {
                    ScanFile(fileName);

                    // Запуск мониторинга файла
                    Task.Run(() => Monitor(Path.GetDirectoryName(fileName)));
                }
                else
                {
                    MessageBox.Show("Выбранный файл не является поддерживаемым форматом (допускаются только .docx и .pdf)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


		/// <summary>
		/// 
		/// 
		/// 
		/// 
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		private void ScanFile(string fileName)
		{
            if (!IsFileAlreadyScanned(fileName))
            {
                if (CheckFile(fileName))
                {
                    string url = DetectURL(fileName);
                    string keywords = DetectKeyword(fileName);

                    bool status = !string.IsNullOrEmpty(url) || !string.IsNullOrEmpty(keywords);

                    if (status)
                    {
                        RespondClean(status, fileName, url, keywords);
                    }
                    else
                    {
                        Reporting(fileName, null);
                    }
                }
            }
            else
            {
                MessageBox.Show($"Файл '{fileName}' уже был отсканирован", "Файл уже отсканирован", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

		private bool IsFileAlreadyScanned(string fileName)
		{
            string selectQuery = "SELECT COUNT(*) FROM ScanResults WHERE FileName = @FileName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@FileName", Path.GetFileName(fileName));
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

		private void Reporting(string file, string componentName)
		{
           try
    {
        string report = string.IsNullOrEmpty(componentName) ?
            $"Файл '{file}' не содержит вредоносных компонентов и является безопасным" :
            $"Файл '{file}' содержал вредоносный компонент: '{componentName}' и был отправлен на проверку администратору";

        System.Diagnostics.Debug.WriteLine(report);
        MessageBox.Show(report, "FileGuard", MessageBoxButtons.OK, MessageBoxIcon.Information);

        string insertQuery = "INSERT INTO ScanResults (FileName, FilePath, ScanTime, Vulnerability) VALUES (@FileName, @FilePath, @ScanTime, @Vulnerability)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@FileName", Path.GetFileName(file));
                command.Parameters.AddWithValue("@FilePath", file);
                command.Parameters.AddWithValue("@ScanTime", DateTime.Now);
                command.Parameters.AddWithValue("@Vulnerability", string.IsNullOrEmpty(componentName) ? "Нет" : "Да");
                command.ExecuteNonQuery();
            }

            UpdateDataGridView();
            connection.Close();
        }
    }
    catch (Exception)
    {
        MessageBox.Show("Ошибка соединения с базой данных");
    }
        }


	///
	private bool CheckFile(string fileName)
		{
            try
            {
                string fileExtension = Path.GetExtension(fileName).ToLower();

                if (fileExtension == ".docx")
                {
                    using (ZipArchive zfile = ZipFile.OpenRead(fileName))
                    {
                        return true;
                    }
                }
                else if (fileExtension == ".pdf")
                {
                    using (PdfReader reader = new PdfReader(fileName))
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[info] file is corrupted or invalid, error: {ex.Message}");
                return false;
            }
        }

		//private bool CheckPDF(string fileName)
		//{
		//    try
		//    {
		//        using (PdfReader reader = new PdfReader(fileName))
		//        {
		//            return true;
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        System.Diagnostics.Debug.WriteLine($"[info] file is corrupted, error: {ex.Message}");
		//        return false;
		//    }
		//}

		private string DetectURL(string fileName)
		{
            List<string> urls = new List<string>();

            try
            {
                using (PdfReader reader = new PdfReader(fileName))
                {
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        string text = PdfTextExtractor.GetTextFromPage(reader, page);
                        string mediapattern = @"(?!\x22)(http|https):\/\/(?!(schemas\.openxmlformats\.org|microsoft\.com)).*?(?=\x22)";

                        foreach (Match match in Regex.Matches(text, mediapattern, RegexOptions.IgnoreCase))
                        {
                            urls.Add(match.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[info] error reading PDF file: {ex.Message}");
            }

            return urls.FirstOrDefault();

        }


        private string Defang(string url)
		{
            Dictionary<string, string> mappings = new Dictionary<string, string>
            {
                {".", "[.]"},
                {":", "[:]"},
                {"http", "hxxp"},
                {"ftp", "fxp"}
            };

            foreach (KeyValuePair<string, string> mapping in mappings)
            {
                url = url.Replace(mapping.Key, mapping.Value);
            }

            return url;
        }

		private string DetectKeyword(string fileName)
		{
            List<string> dangerousKeywords = new List<string>
            {
                "mhtml", "mshta", "!x-usc", "wscript",
                "cmd", "vbscript", "CDATA",
                "OLE Package", "shell", "powershell",
                "Shell", "PowerShell"
            };

            try
            {
                string extension = Path.GetExtension(fileName).ToLower();

                if (extension == ".docx")
                {
                    using (FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (ZipArchive zip = new ZipArchive(f, ZipArchiveMode.Read))
                        {
                            foreach (ZipArchiveEntry entry in zip.Entries)
                            {
                                if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                                {
                                    using (StreamReader sr = new StreamReader(entry.Open()))
                                    {
                                        string line = sr.ReadToEnd();
                                        foreach (string keyword in dangerousKeywords)
                                        {
                                            if (line.Contains(keyword))
                                            {
                                                return keyword;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (extension == ".pdf")
                {
                    using (PdfReader reader = new PdfReader(fileName))
                    {
                        for (int page = 1; page <= reader.NumberOfPages; page++)
                        {
                            string text = PdfTextExtractor.GetTextFromPage(reader, page);
                            foreach (string keyword in dangerousKeywords)
                            {
                                if (text.Contains(keyword))
                                {
                                    return keyword;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[info] error reading file: {ex.Message}");
            }

            return null;
        }


		private void UpdateURL(string file, string url)
		{
			string filename = "word/_rels/document.xml.rels";

			string tempDir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName());
			Directory.CreateDirectory(tempDir);

			using (ZipArchive source = ZipFile.Open(file, ZipArchiveMode.Read))
			{
				foreach (ZipArchiveEntry entry in source.Entries)
				{
					if (entry.FullName == filename)
					{
						string entryFullName = System.IO.Path.Combine(tempDir, entry.FullName);
						Directory.CreateDirectory(System.IO.Path.GetDirectoryName(entryFullName));
						entry.ExtractToFile(entryFullName, true);

						string xmlContent;
						using (StreamReader reader = new StreamReader(entryFullName))
						{
							xmlContent = reader.ReadToEnd();
						}

						xmlContent = xmlContent.Replace(url, Defang(url));

						using (StreamWriter writer = new StreamWriter(entryFullName))
						{
							writer.Write(xmlContent);
						}

						source.GetEntry(filename).Delete();
						source.CreateEntryFromFile(entryFullName, filename);
					}
				}
			}

			Directory.Delete(tempDir, true);
		}


		private void Monitor(string directory)
		{
            Dictionary<string, DateTime> before = FilesToTimestamp(directory);

            while (isMonitoringActive)
            {
                Thread.Sleep(1000);
                Dictionary<string, DateTime> after = FilesToTimestamp(directory);

                List<string> added = after.Keys.Except(before.Keys).ToList();
                List<string> removed = before.Keys.Except(after.Keys).ToList();
                List<string> modified = before.Keys.Where(f => after.ContainsKey(f) && after[f] != before[f]).ToList();

                List<string> instances = new List<string>();
				instances.AddRange(added.Where(f => f.EndsWith(".docx")));
				instances.AddRange(modified.Where(f => f.EndsWith(".docx")));

				foreach (string file in instances)
				{
					System.Diagnostics.Debug.WriteLine($"[mode] monitor");
					System.Diagnostics.Debug.WriteLine($"[time] {DateTime.Now.ToString("dd/MM/yyyy, HH:mm:ss")}");
					System.Diagnostics.Debug.WriteLine($"[file] {file}");

					if (CheckFile(file))
					{
						bool status = false;

						string url = DetectURL(file);
						string keywords = DetectKeyword(file);

						if (url != null)
						{
							System.Diagnostics.Debug.WriteLine($"[warn] external link: {Defang(url)}");
							status = true;
						}
						else
						{
							System.Diagnostics.Debug.WriteLine("[info] external link: None");
						}

						if (!string.IsNullOrEmpty(keywords))
						{
							System.Diagnostics.Debug.WriteLine($"[warn] keywords list: {keywords}");
							status = true;

							if (keywords.Contains("mhtml"))
							{
								System.Diagnostics.Debug.WriteLine($"[warn] looks like a vulnerability CVE-2021-40444");
							}
							else
							{
								System.Diagnostics.Debug.WriteLine($"[warn] looks like a vulnerability CVE-2022-30190");
							}
						}
						else
						{
							System.Diagnostics.Debug.WriteLine("[info] keywords list: None");
						}

						if (status)
						{
							RespondClean(status, file, url, keywords);
						}
					}
				}

				before = after;
			}
		}

		private void RespondClean(bool status, string file, string url, string keywords)
		{
            string filename = "word/_rels/document.xml.rels";

            if (status)
            {
                string tmpFileName = Path.GetTempFileName();
                string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

                Directory.CreateDirectory(tempDir);

                try
                {
                    using (ZipArchive source = ZipFile.Open(file, ZipArchiveMode.Read))
                    {
                        using (ZipArchive destination = ZipFile.Open(tmpFileName, ZipArchiveMode.Create))
                        {
                            foreach (ZipArchiveEntry entry in source.Entries)
                            {
                                if (entry.FullName != filename)
                                {
                                    string entryFullName = Path.Combine(tempDir, entry.FullName);
                                    Directory.CreateDirectory(Path.GetDirectoryName(entryFullName));
                                    entry.ExtractToFile(entryFullName, true);
                                    destination.CreateEntryFromFile(entryFullName, entry.FullName);
                                }
                            }
                        }
                    }

                    File.Delete(file);
                    File.Move(tmpFileName, file);

                    if (url != null)
                    {
                        UpdateURL(file, url);
                    }

                    Reporting(file, keywords);

                    Directory.Delete(tempDir, true);

                    // Записываем в базу данных информацию о заражении, если оно обнаружено
                    string vulnerabilityStatus = string.IsNullOrEmpty(keywords) ? "Нет" : "Да";
                    UpdateDatabaseVulnerabilityStatus(file, vulnerabilityStatus);
                }
                catch (IOException)
                {
                    string message = $"Файл {file} уже был проверен FileGuard.";
                    MessageBox.Show(message, "FileGuard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void UpdateDatabaseVulnerabilityStatus(string fileName, string vulnerabilityStatus)
        {
            try
            {
                string updateQuery = "UPDATE ScanResults SET Vulnerability = @Vulnerability WHERE FileName = @FileName";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FileName", Path.GetFileName(fileName));
                        command.Parameters.AddWithValue("@Vulnerability", vulnerabilityStatus);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса уязвимости в базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dictionary<string, DateTime> FilesToTimestamp(string path)
		{
			return Directory.GetFiles(path).ToDictionary(f => f, f => File.GetLastWriteTime(f));
		}

		public string connectionString = @"Data Source=DESKTOP-N9D0PE1;Initial Catalog=FileGuard;Integrated Security=True;";



		private void UpdateDataGridView()
		{
            try
            {
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new MethodInvoker(delegate { UpdateDataGridView(); }));
                }
                else
                {
                    string selectQuery = "SELECT Id, FileName, FilePath, ScanTime, Vulnerability FROM ScanResults";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }

                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления данных в DataGridView: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void timer__Tick(object sender, EventArgs e)
		{
			UpdateDataGridView();
		}

		private void FunctionForm_Load(object sender, EventArgs e)
		{
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "fileGuardDataSet.ScanResults". При необходимости она может быть перемещена или удалена.
            //this.scanResultsTableAdapter.Fill(this.fileGuardDataSet.ScanResults);
            LoadDataIntoDataGridView();



		}

	   

		private void LoadDataIntoDataGridView()
		{
			
				try
				{
					string query = "SELECT * FROM ScanResults";
					using (SqlConnection connection = new SqlConnection(connectionString))
					{
						using (SqlCommand command = new SqlCommand(query, connection))
						{
							SqlDataAdapter adapter = new SqlDataAdapter(command);
							DataTable dataTable = new DataTable();
							adapter.Fill(dataTable);
							dataGridView1.DataSource = dataTable;
						}
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Ошибка загрузки данных в таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
		   

		}

		private void Reasercher_TextChanged(object sender, EventArgs e)
		{
			FilterDataGridView(Reasercher.Text);
		}

		private void FilterDataGridView(string Reasercher)
		{
			try
			{
				string selectQuery = "SELECT FileName, FilePath, ScanTime, ISNULL(Vulnerability, 'Нет') AS Vulnerability FROM ScanResults WHERE FileName LIKE @SearchText";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
					{
						adapter.SelectCommand.Parameters.AddWithValue("@SearchText", "%" + Reasercher + "%");

						DataTable dt = new DataTable();
						adapter.Fill(dt);
						dataGridView1.DataSource = dt;
					}

					connection.Close();
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Ошибка фильтрации данных в DataGridView", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void OpenPDFgrafics_Click(object sender, EventArgs e)
		{
            string pdfFileName = $"{Environment.MachineName}_PDF_Report.pdf";
            string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), pdfFileName);

            try
            {
                // Создаем документ PDF
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));

                // Добавляем шрифт для поддержки кириллицы
                string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL); // Размер шрифта 12, нормальное начертание

                document.Open();

                // Добавляем заголовок с текущей датой и временем, и зачеркнутый текст
                Paragraph header = new Paragraph();
                string dateTimeNow = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                Chunk chunk = new Chunk($"Отчет от {dateTimeNow}", font);
                chunk.SetUnderline(0.5f, -1.5f); // Создаем зачеркнутый текст
                header.Add(chunk);
                document.Add(header);

                // Получаем данные из базы данных
                string selectQuery = "SELECT COUNT(*) AS Count, Vulnerability FROM ScanResults GROUP BY Vulnerability";
                List<int> counts = new List<int>();
                List<string> vulnerabilities = new List<string>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                counts.Add(reader.GetInt32(0));
                                vulnerabilities.Add(reader.GetString(1));
                            }
                        }
                    }
                    connection.Close();
                }

                // Создаем диаграмму пирога
                MemoryStream chartStream = new MemoryStream();
                Chart chart = new Chart();
                chart.Width = 500;
                chart.Height = 300;
                chart.Titles.Add("Соотношение зараженных и незараженных файлов");
                chart.ChartAreas.Add(new ChartArea());
                chart.Series.Add(new Series("Vulnerability"));
                chart.Series["Vulnerability"].ChartType = SeriesChartType.Pie;
                chart.Series["Vulnerability"].Points.DataBindXY(vulnerabilities, counts);
                chart.SaveImage(chartStream, ChartImageFormat.Png);

                // Добавляем изображение диаграммы в документ PDF
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(chartStream.ToArray());
                document.Add(chartImage);

                // Добавляем список файлов
                Paragraph fileList = new Paragraph("Файлы, участвующие в статистике:", font);
                document.Add(fileList);

                // Получаем список файлов из базы данных
                List<string> files = new List<string>();
                selectQuery = "SELECT FilePath FROM ScanResults";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                files.Add(reader.GetString(0));
                            }
                        }
                    }
                    connection.Close();
                }

                // Добавляем список файлов в документ
                foreach (string file in files)
                {
                    document.Add(new Paragraph(file, font));
                }

                // Добавляем изображение из рабочей директории проекта
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PixelKit.jpg"); // Замените "image.png" на имя вашего файла изображения
                if (File.Exists(imagePath))
                {
                    iTextSharp.text.Image projectImage = iTextSharp.text.Image.GetInstance(imagePath);
                    projectImage.Alignment = Element.ALIGN_CENTER;
                    document.Add(projectImage);
                }

                // Добавляем текст с информацией о программе и лицензии
                Paragraph footer = new Paragraph("Данный файл отчета был создан и проверен программой FileGuard, разработанной в 2024 году и находится под защитой лицензионным соглашением GNU Affero General Public License v3.0.\n\nGNU Affero General Public License v3.0 (AGPL-3.0) — это лицензия, которая позволяет конечным пользователям свободно использовать, изменять и распространять программное обеспечение при условии, что любые изменения и дополнения также будут распространяться под той же лицензией.", font);
                footer.Alignment = Element.ALIGN_CENTER;
                document.Add(footer);

                // Закрываем документ
                document.Close();

                // Открываем созданный PDF-файл
                if (File.Exists(pdfPath))
                {
                    DialogResult result = MessageBox.Show("PDF-файл успешно создан. Открыть файл?", "PDF-файл создан", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(pdfPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void button1_Click(object sender, EventArgs e)
		{
			MainForm MainForm = new MainForm();
			this.Hide();
			MainForm.ShowDialog();
		}

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); // Индекс столбца с ID

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM ScanResults WHERE Id = @Id";
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@Id", selectedId);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Запись успешно удалена");
                            UpdateDataGridView(); // Обновление DataGridView после удаления записи
                        }
                        else
                        {
                            MessageBox.Show("Произошла ошибка при удалении записи");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении записи: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); // Индекс столбца с ID

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string selectQuery = "SELECT FilePath FROM ScanResults WHERE Id = @Id";
                        SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                        selectCommand.Parameters.AddWithValue("@Id", selectedId);
                        string filePath = selectCommand.ExecuteScalar().ToString();

                        // Остановка мониторинга для выбранного файла или папки
                        StopMonitoring(filePath);

                        connection.Close();

                        MessageBox.Show("Мониторинг для выбранного файла или папки был успешно отключен.");

                        // Дополнительно можно изменить цвет выбранной строки на синий
                        dataGridView1.SelectedRows[0].DefaultCellStyle.BackColor = Color.Blue;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при отключении мониторинга: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для отключения мониторинга.");
            }
        }
        private volatile bool isMonitoringActive = true;
        private void StopMonitoring(string directory)
        {
            isMonitoringActive = false;
        }
    }
} 


