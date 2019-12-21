import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';

class ListElement extends StatelessWidget {
  final Newsfeed model;

  const ListElement(this.model);

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceAround,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: <Widget>[
          model.imageUrl != null
              ? Expanded(
                  flex: 2,
                  child: CachedNetworkImage(
                    imageUrl: model.imageUrl,
                    placeholder: (context, url) =>
                        Center(child: CircularProgressIndicator()),
                    errorWidget: (context, url, error) => Icon(Icons.error),
                  ),
                )
              : SizedBox.shrink(),
          Expanded(
            child: buildText(context),
            flex: 5,
          ),
        ],
      ),
    );
  }

  Column buildText(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: <Widget>[
        Text(
          this.model.title,
          style: TextStyle(color: Theme.of(context).accentColor),
        ),
        Text(
          this.model.date,
        ),
        Text(
          this.model.shortDescription,
        ),
      ],
    );
  }
}
