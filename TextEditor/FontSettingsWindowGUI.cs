/**
 * TextEditor++
 * 
 * FontSettingsWindowGUI - The Graphical User Interface for the window from menu Options -> font and color
 */

using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;
using System;

namespace TextEditor
{
    internal class FontSettingsWindowGUI : Form
    {
        //Font name
        private Label fontName;
        private ComboBox fontBox;
        private InstalledFontCollection fonts;
        //Font style
        private Label fontStyle;
        private ComboBox fontStylesBox;
        //Font size
        private Label fontSize;
        private NumericUpDown fontSizeSpiner;
        //Font color
        private Label fontColor;
        private Label fontColorChooser;
        //Event for changing the font
        public new event Action<Font> FontChanged;                                  
        public event Action<Color> ForegroundOnChange;
        //Dynamic font style - changed
        private FontStyle style;
        //Connect to the main GUI
        private readonly ApplicationGUI app;

        /// <summary>
        /// The Graphical User Interface of the Font settings window
        /// </summary>
        public FontSettingsWindowGUI()
        {
            //Initialize components
            PlaceComponents();

            //Setting up the window
            Text = "Настройки на шрифта";
            Icon = Properties.Resources.fontWindowIcon;
            Size = new Size(880, 150);
            Location = new Point(200, 300);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Visible = true;
            BackColor = Color.GhostWhite;
            MaximizeBox = false;
            MinimumSize = Size;
            MaximumSize = Size;
        }

        /// <summary>
        /// Create and set the Graphical User Interface
        /// </summary>
        private void PlaceComponents()
        {
            //Font name
            fontName = new Label()
            {
                Text = "шрифт",
                Font = new Font("Seige UI", 12),
                Bounds = new Rectangle(20, 40, 60, 20)
            };
            Controls.Add(fontName);
            fontBox = new ComboBox()
            {
                Bounds = new Rectangle(82, 37, 250, 30),
                Font = new Font("Seoge UI", 12),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.GhostWhite,
                Cursor = Cursors.Hand
            };
            fonts = new InstalledFontCollection();
            foreach (FontFamily fontFamily in fonts.Families)
            {
                fontBox.Items.Add(fontFamily.Name);
            }
            fontBox.SelectedItem = "Cascadia Code";
            fontBox.SelectedIndexChanged += (sender, eventArgs) =>
            {
                if (fontBox.SelectedItem != null)
                {
                    FontChanged?.Invoke(new Font(fontBox.SelectedItem.ToString(), app.editor.Font.Size));
                };
            };
            fontBox.MouseHover += (sender, eventArgs) =>
            {
                new ToolTip().Show
                (
                    "шрифт",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            Controls.Add(fontBox);

            //Font size
            fontSize = new Label()
            {
                Text = "размер",
                Font = fontName.Font,
                Bounds = new Rectangle(355, 40, 65, 20)
            };
            Controls.Add(fontSize);
            fontSizeSpiner = new NumericUpDown()
            {
                Minimum = 1,
                Maximum = 50,
                Value = (int)app.editor.Font.Size,
                Bounds = new Rectangle(420, 37, 60, 20),
                Font = new Font("Cascadia Code", 12),
                BorderStyle = BorderStyle.FixedSingle,
                Increment = 1
            };
            fontSizeSpiner.ValueChanged += (sender, eventArgs) =>
            {
                if (fontBox.SelectedItem != null)
                {
                    FontChanged?.Invoke(new Font(fontBox.SelectedItem.ToString(), (float)fontSizeSpiner.Value));
                }
            };
            fontSizeSpiner.MouseHover += (sender, eventArgs) =>
            {
                new ToolTip().Show
                (
                    "размер на шрифта",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    2500
                );
            };
            Controls.Add(fontSizeSpiner);

            //Font style
            fontStyle = new Label()
            {
                Text = "стил",
                Font = fontName.Font,
                Bounds = new Rectangle(504, 40, 45, 20)
            };
            Controls.Add(fontStyle);
            fontStylesBox = new ComboBox()
            {
                Bounds = new Rectangle(552, 37, 180, 25),
                Font = fontBox.Font,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.GhostWhite,
                Cursor = Cursors.Hand
            };
            string[] fontStyles = [ "нормален", "наклонен", "удебелен", "наклонен и удебелен" ];
            foreach (string style in fontStyles)
            {
                fontStylesBox.Items.Add(style);
            }
            fontStylesBox.SelectedItem = "нормален";
            fontStylesBox.SelectedIndexChanged += (sender, eventArgs) =>
            {
                switch (fontStylesBox.SelectedItem.ToString())
                {
                    case "нормален":
                        style = FontStyle.Regular;
                        break;
                    case "удебелен":
                        style = FontStyle.Bold;
                        break;
                    case "наклонен":
                        style = FontStyle.Italic;
                        break;
                    case "наклонен и удебелен":
                        style = FontStyle.Bold | FontStyle.Italic;
                        break;
                    default:
                        if 
                        (
                            MessageBox.Show
                            (
                                "Системна грешка. Не може да продължите работа!",
                                "Системна грешка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            ) 
                            == DialogResult.OK
                        )
                        {
                            Application.Exit();
                            Environment.Exit(0);
                        }
                    break;
                }

                FontChanged?.Invoke(new Font(fontBox.SelectedItem.ToString(), (float)fontSizeSpiner.Value, style));
            };
            fontStylesBox.MouseHover += (sender, eventArgs) =>
            {
                ToolTip tip = new ToolTip();
                tip.Show
                (
                    "стил на шрифта",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    2500
                );
            };
            Controls.Add(fontStylesBox);

            //Font color
            fontColor = new Label()
            {
                Text = "цвят",
                Font = fontName.Font,
                Bounds = new Rectangle(756, 40, 45, 20)
            };
            Controls.Add(fontColor);
            fontColorChooser = new Label()
            {
                Bounds = new Rectangle(807, 38, 25, 25),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Black,
            };
            fontColorChooser.Click += (sender, eventArgs) =>
            {
                ColorDialog colorChooser = new ColorDialog();
                if (colorChooser.ShowDialog() == DialogResult.OK)
                {
                    ForegroundOnChange?.Invoke(colorChooser.Color);
                    fontColorChooser.BackColor = colorChooser.Color;
                }
            };
            fontColorChooser.MouseHover += (sender, eventArgs) =>
            {
                new ToolTip().Show
                (
                    "цвят на шрифта", 
                    this, 
                    PointToClient(Cursor.Position).X, 
                    PointToClient(Cursor.Position).Y, 
                    1500
                );
            };
            Controls.Add(fontColorChooser);
           
        }
    }
}