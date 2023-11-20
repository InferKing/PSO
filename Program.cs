using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class FlyingObject
{
    public Point Position { get; set; }
    public int Speed { get; set; } = 1;
}

public class MainForm : Form
{
    private List<FlyingObject> objects;
    private bool isGathered = false;
    private bool isMoving = false;
    private Point gatherPoint;
    private Point targetPoint;
    private System.Windows.Forms.Timer timer;

    public MainForm()
    {
        objects = new List<FlyingObject>();
        var random = new Random();

        for (int i = 0; i < 10; i++)
        {
            objects.Add(new FlyingObject { Position = new Point(random.Next(200), random.Next(200)) });
        }

        var gatherButton = new Button { Text = "Сбор V", Location = new Point(10, 10) };
        gatherButton.Click += (sender, e) =>
        {
            var gatherForm = new GatherForm();
            if (gatherForm.ShowDialog() == DialogResult.OK)
            {
                gatherPoint = gatherForm.GatherPoint;
                isGathered = true;
            }
        };
        Controls.Add(gatherButton);

        var moveButton = new Button { Text = "Старт", Location = new Point(100, 10) };
        moveButton.Click += (sender, e) =>
        {
            var targetForm = new TargetForm();
            if (targetForm.ShowDialog() == DialogResult.OK)
            {
                targetPoint = targetForm.TargetPoint;
                isMoving = true;
            }
        };
        Controls.Add(moveButton);

        timer = new System.Windows.Forms.Timer();
        timer.Interval = 100;
        timer.Tick += (sender, e) => Invalidate();
        timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (isGathered)
        {
            var leader = objects[0]; 
            for (int i = 1; i < objects.Count; i++)
            {
                var position = objects[i].Position;

                var targetX = leader.Position.X + (i - objects.Count / 2) * 10;
                if (position.X < targetX)
                    position.X += objects[i].Speed;
                else if (position.X > targetX)
                    position.X -= objects[i].Speed;

                var targetY = leader.Position.Y;
                if (position.Y < targetY)
                    position.Y += objects[i].Speed;
                else if (position.Y > targetY)
                    position.Y -= objects[i].Speed;

                objects[i].Position = position;
            }
        }

        if (isMoving)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                var position = objects[i].Position;

                if (position.X < targetPoint.X)
                    position.X += objects[i].Speed;
                else if (position.X > targetPoint.X)
                    position.X -= objects[i].Speed;

                if (position.Y < targetPoint.Y)
                    position.Y += objects[i].Speed;
                else if (position.Y > targetPoint.Y)
                    position.Y -= objects[i].Speed;

                if (position.X < 0) position.X = ClientSize.Width;
                if (position.Y < 0) position.Y = ClientSize.Height;
                if (position.X > ClientSize.Width) position.X = 0;
                if (position.Y > ClientSize.Height) position.Y = 0;

                objects[i].Position = position;
            }
        }

        foreach (var obj in objects)
        {
            e.Graphics.FillEllipse(Brushes.Red, obj.Position.X, obj.Position.Y, 10, 10);
        }
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}

public class GatherForm : Form
{
    public Point GatherPoint { get; set; }

    public GatherForm()
    {
        var okButton = new Button { Text = "OK", Location = new Point(10, 70) };
        okButton.Click += (sender, e) => DialogResult = DialogResult.OK;

        var xBox = new TextBox { Location = new Point(10, 10) };
        var yBox = new TextBox { Location = new Point(10, 40) };

        Controls.Add(okButton);
        Controls.Add(xBox);
        Controls.Add(yBox);

        FormClosing += (sender, e) =>
        {
            GatherPoint = new Point(int.Parse(xBox.Text), int.Parse(yBox.Text));
        };
    }
}

public class TargetForm : Form
{
    public Point TargetPoint { get; set; }

    public TargetForm()
    {
        var okButton = new Button { Text = "OK", Location = new Point(10, 70) };
        okButton.Click += (sender, e) => DialogResult = DialogResult.OK;

        var xBox = new TextBox { Location = new Point(10, 10) };
        var yBox = new TextBox { Location = new Point(10, 40) };

        Controls.Add(okButton);
        Controls.Add(xBox);
        Controls.Add(yBox);

        FormClosing += (sender, e) =>
        {
            TargetPoint = new Point(int.Parse(xBox.Text), int.Parse(yBox.Text));
        };
    }
}