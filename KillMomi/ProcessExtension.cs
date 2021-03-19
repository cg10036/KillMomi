using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace KillMomi
{// Token: 0x02000005 RID: 5
	public static class ProcessExtension
	{
		// Token: 0x0600002E RID: 46
		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenThread(ProcessExtension.ThreadAccess A_0, bool A_1, uint A_2);

		// Token: 0x0600002F RID: 47
		[DllImport("kernel32.dll")]
		private static extern uint SuspendThread(IntPtr A_0);

		// Token: 0x06000030 RID: 48
		[DllImport("kernel32.dll")]
		private static extern int ResumeThread(IntPtr A_0);

		// Token: 0x06000031 RID: 49
		[DllImport("kernel32.dll")]
		public static extern int TerminateProcess(IntPtr hProcess, int ExitCode);

		// Token: 0x06000032 RID: 50
		[DllImport("kernel32.dll")]
		public static extern void CloseHandle(IntPtr hProcess);

		// Token: 0x06000033 RID: 51
		[DllImport("kernel32.dll")]
		private static extern bool TerminateThread(IntPtr A_0, uint A_1);

		// Token: 0x06000034 RID: 52
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr OpenProcess(int Access, bool InheritHandle, int ProcessId);

		// Token: 0x06000035 RID: 53
		[DllImport("user32.dll")]
		private static extern bool PostMessage(IntPtr A_0, uint A_1, int A_2, int A_3);

		// Token: 0x06000036 RID: 54
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostThreadMessage(uint threadId, uint msg, UIntPtr wParam, IntPtr lParam);

		// Token: 0x06000037 RID: 55 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public static void Suspend(Process process)
		{
			foreach (object obj in process.Threads)
			{
				ProcessThread processThread = (ProcessThread)obj;
				try
				{
					IntPtr intPtr = ProcessExtension.OpenThread(ProcessExtension.ThreadAccess.SUSPEND_RESUME, false, (uint)processThread.Id);
					ProcessExtension.SuspendThread(intPtr);
					ProcessExtension.CloseHandle(intPtr);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004B30 File Offset: 0x00002D30
		public static void Resume(Process process)
		{
			foreach (object obj in process.Threads)
			{
				ProcessThread processThread = (ProcessThread)obj;
				try
				{
					IntPtr intPtr = ProcessExtension.OpenThread(ProcessExtension.ThreadAccess.SUSPEND_RESUME, false, (uint)processThread.Id);
					ProcessExtension.ResumeThread(intPtr);
					ProcessExtension.CloseHandle(intPtr);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public static void Kills(Process process)
		{
			foreach (object obj in process.Threads)
			{
				ProcessThread processThread = (ProcessThread)obj;
				try
				{
					IntPtr intPtr = ProcessExtension.OpenThread(ProcessExtension.ThreadAccess.TERMINATE, false, (uint)processThread.Id);
					ProcessExtension.TerminateThread(intPtr, 0U);
					ProcessExtension.CloseHandle(intPtr);
					ProcessExtension.PostThreadMessage((uint)processThread.Id, ProcessExtension.a, UIntPtr.Zero, IntPtr.Zero);
				}
				catch
				{
				}
			}
			try
			{
				IntPtr hProcess = ProcessExtension.OpenProcess(1, false, process.Id);
				ProcessExtension.TerminateProcess(hProcess, 0);
				ProcessExtension.CloseHandle(hProcess);
				ProcessExtension.PostMessage(process.MainWindowHandle, ProcessExtension.a, 0, 0);
				process.CloseMainWindow();
				process.Kill();
			}
			catch
			{
			}
		}

		// Token: 0x04000019 RID: 25
		private static uint a = 16U;

		// Token: 0x0400001A RID: 26
		public const int PROCESS_TERMINATE = 1;

		// Token: 0x02000006 RID: 6
		public enum ThreadAccess
		{
			// Token: 0x0400001C RID: 28
			TERMINATE = 1,
			// Token: 0x0400001D RID: 29
			SUSPEND_RESUME,
			// Token: 0x0400001E RID: 30
			GET_CONTEXT = 8,
			// Token: 0x0400001F RID: 31
			SET_CONTEXT = 16,
			// Token: 0x04000020 RID: 32
			SET_INFORMATION = 32,
			// Token: 0x04000021 RID: 33
			QUERY_INFORMATION = 64,
			// Token: 0x04000022 RID: 34
			SET_THREAD_TOKEN = 128,
			// Token: 0x04000023 RID: 35
			IMPERSONATE = 256,
			// Token: 0x04000024 RID: 36
			DIRECT_IMPERSONATION = 512
		}
	}
}