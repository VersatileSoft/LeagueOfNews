import 'dart:convert';

import 'package:http/http.dart';
import 'package:http/http.dart' as http;
import 'package:league_of_news/core/constants.dart';
import 'package:league_of_news/core/error/exceptions.dart';
import 'package:league_of_news/features/newsfeeds/data/models/app_config_model.dart';
import 'package:league_of_news/features/newsfeeds/data/models/newsfeed_model.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/app_config.dart';
import 'package:league_of_news/features/newsfeeds/domain/entities/newsfeed.dart';
import 'package:meta/meta.dart';

abstract class ApiRemoteDataSource {
  Future<AppConfig> getAppConfig();

  Future<List<Newsfeed>> getNewsfeeds(int websiteId, int page);
}

class ApiRemoteDataSourceImpl implements ApiRemoteDataSource {
  final http.Client client;

  ApiRemoteDataSourceImpl({@required this.client});

  @override
  Future<AppConfig> getAppConfig() async {
    Response response;

    try {
      response = await client.get(
        '$GET_CONFIG_URL',
        headers: {
          'Content-Type': 'application/json',
        },
      );
    } catch (_) {
      throw ServerException(message: 'Błąd łączenia z serwerem');
    }

    if (response.statusCode == 200) {
      return AppConfigModel.fromJson(json.decode(response.body));
    } else {
      throw ServerException(message: response.body);
    }
  }

  @override
  Future<List<Newsfeed>> getNewsfeeds(int websiteId, int page) async {
    Response response;

    try {
      response = await client.get(
        '$GET_NEWSFEEDS/$websiteId?page=$page',
        headers: {
          'Content-Type': 'application/json',
        },
      );
    } catch (_) {
      throw ServerException(message: 'Błąd łączenia z serwerem');
    }

    if (response.statusCode == 200) {
      Iterable l = json.decode(response.body);
      return l.map((model) => NewsfeedModel.fromJson(model)).toList();
    } else {
      throw ServerException(message: response.reasonPhrase);
    }
  }
}
