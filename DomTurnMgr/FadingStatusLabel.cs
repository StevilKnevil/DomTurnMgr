using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace DomTurnMgr
{
  public partial class FadingStatusLabel : ToolStripStatusLabel
  {
    public FadingStatusLabel()
    {
      InitializeComponent();
    }

    public override string Text
    {
      set
      {
        base.Text = value;
        if(!DesignMode)
          startFade();
      }
    }

    private float opacity = 1;
    private DateTime startTime;
    public float Duration { get; set; }

    private void startFade()
    {
      opacity = 1;
      this.Invalidate();
      timer1.Stop();
      timer1.Start();
      startTime = DateTime.Now;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Font font = this.Font;
      Color textColor = Color.FromArgb((int)(opacity*255F), this.ForeColor);
      SolidBrush drawBrush = new SolidBrush(textColor);
      e.Graphics.DrawString(this.Text, font, drawBrush, new Point(0, 0));
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      TimeSpan ts = DateTime.Now.Subtract(startTime);
      float factor = ts.Ticks / (Duration * TimeSpan.TicksPerSecond);
      opacity = 1 - factor;
      if (opacity > 1) opacity = 1;
      if (opacity < 0) opacity = 0;

      // Force redraw
      this.Invalidate();
    }
  }
}
