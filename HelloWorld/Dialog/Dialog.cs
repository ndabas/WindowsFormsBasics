namespace HelloWorldTest
{
  using System;
  using System.Windows.Forms;
  using System.Drawing;
  
  class HelloDialog : Form
  {
    [STAThread]
    public static void Main(string[] args)
    {
      Application.Run(new HelloDialog());
    }
    
    public HelloDialog()
    {
      Button but = new Button();
      but.Text = "Click Me";
      but.Location = new Point(74, 75);
      but.Size = new Size(60, 40);
      but.Click += new EventHandler(Button_Click);
      
      this.Controls.Add(but);
    }
    
    protected void Button_Click(object sender, EventArgs e)
    {
      MessageBox.Show("You clicked!");
    }
  }
}  