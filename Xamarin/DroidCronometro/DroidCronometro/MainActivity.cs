using Android.App;
using Android.Widget;
using Android.OS;
using System.Timers;
using Android.Views;
using Android.Content;
namespace DroidCronometro
{
    [Activity(Label = "DroidCronometro", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button btnIniciar, btnPausar, btnRegistrar;
        TextView txtTimer;
        LinearLayout container;
        Timer timer;
        int mins = 0, segs = 0, milesegs = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            btnIniciar = FindViewById<Button>(Resource.Id.btnIniciar);
            btnPausar = FindViewById<Button>(Resource.Id.btnPausar);
            btnRegistrar = FindViewById<Button>(Resource.Id.btnRegistrar);
            container = FindViewById<LinearLayout>(Resource.Id.container);
            txtTimer = FindViewById<TextView>(Resource.Id.textTimer);
            btnIniciar.Click += BtnIniciar_Click;
            btnPausar.Click += BtnPausar_Click;
            btnRegistrar.Click += btnRegistrar_Click;
        }
        private void btnRegistrar_Click(object sender, System.EventArgs e)
        {
            LayoutInflater inflater = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
            View addView = inflater.Inflate(Resource.Layout.row, null);
            TextView txtConteudo = addView.FindViewById<TextView>(Resource.Id.txtTempo);
            txtConteudo.Text = txtTimer.Text;
            container.AddView(addView);
        }
        private void BtnPausar_Click(object sender, System.EventArgs e)
        {
            timer.Stop();
            timer = null;
        }
        private void BtnIniciar_Click(object sender, System.EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            milesegs++;
            if (milesegs >= 1000)
            {
                segs++;
                milesegs = 0;
            }
            if (segs == 59)
            {
                mins++;
                segs = 0;
            }
            RunOnUiThread(() =>
            {
                txtTimer.Text = string.Format("{0}:{1:00}:{2:000}", mins, segs, milesegs);
            });
        }
    }
}