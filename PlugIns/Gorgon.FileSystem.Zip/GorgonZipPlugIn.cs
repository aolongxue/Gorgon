﻿#region MIT.
// 
// Gorgon.
// Copyright (C) 2011 Michael Winsor
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// Created: Monday, June 27, 2011 9:36:13 AM
// 
#endregion

using System.Collections.Generic;
using GorgonLibrary.IO.Zip;
using GorgonLibrary.IO.Zip.Properties;

namespace GorgonLibrary.IO
{
    /// <summary>
    /// Plug-in entry point for the zip file file system provider plug-in.
    /// </summary>
    public class GorgonZipPlugIn
        : GorgonFileSystemProviderPlugIn
    {
        /// <summary>
        /// Header bytes for a zip file.
        /// </summary>
        internal static IEnumerable<byte> ZipHeader = new byte[] { 0x50, 0x4B, 0x3, 0x4 };

		/// <summary>
		/// Function to create a new file system provider plug-in instance.
		/// </summary>
		/// <returns>The file system provider plug-in.</returns>
		protected override GorgonFileSystemProvider OnCreateProvider()
		{
			return new GorgonZipProvider(Resources.GORFS_DESC);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GorgonZipPlugIn"/> class.
		/// </summary>
		public GorgonZipPlugIn()
            : base(Resources.GORFS_PLUGIN_DESC)
		{
		}
	}
}
