/**
 * Text Editor++
 * 
 * Starter - The entry point for the application.
 */

using System.Windows.Forms;
using System;

namespace TextEditor
{

    internal class Starter
    {

        [STAThread]
        static void Main(string[] args)
        {

            Application.SetCompatibleTextRenderingDefault(true);
            Application.EnableVisualStyles();
            Application.Run(new ApplicationGUI());

        }

    }

}