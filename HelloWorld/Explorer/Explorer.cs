using System;
using System.Windows.Forms;
using System.Drawing;

class ExplorerTest : Form
{
  [STAThread]
  public static void Main(string[] args)
  {
    Application.Run(new ExplorerTest());
  }
  
  public ExplorerTest()
  {
    ContainerControl left = new ContainerControl();
    ContainerControl right = new ContainerControl();
    Splitter s = new Splitter();
    Label l = new Label();
    Label r = new Label();
    
    left.Dock = DockStyle.Left;
    left.Size = new Size(100, 100);
    left.BackColor = Color.Blue;
    
    l.Text = "The Left control.";
    
    left.Controls.Add(l);
    
    s.Dock = DockStyle.Left;
    s.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    
    right.Dock = DockStyle.Fill;
    right.Size = new Size(100, 100);
    right.BackColor = Color.Red;
    
    r.Text = "The Right control.";
    
    right.Controls.Add(r);
    
    this.Controls.Add(right);
    this.Controls.Add(s);
    this.Controls.Add(left);
  }
}  