import 'package:dartz/dartz.dart';
import 'package:equatable/equatable.dart';
import 'package:league_of_news/core/error/failures.dart';
import 'package:league_of_news/core/usecases/usecase.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';
import 'package:league_of_news/features/newsfeeds/domain/repositories/app_repository.dart';

class GetNewsfeeds implements UseCase<List<Newsfeed>, Params> {
  final AppRepository repository;

  GetNewsfeeds(this.repository);

  @override
  Future<Either<Failure, List<Newsfeed>>> call(
    Params params,
  ) =>
      repository.getNewsfeeds(params.websiteId, params.page);
}

class Params extends Equatable {
  final int websiteId;
  final int page;

  Params({this.page = 1, this.websiteId});

  @override
  List<Object> get props => [page, websiteId];
}
