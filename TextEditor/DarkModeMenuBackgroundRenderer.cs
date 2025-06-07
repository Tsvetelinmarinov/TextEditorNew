/**
 * Text Editor++
 * 
 * DarkModeMenuBackgroundRenderer - A class that handles the rendering of the menu background in dark mode.
 */

using System.Windows.Forms;
using System.Drawing;

namespace TextEditor
{
    public class DarkModeMenuBackgroundRenderer : ToolStripProfessionalRenderer
    {

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs eventArgs)
        {
         
            if (eventArgs.Item.Selected)
            {
                Rectangle menuItem = new(Point.Empty, eventArgs.Item.Size);
                menuItem.Width--;
                menuItem.Height--;
                eventArgs.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(155, 30, 30, 90)), menuItem);
                using Pen pen = new(Color.FromArgb(155, 130, 130, 90));
                eventArgs.Graphics.DrawRectangle(pen, menuItem);
            }
            else
            {
                base.OnRenderMenuItemBackground(eventArgs);
            }
        }
    }
}
