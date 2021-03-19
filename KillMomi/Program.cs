using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace KillMomi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("잠시만 기다려주세요...");
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "Windows");
                FileInfo[] files = directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                foreach (FileInfo fileInfo in files)
                {
                    if (Path.GetFileNameWithoutExtension(fileInfo.FullName).ToLower().StartsWith("processhide"))
                    {
                        try
                        {
                            fileInfo.Attributes = FileAttributes.Normal;
                            string fullName = fileInfo.FullName;
                            if (Path.GetExtension(fullName).Contains("exe"))
                            {
                                new FileInfo(Directory.GetCurrentDirectory() + "\\Command.exe")
                                {
                                    Attributes = FileAttributes.Normal
                                }.CopyTo(fullName, true);
                                Console.WriteLine("Copy Success");
                            }
                            if (Path.GetExtension(fullName).Contains("dll"))
                            {
                                new FileInfo(Directory.GetCurrentDirectory() + "\\CommandDLL.dll")
                                {
                                    Attributes = FileAttributes.Normal
                                }.CopyTo(fullName, true);
                                Console.WriteLine("Copy Success");
                            }
                            FileInfo fileInfo2 = new FileInfo(fullName);
                            fileInfo2.Attributes = (FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            List<string> lists = new List<string>();
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "Windows");
                FileInfo[] files = directoryInfo.GetFiles("*.exe", SearchOption.TopDirectoryOnly);
                foreach (FileInfo fileInfo2 in files)
                {
                    try
                    {
                        string fullName = fileInfo2.FullName;
                        X509Certificate2 x509Certificate = new X509Certificate2(File.ReadAllBytes(fullName));
                        string subject = x509Certificate.Subject;
                        if (subject.Contains("JNESS") || subject.Contains("제이니스"))
                        {
                            lists.Add(fullName);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "Windows\\SysWOW64");
                FileInfo[] files = directoryInfo.GetFiles("*.exe", SearchOption.TopDirectoryOnly);
                foreach (FileInfo fileInfo2 in files)
                {
                    try
                    {
                        string fullName = fileInfo2.FullName;
                        X509Certificate2 x509Certificate = new X509Certificate2(File.ReadAllBytes(fullName));
                        string subject = x509Certificate.Subject;
                        if (subject.Contains("JNESS") || subject.Contains("제이니스"))
                        {
                            lists.Add(fullName);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "Windows\\System32");
                FileInfo[] files = directoryInfo.GetFiles("*.exe", SearchOption.TopDirectoryOnly);
                foreach (FileInfo fileInfo2 in files)
                {
                    try
                    {
                        string fullName = fileInfo2.FullName;
                        X509Certificate2 x509Certificate = new X509Certificate2(File.ReadAllBytes(fullName));
                        string subject = x509Certificate.Subject;
                        if (subject.Contains("JNESS") || subject.Contains("제이니스"))
                        {
                            lists.Add(fullName);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine(lists.Count);
            foreach (string path in lists)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName.StartsWith(Path.GetFileNameWithoutExtension(path)))
                    {
                        try
                        {
                            ProcessExtension.Kills(process);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
            }
            Console.WriteLine("완료되었습니다.");
            Console.ReadLine();
        }
    }
}