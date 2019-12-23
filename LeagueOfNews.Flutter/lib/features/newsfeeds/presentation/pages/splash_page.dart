import 'package:flutter/material.dart';
import 'package:league_of_news/core/usecases/usecase.dart';
import 'package:league_of_news/features/newsfeeds/domain/usecases/get_app_config.dart';

import '../../../../injection_container.dart';
import 'main_page.dart';

class SplashPage extends StatefulWidget {
  SplashPage({Key key}) : super(key: key);

  @override
  _SplashPageState createState() => _SplashPageState();
}

class _SplashPageState extends State<SplashPage> {
  final GetAppConfig getAppConfig = sl();

  @override
  void initState() {
    _init();
    super.initState();
  }

  _init() {
    getAppConfig(NoParams()).then((value) {
      value.fold((failure) {
        showDialog(
          context: context,
          builder: (_) {
            return AlertDialog(
              title: Text('No internet connection'),
              content: Text('Check your internet connection and try agin'),
              actions: <Widget>[
                FlatButton(
                  child: Text('Try agin'),
                  onPressed: () {
                    Navigator.pop(context);
                    _init();
                  },
                )
              ],
            );
          },
        );
      }, (appConfig) {
        Navigator.of(context)
            .pushReplacement(MaterialPageRoute(builder: (_) => MainPage()));
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Color(0xff202429),
      body: Column(
        mainAxisSize: MainAxisSize.max,
        mainAxisAlignment: MainAxisAlignment.center,
        children: <Widget>[
          Center(
            child: Image.asset('assets/images/icon.png'),
          ),
          CircularProgressIndicator(),
        ],
      ),
    );
  }
}
