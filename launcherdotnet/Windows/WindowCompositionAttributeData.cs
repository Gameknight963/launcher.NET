using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace launcherdotnet.Windows
{
    /// <summary>
    /// Contains the data required by the undocumented
    /// SetWindowCompositionAttribute API.
    /// Specifies which window composition attribute to modify and points
    /// to the data associated with that attribute.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowCompositionAttributeData
    {
        /// <summary>
        /// The window composition attribute to modify.
        /// </summary>
        public WindowCompositionAttribute Attribute;

        /// <summary>
        /// A pointer to the data for the specified attribute.
        /// The structure and size of this data depends on the selected attribute.
        /// </summary>
        public nint Data;

        /// <summary>
        /// The size, in bytes, of the data pointed to by <see cref="Data"/>.
        /// </summary>
        public int SizeOfData;
    }
}
