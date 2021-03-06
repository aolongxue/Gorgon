﻿#region MIT.
// 
// Gorgon.
// Copyright (C) 2014 Michael Winsor
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
// Created: Thursday, February 27, 2014 10:14:00 PM
// 
#endregion

using System;
using System.Xml.Linq;
using GorgonLibrary.IO.Properties;

namespace GorgonLibrary.IO
{
	/// <summary>
	/// A user defined property that is assigned to dependency objects.
	/// </summary>
	public struct GorgonEditorDependencyProperty
		: INamedObject, IEquatable<GorgonEditorDependencyProperty>
	{
		#region Constants.
		private const string NameAttr = "Name";					// Name of the property name attribute.

		/// <summary>
		/// Name of the property node.
		/// </summary>
		internal const string PropertyNode = "Property";
		#endregion

		#region Variables.
		/// <summary>
		/// The name of the property.
		/// </summary>
		public readonly string Name;

		/// <summary>
		/// The value for the property.
		/// </summary>
		public readonly string Value;
		#endregion

		#region Methods.
		/// <summary>
		/// Function to deserialize the property from an XML element.
		/// </summary>
		/// <param name="propertyNode">Node containing the property.</param>
		/// <returns>The dependency property.</returns>
		internal static GorgonEditorDependencyProperty Deserialize(XElement propertyNode)
		{
			XAttribute nameAttr = propertyNode.Attribute(NameAttr);

			if ((nameAttr == null)
				|| (string.IsNullOrWhiteSpace(nameAttr.Value)))
			{
				throw new GorgonException(GorgonResult.CannotRead, Resources.GORFS_ERR_DEPENDENCY_PROP_CORRUPT);
			}

			return new GorgonEditorDependencyProperty(nameAttr.Value, propertyNode.Value);
		}

		/// <summary>
		/// Function to determine if two instances are equal.
		/// </summary>
		/// <param name="left">Left instance to compare.</param>
		/// <param name="right">Right instance to compare.</param>
		/// <returns>TRUE if equal, FALSE if not.</returns>
		public static bool Equals(ref GorgonEditorDependencyProperty left, ref GorgonEditorDependencyProperty right)
		{
			return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj is GorgonEditorDependencyProperty)
			{
				return ((GorgonEditorDependencyProperty)obj).Equals(this);
			}

			return base.Equals(obj);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return 281.GenerateHash(Name);
		}

		/// <summary>
		/// Operator to test for equality between two properties.
		/// </summary>
		/// <param name="left">Left property to compare.</param>
		/// <param name="right">Right property to compare.</param>
		/// <returns>TRUE if equal, FALSE if not.</returns>
		public static bool operator ==(GorgonEditorDependencyProperty left, GorgonEditorDependencyProperty right)
		{
			return Equals(ref left, ref right);
		}

		/// <summary>
		/// Operator to test for inequality between two properties.
		/// </summary>
		/// <param name="left">Left property to compare.</param>
		/// <param name="right">Right property to compare.</param>
		/// <returns>TRUE if equal, FALSE if not.</returns>
		public static bool operator !=(GorgonEditorDependencyProperty left, GorgonEditorDependencyProperty right)
		{
			return !Equals(ref left, ref right);
		}
		#endregion

		#region Constructor/Destructor.
		/// <summary>
		/// Initializes a new instance of the <see cref="GorgonEditorDependencyProperty"/> class.
		/// </summary>
		/// <param name="name">The name of the property.</param>
		/// <param name="value">The value for the property.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="name"/> parameter is NULL (Nothing in VB.Net).</exception>
		/// <exception cref="System.ArgumentException">Thrown when the <paramref name="name"/> parameter is an empty string.</exception>
		internal GorgonEditorDependencyProperty(string name, string value = null)
		{
			Name = name;
			Value = value;
		}
		#endregion

		#region INamedObject Members
		/// <summary>
		/// Property to return the name of this object.
		/// </summary>
		string INamedObject.Name
		{
			get
			{
				return Name;
			}
		}
		#endregion

		#region IEquatable<DependencyProperty> Members
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		public bool Equals(GorgonEditorDependencyProperty other)
		{
			return Equals(ref this, ref other);
		}
		#endregion
	}
}
