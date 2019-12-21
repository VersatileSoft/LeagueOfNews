import 'package:flutter/material.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/app_config.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/website.dart';
import 'package:league_of_news/features/newsfeeds/presentation/widgets/newsfeeds_list.dart';
import 'package:league_of_news/injection_container.dart';

class MainPage extends StatefulWidget {
  @override
  _MainPageState createState() => _MainPageState();
}

class _MainPageState extends State<MainPage> with TickerProviderStateMixin {
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
      drawer: Drawer(
        child: ListView.builder(
          padding: EdgeInsets.zero,
          itemCount: appConfig.websites.length + 1,
          itemBuilder: (context, index) {
            if (index == 0) {
              return SafeArea(
                child: DrawerHeader(
                  child: Container(),
                  decoration: BoxDecoration(
                    image: DecorationImage(
                      image: AssetImage('assets/images/icon.png'),
                      fit: BoxFit.contain,
                    ),
                  ),
                ),
              );
            } else {
              var website = appConfig.websites[index - 1];
              return ListTile(
                title: Text(website.name),
                onTap: () {
                  Navigator.pop(context);
                  setState(() {
                    selectedPageId = website.id;
                  });
                },
              );
            }
          },
        ),
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
