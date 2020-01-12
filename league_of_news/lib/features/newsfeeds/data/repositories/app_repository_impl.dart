import 'package:dartz/dartz.dart';
import 'package:league_of_news/core/error/failures.dart';
import 'package:league_of_news/features/newsfeeds/data/datasources/film_productions_remote_data_source.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/app_config.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';
import 'package:league_of_news/features/newsfeeds/domain/repositories/app_repository.dart';
import 'package:meta/meta.dart';
import '../../../../core/error/exceptions.dart';
import '../../../../injection_container.dart';

class AppRepositoryImpl implements AppRepository {
  final ApiRemoteDataSource remoteDataSource;

  AppRepositoryImpl({@required this.remoteDataSource});

  @override
  Future<Either<Failure, AppConfig>> getAppConfig() async {
    try {
      var result = await remoteDataSource.getAppConfig();
      sl.registerLazySingleton(() => result);
      return Right(result);
    } on ServerException catch (e) {
      return Left(RemoteFailure(message: e.message));
    }
  }

  @override
  Future<Either<Failure, List<Newsfeed>>> getNewsfeeds(
      int websiteId, int page) async {
    try {
      return Right(await remoteDataSource.getNewsfeeds(websiteId, page));
    } on ServerException catch (e) {
      return Left(RemoteFailure(message: e.message));
    }
  }
}
