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
// Created: Monday, June 27, 2011 8:59:02 AM
// 
#endregion

using GorgonLibrary;
using GorgonLibrary.Collections;

namespace GorgonLibrary.FileSystem
{
	/// <summary>
	/// A collection of file system virtual directories.
	/// </summary>
	public class GorgonFileSystemDirectoryCollection
		: GorgonBaseNamedObjectCollection<GorgonFileSystemDirectory>
	{
		#region Properties.
		/// <summary>
		/// Property to return a directory by index.
		/// </summary>
		public GorgonFileSystemDirectory this[int index]
		{
			get
			{
				return GetItem(index);
			}
		}

		/// <summary>
		/// Property to return a directory by name.
		/// </summary>
		public GorgonFileSystemDirectory this[string name]
		{
			get
			{
				return GetItem(GorgonPath.RemoveIllegalPathChars(name));
			}
		}
		#endregion

		#region Methods.
		/// <summary>
		/// Function to return whether an item with the specified name exists in this collection.
		/// </summary>
		/// <param name="name">Name of the item to find.</param>
		/// <returns>TRUE if found, FALSE if not.</returns>
		public override bool Contains(string name)
		{
			return base.Contains(GorgonPath.RemoveIllegalPathChars(name));
		}

		/// <summary>
		/// Function to return the index of a file entry name.
		/// </summary>
		/// <param name="directoryName">Name of the file to return an index for.</param>
		/// <returns>The index of the directory or -1 if it could not be found.</returns>
		public int IndexOf(string directoryName)
		{
			return IndexOf(this[directoryName]);
		}

		/// <summary>
		/// Function to remove a directory from this list.
		/// </summary>
		/// <param name="directory">Directory to remove.</param>
		internal void Remove(GorgonFileSystemDirectory directory)
		{
			RemoveItem(directory);
		}

		/// <summary>
		/// Function to clear all directories from this collection.
		/// </summary>
		internal void Clear()
		{
			ClearItems();
		}

		/// <summary>
		/// Function to add a virtual directory to the collection.
		/// </summary>
		/// <param name="directory">Directory to add.</param>		
		internal void Add(GorgonFileSystemDirectory directory)
		{
			AddItem(directory);
		}
		#endregion

		#region Constructor/Destructor.
		/// <summary>
		/// Initializes a new instance of the <see cref="GorgonFileSystemDirectoryCollection"/> class.
		/// </summary>
		internal GorgonFileSystemDirectoryCollection()
			: base(false)
		{
		}
		#endregion
	}
}
