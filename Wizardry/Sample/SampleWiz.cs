namespace ND.Wizardry.SampleWizard
{
  using System;
  using System.Windows.Forms;
  using System.Drawing;
  using ND.Wizardry;
  
  class WizPage1 : WizardPage
  {
    Label TitleLabel;
    Label DescriptionLabel;
    
    public WizPage1()
    {
      this.PageType = WizardPageType.Welcome;
      this.Text = "Welcome to the Pizza Wizard!";
      
      TitleLabel = new Label();
      TitleLabel.Text = "Welcome to the Pizza Wizard!";
      TitleLabel.Font = new Font(this.Font.FontFamily, 16, FontStyle.Bold);
      TitleLabel.Location = new Point(200, 30);
      TitleLabel.Size = new Size(260, 60);
      
      DescriptionLabel = new Label();
      DescriptionLabel.Text = "The Pizza Wizard will guide you " +
        "through ordering a pizza. Click Next to continue.";
      DescriptionLabel.Location = new Point(200, 120);
      DescriptionLabel.Size = new Size(260, 200);
      
      this.Controls.Add(TitleLabel);
      this.Controls.Add(DescriptionLabel);
      
      this.Activated += new EventHandler(Page_Activated);
    }
    
    protected void Page_Activated(object sender, EventArgs e)
    {
      Wizard.EnableFinish(false);
    }
  }
  
  class WizPage2 : WizardPage
  {
    Label Avail;
    CheckBox Onions;
    CheckBox Capsicum;
    CheckBox HotChicken;
    CheckBox Pepperoni;
    CheckBox Anchovies;
    CheckBox ExtraCheese;
    
    public WizPage2()
    {
      this.Text = "Favorite Toppings";
      this.Title = "Pizza Options";
      this.Subtitle = "Select your favorite pizza toppings.";
      
      Avail = new Label();
      Avail.Size = new Size(this.Width - 40, 40);
      Avail.Text = "Available toppings for your pizza:";
      Avail.Location = new Point(20, 20);
      
      Onions = new CheckBox();
      Onions.Text = "Onions";
      Onions.Location = new Point(20, 40);
      
      Capsicum = new CheckBox();
      Capsicum.Text = "Capsicum";
      Capsicum.Location = new Point(20, 60);
      
      HotChicken = new CheckBox();
      HotChicken.Text = "Hot'n'Spicy Chicken";
      HotChicken.Size = new Size(300, HotChicken.Size.Height);
      HotChicken.Location = new Point(20, 80);
      
      Pepperoni = new CheckBox();
      Pepperoni.Text = "Pepperoni";
      Pepperoni.Location = new Point(20, 100);
      
      Anchovies = new CheckBox();
      Anchovies.Text = "Anchovies";
      Anchovies.Location = new Point(20, 120);
      
      ExtraCheese = new CheckBox();
      ExtraCheese.Text = "Extra Cheese";
      ExtraCheese.Location = new Point(20, 140);
      
      this.Controls.Add(Avail);
      this.Controls.Add(Onions);
      this.Controls.Add(Capsicum);
      this.Controls.Add(HotChicken);
      this.Controls.Add(Pepperoni);
      this.Controls.Add(Anchovies);
      this.Controls.Add(ExtraCheese);
    }
  }
  
  class WizPage3 : WizardPage
  {
    Label Crusts;
    RadioButton Classic;
    RadioButton DeepDish;
    RadioButton Light;
    
    public WizPage3()
    {
      this.Text = "Favorite Crust";
      this.Title = "Pizza Options";
      this.Subtitle = "Select your favorite pizza crust and base.";
      
      Crusts = new Label();
      Crusts.Text = "What style of pizza base and crust would you like?";
      Crusts.Size = new Size(this.Width - 40, 40);
      Crusts.Location = new Point(20, 20);
      
      Classic = new RadioButton();
      Classic.Text = "Classic";
      Classic.Location = new Point(20, 60);
      
      DeepDish = new RadioButton();
      DeepDish.Text = "Deep Dish";
      DeepDish.Location = new Point(20, 80);
      
      Light = new RadioButton();
      Light.Text = "Light and Crunchy";
      Light.Size = new Size(300, Light.Size.Height);
      Light.Location = new Point(20, 100);
      
      this.Controls.Add(Crusts);
      this.Controls.Add(Classic);
      this.Controls.Add(DeepDish);
      this.Controls.Add(Light);
    }
  }
  
  class WizPage4 : WizardPage
  {
    Label TitleLabel;
    Label DescriptionLabel;
    
    public WizPage4()
    {
      this.PageType = WizardPageType.Welcome;
      this.Text = "Ready to Order!";
      
      TitleLabel = new Label();
      TitleLabel.Text = "Ready to Order!";
      TitleLabel.Font = new Font(this.Font.FontFamily, 16, FontStyle.Bold);
      TitleLabel.Location = new Point(200, 30);
      TitleLabel.Size = new Size(260, 60);
      
      DescriptionLabel = new Label();
      DescriptionLabel.Text = "You are now ready to order your pizza. " +
        "To review your selections and make any changes, click Back. Click Finish to order your pizza.";
      DescriptionLabel.Location = new Point(200, 120);
      DescriptionLabel.Size = new Size(260, 200);
      
      this.Controls.Add(TitleLabel);
      this.Controls.Add(DescriptionLabel);
      this.Activated += new EventHandler(Page_Activated);
      this.Deactivate += new System.ComponentModel.CancelEventHandler(Page_Deactivate);
    }
    
    protected void Page_Activated(object sender, EventArgs e)
    {
      Wizard.EnableFinish(true);
    }
    
    protected void Page_Deactivate(object sender, System.ComponentModel.CancelEventArgs e)
    {
      Wizard.EnableFinish(false);
    }
  }
  
  class SampleWizard : WizardForm
  {
    WizPage1 page1;
    WizPage2 page2;
    WizPage3 page3;
    WizPage4 page4;
    
    public SampleWizard()
    {
      page1 = new WizPage1();
      page2 = new WizPage2();
      page3 = new WizPage3();
      page4 = new WizPage4();
      
      this.Pages.Add(page1);
      this.Pages.Add(page2);
      this.Pages.Add(page3);
      this.Pages.Add(page4);
    }
    
    public static void Main(String[] args)
    {
      SampleWizard wiz = new SampleWizard();
      WizardResult res = wiz.ShowWizard();
    }
  }
}  