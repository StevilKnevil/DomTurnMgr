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
      Duration = 10.0f;
    }

    public override string Text
    {
      set
      {
        base.Text = value;
        startFade();
      }
    }

    private float opacity = 0;
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
#if true
      if (opacity > 0)
        base.OnPaint(e);
#else
      // need to resolve: https://stackoverflow.com/questions/48405796/overloading-control-drawing-in-c-sharp-has-text-rendering-problems
      // Create a temp image to draw to and then put that onto the control transparently
      using (Bitmap bmp = new Bitmap(this.Width, this.Height))
      {
        using (Graphics newGraphics = Graphics.FromImage(bmp))
        {
          newGraphics.Clear(this.BackColor);
          PaintEventArgs newEvent = new PaintEventArgs(newGraphics, e.ClipRectangle);
          newGraphics.CompositingMode = CompositingMode.SourceCopy;
          base.OnPaint(newEvent);
          
          //ColorMatrix colorMatrix = new ColorMatrix();
          //colorMatrix.Matrix33 = opacity;

          //ImageAttributes imgAttr = new ImageAttributes();
          //imgAttr.SetColorMatrix(
          //  colorMatrix);

          e.Graphics.Clear(this.BackColor);
          e.Graphics.CompositingMode = CompositingMode.SourceOver;
          e.Graphics.DrawImage(bmp, new Rectangle(0, 0, this.Width, this.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel);//, imgAttr);
        }
      }
#endif
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
