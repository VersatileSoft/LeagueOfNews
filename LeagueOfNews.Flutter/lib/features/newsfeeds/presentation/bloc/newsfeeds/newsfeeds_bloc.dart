import 'dart:async';

import 'package:bloc/bloc.dart';
import 'package:league_of_news/core/error/failures.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';
import 'package:league_of_news/features/newsfeeds/domain/usecases/get_newsfeeds.dart';
import 'package:meta/meta.dart';

import 'newsfeeds_event.dart';
import 'newsfeeds_state.dart';

class NewsfeedsBloc extends Bloc<NewsfeedsEvent, NewsfeedsState> {
  final GetNewsfeeds getNewsfeeds;

  NewsfeedsBloc({
    @required this.getNewsfeeds,
  }) : assert(getNewsfeeds != null);

  @override
  NewsfeedsState get initialState => Loading();

  @override
  Stream<NewsfeedsState> mapEventToState(
    NewsfeedsEvent event,
  ) async* {
    if (event is GetMoreData) {
      int page = 1;
      List<Newsfeed> list = List();
      if (state is Loaded && !event.clearList) {
        page = (state as Loaded).page;
        list = (state as Loaded).newsfeeds;
      }

      if (list.isEmpty) yield Loading();

      final i = await getNewsfeeds(
        Params(websiteId: event.websiteId, page: page),
      );

      yield* i.fold((failure) async* {
        if (failure is RemoteFailure) {
          yield Error(message: failure.message);
        }
      }, (succes) async* {
        list.addAll(succes);
        if (list.isEmpty)
          yield Empty();
        else
          yield Loaded(
            newsfeeds: list,
            page: page + 1,
          );
      });
    }
  }
}
