using Android.App;
using Android.App.Job;
using Android.Util;
using MvvmCross;
using MvvmCross.Platforms.Android.Core;
using LeagueOfNews.Core.Interface;
using System.Threading;

namespace LeagueOfNews.Forms.Services
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
                MvxAndroidSetupSingleton setupSingleton = MvxAndroidSetupSingleton.EnsureSingletonAvailable(Application.Context);
                setupSingleton.EnsureInitialized();

                await Mvx.IoCProvider.Resolve<INewPostsService>().CheckNewPosts();

                JobFinished(args, true);

            }).Start();
        }
    }
}