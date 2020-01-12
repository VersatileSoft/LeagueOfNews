import 'package:flutter/material.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/app_config.dart';

class AppDrawer extends StatelessWidget {
  final AppConfig appConfig;
  final Function(int websiteId) onItemTap;
  AppDrawer({Key key, this.appConfig, this.onItemTap}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Drawer(
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
              onTap: () => onItemTap(website.id),
            );
          }
        },
      ),
    );
  }
}
