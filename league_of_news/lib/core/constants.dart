import 'package:flutter/foundation.dart';

const API_URL =
    kIsWeb ? "https://localhost:5001/api" : 'https://myseriallist.ml/api';

const GET_CONFIG_URL = '$API_URL/AppConfig';
const GET_NEWSFEEDS = '$API_URL/Newsfeed';
