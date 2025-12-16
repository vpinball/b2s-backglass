using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;

namespace B2S_SetUp
{
    public partial class Form1 : Form
    {
        private Dictionary<int, string> lineComments = new Dictionary<int, string>();
        private List<string> headerComments = new List<string>();
        private int lastValueIndex = -1;
        private string currentFileName = "ScreenRes.txt"; // Track current filename

        private static readonly Dictionary<int, string> DefaultLineComments = new Dictionary<int, string>
        {
            { 1, "# Playfield Screen resolution width/height, percent values uses the current LEFTMOST screen size as reference" },
            { 3, "# Backglass width/height, percent values uses the current SELECTED Backglass screen size as reference + all marked with (*)" },
            { 4, @"# Define Backglass screen using Display Devicename screen number (\\.\DISPLAY)x or screen coordinates (@x) or screen index (=x)" },
            { 6, "# Backglass x/y position relative to the upper left corner of the screen selected (*)" },
            { 8, "# width/height of the B2S (or Full) DMD area (*)" },
            { 10, "# x/y position of the B2S (or Full) DMD area - relative to the upper left corner of the backglass window (*)" },
            { 11, "# Y-flip, flips the LED display upside down" },
            { 13, "# Background x/y position - relative to the backglass screen - has to be activated in the settings (*)" },
            { 15, "# Background width/height (*)" },
            { 16, @"# path to the background image (C:\path\Frame) or black if none selected" }
        };

        /// <summary>
        /// Validates if a string is a valid screen resolution value (integer or percent).
        /// </summary>
        private static bool IsValidValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim();
            
            // Check for percent value
            if (value.EndsWith("%"))
            {
                string percentStr = value.Substring(0, value.Length - 1);
                return double.TryParse(percentStr, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
            }
            
            // Check for absolute integer value
            return int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
        }

        private static readonly string[] DefaultHeaderComments = new[]
        {
            "# This is a ScreenRes file for the B2SBackglassServer."
        };

        private string GetVersionComment()
        {
            var version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                         ?? Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return $"# V{version.Split('.').Take(3).Aggregate((a, b) => $"{a}.{b}")}";
        }

        public Form1(string[] args = null)
        {
            InitializeComponent();
            
            // Register all event handlers in one place
            this.btnLoad.Click += btnLoad_Click;
            this.btnSave.Click += btnSave_Click;
            this.btnBrowseBackground.Click += btnBrowseBackground_Click;
            
            this.AllowDrop = true;
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;

            // Handle auto-loading
            if (args != null && args.Length > 0)
            {
                // Try to load the first argument as a file
                if (File.Exists(args[0]))
                {
                    LoadFile(args[0]);
                }
                else
                {
                    MessageBox.Show($"Could not find file: {args[0]}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Try to load ScreenRes.txt from the current directory
                string defaultFile = "ScreenRes.txt";
                if (File.Exists(defaultFile))
                {
                    LoadFile(defaultFile);
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    LoadFile(files[0]);
                }
            }
        }

        private void LoadFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                headerComments.Clear();
                lineComments.Clear();
                lastValueIndex = -1;

                // Store the filename for future use
                currentFileName = Path.GetFileName(filePath);

                bool hasComments = lines.Any(line => line.StartsWith("#"));
                if (!hasComments)
                {
                    // Add version and header comments
                    headerComments.Add(GetVersionComment());
                    headerComments.AddRange(DefaultHeaderComments);
                }
                else
                {
                    // If no version comment is found, add it as first comment
                    bool hasVersionComment = lines.Any(line => line.StartsWith("# V"));
                    if (!hasVersionComment)
                    {
                        headerComments.Add(GetVersionComment());
                    }
                }

                int valueIndex = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("#"))
                    {
                        if (lastValueIndex >= 0)
                        {
                            // Comment after a value
                            lineComments[lastValueIndex] = lines[i];
                        }
                        else if (!lines[i].StartsWith("# V"))
                        {
                            // Header comment (skip version comment as it's handled separately)
                            headerComments.Add(lines[i]);
                        }
                        continue;
                    }

                    // Handle empty lines as values
                    if (!lines[i].StartsWith("#"))
                    {
                        lastValueIndex = valueIndex;

                        switch (valueIndex)
                        {
                            case 0: txtPlayfieldWidth.Text = lines[i]; break;
                            case 1: txtPlayfieldHeight.Text = lines[i]; break;
                            case 2: txtBackglassWidth.Text = lines[i]; break;
                            case 3: txtBackglassHeight.Text = lines[i]; break;
                            case 4: txtDisplayDevice.Text = lines[i]; break;
                            case 5: txtBackglassX.Text = lines[i]; break;
                            case 6: txtBackglassY.Text = lines[i]; break;
                            case 7: txtDMDWidth.Text = lines[i]; break;
                            case 8: txtDMDHeight.Text = lines[i]; break;
                            case 9: txtDMDX.Text = lines[i]; break;
                            case 10: txtDMDY.Text = lines[i]; break;
                            case 11: chkYFlip.Checked = lines[i] == "1"; break;
                            case 12: txtBackgroundX.Text = lines[i]; break;
                            case 13: txtBackgroundY.Text = lines[i]; break;
                            case 14: txtBackgroundWidth.Text = lines[i]; break;
                            case 15: txtBackgroundHeight.Text = lines[i]; break;
                            case 16: txtBackgroundImage.Text = lines[i]; break;
                        }
                        valueIndex++;

                        // Add default line comment if one exists for this value
                        if (!lineComments.ContainsKey(valueIndex - 1) && DefaultLineComments.ContainsKey(valueIndex))
                        {
                            lineComments[valueIndex - 1] = DefaultLineComments[valueIndex];
                        }
                    }
                }

                // Update form title to show loaded file
                this.Text = $"B2S ScreenRes Editor - {currentFileName}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseBackground_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp|All files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtBackgroundImage.Text = ofd.FileName;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "ScreenRes Text files|*.txt|ScreenRes Res files|*.res|All files|*.*";
                sfd.DefaultExt = "txt";
                sfd.FileName = currentFileName;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var outputLines = new List<string>();
                        
                        // Ensure version is first line, then header comments
                        outputLines.Add(GetVersionComment());
                        outputLines.AddRange(headerComments.Where(c => !c.StartsWith("# V")));

                        // Helper to add a value and its associated comment
                        void AddValueWithComment(string value, int index)
                        {
                            outputLines.Add(value);
                            if (lineComments.ContainsKey(index))
                            {
                                outputLines.Add(lineComments[index]);
                            }
                            else if (DefaultLineComments.ContainsKey(index + 1))
                            {
                                outputLines.Add(DefaultLineComments[index + 1]);
                            }
                        }

                        // Add values with their comments
                        AddValueWithComment(txtPlayfieldWidth.Text, 0);
                        AddValueWithComment(txtPlayfieldHeight.Text, 1);
                        AddValueWithComment(txtBackglassWidth.Text, 2);
                        AddValueWithComment(txtBackglassHeight.Text, 3);
                        AddValueWithComment(txtDisplayDevice.Text, 4);
                        AddValueWithComment(txtBackglassX.Text, 5);
                        AddValueWithComment(txtBackglassY.Text, 6);
                        AddValueWithComment(txtDMDWidth.Text, 7);
                        AddValueWithComment(txtDMDHeight.Text, 8);
                        AddValueWithComment(txtDMDX.Text, 9);
                        AddValueWithComment(txtDMDY.Text, 10);
                        AddValueWithComment(chkYFlip.Checked ? "1" : "0", 11);
                        AddValueWithComment(txtBackgroundX.Text, 12);
                        AddValueWithComment(txtBackgroundY.Text, 13);
                        AddValueWithComment(txtBackgroundWidth.Text, 14);
                        AddValueWithComment(txtBackgroundHeight.Text, 15);
                        AddValueWithComment(txtBackgroundImage.Text, 16);

                        File.WriteAllLines(sfd.FileName, outputLines);
                        
                        // Update current filename and form title
                        currentFileName = Path.GetFileName(sfd.FileName);
                        this.Text = $"B2S ScreenRes Editor - {currentFileName}";
                        
                        MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "ScreenRes Text files|*.txt|ScreenRes Res files|*.res|All files|*.*";
                ofd.FileName = currentFileName;
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadFile(ofd.FileName);
                }
            }
        }
    }
}