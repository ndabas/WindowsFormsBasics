namespace ND.Wizardry
{
  using System;
  using System.Windows.Forms;
  using System.Drawing;
  using System.ComponentModel;
  
  public enum WizardPageType
  {
    Welcome,    // A welcome page.
    Completion, // A completion page.
    Interior    // An interior page.
  }
  
  public class WizardPage : ContainerControl
  {
    public WizardPageType PageType
    {
      get { return _PageType; }
      set
      {
        _PageType = value;
        
        if((_PageType == WizardPageType.Welcome)
           || (_PageType == WizardPageType.Completion))
        {
          this.BackColor = Color.FromKnownColor(KnownColor.Window);
        }
      }
    }    
    internal WizardPageType _PageType;
    
    internal WizardForm _Wizard;
    public WizardForm Wizard
    {
      get { return _Wizard; }
    }
    
    public String Title;
    public String Subtitle;
    
    // The index of this page in the Pages collection.
    internal Int32 PageIndex;
    
    public event EventHandler Activated;
    public event CancelEventHandler Deactivate;
    
    protected internal virtual void OnActivated(EventArgs e)
    {
      if(Activated != null)
      {
        Activated(this, e);
      }
    }
    
    protected internal virtual void OnDeactivate(CancelEventArgs e)
    {
      if(Deactivate != null)
      {
        Deactivate(this, e);
      }
    }
    
    public WizardPage()
    {
      this.Dock = DockStyle.Fill;
      this.Size = new Size(360, 500);
      this.PageType = WizardPageType.Interior;
      this.PageIndex = -1;
    }
    
  }
  
}