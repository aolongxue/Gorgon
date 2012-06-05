﻿#region MIT.
// 
// Gorgon.
// Copyright (C) 2012 Michael Winsor
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
// Created: Thursday, May 31, 2012 7:27:14 AM
// 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GorgonLibrary.Diagnostics;
using GorgonLibrary.FileSystem;

namespace GorgonLibrary.UI
{
	/// <summary>
	/// The open file dialog interface.
	/// </summary>
	public class GorgonFileSystemOpenFile
		: GorgonFileSystemDialog
	{
		#region Properties.
		/// <summary>
		/// Property to set or return whether to allow multiple files to be selected or not.
		/// </summary>
		public bool AllowMultiSelect
		{
			get;
			set;
		}
		
		/// <summary>
		/// Property to set or return whether to check to see if a file exists or not.
		/// </summary>
		/// <remarks>If this value is set to FALSE, the user is responsible for checking filename validity.</remarks>
		public bool CheckIfFileExists
		{
			get;
			set;
		}
		#endregion

		#region Methods.
		/// <summary>
		/// Function to initialize the window.
		/// </summary>
		protected override void InitWindow()
		{
			DialogWindow.AllowMultiSelect = AllowMultiSelect;
			DialogWindow.CheckForExistingFile = CheckIfFileExists;
		}
		#endregion

		#region Constructor/Destructor.
		/// <summary>
		/// Initializes a new instance of the <see cref="GorgonFileSystemOpenFile"/> class.
		/// </summary>
		/// <param name="fileSystem">The file system.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="fileSystem"/> parameter is NULL (Nothing in VB.Net).</exception>
		/// <exception cref="System.ArgumentException">Thrown when the file system has had a directory or packed file mounted.</exception>
		public GorgonFileSystemOpenFile(GorgonFileSystem fileSystem)
			: base(fileSystem, true)
		{
			AllowMultiSelect = true;
			CheckIfFileExists = true;
			Text = "Open file...";
		}
		#endregion
	}
}
