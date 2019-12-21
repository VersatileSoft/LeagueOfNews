import 'package:equatable/equatable.dart';
import 'package:meta/meta.dart';

@immutable
abstract class NewsfeedsEvent extends Equatable {}

class GetMoreData extends NewsfeedsEvent {
  final int websiteId;

  GetMoreData(this.websiteId);

  @override
  List<Object> get props => [websiteId];
}
