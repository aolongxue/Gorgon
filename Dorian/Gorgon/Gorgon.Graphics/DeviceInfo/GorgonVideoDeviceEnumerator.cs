﻿#region MIT.
// 
// Gorgon.
// Copyright (C) 2013 Michael Winsor
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
// Created: Saturday, February 23, 2013 4:00:19 PM
// 
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using SharpDX.Direct3D;
using D3D = SharpDX.Direct3D11;
using GorgonLibrary.Collections.Specialized;
using GorgonLibrary.Graphics.Properties;

namespace GorgonLibrary.Graphics
{
	/// <summary>
	/// Enumerates information about the installed video devices on the system.
	/// </summary>
	/// <remarks>Use this to retrieve a list of video devices available on the system. A video device may be a discreet video card, or a device on the motherboard. 
	/// Retrieve the video device information by calling the <see cref="GorgonLibrary.Graphics.GorgonVideoDeviceEnumerator.Enumerate">Enumerate</see> method and the 
	/// <see cref="P:GorgonLibrary.Graphics.GorgonVideoDeviceEnumerator.VideoDevices">VideoDevices</see> property will be populated with video device information.  From 
	/// there you can pass a <see cref="GorgonLibrary.Graphics.GorgonVideoDevice">GorgonVideoDevice</see> object into the <see cref="GorgonLibrary.Graphics.GorgonGraphics">
	/// GorgonGraphics</see> constructor to use a specific video device.
	/// <para>This interface will allow enumeration of the WARP/Reference devices.  WARP is a high performance software device that will emulate much of the functionality 
	/// that a real video device would have. The reference device is a fully featured device, but is incredibly slow and useful when debugging driver issues.</para>
	/// </remarks>
	public static class GorgonVideoDeviceEnumerator
	{
		#region Variables.
		private static int _lockIncr;

		#endregion

		#region Properties.
		/// <summary>
		/// Property to return the list of available video devices installed on the system.
		/// </summary>
		public static GorgonNamedObjectReadOnlyCollection<GorgonVideoDevice> VideoDevices
		{
			get;
			private set;
		}
		#endregion

		#region Methods.
		#pragma warning disable 0618
		/// <summary>
		/// Function to add the WARP software device.
		/// </summary>
		/// <param name="index">Index of the device.</param>
		/// <returns>The video device used for WARP software rendering.</returns>
		private static GorgonVideoDevice GetWARPSoftwareDevice(int index)
		{
			GorgonVideoDevice device;

#if DEBUG
			using (var D3DDevice = new D3D.Device(SharpDX.Direct3D.DriverType.Warp, D3D.DeviceCreationFlags.Debug, FeatureLevel.Level_10_1, FeatureLevel.Level_10_0, FeatureLevel.Level_9_3))
			{
#else
			using (var D3DDevice = new D3D.Device(SharpDX.Direct3D.DriverType.Warp, D3D.DeviceCreationFlags.None, FeatureLevel.Level_10_1, FeatureLevel.Level_10_0, FeatureLevel.Level_9_3))
			{
#endif
				using (var giDevice = D3DDevice.QueryInterface<SharpDX.DXGI.Device1>())
				{
					using (var adapter = giDevice.GetParent<SharpDX.DXGI.Adapter1>())
					{
						device = new GorgonVideoDevice(adapter, VideoDeviceType.Software, index);

						PrintLog(device);

						// Get the outputs for the device.
						int outputCount = adapter.GetOutputCount();

						GetOutputs(device, D3DDevice, adapter, outputCount, outputCount == 0);
					}
				}
			}
			
			return device;
		}

#if DEBUG
		/// <summary>
		/// Function to add the reference device.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private static GorgonVideoDevice GetRefSoftwareDevice(int index)
		{
			GorgonVideoDevice device;

			using (var D3DDevice = new D3D.Device(SharpDX.Direct3D.DriverType.Reference, D3D.DeviceCreationFlags.Debug))
			{
				using (var giDevice = D3DDevice.QueryInterface<SharpDX.DXGI.Device1>())
				{
					using(var adapter = giDevice.Adapter)
					{
						device = new GorgonVideoDevice(adapter, VideoDeviceType.ReferenceRasterizer, index);

						PrintLog(device);

						int outputCount = adapter.GetOutputCount();
						GetOutputs(device, D3DDevice, adapter, outputCount, outputCount == 0);
					}
				}
			}

			return device;
		}
#endif
		#pragma warning restore 0618

		/// <summary>
		/// Function to print device log information.
		/// </summary>
		/// <param name="device">Device to print.</param>
		private static void PrintLog(GorgonVideoDevice device)
		{
			Gorgon.Log.Print(
				device.VideoDeviceType == VideoDeviceType.ReferenceRasterizer
					? "Device found: {0} ---> !!!** WARNING:  A reference rasterizer has very poor performance."
					: "Device found: {0}", Diagnostics.LoggingLevel.Simple, device.Name);
			Gorgon.Log.Print("===================================================================", Diagnostics.LoggingLevel.Verbose);
			Gorgon.Log.Print("Hardware feature level: {0}", Diagnostics.LoggingLevel.Verbose, device.HardwareFeatureLevel);
			Gorgon.Log.Print("Limited to feature level: {0}", Diagnostics.LoggingLevel.Verbose, device.SupportedFeatureLevel);
			Gorgon.Log.Print("Video memory: {0}", Diagnostics.LoggingLevel.Verbose, device.DedicatedVideoMemory.FormatMemory());
			Gorgon.Log.Print("System memory: {0}", Diagnostics.LoggingLevel.Verbose, device.DedicatedSystemMemory.FormatMemory());
			Gorgon.Log.Print("Shared memory: {0}", Diagnostics.LoggingLevel.Verbose, device.SharedSystemMemory.FormatMemory());
			Gorgon.Log.Print("Device ID: 0x{0}", Diagnostics.LoggingLevel.Verbose, device.DeviceID.FormatHex());
			Gorgon.Log.Print("Sub-system ID: 0x{0}", Diagnostics.LoggingLevel.Verbose, device.SubSystemID.FormatHex());
			Gorgon.Log.Print("Vendor ID: 0x{0}", Diagnostics.LoggingLevel.Verbose, device.VendorID.FormatHex());
			Gorgon.Log.Print("Revision: {0}", Diagnostics.LoggingLevel.Verbose, device.Revision);
			Gorgon.Log.Print("Unique ID: 0x{0}", Diagnostics.LoggingLevel.Verbose, device.UUID.FormatHex());
			Gorgon.Log.Print("===================================================================", Diagnostics.LoggingLevel.Verbose);
		}
				
		/// <summary>
		/// Function to retrieve the video modes for the output.
		/// </summary>
		/// <param name="output">Output that owns the video modes.</param>
		/// <param name="D3DDevice">D3D device for filtering supported display modes.</param>
		/// <param name="giOutput">Output that contains the video modes.</param>
		private static void GetVideoModes(GorgonVideoOutput output, D3D.Device D3DDevice, SharpDX.DXGI.Output giOutput)
		{
			var formats = (BufferFormat[])Enum.GetValues(typeof(BufferFormat));

			Gorgon.Log.Print("Retrieving video modes for output '{0}'...", Diagnostics.LoggingLevel.Simple, output.Name);
			Gorgon.Log.Print("===================================================================", Diagnostics.LoggingLevel.Verbose);

			// Test each format for display compatibility.
			foreach (var format in formats)
			{
				var giFormat = (SharpDX.DXGI.Format)format;
				SharpDX.DXGI.ModeDescription[] modes = giOutput.GetDisplayModeList(giFormat, SharpDX.DXGI.DisplayModeEnumerationFlags.Scaling | SharpDX.DXGI.DisplayModeEnumerationFlags.Interlaced);

				if ((modes == null) || (modes.Length <= 0))
				{
					continue;
				}

				GorgonVideoMode[] videoModes = (from mode in modes
				                                where (D3DDevice.CheckFormatSupport(giFormat) & D3D.FormatSupport.Display) == D3D.FormatSupport.Display
				                                select GorgonVideoMode.Convert(mode)).ToArray();

				if (videoModes.Length > 0)
				{
					output.VideoModes = new ReadOnlyCollection<GorgonVideoMode>(videoModes);
				}
			}

			// Output to log.
			foreach (var videoMode in output.VideoModes)
			{
				Gorgon.Log.Print("Mode: {0}x{1}, Format: {2}, Refresh Rate: {3}/{4}", Diagnostics.LoggingLevel.Verbose, videoMode.Width, videoMode.Height, videoMode.Format, videoMode.RefreshRateNumerator, videoMode.RefreshRateDenominator);
			}

			Gorgon.Log.Print("===================================================================", Diagnostics.LoggingLevel.Verbose);
			Gorgon.Log.Print("Found {0} video modes for output '{1}'.", Diagnostics.LoggingLevel.Simple, output.VideoModes.Count, output.Name);
		}

		#pragma warning disable 0618
		/// <summary>
		/// Function to retrieve the list of outputs for the video device.
		/// </summary>
		/// <param name="adapter">Adapter containing the outputs.</param>
		/// <param name="D3Ddevice">D3D device to find closest matching mode.</param>
		/// <param name="device">Device used to filter video modes that aren't supported.</param>
		/// <param name="outputCount">The number of outputs attached to the device.</param>
		/// <param name="noOutputDevice">TRUE if the device has no outputs, FALSE if it does.</param>
		private static void GetOutputs(GorgonVideoDevice device, D3D.Device D3Ddevice, SharpDX.DXGI.Adapter adapter, int outputCount, bool noOutputDevice)
		{
			var outputs = new List<GorgonVideoOutput>(outputCount);
			
			// We need to fake outputs.
			// Windows 8 does not support outputs on WARP devices and ref rasterizer devices.
			if (noOutputDevice)
			{
				var output = new GorgonVideoOutput(device);

				Gorgon.Log.Print("Found output {0}.", Diagnostics.LoggingLevel.Simple, output.Name);
				Gorgon.Log.Print("===================================================================", Diagnostics.LoggingLevel.Verbose);
				Gorgon.Log.Print("Output bounds: ({0}x{1})-({2}x{3})", Diagnostics.LoggingLevel.Verbose, output.OutputBounds.Left, output.OutputBounds.Top, output.OutputBounds.Right, output.OutputBounds.Bottom);
				Gorgon.Log.Print("Monitor handle: 0x{0}", Diagnostics.LoggingLevel.Verbose, output.Handle.FormatHex());
				Gorgon.Log.Print("Attached to desktop: {0}", Diagnostics.LoggingLevel.Verbose, output.IsAttachedToDesktop);
				Gorgon.Log.Print("Monitor rotation: {0}\u00B0", Diagnostics.LoggingLevel.Verbose, output.Rotation);
				Gorgon.Log.Print("===================================================================", Diagnostics.LoggingLevel.Verbose);

				outputs.Add(output);

				// No video modes for these devices.
				output.VideoModes = new GorgonVideoMode[0];

				device.Outputs = new GorgonNamedObjectReadOnlyCollection<GorgonVideoOutput>(false, outputs);

				Gorgon.Log.Print("Output {0} on device {1} has no video modes.", Diagnostics.LoggingLevel.Verbose, output.Name, device.Name);
				return;
			}

			// Get outputs.
			for (int i = 0; i < outputCount; i++)
			{
				using (SharpDX.DXGI.Output giOutput = adapter.GetOutput(i))
				{
					var output = new GorgonVideoOutput(giOutput, device, i);

					SharpDX.DXGI.ModeDescription findMode = GorgonVideoMode.Convert(new GorgonVideoMode(output.OutputBounds.Width, output.OutputBounds.Height, BufferFormat.R8G8B8A8_UIntNormal, 60, 1));
					SharpDX.DXGI.ModeDescription result;

					// Get the default (desktop) video mode.
					giOutput.GetClosestMatchingMode(D3Ddevice, findMode, out result);
					output.DefaultVideoMode = GorgonVideoMode.Convert(result);

					GetVideoModes(output, D3Ddevice, giOutput);

					Gorgon.Log.Print("Found output {0}.", Diagnostics.LoggingLevel.Simple, output.Name);
					Gorgon.Log.Print("===================================================================", Diagnostics.LoggingLevel.Verbose);
					Gorgon.Log.Print("Output bounds: ({0}x{1})-({2}x{3})", Diagnostics.LoggingLevel.Verbose, output.OutputBounds.Left, output.OutputBounds.Top, output.OutputBounds.Right, output.OutputBounds.Bottom);
					Gorgon.Log.Print("Monitor handle: 0x{0}", Diagnostics.LoggingLevel.Verbose, output.Handle.FormatHex());
					Gorgon.Log.Print("Attached to desktop: {0}", Diagnostics.LoggingLevel.Verbose, output.IsAttachedToDesktop);
					Gorgon.Log.Print("Monitor rotation: {0}\u00B0", Diagnostics.LoggingLevel.Verbose, output.Rotation);
					Gorgon.Log.Print("===================================================================", Diagnostics.LoggingLevel.Verbose);

					if (output.VideoModes.Count > 0)
					{
						outputs.Add(output);
					}
					else
					{
						Gorgon.Log.Print("Output {0} on device {1} has no video modes!", Diagnostics.LoggingLevel.Verbose, output.Name, device.Name);
					}
				}
			}

			device.Outputs = new GorgonNamedObjectReadOnlyCollection<GorgonVideoOutput>(false, outputs);
		}

		/// <summary>
		/// Function to perform an enumeration of the video devices attached to the system.
		/// </summary>
		/// <param name="enumerateWARPDevice">TRUE to enumerate the WARP software device.  FALSE to exclude it.</param>
		/// <param name="enumerateREFDevice">TRUE to enumerate the reference device.  FALSE to exclude it.</param>
		/// <remarks>This method will populate the <see cref="GorgonLibrary.Graphics.GorgonVideoDeviceEnumerator">GorgonVideoDeviceEnumerator</see> with information about the video devices 
		/// installed in the system.
		/// <para>You may include the WARP device, which is a software based device that emulates most of the functionality of a video device, by setting the <paramref name="enumerateWARPDevice"/> to TRUE.</para>
		/// <para>You may include the reference device, which is a software based device that all the functionality of a video device, by setting the <paramref name="enumerateREFDevice"/> to TRUE.  
		/// If a reference device is used in rendering, the performance will be poor and as such, this device is only useful to diagnosing issues with video drivers.</para>
		/// <para>The reference device is a DEBUG only device, and as such, it will only appear under the following conditions:
		/// <list type="bullet">
		///		<item>
		///			<description>The DEBUG version of the Gorgon library is used.</description>
		///			<description>The Direct 3D SDK is installed.  The reference rasterizer is only included with the SDK.</description>
		///		</item>
		/// </list>
		/// </para>
		/// </remarks>
		public static void Enumerate(bool enumerateWARPDevice, bool enumerateREFDevice)
		{
#if DEBUG
			// Turn on object tracking if it's not already enabled.
			if (!SharpDX.Configuration.EnableObjectTracking)
			{
				SharpDX.Configuration.EnableObjectTracking = true;
			}
#endif
		    try
		    {
		        // Create the DXGI factory object used to gather the information.
		        if (Interlocked.Increment(ref _lockIncr) > 1)
		        {
		            return;
		        }

			    List<GorgonVideoDevice> devices;

			    using(var factory = new SharpDX.DXGI.Factory1())
		        {
		            int adapterCount = factory.GetAdapterCount1();

		            devices = new List<GorgonVideoDevice>(adapterCount + 2);

		            Gorgon.Log.Print("Enumerating video devices...", Diagnostics.LoggingLevel.Simple);

		            // Begin gathering device information.
		            for (int i = 0; i < adapterCount; i++)
		            {
		                // Get the video device information.
		                using(var adapter = factory.GetAdapter1(i))
		                {
		                    // Only enumerate local devices.
		                    int outputCount = adapter.GetOutputCount();

			                if (((adapter.Description1.Flags & SharpDX.DXGI.AdapterFlags.Remote) != 0) || (outputCount <= 0))
			                {
				                continue;
			                }

			                var videoDevice = new GorgonVideoDevice(adapter, VideoDeviceType.Hardware, i);

			                // Don't allow unsupported devices.
			                if (videoDevice.HardwareFeatureLevel == DeviceFeatureLevel.Unsupported)
			                {
				                continue;
			                }

			                // We create a D3D device here to filter out unsupported video modes from the format list.
			                using(var D3DDevice = new D3D.Device(adapter))
			                {
				                D3DDevice.DebugName = "Output enumerator device.";
				                PrintLog(videoDevice);

				                GetOutputs(videoDevice, D3DDevice, adapter, outputCount, false);

				                // Ensure we actually have outputs to use.
				                if (videoDevice.Outputs.Count > 0)
				                {
					                devices.Add(videoDevice);
				                }
				                else
				                {
					                Gorgon.Log.Print("Video device {0} has no outputs!",
					                                 Diagnostics.LoggingLevel.Verbose, videoDevice.Name);
				                }
			                }
		                }
		            }

		            // Get software devices.
		            if (enumerateWARPDevice)
		            {
		                var device = GetWARPSoftwareDevice(devices.Count);

		                if (device.Outputs.Count > 0)
		                {
		                    devices.Add(device);
		                }
		            }

#if DEBUG
		            if (enumerateREFDevice)
		            {
		                var device = GetRefSoftwareDevice(devices.Count);

		                if (device.Outputs.Count > 0)
		                {
		                    devices.Add(device);
		                }
		            }
#endif
		        }

		        VideoDevices = new GorgonNamedObjectReadOnlyCollection<GorgonVideoDevice>(false, devices);

                if (devices.Count == 0)
                {
	                throw new GorgonException(GorgonResult.CannotEnumerate, Resources.GORGFX_NO_SUPPORTED_DEVICES);
                }

		        Gorgon.Log.Print("Found {0} video devices.", Diagnostics.LoggingLevel.Simple, VideoDevices.Count);
		    }
		    finally
		    {
		        Interlocked.Decrement(ref _lockIncr);
		    }
		}
		#pragma warning restore 0618
		#endregion

		#region Constructor/Destructor.
		/// <summary>
		/// Initializes the <see cref="GorgonVideoDeviceEnumerator"/> class.
		/// </summary>
		static GorgonVideoDeviceEnumerator()
		{
			VideoDevices = new GorgonNamedObjectReadOnlyCollection<GorgonVideoDevice>(false, new GorgonVideoDevice[] { });
			Enumerate(false, false);
		}
		#endregion
	}
}
