import 'package:equatable/equatable.dart';

abstract class Failure extends Equatable {
  @override
  List<Object> get props => [];
}

class CacheFailure extends Failure {}

class NoAuthorizationFailure extends Failure {}

class RemoteFailure extends Failure {
  final String message;

  RemoteFailure({this.message});

  @override
  List<Object> get props => [message];
}
