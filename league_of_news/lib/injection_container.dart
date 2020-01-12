import 'package:get_it/get_it.dart';
import 'package:http/http.dart' as http;
import 'package:league_of_news/features/newsfeeds/domain/usecases/get_app_config.dart';
import 'package:league_of_news/features/newsfeeds/domain/usecases/get_newsfeeds.dart';
import 'package:league_of_news/features/newsfeeds/presentation/bloc/newsfeeds/newsfeeds_bloc.dart';

import 'features/newsfeeds/data/datasources/film_productions_remote_data_source.dart';
import 'features/newsfeeds/data/repositories/app_repository_impl.dart';
import 'features/newsfeeds/domain/repositories/app_repository.dart';

final sl = GetIt.instance;

Future init() async {
  // Bloc

  // sl.registerFactory(
  //   () => AddFilmProductionBloc(),
  // );

  sl.registerFactory(
    () => NewsfeedsBloc(
      getNewsfeeds: sl(),
    ),
  );

  // sl.registerFactory(
  //   () => MainBloc(
  //     getToken: sl(),
  //     getUsername: sl(),
  //   ),
  // );

  // sl.registerFactory(
  //   () => AuthenticationBloc(
  //     login: sl(),
  //     register: sl(),
  //   ),
  // );

  // sl.registerFactory(
  //   () => EpisodeBloc(
  //     getEpisodes: sl(),
  //   ),
  // );

  // sl.registerFactory(
  //   () => CommentsBloc(
  //     getComments: sl(),
  //   ),
  // );

  // sl.registerFactory(
  //   () => TopRatedBloc(
  //     getTopRated: sl(),
  //   ),
  // );

  // sl.registerFactory(
  //   () => FilmProductionBloc(
  //     getFilmProduction: sl(),
  //   ),
  // );

  // // Use cases
  sl.registerLazySingleton(() => GetAppConfig(sl()));
  sl.registerLazySingleton(() => GetNewsfeeds(sl()));
  // sl.registerLazySingleton(() => GetUsername(sl()));
  // sl.registerLazySingleton(() => Login(sl()));
  // sl.registerLazySingleton(() => Register(sl()));
  // sl.registerLazySingleton(() => GetToken(sl()));
  // sl.registerLazySingleton(() => GetEpisodes(sl()));
  // sl.registerLazySingleton(() => TopRated(sl()));
  // sl.registerLazySingleton(() => GetFilmProduction(sl()));
  // sl.registerLazySingleton(() => GetComments(sl()));

  // // Repository
  sl.registerLazySingleton<AppRepository>(
    () => AppRepositoryImpl(
      remoteDataSource: sl(),
    ),
  );

  // sl.registerLazySingleton<AuthenticationRepository>(
  //   () => AuthenticationRepositoryImpl(
  //     authenticationLocalDataSource: sl(),
  //     authenticationRemoteDataSource: sl(),
  //   ),
  // );

  // // Data sources
  sl.registerLazySingleton<ApiRemoteDataSource>(
    () => ApiRemoteDataSourceImpl(client: sl()),
  );

  // sl.registerLazySingleton<AuthenticationLocalDataSource>(
  //   () => AuthenticationLocalDataSourceImpl(sharedPreferences: sl()),
  // );

  // sl.registerLazySingleton<FilmProductionsRemoteDataSource>(
  //   () => FilmProductionsRemoteDataSourceImpl(client: sl()),
  // );

  // //! External
  sl.registerLazySingleton(() => http.Client());

  // final sharedPreferences = await SharedPreferences.getInstance();

  // sl.registerLazySingleton(() => sharedPreferences);
}
