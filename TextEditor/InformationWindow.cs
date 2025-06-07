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
        /// <summary>
        /// The Graphical User Interface of the Information window
        /// </summary>
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

        /// <summary>
        /// Create the Graphical User Interface
        /// </summary>
        private void BuildWindow()
        {
            BuildLogos();
            BuildDescription();
        }
        /// <summary>
        /// Create and set Facebook and Github logos
        /// </summary>
        private void BuildLogos()
        {
            //Spacer
            Label bottomSpacer = new()
            {
                BackColor = Color.DarkGray,
                Bounds = new Rectangle(0, 535, 530, 1)
            };
            //Facebook logo with link to my profile
            PictureBox fbLogo = new()
            {
                Image = Properties.Resources.facebook,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand,
                Bounds = new Rectangle(467, 30, 30, 30)
            };
            fbLogo.MouseHover += (sender, eventArgs) =>
            {
                ToolTip tip = new();
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
                string profile = "https://www.facebook.com/profile.php?id=100010457925248";
                Process.Start(new ProcessStartInfo(profile) { UseShellExecute = true });
            };
            //Information icon
            PictureBox infoIcon = new()
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
                string repo = "https://github.com/Tsvetelinmarinov/TextEditorNew";
                Process.Start(new ProcessStartInfo(repo) { UseShellExecute = true });
            };
            //Spacer / Separator
            Label topSpacer = new()
            {
                BackColor = Color.DarkGray,
                Bounds = new Rectangle(0, 75, 530, 1)
            };

            Controls.Add(topSpacer);
            Controls.Add(bottomSpacer);
            Controls.Add(infoIcon);
            Controls.Add(fbLogo);
        }
        /// <summary>
        /// Create and set the description labes
        /// </summary>
        private void BuildDescription()
        {
            //Labels with the information about the editor
            Label appName = new()
            {
                Text = "Име на приложението",
                Font = new Font("Seoge UI", 11),
                Bounds = new Rectangle(5, 140, 200, 20)
            };
            Label appName2 = new()
            {
                Text = "TextEditor++",
                Font = appName.Font,
                Bounds = new Rectangle(10, 160, 130, 20)
            };
            Label appVersion = new()
            {
                Text = "Версия",
                Font = appName.Font,
                Bounds = new Rectangle(5, 200, 100, 20)
            };
            Label appVersion2 = new()
            {
                Text = "1.0.0",
                Font = appName.Font,
                Bounds = new Rectangle(10, 220, 100, 20)
            };
            Label description = new()
            {
                Text = "Предназначение",
                Font = appName.Font,
                Bounds = new Rectangle(5, 260, 130, 20)
            };
            Label description2 = new()
            {
                Text = "Опростен текстов рекактор работещ с базови програмни файлове",
                Font = appName.Font,
                Bounds = new Rectangle(10, 280, 480, 20)
            };
            Label author = new()
            {
                Text = "Програмист",
                Font = appName.Font,
                Bounds = new Rectangle(5, 320, 100, 20)
            };
            Label authorName = new()
            {
                Text = "Цветелин Маринов",
                Font = appName.Font,
                Bounds = new Rectangle(10, 340, 150, 20)
            };
            Label manifacture = new()
            {
                Text = "Дата на пускане в употреба",
                Font = appName.Font,
                Bounds = new Rectangle(5, 380, 300, 20)
            };
            Label manifacture2 = new()
            {
                Text = "Неделя, 11 Май, 2025",
                Font = appName.Font,
                Bounds = new Rectangle(10, 400, 170, 20)
            };
            Label place = new()
            {
                Text = "Място на пройзводство",
                Font = appName.Font,
                Bounds = new Rectangle(5, 440, 190, 20)
            };
            Label place2 = new()
            {
                Text = "София, България (BG), Европейски съюз (EU)",
                Font = appName.Font,
                Bounds = new Rectangle(10, 460, 500, 20)
            };

            Controls.Add(appName);
            Controls.Add(appName2);
            Controls.Add(appVersion);
            Controls.Add(appVersion2);
            Controls.Add(description);
            Controls.Add(description2);
            Controls.Add(author);
            Controls.Add(authorName);
            Controls.Add(manifacture);
            Controls.Add(manifacture2);
            Controls.Add(place);
            Controls.Add(place2);
        }
    }
}