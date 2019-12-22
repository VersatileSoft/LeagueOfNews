import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';

class ListElement extends StatelessWidget {
  final Newsfeed model;

  const ListElement(this.model);

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 130,
      child: Card(
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: <Widget>[
            model.imageUrl != null
                ? Expanded(
                    flex: 3,
                    child: CachedNetworkImage(
                      fit: BoxFit.fitHeight,
                      height: 120,
                      imageUrl: model.imageUrl,
                      placeholder: (context, url) =>
                          Center(child: CircularProgressIndicator()),
                      errorWidget: (context, url, error) => Icon(Icons.error),
                    ),
                  )
                : SizedBox.shrink(),
            Expanded(
              flex: 5,
              child: Padding(
                padding: const EdgeInsets.only(left: 10),
                child: buildText(context),
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget buildText(BuildContext context) {
    return Container(
      height: 130,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: <Widget>[
          Text(
            this.model.title,
            maxLines: 2,
            overflow: TextOverflow.ellipsis,
            style: TextStyle(
                color: Theme.of(context).accentColor,
                fontSize: 20,
                fontWeight: FontWeight.bold),
          ),
          Text(
            this.model.date,
          ),
          Text(
            this.model.shortDescription,
            maxLines: 3,
            overflow: TextOverflow.ellipsis,
            style: TextStyle(fontSize: 15),
          ),
        ],
      ),
    );
  }
}
