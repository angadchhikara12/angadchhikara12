namespace Windows_Crash_meme;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        SuspendLayout();

        // Main launcher window
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, 200);

        Text = "Free Robux";

        StartPosition = FormStartPosition.CenterScreen;

        BackColor = Color.White;

        FormBorderStyle = FormBorderStyle.FixedDialog;

        MaximizeBox = false;
        MinimizeBox = false;


        ResumeLayout(false);
    }

    #endregion
}