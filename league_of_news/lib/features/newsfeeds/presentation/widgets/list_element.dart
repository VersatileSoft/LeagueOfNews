import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';
import 'package:responsive_builder/responsive_builder.dart';

class ListElement extends StatelessWidget {
  final Newsfeed model;

  const ListElement(this.model);

  @override
  Widget build(BuildContext context) {
    return Align(
      alignment: Alignment.topCenter,
      child: Container(
        constraints: BoxConstraints(maxWidth: 800),
        height: 140,
        child: Card(
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: <Widget>[
              model.imageUrl != null
                  ? Expanded(
                      flex: 2,
                      child: ScreenTypeLayout(
                        mobile: CachedNetworkImage(
                          fit: BoxFit.fitHeight,
                          height: 120,
                          imageUrl: model.imageUrl,
                          placeholder: (context, url) =>
                              Center(child: CircularProgressIndicator()),
                          errorWidget: (context, url, error) =>
                              Icon(Icons.error),
                        ),
                        tablet: Image.network(
                          model.imageUrl,
                          fit: BoxFit.fitHeight,
                        ),
                        desktop: Image.network(
                          model.imageUrl,
                          fit: BoxFit.cover,
                          height: 140,
                        ),
                      ),
                    )
                  : SizedBox.shrink(),
              Expanded(
                flex: 6,
                child: Padding(
                  padding: const EdgeInsets.only(left: 10),
                  child: buildText(context),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget buildText(BuildContext context) {
    return Container(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.start,
        children: <Widget>[
          Padding(
            padding: const EdgeInsets.only(top: 10, left: 10),
            child: Text(
              this.model.title,
              maxLines: 2,
              overflow: TextOverflow.ellipsis,
              style: TextStyle(
                  color: Theme.of(context).accentColor,
                  fontSize: 20,
                  fontWeight: FontWeight.bold),
            ),
          ),
          Padding(
            padding: const EdgeInsets.only(left: 10),
            child: Text(
              this.model.date,
            ),
          ),
          Expanded(
            child: Padding(
              padding: const EdgeInsets.all(10),
              child: Text(
                this.model.shortDescription,
                maxLines: 3,
                overflow: TextOverflow.clip,
                style: TextStyle(fontSize: 15),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
