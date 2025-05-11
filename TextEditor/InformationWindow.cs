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

        //Application name 
        private Label appName;
        private Label appName2;

        //Application version
        private Label appVersion;
        private Label appVersion2;

        //Description
        private Label description;
        private Label description2;

        //Author
        private Label author;
        private Label authorName;

        //Manifacture
        private Label manifacture;
        private Label manifacture2;

        //Place
        private Label place;
        private Label place2;



        public InformationWindow()
        {

            BuildWindow();

            Text = "Информация за приложението";
            Size = new Size(530, 600);
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
                Bounds = new Rectangle(0, 535, 530, 1)
            };
            Controls.Add(bottomSpacer);

            //Facebook logo with link to my profile
            PictureBox fbLogo = new PictureBox()
            {
                Image = Properties.Resources.facebook,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand,
                Bounds = new Rectangle(467, 30, 30, 30)
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

            //Information icon
            PictureBox infoIcon = new PictureBox()
            {
                Image = Properties.Resources.github,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand,
                Bounds = new Rectangle(10, 30, 30, 30)
            };
            infoIcon.MouseHover += (sender, eventArgs) =>
            {
                new ToolTip().Show
                (
                    "Линк към GitHub репозиторията на приложението",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    3500
                );
            };
            infoIcon.Click += (sender, eventArgs) =>
            {
                Process.Start
                (
                    new ProcessStartInfo("https://github.com/Tsvetelinmarinov/TextEditorNew")
                    {
                        UseShellExecute = true
                    }
                );
            };
            Controls.Add(infoIcon);

            //Spacer / Separator
            Label topSpacer = new Label()
            {
                BackColor = Color.DarkGray,
                Bounds = new Rectangle(0, 75, 530, 1)
            };
            Controls.Add(topSpacer);

            //Labels with the information about the editor
            appName = new Label()
            {
                Text = "Име на приложението",
                Font = new Font("Seoge UI", 11),
                Bounds = new Rectangle(5, 140, 200, 20)
            };
            Controls.Add(appName);

            appName2 = new Label()
            {
                Text = "TextEditor++",
                Font = appName.Font,
                Bounds = new Rectangle(10, 160, 130, 20)
            };
            Controls.Add(appName2);

            appVersion = new Label()
            {
                Text = "Версия",
                Font = appName.Font,
                Bounds = new Rectangle(5, 200, 100, 20)
            };
            Controls.Add(appVersion);

            appVersion2 = new Label()
            {
                Text = "1.0.0",
                Font = appName.Font,
                Bounds = new Rectangle(10, 220, 100, 20)
            };
            Controls.Add(appVersion2);

            description = new Label()
            {
                Text = "Предназначение",
                Font = appName.Font,
                Bounds = new Rectangle(5, 260, 130, 20)
            };
            Controls.Add(description);

            description2 = new Label()
            {
                Text = "Опростен текстов рекактор работещ с базови програмни файлове",
                Font = appName.Font,
                Bounds = new Rectangle(10, 280, 480, 20)
            };
            Controls.Add(description2);

            author = new Label()
            {
                Text = "Програмист",
                Font = appName.Font,
                Bounds = new Rectangle(5, 320, 100, 20)
            };
            Controls.Add(author);

            authorName = new Label()
            {
                Text = "Цветелин Маринов",
                Font = appName.Font,
                Bounds = new Rectangle(10, 340, 150, 20)
            };
            Controls.Add(authorName);

            manifacture = new Label()
            {
                Text = "Дата на пускане в употреба",
                Font = appName.Font,
                Bounds = new Rectangle(5, 380, 300, 20)
            };
            Controls.Add(manifacture);

            manifacture2 = new Label()
            {
                Text = "Неделя, 11 Май, 2025",
                Font = appName.Font,
                Bounds = new Rectangle(10, 400, 170, 20)
            };
            Controls.Add(manifacture2);

            place = new Label()
            {
                Text = "Място на пройзводство",
                Font = appName.Font,
                Bounds = new Rectangle(5, 440, 190, 20)
            };
            Controls.Add(place);

            place2 = new Label()
            {
                Text = "София, България (BG), Европейски съюз (EU)",
                Font = appName.Font,
                Bounds = new Rectangle(10, 460, 500, 20)
            };
            Controls.Add(place2);

        }

    }

}