import 'package:flutter/material.dart';

import 'features/newsfeeds/presentation/pages/splash_page.dart';
import 'injection_container.dart';

void main() async {
  await init();
  runApp(App());
}

class App extends StatelessWidget {
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'League of News',
      theme: ThemeData(
        brightness: Brightness.dark,
        primaryColor: Color(0xff202429),
        accentColor: Color(0xffcca75c),
        primaryColorDark: Color(0xff1a1d21),
        fontFamily: 'Montserrat',
      ),
      home: SplashPage(),
    );
  }
}
