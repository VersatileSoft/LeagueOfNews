import 'package:league_of_news/features/newsfeeds/data/models/website_model.dart';
import 'package:meta/meta.dart';

class Website {
  final int id;
  final String name;
  final List<WebsiteModel> subpages;
  final String url;

  Website({
    @required this.id,
    @required this.name,
    @required this.subpages,
    @required this.url,
  });
}
