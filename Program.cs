using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

internal static class Program
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    private static bool ConsoleVisible = true;

    [STAThread]
    static void Main()
    {
        // Hide the console window at startup
        var handle = GetConsoleWindow();
        ShowWindow(handle, SW_HIDE);
        ConsoleVisible = false;

        ApplicationConfiguration.Initialize();

        var form = new Form
        {
            Text = "Hybrid WinForms App",
            Width = 800,
            Height = 600,
            StartPosition = FormStartPosition.CenterScreen,
            BackColor = Color.LightSteelBlue
        };

        var button = new Button
        {
            Text = "Show Console",
            Location = new Point(350, 250),
            AutoSize = true
        };

        button.Click += (sender, args) =>
        {
            if (ConsoleVisible)
            {
                ShowWindow(handle, SW_HIDE);
                ConsoleVisible = false;
                return;
            }
            ShowWindow(handle, SW_SHOW);
            ConsoleVisible = true;
        };

        form.Controls.Add(button);

        Application.Run(form);
    }
}
