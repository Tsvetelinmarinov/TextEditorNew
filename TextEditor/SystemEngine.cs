/**
 * Text Editor++
 * 
 * SystemEngine - A class that provides system-level functionality for the text editor.
 * All methods and voids for system-level functionality are defined here.
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
        public void ChangeToDarkMode(
            ApplicationGUI editor, RichTextBox box, Panel boxPanel, MenuStrip menuBar, ToolStripMenuItem menu1,
            ToolStripMenuItem menu2, ToolStripMenuItem menu3, ToolStripMenuItem menu4, ToolStripMenuItem menu5,
            ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8, ToolStripMenuItem menu9,
            ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12, ToolStripMenuItem menu13,
            ToolStripMenuItem menu14, ToolStripMenuItem menu15, ToolStripMenuItem menu16, ToolStripMenuItem menu17,
            ToolStripMenuItem menu18, ToolStripMenuItem menu19, ToolStripMenuItem menu20, ToolStripMenuItem menu21,
            ToolStripMenuItem menu22, ToolStripMenuItem menu23, ToolStripMenuItem menu24, ToolStripMenuItem menu25,
            ToolStripMenuItem menu26, ToolStripMenuItem menu27
        )
        {

            ToolStripMenuItem[] menus = new ToolStripMenuItem[27];
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
            menus[22] = menu23;
            menus[23] = menu24;
            menus[24] = menu25;
            menus[25] = menu26;
            menus[26] = menu27;

            editor.BackColor = Color.FromArgb(35, 35, 35);
            box.TextChanged += (sender, eventArgs) => { MatchKeywordsDark(editor.editor); };
            boxPanel.BackColor = Color.FromArgb(25, 25, 25);

            box.BackColor = Color.FromArgb(25, 25, 25);
            box.ForeColor = Color.GhostWhite;
            box.BorderStyle = BorderStyle.None;

            menuBar.BackColor = Color.FromArgb(255, 21, 21, 21);
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
        public void ChangeToBlueMode(
            ApplicationGUI editor,
            RichTextBox box,
            Panel boxPanel,
            MenuStrip menuBar,
            ToolStripMenuItem menu1,
            ToolStripMenuItem menu2,
            ToolStripMenuItem menu3,
            ToolStripMenuItem menu4,
            ToolStripMenuItem menu5,
            ToolStripMenuItem menu6,
            ToolStripMenuItem menu7,
            ToolStripMenuItem menu8,
            ToolStripMenuItem menu9,
            ToolStripMenuItem menu10,
            ToolStripMenuItem menu11,
            ToolStripMenuItem menu12,
            ToolStripMenuItem menu13,
            ToolStripMenuItem menu14,
            ToolStripMenuItem menu15,
            ToolStripMenuItem menu16,
            ToolStripMenuItem menu17,
            ToolStripMenuItem menu18,
            ToolStripMenuItem menu19,
            ToolStripMenuItem menu20,
            ToolStripMenuItem menu21,
            ToolStripMenuItem menu22,
            ToolStripMenuItem menu23,
            ToolStripMenuItem menu24,
            ToolStripMenuItem menu25,
            ToolStripMenuItem menu26,
            ToolStripMenuItem menu27
        )
        {

            editor.BackColor = Color.GhostWhite;
            box.TextChanged += (sender, eventArgs) => { MatchKeywords(editor.editor); };
            boxPanel.BackColor = Color.FromArgb(140, 80, 80, 200);

            menuBar.BackColor = Color.FromArgb(20, 10, 50, 220);
            menuBar.Renderer = new MenuBackgroundRender();

            box.BackColor = Color.White;
            box.ForeColor = Color.FromArgb(255, 50, 50, 50);

            ToolStripMenuItem[] menus =
            {
                menu1, menu2, menu3, menu4, menu5, menu6, menu7, menu8, 
                menu9, menu10, menu11, menu12, menu13, menu14, menu15, 
                menu16, menu17, menu18, menu19, menu20, menu21, menu22, 
                menu23, menu24, menu25, menu26, menu27
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
        public void ChangeToLightMode(
            ApplicationGUI editor, RichTextBox box, Panel boxPanel, MenuStrip menuBar, ToolStripMenuItem menu1, 
            ToolStripMenuItem menu2, ToolStripMenuItem menu3, ToolStripMenuItem menu4, ToolStripMenuItem menu5, 
            ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8,  ToolStripMenuItem menu9,
            ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12, ToolStripMenuItem menu13,
            ToolStripMenuItem menu14, ToolStripMenuItem menu15, ToolStripMenuItem menu16, ToolStripMenuItem menu17,
            ToolStripMenuItem menu18, ToolStripMenuItem menu19, ToolStripMenuItem menu20, ToolStripMenuItem menu21,
            ToolStripMenuItem menu22, ToolStripMenuItem menu23,ToolStripMenuItem menu24, ToolStripMenuItem menu25,
            ToolStripMenuItem menu26, ToolStripMenuItem menu27
        )
        {

            editor.BackColor = Color.White;
            box.TextChanged += (sender, eventArgs) => { MatchKeywords(editor.editor); };
            boxPanel.BackColor = Color.FromArgb(60, 100, 60, 100);

            box.BackColor = Color.GhostWhite;
            box.ForeColor = Color.FromArgb(60, 60, 60);

            menuBar.BackColor = Color.AliceBlue;
            menuBar.Renderer = new LightModeMenuBackgroundRenderer();

            ToolStripMenuItem[] menus = 
            {
                menu1, menu2, menu3, menu4, menu5, menu6, menu7, menu8, menu9,
                menu10, menu11, menu12, menu13, menu14, menu15, menu16,
                menu17, menu18, menu19, menu20, menu21, menu22, menu23,
                menu24, menu25, menu26, menu27
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
        public void SetBlueModeIcons(
            ToolStripMenuItem menu1, ToolStripMenuItem menu2, ToolStripMenuItem menu3, ToolStripMenuItem menu4,
            ToolStripMenuItem menu5, ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8,
            ToolStripMenuItem menu9, ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12,
            ToolStripMenuItem menu13, ToolStripMenuItem menu14, ToolStripMenuItem menu15, ToolStripMenuItem menu16,
            ToolStripMenuItem menu17, ToolStripMenuItem menu18, ToolStripMenuItem menu19, ToolStripMenuItem menu20
        )
        {

            menu1.Image = Properties.Resources.newFile;
            menu2.Image = Properties.Resources.newWindow;
            menu3.Image = Properties.Resources.open;
            menu4.Image = Properties.Resources.save;
            menu5.Image = Properties.Resources.restart;
            menu6.Image = Properties.Resources.exit;
            menu7.Image = Properties.Resources.undo;
            menu8.Image = Properties.Resources.redo;
            menu9.Image = Properties.Resources.select;
            menu10.Image = Properties.Resources.cut;
            menu11.Image = Properties.Resources.copy;
            menu12.Image = Properties.Resources.paste;
            menu13.Image = Properties.Resources.delete;
            menu14.Image = Properties.Resources.uppercase;
            menu15.Image = Properties.Resources.lowercase;
            menu16.Image = Properties.Resources.markSelection;
            menu17.Image = Properties.Resources.deselect;
            menu18.Image = Properties.Resources.theme;
            menu19.Image = Properties.Resources.fontAndColor;
            menu20.Image = Properties.Resources.infoMenu;

        }


        /**
         * Change the icons for the light mode
         */
        public void SetLightModeIcons(
            ToolStripMenuItem menu1, ToolStripMenuItem menu2, ToolStripMenuItem menu3, ToolStripMenuItem menu4,
            ToolStripMenuItem menu5, ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8,
            ToolStripMenuItem menu9, ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12,
            ToolStripMenuItem menu13, ToolStripMenuItem menu14, ToolStripMenuItem menu15, ToolStripMenuItem menu16,
            ToolStripMenuItem menu17, ToolStripMenuItem menu18, ToolStripMenuItem menu19, ToolStripMenuItem menu20
        )
        {

            menu1.Image = Properties.Resources.newFile;
            menu2.Image = Properties.Resources.newWindow;
            menu3.Image = Properties.Resources.open;
            menu4.Image = Properties.Resources.save;
            menu5.Image = Properties.Resources.restart;
            menu6.Image = Properties.Resources.exit;
            menu7.Image = Properties.Resources.undo;
            menu8.Image = Properties.Resources.redo;
            menu9.Image = Properties.Resources.select;
            menu10.Image = Properties.Resources.cut;
            menu11.Image = Properties.Resources.copy;
            menu12.Image = Properties.Resources.paste;
            menu13.Image = Properties.Resources.delete;
            menu14.Image = Properties.Resources.uppercase;
            menu15.Image = Properties.Resources.lowercase;
            menu16.Image = Properties.Resources.markSelection;
            menu17.Image = Properties.Resources.deselect;
            menu18.Image = Properties.Resources.theme;
            menu19.Image = Properties.Resources.fontAndColor;
            menu20.Image = Properties.Resources.infoMenu;

        }


        /**
         * Change the icons for the dark mode
         */
        public void SetDarkModeIcons(
            ToolStripMenuItem menu1, ToolStripMenuItem menu2, ToolStripMenuItem menu3, ToolStripMenuItem menu4,
            ToolStripMenuItem menu5, ToolStripMenuItem menu6, ToolStripMenuItem menu7, ToolStripMenuItem menu8, 
            ToolStripMenuItem menu9, ToolStripMenuItem menu10, ToolStripMenuItem menu11, ToolStripMenuItem menu12,
            ToolStripMenuItem menu13, ToolStripMenuItem menu14, ToolStripMenuItem menu15, ToolStripMenuItem menu16,
            ToolStripMenuItem menu17, ToolStripMenuItem menu18, ToolStripMenuItem menu19, ToolStripMenuItem menu20
        )
        {

            menu1.Image = Properties.Resources.newFileDark;
            menu2.Image = Properties.Resources.newWindowDark;
            menu3.Image = Properties.Resources.openDark;
            menu4.Image = Properties.Resources.saveDark;
            menu5.Image = Properties.Resources.restartDark;
            menu6.Image = Properties.Resources.exitDark;
            menu7.Image = Properties.Resources.undoDark;
            menu8.Image = Properties.Resources.redoDark;
            menu9.Image = Properties.Resources.selectDark;
            menu10.Image = Properties.Resources.cutDark;
            menu11.Image = Properties.Resources.copyDark;
            menu12.Image = Properties.Resources.pasteDark;
            menu13.Image = Properties.Resources.deleteDark;
            menu14.Image = Properties.Resources.uppercaseDark;
            menu15.Image = Properties.Resources.lowercaseDark;
            menu16.Image = Properties.Resources.markSelectionDark;
            menu17.Image = Properties.Resources.deselectDark;
            menu18.Image = Properties.Resources.themeDark;
            menu19.Image = Properties.Resources.fontAndColorDark;
            menu20.Image = Properties.Resources.infoDark;

        }


        /**
         * Coloring the keywords in the text editor for the light and the blue mode
         */
        public void MatchKeywords(RichTextBox editor)
        {

            // Регулярен израз за всички C# ключови думи (цели думи)
            string keywordsPattern =
                @"\b(abstract|as|base|bool|break|byte|case|catch|char|
                checked|class|const|continue|decimal|default|delegate|
                do|double|else|enum|event|explicit|extern|false|finally|
                fixed|float|for|foreach|goto|if|implicit|internal|in|int|interface|
                is|lock|long|namespace|new|null|object|operator|out|
                override|params|private|protected|public|readonly|ref|return|
                sbyte|sealed|short|sizeof|stackalloc|static|string|struct|
                switch|this|throw|true|try|typeof|uint|ulong|unchecked|
                unsafe|ushort|using|virtual|void|volatile|while)\b";

            //Запазване на позицията текущата селекция
            int selectionStart = editor.SelectionStart;
            int selectionLength = editor.SelectionLength;

            editor.SuspendLayout(); // Спиране на обновяването на редактора за по-добра производителност

            // Оцвети целия текст в стандартен цвят
            editor.SelectAll();
            editor.SelectionColor = Color.FromArgb(100, 40, 40, 40);

            // Оцвети стринговете първо в кафяво
            foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
            {
                editor.Select(str.Index, str.Length);
                editor.SelectionColor = Color.Brown; // или друг цвят за стрингове
            }

            // Оцвети числата в червено
            foreach (Match number in Regex.Matches(editor.Text, @"[0-9]"))
            {
                editor.Select(number.Index, number.Length);
                editor.SelectionColor = Color.SaddleBrown; // или друг цвят за числа
            }


            // Оцвети ключовите думи, които не са в стринг
            foreach (Match match in Regex.Matches(editor.Text, keywordsPattern))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (match.Index >= str.Index && match.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(match.Index, match.Length);
                    editor.SelectionColor = Color.Blue;
                }
            }

            // Оцвети имената на методи, които не са в стринг
            foreach (Match match in Regex.Matches(editor.Text, @"([A-Za-z_][A-Za-z0-9_]*)(?=\()"))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (match.Index >= str.Index && match.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(match.Index, match.Length);
                    editor.SelectionColor = Color.FromArgb(116, 83, 31);
                }
            }


            // Оцветяване на знаците за математични операции
            foreach (Match op in Regex.Matches(editor.Text, @"[+\-*/=<>!&\$\@\^|^%]"))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (op.Index >= str.Index && op.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(op.Index, op.Length);
                    editor.SelectionColor = Color.DarkGreen;
                }
            }

            // Оцветяване на [ и ] знаци
            foreach (Match bracket in Regex.Matches(editor.Text, @"[\[\]]"))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (bracket.Index >= str.Index && bracket.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(bracket.Index, bracket.Length);
                    editor.SelectionColor = Color.DarkOrange;
                }
            }

            // Оцветяване на имена на променливи
            foreach (Match variable in Regex.Matches(editor.Text, @"\b([a-zA-Z_][a-zA-Z0-9_]*)(?=\.)\b"))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (variable.Index >= str.Index && variable.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(variable.Index, variable.Length);
                    editor.SelectionColor = Color.Teal;
                }
            }

            // Коменари
            foreach (Match comment in Regex.Matches(editor.Text, @"(\/\/.*?\/\/|\/\*[\s\S]*?\*\/)")) 
            {
                editor.Select(comment.Index, comment.Length);
                editor.SelectionColor = Color.DarkGreen;
            }

            // Възстанови селекцията в ървоначалното ѝ състояние
            editor.Select(selectionStart, selectionLength);
            editor.SelectionColor = Color.FromArgb(100, 40, 40, 40);

            editor.ResumeLayout(); // Възобновяване на обновяването на редактора

        }


        /**
         *  Coloring the keywords on the dark mode 
         */
        public void MatchKeywordsDark(RichTextBox editor)
        {

            // Шаблон за всички C# ключови думи (цели думи)
            string keywordsPattern =
                @"\b(abstract|as|base|bool|break|byte|case|catch|char|
                checked|class|const|continue|decimal|default|delegate|
                do|double|else|enum|event|explicit|extern|false|finally|
                fixed|float|for|foreach|goto|if|implicit|internal|in|int|interface|
                is|lock|long|namespace|new|null|object|operator|out|
                override|params|private|protected|public|readonly|ref|return|
                sbyte|sealed|short|sizeof|stackalloc|static|string|struct|
                switch|this|throw|true|try|typeof|uint|ulong|unchecked|
                unsafe|ushort|using|virtual|void|volatile|while)\b";

            // Запазване на позицията на текущата селекция
            int selectionStart = editor.SelectionStart;
            int selectionLength = editor.SelectionLength;

            editor.SuspendLayout(); // Спиране на обновяването на редактора за по-добра производителност

            // Оцвети целия текст в стандартен цвят
            editor.SelectAll();
            editor.SelectionColor = Color.GhostWhite;

            // Оцвети стринговете първо
            foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
            {
                editor.Select(str.Index, str.Length);
                editor.SelectionColor = Color.RosyBrown;
            }

            // Оцвети числата в червено
            foreach (Match number in Regex.Matches(editor.Text, @"[0-9]"))
            {
                editor.Select(number.Index, number.Length);
                editor.SelectionColor = Color.PaleVioletRed;
            }

            // Оцвети ключовите думи, които не са в стринг
            foreach (Match match in Regex.Matches(editor.Text, keywordsPattern))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (match.Index >= str.Index && match.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(match.Index, match.Length);
                    editor.SelectionColor = Color.FromArgb(86, 156, 214);
                }
            }

            // Оцвети имената на методи, които не са в стринг
            foreach (Match match in Regex.Matches(editor.Text, @"\b([A-Za-z_][A-Za-z0-9_]*)(?=\()"))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (match.Index >= str.Index && match.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(match.Index, match.Length);
                    editor.SelectionColor = Color.FromArgb(220, 220, 170);
                }
            }

            // Оцветяване на знаците за математични операции
            foreach (Match op in Regex.Matches(editor.Text, @"[+\-*/=<>!&\$\@\^|^%]"))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (op.Index >= str.Index && op.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(op.Index, op.Length);
                    editor.SelectionColor = Color.PaleVioletRed;
                }
            }

            // Оцветяване на [ и ] знаци
            foreach (Match bracket in Regex.Matches(editor.Text, @"[\[\]]"))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (bracket.Index >= str.Index && bracket.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(bracket.Index, bracket.Length);
                    editor.SelectionColor = Color.DarkOrange;
                }
            }

            // Оцветяване на имена на променливи и класове
            foreach (Match variable in Regex.Matches(editor.Text, @"\b([a-zA-Z_][a-zA-Z0-9_]*)(?=\.)\b"))
            {
                // Проверка дали не е в стринг
                bool inString = false;
                foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                {
                    if (variable.Index >= str.Index && variable.Index < str.Index + str.Length)
                    {
                        inString = true;
                        break;
                    }
                }
                if (!inString)
                {
                    editor.Select(variable.Index, variable.Length);
                    editor.SelectionColor = Color.FromArgb(78, 201, 176);
                }
            }

            // Коменари
            foreach (Match comment in Regex.Matches(editor.Text, @"\/\/.*?\/\/|\/\*[\s\S]*?\*\/"))
            {
                editor.Select(comment.Index, comment.Length);
                editor.SelectionColor = Color.FromArgb(20, 142, 10);
            }

            // Възстанови селекцията
            editor.Select(selectionStart, selectionLength);
            editor.SelectionColor = Color.GhostWhite;

            editor.ResumeLayout(); // Възобновяване на обновяването на редактора

        }


        /**
         * Compile and run the code in the editor
         */
        public void CompileAndRun(RichTextBox editor)
        {

            // 1. Запиши кода във временен файл
            string tempPath = Path.Combine(Path.GetTempPath(), "TempEditorCode.cs");
            File.WriteAllText(tempPath, editor.Text.Replace("\n", "\r\n"));

            // 2. Компилирай с csc.exe
            string cscPath = Path.Combine(
                System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(),
                "csc.exe"
            );
            string exePath = Path.Combine(Path.GetTempPath(), "TempEditorCode.exe");

            Process compileProcess = new();
            compileProcess.StartInfo.FileName = cscPath;
            compileProcess.StartInfo.Arguments = $"/t:exe /out:\"{exePath}\" \"{tempPath}\"";
            compileProcess.StartInfo.CreateNoWindow = true;
            compileProcess.StartInfo.UseShellExecute = false;
            compileProcess.StartInfo.RedirectStandardOutput = true;
            compileProcess.StartInfo.RedirectStandardError = true;
            compileProcess.Start();
            _ = compileProcess.StandardOutput.ReadToEnd();
            string error = compileProcess.StandardError.ReadToEnd();
            compileProcess.WaitForExit();

            if (compileProcess.ExitCode != 0)
            {
                MessageBox.Show(
                    "Compile error:\n" + error, 
                    "C# Compile Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                
                return;
            }

            // 3. Стартирай exe файла в терминал
            Process runProcess = new();
            runProcess.StartInfo.FileName = "cmd.exe";
            runProcess.StartInfo.Arguments = $"/K \"\"{exePath}\"\"";
            runProcess.Start();

        }

    }

}