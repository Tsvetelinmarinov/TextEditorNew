/**
 * TextEditor++
 * 
 * FontSettingsWindowGUI - The Graphical User Interface for the window from menu Options -> font and color
 */

using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;


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
        private string styles;

        //Font size
        private Label fontSize;
        private NumericUpDown fontSizeSpiner;


        //Font color
        private Label fontColor;
        private ColorDialog fontColorChooser;


        public FontSettingsWindowGUI()
        {

            //Initialize components
            PlaceComponents();

            //Setting up the window
            Text = "Настройки на шрифта";
            Icon = Properties.Resources.fontWindowIcon;
            Size = new Size(900, 150);
            Location = new Point(200, 300);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Visible = true;
            BackColor = Color.GhostWhite;
            MaximizeBox = false;
            MinimumSize = Size;
            MaximumSize = Size;

        }


        /**
         * Build the font settings window components
         */
        private void PlaceComponents()
        {

            //Font name
            fontName = new Label()
            {
                Text = "Шрифт",
                Font = new Font("Seige UI", 11),
                Bounds = new Rectangle(20, 40, 60, 20)
            };
            Controls.Add(fontName);

            fontBox = new ComboBox()
            {
                Bounds = new Rectangle(82, 37, 250, 30),
                Font = new Font("Seige UI", 12),
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

            Controls.Add(fontBox);

        }

    }

}