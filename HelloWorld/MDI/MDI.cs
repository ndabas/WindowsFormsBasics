using System;
using System.Windows.Forms;
using System.Drawing;

class MDIChildForm : Form
{
  public MDIChildForm()
  {
    TextBox client = new TextBox();
    client.Dock = DockStyle.Fill;
    client.AutoSize = false;
    
    this.Controls.Add(client);
  }
}

class MDIParentForm : Form
{
  public MDIParentForm()
  {
    this.IsMdiContainer = true;
    NewDoc();
    
    MainMenu mnu = new MainMenu();
    
    MenuItem itm = mnu.MenuItems.Add("&File");
    itm.MenuItems.Add(new MenuItem("&New", new EventHandler(FileNew_Click), Shortcut.CtrlO));
    
    itm = mnu.MenuItems.Add("&Window");
    itm.MenuItems.Add("&Cascade", new EventHandler(WindowCascade_Click));
    itm.MenuItems.Add("Tile &Horizontally", new EventHandler(WindowTileH_Click));
    itm.MenuItems.Add("Tile &Vertically", new EventHandler(WindowTileV_Click));
    itm.MdiList = true;
    
    this.Menu = mnu;
  }
  
  protected void FileNew_Click(object sender, EventArgs e)
  {
    NewDoc();
  }
  
  protected void WindowCascade_Click(object sender, EventArgs e)
  {
    this.LayoutMdi(MdiLayout.Cascade);
  }

  protected void WindowTileH_Click(object sender, EventArgs e)
  {
    this.LayoutMdi(MdiLayout.TileHorizontal);
  }

  protected void WindowTileV_Click(object sender, EventArgs e)
  {
    this.LayoutMdi(MdiLayout.TileVertical);
  }
  
  protected virtual void NewDoc()
  {
    MDIChildForm child = new MDIChildForm();
    child.MdiParent = this;
    child.Show();
  }
  
  [STAThreadAttribute]
  public static void Main(String [] Args)
  {
    Application.Run(new MDIParentForm());
  }
}