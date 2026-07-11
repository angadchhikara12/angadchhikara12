namespace Windows_Crash_meme;

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private Timer? spawnTimer;

    private int errorCount = 1;
    private int delay = 500;

    private readonly Random random = new();
    private readonly int errorCountLimit = 500;

    private readonly List<Form> errorWindows = new();


    public Form1()
    {
        InitializeComponent();

        KeyPreview = true;
        KeyDown += Form1_KeyDown;

        _ = Run_batch();
    }


    private async Task Run_batch()
    {
        string batPath = ExtractBatchFile();


        if (!File.Exists(batPath))
        {
            MessageBox.Show(
                "amir.bat was not found.",
                "Missing File",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            return;
        }


        try
        {
            Process? process = Process.Start(new ProcessStartInfo
            {
                FileName = batPath,
                UseShellExecute = true
            });


            if (process != null)
            {
                await process.WaitForExitAsync();
            }


            StartErrorSpam();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Batch Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }



    private void StartErrorSpam()
    {
        if (InvokeRequired)
        {
            Invoke(StartErrorSpam);
            return;
        }


        spawnTimer = new Timer
        {
            Interval = delay
        };


        spawnTimer.Tick += SpawnError;
        spawnTimer.Start();
    }



    private void Form1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Control &&
            e.Shift &&
            e.Alt &&
            e.KeyCode == Keys.Escape)
        {
            spawnTimer?.Stop();


            foreach (Form window in errorWindows.ToList())
            {
                window.Close();
            }


            MessageBox.Show(
                "Developer escape sequence activated.\n\n" +
                "SUPER WINDOWS ERROR defeated.\n\n" +
                "Amir has cancelled the delivery.",
                "Secret Ending",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );


            Application.Exit();
        }
    }

    private string ExtractBatchFile()
    {
        string tempPath = Path.Combine(
            Path.GetTempPath(),
            "amir.bat"
        );


        using Stream? stream =
            typeof(Form1).Assembly.GetManifestResourceStream(
                "Windows_Crash_meme.Assets.amir.bat"
            );


        if (stream == null)
            throw new Exception("Embedded BAT not found.");


        using FileStream file =
            new FileStream(
                tempPath,
                FileMode.Create,
                FileAccess.Write
            );


        stream.CopyTo(file);


        return tempPath;
    }

    private void SpawnError(object? sender, EventArgs e)
    {
        errorCount++;

        CreateErrorWindow();


        if (delay > 50)
        {
            delay -= 50;

            if (delay < 50)
                delay = 50;


            if (spawnTimer != null)
                spawnTimer.Interval = delay;
        }



        if (errorCount >= errorCountLimit)
        {
            spawnTimer?.Stop();

            DialogResult result = MessageBox.Show(
                "Nothing happened.\n\n" +
                "Your PC is safe.\n\n" +
                "Bruh, don't download sketchy files.\n" +
                "It isn't worth risking your data\n" +
                "and your device. Have a good day!",
                "Safety Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.OK)
            {
                // Close every fake error window
                foreach (Form window in errorWindows.ToList())
                {
                    window.Close();
                }

                errorWindows.Clear();

                // Close the main form
                Close();

                // Exit the application
                Application.Exit();
            }
        }
    }

    private Image LoadEmbeddedImage()
    {
        using Stream? stream =
            typeof(Form1).Assembly.GetManifestResourceStream(
                "Windows_Crash_meme.Assets.image-removebg-preview.png"
            );


        if (stream == null)
            return SystemIcons.Error.ToBitmap();


        return Image.FromStream(stream);
    }

    private void CreateErrorWindow()
    {
        Form error = new Form
        {
            Text = "Virus Detected (0x5K1LL-155U3)",
            ClientSize = new Size(430, 170),

            StartPosition = FormStartPosition.Manual,
            FormBorderStyle = FormBorderStyle.FixedDialog,

            MaximizeBox = false,
            MinimizeBox = false,

            BackColor = Color.White,
            ShowInTaskbar = false
        };



        error.Location = new Point(
            random.Next(
                0,
                Screen.PrimaryScreen!.Bounds.Width - error.Width
            ),

            random.Next(
                0,
                Screen.PrimaryScreen!.Bounds.Height - error.Height
            )
        );



        Image imagePath = LoadEmbeddedImage();


        PictureBox icon = new PictureBox
        {
            Image = LoadEmbeddedImage(),

            SizeMode = PictureBoxSizeMode.Zoom,

            Size = new Size(65, 65),

            Location = new Point(10, 22)
        };



        Label message = new Label
        {
            Text =
                "A virus has been detected.",

            Font = new Font(
                "Segoe UI",
                9F
            ),

            AutoSize = false,

            Size = new Size(320, 55),

            Location = new Point(70, 25)
        };



        Button okButton = new Button
        {
            Text = "OK",

            Size = new Size(100, 32),

            Location = new Point(300, 120)
        };



        error.Controls.Add(icon);
        error.Controls.Add(message);
        error.Controls.Add(okButton);



        errorWindows.Add(error);



        error.FormClosed += (s, e) =>
        {
            errorWindows.Remove(error);
        };



        error.Show();


        error.Shown += (s, e) =>
        {
            error.ActiveControl = null;
        };
    }
}