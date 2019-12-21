import 'package:equatable/equatable.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';
import 'package:meta/meta.dart';

@immutable
abstract class NewsfeedsState extends Equatable {
  @override
  List<Object> get props => [];
}

class Empty extends NewsfeedsState {}

class Loading extends NewsfeedsState {}

class Loaded extends NewsfeedsState {
  final List<Newsfeed> newsfeeds;
  final int page;

  Loaded({
    @required this.newsfeeds,
    @required this.page,
  });

  @override
  List<Object> get props => [newsfeeds, page];
}

class Error extends NewsfeedsState {
  final String message;

  Error({@required this.message});

  @override
  List<Object> get props => [message];
}
