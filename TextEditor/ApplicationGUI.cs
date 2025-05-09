/**
 * Text Editor++
 * 
 * ApplicationGUI - The Graphical User Interface for the application.
 */

using System.Windows.Forms;
using System.Drawing;


namespace TextEditor
{

    internal partial class ApplicationGUI : Form
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
        private ToolTip tip = new ToolTip();



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
            this.Size = new Size(1300, 700);
            this.Icon = Properties.Resources.icon;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.GhostWhite;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

        }


        /**
         * Builds the GUI of the application.
         */
        private void BuildGUI()
        {

            //Setting up the text box
            editor = new RichTextBox()
            {
                Bounds = new Rectangle(0, 55, 1280, 605),
                BorderStyle = BorderStyle.None,
                Font = new Font("Cascadia Code", 14),
                ForeColor = Color.FromArgb(255, 50, 50, 50),
                BackColor = Color.White,
                Multiline = true,
                ScrollBars = RichTextBoxScrollBars.Both,
                WordWrap = false
            };
            this.Controls.Add(editor);

            //Setting up the menu bar
            menuBar = new MenuStrip()
            {
                Bounds = new Rectangle(0, 0, this.Width, 30),
                BackColor = Color.FromArgb(20, 10, 50, 220),
                Cursor = Cursors.Hand
            };
            this.Controls.Add(menuBar);
            menuBar.Renderer = new MenuBackgroundRender();

            //Setting up the file menu
            fileMenu = new ToolStripMenuItem()
            {
                Text = "Файл",
                Font = new Font("Seoge UI", 10),
                BackColor = this.BackColor,
            };
            fileMenu.MouseHover += (sender, eventArgs) =>
            {
                Point currentCursorPosition = this.PointToClient(Cursor.Position);
                tip.Show
                (
                    "Опций за файлове",
                    this,
                    currentCursorPosition.X,
                    currentCursorPosition.Y,
                    1000
                );
            };
            menuBar.Items.Add(fileMenu);

            //Setting up the new file menu
            newFile = new ToolStripMenuItem()
            {
                Text = "нов файл",
                Font = new Font("Seoge UI", 10),
                Image = Properties.Resources.newFile
            };
            newFile.Click += (sender, eventArgs) => { new SystemEngine().CreateNewFile(editor); };
            newFile.MouseHover += (sender, eventArgs) =>
            {
                Point currentCursorPosition = this.PointToClient(Cursor.Position);
                tip.Show
                (
                    "Създаване на нов файл",
                    this,
                    currentCursorPosition.X,
                    currentCursorPosition.Y,
                    1500
                );
            };
            fileMenu.DropDownItems.Add(newFile);

            //Setting up the open menu
            open = new ToolStripMenuItem()
            {
                Text = "отвори",
                Font = newFile.Font,
                Image = Properties.Resources.open
            };
            open.Click += (sender, eventArgs) => { new SystemEngine().LoadDataFromFile(editor); };
            open.MouseHover += (sender, eventArgs) =>
            {
                Point currentCursorLocation = this.PointToClient(Cursor.Position);
                tip.Show
                (
                    "Отваряне на файл",
                    this,
                    currentCursorLocation.X,
                    currentCursorLocation.Y,
                    1500
                );
            };
            fileMenu.DropDownItems.Add(open);

            //Setting up the save menu
            save = new ToolStripMenuItem()
            {
                Text = "запиши",
                Font = newFile.Font,
                Image = Properties.Resources.save
            };
            save.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Запазване на файл",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
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
            reboot.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Рестартитане на приложението",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
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
            exit.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Изход от приложението",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
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
            editMenu.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Опций за редактиране",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            menuBar.Items.Add(editMenu);

            //Setting up the undo menu
            back = new ToolStripMenuItem()
            {
                Text = "предишна стъпка",
                Font = newFile.Font,
                Image = Properties.Resources.undo
            };
            back.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Връщане назад",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
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
            next.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Следваща стъпка",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
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
            selectAll.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Избиране на всичко",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
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
            cut.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Изрязване",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
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
            copy.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Копиране",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
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
            paste.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Поставяне",
                    this,
                    this.PointToClient(Cursor.Position).X,
                    this.PointToClient(Cursor.Position).Y,
                    1500
                );
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
            deleteAll.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Изтриване на всичко",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            deleteAll.Click += (sender, eventArgs) => { editor.Clear(); };
            editMenu.DropDownItems.Add(deleteAll);

            //Settin up the options menu
            optionsMenu = new ToolStripMenuItem()
            {
                Text = "Опций",
                Font = fileMenu.Font,
                BackColor = fileMenu.BackColor
            };
            optionsMenu.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Опций за настройки",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            menuBar.Items.Add(optionsMenu);

            //Setting up the appearance menu
            appearance = new ToolStripMenuItem()
            {
                Text = "изглед",
                Font = newFile.Font,
                Image = Properties.Resources.theme
            };
            appearance.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Избор на тема",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            optionsMenu.DropDownItems.Add(appearance);

            //Setting up the blue mode menu
            blueMode = new ToolStripMenuItem()
            {
                Text = "Синя тема",
                Font = newFile.Font
            };
            blueMode.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "оригинална синя тема",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            blueMode.Click += (sender, eventArgs) =>
            {
                new SystemEngine().ChangeToBlueMode
                (
                    this,
                    editor,
                    menuBar,
                    fileMenu,
                    editMenu,
                    optionsMenu,
                    helpMenu,
                    newFile,
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
            darkMode.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "тъмен режим",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            darkMode.Click += (sender, eventArgs) =>
            {
                new SystemEngine().ChangeToDarkMode
                (
                    this,
                    editor,
                    menuBar,
                    fileMenu,
                    editMenu,
                    optionsMenu,
                    helpMenu,
                    newFile,
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
                    appearance,
                    blueMode,
                    darkMode,
                    lightMode,
                    fontAndColor,
                    information
                );

                new SystemEngine().SetDarkModeIcons
                (
                    newFile, open, save, reboot, exit,
                    back, next, selectAll, cut, copy, paste,
                    deleteAll, appearance, fontAndColor, information
                );
            };
            appearance.DropDownItems.Add(darkMode);

            //Setting up the light mode menu
            lightMode = new ToolStripMenuItem()
            {
                Text = "светла тема",
                Font = newFile.Font
            };
            lightMode.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "светъл режим",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            lightMode.Click += (sender, eventArgs) =>
            {
                new SystemEngine().ChangeToLightMode
                (
                    this,
                    editor,
                    menuBar,
                    fileMenu,
                    editMenu,
                    optionsMenu,
                    helpMenu,
                    newFile,
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
            fontAndColor.MouseHover += (sender, eventArgs) =>
            {
                new ToolTip().Show
                (
                    "Опций за промяна на шрифта",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    2500
                );
            };
            fontAndColor.Click += (sender, eventArgs) => 
            { 
               FontSettingsWindowGUI settingsWin = new FontSettingsWindowGUI();
               settingsWin.FontChanged += (newFont) =>
               {
                   editor.Font = newFont;
               };
               settingsWin.ForegroundOnChange += (newForeColor) =>
               {
                   editor.ForeColor = newForeColor;
               };
            };
            optionsMenu.DropDownItems.Add(fontAndColor);

            //Setting up the help menu
            helpMenu = new ToolStripMenuItem()
            {
                Text = "Относно",
                Font = fileMenu.Font,
                BackColor = fileMenu.BackColor
            };
            helpMenu.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "Информация",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            menuBar.Items.Add(helpMenu);

            //Setting up the information menu
            information = new ToolStripMenuItem()
            {
                Text = "информация",
                Font = newFile.Font,
                Image = Properties.Resources.infoMenu
            };
            information.MouseHover += (sender, eventArgs) =>
            {
                tip.Show
                (
                    "относно приложението",
                    this,
                    PointToClient(Cursor.Position).X,
                    PointToClient(Cursor.Position).Y,
                    1500
                );
            };
            information.Click += (sender, eventArgs) => { new InformationWindow(); };
            helpMenu.DropDownItems.Add(information);            

        }

    } 

}