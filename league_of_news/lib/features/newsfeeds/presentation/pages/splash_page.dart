import 'package:flutter/material.dart';
import 'package:league_of_news/core/usecases/usecase.dart';
import 'package:league_of_news/features/newsfeeds/domain/usecases/get_app_config.dart';
import 'package:league_of_news/features/newsfeeds/presentation/pages/main_page_desktop.dart';
import 'package:league_of_news/features/newsfeeds/presentation/pages/main_page_mobile.dart';
import 'package:responsive_builder/responsive_builder.dart';
import '../../../../injection_container.dart';

class SplashPage extends StatefulWidget {
  SplashPage({Key key}) : super(key: key);

  @override
  _SplashPageState createState() => _SplashPageState();
}

class _SplashPageState extends State<SplashPage> {
  final GetAppConfig getAppConfig = sl();

  @override
  void initState() {
    super.initState();
    _init();
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
        Navigator.of(context).pushReplacement(
          MaterialPageRoute(
            builder: (_) => ScreenTypeLayout(
              desktop: MainPageDesktop(),
              tablet: MainPageDesktop(),
              mobile: MainPageMobile(),
            ),
          ),
        );
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
