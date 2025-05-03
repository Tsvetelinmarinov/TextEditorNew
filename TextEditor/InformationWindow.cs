/**
 * Text Editor++
 * 
 * InformationWindow - A window that displays information about the editor.
 */

using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;


namespace TextEditor
{

    public class InformationWindow : Form
    {

        public InformationWindow()
        {
            BuildWindow();

            Text = "Информация за приложението";
            Size = new Size(500, 700);
            Icon = Properties.Resources.infoIcon;
            Visible = true;
            BackColor = Color.GhostWhite;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimumSize = Size;
            MaximumSize = Size;

        }

        private void BuildWindow()
        {

            //Spacer
            Label bottomSpacer = new Label()
            {
                BackColor = Color.DarkGray,
                Bounds = new Rectangle(0, 635, 500, 1)
            };
            Controls.Add(bottomSpacer);

            //Facebook logo with link to my profile
            PictureBox fbLogo = new PictureBox()
            {
                Image = Properties.Resources.facebook,
                //SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand,
                Bounds = new Rectangle(440, 30, 30, 30)
            };
            fbLogo.MouseHover += (sender, eventArgs) =>
            {
                ToolTip tip = new ToolTip();
                tip.Show
                (
                    "Линк към профила ми във Facebook",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            fbLogo.Click += (sender, eventArgs) =>
            {
                Process.Start
                (
                    new ProcessStartInfo("https://www.facebook.com/profile.php?id=100010457925248")
                    {
                        UseShellExecute = true
                    }
                );
            };
            Controls.Add(fbLogo);

            //Text box with the information about the editor
            RichTextBox infoBox = new RichTextBox()
            {
                Bounds = new Rectangle(0, 120, 440, 580),
                BackColor = Color.GhostWhite,
                Font = new Font("Cascadia Code SemiLight", 13),
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.None,
                Cursor = Cursors.Hand,
                ReadOnly = true,
                Multiline = true,
                Text = Properties.Resources.informationText
            };
            Controls.Add(infoBox);

        }

    }

}