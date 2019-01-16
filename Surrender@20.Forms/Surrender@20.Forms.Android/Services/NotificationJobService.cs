using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.Droid;
using Surrender_20.Forms.ViewModels;
using Surrender_20.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Resource = Surrender_20.Forms.Droid.Resource;

namespace Surrender_20.Forms.Services
{

    [Service(Exported = true, Permission = "android.permission.BIND_JOB_SERVICE")]
    public class NotificationJobService : JobService
    {
        private static readonly string TAG = "ExampleJobService";

        public override bool OnStartJob(JobParameters args)
        {
            Log.Info(TAG, "on start job: " + args.JobId);

            DoBackgroundWork(args);
            
            return true;
        }

        public override bool OnStopJob(JobParameters args)
        {
            return true;
        }

        private void DoBackgroundWork(JobParameters args)
        {
            new Thread(async () =>
            {
                var setupSingleton = MvxAndroidSetupSingleton.EnsureSingletonAvailable(Application.Context);
                setupSingleton.EnsureInitialized();
                
                await Mvx.IoCProvider.Resolve<INewPostsService>().CheckNewPosts();

                JobFinished(args, true);

            }).Start();
        }
    }
}
