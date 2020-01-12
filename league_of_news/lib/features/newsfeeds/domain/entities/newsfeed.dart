import 'package:meta/meta.dart';

class Newsfeed {
  final String urlToNewsfeed;
  final String date;
  final String title;
  final String shortDescription;
  final String imageUrl;

  Newsfeed({
    @required this.urlToNewsfeed,
    @required this.date,
    @required this.title,
    @required this.shortDescription,
    @required this.imageUrl,
  });
}
