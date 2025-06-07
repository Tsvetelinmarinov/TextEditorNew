/**
 * Text Editor++
 * 
 * ApplicationGUI - The Graphical User Interface for the application.
 */

using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TextEditor
{
    internal class ApplicationGUI : Form
    {
        //Menu bar for the application 
        private readonly MenuStrip menuBar = new();
        //File menu
        private readonly ToolStripMenuItem fileMenu = new();
        //Edit menu
        private readonly ToolStripMenuItem editMenu = new();
        //Options menu
        private readonly ToolStripMenuItem optionsMenu = new();
        //Help menu
        private readonly ToolStripMenuItem helpMenu = new();
        //menu 'open'
        private readonly ToolStripMenuItem open = new();
        //menu 'save'
        private readonly ToolStripMenuItem save = new();
        //menu 'new file'
        private readonly ToolStripMenuItem newFile = new();
        //menu 'new window'
        private readonly ToolStripMenuItem newWindow = new();
        //menu 'restart'
        private readonly ToolStripMenuItem reboot = new();
        //menu 'exit'
        private readonly ToolStripMenuItem exit = new();
        //menu 'back'
        private readonly ToolStripMenuItem back = new();
        //menu 'next'
        private readonly ToolStripMenuItem next = new();
        //menu 'select all'
        private readonly ToolStripMenuItem selectAll = new();
        //menu 'cut'
        private readonly ToolStripMenuItem cut = new();
        //menu 'copy'
        private readonly ToolStripMenuItem copy = new();
        //menu 'paste'
        private readonly ToolStripMenuItem paste = new();
        //menu 'delete all'
        private readonly ToolStripMenuItem deleteAll = new();
        //Menu 'make uppercase'
        private readonly ToolStripMenuItem uppercase = new();
        //Menu 'lowercase'
        private readonly ToolStripMenuItem lowercase = new();
        //menu 'mark selection'
        private readonly ToolStripMenuItem markSelection = new();
        //menu 'unmark selection'
        private readonly ToolStripMenuItem unmarkSelection = new();
        //menu 'appearance'
        private readonly ToolStripMenuItem appearance = new();
        //menu 'blue mode'
        private readonly ToolStripMenuItem blueMode = new();
        //check menu 'dark'
        private readonly ToolStripMenuItem darkMode = new();
        //check menu 'light'
        private readonly ToolStripMenuItem lightMode = new();
        //Group of the menu 'appearance'
        private readonly ToolStripMenuItem[] colorModeMenus = new ToolStripMenuItem[3];
        //menu "font and color"
        private readonly ToolStripMenuItem fontAndColor = new();
        //menu 'information'
        private readonly ToolStripMenuItem information = new();
        //Text box - the editor
        public RichTextBox editor = new();
        //Text area panel
        private readonly Panel textAreaPanel = new();
        //ToolTip for the menu bar and the menus
        private readonly ToolTip tip = new();
        //Button run
        private readonly PictureBox execute = new();

        /// <summary>
        ///  The Graphical User Interface(GUI) of the application.
        /// </summary>
        /// Constructor for the ApplicationGUI class.
        /// Initializes the form and its components.
        public ApplicationGUI()
        {      
            //Fix the size of the window
            this.Text = "Текстов редактор";
            this.Size = new Size(1800, 930);
            this.Icon = Properties.Resources.icon;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.GhostWhite;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.Shown += (sender, eventArgs) => { darkMode.PerformClick(); };// Set the dark mode default 
            this.KeyPreview = true; // Enable key preview to handle key events in the form
            this.KeyDown += (sender, eventArgs) =>
            {
                if (eventArgs.KeyCode == Keys.F5)
                {
                    new SystemEngine().CompileAndRun(editor);
                    eventArgs.Handled = true;
                }
            };

            //Build the GUI
            BuildGUI();
        }

        private void BuildGUI()
        {   
            BuildMenuBar();
            BuildPanelAndEditor();
            BuildFileMenuAndFileMenuItems();
            BuildTheEditMenuAndTheHalfOfTheEditMenuItems();
            BuildSecondHalfOfTheEditMenuItems();
            BuildOptionsMenuAndTheOptions();
            BuildHelpMenuAndInformationMenuItem();
            BuildExecutionButton();
        }
        private void BuildPanelAndEditor()
       {
            // Setting up the panel for the editor
            textAreaPanel.Bounds = new Rectangle(-1, 55, 1782, 834);
            textAreaPanel.BackColor = Color.FromArgb(140, 80, 80, 200); // Border color
            textAreaPanel.Padding = new Padding(1);// Thickness of the border
            Controls.Add(textAreaPanel);

            // Setting up the text box
            editor.Bounds = new Rectangle(-1, 6, 1782, 834);
            editor.Font = new Font("Cascadia Code", 13);
            editor.ForeColor = Color.FromArgb(50, 50, 50);
            editor.BackColor = Color.White;
            editor.Multiline = true;
            editor.ScrollBars = RichTextBoxScrollBars.Both;
            editor.WordWrap = false;
            editor.AcceptsTab = true;
            //Coloring the keywords in the editor and set the indent
            editor.TextChanged += (sender, eventArgs) =>
            {
                editor.SelectionIndent = 6;
                new SystemEngine().MatchKeywords(editor);
            };
            //Match the cruely brackets in the editor
            editor.KeyPress += (sender, eventArgs) =>
            {
                if (eventArgs.KeyChar == '{')
                {
                    //Запази текущата позиция на курсора
                    int currentCursorPosition = editor.SelectionStart;
                    // Проверка дали не е в стринг
                    bool inString = false;
                    foreach (Match str in Regex.Matches(editor.Text, "\"(?:\\\\.|[^\"])*\""))
                    {
                        if (str.Index >= str.Index && str.Index < str.Index + str.Length)
                        {
                            inString = true;
                            break;
                        }
                    }

                    if (!inString)
                    {
                        // Вмъкни текста на позицията на курсора
                        editor.SelectedText = "\n{\n\n    \n\n}";
                        // Премести курсора между скобите, след отстъпа
                        editor.SelectionStart = currentCursorPosition + 8;
                        eventArgs.Handled = true;
                    }

                }

            };
            //Write four spaces when pressing tab
            editor.KeyDown += (sender, eventArgs) =>
            {
                if (eventArgs.KeyCode == Keys.Tab)
                {
                    editor.SelectedText = "    "; // Вмъкни четири интервала
                    eventArgs.SuppressKeyPress = true; // Прекъсни стандартното поведение на таб
                }
            };
            textAreaPanel.Controls.Add(editor);
        }
        private void BuildMenuBar()
        {
            //Setting up the menu bar
            menuBar.Bounds = new Rectangle(0, 0, this.Width, 20);
            menuBar.BackColor = Color.FromArgb(20, 10, 50, 220);
            menuBar.Cursor = Cursors.Hand;
            menuBar.Renderer = new MenuBackgroundRender();
            this.Controls.Add(menuBar);
        }
        private void BuildFileMenuAndFileMenuItems()
        {
            //Setting up the file menu
            fileMenu.Text = "Файл";
            fileMenu.Font = new Font("Seoge UI", 11);
            fileMenu.BackColor = this.BackColor;
            menuBar.Items.Add(fileMenu);

            //Setting up the new file menu
            newFile.Text = "нов файл";
            newFile.Font = new Font("Seoge UI", 11);
            newFile.Image = Properties.Resources.newFile;
            newFile.Click += (sender, eventArgs) => { new SystemEngine().CreateNewFile(editor); };
            fileMenu.DropDownItems.Add(newFile);

            //menu 'new window'
            newWindow.Text = "нов прозорец";
            newWindow.Font = newFile.Font;
            newWindow.Image = Properties.Resources.newWindow;
            newWindow.Click += (sender, eventArgs) => { new ApplicationGUI().Show(); };
            fileMenu.DropDownItems.Add(newWindow);

            //Setting up the open menu
            open.Text = "отвори";
            open.Font = newFile.Font;
            open.Image = Properties.Resources.open;
            open.Click += (sender, eventArgs) => { new SystemEngine().LoadDataFromFile(editor); };
            fileMenu.DropDownItems.Add(open);

            //Setting up the save menu
            save.Text = "запиши";
            save.Font = newFile.Font;
            save.Image = Properties.Resources.save;
            save.Click += (sender, eventArgs) => { new SystemEngine().ExportDataToLocalFile(editor); };
            fileMenu.DropDownItems.Add(save);

            //Setting up the reboot menu
            reboot.Text = "рестартиране";
            reboot.Font = newFile.Font;
            reboot.Image = Properties.Resources.restart;
            reboot.Click += (sender, eventArgs) => { new SystemEngine().Reboot(editor); };
            fileMenu.DropDownItems.Add(reboot);

            //Setting up the exit menu
            exit.Text = "изход";
            exit.Font = newFile.Font;
            exit.Image = Properties.Resources.exit;
            exit.Click += (sender, eventArgs) => { new SystemEngine().Quit(editor); };
            fileMenu.DropDownItems.Add(exit);
        }
        private void BuildTheEditMenuAndTheHalfOfTheEditMenuItems()
        {
            //Setting up the edit menu
            editMenu.Text = "Редакция";
            editMenu.Font = fileMenu.Font;
            editMenu.BackColor = fileMenu.BackColor;
            menuBar.Items.Add(editMenu);

            //Setting up the undo menu
            back.Text = "предишна стъпка";
            back.Font = newFile.Font;
            back.Image = Properties.Resources.undo;
            back.Click += (sender, eventArgs) => { editor.Undo(); };
            editMenu.DropDownItems.Add(back);

            //Setting up the redo menu
            next.Text = "предходна стъпка";
            next.Font = newFile.Font;
            next.Image = Properties.Resources.redo;
            next.Click += (sender, eventArgs) => { editor.Redo(); };
            editMenu.DropDownItems.Add(next);

            //Setting up the select all menu
            selectAll.Text = "селектирай всичко";
            selectAll.Font = newFile.Font;
            selectAll.Image = Properties.Resources.select;
            selectAll.Click += (sender, eventArgs) => { editor.SelectAll(); };
            editMenu.DropDownItems.Add(selectAll);

            //Setting up the cut menu
            cut.Text = "изрежи";
            cut.Font = newFile.Font;
            cut.Image = Properties.Resources.cut;
            cut.Click += (sender, eventArgs) => { editor.Cut(); };
            editMenu.DropDownItems.Add(cut);

            //Setting up the copy menu
            copy.Text = "копиране";
            copy.Font = newFile.Font;
            copy.Image = Properties.Resources.copy;
            copy.Click += (sender, eventArgs) => { editor.Copy(); };
            editMenu.DropDownItems.Add(copy);
        }
        private void BuildSecondHalfOfTheEditMenuItems()
        {
            //Setting up the paste menu
            paste.Text = "поставяне";
            paste.Font = newFile.Font;
            paste.Image = Properties.Resources.paste;
            paste.Click += (sender, eventArgs) => { editor.Paste(); };
            editMenu.DropDownItems.Add(paste);

            //Setting up the delete all menu
            deleteAll.Text = "изтриване на всичко";
            deleteAll.Font = newFile.Font;
            deleteAll.Image = Properties.Resources.delete;
            deleteAll.Click += (sender, eventArgs) => { editor.Clear(); };
            editMenu.DropDownItems.Add(deleteAll);

            //menu 'uppercase'
            uppercase.Text = "само главни букви";
            uppercase.Font = newFile.Font;
            uppercase.Image = Properties.Resources.uppercase;
            uppercase.Click += (sender, eventArgs) => { editor.Text = editor.Text?.ToUpper(); };
            editMenu.DropDownItems.Add(uppercase);

            //menu 'lowercase'
            lowercase.Text = "само малки букви";
            lowercase.Font = newFile.Font;
            lowercase.Image = Properties.Resources.lowercase;
            lowercase.Click += (sender, eventArgs) => { editor.Text = editor.Text?.ToLower(); };
            editMenu.DropDownItems.Add(lowercase);

            //menu 'change selection text fore'
            markSelection.Text = "маркиране на селекцията";
            markSelection.Font = newFile.Font;
            markSelection.Image = Properties.Resources.markSelection;
            markSelection.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "маркиране на селектирания текст в жълто",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    2000
                );
            };
            markSelection.Click += (sender, eventArgs) => { editor.SelectionBackColor = Color.Yellow; };
            editMenu.DropDownItems.Add(markSelection);

            //menu 'unmark selection'
            unmarkSelection.Text = "отмаркиране на селекцията";
            unmarkSelection.Font = newFile.Font;
            unmarkSelection.Image = Properties.Resources.deselect;
            unmarkSelection.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "отмаркиране на селектирания текст",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    2000
                );
            };
            unmarkSelection.Click += (sender, eventArgs) => { editor.SelectionBackColor = editor.BackColor; };
            editMenu.DropDownItems.Add(unmarkSelection);
        }
        private void BuildOptionsMenuAndTheOptions()
        {
            //Create array of all menus and menu items
            ToolStripMenuItem[] menuComplect =
            [
                fileMenu, editMenu, optionsMenu, helpMenu,
                newFile, newWindow, open, save, reboot, exit,
                back, next, selectAll, cut, copy, paste, 
                deleteAll, uppercase, lowercase, markSelection,
                unmarkSelection, appearance, blueMode, darkMode,
                lightMode, fontAndColor, information
            ];
            //Create array of all menus with icons
            ToolStripMenuItem[] menusWithIcon = 
            [
                newFile, newWindow, open, save, reboot,
                exit, back, next, selectAll, cut, copy, paste,
                deleteAll, lowercase, uppercase, markSelection,
                unmarkSelection, appearance, fontAndColor, information
            ];
            //Settin up the options menu
            optionsMenu.Text = "Опций";
            optionsMenu.Font = fileMenu.Font;
            optionsMenu.BackColor = fileMenu.BackColor;
            menuBar.Items.Add(optionsMenu);
            //Setting up the appearance menu
            appearance.Text = "тема";
            appearance.Font = newFile.Font;
            appearance.Image = Properties.Resources.theme;
            optionsMenu.DropDownItems.Add(appearance);
            //Setting up the blue mode menu
            blueMode.Text = "синя тема";
            blueMode.Font = newFile.Font;
            blueMode.Click += (sender, eventArgs) =>
            {
                new SystemEngine().ChangeToBlueMode(this, editor, textAreaPanel, menuBar, menuComplect);
                new SystemEngine().SetBlueModeIcons(menusWithIcon);
            };
            appearance.DropDownItems.Add(blueMode);
            //Setting up the dark mode menu
            darkMode.Text = "тъмна тема";
            darkMode.Font = newFile.Font;
            darkMode.Click += (sender, eventArgs) =>
            {
                new SystemEngine().ChangeToDarkMode(this, textAreaPanel, editor, menuBar, menuComplect);
                new SystemEngine().SetDarkModeIcons(menusWithIcon);
            };
            appearance.DropDownItems.Add(darkMode);
            //Setting up the light mode menu
            lightMode.Text = "светла тема";
            lightMode.Font = newFile.Font;
            lightMode.Click += (sender, eventArgs) =>
            {
                new SystemEngine().ChangeToLightMode(this, editor, textAreaPanel, menuBar, menuComplect);
                new SystemEngine().SetLightModeIcons(menusWithIcon);
            };
            appearance.DropDownItems.Add(lightMode);
            //Group the menu items for appearance
            colorModeMenus[0] = blueMode;
            colorModeMenus[1] = darkMode;
            colorModeMenus[2] = lightMode;
            new SystemEngine().GroupMenuItems(colorModeMenus);
            //Setting up the font and color menu
            fontAndColor.Text = "Шрифт и цвят";
            fontAndColor.Font = newFile.Font;
            fontAndColor.Image = Properties.Resources.fontAndColor;
            fontAndColor.Click += (sender, eventArgs) =>
            {
                FontSettingsWindowGUI settingsWin = new();
                settingsWin.FontChanged += (newFont) => { editor.Font = newFont; };
                settingsWin.ForegroundOnChange += (newForeColor) => { editor.ForeColor = newForeColor; };
            };
            optionsMenu.DropDownItems.Add(fontAndColor);
        }
        private void BuildHelpMenuAndInformationMenuItem()
        {
            //Setting up the help menu
            helpMenu.Text = "Относно";
            helpMenu.Font = fileMenu.Font;
            helpMenu.BackColor = fileMenu.BackColor;
            menuBar.Items.Add(helpMenu);

            //Setting up the information menu
            information.Text = "информация";
            information.Font = newFile.Font;
            information.Image = Properties.Resources.infoMenu;
            information.Click += (sender, eventArgs) => { new InformationWindow(); };
            helpMenu.DropDownItems.Add(information);
        }
        private void BuildExecutionButton()
        {
            //Button run
            execute.Bounds = new Rectangle(1700, 30, 18, 18);
            execute.BackColor = Color.FromArgb(200, 31, 31, 31);
            execute.Image = Properties.Resources.run;
            execute.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(execute);
            tip.SetToolTip(execute, "Изпълни кода в редактора");
            execute.MouseClick += (sender, eventArgs) => { new SystemEngine().CompileAndRun(editor); };
        }
    } 
}