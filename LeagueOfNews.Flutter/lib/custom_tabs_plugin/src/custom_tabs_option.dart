import 'package:flutter/painting.dart';

class CustomTabsOption {
  final Color toolbarColor;
  final bool enableUrlBarHiding;
  final bool enableDefaultShare;
  final bool showPageTitle;
  final bool enableInstantApps;
  final CustomTabsAnimation animation;
  final Map<String, String> headers;

  const CustomTabsOption({
    this.toolbarColor,
    this.enableUrlBarHiding,
    this.enableDefaultShare,
    this.showPageTitle,
    this.enableInstantApps,
    this.animation,
    this.headers,
  });

  Map<String, dynamic> toMap() {
    final dest = <String, dynamic>{};
    if (toolbarColor != null) {
      dest['toolbarColor'] = '#${toolbarColor.value.toRadixString(16)}';
    }
    if (enableUrlBarHiding != null) {
      dest['enableUrlBarHiding'] = enableUrlBarHiding;
    }
    if (enableDefaultShare != null) {
      dest['enableDefaultShare'] = enableDefaultShare;
    }
    if (showPageTitle != null) {
      dest['showPageTitle'] = showPageTitle;
    }
    if (enableInstantApps != null) {
      dest['enableInstantApps'] = enableInstantApps;
    }
    if (animation != null) {
      dest['animations'] = animation.toMap();
    }
    if (headers != null) {
      dest['headers'] = headers;
    }
    return dest;
  }
}

class CustomTabsAnimation {
  const CustomTabsAnimation({
    this.startEnter,
    this.startExit,
    this.endEnter,
    this.endExit,
  });

  factory CustomTabsAnimation.slideIn() {
    _slideIn ??= const CustomTabsAnimation(
      startEnter: 'slide_in_right',
      startExit: 'slide_out_left',
      endEnter: 'android:anim/slide_in_left',
      endExit: 'android:anim/slide_out_right',
    );
    return _slideIn;
  }

  factory CustomTabsAnimation.fade() {
    _fade ??= const CustomTabsAnimation(
      startEnter: 'android:anim/fade_in',
      startExit: 'android:anim/fade_out',
      endEnter: 'android:anim/fade_in',
      endExit: 'android:anim/fade_out',
    );
    return _fade;
  }

  static CustomTabsAnimation _slideIn;

  static CustomTabsAnimation _fade;

  final String startEnter;

  final String startExit;

  final String endEnter;

  final String endExit;

  Map<String, String> toMap() {
    final dest = <String, String>{};
    if (startEnter != null && startExit != null) {
      dest['startEnter'] = startEnter;
      dest['startExit'] = startExit;
    }
    if (endEnter != null && endExit != null) {
      dest['endEnter'] = endEnter;
      dest['endExit'] = endExit;
    }
    return dest;
  }
}
