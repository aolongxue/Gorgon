// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "GorgonLibrary.IO.GorgonChunkedFormat.#TempBuffer")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "397*previousHash", Scope = "member", Target = "GorgonLibrary.GorgonHashGenerationExtension.#GenerateHash`1(System.Int32,!!0)")]
[assembly: SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "GorgonLibrary.Native.Win32API.GetWindowThreadProcessId(System.IntPtr,System.UInt32@)", Scope = "member", Target = "GorgonLibrary.Diagnostics.GorgonProcess.#GetActiveProcess()")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "startPointIndex+1", Scope = "member", Target = "GorgonLibrary.Math.GorgonSpline.#GetInterpolatedValue(System.Int32,System.Single)")]
[assembly: SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "GorgonLibrary.Native.Win32API.GetWindowThreadProcessId(System.IntPtr,System.UInt32@)", Scope = "member", Target = "GorgonLibrary.Diagnostics.GorgonProcess.#GetProcessByWindow(System.IntPtr)")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "-139095488+previousHash", Scope = "member", Target = "GorgonLibrary.GorgonHashGenerationExtension.#GenerateHash`1(System.Int32,!!0)")]
