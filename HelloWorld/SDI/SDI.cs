using System;
using System.Windows.Forms;
using System.Drawing;

class SDIForm : Form
{
  public SDIForm()
  {
    TextBox client = new TextBox();
    client.Dock = DockStyle.Fill;
    client.AutoSize = false;
    
    this.Controls.Add(client);
  }
  
  [STAThreadAttribute]
  public static void Main(String [] Args)
  {
    Application.Run(new SDIForm());
  }
}  