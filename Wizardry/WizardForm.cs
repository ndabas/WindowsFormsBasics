namespace ND.Wizardry
{
  using System;
  using System.Windows.Forms;
  using System.Drawing;
  using System.Collections;
  using System.ComponentModel;
  using System.Resources;
  
  public enum WizardResult
  {
    Cancelled, // User cancelled or closed the wizard.
    Finished   // User finished.
  }
  
  public enum WizardButton
  {
    Cancel,
    Next,
    Back,
    Finish
  }
  
  public class WizardButtonClickEventArgs : CancelEventArgs
  {
    // The button clicked by the user.
    public WizardButton Button
    {
      get { return _Button; }
    }
    internal WizardButton _Button;
    
    public WizardButtonClickEventArgs(WizardButton clickedButton)
    {
      this.Cancel = false;
      this._Button = clickedButton;
    }
  }
  
  public delegate void WizardButtonClickEventHandler(object sender,
    WizardButtonClickEventArgs e);
  
  public class WizardForm : Form
  {
    // A collection of all the pages in the wizard.
    public class PageCollection : Form.ControlCollection
    {
      internal WizardForm _Wizard;
      public WizardForm Wizard
      {
        get { return _Wizard; }
      }
      
      public PageCollection(WizardForm owner) : base(new Form())
      {
        _Wizard = owner;
      }
      
      public void Add(WizardPage value)
      {
        value._Wizard = Wizard;
        base.Add(value);
      }
      
      public void Remove(WizardPage value)
      {
        base.Remove(value);
      }
      
      public void AddRange(WizardPage[] pages)
      {
        foreach(WizardPage page in pages)
        {
          page._Wizard = Wizard;
        }
        base.AddRange(pages);
      }
      
      public new WizardPage this[int index]
      {
        get { return (WizardPage)base[index]; }
      }
    }
    
    internal PageCollection _Pages;
    public PageCollection Pages
    {
      get { return _Pages; }
    }
    
    // Wizard controls
    protected ContainerControl ButtonPanel;
    protected Button WizCancelButton;
    protected Button WizNextButton;
    protected Button WizBackButton;
    protected Button WizFinishButton;
    
    // Other wizard UI elements
    protected ContainerControl TitlePanel;
    protected Label TitleLabel;
    protected Label SubtitleLabel;
    protected Panel Line1;
    protected Panel Line2;
    
    // The current wizard page
    internal WizardPage _CurrentPage;
    public WizardPage CurrentPage
    {
      get { return _CurrentPage; }
      set
      {
        if(value != CurrentPage)
        {
          CancelEventArgs deactivatevt = new CancelEventArgs();
          CurrentPage.OnDeactivate(deactivatevt);
          
          if(!deactivatevt.Cancel)
          {
            this.Controls.Clear();
            
            // Put the current page back into the Pages collection.
            if(CurrentPage.PageIndex >= 0)
            {
              CurrentPage.TextChanged -= new EventHandler(CurrentPage_TextChanged);
              Pages.Add(CurrentPage);
              Pages.SetChildIndex(CurrentPage, CurrentPage.PageIndex);
            }
            
            _CurrentPage = value;
            CurrentPage.PageIndex = Pages.GetChildIndex(CurrentPage);
            Pages.Remove(CurrentPage);
            
            // Set the title and subtitle text.
            if(CurrentPage.Subtitle != null)
              SubtitleLabel.Text = CurrentPage.Subtitle;
            if(CurrentPage.Title != null)
              TitleLabel.Text = CurrentPage.Title;
            
            // Only add the title panel if it is an interior page.
            this.Controls.Add(CurrentPage);
            if(CurrentPage.PageType == WizardPageType.Interior)
              this.Controls.Add(TitlePanel);
            this.Controls.Add(ButtonPanel);
            
            WizBackButton.Enabled = (CurrentPage.PageIndex == 0) ? false : true;
            WizNextButton.Enabled = (CurrentPage.PageIndex == Pages.Count) ? false : true;
            this.AcceptButton = (WizNextButton.Enabled ? WizNextButton : WizFinishButton);
            
            CurrentPage.TextChanged += new EventHandler(CurrentPage_TextChanged);
            CurrentPage.OnActivated(new EventArgs());
            this.Text = CurrentPage.Text;
          }
        }
      }
    }
    
    // The WizardButtonClick Event.
    public event WizardButtonClickEventHandler WizardButtonClick;
    protected virtual void OnWizardButtonClick(WizardButtonClickEventArgs e)
    {
      if(WizardButtonClick != null)
      {
        WizardButtonClick(this, e);
      }
    }
    
    public WizardForm()
    {
      // All strings are in a resource.
      ResourceManager resMan = new ResourceManager(typeof(WizardForm));
      
      this._Pages = new PageCollection(this);
      this._CurrentPage = new WizardPage();
      
      this.ButtonPanel = new ContainerControl();
      this.WizCancelButton = new Button();
      this.WizNextButton = new Button();
      this.WizBackButton = new Button();  
      this.WizFinishButton = new Button();
      
      this.Size = new Size(600, 420);
      this.Font = new Font("Tahoma", this.Font.Size);
      
      this.Closing += new CancelEventHandler(WizardForm_Closing);
      
      this.TitlePanel = new ContainerControl();
      this.TitleLabel = new Label();
      this.SubtitleLabel = new Label();
      
      Line1 = new Panel();
      Line1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      Line1.Dock = DockStyle.Bottom;
      Line1.Size = new Size(this.Width, 1);
      
      Line2 = new Panel();
      Line2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      Line2.Location = new Point(0, 0);
      Line2.Dock = DockStyle.Top;
      Line2.Size = new Size(this.Width, 1);
      
      TitlePanel.BackColor = Color.White;
      TitlePanel.Dock = DockStyle.Top;
      TitlePanel.Size = new Size(this.Width, 60);
      
      TitleLabel.Location = new Point(10, 10);
      TitleLabel.AutoSize = true;
      TitleLabel.Font = new Font(this.Font, FontStyle.Bold);
      
      SubtitleLabel.Location = new Point(30, 30);
      SubtitleLabel.AutoSize = true;
      
      TitlePanel.Controls.Add(TitleLabel);
      TitlePanel.Controls.Add(SubtitleLabel);
      TitlePanel.Controls.Add(Line1);
      
      WizFinishButton.Text = resMan.GetString("FinishButtonText");
      WizFinishButton.FlatStyle = FlatStyle.Flat;
      WizFinishButton.Click += new EventHandler(WizFinishButton_Click);
      WizFinishButton.DialogResult = DialogResult.OK;
      WizFinishButton.Anchor = AnchorStyles.Right;
      WizFinishButton.Location = 
        new Point(this.Width - WizFinishButton.Width - 10, 10);
      
      WizNextButton.Text = resMan.GetString("NextButtonText");
      WizNextButton.FlatStyle = FlatStyle.Flat;
      WizNextButton.Click += new EventHandler(WizNextButton_Click);
      WizNextButton.Anchor = AnchorStyles.Right;
      WizNextButton.Location = 
        new Point(this.Width - WizNextButton.Width -
         WizFinishButton.Width - 20, 10);
      
      WizBackButton.Text = resMan.GetString("BackButtonText");
      WizBackButton.FlatStyle = FlatStyle.Flat;
      WizBackButton.Click += new EventHandler(WizBackButton_Click);
      WizBackButton.Anchor = AnchorStyles.Right;
      WizBackButton.Location = 
        new Point(this.Width - WizBackButton.Width -
         WizNextButton.Width - WizFinishButton.Width - 20, 10);
      
      WizCancelButton.Text = resMan.GetString("CancelButtonText");
      WizCancelButton.FlatStyle = FlatStyle.Flat;
      WizCancelButton.Click += new EventHandler(WizCancelButton_Click);
      WizCancelButton.DialogResult = DialogResult.Cancel;
      WizCancelButton.Anchor = AnchorStyles.Right;
      WizCancelButton.Location = 
        new Point(this.Width - WizCancelButton.Width -
         WizBackButton.Width - WizNextButton.Width -
         WizFinishButton.Width - 30, 10);
      
      ButtonPanel.Dock = DockStyle.Bottom;
      ButtonPanel.Size = new Size(this.Width, 40);
      
      ButtonPanel.Controls.Add(Line2);
      ButtonPanel.Controls.Add(WizCancelButton);
      ButtonPanel.Controls.Add(WizBackButton);
      ButtonPanel.Controls.Add(WizNextButton);
      ButtonPanel.Controls.Add(WizFinishButton);
      
      this.CancelButton = WizCancelButton;
      this.AcceptButton = WizNextButton;
    }
    
    // Displays the wizard.
    public WizardResult ShowWizard()
    {
      Size newSize = new Size(0, 0);
      
      // Adjust the wizard size according to the largest
      // page added.
      foreach(Control curPage in Pages)
      {
        if(curPage.Size.Height > newSize.Height)
          newSize.Height = curPage.Size.Height;
        if(curPage.Size.Width > newSize.Width)
          newSize.Width = curPage.Size.Width;
      }
      
      CurrentPage = Pages[0];
      
      if(ShowDialog() == DialogResult.OK)
        return WizardResult.Finished;
      else
        return WizardResult.Cancelled;
    }
    
    // Enable or Disable the buttons
    
    public void EnableFinish(bool enable)
    {
      WizFinishButton.Enabled = enable;
    }
    
    public void EnableBack(bool enable)
    {
      WizBackButton.Enabled = enable;
    }
    
    public void EnableNext(bool enable)
    {
      WizNextButton.Enabled = enable;
    }
    
    public void EnableCancel(bool enable)
    {
      WizCancelButton.Enabled = enable;
    }
    
    
    // Event Handlers for the UI.
    
    protected void WizNextButton_Click(object sender, EventArgs e)
    {
      WizardButtonClickEventArgs wizEvt = new WizardButtonClickEventArgs(WizardButton.Next);
      OnWizardButtonClick(wizEvt);
      if(wizEvt.Cancel)
        return;
      
      Int32 newIndex = CurrentPage.PageIndex;
      CurrentPage = Pages[newIndex];
    }
    
    protected void WizBackButton_Click(object sender, EventArgs e)
    {
      WizardButtonClickEventArgs wizEvt = new WizardButtonClickEventArgs(WizardButton.Back);
      OnWizardButtonClick(wizEvt);
      if(wizEvt.Cancel)
        return;
      
      Int32 newIndex = CurrentPage.PageIndex - 1;
      CurrentPage = Pages[newIndex];
    }
    
    protected void WizFinishButton_Click(object sender, EventArgs e)
    {
      WizardButtonClickEventArgs wizEvt = new WizardButtonClickEventArgs(WizardButton.Finish);
      OnWizardButtonClick(wizEvt);
      if(wizEvt.Cancel)
        this.DialogResult = DialogResult.None;
    }
    
    protected void WizCancelButton_Click(object sender, EventArgs e)
    {
      WizardButtonClickEventArgs wizEvt = new WizardButtonClickEventArgs(WizardButton.Cancel);
      OnWizardButtonClick(wizEvt);
      if(wizEvt.Cancel)
        this.DialogResult = DialogResult.None;
    }
    
    protected void WizardForm_Closing(object sender, CancelEventArgs e)
    {
      CancelEventArgs closingevt =  new CancelEventArgs();
      CurrentPage.OnDeactivate(closingevt);
      e.Cancel = closingevt.Cancel;
    }
    
    protected void CurrentPage_TextChanged(object sender, EventArgs e)
    {
      this.Text = CurrentPage.Text;
    }
  }
  
}
