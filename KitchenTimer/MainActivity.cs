using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Threading;
using System;
using Android.Media;

namespace KitchenTimer
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        private int _ramainingMilliSec = 0; //残り時間変数定義
        private bool _isStart = false; //スタートしてるか否か（最初は止まってるのでfalse）
        private Button _startButton; //ボタン定義
        private Button _clearButton; //ボタン定義
        private Timer _timer; //タイマー変数定義

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);  //activity_mainビューを読み込ませる

            var add10MinButton = FindViewById<Button>(Resource.Id.Add10MinButton); //クリックイベントの取得
            add10MinButton.Click += Add10MinButton_Click; //クリックイベントが呼び出された時にAdd10MinButton_Clickが呼び出される

            var add1MinButton = FindViewById<Button>(Resource.Id.Add1MinButton);
            add1MinButton.Click += (s, e) =>
            {
                _ramainingMilliSec += 60 * 1000;
                ShowRemainingTime();
            };

            var add10SecButon = FindViewById<Button>(Resource.Id.Add10SecButton);
            add10SecButon.Click += Add10SecButon_Click;

            var add1SecButon = FindViewById<Button>(Resource.Id.Add1SecButton);
            add1SecButon.Click += Add1SecButon_Click;

            _startButton = FindViewById<Button>(Resource.Id.StartButton); //スタートボタンのクリックイベント
            _startButton.Click += _startButton_Click;

            _clearButton = FindViewById<Button>(Resource.Id.ClearButton);
            _clearButton.Click += _clearButton_Click;

            _timer = new Timer(Timer_OnTick, null, 0, 100); //Timer_OnTickをコールバック関数として、すぐ(0)タイマー起動、100ミリ秒間隔でTimer_OnTickを呼び出す
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            _ramainingMilliSec = 0; //reset
            ShowRemainingTime();
        }

        private void Timer_OnTick(object state)
        {
            if(!_isStart) //スタートしてなかったらそのまま戻る
            {
                return;
            }

            RunOnUiThread(() =>
            {
                _ramainingMilliSec -= 100;
                if (_ramainingMilliSec <= 0)
                {
                    _isStart = false; //のこり時間が0ミリ秒になったのでストップ
                    _ramainingMilliSec = 0;
                    _startButton.Text = "START";
                    //アラーム鳴らす
                    var toneGenerator = new ToneGenerator(Stream.System, 50);
                    toneGenerator.StartTone(Tone.PropBeep);
                }
                ShowRemainingTime();
            });
        }

        private void _startButton_Click(object sender, System.EventArgs e)
        {
            _isStart = !_isStart; //_isStartの値を反対に（true/false）
            if(_isStart)
            {
                _startButton.Text = "STOP";
            }
            else
            {
                _startButton.Text = "START";
            }
        }

        private void Add1SecButon_Click(object sender, System.EventArgs e)
        {
            _ramainingMilliSec += 1 * 1000; //1秒追加
            ShowRemainingTime();
        }

        private void Add10SecButon_Click(object sender, System.EventArgs e)
        {
            _ramainingMilliSec += 10 * 1000; //10秒追加
            ShowRemainingTime();
        }

        private void Add10MinButton_Click(object sender, System.EventArgs e)
        {
            _ramainingMilliSec += 600 * 1000; //10分追加
            ShowRemainingTime();
        }


        //残り時間を表示
        private void ShowRemainingTime()
        {
            var sec = _ramainingMilliSec / 1000;
            FindViewById<TextView>(Resource.Id.RemainingTimeTextView).Text = string.Format("{0:f0}:{1:d2}",
                                    sec / 60,
                                    sec % 60);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}