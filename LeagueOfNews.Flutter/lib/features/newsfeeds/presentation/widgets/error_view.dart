import 'package:flutter/material.dart';

class ErrorView extends StatelessWidget {
  const ErrorView({
    Key key,
    @required this.reload,
    @required this.message,
  }) : super(key: key);

  final Function reload;
  final String message;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Padding(
          padding: EdgeInsets.only(top: 50),
          child: Center(
            child: Text(
              message,
              style: TextStyle(fontSize: 30),
            ),
          ),
        ),
        SizedBox(height: 20),
        RaisedButton(
          child: Text('Odśwież'),
          onPressed: reload,
        )
      ],
    );
  }
}
