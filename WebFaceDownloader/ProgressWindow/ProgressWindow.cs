namespace WebFaceDownloader
{
    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Drawing.Drawing2D;
    using System.Threading;

    /// <summary>
    /// Summary description for ProgressWindow.
    /// </summary>
    public class ProgressWindow : System.Windows.Forms.Form, IProgressCallback
	{

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public delegate void SetTextInvoker(String text);
		public delegate void IncrementInvoker( int val );
		public delegate void StepToInvoker( int val );
		public delegate void RangeInvoker( int minimum, int maximum );
        public delegate void SetImagePathInvoker(String img_path,int tmp,int frame_id,int year,int month,int date,int hour,int minute,int sec);
        public delegate void SetImageBigPICInvoker(string img_path);
        public delegate void SetImageTmpPICInvoker(string img_path);
        public delegate void SetMaxTempUpdateInvoker(int frame_id,int tmp);

        private String titleRoot = "";
		private System.Threading.ManualResetEvent initEvent = new System.Threading.ManualResetEvent(false);
		private System.Threading.ManualResetEvent abortEvent = new System.Threading.ManualResetEvent(false);
		private bool requiresClose = true;
        private Label label;
        private ProgressBar progressBar;
        private Button cancelButton;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private int nImageShowNo = 0 ;
        private PictureBox pictureBox5;
        private TextBox txtInfoBox;
        private PictureBox pictureBoxBigPIC;
        private Label lbDate1;
        private Label lbDate2;
        private Label lbDate3;
        private Label lbDate4;
        private Label lbDate5;
        private Label lbTMP1;
        private Label lbTMP2;
        private Label lbTMP3;
        private Label lbTMP4;
        private Label lbTMP5;
        private PictureBox pictureBoxTMPPic;
        private Label lbTMP;
        private Label lbTMPValue;
        private Label lbTMPInfo;
        public bool isBegin;

		public ProgressWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		#region Implementation of IProgressCallback
		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress meter.
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		public void Begin( int minimum, int maximum )
		{
            isBegin = true;
            initEvent.WaitOne();
			Invoke( new RangeInvoker( DoBegin ), new object[] { minimum, maximum } );
		}

		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress callback, without setting the range
		/// </summary>
		public void Begin()
		{
			initEvent.WaitOne();
			Invoke( new MethodInvoker( DoBegin ) );
		}

		/// <summary>
		/// Call this method from the worker thread to reset the range in the progress callback
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		public void SetRange( int minimum, int maximum )
		{
			initEvent.WaitOne();
			Invoke( new RangeInvoker( DoSetRange ), new object[] { minimum, maximum } );
		}

        /// <summary>
        /// Call this method from the worker thread to update the progress text.
        /// </summary>
        /// <param name="text">The progress text to display</param>
        public void SetText( String text )
		{
			Invoke( new SetTextInvoker(DoSetText), new object[] { text } );
		}

        /// <summary>
        /// Call this method from the worker thread to update the progress text.
        /// </summary>
        /// <param name="text">The progress text to display</param>
        public void SetTitle(String text)
        {
            Invoke(new SetTextInvoker(DoSetTitle), new object[] { text });
        }

        /// <summary>
        /// Call this method from the worker thread to increase the progress counter by a specified value.
        /// </summary>
        /// <param name="val">The amount by which to increment the progress indicator</param>
        public void Increment( int val )
		{
			Invoke( new IncrementInvoker( DoIncrement ), new object[] { val } );
		}

		/// <summary>
		/// Call this method from the worker thread to step the progress meter to a particular value.
		/// </summary>
		/// <param name="val"></param>
		public void StepTo( int val )
		{
			Invoke( new StepToInvoker( DoStepTo ), new object[] { val } );
		}

		/// <summary>
		/// If this property is true, then you should abort work
		/// </summary>
		public bool IsAborting
		{
			get
			{
				return abortEvent.WaitOne( 0, false );
			}
		}

		/// <summary>
		/// Call this method from the worker thread to finalize the progress meter
		/// </summary>
		public void End()
		{
			if( requiresClose )
			{
				Invoke( new MethodInvoker( DoEnd ) );
			}

            isBegin = false;

        }

        public void WrittingBuffer1()
        {
            Invoke(new MethodInvoker(DoWrittingBuffer1)); 
        }
        public void WrittingBuffer2()
        {
            Invoke(new MethodInvoker(DoWrittingBuffer2)); 
        }

        public void SetImage(string img_path,int tmp, int frame_id, int year, int month, int date, int hour, int minute, int sec)
        {
            Invoke(new SetImagePathInvoker(DoSetImagePath), new object[] { img_path,tmp, frame_id, year, month,date,hour,minute,sec });
        }
        public void SetImageBigPIC(string img_path)
        {
            Invoke(new SetImageBigPICInvoker(DoSetImageBigPICPath), new object[] { img_path });
        }
        public void SetImageTmpPIC(string img_path)
        {
            Invoke(new SetImageTmpPICInvoker(DoSetImageTmpPICPath), new object[] { img_path });
        }
        public void SetMaxTempUpdate(int frame_id,int tmp)
        {
            Invoke(new SetMaxTempUpdateInvoker(DoSetMaxTempUpdate), new object[] { frame_id, tmp });
        }
        #endregion

        #region Implementation members invoked on the owner thread
        private void DoSetText( String text )
		{
            //label.Text = text;
            txtInfoBox.AppendText(text);

            if (txtInfoBox.TextLength > 5000)
                txtInfoBox.Clear();
        }
        private void DoSetTitle(String text)
        {
            Text = text;
        }

        private void DoIncrement( int val )
		{
			progressBar.Increment( val );
			UpdateStatusText();
		}

		private void DoStepTo( int val )
		{
			progressBar.Value = val;
			UpdateStatusText();
		}

		private void DoBegin( int minimum, int maximum )
		{
			DoBegin();
			DoSetRange( minimum, maximum );
		}

		private void DoBegin()
		{
			cancelButton.Enabled = true;
			ControlBox = true;
		}

		private void DoSetRange( int minimum, int maximum )
		{
			progressBar.Minimum = minimum;
			progressBar.Maximum = maximum;
			progressBar.Value = minimum;
			titleRoot = Text;
		}

        private void DoSetTitle(int minimum, int maximum)
        {
            titleRoot = Text;
        }
        
        private void DoEnd()
		{
			Close();
		}

        private void DoWrittingBuffer1()
        {

        }

        private void DoWrittingBuffer2()
        {

        }
        /*
        //灰度圖的筆刷混色
        private ColorBlend getColorBlend()
        {
            float _brushStop = 1.0f;
            int _intensity = 1;
            ColorBlend colors = new ColorBlend(3);

            // Set brush stops. 
            //  _brushStop 和 _intensity 分別是界面上由用戶指定的筆刷變化點和單點中心濃度。這就相當於是WPF/SL中的GradientStops，只不過是分別指定位置和顏色。
            colors.Positions = new float[3] { 0, _brushStop, 1 };

            // The intensity value adjusts alpha of gradient colors. 
            colors.Colors = new Color[3] {
                Color.FromArgb(0, Color.White),
                Color.FromArgb(_intensity, Color.Black),
                // The following colors can be any color - Only the alpha value is used. 
                Color.FromArgb(_intensity, Color.Black)
            };
            return colors;
        }
        */
        /*
        private Color blackBodyColor(double temp)
        {
            float x = (float)(temp / 1000.0);
            float x2 = x * x;
            float x3 = x2 * x;
            float x4 = x3 * x;
            float x5 = x4 * x;

            float R, G, B = 0f;

            // red
            if (temp <= 6600)
                R = 1f;
            else
                R = 0.0002889f * x5 - 0.01258f * x4 + 0.2148f * x3 - 1.776f * x2 + 6.907f * x - 8.723f;

            // green
            if (temp <= 6600)
                G = -4.593e-05f * x5 + 0.001424f * x4 - 0.01489f * x3 + 0.0498f * x2 + 0.1669f * x - 0.1653f;
            else
                G = -1.308e-07f * x5 + 1.745e-05f * x4 - 0.0009116f * x3 + 0.02348f * x2 - 0.3048f * x + 2.159f;

            // blue
            if (temp <= 2000f)
                B = 0f;
            else if (temp < 6600f)
                B = 1.764e-05f * x5 + 0.0003575f * x4 - 0.01554f * x3 + 0.1549f * x2 - 0.3682f * x + 0.2386f;
            else
                B = 1f;

            //return Color.FromScRgb(1f, R, G, B);
            int r = Math.Min(Math.Max((int)(R * 255f), 0), 255);
            int g = Math.Min(Math.Max((int)(G * 255f), 0), 255);
            int b = Math.Min(Math.Max((int)(B * 255f), 0), 255);         

            return Color.FromArgb(255, r, g, b);
        }
        */
        private static Color[] ThreeColors =
        {
            Color.FromArgb(0, 149, 38),  //    Min-Tmp
            Color.FromArgb(255, 224, 53), //    Mid-Tmp
            Color.FromArgb(245, 10, 12), //    Hi-Tmp
        };

        private Color Tmp2Color(int Tmp, int maxTmp,int minTmp)
        {
            Color tmpColor;
            int midTmp = (maxTmp + minTmp) / 2;

            if (Tmp > midTmp)
            {
                int r = ThreeColors[1].R - ((Tmp - midTmp) * (ThreeColors[1].R - ThreeColors[2].R) / (maxTmp - midTmp));
                int g = ThreeColors[1].G - ((Tmp - midTmp) * (ThreeColors[1].G - ThreeColors[2].G) / (maxTmp - midTmp));
                int b = ThreeColors[1].B - ((Tmp - midTmp) * (ThreeColors[1].B - ThreeColors[2].B) / (maxTmp - midTmp));

                tmpColor = Color.FromArgb(r, g, b);

            }
            else if(Tmp < midTmp)
            {
                int r = ThreeColors[1].R - ((midTmp - Tmp) * (ThreeColors[1].R - ThreeColors[0].R) / (midTmp - minTmp));
                int g = ThreeColors[1].G - ((midTmp - Tmp) * (ThreeColors[1].G - ThreeColors[0].G) / (midTmp - minTmp));
                int b = ThreeColors[1].B - ((midTmp - Tmp) * (ThreeColors[1].B - ThreeColors[0].B) / (midTmp - minTmp));

                tmpColor = Color.FromArgb(200,r, g, b);
            }
            else // (Tmp == midTmp)
            {
                tmpColor = ThreeColors[1];
            }

            return tmpColor;
        }

        private void DoSetMaxTempUpdate(int frame_id,int tmp)
        {
            lbTMPValue.Text = (tmp / 10).ToString() + "." + (tmp % 10).ToString() + "℃"; ;

            //if (pictureBox5.Image != null)
            //    pictureBox5.Image.Dispose();
            if ((pictureBox1.Tag !=null) && (frame_id == (int)pictureBox1.Tag))
            {
                int v = (int)(Convert.ToDouble(lbTMP1.Text.Trim(new Char[] { '℃' })) * 10f);
                if (tmp > v)
                    lbTMP1.Text = (tmp / 10).ToString() + "." + (tmp % 10).ToString() + "℃"; ;
            }
            else if ((pictureBox2.Tag != null) && (frame_id == (int)pictureBox2.Tag))
            {
                int v = (int)(Convert.ToDouble(lbTMP2.Text.Trim(new Char[] { '℃' })) * 10f);
                if (tmp > v)
                    lbTMP2.Text = (tmp / 10).ToString() + "." + (tmp % 10).ToString() + "℃"; ;
            }
            else if ((pictureBox3.Tag != null) && (frame_id == (int)pictureBox3.Tag))
            {
                int v = (int)(Convert.ToDouble(lbTMP3.Text.Trim(new Char[] { '℃' })) * 10f);
                if (tmp > v)
                    lbTMP3.Text = (tmp / 10).ToString() + "." + (tmp % 10).ToString() + "℃"; ;
            }
            else if ((pictureBox4.Tag != null) && (frame_id == (int)pictureBox4.Tag))
            {
                int v = (int)(Convert.ToDouble(lbTMP4.Text.Trim(new Char[] { '℃' })) * 10f);
                if (tmp > v)
                    lbTMP4.Text = (tmp / 10).ToString() + "." + (tmp % 10).ToString() + "℃"; ;
            }
            else if ((pictureBox5.Tag != null) && (frame_id == (int)pictureBox5.Tag))
            {
                int v = (int)(Convert.ToDouble(lbTMP5.Text.Trim(new Char[] { '℃' })) * 10f);
                if (tmp > v)
                    lbTMP5.Text = (tmp / 10).ToString() + "." + (tmp % 10).ToString() + "℃"; ;
            }

        }


        private void DoSetImageTmpPICPath(String img_path)
        {
            int p, p0, p1, p2, p3, p4, p5, p6, p7;
            int th = 64;

            string the_img_path = img_path;
            int i=0,j=0,maxValue = 0, minValue = 65535;

            byte[] bytes = File.ReadAllBytes(the_img_path);

            // Retrieve the image.
            Bitmap rawbmp = new Bitmap(32,32, PixelFormat.Format32bppArgb);

            // Get the max & min value in bytes
            /*
            for (i = 0; i < (bytes.Length / 2); i++)
            {
                p = BitConverter.ToInt16(bytes, i * 2);

                if (minValue > p)
                    minValue = p;

                if (maxValue < p)
                    maxValue = p;
            }
            */
            // Loop through the images pixels to reset color.
  
            for (j = 0; j < rawbmp.Height; j++)
            {
                for (i = 0; i < rawbmp.Width; i++)
                {
                    if ((j == 0) && (i == 0)) // 左上角
                    {
                        p = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i) * 2);
                    }
                    else if ((j == 0) && (i == (rawbmp.Width - 1))) // 右上角
                    {
                        p = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i) * 2);
                    }
                    else if ((j == (rawbmp.Height - 1)) && (i == 0)) // 左下角
                    {
                        p = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i) * 2);
                    }
                    else if ((j == (rawbmp.Height - 1)) && (i == (rawbmp.Width - 1))) // 右下角
                    {
                        p = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i) * 2);
                    }
                    else if (j == 0) // 上邊線
                    {

                        p = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i) * 2);
                    }
                    else if (i == 0) // 左邊線
                    {
                        p = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i) * 2);
                    }
                    else if (j == (rawbmp.Height - 1)) // 下邊線
                    {
                        p = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i) * 2);

                    }
                    else if (i == (rawbmp.Width - 1)) // 右邊線
                    {
                        p = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i) * 2);
                    }
                    else
                    {
                        // P0  P1  P2
                        // P3  P   P4
                        // P5  P6  P7
                        p = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i) * 2);

                        p0 = BitConverter.ToInt16(bytes, (j - 1) * rawbmp.Width * 2 + (i - 1) * 2);
                        p1 = BitConverter.ToInt16(bytes, (j - 1) * rawbmp.Width * 2 + (i) * 2);
                        p2 = BitConverter.ToInt16(bytes, (j - 1) * rawbmp.Width * 2 + (i + 1) * 2);
                        p3 = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i - 1) * 2);
                        p4 = BitConverter.ToInt16(bytes, (j) * rawbmp.Width * 2 + (i + 1) * 2);
                        p5 = BitConverter.ToInt16(bytes, (j + 1) * rawbmp.Width * 2 + (i - 1) * 2);
                        p6 = BitConverter.ToInt16(bytes, (j + 1) * rawbmp.Width * 2 + (i) * 2);
                        p7 = BitConverter.ToInt16(bytes, (j + 1) * rawbmp.Width * 2 + (i + 1) * 2);

                        int sum = 0;
                        if (Math.Abs(p - p0) > th) sum++;
                        if (Math.Abs(p - p1) > th) sum++;
                        if (Math.Abs(p - p2) > th) sum++;
                        if (Math.Abs(p - p3) > th) sum++;
                        if (Math.Abs(p - p4) > th) sum++;
                        if (Math.Abs(p - p5) > th) sum++;
                        if (Math.Abs(p - p6) > th) sum++;
                        if (Math.Abs(p - p7) > th) sum++;

                        if (sum == 8)
                        {
                            p = (p0 + p1 + p2 + p3 + p4 + p5 + p6 + p7) / 8;

                        } // su
                    }

                    if ( p > 0)
                    {
                        if (minValue > p)
                            minValue = p;

                        if (maxValue < p)
                            maxValue = p;
                    }

                } // i
            } // j
            
            // Loop through the images pixels to reset color.
            for (j = 0; j < rawbmp.Height; j++)
            {
                for (i = 0; i < rawbmp.Width; i++)
                {
                    p = BitConverter.ToInt16(bytes, (j    ) * rawbmp.Width * 2 + (i    ) * 2);

                    if (p <= minValue) p = minValue;
                    if (p >= maxValue) p = maxValue;

                    rawbmp.SetPixel(i, j, Tmp2Color(p, maxValue, minValue));

                } // i
            } // j


            Bitmap mybmp = new Bitmap(pictureBoxTMPPic.Width, pictureBoxTMPPic.Height);

            Image imageSrc = rawbmp;
            Image imageDraw = default(Image);

            Graphics gr = Graphics.FromImage(mybmp);
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            gr.DrawImage(imageSrc, 0, 0, pictureBoxTMPPic.Width, pictureBoxTMPPic.Height);
            imageDraw = mybmp;

            pictureBoxTMPPic.Image = imageDraw;
        }

        private void DoSetImageBigPICPath(String img_path)
        {
            try
            {
                string the_img_path = img_path;

                Image imageInfo = Image.FromFile(the_img_path);
                Image imageDraw = default(Image);

                int imgWidth = imageInfo.Width;
                int imgHeight = imageInfo.Height;

                //Bitmap mybmp = new Bitmap(imgWidth, imgHeight);
                Bitmap mybmp = new Bitmap(pictureBoxBigPIC.Width, pictureBoxBigPIC.Height);
                Graphics gr = Graphics.FromImage(mybmp);

                gr.DrawImage(imageInfo, 0, 0, pictureBoxBigPIC.Width, pictureBoxBigPIC.Height);
                imageDraw = mybmp;

                //imageInfo.Save("d:\\testSrc.jpg", ImageFormat.Jpeg);
                //imageDraw.Save("d:\\testConv.jpg", ImageFormat.Jpeg);

                // free resource
                imageInfo.Dispose();
                gr.Dispose();

                if (pictureBoxBigPIC.Image != null)
                    pictureBoxBigPIC.Image.Dispose();

                pictureBoxBigPIC.Image = imageDraw;
                pictureBoxBigPIC.Refresh();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void DoSetImagePath(String img_path, int tmp, int frame_id, int year, int month, int date, int hour, int minute, int sec)
        {
            string the_img_path = img_path;

            Image imageInfo = Image.FromFile(the_img_path);
            Image imageDraw = default(Image);

            int imgWidth = imageInfo.Width;
            int imgHeight = imageInfo.Height;

            //Bitmap mybmp = new Bitmap(imgWidth, imgHeight);
            Bitmap mybmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics gr = Graphics.FromImage(mybmp);

            gr.DrawImage(imageInfo, 0, 0, pictureBox1.Width, pictureBox1.Height);
            imageDraw = mybmp;


            // free resource
            imageInfo.Dispose();
            gr.Dispose();

            if (nImageShowNo >= 4)
            {
                //if (pictureBox5.Image != null)
                //    pictureBox5.Image.Dispose();
                pictureBox5.Image = pictureBox4.Image;
                pictureBox5.Tag = pictureBox4.Tag;
                pictureBox5.Refresh();

                lbDate5.Text = lbDate4.Text;
                lbTMP5.Text = lbTMP4.Text;
            } // nImageShowNo

            if (nImageShowNo >= 3)
            {
                //if (pictureBox4.Image != null)
                //    pictureBox4.Image.Dispose();
                pictureBox4.Image = pictureBox3.Image;
                pictureBox4.Tag = pictureBox3.Tag;
                pictureBox4.Refresh();

                lbDate4.Text = lbDate3.Text;
                lbTMP4.Text = lbTMP3.Text;
            } // nImageShowNo

            if (nImageShowNo >= 2)
            {
                //if (pictureBox3.Image != null)
                //    pictureBox3.Image.Dispose();
                pictureBox3.Image = pictureBox2.Image;
                pictureBox3.Tag = pictureBox2.Tag;
                pictureBox3.Refresh();

                lbDate3.Text = lbDate2.Text;
                lbTMP3.Text = lbTMP2.Text;
            } // nImageShowNo

            if (nImageShowNo >= 1)
            {
                //if (pictureBox2.Image != null)
                //    pictureBox2.Image.Dispose();
                pictureBox2.Image = pictureBox1.Image;
                pictureBox2.Tag = pictureBox1.Tag;
                pictureBox2.Refresh();

                lbDate2.Text = lbDate1.Text;
                lbTMP2.Text = lbTMP1.Text;
            } // nImageShowNo

            //if (pictureBox1.Image != null)
            //    pictureBox1.Image.Dispose();
            pictureBox1.Image = imageDraw;
            pictureBox1.Tag = frame_id;
            pictureBox1.Refresh();

            lbDate1.Text = hour.ToString("D2") + ":" + minute.ToString("D2");
            lbTMP1.Text = (tmp / 10).ToString() + "." + (tmp % 10).ToString() + "℃"; ;
            lbTMPValue.Text = (tmp / 10).ToString() + "." + (tmp % 10).ToString() + "℃"; ;

            double tmpValue = Convert.ToDouble(lbTMPValue.Text.Trim(new Char[] { '℃'}));
            double tmp1 = Convert.ToDouble(lbTMP1.Text.Trim(new Char[] { '℃' }));
            double tmp2 = Convert.ToDouble(lbTMP2.Text.Trim(new Char[] { '℃' }));
            double tmp3 = Convert.ToDouble(lbTMP3.Text.Trim(new Char[] { '℃' }));
            double tmp4 = Convert.ToDouble(lbTMP4.Text.Trim(new Char[] { '℃' }));
            double tmp5 = Convert.ToDouble(lbTMP5.Text.Trim(new Char[] { '℃' }));

            if (tmpValue >= 37.5)
            {
                if (tmpValue >= 39)
                    lbTMPInfo.Text = "溫度過高";
                else
                    lbTMPInfo.Text = "溫度異常";

                lbTMPValue.ForeColor = Color.Red;
            }
            else
            {
                lbTMPValue.ForeColor = Color.Black;
                lbTMPInfo.Text = "";
            }

            if (tmp1 >= 37.5)
                lbTMP1.ForeColor = Color.Red;
            else
                lbTMP1.ForeColor = Color.Black;

            if (tmp2 >= 37.5)
                lbTMP2.ForeColor = Color.Red;
            else
                lbTMP2.ForeColor = Color.Black;

            if (tmp3 >= 37.5)
                lbTMP3.ForeColor = Color.Red;
            else
                lbTMP3.ForeColor = Color.Black;

            if (tmp4 >= 37.5)
                lbTMP4.ForeColor = Color.Red;
            else
                lbTMP4.ForeColor = Color.Black;

            if (tmp5 >= 37.5)
                lbTMP5.ForeColor = Color.Red;
            else
                lbTMP5.ForeColor = Color.Black;

            if (nImageShowNo++ > 6)
                nImageShowNo = 6;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Handles the form load, and sets an event to ensure that
        /// intialization is synchronized with the appearance of the form.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(System.EventArgs e)
		{
			base.OnLoad( e );
			ControlBox = false;
			initEvent.Set();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Handler for 'Close' clicking
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			requiresClose = false;
			AbortWork();

            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();

            if (pictureBox2.Image != null)
                pictureBox2.Image.Dispose();

            if (pictureBox3.Image != null)
                pictureBox3.Image.Dispose();

            if (pictureBox4.Image != null)
                pictureBox4.Image.Dispose();

            if (pictureBox5.Image != null)
                pictureBox5.Image.Dispose();

            base.OnClosing( e );
		}
		#endregion
		
		#region Implementation Utilities
		/// <summary>
		/// Utility function that formats and updates the title bar text
		/// </summary>
		private void UpdateStatusText()
		{
            //Text = titleRoot + String.Format( " - {0}% complete", (progressBar.Value * 100 ) / (progressBar.Maximum - progressBar.Minimum) );
            //Text = String.Format("Progress : {0}% ", (progressBar.Value * 100) / (progressBar.Maximum - progressBar.Minimum));
        }
		
		/// <summary>
		/// Utility function to terminate the thread
		/// </summary>
		private void AbortWork()
		{
			abortEvent.Set();
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.txtInfoBox = new System.Windows.Forms.TextBox();
            this.pictureBoxBigPIC = new System.Windows.Forms.PictureBox();
            this.lbDate1 = new System.Windows.Forms.Label();
            this.lbDate2 = new System.Windows.Forms.Label();
            this.lbDate3 = new System.Windows.Forms.Label();
            this.lbDate4 = new System.Windows.Forms.Label();
            this.lbDate5 = new System.Windows.Forms.Label();
            this.lbTMP1 = new System.Windows.Forms.Label();
            this.lbTMP2 = new System.Windows.Forms.Label();
            this.lbTMP3 = new System.Windows.Forms.Label();
            this.lbTMP4 = new System.Windows.Forms.Label();
            this.lbTMP5 = new System.Windows.Forms.Label();
            this.pictureBoxTMPPic = new System.Windows.Forms.PictureBox();
            this.lbTMP = new System.Windows.Forms.Label();
            this.lbTMPValue = new System.Windows.Forms.Label();
            this.lbTMPInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBigPIC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTMPPic)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label.Location = new System.Drawing.Point(-6, 625);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(751, 62);
            this.label.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.ForeColor = System.Drawing.Color.Crimson;
            this.progressBar.Location = new System.Drawing.Point(-2, 690);
            this.progressBar.Name = "progressBar";
            this.progressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progressBar.Size = new System.Drawing.Size(1329, 10);
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            this.progressBar.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft JhengHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(987, 643);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(152, 41);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "STOP";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(778, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(101, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(885, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(101, 144);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(992, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(101, 144);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(1099, 12);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(101, 144);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(1206, 12);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(101, 144);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 3;
            this.pictureBox5.TabStop = false;
            // 
            // txtInfoBox
            // 
            this.txtInfoBox.Font = new System.Drawing.Font("Microsoft JhengHei", 9F);
            this.txtInfoBox.Location = new System.Drawing.Point(779, 544);
            this.txtInfoBox.Multiline = true;
            this.txtInfoBox.Name = "txtInfoBox";
            this.txtInfoBox.ReadOnly = true;
            this.txtInfoBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfoBox.Size = new System.Drawing.Size(529, 93);
            this.txtInfoBox.TabIndex = 4;
            // 
            // pictureBoxBigPIC
            // 
            this.pictureBoxBigPIC.Location = new System.Drawing.Point(15, 13);
            this.pictureBoxBigPIC.Name = "pictureBoxBigPIC";
            this.pictureBoxBigPIC.Size = new System.Drawing.Size(750, 675);
            this.pictureBoxBigPIC.TabIndex = 5;
            this.pictureBoxBigPIC.TabStop = false;
            // 
            // lbDate1
            // 
            this.lbDate1.AutoSize = true;
            this.lbDate1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbDate1.Location = new System.Drawing.Point(772, 162);
            this.lbDate1.Name = "lbDate1";
            this.lbDate1.Size = new System.Drawing.Size(90, 36);
            this.lbDate1.TabIndex = 6;
            this.lbDate1.Text = "00:00";
            // 
            // lbDate2
            // 
            this.lbDate2.AutoSize = true;
            this.lbDate2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbDate2.Location = new System.Drawing.Point(879, 162);
            this.lbDate2.Name = "lbDate2";
            this.lbDate2.Size = new System.Drawing.Size(90, 36);
            this.lbDate2.TabIndex = 6;
            this.lbDate2.Text = "00:00";
            // 
            // lbDate3
            // 
            this.lbDate3.AutoSize = true;
            this.lbDate3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold);
            this.lbDate3.Location = new System.Drawing.Point(989, 164);
            this.lbDate3.Name = "lbDate3";
            this.lbDate3.Size = new System.Drawing.Size(97, 38);
            this.lbDate3.TabIndex = 6;
            this.lbDate3.Text = "00:00";
            // 
            // lbDate4
            // 
            this.lbDate4.AutoSize = true;
            this.lbDate4.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold);
            this.lbDate4.Location = new System.Drawing.Point(1098, 164);
            this.lbDate4.Name = "lbDate4";
            this.lbDate4.Size = new System.Drawing.Size(97, 38);
            this.lbDate4.TabIndex = 6;
            this.lbDate4.Text = "00:00";
            // 
            // lbDate5
            // 
            this.lbDate5.AutoSize = true;
            this.lbDate5.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold);
            this.lbDate5.Location = new System.Drawing.Point(1203, 164);
            this.lbDate5.Name = "lbDate5";
            this.lbDate5.Size = new System.Drawing.Size(97, 38);
            this.lbDate5.TabIndex = 6;
            this.lbDate5.Text = "00:00";
            // 
            // lbTMP1
            // 
            this.lbTMP1.AutoSize = true;
            this.lbTMP1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbTMP1.Location = new System.Drawing.Point(772, 195);
            this.lbTMP1.Name = "lbTMP1";
            this.lbTMP1.Size = new System.Drawing.Size(90, 36);
            this.lbTMP1.TabIndex = 7;
            this.lbTMP1.Text = "00.00";
            // 
            // lbTMP2
            // 
            this.lbTMP2.AutoSize = true;
            this.lbTMP2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.lbTMP2.Location = new System.Drawing.Point(879, 196);
            this.lbTMP2.Name = "lbTMP2";
            this.lbTMP2.Size = new System.Drawing.Size(90, 36);
            this.lbTMP2.TabIndex = 7;
            this.lbTMP2.Text = "00.00";
            // 
            // lbTMP3
            // 
            this.lbTMP3.AutoSize = true;
            this.lbTMP3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.lbTMP3.Location = new System.Drawing.Point(990, 200);
            this.lbTMP3.Name = "lbTMP3";
            this.lbTMP3.Size = new System.Drawing.Size(90, 36);
            this.lbTMP3.TabIndex = 7;
            this.lbTMP3.Text = "00.00";
            // 
            // lbTMP4
            // 
            this.lbTMP4.AutoSize = true;
            this.lbTMP4.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.lbTMP4.Location = new System.Drawing.Point(1099, 200);
            this.lbTMP4.Name = "lbTMP4";
            this.lbTMP4.Size = new System.Drawing.Size(90, 36);
            this.lbTMP4.TabIndex = 7;
            this.lbTMP4.Text = "00.00";
            // 
            // lbTMP5
            // 
            this.lbTMP5.AutoSize = true;
            this.lbTMP5.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.lbTMP5.Location = new System.Drawing.Point(1204, 200);
            this.lbTMP5.Name = "lbTMP5";
            this.lbTMP5.Size = new System.Drawing.Size(90, 36);
            this.lbTMP5.TabIndex = 8;
            this.lbTMP5.Text = "00.00";
            // 
            // pictureBoxTMPPic
            // 
            this.pictureBoxTMPPic.Location = new System.Drawing.Point(779, 239);
            this.pictureBoxTMPPic.Name = "pictureBoxTMPPic";
            this.pictureBoxTMPPic.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxTMPPic.TabIndex = 9;
            this.pictureBoxTMPPic.TabStop = false;
            // 
            // lbTMP
            // 
            this.lbTMP.AutoSize = true;
            this.lbTMP.Font = new System.Drawing.Font("Microsoft JhengHei UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbTMP.Location = new System.Drawing.Point(1041, 249);
            this.lbTMP.Name = "lbTMP";
            this.lbTMP.Size = new System.Drawing.Size(103, 43);
            this.lbTMP.TabIndex = 7;
            this.lbTMP.Text = "TMP ";
            // 
            // lbTMPValue
            // 
            this.lbTMPValue.AutoSize = true;
            this.lbTMPValue.Font = new System.Drawing.Font("Microsoft JhengHei UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbTMPValue.Location = new System.Drawing.Point(1150, 249);
            this.lbTMPValue.Name = "lbTMPValue";
            this.lbTMPValue.Size = new System.Drawing.Size(0, 43);
            this.lbTMPValue.TabIndex = 7;
            // 
            // lbTMPInfo
            // 
            this.lbTMPInfo.AutoSize = true;
            this.lbTMPInfo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbTMPInfo.Location = new System.Drawing.Point(776, 498);
            this.lbTMPInfo.Name = "lbTMPInfo";
            this.lbTMPInfo.Size = new System.Drawing.Size(0, 43);
            this.lbTMPInfo.TabIndex = 7;
            // 
            // ProgressWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
            this.ClientSize = new System.Drawing.Size(1320, 696);
            this.Controls.Add(this.pictureBoxTMPPic);
            this.Controls.Add(this.lbTMP5);
            this.Controls.Add(this.lbTMP4);
            this.Controls.Add(this.lbTMP3);
            this.Controls.Add(this.lbTMP2);
            this.Controls.Add(this.lbTMPValue);
            this.Controls.Add(this.lbTMPInfo);
            this.Controls.Add(this.lbTMP);
            this.Controls.Add(this.lbTMP1);
            this.Controls.Add(this.lbDate5);
            this.Controls.Add(this.lbDate4);
            this.Controls.Add(this.lbDate3);
            this.Controls.Add(this.lbDate2);
            this.Controls.Add(this.lbDate1);
            this.Controls.Add(this.pictureBoxBigPIC);
            this.Controls.Add(this.txtInfoBox);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ProgressWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProgressWindow";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBigPIC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTMPPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private void cancelButton_Click(object sender, EventArgs e)
        {
            isBegin = false;
            Thread.Sleep(500);
        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }
    }
}
