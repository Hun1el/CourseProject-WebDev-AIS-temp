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
        private static Form lastForm = null;
        private static int lastWidth = 0;

        public static void Resize(Form parentForm, int newWidth, string panelRightName = "panel2")
        {
            if (parentForm == null)
            {
                return;
            }

            if (lastForm != parentForm)
            {
                lastForm = parentForm;
                lastWidth = parentForm.Width;
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

        public static void ResetFormSize(Form parentForm, string panelRightName = "panel2")
        {
            if (parentForm != null && lastForm == parentForm && lastWidth > 0)
            {
                Resize(parentForm, lastWidth, panelRightName);
                lastForm = null;
                lastWidth = 0;
            }
        }

        private static void DispControl(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                DispControl(c);
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
                DispControl(c);
            }

            panel.Controls.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
