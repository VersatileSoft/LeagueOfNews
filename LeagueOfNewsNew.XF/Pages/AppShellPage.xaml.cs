using System;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.PageModels;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF.Pages
{
    public partial class AppShellPage : Shell
    {
        public AppShellPage()
        {
            InitializeComponent();
            this.SetPageModel();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            AppShellPageModel pageModel = (AppShellPageModel)BindingContext;
            foreach (Website website in pageModel.Websites)
            {
                Tab tab = new Tab
                {
                    Title = website.Name,
                    Icon = ImageSource.FromUri(new Uri($"{App.ICONS_URL}/android/{website.Icon}.png"))
                };

                if (website.Subpages != null)
                {
                    foreach (Website subPage in website.Subpages)
                    {
                        tab.Items.Add(new ShellContent
                        {
                            Title = subPage.Name,
                            Content = new NewsfeedListPage(subPage.Id)
                        });
                    }
                }
                else
                {
                    tab.Items.Add(new ShellContent
                    {
                        Content = new NewsfeedListPage(website.Id)
                    });
                }
                TabBar.Items.Add(tab);
            }
        }
    }
}