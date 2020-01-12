import 'package:flutter/material.dart';
import 'package:league_of_news/custom_tabs_plugin/flutter_custom_tabs.dart';
import 'package:league_of_news/features/newsfeeds/presentation/utils/theme_data.dart';

void launchURL(String url, BuildContext context) async {
  try {
    await launch(
      url,
      option: new CustomTabsOption(
          toolbarColor: themeData.primaryColor,
          enableDefaultShare: true,
          enableUrlBarHiding: true,
          showPageTitle: true,
          enableInstantApps: true,
          animation: new CustomTabsAnimation.slideIn()
      ),
    );
  } catch (e) {
    Scaffold.of(context).showSnackBar(
      SnackBar(content: Text('Browser is required to open news')),
    );
  }
}