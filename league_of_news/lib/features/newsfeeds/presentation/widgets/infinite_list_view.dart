import 'package:flutter/material.dart';

typedef ElementBuilder<T> = Widget Function(BuildContext context, T data);

class InfiniteListView<T> extends StatefulWidget {
  final List<T> items;
  final ElementBuilder builder;
  final bool hasReachedEndOfResults;
  final Function(T model, BuildContext context) onElementTap;
  final Function getMoreDataEvent;

  InfiniteListView({
    Key key,
    @required this.items,
    @required this.builder,
    @required this.hasReachedEndOfResults,
    this.onElementTap,
    this.getMoreDataEvent,
  }) : super(key: key);

  @override
  _InfiniteListViewState createState() => _InfiniteListViewState();
}

class _InfiniteListViewState extends State<InfiniteListView> {
  ScrollController _scrollController = ScrollController();

  @override
  void initState() {
    super.initState();
    _scrollController.addListener(_scrollListener);
  }

  _scrollListener() {
    if (_scrollController.offset >=
            _scrollController.position.maxScrollExtent &&
        !_scrollController.position.outOfRange) {
      widget.getMoreDataEvent();
    }
    FocusScope.of(context).requestFocus(FocusNode());
  }

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      controller: _scrollController,
      itemBuilder: (context, index) => _buildItem(index, context),
      itemCount: calculateListItemCount(),
    );
  }

  int calculateListItemCount() {
    return widget.items.length + (widget.hasReachedEndOfResults ? 0 : 1);
  }

  Widget _buildItem(int index, BuildContext context) {
    if (index >= widget.items.length) {
      return _buildLoader();
    } else {
      return GestureDetector(
        child: widget.builder(context, widget.items[index]),
        onTap: () {
          widget.onElementTap(widget.items[index], context);
        },
      );
    }
  }

  Widget _buildLoader() {
    return Center(
      child: Padding(
        padding: EdgeInsets.all(10),
        child: CircularProgressIndicator(),
      ),
    );
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }
}
