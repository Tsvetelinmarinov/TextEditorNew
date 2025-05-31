/**
 * Text Editor++
 * 
 * ApplicationGUI - The Graphical User Interface for the application.
 */

using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace TextEditor
{

    internal class ApplicationGUI : Form
    {

        //Menu bar for the application 
        private MenuStrip menuBar;

        //File menu
        private ToolStripMenuItem fileMenu;

        //Edit menu
        private ToolStripMenuItem editMenu;

        //Options menu
        private ToolStripMenuItem optionsMenu;

        //Help menu
        private ToolStripMenuItem helpMenu;

        //menu 'open'
        private ToolStripMenuItem open;

        //menu 'save'
        private ToolStripMenuItem save;

        //menu 'new file'
        private ToolStripMenuItem newFile;

        //menu 'new window'
        private ToolStripMenuItem newWindow;

        //menu 'restart'
        private ToolStripMenuItem reboot;

        //menu 'exit'
        private ToolStripMenuItem exit;

        //menu 'back'
        private ToolStripMenuItem back;

        //menu 'next'
        private ToolStripMenuItem next;

        //menu 'select all'
        private ToolStripMenuItem selectAll;

        //menu 'cut'
        private ToolStripMenuItem cut;

        //menu 'copy'
        private ToolStripMenuItem copy;

        //menu 'paste'
        private ToolStripMenuItem paste;

        //menu 'delete all'
        private ToolStripMenuItem deleteAll;

        //Menu 'make uppercase'
        private ToolStripMenuItem uppercase;

        //Menu 'lowercase'
        private ToolStripMenuItem lowercase;

        //menu 'mark selection'
        private ToolStripMenuItem markSelection;

        //menu 'unmark selection'
        private ToolStripMenuItem unmarkSelection;

        //menu 'appearance'
        private ToolStripMenuItem appearance;

        //menu 'blue mode'
        private ToolStripMenuItem blueMode;

        //check menu 'dark'
        private ToolStripMenuItem darkMode;

        //check menu 'light'
        private ToolStripMenuItem lightMode;

        //Group of the menu 'appearance'
        private ToolStripMenuItem[] colorModeMenus = new ToolStripMenuItem[3];

        //menu "font and color"
        private ToolStripMenuItem fontAndColor;

        //menu 'information'
        private ToolStripMenuItem information;

        //Text box - the editor
        public RichTextBox editor;

        //ToolTip for the menu bar and the menus
        private readonly ToolTip tip = new ToolTip();
  

        /**
         * Constructor for the ApplicationGUI class.
         * Initializes the form and its components.
         */
        public ApplicationGUI()
        {

            //Build the GUI
            BuildGUI();

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

        }


        /**
         * Builds the GUI of the application.
         */
        private void BuildGUI()
        {

            // Setting up the panel for the editor
            Panel textAreaPanel = new()
            {
                Bounds = new Rectangle(-1, 55, 1780, 832),
                BackColor = Color.FromArgb(140, 80, 80, 200), // Border color
                Padding = new Padding(1),// Thickness of the border
            };
            Controls.Add(textAreaPanel);


            // Setting up the text box
            editor = new RichTextBox()
            {
                Bounds = new Rectangle(-1, -1, 1780, 832),
                Font = new Font("Cascadia Code", 13),
                ForeColor = Color.FromArgb(50, 50, 50),
                BackColor = Color.White,
                Multiline = true,
                ScrollBars = RichTextBoxScrollBars.Both,
                WordWrap = false,
                AcceptsTab = true,
            };
            textAreaPanel.Controls.Add(editor);

            //Coloring the keywords in the editor
            editor.TextChanged += (sender, eventArgs) => 
            {
                editor.SelectionIndent = 2;
                new SystemEngine().MatchKeywords(editor); 
            };

            //Match the cruely brackets in the editor
            editor.KeyPress += (sender, eventArgs) =>
            {
                if (eventArgs.KeyChar == '{')
                {
                    //Запази текущата позиция на курсора
                    int currentCursorPosition = editor.SelectionStart;

                    // Вмъкни текста на позицията на курсора
                    editor.SelectedText = "\n{\n\n    \n\n}";

                    // Премести курсора между скобите, след отстъпа
                    editor.SelectionStart = currentCursorPosition + 8;
                    eventArgs.Handled = true;
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

            //Setting up the menu bar
            menuBar = new MenuStrip()
            {
                Bounds = new Rectangle(0, 0, this.Width, 20),
                BackColor = Color.FromArgb(20, 10, 50, 220),
                Cursor = Cursors.Hand
            };
            this.Controls.Add(menuBar);
            menuBar.Renderer = new MenuBackgroundRender();

            //Setting up the file menu
            fileMenu = new ToolStripMenuItem()
            {
                Text = "Файл",
                Font = new Font("Seoge UI", 11),
                BackColor = this.BackColor,
            };
            menuBar.Items.Add(fileMenu);

            //Setting up the new file menu
            newFile = new ToolStripMenuItem()
            {
                Text = "нов файл",
                Font = new Font("Seoge UI", 11),
                Image = Properties.Resources.newFile
            };
            newFile.Click += (sender, eventArgs) => { new SystemEngine().CreateNewFile(editor); };
            fileMenu.DropDownItems.Add(newFile);

            //menu 'new window'
            newWindow = new ToolStripMenuItem()
            {
                Text = "нов прозорец",
                Font = newFile.Font,
                Image = Properties.Resources.newWindow
            };
            newWindow.Click += (sender, eventArgs) => { new ApplicationGUI().Show(); };
            fileMenu.DropDownItems.Add(newWindow);

            //Setting up the open menu
            open = new ToolStripMenuItem()
            {
                Text = "отвори",
                Font = newFile.Font,
                Image = Properties.Resources.open
            };
            open.Click += (sender, eventArgs) => { new SystemEngine().LoadDataFromFile(editor); };
            fileMenu.DropDownItems.Add(open);

            //Setting up the save menu
            save = new ToolStripMenuItem()
            {
                Text = "запиши",
                Font = newFile.Font,
                Image = Properties.Resources.save
            };
            save.Click += (sender, eventArgs) => { new SystemEngine().ExportDataToLocalFile(editor); };
            fileMenu.DropDownItems.Add(save);

            //Setting up the reboot menu
            reboot = new ToolStripMenuItem()
            {
                Text = "рестартиране",
                Font = newFile.Font,
                Image = Properties.Resources.restart
            };
            reboot.Click += (sender, eventArgs) => { new SystemEngine().Reboot(editor); };
            fileMenu.DropDownItems.Add(reboot);

            //Setting up the exit menu
            exit = new ToolStripMenuItem()
            {
                Text = "изход",
                Font = newFile.Font,
                Image = Properties.Resources.exit
            };
            exit.Click += (sender, eventArgs) => { new SystemEngine().Quit(editor); };
            fileMenu.DropDownItems.Add(exit);

            //Setting up the edit menu
            editMenu = new ToolStripMenuItem()
            {
                Text = "Редакция",
                Font = fileMenu.Font,
                BackColor = fileMenu.BackColor
            };
            menuBar.Items.Add(editMenu);

            //Setting up the undo menu
            back = new ToolStripMenuItem()
            {
                Text = "предишна стъпка",
                Font = newFile.Font,
                Image = Properties.Resources.undo
            };
            back.Click += (sender, eventArgs) => { editor.Undo(); };
            editMenu.DropDownItems.Add(back);

            //Setting up the redo menu
            next = new ToolStripMenuItem()
            {
                Text = "предходна стъпка",
                Font = newFile.Font,
                Image = Properties.Resources.redo
            };
            next.Click += (sender, eventArgs) => { editor.Redo(); };
            editMenu.DropDownItems.Add(next);

            //Setting up the select all menu
            selectAll = new ToolStripMenuItem()
            {
                Text = "селектирай всичко",
                Font = newFile.Font,
                Image = Properties.Resources.select
            };
            selectAll.Click += (sender, eventArgs) => { editor.SelectAll(); };
            editMenu.DropDownItems.Add(selectAll);

            //Setting up the cut menu
            cut = new ToolStripMenuItem()
            {
                Text = "изрежи",
                Font = newFile.Font,
                Image = Properties.Resources.cut
            };
            cut.Click += (sender, eventArgs) => { editor.Cut(); };
            editMenu.DropDownItems.Add(cut);

            //Setting up the copy menu
            copy = new ToolStripMenuItem()
            {
                Text = "копиране",
                Font = newFile.Font,
                Image = Properties.Resources.copy
            };
            copy.Click += (sender, eventArgs) => { editor.Copy(); };
            editMenu.DropDownItems.Add(copy);

            //Setting up the paste menu
            paste = new ToolStripMenuItem()
            {
                Text = "поставяне",
                Font = newFile.Font,
                Image = Properties.Resources.paste
            };
            paste.Click += (sender, eventArgs) => { editor.Paste(); };
            editMenu.DropDownItems.Add(paste);

            //Setting up the delete all menu
            deleteAll = new ToolStripMenuItem()
            {
                Text = "изтриване на всичко",
                Font = newFile.Font,
                Image = Properties.Resources.delete
            }; 
            deleteAll.Click += (sender, eventArgs) => { editor.Clear(); };
            editMenu.DropDownItems.Add(deleteAll);

            //menu 'uppercase'
            uppercase = new ToolStripMenuItem()
            {
                Text = "само главни букви",
                Font = newFile.Font,
                Image = Properties.Resources.uppercase
            };
            uppercase.Click += (sender, eventArgs) => { editor.Text = editor.Text?.ToUpper(); };
            editMenu.DropDownItems.Add(uppercase);

            //menu 'lowercase'
            lowercase = new ToolStripMenuItem()
            {
                Text = "само малки букви",
                Font = newFile.Font,
                Image = Properties.Resources.lowercase
            };
            lowercase.Click += (sender, eventArgs) => { editor.Text = editor.Text?.ToLower(); };
            editMenu.DropDownItems.Add(lowercase);

            //menu 'change selection text fore'
            markSelection = new ToolStripMenuItem()
            {
                Text = "маркиране на селекцията",
                Font = newFile.Font,
                Image = Properties.Resources.markSelection
            };
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
            unmarkSelection = new ToolStripMenuItem()
            {
                Text = "отмаркиране на селекцията",
                Font = newFile.Font,
                Image = Properties.Resources.deselect
            };
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

            //Settin up the options menu
            optionsMenu = new ToolStripMenuItem()
            {
                Text = "Опций",
                Font = fileMenu.Font,
                BackColor = fileMenu.BackColor
            };
            menuBar.Items.Add(optionsMenu);

            //Setting up the appearance menu
            appearance = new ToolStripMenuItem()
            {
                Text = "тема",
                Font = newFile.Font,
                Image = Properties.Resources.theme
            };
            optionsMenu.DropDownItems.Add(appearance);

            //Setting up the blue mode menu
            blueMode = new ToolStripMenuItem()
            {
                Text = "синя тема",
                Font = newFile.Font
            };
            blueMode.Click += (sender, eventArgs) =>
            {
                new SystemEngine().ChangeToBlueMode
                (
                    this,
                    editor,
                    textAreaPanel,
                    menuBar,
                    fileMenu,
                    editMenu,
                    optionsMenu,
                    helpMenu,
                    newFile,
                    newWindow,
                    open,
                    save,
                    reboot,
                    exit,
                    back,
                    next,
                    selectAll,
                    cut,
                    copy,
                    paste,
                    deleteAll,
                    uppercase,
                    lowercase,
                    markSelection,
                    unmarkSelection,
                    appearance,
                    blueMode,
                    darkMode,
                    lightMode,
                    fontAndColor,
                    information
                );

                new SystemEngine().SetBlueModeIcons
                (
                    newFile,
                    newWindow,
                    open,
                    save,
                    reboot,
                    exit,
                    back,
                    next,
                    selectAll,
                    cut,
                    copy,
                    paste,
                    deleteAll,
                    uppercase,
                    lowercase,
                    markSelection,
                    unmarkSelection,
                    appearance,
                    fontAndColor,
                    information
                );
            };
            appearance.DropDownItems.Add(blueMode);

            //Setting up the dark mode menu
            darkMode = new ToolStripMenuItem()
            {
                Text = "тъмна тема",
                Font = newFile.Font
            };
            darkMode.Click += (sender, eventArgs) =>
            {
                new SystemEngine().ChangeToDarkMode
                (
                    this,
                    editor,
                    textAreaPanel,
                    menuBar,
                    fileMenu,
                    editMenu,
                    optionsMenu,
                    helpMenu,
                    newFile,
                    newWindow,
                    open,
                    save,
                    reboot,
                    exit,
                    back,
                    next,
                    selectAll,
                    cut,
                    copy,
                    paste,
                    deleteAll,
                    uppercase,
                    lowercase,
                    markSelection,
                    unmarkSelection,
                    appearance,
                    blueMode,
                    darkMode,
                    lightMode,
                    fontAndColor,
                    information
                );

                new SystemEngine().SetDarkModeIcons
                (
                    newFile, newWindow, open, save, reboot, exit,
                    back, next, selectAll, cut, copy, paste,
                    deleteAll, uppercase, lowercase, markSelection,
                    unmarkSelection, appearance, fontAndColor, information
                );
            };
            appearance.DropDownItems.Add(darkMode);

            //Setting up the light mode menu
            lightMode = new ToolStripMenuItem()
            {
                Text = "светла тема",
                Font = newFile.Font
            };
            lightMode.Click += (sender, eventArgs) =>
            {
                new SystemEngine().ChangeToLightMode
                (
                    this,
                    editor,
                    textAreaPanel,
                    menuBar,
                    fileMenu,
                    editMenu,
                    optionsMenu,
                    helpMenu,
                    newFile,
                    newWindow,
                    open,
                    save,
                    reboot,
                    exit,
                    back,
                    next,
                    cut,
                    copy,
                    paste,
                    selectAll,
                    deleteAll,
                    uppercase,
                    lowercase,
                    markSelection,
                    unmarkSelection,
                    appearance,
                    blueMode,
                    darkMode,
                    lightMode,
                    fontAndColor,
                    information
                );

                new SystemEngine().SetLightModeIcons
                (
                    newFile,
                    newWindow,
                    open,
                    save,
                    reboot,
                    exit,
                    back,
                    next,
                    selectAll,
                    cut,
                    copy,
                    paste,
                    deleteAll,
                    uppercase,
                    lowercase,
                    markSelection,
                    unmarkSelection,
                    appearance,
                    fontAndColor,
                    information
                );
            };
            appearance.DropDownItems.Add(lightMode);

            //Group the menu items for appearance
            colorModeMenus[0] = blueMode;
            colorModeMenus[1] = darkMode;
            colorModeMenus[2] = lightMode;

            new SystemEngine().GroupMenuItems(colorModeMenus);

            //Setting up the font and color menu
            fontAndColor = new ToolStripMenuItem()
            {
                Text = "Шрифт и цвят",
                Font = newFile.Font,
                Image = Properties.Resources.fontAndColor
            };
            fontAndColor.Click += (sender, eventArgs) => 
            { 
               FontSettingsWindowGUI settingsWin = new FontSettingsWindowGUI();
               settingsWin.FontChanged += (newFont) => { editor.Font = newFont; };
               settingsWin.ForegroundOnChange += (newForeColor) => { editor.ForeColor = newForeColor; };
            };
            optionsMenu.DropDownItems.Add(fontAndColor);

            //Setting up the help menu
            helpMenu = new ToolStripMenuItem()
            {
                Text = "Относно",
                Font = fileMenu.Font,
                BackColor = fileMenu.BackColor
            };
            menuBar.Items.Add(helpMenu);

            //Setting up the information menu
            information = new ToolStripMenuItem()
            {
                Text = "информация",
                Font = newFile.Font,
                Image = Properties.Resources.infoMenu
            };
            information.Click += (sender, eventArgs) => { new InformationWindow(); };
            helpMenu.DropDownItems.Add(information);            

        }

    } 

}