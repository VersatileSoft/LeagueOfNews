
import 'package:league_of_news/features/newsfeeds/domain/entities/website.dart';
import 'package:meta/meta.dart';

class WebsiteModel extends Website {
  WebsiteModel({
    @required id,
    @required name,
    @required subpages,
    @required url,
  }) : super(
          id: id,
          name: name,
          subpages: subpages,
          url: url,
        );

  factory WebsiteModel.fromJson(Map<String, dynamic> json) {
    var subpages;
    if (json['subpages'] != null) {
      subpages = new List<WebsiteModel>();
      json['subpages'].forEach((v) {
        subpages.add(new WebsiteModel.fromJson(v));
      });
    }

    return WebsiteModel(
      id: json['id'],
      name: json['name'],
      url: json['url'],
      subpages: subpages,
    );
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['name'] = this.name;
    if (this.subpages != null) {
      data['subpages'] = this.subpages.map((v) => v.toJson()).toList();
    }
    data['url'] = this.url;
    return data;
  }
}
