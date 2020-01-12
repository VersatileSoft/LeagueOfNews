import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:league_of_news/features/newsfeeds/presentation/bloc/newsfeeds/bloc.dart';
import 'package:league_of_news/features/newsfeeds/presentation/utils/custom_tab_launcher.dart';

import '../../../../injection_container.dart';
import 'error_view.dart';
import 'infinite_list_view.dart';
import 'list_element.dart';

class NewsfeedsList extends StatefulWidget {
  final int websiteId;

  const NewsfeedsList({
    Key key,
    @required this.websiteId,
  }) : super(key: key);

  @override
  _TopRatedListState createState() => _TopRatedListState();
}

class _TopRatedListState extends State<NewsfeedsList>
    with AutomaticKeepAliveClientMixin {
  @override
  bool get wantKeepAlive => true;
  NewsfeedsBloc bloc = sl<NewsfeedsBloc>();

  @override
  void initState() {
    super.initState();
    bloc.add(GetMoreData(widget.websiteId));
  }

  @override
  void didUpdateWidget(NewsfeedsList oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.websiteId != widget.websiteId)
      bloc.add(GetMoreData(widget.websiteId, clearList: true));
  }

  @override
  Widget build(BuildContext context) {
    super.build(context);
    return BlocBuilder(
      bloc: bloc,
      builder: (context, NewsfeedsState state) {
        if (state is Loading) {
          return _buildLoader();
        } else if (state is Empty) {
          return Center(
            child: Text(
              'Brak wyników!',
              style: TextStyle(fontSize: 20),
            ),
          );
        } else if (state is Loaded) {
          return _buildInfiniteListView(state);
        } else if (state is Error) {
          return _buildErrorView(state);
        }
        return Text("error");
      },
    );
  }

  Widget _buildLoader() {
    return Center(
      child: Padding(
        padding: EdgeInsets.all(10),
        child: CircularProgressIndicator(),
      ),
    );
  }

  Widget _buildInfiniteListView(Loaded state) {
    return InfiniteListView(
      builder: (context, model) => ListElement(model),
      hasReachedEndOfResults: false,
      items: state.newsfeeds,
      getMoreDataEvent: () => bloc.add(GetMoreData(widget.websiteId)),
      onElementTap: (model, context) => launchURL(model.urlToNewsfeed, context),
    );
  }

  Widget _buildErrorView(Error state) {
    return ErrorView(
      message: state.message,
      reload: () => bloc.add(GetMoreData(widget.websiteId)),
    );
  }
}
