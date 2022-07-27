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

        private const string BACKUP_FILE_NAME = "defaults.zip";

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

        private readonly Color labelForeColor;
        private readonly Color textBoxForeColor;
        private readonly Color textBoxBackColor;

        private readonly string backupFileNameWithPath;

        private readonly Dictionary<string, string> screenSizesUI;

        private string selectedScreenSize;

        /// <summary>
        /// Creates the Form of the Font Manager Window
        /// </summary>
        /// <param name="screenWidth"></param>
        public FontsManagerForm(string screenWidth)
        {
            InitializeComponent();

            labelForeColor = label1.ForeColor;
            textBoxForeColor = textBox1.ForeColor;
            textBoxBackColor = textBox1.BackColor;

            // Use the same icon as executable
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            screenSizesUI = new Dictionary<string, string>
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

            if (screenWidthsUIComboBox.Items.Contains(screenWidth))
            {
                switch (screenWidth)
                {
                    case "1366":
                        screenWidthsUIComboBox.SelectedItem = "1024";
                        break;
                    case "1280":
                        screenWidthsUIComboBox.SelectedItem = "800";
                        break;
                    case "1920":
                        screenWidthsUIComboBox.SelectedItem = "1280";
                        break;
                    default:
                        screenWidthsUIComboBox.SelectedItem = screenWidth;
                        break;
                }
            }
            else
                screenWidthsUIComboBox.SelectedIndex = 0;

            selectedScreenSize = screenSizesUI[screenWidthsUIComboBox.SelectedItem.ToString()];

            backupFileNameWithPath = FONTS_DIRECTORY + "\\" + BACKUP_FILE_NAME;

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

            screenWidthsUIComboBox.SelectedIndexChanged += new EventHandler(ScreenWidthsUIComboBox_SelectedIndexChanged);

            // Select SAVE button instead of first TextBox
            saveButton.Select();
        }

        /// <summary>
        /// This method will be called right after Constructor
        /// </summary>
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
                    // DO NOT just create arhive in the same directory, because it will try to achive itself ;-)
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

        /// <summary>
        /// This method initializes GUI with font names and their sizes
        /// </summary>
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

        /// <summary>
        /// This method initialises one "line": TextBox + Button + NumericUpDown for a particular font
        /// </summary>
        private void ReadFntToUI(string fileNameWithPath, string searchFor, Label label, TextBox textBox, Button button, NumericUpDown numericUpDown)
        {
            if (File.Exists(fileNameWithPath))
            {
                // Useful after pressing Default button
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
                                // ... unless you specify the BackColor. Even if you would change it from System.Control
                                // to System.Control. Because... logic ;-)
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
                label.ForeColor = Color.Red;
                textBox.Enabled = false;
                button.Enabled = false;
                numericUpDown.Enabled = false;
            }
        }

        /// <summary>
        /// This method will be called when user changes screen resolution ComboBox
        /// </summary>
        private void ScreenWidthsUIComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedScreenSize = screenSizesUI[screenWidthsUIComboBox.SelectedItem.ToString()];

            InitializeGUIWithFonts();
        }

        /// <summary>
        /// This method will be called when one of the TextBoxes has been changed
        /// </summary>
        private void SomeFieldChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = true;
        }

        /// <summary>
        /// This method saves all the font config files
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + ALBERTUS_EXTRA_BOLD_12,      textBox1,  numericUpDown1);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + ALBERTUS_EXTRA_BOLD_14,      textBox2,  numericUpDown2);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + ALBERTUS_EXTRA_BOLD_16,      textBox3,  numericUpDown3);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + ENGRAVERS_OLD_ENGLISH_MT30,  textBox4,  numericUpDown4);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + GILLSANS_11,                 textBox5,  numericUpDown5);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + GILLSANS_11B,                textBox6,  numericUpDown6);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + GILLSANS_16,                 textBox7,  numericUpDown7);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + GILLSANS_BOLD_16,            textBox8,  numericUpDown8);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + QUORUM_MEDIUM_BOLD_13,       textBox9,  numericUpDown9);
            WriteUIToFnt(FONTS_DIRECTORY + "\\" + QUORUM_MEDIUM_BOLD_16,      textBox10, numericUpDown10);

            saveButton.Enabled = false;
            defaultButton.Enabled = true;
        }

        /// <summary>
        /// This method saves changes to a single font configuration file
        /// </summary>
        private void WriteUIToFnt(string fileNameWithPath, TextBox textBox, NumericUpDown numericUpDown)
        {
            if (File.Exists(fileNameWithPath))
            {
                // TODO: Maybe use something less barbaric, like FileStream ;-)
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
                    else if (lines[i].Contains(screenSizesUI[screenWidthsUIComboBox.SelectedItem.ToString()]))
                    {
                        int indexOfEqualSign = lines[i].IndexOf("=");
                        int indexOfEndSimbol = lines[i].IndexOf(";");

                        string currentValue = lines[i].Substring(indexOfEqualSign + 1, indexOfEndSimbol - indexOfEqualSign - 1);
                        currentValue = currentValue.Replace(" ", "");

                        // We are trying to avoid replacing blindly in line like this: "size1024 = 24;" where BOTH "24" would be replaced
                        int indexOfLastValue = lines[i].LastIndexOf(currentValue);

                        lines[i] = lines[i].Remove(indexOfLastValue, currentValue.Length).Insert(indexOfLastValue, numericUpDown.Value.ToString());
                    }
                }

                File.WriteAllLines(fileNameWithPath, lines);
            }
            else
            {
                // TODO: Maybe do something when there is no such file
            }
        }

        /// <summary>
        /// This method restores all the changes since the first startr of the Font Manager
        /// </summary>
        private void DefaultButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(backupFileNameWithPath))
            {
                // We can't use this method because it will throw an Exception if file already exists
                //ZipFile.ExtractToDirectory(backupFileNameWithPath, FONTS_DIRECTORY);

                using (ZipArchive zip = ZipFile.Open(backupFileNameWithPath, ZipArchiveMode.Read))
                {
                    for (int i = 0; i < zip.Entries.Count; i++)
                    {
                        ZipArchiveEntry entry = zip.Entries[i];
                        entry.ExtractToFile(FONTS_DIRECTORY + "\\" + entry.Name, true);
                    }
                }

                InitializeGUIWithFonts();
            }

            saveButton.Enabled = false;
            defaultButton.Enabled = false;
        }

        /// <summary>
        /// This method opens the DoW fonts folder
        /// </summary>
        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(FONTS_DIRECTORY);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox1.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox2.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox3.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox4.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox5.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox6.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox7.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button8_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox8.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button9_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox9.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        /// <summary>
        /// This method adds a file name from a OpenFileDialog
        /// </summary>
        private void Button10_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                textBox10.Text = Path.GetFileName(openFileDialog1.FileName);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ThemedMessageBox.Show("1024x768 -> 1024\n1280x720 -> 800\n1366x768 -> 1024\n2560x1440 -> ?\n1920x1080 -> 1280\n4096x2160 -> ?", "Discovered screen withts");
        }
    }
}
