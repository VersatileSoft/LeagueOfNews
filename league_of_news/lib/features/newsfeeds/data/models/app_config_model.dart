
import 'package:league_of_news/features/newsfeeds/domain/entities/app_config.dart';
import 'package:meta/meta.dart';

import 'website_model.dart';

class AppConfigModel extends AppConfig {
  AppConfigModel({
    @required websites,
  }) : super(
          websites: websites,
        );

  factory AppConfigModel.fromJson(Map<String, dynamic> json) {
    var websites;
    if (json['websites'] != null) {
      websites = new List<WebsiteModel>();
      json['websites'].forEach((v) {
        websites.add(new WebsiteModel.fromJson(v));
      });
    }
    return AppConfigModel(websites: websites);
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    if (this.websites != null) {
      data['websites'] = this.websites.map((v) => v.toJson()).toList();
    }
    return data;
  }
}
