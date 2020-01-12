import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';
import 'package:meta/meta.dart';

class NewsfeedModel extends Newsfeed {
  NewsfeedModel({
    @required date,
    @required imageUrl,
    @required title,
    @required urlToNewsfeed,
    @required shortDescription,
  }) : super(
          date: date,
          imageUrl: imageUrl,
          title: title,
          urlToNewsfeed: urlToNewsfeed,
          shortDescription: shortDescription,
        );

  factory NewsfeedModel.fromJson(Map<String, dynamic> json) {
    return NewsfeedModel(
      urlToNewsfeed: json['urlToNewsfeed'],
      date: json['date'],
      title: json['title'],
      shortDescription: json['shortDescription'],
      imageUrl: json['imageUrl'],
    );
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['urlToNewsfeed'] = this.urlToNewsfeed;
    data['date'] = this.date;
    data['title'] = this.title;
    data['shortDescription'] = this.shortDescription;
    data['imageUrl'] = this.imageUrl;
    return data;
  }
}
