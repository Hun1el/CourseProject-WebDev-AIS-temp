using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSiteDev
{
    public static class FormControl
    {
        public static void Resize(Form parentForm, int newWidth, string panelRightName = "panel2")
        {
            if (parentForm == null)
            {
                return;
            }

            parentForm.SuspendLayout();

            int delta = newWidth - parentForm.Width;
            parentForm.Width = newWidth;

            var panelRight = parentForm.Controls[panelRightName];

            if (panelRight != null)
            {
                panelRight.Width += delta;
            }

            parentForm.ResumeLayout();
            parentForm.Invalidate();
        }

        private static void DisposeControlRecursively(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                DisposeControlRecursively(c);
            }

            if (ctrl is PictureBox pb && pb.Image != null)
            {
                pb.Image.Dispose();
                pb.Image = null;
            }

            ctrl.Dispose();
        }

        public static void ClearPanelControls(Panel panel)
        {
            if (panel == null)
            {
                return;
            }

            foreach (Control c in panel.Controls)
            {
                DisposeControlRecursively(c);
            }

            panel.Controls.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
