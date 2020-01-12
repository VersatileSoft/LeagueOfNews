import 'package:flutter/material.dart';

import 'features/newsfeeds/presentation/pages/splash_page.dart';
import 'features/newsfeeds/presentation/utils/theme_data.dart';
import 'injection_container.dart';

void main() async {
  await init();
  runApp(App());
}

class App extends StatefulWidget {
  @override
  _AppState createState() => _AppState();
}

class _AppState extends State<App> {

  @override
  void initState() {
    super.initState();
  }

  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'League of News',
      theme: themeData,
      home: SplashPage(),
    );
  }
}
