import 'package:dartz/dartz.dart';
import 'package:league_of_news/core/error/failures.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/app_config.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';

abstract class AppRepository {
  Future<Either<Failure, AppConfig>> getAppConfig();

  Future<Either<Failure, List<Newsfeed>>> getNewsfeeds(int websiteId, int page);
}
