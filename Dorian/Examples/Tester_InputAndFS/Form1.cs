﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GorgonLibrary;
using GorgonLibrary.UI;
using GorgonLibrary.Diagnostics;
using GorgonLibrary.PlugIns;
using GorgonLibrary.Graphics;
using GorgonLibrary.HID;
using GorgonLibrary.FileSystem;

namespace Tester
{
	public partial class Form1 : Form
	{
		GorgonInputDeviceFactory input = null;
		GorgonInputDeviceFactory xinput = null;
		GorgonPointingDevice mouse = null;
		GorgonKeyboard keyboard = null;
		GorgonFileSystem fileSystem = null;
		GorgonJoystick joystick = null;
		GorgonTimer pulseTimer = null;
		bool pulse = false;
		Random rnd = new Random();

		private bool Idle(GorgonFrameRate timing)
		{
			labelMouse.Text = mouse.Position.X.ToString() + "x" + mouse.Position.Y.ToString() + "\n\n";

			if (joystick != null)
			{
				joystick.Poll();

				labelMouse.Text = string.Format("Left Stick: {0}x{1} ({8})  Right stick:{2}x{3} ({9})  Rudder:{4}  Throttle:{5}\nPOV: {6} POV Direction: {7}\n", 
						joystick.X, joystick.Y, joystick.SecondaryX, joystick.SecondaryY, joystick.Rudder, joystick.Throttle, joystick.POV, joystick.Direction.POV, joystick.Direction.X|joystick.Direction.Y, joystick.Direction.SecondaryX|joystick.Direction.SecondaryY);

				for (int i = 0; i < joystick.Capabilities.ButtonCount; i++)
					labelMouse.Text += "Button :" + joystick.Button[i].Name + " " + joystick.Button[i].IsPressed.ToString() + "\n";

				if (((joystick.Capabilities.ExtraCapabilities & JoystickCapabilityFlags.SupportsVibration) == JoystickCapabilityFlags.SupportsVibration) && (joystick.Button["A"].IsPressed))
				{
					pulseTimer = new GorgonTimer();
				}

				if (((joystick.Capabilities.ExtraCapabilities & JoystickCapabilityFlags.SupportsVibration) == JoystickCapabilityFlags.SupportsVibration) && (pulseTimer == null))
				{
					float normalize1 = (float)joystick.Rudder;
					float normalize2 = (float)joystick.Throttle;

					normalize1 = (float)Math.Pow(normalize1 + 1.0f, 2);
					normalize2 = (float)Math.Pow(normalize2 + 1.0f, 2);

					if (normalize1 > 65535.0f)
						normalize1 = 65535.0f;
					if (normalize2 > 65535.0f)
						normalize2 = 65535.0f;

					if (joystick.Rudder > 0)
						joystick.Vibrate(0, (int)normalize1);
					else
						joystick.Vibrate(0, 0);

					if (joystick.Throttle > 0)
						joystick.Vibrate(1, (int)normalize2);
					else
						joystick.Vibrate(1, 0);
				}

				if (((joystick.Capabilities.ExtraCapabilities & JoystickCapabilityFlags.SupportsVibration) == JoystickCapabilityFlags.SupportsVibration) && (joystick.Button["B"].IsPressed))
				{
					joystick.Vibrate(0, 0);
					joystick.Vibrate(1, 0);
					pulseTimer = null;
				}

				if (pulseTimer != null)
				{
					if (pulseTimer.Milliseconds > 1000)
					{
						pulse = !pulse;
						pulseTimer.Reset();
					}

					if (pulse)
					{
						joystick.Vibrate(0, rnd.Next(joystick.Capabilities.VibrationMotorRanges[0].Minimum, joystick.Capabilities.VibrationMotorRanges[0].Maximum));
						joystick.Vibrate(1, 0);
					}
					else
					{
						joystick.Vibrate(0, 0);
						joystick.Vibrate(1, rnd.Next(joystick.Capabilities.VibrationMotorRanges[0].Minimum, joystick.Capabilities.VibrationMotorRanges[0].Maximum));
					}
				}

			}

			return true;
		}
		
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			try 
			{
				Gorgon.Initialize(this);
				GorgonPlugInFactory.SearchPaths.Add(@"..\..\..\..\PlugIns\bin\debug");				
				GorgonPlugInFactory.LoadPlugInAssembly(@"Gorgon.HID.RawInput.dll");
				GorgonPlugInFactory.LoadPlugInAssembly(@"Gorgon.HID.XInput.dll");
				GorgonPlugInFactory.LoadPlugInAssembly(@"Gorgon.FileSystem.Zip.dll");
				GorgonPlugInFactory.LoadPlugInAssembly(@"Gorgon.FileSystem.BZ2Packfile.dll");
				input = GorgonHIDFactory.CreateInputDeviceFactory("GorgonLibrary.HID.GorgonRawInput");
				xinput = GorgonHIDFactory.CreateInputDeviceFactory("GorgonLibrary.HID.GorgonXInput");

				mouse = input.CreatePointingDevice();
				keyboard = input.CreateKeyboard();

				foreach (GorgonInputDeviceName name in xinput.JoystickDevices)
				{
					if (name.IsConnected)
					{
						joystick = xinput.CreateJoystick(name);
						break;
					}
				}

				if (joystick == null) 
				{
					foreach (GorgonInputDeviceName name in input.JoystickDevices)
					{
						if (name.IsConnected)
						{
							joystick = input.CreateJoystick(name);
							break;
						}
					}
				}

				if (joystick != null)
				{
					joystick.DeadZone.X = new GorgonLibrary.Math.GorgonMinMax(-2500, 2500);
					joystick.DeadZone.Y = new GorgonLibrary.Math.GorgonMinMax(-2500, 2500);
					joystick.DeadZone.SecondaryX = new GorgonLibrary.Math.GorgonMinMax(-2500, 2500);
					joystick.DeadZone.SecondaryY = new GorgonLibrary.Math.GorgonMinMax(-2500, 2500);
				}

				fileSystem = new GorgonFileSystem();
				fileSystem.AddProvider("GorgonLibrary.FileSystem.GorgonZipFileSystemProvider");
				fileSystem.AddProvider("GorgonLibrary.FileSystem.GorgonBZ2FileSystemProvider");
				fileSystem.Mount(System.IO.Path.GetPathRoot(Application.ExecutablePath) + @"unpak\", "/FS");
				fileSystem.Mount(System.IO.Path.GetPathRoot(Application.ExecutablePath) + @"unpak\ParilTest.zip", "/Zip");
				fileSystem.Mount(System.IO.Path.GetPathRoot(Application.ExecutablePath) + @"unpak\BZipFileSystem.gorPack");

				System.IO.Stream stream = fileSystem.GetFile("/Shaders/Blur.fx").OpenStream(false);
				byte[] streamFile = new byte[stream.Length];
				stream.Read(streamFile, 0, (int)stream.Length);
				byte[] file = fileSystem.GetFile("/Shaders/Cloak.fx").Read();
				
				Gorgon.Go(Idle);
			}
			catch (Exception ex)
			{
				GorgonException.Catch(ex, () => GorgonDialogs.ErrorBox(this, ex));
				Close();
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);

			if (input != null)
				input.Dispose();
			Gorgon.Terminate();
		}

		public Form1()
		{
			InitializeComponent();
		}
	}
}
