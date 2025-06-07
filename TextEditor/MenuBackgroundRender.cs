/**
 * Text Editor++
 * 
 * MenuBackgroundRender - A class that handles the rendering of the menu background.
 */

using System.Drawing;
using System.Windows.Forms;

namespace TextEditor
{
    internal class MenuBackgroundRender : ToolStripProfessionalRenderer
    {      
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs eventArgs)
        {

            if (eventArgs.Item.Selected)
            {
                Rectangle menuRectangle = new Rectangle(Point.Empty, eventArgs.Item.Size);
                menuRectangle.Width--;
                menuRectangle.Height--;
                eventArgs.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(160, 170, 190, 240)), menuRectangle);

                using Pen borderWriter = new Pen(Color.FromArgb(80, 80, 80));
                eventArgs.Graphics.DrawRectangle(borderWriter, menuRectangle);
            }
            else
            {
                base.OnRenderMenuItemBackground(eventArgs);
            }
        }
    }
}
