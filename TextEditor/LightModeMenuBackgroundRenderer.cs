/**
 * Text Editor++
 * LightModeMenuBackgroundRenderer - A class that handles the rendering of the menu background in light mode.
 */

using System.Windows.Forms;
using System.Drawing;

namespace TextEditor
{

    internal class LightModeMenuBackgroundRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            
            if (e.Item.Selected)
            {
                Rectangle menu = new(Point.Empty, e.Item.Size);
                menu.Width--;
                menu.Height--;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 20, 50, 160)), menu);
                using Pen pen = new(Color.DarkSlateGray);
                e.Graphics.DrawRectangle(pen, menu);
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }
    }
}
