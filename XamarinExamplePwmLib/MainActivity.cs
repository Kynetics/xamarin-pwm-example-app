using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Com.Kynetics.Libpwm;
using Java.IO;
using Android.Util;
using System;
using System.IO.Pipes;

namespace XamarinExamplePwmLib
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private const string TAG = "PwmLibrary";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Log.Info(TAG, "Start");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            try
            {
                TestLibrary();
            } catch (IOException e)
            {
                Log.Error(TAG, "Error during library test", e);
            }
            Log.Info(TAG, "End");

        }

        private void TestLibrary()
        {
            IPwmManager manager = PwmManagerFactory.Instance;

            int[] controllers = manager.GetPwmControllers();

            if(controllers == null)
            {
                Log.Error(TAG, "Error getting controllers");
                return;
            }

            foreach(int controller in controllers)
            {
                Log.Info(TAG, "Found PWM controller " + controller);
            }

            int[] controllerchannels = manager.GetPwmChannels(1);

            Log.Info(TAG, "Controller 1 has " + controllerchannels.Length + " channels");

            IPwm pwm = manager.Open(1, 0);


            Log.Info(TAG, "pwm 1:0 is " + pwm.Enabled);

            pwm.FrequencyHz = 120;

            Log.Info(TAG, "pwm 1:0 frequency: " + pwm.FrequencyHz + " HZ");

            pwm.Enabled = true;

            Log.Info(TAG, "pwm 1:0 is " + pwm.Enabled);

            pwm.DutyCycle = 50;

            Log.Info(TAG, "pwm 1:0 duty cycle: " + pwm.DutyCycle + "%");

            pwm.Polarity = PwmPolarity.PolarityNormal;

            Log.Info(TAG, "pwm 1:0 polarity is " + pwm.Polarity);

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}