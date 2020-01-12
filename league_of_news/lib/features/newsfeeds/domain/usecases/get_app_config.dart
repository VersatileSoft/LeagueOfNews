import 'package:dartz/dartz.dart';
import 'package:league_of_news/core/error/failures.dart';
import 'package:league_of_news/core/usecases/usecase.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/app_config.dart';
import 'package:league_of_news/features/newsfeeds/domain/repositories/app_repository.dart';

class GetAppConfig implements UseCase<AppConfig, NoParams> {
  final AppRepository repository;

  GetAppConfig(this.repository);

  @override
  Future<Either<Failure, AppConfig>> call(NoParams params) =>
      repository.getAppConfig();
}
