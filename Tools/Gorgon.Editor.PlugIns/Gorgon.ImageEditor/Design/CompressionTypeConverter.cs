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
// Created: Wednesday, October 08, 2014 8:16:21 PM
// 
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using GorgonLibrary.Editor.ImageEditorPlugIn.Properties;
using GorgonLibrary.Graphics;

namespace GorgonLibrary.Editor.ImageEditorPlugIn
{
    /// <summary>
    /// A type converter used to retrieve a list of compression formats.
    /// </summary>
    class CompressionTypeConverter
        : TypeConverter
    {
        #region Methods.
        /// <summary>
        /// Returns whether this converter can convert the object to the specified type, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
        /// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that represents the converted value.
        /// </returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            var bufferFormat = BufferFormat.Unknown;

            if (value != null)
            {
                bufferFormat = (BufferFormat)value;
            }

            if (bufferFormat != BufferFormat.Unknown)
            {
                return bufferFormat.ToString();
            }

            CultureInfo prevCulture = Resources.Culture;

            try
            {
                Resources.Culture = culture;
                return Resources.GORIMG_TEXT_NONE;
            }
            finally
            {
                Resources.Culture = prevCulture;
            }
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that represents the converted value.
        /// </returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if ((value == null)
                || (value.GetType() != typeof(string)))
            {
                return base.ConvertFrom(context, culture, value);
            }

            string formatString = value.ToString();

            // Why doesn't string.Equals have an overload that takes a culture object?
            CultureInfo prevCulture = Resources.Culture;

            try
            {
                Resources.Culture = culture;
                if (string.Compare(formatString, Resources.GORIMG_TEXT_NONE, culture, CompareOptions.IgnoreCase) == 0)
                {
                    return BufferFormat.Unknown;
                }

                BufferFormat format;
                if (!Enum.TryParse(formatString, out format))
                {
                    throw new InvalidCastException(string.Format(culture,
                                                                 Resources.GORIMG_ERR_UNRECOGNIZED_IMAGE_FORMAT,
                                                                 formatString));
                }

                return format;
            }
            finally
            {
                Resources.Culture = prevCulture;
            }
        }

        /// <summary>
        /// Returns whether this object supports properties, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <returns>
        /// true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.
        /// </returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        /// <summary>
        /// Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <returns>
        /// true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.
        /// </returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.
        /// </returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var content = (GorgonImageContent)((ContentTypeDescriptor)context.Instance).Content;
            BufferFormat currentFormat = content.ImageFormat;

            var formats = new List<BufferFormat>
                          {
                              BufferFormat.Unknown
                          };

            // Image width/height must be a multiple of four to compress.
            if (((content.Width % 4) != 0)
                || ((content.Height % 4) != 0))
            {
                return new StandardValuesCollection(formats);
            }

            switch (currentFormat)
            {
                case BufferFormat.R8G8B8A8_UIntNormal:
                case BufferFormat.B8G8R8X8_UIntNormal:
                case BufferFormat.B8G8R8A8_UIntNormal:
                    formats.Add(BufferFormat.BC1_UIntNormal);
                    formats.Add(BufferFormat.BC2_UIntNormal);
                    formats.Add(BufferFormat.BC3_UIntNormal);

                    if (ContentObject.Graphics.VideoDevice.SupportedFeatureLevel >= DeviceFeatureLevel.SM5)
                    {
                        formats.Add(BufferFormat.BC7_UIntNormal);
                    }

                    break;
                case BufferFormat.R8G8B8A8_UIntNormal_sRGB:
                case BufferFormat.B8G8R8A8_UIntNormal_sRGB:
                case BufferFormat.B8G8R8X8_UIntNormal_sRGB:
                    formats.Add(BufferFormat.BC1_UIntNormal_sRGB);
                    formats.Add(BufferFormat.BC2_UIntNormal_sRGB);
                    formats.Add(BufferFormat.BC3_UIntNormal_sRGB);

                    if (ContentObject.Graphics.VideoDevice.SupportedFeatureLevel >= DeviceFeatureLevel.SM5)
                    {
                        formats.Add(BufferFormat.BC7_UIntNormal_sRGB);
                    }
                    break;
                case BufferFormat.R8_IntNormal:
                    formats.Add(BufferFormat.BC4_IntNormal);
                    break;
                case BufferFormat.R8_UIntNormal:
                    formats.Add(BufferFormat.BC4_UIntNormal);
                    break;
                case BufferFormat.R8G8_IntNormal:
                    formats.Add(BufferFormat.BC5_IntNormal);
                    break;
                case BufferFormat.R8G8_UIntNormal:
                    formats.Add(BufferFormat.BC5_UIntNormal);
                    break;
                case BufferFormat.R16G16B16A16_Float:
                    formats.Add(BufferFormat.BC6H_UF16);
                    formats.Add(BufferFormat.BC6H_SF16);
                    break;
            }


	        return new StandardValuesCollection(formats);
        }
        #endregion
    }
}
