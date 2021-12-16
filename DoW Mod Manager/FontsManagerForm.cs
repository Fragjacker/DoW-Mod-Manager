using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace DoW_Mod_Manager
{
    public partial class FontsManagerForm : Form
    {
        private const string ALBERTUS_EXTRA_BOLD_12     = "albertus extra bold12.fnt";
        private const string ALBERTUS_EXTRA_BOLD_14     = "albertus extra bold14.fnt";
        private const string ALBERTUS_EXTRA_BOLD_16     = "albertus extra bold16.fnt";
        private const string ENGRAVERS_OLD_ENGLISH_MT30 = "engravers old english mt30.fnt";
        private const string GILLSANS_11                = "gillsans_11.fnt";
        private const string GILLSANS_11B               = "gillsans_11b.fnt";
        private const string GILLSANS_16                = "gillsans_16.fnt";
        private const string GILLSANS_BOLD_16           = "gillsans_bold_16.fnt";
        private const string QUORUM_MEDIUM_BOLD_13      = "quorum medium bold13.fnt";
        private const string QUORUM_MEDIUM_BOLD_16      = "quorum medium bold16.fnt";

        private const string SIZE_DEFAULT = "sizeDefault";
        private const string SIZE_640     = "size640";
        private const string SIZE_800     = "size800";
        private const string SIZE_1024    = "size1024";
        private const string SIZE_1280    = "size1280";
        private const string SIZE_1366    = "size1366";
        private const string SIZE_1600    = "size1600";
        private const string SIZE_1920    = "size1920";
        private const string SIZE_2560    = "size2560";
        private const string SIZE_4096    = "size4096";

        private readonly string FONTS_DIRECTORY = Path.Combine(Directory.GetCurrentDirectory(), "Engine", "Locale", "English", "data", "font");
        private readonly string BACKUP_FILE_NAME = "backup.zip";

        private readonly Color labelForeColor;
        private readonly Color textBoxForeColor;
        private readonly Color textBoxBackColor;

        private readonly string backupFileNameWithPath;

        //private readonly DirectoryInfo fontsDirectory;

        private readonly Dictionary<string, string> screenSizes;
        private string selectedScreenSize;

        //private readonly FileInfo[] fonts;

        public FontsManagerForm(string screenWidth)
        {
            InitializeComponent();

            labelForeColor = label1.ForeColor;
            textBoxForeColor = textBox1.ForeColor;
            textBoxBackColor = textBox1.BackColor;

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            screenSizes = new Dictionary<string, string>
            {
                { "Default", SIZE_DEFAULT },
                { "640", SIZE_640 },
                { "800", SIZE_800 },
                { "1024", SIZE_1024 },
                { "1280", SIZE_1280 },
                { "1366", SIZE_1366 },
                { "1600", SIZE_1600 },
                { "1920", SIZE_1920 },
                { "2560", SIZE_2560 },
                { "4096", SIZE_4096 }
            };

            if (comboBox1.Items.Contains(screenWidth))
                comboBox1.SelectedItem = screenWidth;
            else
                comboBox1.SelectedIndex = 0;

            selectedScreenSize = screenSizes[comboBox1.SelectedItem.ToString()];

            backupFileNameWithPath = FONTS_DIRECTORY + "\\" + BACKUP_FILE_NAME;

            //fontsDirectory = new DirectoryInfo(FONTS_DIRECTORY);
            //fonts = fontsDirectory.GetFiles("*.ttf");

            InitializeGUIWithFonts();

            // Register events here to prevent them from firing after fonts information would be loaded
            numericUpDown1.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox1.TextChanged += new EventHandler(SomeFieldChanged);
            numericUpDown2.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox2.TextChanged += new EventHandler(SomeFieldChanged);
            numericUpDown3.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox3.TextChanged += new EventHandler(SomeFieldChanged);
            numericUpDown4.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox4.TextChanged += new EventHandler(SomeFieldChanged);
            numericUpDown5.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox5.TextChanged += new EventHandler(SomeFieldChanged);
            numericUpDown6.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox6.TextChanged += new EventHandler(SomeFieldChanged);
            numericUpDown7.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox7.TextChanged += new EventHandler(SomeFieldChanged);
            numericUpDown8.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox8.TextChanged += new EventHandler(SomeFieldChanged);
            numericUpDown9.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox9.TextChanged += new EventHandler(SomeFieldChanged);
            numericUpDown10.ValueChanged += new EventHandler(SomeFieldChanged);
            textBox10.TextChanged += new EventHandler(SomeFieldChanged);

            comboBox1.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);

            saveButton.Select();
        }

        private void FontsManagerForm_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(FONTS_DIRECTORY))
            {
                openFileDialog1.InitialDirectory = FONTS_DIRECTORY;

                if (!File.Exists(backupFileNameWithPath))
                {
                    // First, we create an archive from a font folder and write it in DoW root directory
                    ZipFile.CreateFromDirectory(FONTS_DIRECTORY, BACKUP_FILE_NAME);

                    // Then we move backup file to a Font directory for convenience
                    // DO NOT just create arhive in the same directory,
                    // because it will try to achive itself ;-)
                    // (or use more complex ZipArchive class)
                    File.Move(BACKUP_FILE_NAME, backupFileNameWithPath);
                }
            }
            else
            {
                ThemedMessageBox.Show("If you want to manage your fonts - install \"A Larger Fonts\" mod first!", "Suggestion:");
                Close();
            }
        }

        private void InitializeGUIWithFonts()
        {
            ReadFntToUI(FONTS_DIRECTORY + "\\" + ALBERTUS_EXTRA_BOLD_12,     selectedScreenSize,  label1,  textBox1,  button1,  numericUpDown1);
            ReadFntToUI(FONTS_DIRECTORY + "\\" + ALBERTUS_EXTRA_BOLD_14,     selectedScreenSize,  label2,  textBox2,  button2,  numericUpDown2);
            ReadFntToUI(FONTS_DIRECTORY + "\\" + ALBERTUS_EXTRA_BOLD_16,     selectedScreenSize,  label3,  textBox3,  button3,  numericUpDown3);
            ReadFntToUI(FONTS_DIRECTORY + "\\" + ENGRAVERS_OLD_ENGLISH_MT30, selectedScreenSize,  label4,  textBox4,  button4,  numericUpDown4);
            ReadFntToUI(FONTS_DIRECTORY + "\\" + GILLSANS_11,                selectedScreenSize,  label5,  textBox5,  button5,  numericUpDown5);
            ReadFntToUI(FONTS_DIRECTORY + "\\" + GILLSANS_11B,               selectedScreenSize,  label6,  textBox6,  button6,  numericUpDown6);
            ReadFntToUI(FONTS_DIRECTORY + "\\" + GILLSANS_16,                selectedScreenSize,  label7,  textBox7,  button7,  numericUpDown7);
            ReadFntToUI(FONTS_DIRECTORY + "\\" + GILLSANS_BOLD_16,           selectedScreenSize,  label8,  textBox8,  button8,  numericUpDown8);
            ReadFntToUI(FONTS_DIRECTORY + "\\" + QUORUM_MEDIUM_BOLD_13,      selectedScreenSize,  label9,  textBox9,  button9,  numericUpDown9);
            ReadFntToUI(FONTS_DIRECTORY + "\\" + QUORUM_MEDIUM_BOLD_16,      selectedScreenSize, label10, textBox10, button10, numericUpDown10);
        }

        private void ReadFntToUI(string fileNameWithPath, string searchFor, Label label, TextBox textBox, Button button, NumericUpDown numericUpDown)
        {
            if (File.Exists(fileNameWithPath))
            {
                // Useful after pressing Default button
                FileInfo file = new FileInfo(fileNameWithPath);
                label.ForeColor = labelForeColor;
                textBox.ForeColor = textBoxForeColor;
                textBox.BackColor = textBoxBackColor;
                textBox.Enabled = true;
                button.Enabled = true;
                numericUpDown.Enabled = true;

                using (StreamReader sr = new StreamReader(fileNameWithPath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("file"))
                        {
                            int firstIndexOfQuote = line.IndexOf('"');
                            int lastIndexOfQuote = line.LastIndexOf('"');

                            string fileName = line.Substring(firstIndexOfQuote + 1, lastIndexOfQuote - firstIndexOfQuote - 1);
                            if (File.Exists(FONTS_DIRECTORY + "\\" + fileName))
                                textBox.Text = fileName;
                            else
                            {
                                textBox.Text = $"File {fileName} was not found!";
                                // You can't change ForeColor if a TextBox if's ReadOnly = true
                                // ... unless you specify the BackColor. Even if you would change
                                // it from System.Control to System.Control. Because... logic ;-)
                                textBox.BackColor = SystemColors.Control;
                                textBox.ForeColor = Color.Red;        
                            }
                        }
                        else if (line.Contains(searchFor))
                        {
                            line = line.Replace(" ", "");

                            int indexOfEqualSign = line.IndexOf("=");
                            int indexOfEndSimbol = line.IndexOf(";");

                            numericUpDown.Value = Convert.ToDecimal(line.Substring(indexOfEqualSign + 1, indexOfEndSimbol - indexOfEqualSign - 1));

                            break;      // We found what we searched for
                        }
                    }
                }
            }
            else
            {
                FileInfo file = new FileInfo(fileNameWithPath);
                label.ForeColor = Color.Red;
                textBox.Enabled = false;
                button.Enabled = false;
                numericUpDown.Enabled = false;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedScreenSize = screenSizes[comboBox1.SelectedItem.ToString()];

            InitializeGUIWithFonts();
        }

        private void SomeFieldChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + ALBERTUS_EXTRA_BOLD_12, textBox1, numericUpDown1);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + ALBERTUS_EXTRA_BOLD_14, textBox2, numericUpDown2);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + ALBERTUS_EXTRA_BOLD_16, textBox3, numericUpDown3);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + ENGRAVERS_OLD_ENGLISH_MT30, textBox4, numericUpDown4);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + GILLSANS_11, textBox5, numericUpDown5);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + GILLSANS_11B, textBox6, numericUpDown6);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + GILLSANS_16, textBox7, numericUpDown7);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + GILLSANS_BOLD_16, textBox8, numericUpDown8);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + QUORUM_MEDIUM_BOLD_13, textBox9, numericUpDown9);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + QUORUM_MEDIUM_BOLD_16, textBox10, numericUpDown10);

            saveButton.Enabled = false;
            defaultButton.Enabled = true;
        }

        private void WriteUIToFnt(string fileNameWithPath, TextBox textBox, NumericUpDown numericUpDown)
        {
            if (File.Exists(fileNameWithPath))
            {
                string[] lines = File.ReadAllLines(fileNameWithPath);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("file"))
                    {
                        int firstIndexOfQuote = lines[i].IndexOf('"');
                        int lastIndexOfQuote = lines[i].LastIndexOf('"');

                        string currentValue = lines[i].Substring(firstIndexOfQuote + 1, lastIndexOfQuote - firstIndexOfQuote - 1);
                        lines[i] = lines[i].Replace(currentValue, textBox.Text);
                    }
                    else if (lines[i].Contains(screenSizes[comboBox1.SelectedItem.ToString()]))
                    {
                        // We don't want to change this string - just to make it easier to parse
                        string tempLine = lines[i].Replace(" ", "");

                        int indexOfEqualSign = tempLine.IndexOf("=");
                        int indexOfEndSimbol = tempLine.IndexOf(";");

                        tempLine = tempLine.Substring(indexOfEqualSign + 1, indexOfEndSimbol - indexOfEqualSign - 1);

                        // Now when we know what was the previous value - we could replace it with a new one
                        lines[i] = lines[i].Replace(tempLine, numericUpDown.Value.ToString());
                    }
                }

                File.WriteAllLines(fileNameWithPath, lines);
            }
            else
            {
                // TODO: Do something when there is no such file!
            }
        }

        private void DefaultButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(backupFileNameWithPath))
            {
                // We can't use this method because it will throw an Exception if file already exists
                //ZipFile.ExtractToDirectory(backupFileNameWithPath, FONTS_DIRECTORY);

                using (ZipArchive zip = ZipFile.Open(backupFileNameWithPath, ZipArchiveMode.Read))
                {
                    foreach (var entry in zip.Entries)
                    {
                        entry.ExtractToFile(FONTS_DIRECTORY + "\\" + entry.Name, true);
                    }
                }

                InitializeGUIWithFonts();
            }

            saveButton.Enabled = false;
            defaultButton.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox1.Text = file.Name;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox2.Text = file.Name;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox3.Text = file.Name;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox4.Text = file.Name;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox5.Text = file.Name;
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox6.Text = file.Name;
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox7.Text = file.Name;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox8.Text = file.Name;
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox9.Text = file.Name;
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                textBox10.Text = file.Name;
            }
        }
    }
}
