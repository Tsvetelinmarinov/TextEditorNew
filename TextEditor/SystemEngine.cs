/**
 * Text Editor++
 * 
 * SystemEngine - A class that provides system-level functionality for the text editor.
 * All methods and voids for system-level functionality are defined here.
 */

using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace TextEditor
{

    internal class SystemEngine
    {

        //Create a new file
        public void CreateNewFile(RichTextBox box)
        {

            if 
            (
                box.Text != null && 
                box.Text != "" &&
                MessageBox.Show
                (
                    "Незапазените данни ще бъдат загубени. Искате ли да продължите?",
                    "Текстов редактор",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                )
                == DialogResult.Yes
            )
            {
                box.Clear();
            }

        }


        /**
         * Open a file
         */
        public void LoadDataFromFile(RichTextBox box)
        {

            if 
            (   
               box.Text != null && 
               box.Text != "" &&
               MessageBox.Show
               (
                   "Незапазените данни ще бъдат загубени. Искате ли да продължите?",
                   "Текстов редактор",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question
               )
               == DialogResult.Yes
            )
            {

                using
                (
                    OpenFileDialog fileChooser = new OpenFileDialog()
                    {
                        Title = "Отваряне на файл",
                        Filter =
                        "Text Files (*.txt)|*.txt|" +
                        "All Files (*.*)|*.*|" +
                        "C# Files (*.cs)|*.cs|" +
                        "C++ Files (*.cpp)|*.cpp|" +
                        "HTML document (*.html)|*.html|" +
                        "Stylesheet (*.css)|*.css|" +
                        "JavaScript Files (*.js)|*.js",
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                        Multiselect = true,
                        DefaultExt = "txt"
                    }
                )
                {

                    if (fileChooser.ShowDialog() == DialogResult.OK)
                    {
                        box.Text = File.ReadAllText(fileChooser.FileName);
                    }

                }

            }
            else
            {
                using 
                (
                    OpenFileDialog fileChooser = new OpenFileDialog()
                    {
                        Title = "Отваряне на файл",
                        Filter = 
                        "All Files (*.*)|*.*|" +
                        "Text Files (*.txt)|*.txt|" +
                        "C# Files (*.cs)|*.cs|" +
                        "C++ Files (*.cpp)|*.cpp|" +
                        "HTML document (*.html)|*.html|" +
                        "Stylesheet (*.css)|*.css|" +
                        "JavaScript Files (*.js)|*.js",
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                        DefaultExt = "txt",
                        Multiselect = true
                    }
                )
                {

                    if (fileChooser.ShowDialog() == DialogResult.OK)
                    {
                        box.Text = File.ReadAllText(fileChooser.FileName);
                    }

                }
            }

        }


        /**
         * Save the current file in the editor
         */
        public void ExportDataToLocalFile(RichTextBox box)
        {

            if (box.Text != null && box.Text != "")
            {

                using 
                (
                    SaveFileDialog exporter = new SaveFileDialog()
                    {
                        Title = "Запазване на файл",
                        Filter = 
                        "Text Files (*.txt)|*.txt|" +
                        "All Files (*.*)|*.*|" +
                        "C# Files (*.cs)|*.cs|" +
                        "C++ Files (*.cpp)|*.cpp|" +
                        "HTML document (*.html)|*.html|" +
                        "Stylesheet (*.css)|*.css|" +
                        "JavaScript Files (*.js)|*.js|" +
                        "C Files (*.c)|*.c",
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        DefaultExt = "txt"
                    }
                )
                {

                    if (exporter.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(exporter.FileName, box.Text);
                    }

                }

            }

        }


        /**
         *  Restart the application
         */
        public void Reboot(RichTextBox box)
        {

            if (box.Text != null && box.Text != "")
            {

                if 
                (
                    MessageBox.Show
                    (
                        "Незапазените данни ще бъдат загубени. Искате ли да продължите?",
                        "Текстов редактор",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    )
                    == DialogResult.Yes
                )
                {

                    Application.Restart();

                }

            }
            else
            {
                Application.Restart();
            }

        }


        /**
         * Quit the application
         */
        public void Quit(RichTextBox box)
        {

            bool hasText = box.Text != null && box.Text != "";
            
            if (hasText)
            {

                if 
                (
                    MessageBox.Show
                    (
                        "Незапазените данни ще бъдат загубени. Искате ли да продължите?",
                        "Текстов редактор",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    )
                    == DialogResult.Yes
                )
                {

                    Application.Exit();
                    Environment.Exit(0);

                }

            }
            else
            {
                Application.Exit();
                Environment.Exit(0);
            }

        }


        /**
         * Group all menus from appearance menu
         */
        public void GroupMenuItems(ToolStripMenuItem[] menus)
        {

            bool hasChecked = false;

            foreach (ToolStripMenuItem menu in menus)
            {

                if (menu.Checked)
                {
                    hasChecked = true;
                }

                menu.CheckOnClick = true;
                menu.Click += (sender, eventArgs) =>
                {
                    foreach (ToolStripMenuItem otherMenu in menus) 
                    {
                        otherMenu.Checked = false;
                    }

                    (sender as ToolStripMenuItem).Checked = true;
                };

            }

            if (!hasChecked && menus.Length > 0)
            {
                menus[0].Checked = true;
            }

        }


        /**
         * Change to dark mode
         */
        public void ChangeToDarkMode
        (
            ApplicationGUI editor, RichTextBox box, MenuStrip menuBar, ToolStripMenuItem menu1,
            ToolStripMenuItem menu2, ToolStripMenuItem menu3, ToolStripMenuItem menu4, ToolStripMenuItem menu5,
            ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8, ToolStripMenuItem menu9,
            ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12, ToolStripMenuItem menu13,
            ToolStripMenuItem menu14, ToolStripMenuItem menu15, ToolStripMenuItem menu16, ToolStripMenuItem menu17,
            ToolStripMenuItem menu18,  ToolStripMenuItem menu19, ToolStripMenuItem menu20, ToolStripMenuItem menu21,ToolStripMenuItem menu22
        )
        {

            ToolStripMenuItem[] menus = new ToolStripMenuItem[22];
            menus[0] = menu1;
            menus[1] = menu2;
            menus[2] = menu3;
            menus[3] = menu4;
            menus[4] = menu5;
            menus[5] = menu6;
            menus[6] = menu7;
            menus[7] = menu8;
            menus[8] = menu9;
            menus[9] = menu10;
            menus[10] = menu11;
            menus[11] = menu12;
            menus[12] = menu13;
            menus[13] = menu14;
            menus[14] = menu15;
            menus[15] = menu16;
            menus[16] = menu17;
            menus[17] = menu18;
            menus[18] = menu19;
            menus[19] = menu20;
            menus[20] = menu21;
            menus[21] = menu22;

            editor.BackColor = Color.FromArgb(255, 35, 35, 35);

            box.BackColor = Color.FromArgb(40, 40, 40);
            box.ForeColor = Color.GhostWhite;

            menuBar.BackColor = Color.FromArgb(255, 35, 35, 35);
            menuBar.Renderer = new DarkModeMenuBackgroundRenderer();

            foreach (ToolStripMenuItem menu in menus)
            {
                menu.BackColor = menuBar.BackColor;
                menu.ForeColor = Color.GhostWhite;
            };

        }


        /**
         * Change to original blue mode
         */
        public void ChangeToBlueMode
        (
            ApplicationGUI editor,  RichTextBox box,  MenuStrip menuBar,  ToolStripMenuItem menu1, ToolStripMenuItem menu2, ToolStripMenuItem menu3,
            ToolStripMenuItem menu4, ToolStripMenuItem menu5, ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8,
            ToolStripMenuItem menu9, ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12, ToolStripMenuItem menu13,
            ToolStripMenuItem menu14, ToolStripMenuItem menu15, ToolStripMenuItem menu16, ToolStripMenuItem menu17, ToolStripMenuItem menu18,
            ToolStripMenuItem menu19, ToolStripMenuItem menu20, ToolStripMenuItem menu21, ToolStripMenuItem menu22
        )
        {

            editor.BackColor = Color.GhostWhite;

            menuBar.BackColor = Color.FromArgb(20, 10, 50, 220);
            menuBar.Renderer = new MenuBackgroundRender();

            box.BackColor = Color.White;
            box.ForeColor = Color.FromArgb(255, 50, 50, 50);

            ToolStripMenuItem[] menus =
            {
                menu1, menu2, menu3, menu4, menu5, menu6, menu7, menu8, menu9,
                menu10, menu11, menu12, menu13, menu14, menu15, menu16,
                menu17, menu18, menu19, menu20, menu21, menu22
            };

            foreach (var menu in menus)
            {
                menu.BackColor = Color.GhostWhite;
                menu.ForeColor = Color.Black;
            }

        }


        /**
         * Switch to light mode
         */
        public void ChangeToLightMode
        (
            ApplicationGUI editor, RichTextBox box, MenuStrip menuBar, ToolStripMenuItem menu1, ToolStripMenuItem menu2, ToolStripMenuItem menu3,
            ToolStripMenuItem menu4, ToolStripMenuItem menu5, ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8, 
            ToolStripMenuItem menu9, ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12, ToolStripMenuItem menu13, 
            ToolStripMenuItem menu14, ToolStripMenuItem menu15, ToolStripMenuItem menu16, ToolStripMenuItem menu17, ToolStripMenuItem menu18, 
            ToolStripMenuItem menu19, ToolStripMenuItem menu20, ToolStripMenuItem menu21, ToolStripMenuItem menu22
        )
        {

            editor.BackColor = Color.White;

            box.BackColor = Color.GhostWhite;
            box.ForeColor = Color.FromArgb(60, 60, 60);

            menuBar.BackColor = Color.AliceBlue;
            menuBar.Renderer = new LightModeMenuBackgroundRenderer();

            ToolStripMenuItem[] menus = 
            {
                menu1, menu2, menu3, menu4, menu5, menu6, menu7, menu8, menu9,
                menu10, menu11, menu12, menu13, menu14, menu15, menu16,
                menu17, menu18, menu19, menu20, menu21, menu22
            };

            foreach (ToolStripMenuItem menu in menus)
            {
                menu.BackColor = editor.BackColor;
                menu.ForeColor = Color.Black;
            }
        }


        /**
         * Change the icons for the blue mode
         */
        public void SetBlueModeIcons
        (
            ToolStripMenuItem menu1, ToolStripMenuItem menu2, ToolStripMenuItem menu3, ToolStripMenuItem menu4,
            ToolStripMenuItem menu5, ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8,
            ToolStripMenuItem menu9, ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12,
            ToolStripMenuItem menu13, ToolStripMenuItem menu14, ToolStripMenuItem menu15
        )
        {

            menu1.Image = Properties.Resources.newFile;
            menu2.Image = Properties.Resources.open;
            menu3.Image = Properties.Resources.save;
            menu4.Image = Properties.Resources.restart;
            menu5.Image = Properties.Resources.exit;
            menu6.Image = Properties.Resources.undo;
            menu7.Image = Properties.Resources.redo;
            menu8.Image = Properties.Resources.select;
            menu9.Image = Properties.Resources.cut;
            menu10.Image = Properties.Resources.copy;
            menu11.Image = Properties.Resources.paste;
            menu12.Image = Properties.Resources.delete;
            menu13.Image = Properties.Resources.theme;
            menu14.Image = Properties.Resources.fontAndColor;
            menu15.Image = Properties.Resources.infoMenu;

        }


        /**
         * Change the icons for the light mode
         */
        public void SetLightModeIcons
        (
            ToolStripMenuItem menu1, ToolStripMenuItem menu2, ToolStripMenuItem menu3, ToolStripMenuItem menu4,
            ToolStripMenuItem menu5, ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8,
            ToolStripMenuItem menu9, ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12,
            ToolStripMenuItem menu13, ToolStripMenuItem menu14, ToolStripMenuItem menu15
        )
        {

            menu1.Image = Properties.Resources.newFile;
            menu2.Image = Properties.Resources.open;
            menu3.Image = Properties.Resources.save;
            menu4.Image = Properties.Resources.restart;
            menu5.Image = Properties.Resources.exit;
            menu6.Image = Properties.Resources.undo;
            menu7.Image = Properties.Resources.redo;
            menu8.Image = Properties.Resources.select;
            menu9.Image = Properties.Resources.cut;
            menu10.Image = Properties.Resources.copy;
            menu11.Image = Properties.Resources.paste;
            menu12.Image = Properties.Resources.delete;
            menu13.Image = Properties.Resources.theme;
            menu14.Image = Properties.Resources.fontAndColor;
            menu15.Image = Properties.Resources.infoMenu;

        }


        /**
         * Change the icons for the dark mode
         */
        public void SetDarkModeIcons
        (
            ToolStripMenuItem menu1, ToolStripMenuItem menu2, ToolStripMenuItem menu3, ToolStripMenuItem menu4,
            ToolStripMenuItem menu5, ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8, 
            ToolStripMenuItem menu9, ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12,
            ToolStripMenuItem menu13, ToolStripMenuItem menu14, ToolStripMenuItem menu15
        ) 
        {

            menu1.Image = Properties.Resources.newFileDark;
            menu2.Image = Properties.Resources.openDark;
            menu3.Image = Properties.Resources.saveDark;
            menu4.Image = Properties.Resources.restartDark;
            menu5.Image = Properties.Resources.exitDark;
            menu6.Image = Properties.Resources.undoDark;
            menu7.Image = Properties.Resources.redoDark;
            menu8.Image = Properties.Resources.selectDark;
            menu9.Image = Properties.Resources.cutDark;
            menu10.Image = Properties.Resources.copyDark;
            menu11.Image = Properties.Resources.pasteDark;
            menu12.Image = Properties.Resources.deleteDark;
            menu13.Image = Properties.Resources.themeDark;
            menu14.Image = Properties.Resources.fontAndColorDark;
            menu15.Image = Properties.Resources.infoDark;

        }

    }

}