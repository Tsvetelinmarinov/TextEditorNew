/**
 * Text Editor++
 * 
 * InformationWindow - A window that displays information about the editor.
 */

using System.Windows.Forms;
using System.Drawing;


namespace TextEditor
{

    public class InformationWindow : Form
    {

        public InformationWindow()
        {

            BuildWindow();

            Text = "Information";
            Size = new Size(500, 700);
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

            //Spacers
            Label leftTopSpacer = new Label()
            {
                BackColor = Color.DarkGray,
                Size = new Size(350, 1),
                Location = new Point(0, 30),
            };
            Controls.Add(leftTopSpacer);

            Label rightTopSpacer = new Label()
            {
                BackColor = Color.DarkGray,
                Bounds = new Rectangle(440, 30, 60, 1)
            };
            Controls.Add(rightTopSpacer);

            Label bottomSpacer = new Label()
            {
                BackColor = Color.DarkGray,
                Bounds = new Rectangle(0, 635, 500, 1)
            };
            Controls.Add(bottomSpacer);

        }

    }

}