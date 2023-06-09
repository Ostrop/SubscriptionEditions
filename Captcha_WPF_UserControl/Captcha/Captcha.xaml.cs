﻿
namespace Captcha
{
    public partial class Captcha : System.Windows.Controls.UserControl
    {
        public string CaptchaValue { get; set; }

        public event System.EventHandler CaptchaRefreshed;

        public Captcha()
        {
            InitializeComponent();

            CreateCaptcha();
        }
        public void ResetCaptcha()
        {
            CreateCaptcha();
        }
        private void ResetCaptchaButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateCaptcha();
        }

        private void CreateCaptcha()
        {
            string allowchar = string.Empty;
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            string[] ar = allowchar.Split(a);
            string pwd = string.Empty;
            string temp = string.Empty;
            System.Random r = new System.Random();

            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];

                pwd += temp;
            }

            CaptchaText.Text = pwd;

            CaptchaValue = CaptchaText.Text;

            CaptchaRefreshed?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
