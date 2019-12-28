package com.example.league_of_news;

import com.google.firebase.messaging.FirebaseMessaging;

import androidx.annotation.NonNull;
import io.flutter.embedding.android.FlutterActivity;
import io.flutter.embedding.engine.FlutterEngine;
import io.flutter.plugins.FlutterCustomTabsPlugin;

public class MainActivity extends FlutterActivity {

    public MainActivity(){
        int i = 1+1;
    }

    @Override
    public void configureFlutterEngine(@NonNull FlutterEngine flutterEngine) {
        super.configureFlutterEngine(flutterEngine);
      //  GeneratedPluginRegistrant.registerWith(flutterEngine);
        flutterEngine.getPlugins().add(new FlutterCustomTabsPlugin(this));
        FirebaseMessaging.getInstance().subscribeToTopic("News");
    }
}
