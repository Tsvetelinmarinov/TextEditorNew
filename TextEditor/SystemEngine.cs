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
        /// <summary>
        /// Creates a new file in the text editor
        /// </summary>
        /// <param name="box">The text editor to work with</param>
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
        /// <summary>
        /// Opens local file in the text editor
        /// </summary>
        /// <param name="box"></param>
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
                using OpenFileDialog fileChooser = new()
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
                };
                if (fileChooser.ShowDialog() == DialogResult.OK)
                {
                    box.Text = File.ReadAllText(fileChooser.FileName);
                }
            }
            else
            {
                using OpenFileDialog fileChooser = new()
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
                };
                if (fileChooser.ShowDialog() == DialogResult.OK)
                {
                    box.Text = File.ReadAllText(fileChooser.FileName);
                }
            }
        }
        /// <summary>
        /// Save current file in the editor to the local system
        /// </summary>
        /// <param name="box"></param>
        public void ExportDataToLocalFile(RichTextBox box)
        {
            if (box.Text != null && box.Text != "")
            {
                using SaveFileDialog exporter = new()
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
                };
                if (exporter.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(exporter.FileName, box.Text);
                }
            }
        }
        /// <summary>
        /// Restart the application
        /// </summary>
        /// <param name="box"></param>
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
        /// <summary>
        /// Quit the application
        /// </summary>
        /// <param name="box"></param>
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
        /// <summary>
        /// Group the menu items from "theme menu"
        /// </summary>
        /// <param name="menus"></param>
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
        /// <summary>
        /// Change to the dark mode
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="boxPanel"></param>
        /// <param name="box"></param>
        /// <param name="menuBar"></param>
        /// <param name="menus"></param>
        public void ChangeToDarkMode(ApplicationGUI editor, Panel boxPanel, RichTextBox box, MenuStrip menuBar, ToolStripMenuItem[] menus)
        {           
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
        /// <summary>
        /// Change to the origina blue mode
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="box"></param>
        /// <param name="boxPanel"></param>
        /// <param name="menuBar"></param>
        /// <param name="menus"></param>
        public void ChangeToBlueMode(ApplicationGUI editor, RichTextBox box, Panel boxPanel, MenuStrip menuBar, ToolStripMenuItem[] menus)
        {
            editor.BackColor = Color.GhostWhite;
            box.TextChanged += (sender, eventArgs) => { MatchKeywords(editor.editor); };
            boxPanel.BackColor = Color.FromArgb(140, 80, 80, 200);
            menuBar.BackColor = Color.FromArgb(20, 10, 50, 220);
            menuBar.Renderer = new MenuBackgroundRender();
            box.BackColor = Color.White;
            box.ForeColor = Color.FromArgb(255, 50, 50, 50);

            foreach (var menu in menus)
            {
                menu.BackColor = Color.GhostWhite;
                menu.ForeColor = Color.Black;
            }
        }      
        /// <summary>
       /// Changle to the ligth mode
       /// </summary>
       /// <param name="editor"></param>
       /// <param name="box"></param>
       /// <param name="boxPanel"></param>
       /// <param name="menuBar"></param>
       /// <param name="menus"></param>
        public void ChangeToLightMode(ApplicationGUI editor, RichTextBox box, Panel boxPanel, MenuStrip menuBar, ToolStripMenuItem[] menus)
        {
            editor.BackColor = Color.White;
            box.TextChanged += (sender, eventArgs) => { MatchKeywords(editor.editor); };
            boxPanel.BackColor = Color.FromArgb(60, 100, 60, 100);
            box.BackColor = Color.GhostWhite;
            box.ForeColor = Color.FromArgb(60, 60, 60);
            menuBar.BackColor = Color.AliceBlue;
            menuBar.Renderer = new LightModeMenuBackgroundRenderer();

            foreach (ToolStripMenuItem menu in menus)
            {
                menu.BackColor = editor.BackColor;
                menu.ForeColor = Color.Black;
            }
        }     
        /// <summary>
        /// Set the blue mode icons of the menus
        /// </summary>
        /// <param name="menus"></param>
        public void SetBlueModeIcons(ToolStripMenuItem[] menus)
        {         
            menus[0].Image = Properties.Resources.newFile;
            menus[1].Image = Properties.Resources.newWindow;
            menus[2].Image = Properties.Resources.open;
            menus[3].Image = Properties.Resources.save;
            menus[4].Image = Properties.Resources.restart;
            menus[5].Image = Properties.Resources.exit;
            menus[6].Image = Properties.Resources.undo;
            menus[7].Image = Properties.Resources.redo;
            menus[8].Image = Properties.Resources.select;
            menus[9].Image = Properties.Resources.cut;
            menus[10].Image = Properties.Resources.copy;
            menus[11].Image = Properties.Resources.paste;
            menus[12].Image = Properties.Resources.delete;
            menus[13].Image = Properties.Resources.uppercase;
            menus[14].Image = Properties.Resources.lowercase;
            menus[15].Image = Properties.Resources.markSelection;
            menus[16].Image = Properties.Resources.deselect;
            menus[17].Image = Properties.Resources.theme;
            menus[18].Image = Properties.Resources.fontAndColor;
            menus[19].Image = Properties.Resources.infoMenu;
        }      
        /// <summary>
        /// Set the ligth mode icons of the menus
        /// </summary>
        /// <param name="menus"></param>
        public void SetLightModeIcons(ToolStripMenuItem[] menus)
        {
            menus[0].Image = Properties.Resources.newFile;
            menus[1].Image = Properties.Resources.newWindow;
            menus[2].Image = Properties.Resources.open;
            menus[3].Image = Properties.Resources.save;
            menus[4].Image = Properties.Resources.restart;
            menus[5].Image = Properties.Resources.exit;
            menus[6].Image = Properties.Resources.undo;
            menus[7].Image = Properties.Resources.redo;
            menus[8].Image = Properties.Resources.select;
            menus[9].Image = Properties.Resources.cut;
            menus[10].Image = Properties.Resources.copy;
            menus[11].Image = Properties.Resources.paste;
            menus[12].Image = Properties.Resources.delete;
            menus[13].Image = Properties.Resources.uppercase;
            menus[14].Image = Properties.Resources.lowercase;
            menus[15].Image = Properties.Resources.markSelection;
            menus[16].Image = Properties.Resources.deselect;
            menus[17].Image = Properties.Resources.theme;
            menus[18].Image = Properties.Resources.fontAndColor;
            menus[19].Image = Properties.Resources.infoMenu;
        }       
        /// <summary>
        /// Set the dark mode icons of the menus
        /// </summary>
        /// <param name="menus"></param>
        public void SetDarkModeIcons(ToolStripMenuItem[] menus)
        {
            menus[0].Image = Properties.Resources.newFileDark;
            menus[1].Image = Properties.Resources.newWindowDark;
            menus[2].Image = Properties.Resources.openDark;
            menus[3].Image = Properties.Resources.saveDark;
            menus[4].Image = Properties.Resources.restartDark;
            menus[5].Image = Properties.Resources.exitDark;
            menus[6].Image = Properties.Resources.undoDark;
            menus[7].Image = Properties.Resources.redoDark;
            menus[8].Image = Properties.Resources.selectDark;
            menus[9].Image = Properties.Resources.cutDark;
            menus[10].Image = Properties.Resources.copyDark;
            menus[11].Image = Properties.Resources.pasteDark;
            menus[12].Image = Properties.Resources.deleteDark;
            menus[13].Image = Properties.Resources.uppercaseDark;
            menus[14].Image = Properties.Resources.lowercaseDark;
            menus[15].Image = Properties.Resources.markSelectionDark;
            menus[16].Image = Properties.Resources.deselectDark;
            menus[17].Image = Properties.Resources.themeDark;
            menus[18].Image = Properties.Resources.fontAndColorDark;
            menus[19].Image = Properties.Resources.infoDark;
        }     
        /// <summary>
        /// Paint the keywords, strings, numbers, special symbols and methods
        /// in the blue mode and the ligth mode
        /// </summary>
        /// <param name="editor"></param>
        public void MatchKeywords(RichTextBox editor)
        {
            // Регулярен израз за всички C# ключови думи (цели думи)
            string keywordsPattern = @"\b(abstract|as|base|bool|break|byte|case|catch|char|
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
        /// <summary>
        /// Paint the keywords, strings, numbers, special symbols and methods
        /// in the dark mode
        /// </summary>
        /// <param name="editor"></param>
        public void MatchKeywordsDark(RichTextBox editor)
        {
            // Шаблон за всички C# ключови думи (цели думи)
            string keywordsPattern = @"\b(abstract|as|base|bool|break|byte|case|catch|char|
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
            // Спиране на обновяването на редактора за по-добра производителност
            editor.SuspendLayout(); 
            // Оцвети целия текст в стандартен цвят
            editor.SelectAll();
            editor.SelectionColor = Color.GhostWhite;

            // Оцвети стринговете
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
        /// <summary>
        /// Compile the text in the text box and run it with csc.exe(The C# compiler)
        /// in the system terminal or power shell
        /// </summary>
        /// <param name="editor"></param>
        public void CompileAndRun(RichTextBox editor)
        {
            // 1. Запиши кода във временен файл
            string tempPath = Path.Combine(Path.GetTempPath(), "buffer.cs");
            File.WriteAllText(tempPath, editor.Text.Replace("\n", "\r\n"));
            // 2. Компилирай с csc.exe
            string cscPath = Path.Combine(
                System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(),
                "csc.exe"
            );
            string exePath = Path.Combine(Path.GetTempPath(), "application.exe");
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
                    "Грешка по време на компилация:  " + error, 
                    "Текстов редактор",
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