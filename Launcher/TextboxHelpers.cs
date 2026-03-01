using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace launcherdotnet
{
    public static class TextBoxHelpers
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        /// <summary>
        /// sets the placeholder text of a textbox
        /// </summary>
        public static void SetPlaceholder(this TextBox textBox, string placeholder)
        {
            if (textBox == null) throw new ArgumentNullException(nameof(textBox));
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, placeholder);
        }
    }
}
