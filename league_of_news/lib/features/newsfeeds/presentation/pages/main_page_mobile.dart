import 'package:flutter/material.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/app_config.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/website.dart';
import 'package:league_of_news/features/newsfeeds/presentation/widgets/app_drawer.dart';
import 'package:league_of_news/features/newsfeeds/presentation/widgets/newsfeeds_list.dart';
import 'package:league_of_news/injection_container.dart';

class MainPageMobile extends StatefulWidget {
  @override
  _MainPageMobileState createState() => _MainPageMobileState();
}

class _MainPageMobileState extends State<MainPageMobile>
    with TickerProviderStateMixin {
  final AppConfig appConfig = sl();
  int selectedPageId = 0;

  @override
  Widget build(BuildContext context) {
    var selectedWebsite =
        appConfig.websites.firstWhere((w) => w.id == selectedPageId);
    var tabController = selectedWebsite.subpages != null
        ? TabController(length: selectedWebsite.subpages.length, vsync: this)
        : null;

    return Scaffold(
      appBar: AppBar(
        title: Text(
          appConfig.websites.firstWhere((w) => w.id == selectedPageId).name,
        ),
        bottom: selectedWebsite.subpages != null
            ? TabBar(
                isScrollable: true,
                controller: tabController,
                tabs: List.generate(selectedWebsite.subpages.length, (index) {
                  return Tab(
                    text: selectedWebsite.subpages[index].name,
                  );
                }),
              )
            : null,
      ),
      drawer: AppDrawer(
        appConfig: appConfig,
        onItemTap: (websiteId) {
          Navigator.pop(context);
          setState(() {
            selectedPageId = websiteId;
          });
        },
      ),
      body: selectedWebsite.subpages != null
          ? _buildTabBarView(selectedWebsite, tabController)
          : NewsfeedsList(
              websiteId: selectedWebsite.id,
            ),
    );
  }

  _buildTabBarView(Website website, tabController) {
    return TabBarView(
      controller: tabController,
      children: List.generate(
        website.subpages.length,
        (index) {
          return NewsfeedsList(
            websiteId: website.subpages[index].id,
          );
        },
      ),
    );
  }
}
