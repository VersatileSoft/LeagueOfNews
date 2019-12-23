package io.flutter.plugins;

import android.app.Activity;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.net.Uri;

import java.util.Map;

import androidx.annotation.NonNull;
import androidx.browser.customtabs.CustomTabsIntent;
import io.flutter.embedding.engine.plugins.FlutterPlugin;
import io.flutter.plugin.common.MethodCall;
import io.flutter.plugin.common.MethodChannel;
import io.flutter.plugin.common.MethodChannel.MethodCallHandler;
import io.flutter.plugin.common.MethodChannel.Result;
import io.flutter.plugins.internal.Launcher;

/** FlutterCustomTabsPlugin */
public class FlutterCustomTabsPlugin implements FlutterPlugin, MethodCallHandler {

  private static final String KEY_OPTION = "option";

  private static final String KEY_URL = "url";

  private static final String CODE_LAUNCH_ERROR = "LAUNCH_ERROR";

  private Context appContext;

  private Launcher launcher;

  public FlutterCustomTabsPlugin(Activity activity){
      appContext = activity;
  }

  @Override
  public void onAttachedToEngine(@NonNull FlutterPluginBinding flutterPluginBinding) {
    final MethodChannel channel = new MethodChannel(flutterPluginBinding.getBinaryMessenger(), "flutter_custom_tabs");
    channel.setMethodCallHandler(this);
    launcher = new Launcher(appContext);
  }

  @Override
  @SuppressWarnings("unchecked")
  public void onMethodCall(@NonNull MethodCall call, @NonNull Result result) {
    if ("launch".equals(call.method)) {
      launch(((Map<String, Object>) call.arguments), result);
    } else {
      result.notImplemented();
    }
  }

  @SuppressWarnings("unchecked")
  private void launch(@NonNull Map<String, Object> args, @NonNull MethodChannel.Result result) {
    Object url = args.get(KEY_URL);
    Uri uri =Uri.parse("");
    if(url != null) uri = Uri.parse(url.toString());
    final Map<String, Object> options = (Map<String, Object>) args.get(KEY_OPTION);
    CustomTabsIntent customTabsIntent = null;
    if(options != null)
      customTabsIntent = launcher.buildIntent(options);

//    final Context context;
//    if (registrar.activity() != null) {
//      context = registrar.activity();
//    } else {
//      context = registrar.context();
//    if(customTabsIntent != null)
//        customTabsIntent.intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
////    }

    try {
      if(customTabsIntent != null)
      launcher.launch(appContext, uri, customTabsIntent);
      result.success(null);
    } catch (ActivityNotFoundException e) {
      result.error(CODE_LAUNCH_ERROR, e.getMessage(), null);
    }
  }

  @Override
  public void onDetachedFromEngine(@NonNull FlutterPluginBinding binding) {
  }
}
