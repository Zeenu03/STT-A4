// Form1.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimeColorChanger
{
    public partial class Form1 : Form
    {
        private DateTime targetTime;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000; // 1 second
            timer1.Tick += Timer1_Tick;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (TimeSpan.TryParse(txtTime.Text.Trim(), out TimeSpan ts))
            {
                targetTime = DateTime.Today.Add(ts);

                // If target is already past for today, assume tomorrow
                if (targetTime <= DateTime.Now)
                {
                    targetTime = targetTime.AddDays(1);
                }

                btnStart.Enabled = false;
                txtTime.Enabled = false;
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Please enter a valid time in HH:MM:SS format.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTime.Focus();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Change background to a random color
            this.BackColor = Color.FromArgb(
                random.Next(256),
                random.Next(256),
                random.Next(256));

            // Check if we've reached or passed the target time
            if (DateTime.Now >= targetTime)
            {
                timer1.Stop();
                MessageBox.Show($"Target time reached: {targetTime:HH:mm:ss}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnStart.Enabled = true;
                txtTime.Enabled = true;
            }
        }
    }
