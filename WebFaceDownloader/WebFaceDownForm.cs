#define _NETWORK_CMD_V2

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebFaceDownloader
{
    public partial class WebFaceDownForm : Form
    {

        Socket m_socClient;
        StreamWriter swLog = null;
        string tmpTMPPicFileName;
        string tmpBigPicFileName;
        string logFileName;

        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

            }
            catch (Exception e)
            {
                //OnErrorEvent(new ErrorEventArgs(e));
                //MessageBox.Show("Error :" + e.Message);
            }
            finally
            {
                connectDone.Set();
            }
        }

        public WebFaceDownForm()
        {
            InitializeComponent();
            //初始化默認居中顯示
            this.StartPosition = FormStartPosition.CenterScreen;

        }


        public class _AI_COMMAND_
        {
            public struct _sc_ai_cmd_t
            {
                // _sc_ai_cmd_t v2
                public UInt16 cmd_1;
                public UInt16 cmd_2; // bit0: wr i2c reg, bit1: rd i2c reg
                public UInt16 rdy_1_0;
                public UInt16 rdy_1_1;
                public UInt16 rdy_2_0;
                public UInt16 rdy_2_1;
                public UInt16 obj_amount_0;
                public UInt16 obj_amount_1;
                public UInt32 transmit_size_0;
                public UInt32 transmit_size_1;
                public UInt32 transmit_checksum_0;
                public UInt32 transmit_checksum_1;
                public UInt32 data_addr_0;
                public UInt32 data_addr_1;
                public UInt16 err_code;
                public UInt16 ai_status;
            } // _sc_ai_cmd_t

            _sc_ai_cmd_t psCmd = new _sc_ai_cmd_t();

            public UInt16[] cmd = new UInt16[2];
            public UInt16[] rdy_1 = new UInt16[2];
            public UInt16[] rdy_2 = new UInt16[2];
            public UInt16[] obj_amount = new UInt16[2];
            public UInt32[] transmit_size = new UInt32[2];
            public UInt32[] transmit_checksum = new UInt32[2];
            public UInt32[] data_addr = new UInt32[2];

            public _AI_COMMAND_(byte[] buffer)
            {
                psCmd = (_sc_ai_cmd_t)BytesToStruct(buffer, psCmd.GetType());

                cmd[0] = psCmd.cmd_1;
                cmd[1] = psCmd.cmd_2;
                rdy_1[0] = psCmd.rdy_1_0;
                rdy_1[1] = psCmd.rdy_1_1;
                rdy_2[0] = psCmd.rdy_2_0;
                rdy_2[1] = psCmd.rdy_2_1;
                obj_amount[0] = psCmd.obj_amount_0;
                obj_amount[1] = psCmd.obj_amount_1;
                transmit_size[0] = psCmd.transmit_size_0;
                transmit_size[1] = psCmd.transmit_size_1;
                transmit_checksum[0] = psCmd.transmit_checksum_0;
                transmit_checksum[1] = psCmd.transmit_checksum_1;
                data_addr[0] = psCmd.data_addr_0;
                data_addr[1] = psCmd.data_addr_1;
            } // _AI_COMMAND_

            public int getAiCmdStructSize()
            {
                return Marshal.SizeOf(psCmd);
            }

            //結構體轉位元組陣列
            public byte[] StructToBytes(object structObj)
            {

                int size = Marshal.SizeOf(structObj);//獲取結構體的大小
                IntPtr buffer = Marshal.AllocHGlobal(size);//分配記憶體
                try
                {
                    Marshal.StructureToPtr(structObj, buffer, false);//將資料從託管物件封裝到非託管內容並不銷毀舊物體
                    byte[] bytes = new byte[size];
                    Marshal.Copy(buffer, bytes, 0, size);//將資料從非託管記憶體複製到陣列內
                    return bytes;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);//釋放記憶體
                }
            }

            //位元組轉結構體
            public object BytesToStruct(byte[] bytes, Type strcutType)
            {
                int size = Marshal.SizeOf(strcutType);
                IntPtr buffer = Marshal.AllocHGlobal(size);
                try
                {
                    Marshal.Copy(bytes, 0, buffer, size);
                    return Marshal.PtrToStructure(buffer, strcutType);
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }

            }

        }

        public class DoThreadWorkInfo
        {
            public object state;
            public string strSouceWebUrl;
            public string strUsrTargetFile;
            public int nSrcPicWidth;
            public int nSrcPicHeight;
            public int nCropFaceStartX;
            public int nCropFaceStartY;
            public int nCropFaceWidth;
            public int nCropFaceHeight;
            public int nFaceResizeWidth;
            public int nFaceReSizeHeight;
            public int nFaceArrayNumHeri;
            public int nFaceArrayNumVert;
            public int nFaceTotalNumber;
            public int nBatchCount;
            public string strComboFileExt;
        }

        public class DownloadImage
        {
            private string imageUrl;
            private Bitmap bitmap;
            public DownloadImage(string imageUrl)
            {
                this.imageUrl = imageUrl;
            }
            public bool Download()
            {
                try
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(imageUrl);
                    bitmap = new Bitmap(stream);
                    stream.Flush();
                    stream.Close();
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    MessageBox.Show(e.Message);
                    return false;
                }

                return true;
            }
            public Bitmap GetImage()
            {
                return bitmap;
            }
            public void SaveImage(string filename, ImageFormat format)
            {
                if (bitmap != null)
                {
                    bitmap.Save(filename, format);
                }
            }
        }

        private Image HorizontalMergeImages(Image img1, Image img2)
        {
            Image MergedImage = default(Image);
            Int32 Wide = 0;
            Int32 High = 0;
            Wide = img1.Width + img2.Width;//設定寬度           
            if (img1.Height >= img2.Height)
            {
                High = img1.Height;
            }
            else
            {
                High = img2.Height;
            }
            Bitmap mybmp = new Bitmap(Wide, High);
            Graphics gr = Graphics.FromImage(mybmp);
            //處理第一張圖片
            gr.DrawImage(img1, 0, 0);
            //處理第二張圖片
            gr.DrawImage(img2, img1.Width, 0);
            MergedImage = mybmp;
            gr.Dispose();
            return MergedImage;
        }
        private Image VerticallMergeImages(Image img1, Image img2)
        {
            Image MergedImage = default(Image);
            Int32 Wide = 0;
            Int32 High = 0;
            High = img1.Height + img2.Height;//設定寬度           
            if (img1.Width >= img2.Width)
            {
                Wide = img1.Width;
            }
            else
            {
                Wide = img2.Width;
            }
            Bitmap mybmp = new Bitmap(Wide, High);
            Graphics gr = Graphics.FromImage(mybmp);
            //處理第一張圖片
            gr.DrawImage(img1, 0, 0);
            //處理第二張圖片
            gr.DrawImage(img2, 0, img1.Height);
            MergedImage = mybmp;
            gr.Dispose();
            return MergedImage;
        }
        private Image imgCrop(Image image, Rectangle selection)
        {
            Bitmap bmp = image as Bitmap;

            // Check if it is a bitmap:
            if (bmp == null)
                throw new ArgumentException("No valid bitmap");

            // Crop the image:
            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

            // Release the resources:
            image.Dispose();

            return cropBmp;
        }

        public Image imgResize(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine($"{logMessage}");
        }

        public void LogMsg(String lmsg, TextWriter w)
        {
            if(cbDumpLog.Checked == true)
            {
                Log(lmsg,w);
            }

            Debug.Print(lmsg);
        }

        private void netIPOpenPort(String szIPSelected, String szPort)
        {
            m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //String szIPSelected = strWebURL.Text;
            //String szPort = strWebPort.Text;
            int alPort = System.Convert.ToInt16(szPort, 10);

            System.Net.IPAddress remoteIPAddress = System.Net.IPAddress.Parse(szIPSelected);
            System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, alPort);

            //m_socClient.Connect(remoteEndPoint);
            connectDone.Reset();
            m_socClient.BeginConnect(remoteEndPoint, new AsyncCallback(ConnectCallback), m_socClient);
            connectDone.WaitOne(3000, false);//等待3秒
        }

        private bool netIsConnect()
        {
            return m_socClient.Connected;
        }

        private void netIPClosePort()
        {
            m_socClient.Close();
            m_socClient.Dispose();
        }

        private bool netGetOneTMPRaw(String szData, String tmpFileName)
        {
            int tmp_size = 2048;
            int rx = 0, received = 0;

            byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);
            byte[] pTMP = new byte[tmp_size];

            m_socClient.Send(byData, byData.Length, SocketFlags.None);
            //Thread.Sleep(200);

            received = 0;
            do
            {
                rx = m_socClient.Receive(pTMP, received, tmp_size - received, SocketFlags.None);
                received += rx;

            } while (received < tmp_size); // 多收的32是夾帶的溫度時間訊息

            // save file
            FileStream fsFile = new FileStream(tmpFileName, FileMode.Create);
            fsFile.Seek(0, SeekOrigin.Begin);
            fsFile.Write(pTMP, 0, tmp_size);
            fsFile.Close();

            return true;
        }


        private byte[] netGetTmpMaxInfo()
        {
            int rx = 0, data_lens, received = 0;

            string szData = "GET TMP MAX";
            byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);
            byte[] byLens = new byte[4];

            m_socClient.Send(byData, byData.Length, SocketFlags.None);
            //Thread.Sleep(200);
            rx = m_socClient.Receive(byLens, 0, 4, SocketFlags.None);
            //Thread.Sleep(200);

            data_lens = BitConverter.ToInt32(byLens, 0);

            if (data_lens == 0x06)
            {
                byte[] byRecvData = new byte[data_lens];

                received = 0;
                do
                {
                    rx = m_socClient.Receive(byRecvData, received, data_lens - received, SocketFlags.None);
                    received += rx;

                } while (received < data_lens);

                return byRecvData;
            }

            return null;
        }

        private _AI_COMMAND_ netSendCmd(String cmd)
        {
            int rx = 0;
            int bycmd_size = Marshal.SizeOf(typeof(_AI_COMMAND_._sc_ai_cmd_t));

            byte[] byData = System.Text.Encoding.ASCII.GetBytes(cmd);
            byte[] byLens = new byte[4];
            byte[] bycmd_status = new byte[bycmd_size];

            m_socClient.Send(byData, byData.Length, SocketFlags.None);
            //Thread.Sleep(200);
            rx = m_socClient.Receive(bycmd_status, 0, bycmd_size, SocketFlags.None);
            //Tread.Sleep(200);

            return new _AI_COMMAND_(bycmd_status);
        }

        private int netSetOneBigPhoto()
        {
            int ppindex = -1;

            // Command 送出 STR_SET_AI_COMMAND_BIG_PHOTO ( 0x01 )
            _AI_COMMAND_ aiCmdSet = netSendCmd("SET AI CMD BP");

            if (aiCmdSet.cmd[0] == 0x01) 
            {
                ppindex = 0;
            }
            else if (aiCmdSet.cmd[1] == 0x01)
            {
                ppindex = 1;
            }

            LogMsg(String.Format("aiCmdSet : cmd[0] = 0x{0:x4}, cmd[1] = 0x{1:x4}, obj[0]=0x{2:x4}, obj[1]=0x{3:x4}, rdy_1[0] = 0x{4:x4}, rdy_1[1] = 0x{5:x4}"
                    , aiCmdSet.cmd[0], aiCmdSet.cmd[1]
                    , aiCmdSet.obj_amount[0], aiCmdSet.obj_amount[1]
                    , aiCmdSet.rdy_1[0], aiCmdSet.rdy_1[1]),swLog);

            if((aiCmdSet.cmd[0] > 0x200) || (aiCmdSet.cmd[1] > 0x200) || (aiCmdSet.obj_amount[0] > 0x005) || (aiCmdSet.obj_amount[0] > 0x005))
                return -1; // 資料混亂掉 Error ( 如 SET_AI_CMD_BP ) 時, 去取到了 (GET_AI_CMD_FB ) 所傳送的數據, rdy 沒確認好清空就來讀大圖時會機率性發生這種問題

            return ppindex;
        }

        private int netGetOneBigPhoto(int ppindex = -1, String tmpFileName = "")
        {
            int rx = 0;
            int received = 0;
            int S16obj_num = 0;
            int jpg_size = 0;

            // Command 送出 STR_SET_AI_COMMAND_BIG_PHOTO ( 0x01 )
            //_AI_COMMAND_ aiCmdSet = netSendCmd("SET AI CMD BP");
            //ppindex = netSetOneBigPhoto();

            if (ppindex >= 0)
            {
                // Check STR_SET_AI_COMMAND_BIG_PHOTO ( 0x01 ) command is Ready
                _AI_COMMAND_ aiCmdGet;
                int cntWaitRdy = 0;
                do
                {
                    aiCmdGet = netSendCmd("GET AI CMD BP");
                    LogMsg(String.Format("aiCmdGet : cmd[0] = 0x{0:x4}, cmd[1] = 0x{1:x4}, obj[0]=0x{2:x4}, obj[1]=0x{3:x4}, rdy_1[0] = 0x{4:x4}, rdy_1[1] = 0x{5:x4}"
                                , aiCmdGet.cmd[0], aiCmdGet.cmd[1]
                                , aiCmdGet.obj_amount[0], aiCmdGet.obj_amount[1]
                                , aiCmdGet.rdy_1[0], aiCmdGet.rdy_1[1]),swLog);

                    // BUG Note here : 如果 cmd == 0x201 , 表示 FW 發生錯誤 , 0x200 及 0x001 二個 command 衝在一起了, 此時05p那端人臉框己卡住
                    // 這在 FW V12B 之前的版本會發生
                    if ((aiCmdGet.cmd[0] == 0x201) || (aiCmdGet.cmd[1] == 0x201))
                    {
                        return -1;
                    }
                    else if ((aiCmdGet.cmd[0] == 0x200) || (aiCmdGet.cmd[1] == 0x200))
                    {
                        // FB圖插進來了, 放棄
                        return -2;
                    }
                    if (cntWaitRdy++ > 10)
                        break; // timeout

                    Thread.Sleep(250);
                } while (aiCmdGet.rdy_1[ppindex] != 0x01);


                if (aiCmdGet.rdy_1[ppindex] == 0x01)
                {
                    jpg_size = (int)aiCmdGet.transmit_size[ppindex];
                    //
                    S16obj_num = (short)aiCmdGet.obj_amount[ppindex];
                    //
                    byte[] byRecvData = new byte[jpg_size];

                    Stopwatch sw = new Stopwatch();

                    sw.Reset();
                    sw.Start();

                    received = 0;

                    do
                    {
                        rx = m_socClient.Receive(byRecvData, received, jpg_size - received, SocketFlags.None);
                        received += rx;
                    } while (received < jpg_size);

                    sw.Stop();

                    // save jpeg file
                    FileStream fsFile = new FileStream(tmpFileName, FileMode.Create);
                    fsFile.Seek(0, SeekOrigin.Begin);
                    fsFile.Write(byRecvData, 6, jpg_size - 6);
                    fsFile.Close();

                    return 0x01;
                } // rdy_1

            } // index

            return 0;
        } // netGetOneBigPhoto

        /*
            #define STR_HELLO_MIPY                    "Hello Mipy\0"
            #define STR_THIS_MIPY                     "This is Mipy\0"
            #define STR_UNKNOW_ERROR                  "Unknow Error\0"
            #define STR_SNAP_SHUT                     "Snap Shot\0"
            #define STR_SET_AI_COMMAND_BIG_PHOTO      "SET AI CMD BP\0"
            #define STR_SET_AI_COMMAND_SMALL_PHOTO    "SET AI CMD SP\0"
            #define STR_SET_AI_COMMAND_FB_PHOTO       "SET AI CMD FB\0"
            #define STR_SET_AI_DATE                   "SET AI DATE\0"
            #define STR_GET_AI_TMP_MAX                "GET TMP MAX\0"

            #define STR_GET_AI_COMMAND_FB_PHOTO       "GET AI CMD FB\0"
            #define STR_GET_AI_COMMAND_TMP_PHOTO      "GET TMP CMD\0"
            #define STR_SET_AI_COMMAND_INFORMATION    "SET AI CMD INFOR\0"
            #define STR_GET_AI_COMMAND_BIG_PHOTO      "GET AI CMD BP\0"
            #define STR_GET_AI_COMMAND_SMALL_PHOTO    "GET AI CMD SP\0"
            #define STR_GET_AI_COMMAND_INFORMATION    "GET AI CMD INFOR\0"
         */

        private void Do05PAiFBFaceDownloadThreadWork(object status)
        {
            DoThreadWorkInfo info = status as DoThreadWorkInfo;
            IProgressCallback callback = info.state as IProgressCallback;

            bool bCmd201Error = false;
            int netStage = 0;
            int num_of_jpg = 0;
            int cntConnectNetRetry = 0;
            string tmpTitleMsg = "";
          

            int received = 0;
            int snPic = 0;
            _AI_COMMAND_ aiCmdFB;

            if (cbDumpLog.Checked == true)
            {
                //FileStream fsFile = new FileStream(@"d:\log.txt", FileMode.OpenOrCreate);
                swLog = File.AppendText(logFileName);
                swLog.WriteLine($"\r\nLog at {DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                swLog.WriteLine("-------------------------------\r\n");
            }

            try
            {
                List<string> listFaceFileName = new List<string>();
                int StartStep = 0;

                callback.Begin(0, 100);

                Rectangle rcCrop = new Rectangle((info.nSrcPicWidth - info.nCropFaceWidth) / 2,
                                                 (info.nSrcPicHeight - info.nCropFaceHeight) / 2,
                                                  info.nCropFaceWidth,
                                                  info.nCropFaceHeight);

                /////////////////////////////////////////////////////////////////////////////////
                do
                {
                    // Connect to URL HTTP:// ...
                    netIPOpenPort(strWebURL.Text, strWebPort.Text);

                    // 連接成功
                    if (m_socClient.Connected)
                    {
                        cntConnectNetRetry = 0;

                        tmpTitleMsg = "Device is connedted";
                        callback.SetTitle(tmpTitleMsg);

                        int jpg_size = 0;
                        short S16obj_num = 0;
                        int ppindex = -1;

                        try
                        {
                            received = 0;
                            jpg_size = 0;
                            S16obj_num = 0;

                            aiCmdFB = netSendCmd("GET AI CMD FB");

                            LogMsg(String.Format("netStage={0}, aiCmdFB : cmd[0]=0x{1:x4}, cmd[1]=0x{2:x4}, obj[0]=0x{3:x4}, obj[1]=0x{4:x4}, rdy_1[0]=0x{5:x4}, rdy_1[1]=0x{6:x4}",
                                                 netStage
                                                 , aiCmdFB.cmd[0], aiCmdFB.cmd[1]
                                                 , aiCmdFB.obj_amount[0], aiCmdFB.obj_amount[1]
                                                 , aiCmdFB.rdy_1[0], aiCmdFB.rdy_1[1]), swLog);

                            if ((aiCmdFB.cmd[0] == 0x201) || (aiCmdFB.cmd[1] == 0x201))
                            {
                                bCmd201Error = true;                               
                            }
                            else // cmd is not 0x201 error
                            {
                                switch (netStage)
                                {
                                    //////////////////////////////////////////////////////////////////////////////
                                    default:
                                    case 0: // Check FB_PHOTO
                                        if ((aiCmdFB.rdy_1[0] == 0x200) || (aiCmdFB.rdy_1[1] == 0x200))
                                        {
                                            netStage = 0;

                                            if (aiCmdFB.rdy_1[0] == 0x200)
                                            {
                                                ppindex = 0;
                                            }
                                            else if (aiCmdFB.rdy_1[1] == 0x200)
                                            {
                                                ppindex = 1;
                                            }

                                            if (ppindex >= 0)
                                            {
                                                jpg_size = (int)aiCmdFB.transmit_size[ppindex];
                                                //
                                                S16obj_num = (short)aiCmdFB.obj_amount[ppindex];
                                                //
                                                byte[] byRecvData = new byte[jpg_size];

                                                Stopwatch sw = new Stopwatch();

                                                sw.Reset();
                                                sw.Start();

                                                received = 0;
                                                do
                                                {
                                                    int rx = m_socClient.Receive(byRecvData, received, jpg_size - received, SocketFlags.None);
                                                    received += rx;

                                                } while (received < jpg_size); // 多收的32是夾帶的溫度時間訊息

                                                sw.Stop();

                                                num_of_jpg = BitConverter.ToInt16(byRecvData, 0);

                                                tmpTitleMsg += "...Got " + num_of_jpg.ToString() + " Face";
                                                callback.SetTitle(tmpTitleMsg);
                                                callback.SetText("Got " + num_of_jpg.ToString() + " Face" + "\r\n");
                                                //Thread.Sleep(1000);

                                                byte[] pJpg = byRecvData;
                                                int jpg_offset = 0;
                                                Boolean bBigPic = false;

                                                String.Format("", num_of_jpg);

                                                if (netGetOneTMPRaw("GET TMP CMD", tmpTMPPicFileName))
                                                {
                                                    callback.SetImageTmpPIC(tmpTMPPicFileName);
                                                }

                                                for (int i = 0; i < num_of_jpg; i++)
                                                {
                                                    int jpg_size_addr = i * 4 + 2;
                                                    int jpg_start_addr = num_of_jpg * 4 + 2 + jpg_offset;
                                                    int jpg_total_size = BitConverter.ToInt32(byRecvData, jpg_size_addr);
                                                    int jpg_bitstream_size = jpg_total_size - 32;

                                                    //int jpg_w = BitConverter.ToUInt16(byRecvData, jpg_start_addr +  jpg_bitstream_size + 2);
                                                    //int jpg_h = BitConverter.ToUInt16(byRecvData, jpg_start_addr +  jpg_bitstream_size + 4);
                                                    int jpg_w = BitConverter.ToUInt16(byRecvData, jpg_start_addr + 78);// jpg_bitstream_size + 2);
                                                    int jpg_h = BitConverter.ToUInt16(byRecvData, jpg_start_addr + 80);// jpg_bitstream_size + 4);

                                                    int tmp = 0, year = 0, month = 0, date = 0, hour = 0, minute = 0, sec = 0, frame_id;

                                                    if ((jpg_w > 0x90) && (jpg_h > 0xc0))
                                                        bBigPic = true;
                                                    else
                                                        bBigPic = false;

                                                    if (bBigPic)
                                                    {
                                                        jpg_w = BitConverter.ToUInt16(byRecvData, jpg_start_addr + 78);// jpg_bitstream_size + 2);
                                                        jpg_h = BitConverter.ToUInt16(byRecvData, jpg_start_addr + 80);// jpg_bitstream_size + 4);
                                                        frame_id = 0;
                                                    }
                                                    else // bBigPic == false
                                                    {
                                                        tmp = BitConverter.ToUInt16(byRecvData, jpg_start_addr + jpg_bitstream_size + 6);
                                                        year = BitConverter.ToUInt16(byRecvData, jpg_start_addr + jpg_bitstream_size + 8);
                                                        month = BitConverter.ToUInt16(byRecvData, jpg_start_addr + jpg_bitstream_size + 10);
                                                        date = BitConverter.ToUInt16(byRecvData, jpg_start_addr + jpg_bitstream_size + 12);
                                                        hour = BitConverter.ToUInt16(byRecvData, jpg_start_addr + jpg_bitstream_size + 14);
                                                        minute = BitConverter.ToUInt16(byRecvData, jpg_start_addr + jpg_bitstream_size + 16);
                                                        sec = BitConverter.ToUInt16(byRecvData, jpg_start_addr + jpg_bitstream_size + 18);
                                                        frame_id = BitConverter.ToUInt16(byRecvData, jpg_start_addr + jpg_bitstream_size + 20);

                                                    } // bBigPic

                                                    double time_result = sw.Elapsed.TotalMilliseconds;

                                                    //string tmpFileName = @"_face" + i.ToString() + ".jpg";
                                                    //string tmpFileNameFullPath = (tmpFileFolderPath + "\\" + tmpFileName);
                                                    //string saveFileName = textBoxSavingPatch.Text + "\\" + "snapshot_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
                                                    string strFaceTmp = (tmp / 10).ToString() + "." + (tmp % 10).ToString() + "℃";

                                                    string strFaceDate = year.ToString("00") + "_"
                                                                        + month.ToString("00") + "_"
                                                                        + date.ToString("00") + "_"
                                                                        + hour.ToString("00") + "_"
                                                                        + minute.ToString("00") + "_"
                                                                        + sec.ToString("00");

                                                    string tmpFileName = "Face_" + i.ToString() + "_" + strFaceTmp + strFaceDate + "_no" + (snPic++).ToString() + ".jpg";

                                                    string tmpFileNameFullPath = textBoxSavingPatch.Text + "\\" + tmpFileName;

                                                    //callback.SetText("Save " + Path.GetFileName(tmpFileNameFullPath) + "\r\n");
                                                    //callback.SetText("Trans Size = " + received.ToString() + " bytes" + "\r\n");
                                                    //callback.SetText("Trans Time = " + time_result.ToString() + "ms" + "\r\n");
                                                    //callback.SetText("Trans Rate = " + ((int)(received / time_result)).ToString() + " (KB/s)" + "\r\n");

                                                    callback.SetText("Face[" + i.ToString() + "]_" + strFaceTmp + "_at_" + strFaceDate + "\r\n");
                                                    //Thread.Sleep(1000);

                                                    // save jpeg file
                                                    FileStream fsFile = new FileStream(tmpFileNameFullPath, FileMode.Create);
                                                    fsFile.Seek(0, SeekOrigin.Begin);

                                                    //fsFile.Write(byRecvData, (2 + S16obj_num * 4), (received - (2 + S16obj_num * 4))); //fsFile.Write(byRecvData, 0, received);// (int)fileStream.Length);
                                                    fsFile.Write(byRecvData, num_of_jpg * 4 + 2 + jpg_offset, jpg_total_size);
                                                    fsFile.Close();

                                                    //jpg_offset 向後累積 jpeg size
                                                    jpg_offset += jpg_total_size;

                                                    listFaceFileName.Add(tmpFileNameFullPath);

                                                    if (bBigPic)
                                                    {
                                                        callback.SetImageBigPIC(tmpFileNameFullPath);

                                                        if (StartStep == 0) // 高溫大圖先一步手動大圖, 所以再此設定不再手動抓取第一張大圖
                                                            StartStep = 2;
                                                    }
                                                    else // !bBigPic
                                                    {
                                                        //callback.SetText(@"Downloading pictures " +  @"/" + info.nBatchCount.ToString() + @") " + @"... " + tmpFileName);
                                                        callback.SetImage(tmpFileNameFullPath, tmp, frame_id, year, month, date, hour, minute, sec);
                                                        callback.StepTo(i * 100 / num_of_jpg);
                                                    } // bBigPic

                                                    //Thread.Sleep(1000);

                                                    //ProcessStartInfo open = new ProcessStartInfo();
                                                    //open.FileName = saveFileName;
                                                    //Process.Start(open);
                                                } // for, i
                                            } // ppindex

                                        }
                                        else if ((aiCmdFB.rdy_1[0] == 0x001) || (aiCmdFB.rdy_1[1] == 0x001)) // 有殘留的大圖沒處理
                                        {

                                            int index = -1;
                                            if (aiCmdFB.rdy_1[0] == 0x001)
                                                index = 0;
                                            else if (aiCmdFB.rdy_1[1] == 0x001)
                                                index = 1;

                                            int getBigRet = netGetOneBigPhoto(index, tmpBigPicFileName);

                                            if (getBigRet == 0)
                                            {
                                                // not ready
                                            }
                                            else if (getBigRet == -1)
                                            {
                                                bCmd201Error = true;
                                                break;
                                            }
                                            else if (getBigRet == -2)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                callback.SetImageBigPIC(tmpBigPicFileName);

                                                if (netGetOneTMPRaw("GET TMP CMD", tmpTMPPicFileName))
                                                {
                                                    callback.SetImageTmpPIC(tmpTMPPicFileName);
                                                } // GET TMP CMD
                                                StartStep = 2;
                                            }
                                        }
                                        else
                                        {
                                            // ther is no any FB Photo 
                                            if (StartStep < 1)
                                                netStage = 2; // Next : Get Big Photo at the first run time
                                            else
                                                netStage = 3; // Next :normal running stage
                                        }
                                        break;
                                    //////////////////////////////////////////////////////////////////////////////
                                    case 1: // Get FB_PHOTO

                                        break;
                                    //////////////////////////////////////////////////////////////////////////////
                                    case 2:// Get BIG Photo

                                        if (StartStep == 0)
                                        {
                                            if ((aiCmdFB.rdy_1[0] == 0x000) && (aiCmdFB.rdy_1[1] == 0x000)) // 確保 buffer 內無 ready 圖才能抓大圖
                                            {
                                                int getBigRet = netGetOneBigPhoto(netSetOneBigPhoto(), tmpBigPicFileName);

                                                if (getBigRet == 0)
                                                {
                                                    // not ready
                                                }
                                                else if (getBigRet == -1)
                                                {
                                                    bCmd201Error = true;
                                                }
                                                else if (getBigRet == -2)
                                                {

                                                }
                                                else
                                                {
                                                    callback.SetImageBigPIC(tmpBigPicFileName);
                                                    //bSetCMDFB = true;
                                                    StartStep++;
                                                }

                                                if (StartStep == 1)
                                                {
                                                    if (netGetOneTMPRaw("GET TMP CMD", tmpTMPPicFileName))
                                                    {
                                                        callback.SetImageTmpPIC(tmpTMPPicFileName);
                                                        StartStep++;

                                                    } // GET TMP CMD
                                                } // StartStep

                                            } // check rdy_1 buffer is empty

                                        } // StartStep == 0

                                        netStage = 0; // next back to stage0       
                                        break;
                                    //////////////////////////////////////////////////////////////////////////////
                                    case 3:
                                        netStage = 0; // next back to stage0
                                                      // tmpTitleMsg += "...Got 0 Face";
                                                      // callback.SetTitle(tmpTitleMsg);

                                        if (snPic > 0)
                                        {
                                            byte[] byRecvInfo = netGetTmpMaxInfo();

                                            if (byRecvInfo != null)
                                            {
                                                int num = BitConverter.ToInt16(byRecvInfo, 0);
                                                int frame_id = BitConverter.ToInt16(byRecvInfo, 2);
                                                int tmp = BitConverter.ToInt16(byRecvInfo, 4);

                                                for (int i = 0; i < num; i++)
                                                {
                                                    callback.SetMaxTempUpdate(frame_id, tmp);
                                                } // i

                                            } // byRecvInfo
                                              //callback.SetText("num=" + num.ToString() + " ,id=" + frame_id.ToString() + " ,tmp=" + tmp.ToString() + "\r\n");
                                        }

                                        break;
                                } // switch
                            } // cmd 0x201 check
                           
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            //break;
                        }

                    }
                    else // !m_socClient.Connected
                    {
                        //MessageBox.Show("连接失败! ");
                        //callback.SetText("连接失败!\r\n");
                        tmpTitleMsg = "Device connected failed !";
                        callback.SetTitle(tmpTitleMsg);

                        if(cntConnectNetRetry++ > 5)
                        {
                            break;
                        }
                    }

                    callback.StepTo(100);

                    if (bCapAndSave.Checked == false)
                    {
                        for (int k = 0; k < listFaceFileName.Count; k++)
                        {
                            File.Delete(listFaceFileName[k]);
                        } // k

                    } // bCapAndSave.Checked

                    listFaceFileName.Clear();
                    // Directory.Delete(tmpFileFolderPath);
                    callback.StepTo(100);

                    netIPClosePort();
                    //m_socClient.Close();
                    //m_socClient.Dispose();

                    if (bCmd201Error == true)
                    {
                        MessageBox.Show("Cmd 201 Error ! ");
                        break;
                    }

                    Thread.Sleep(250);
                }
                while (callback.IsAborting == false);

                callback.SetText(@"Done.");
            }
            catch (System.InvalidOperationException)
            {

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (System.Threading.ThreadInterruptedException)
            {

            }
            finally
            {

            }

            if (callback != null)
            {
                callback.End();
            }

            if (netIsConnect())
            {
                netIPClosePort();
            }

            if (cbClearTheTempFiles.Checked == true)
            {
                if (File.Exists(tmpBigPicFileName) == true)
                {
                    File.Delete(tmpBigPicFileName);
                }

                if (File.Exists(tmpTMPPicFileName) == true)
                {
                    File.Delete(tmpTMPPicFileName);
                }
            }

            if (cbDumpLog.Checked == true)
            {
                swLog.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void textFaceArrayNumHeri_TextChanged(object sender, EventArgs e)
        {
            textTotalFaceNumber.Text = Convert.ToString(Convert.ToInt16(textFaceArrayNumHeri.Text)
                                       * Convert.ToInt16(textFaceArrayNumVert.Text));
        }

        private void textFaceArrayNumVert_TextChanged(object sender, EventArgs e)
        {
            textTotalFaceNumber.Text = Convert.ToString(Convert.ToInt16(textFaceArrayNumHeri.Text)
                                       * Convert.ToInt16(textFaceArrayNumVert.Text));
        }

        private void WebFaceDownForm_Load(object sender, EventArgs e)
        {
            textTotalFaceNumber.Text = Convert.ToString(Convert.ToInt16(textFaceArrayNumHeri.Text)
                                       * Convert.ToInt16(textFaceArrayNumVert.Text));

            string file_name = Path.GetFullPath(textTargetMergedFile.Text);

            if (comboFileExt.Text == @"PNG")
            {
                textTargetMergedFile.Text = Path.ChangeExtension(file_name, @".png");
            }
            else if (comboFileExt.Text == @"BMP")
            {
                textTargetMergedFile.Text = Path.ChangeExtension(file_name, @".bmp");
            }
            else // Jpeg
            {
                textTargetMergedFile.Text = Path.ChangeExtension(file_name, @".jpg");
            }

            if (textBoxSavingPatch.Text == "")
            {
                textBoxSavingPatch.Text = System.Environment.CurrentDirectory;
            }

            tmpTMPPicFileName = textBoxSavingPatch.Text + "\\TmpPic.raw";
            tmpBigPicFileName = textBoxSavingPatch.Text + "\\TmpBig.jpg";
            logFileName = textBoxSavingPatch.Text + "\\TmpLog.txt";
      
        }

        private void comboFileExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file_name = Path.GetFullPath(textTargetMergedFile.Text);

            if (comboFileExt.Text == @"PNG")
            {
                textTargetMergedFile.Text = Path.ChangeExtension(file_name, @".png");
            }
            else if (comboFileExt.Text == @"BMP")
            {
                textTargetMergedFile.Text = Path.ChangeExtension(file_name, @".bmp");
            }
            else // Jpeg
            {
                textTargetMergedFile.Text = Path.ChangeExtension(file_name, @".jpg");
            }

            // var result = Path.ChangeExtension(myffile, ".jpg");
            // In the case if you also want to physically change the extension, you could use File.Move method:
            // File.Move(myffile, Path.ChangeExtension(myffile, ".jpg"));
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            try
            {

                netIPOpenPort(strWebURL.Text, strWebPort.Text);
                //Thread.Sleep(100);
                netIPClosePort();
                Thread.Sleep(300);
                netIPOpenPort(strWebURL.Text, strWebPort.Text);
                //Thread.Sleep(100);

                /*
                m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                String szIPSelected = strWebURL.Text;
                String szPort = strWebPort.Text;
                int alPort = System.Convert.ToInt16(szPort, 10);

                System.Net.IPAddress remoteIPAddress = System.Net.IPAddress.Parse(szIPSelected);
                System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, alPort);

                //m_socClient.Connect(remoteEndPoint);
                connectDone.Reset();
                m_socClient.BeginConnect(remoteEndPoint, new AsyncCallback(ConnectCallback), m_socClient);
                connectDone.WaitOne(2000, false);//等待5秒
                */

                //连接成功
                if (m_socClient.Connected)
                {
                    //do something
                    //MessageBox.Show("连接成功");
                }
                else
                {
                    MessageBox.Show("连接失败! ");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败! " + "Error : " + ex.Message);
                return;
            }

            String szData = "Hello Mipy";
            byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);
            byte[] byLens = new byte[4];
            int rx = 0, received = 0;
            int data_lens = 0;
            bool bSuccess = false;

            try
            {
                textMsgBox.Text += "=========================\r\n";
                textMsgBox.Text += "Tx : " + szData + "\r\n";

                m_socClient.Send(byData, byData.Length, SocketFlags.None);
                //m_socClient.ReceiveTimeout = 50000;

                Stopwatch sw = new Stopwatch();

                sw.Reset();
                sw.Start();

                rx = m_socClient.Receive(byLens, 0, 4, SocketFlags.None);

                data_lens = BitConverter.ToInt32(byLens, 0);

                byte[] byRecvData = new byte[data_lens];

                received = 0;
                do
                {
                    rx = m_socClient.Receive(byRecvData, received, data_lens - received, SocketFlags.None);
                    received += rx;

                } while (received < data_lens);

                string retSting = Encoding.UTF8.GetString(byRecvData);

                sw.Stop();

                string time_result = sw.Elapsed.TotalMilliseconds.ToString();

                textMsgBox.Text += "Rx : " + retSting + "\r\n";

                textMsgBox.Text += "Trans Time = " + time_result.ToString() + "ms" + "\r\n";

                bSuccess = true;

            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
                bSuccess = false;
            }

            netIPClosePort();

            if (bSuccess == true)
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败! ");
            }
            //m_socClient.Close();
        }

        private void btnGetFBFaces_Click(object sender, EventArgs e)
        {
            try
            {
                DoThreadWorkInfo info = new DoThreadWorkInfo();
                ProgressWindow progress = new ProgressWindow();

                //progress.Text = "Processing...";

                info.state = progress;
                info.strSouceWebUrl = strWebURL.Text;
                info.strUsrTargetFile = textTargetMergedFile.Text;
                info.nSrcPicWidth = Convert.ToInt16(TextSrcPicWidth.Text);
                info.nSrcPicHeight = Convert.ToInt16(TextSrcPicHeight.Text);
                info.nCropFaceStartX = Convert.ToInt16(textCropFaceStartX.Text);
                info.nCropFaceStartY = Convert.ToInt16(textCropFaceStartY.Text);
                info.nCropFaceWidth = Convert.ToInt16(textCropFaceWidth.Text);
                info.nCropFaceHeight = Convert.ToInt16(textCropFaceHeight.Text);
                info.nFaceResizeWidth = Convert.ToInt16(textFaceResizeWidth.Text);
                info.nFaceReSizeHeight = Convert.ToInt16(textFaceReSizeHeight.Text);
                info.nFaceArrayNumHeri = Convert.ToInt16(textFaceArrayNumHeri.Text);
                info.nFaceArrayNumVert = Convert.ToInt16(textFaceArrayNumVert.Text);
                info.nFaceTotalNumber = Convert.ToInt16(textTotalFaceNumber.Text);
                info.nBatchCount = Convert.ToInt16(textBatchCount.Text);
                info.strComboFileExt = comboFileExt.SelectedItem.ToString();
               
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Do05PAiFBFaceDownloadThreadWork), info);
                progress.ShowDialog();

                while (progress.isBegin != false) // wait thread done 
                {
                    Thread.Sleep(200);
                }
            }
            catch (Exception asdf)
            {
                MessageBox.Show("Error:" + asdf.Message);
            }
            finally
            {

            }


        } //         private void btnGetFBFaces_Click(object sender, EventArgs e)

        private void btnUpdateDate_Click(object sender, EventArgs e)
        {
            //szData = szData.Replace(szData, "SET AI DATE\0"); // Length = 12
            byte[] byData = System.Text.Encoding.ASCII.GetBytes("SET AI DATE\0");
            byte[] byDataWithDate = new byte[byData.Length + 6 * 2]; // 年月日時分秒各2個byte => 6*2

            byData.CopyTo(byDataWithDate, 0);
            // Year
            byDataWithDate[byData.Length + 0] = (byte)((UInt16)DateTime.Now.Year & 0x00FF);
            byDataWithDate[byData.Length + 1] = (byte)(((UInt16)DateTime.Now.Year & 0xFF00) / 256);
            // month
            byDataWithDate[byData.Length + 2] = (byte)((UInt16)DateTime.Now.Month & 0x00FF);
            byDataWithDate[byData.Length + 3] = (byte)(((UInt16)DateTime.Now.Month & 0xFF00) / 256);
            // Day
            byDataWithDate[byData.Length + 4] = (byte)((UInt16)DateTime.Now.Day & 0x00FF);
            byDataWithDate[byData.Length + 5] = (byte)(((UInt16)DateTime.Now.Day & 0xFF00) / 256);
            // Hour
            byDataWithDate[byData.Length + 6] = (byte)((UInt16)DateTime.Now.Hour & 0x00FF);
            byDataWithDate[byData.Length + 7] = (byte)(((UInt16)DateTime.Now.Hour & 0xFF00) / 256);
            // minute
            byDataWithDate[byData.Length + 8] = (byte)((UInt16)DateTime.Now.Minute & 0x00FF);
            byDataWithDate[byData.Length + 9] = (byte)(((UInt16)DateTime.Now.Minute & 0xFF00) / 256);
            // sec
            byDataWithDate[byData.Length + 10] = (byte)((UInt16)DateTime.Now.Second & 0x00FF);
            byDataWithDate[byData.Length + 11] = (byte)(((UInt16)DateTime.Now.Second & 0xFF00) / 256);

            netIPOpenPort(strWebURL.Text, strWebPort.Text);

            m_socClient.Send(byDataWithDate, byDataWithDate.Length, SocketFlags.None);
            System.Threading.Thread.Sleep(200);

            netIPClosePort();

            textMsgBox.Text += "Update DATE : " + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "\r\n";

        }

        private void btnDelLog_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure delete the LOG file ?", "Mipy ASK", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (File.Exists(logFileName) == true)
                {
                    File.Delete(logFileName);
                    MessageBox.Show("Log File Deleted");

                } // logFileName
                else
                {
                    MessageBox.Show("Delete Log File Failed");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }
    }
}
