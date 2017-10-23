namespace Browser.Views
{
    using System.Windows.Forms;

    partial class Browser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Browser));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.Favicons = new System.Windows.Forms.ImageList(this.components);
            this.SideBar = new System.Windows.Forms.TabControl();
            this.FavoritesTab = new System.Windows.Forms.TabPage();
            this.Favorites = new BindableListView();
            this.FavoritesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FavoritesUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HistoryTab = new System.Windows.Forms.TabPage();
            this.History = new BindableListView();
            this.Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newIncognitoTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goHomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsHomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FavoritesRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditFavoritesListItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFavoritesListItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HistoryRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.HistoryRightClickAddToFavoritesPressed = new System.Windows.Forms.ToolStripMenuItem();
            this.HistoryRightClickOpenPressed = new System.Windows.Forms.ToolStripMenuItem();
            this.SideBar.SuspendLayout();
            this.FavoritesTab.SuspendLayout();
            this.HistoryTab.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.FavoritesRightClickMenu.SuspendLayout();
            this.HistoryRightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.AllowDrop = true;
            resources.ApplyResources(this.Tabs, "Tabs");
            this.Tabs.ImageList = this.Favicons;
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.TabStop = false;
            this.Tabs.SelectedIndexChanged += new System.EventHandler(this.OnSelectedTabIndexChanged);
            this.Tabs.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.OnCloseTabButtonPressed);
            this.Tabs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnTabsMouseClick);
            // 
            // Favicons
            // 
            this.Favicons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Favicons.ImageStream")));
            this.Favicons.TransparentColor = System.Drawing.Color.Transparent;
            this.Favicons.Images.SetKeyName(0, "nofavicon.gif");
            // 
            // SideBar
            // 
            resources.ApplyResources(this.SideBar, "SideBar");
            this.SideBar.Controls.Add(this.FavoritesTab);
            this.SideBar.Controls.Add(this.HistoryTab);
            this.SideBar.ImageList = this.Favicons;
            this.SideBar.Name = "SideBar";
            this.SideBar.SelectedIndex = 0;
            // 
            // FavoritesTab
            // 
            this.FavoritesTab.Controls.Add(this.Favorites);
            this.FavoritesTab.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.FavoritesTab, "FavoritesTab");
            this.FavoritesTab.Name = "FavoritesTab";
            this.FavoritesTab.UseVisualStyleBackColor = true;
            // 
            // Favorites
            // 
            this.Favorites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FavoritesName,
            this.FavoritesUrl});
            this.Favorites.DataMember = null;
            this.Favorites.DataSource = null;
            resources.ApplyResources(this.Favorites, "Favorites");
            this.Favorites.Name = "Favorites";
            this.Favorites.UseCompatibleStateImageBehavior = false;
            this.Favorites.View = System.Windows.Forms.View.Details;
            this.Favorites.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnFavoritesListClick);
            this.Favorites.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnFavoritesListDoubleClicked);
            // 
            // FavoritesName
            // 
            resources.ApplyResources(this.FavoritesName, "FavoritesName");
            // 
            // FavoritesUrl
            // 
            resources.ApplyResources(this.FavoritesUrl, "FavoritesUrl");
            // 
            // HistoryTab
            // 
            this.HistoryTab.Controls.Add(this.History);
            resources.ApplyResources(this.HistoryTab, "HistoryTab");
            this.HistoryTab.Name = "HistoryTab";
            this.HistoryTab.UseVisualStyleBackColor = true;
            // 
            // History
            // 
            resources.ApplyResources(this.History, "History");
            this.History.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Title,
            this.Date});
            this.History.DataMember = null;
            this.History.DataSource = null;
            this.History.Name = "History";
            this.History.UseCompatibleStateImageBehavior = false;
            this.History.View = System.Windows.Forms.View.Details;
            this.History.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnHistoryListClick);
            this.History.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnHistoryListDoubleClicked);
            // 
            // Title
            // 
            resources.ApplyResources(this.Title, "Title");
            // 
            // Date
            // 
            resources.ApplyResources(this.Date, "Date");
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem});
            resources.ApplyResources(this.MainMenu, "MainMenu");
            this.MainMenu.Name = "MainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTabToolStripMenuItem,
            this.newWindowToolStripMenuItem,
            this.closeTabToolStripMenuItem,
            this.quitToolStripMenuItem,
            this.newIncognitoTabToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newTabToolStripMenuItem
            // 
            this.newTabToolStripMenuItem.Name = "newTabToolStripMenuItem";
            resources.ApplyResources(this.newTabToolStripMenuItem, "newTabToolStripMenuItem");
            this.newTabToolStripMenuItem.Click += new System.EventHandler(this.OnNewTabButtonPressed);
            // 
            // newWindowToolStripMenuItem
            // 
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            resources.ApplyResources(this.newWindowToolStripMenuItem, "newWindowToolStripMenuItem");
            this.newWindowToolStripMenuItem.Click += new System.EventHandler(this.OnNewWindowButtonPressed);
            // 
            // closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            resources.ApplyResources(this.closeTabToolStripMenuItem, "closeTabToolStripMenuItem");
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.OnCloseTabButtonPressed);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            resources.ApplyResources(this.quitToolStripMenuItem, "quitToolStripMenuItem");
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.OnCloseWindowButtonPressed);
            // 
            // newIncognitoTabToolStripMenuItem
            // 
            this.newIncognitoTabToolStripMenuItem.Name = "newIncognitoTabToolStripMenuItem";
            resources.ApplyResources(this.newIncognitoTabToolStripMenuItem, "newIncognitoTabToolStripMenuItem");
            this.newIncognitoTabToolStripMenuItem.Click += new System.EventHandler(this.OnNewIncognitoTabButtonPressed);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextTabToolStripMenuItem,
            this.previousTabToolStripMenuItem,
            this.goHomeToolStripMenuItem,
            this.setAsHomeToolStripMenuItem,
            this.reloadTabToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            resources.ApplyResources(this.windowToolStripMenuItem, "windowToolStripMenuItem");
            // 
            // nextTabToolStripMenuItem
            // 
            this.nextTabToolStripMenuItem.Name = "nextTabToolStripMenuItem";
            resources.ApplyResources(this.nextTabToolStripMenuItem, "nextTabToolStripMenuItem");
            this.nextTabToolStripMenuItem.Click += new System.EventHandler(this.OnNextTabButtonPressed);
            // 
            // previousTabToolStripMenuItem
            // 
            this.previousTabToolStripMenuItem.Name = "previousTabToolStripMenuItem";
            resources.ApplyResources(this.previousTabToolStripMenuItem, "previousTabToolStripMenuItem");
            this.previousTabToolStripMenuItem.Click += new System.EventHandler(this.OnPrevTabButtonPressed);
            // 
            // goHomeToolStripMenuItem
            // 
            resources.ApplyResources(this.goHomeToolStripMenuItem, "goHomeToolStripMenuItem");
            this.goHomeToolStripMenuItem.Name = "goHomeToolStripMenuItem";
            this.goHomeToolStripMenuItem.Click += new System.EventHandler(this.OnGoHomeButtonPressed);
            // 
            // setAsHomeToolStripMenuItem
            // 
            this.setAsHomeToolStripMenuItem.Name = "setAsHomeToolStripMenuItem";
            resources.ApplyResources(this.setAsHomeToolStripMenuItem, "setAsHomeToolStripMenuItem");
            this.setAsHomeToolStripMenuItem.Click += new System.EventHandler(this.OnSetAsHomeButtonPressed);
            // 
            // reloadTabToolStripMenuItem
            // 
            this.reloadTabToolStripMenuItem.Name = "reloadTabToolStripMenuItem";
            resources.ApplyResources(this.reloadTabToolStripMenuItem, "reloadTabToolStripMenuItem");
            this.reloadTabToolStripMenuItem.Click += new System.EventHandler(this.OnReloadTabButtonPressed);
            // 
            // FavoritesRightClickMenu
            // 
            this.FavoritesRightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.FavoritesRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditFavoritesListItem,
            this.OpenFavoritesListItem});
            this.FavoritesRightClickMenu.Name = "FavoritesRightClickMenu";
            this.FavoritesRightClickMenu.ShowImageMargin = false;
            resources.ApplyResources(this.FavoritesRightClickMenu, "FavoritesRightClickMenu");
            // 
            // EditFavoritesListItem
            // 
            this.EditFavoritesListItem.Name = "EditFavoritesListItem";
            resources.ApplyResources(this.EditFavoritesListItem, "EditFavoritesListItem");
            this.EditFavoritesListItem.Click += new System.EventHandler(this.EditFavoritesItem);
            // 
            // OpenFavoritesListItem
            // 
            this.OpenFavoritesListItem.Name = "OpenFavoritesListItem";
            resources.ApplyResources(this.OpenFavoritesListItem, "OpenFavoritesListItem");
            this.OpenFavoritesListItem.Click += new System.EventHandler(this.OpenFavoritesItem);
            // 
            // HistoryRightClickMenu
            // 
            this.HistoryRightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.HistoryRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HistoryRightClickAddToFavoritesPressed,
            this.HistoryRightClickOpenPressed});
            this.HistoryRightClickMenu.Name = "FavoritesRightClickMenu";
            this.HistoryRightClickMenu.ShowImageMargin = false;
            resources.ApplyResources(this.HistoryRightClickMenu, "HistoryRightClickMenu");
            // 
            // HistoryRightClickAddToFavoritesPressed
            // 
            this.HistoryRightClickAddToFavoritesPressed.Name = "HistoryRightClickAddToFavoritesPressed";
            resources.ApplyResources(this.HistoryRightClickAddToFavoritesPressed, "HistoryRightClickAddToFavoritesPressed");
            this.HistoryRightClickAddToFavoritesPressed.Click += new System.EventHandler(this.SaveHistoryItem);
            // 
            // HistoryRightClickOpenPressed
            // 
            this.HistoryRightClickOpenPressed.Name = "HistoryRightClickOpenPressed";
            resources.ApplyResources(this.HistoryRightClickOpenPressed, "HistoryRightClickOpenPressed");
            this.HistoryRightClickOpenPressed.Click += new System.EventHandler(this.OpenHistoryItem);
            // 
            // Browser
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SideBar);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.MainMenu);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Browser";
            this.SideBar.ResumeLayout(false);
            this.FavoritesTab.ResumeLayout(false);
            this.HistoryTab.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.FavoritesRightClickMenu.ResumeLayout(false);
            this.HistoryRightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabControl SideBar;
        private System.Windows.Forms.TabPage FavoritesTab;
        private System.Windows.Forms.TabPage HistoryTab;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goHomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newIncognitoTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsHomeToolStripMenuItem;
        private System.Windows.Forms.ImageList Favicons;
        private System.Windows.Forms.ContextMenuStrip FavoritesRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem EditFavoritesListItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFavoritesListItem;
        private System.Windows.Forms.ToolStripMenuItem reloadTabToolStripMenuItem;
        private BindableListView History;
        private System.Windows.Forms.ColumnHeader Title;
        private System.Windows.Forms.ColumnHeader Date;
        private BindableListView Favorites;
        private System.Windows.Forms.ColumnHeader FavoritesName;
        private System.Windows.Forms.ColumnHeader FavoritesUrl;
        private ContextMenuStrip HistoryRightClickMenu;
        private ToolStripMenuItem HistoryRightClickAddToFavoritesPressed;
        private ToolStripMenuItem HistoryRightClickOpenPressed;
    }
}

