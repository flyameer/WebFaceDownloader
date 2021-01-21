namespace WebFaceDownloader
{
    partial class WebFaceDownForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebFaceDownForm));
            this.label1 = new System.Windows.Forms.Label();
            this.strWebURL = new System.Windows.Forms.TextBox();
            this.btnCreateFaceArrayPicture = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TextSrcPicWidth = new System.Windows.Forms.TextBox();
            this.TextSrcPicHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textTotalFaceNumber = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textFaceArrayNumVert = new System.Windows.Forms.TextBox();
            this.textFaceReSizeHeight = new System.Windows.Forms.TextBox();
            this.textCropFaceHeight = new System.Windows.Forms.TextBox();
            this.textFaceArrayNumHeri = new System.Windows.Forms.TextBox();
            this.textFaceResizeWidth = new System.Windows.Forms.TextBox();
            this.textCropFaceWidth = new System.Windows.Forms.TextBox();
            this.textCropFaceStartY = new System.Windows.Forms.TextBox();
            this.textCropFaceStartX = new System.Windows.Forms.TextBox();
            this.textTargetMergedFile = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBatchCount = new System.Windows.Forms.TextBox();
            this.comboFileExt = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.strWebPort = new System.Windows.Forms.TextBox();
            this.textMsgBox = new System.Windows.Forms.TextBox();
            this.btnGetFBFaces = new System.Windows.Forms.Button();
            this.bCapAndSave = new System.Windows.Forms.CheckBox();
            this.textBoxSavingPatch = new System.Windows.Forms.TextBox();
            this.btnUpdateDate = new System.Windows.Forms.Button();
            this.cbClearTheTempFiles = new System.Windows.Forms.CheckBox();
            this.cbDumpLog = new System.Windows.Forms.CheckBox();
            this.btnDelLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(23, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target IP Address :";
            // 
            // strWebURL
            // 
            this.strWebURL.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.strWebURL.Location = new System.Drawing.Point(180, 76);
            this.strWebURL.Name = "strWebURL";
            this.strWebURL.Size = new System.Drawing.Size(133, 27);
            this.strWebURL.TabIndex = 1;
            this.strWebURL.Text = "192.168.0.2";
            // 
            // btnCreateFaceArrayPicture
            // 
            this.btnCreateFaceArrayPicture.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCreateFaceArrayPicture.Location = new System.Drawing.Point(629, 296);
            this.btnCreateFaceArrayPicture.Name = "btnCreateFaceArrayPicture";
            this.btnCreateFaceArrayPicture.Size = new System.Drawing.Size(253, 54);
            this.btnCreateFaceArrayPicture.TabIndex = 2;
            this.btnCreateFaceArrayPicture.Text = "Create Face Array Picture !";
            this.btnCreateFaceArrayPicture.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(622, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Source Picture Size ( w x h ) = ";
            // 
            // TextSrcPicWidth
            // 
            this.TextSrcPicWidth.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TextSrcPicWidth.Location = new System.Drawing.Point(854, 56);
            this.TextSrcPicWidth.Name = "TextSrcPicWidth";
            this.TextSrcPicWidth.ReadOnly = true;
            this.TextSrcPicWidth.Size = new System.Drawing.Size(58, 27);
            this.TextSrcPicWidth.TabIndex = 3;
            this.TextSrcPicWidth.Text = "1024";
            this.TextSrcPicWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextSrcPicHeight
            // 
            this.TextSrcPicHeight.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TextSrcPicHeight.Location = new System.Drawing.Point(937, 56);
            this.TextSrcPicHeight.Name = "TextSrcPicHeight";
            this.TextSrcPicHeight.ReadOnly = true;
            this.TextSrcPicHeight.Size = new System.Drawing.Size(58, 27);
            this.TextSrcPicHeight.TabIndex = 3;
            this.TextSrcPicHeight.Text = "1024";
            this.TextSrcPicHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(918, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(622, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Face Crop  ( x, y , w , h ) = ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(888, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = ",";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(971, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = ",";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(1055, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = ",";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClose.Location = new System.Drawing.Point(368, 389);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(173, 55);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(622, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 19);
            this.label8.TabIndex = 0;
            this.label8.Text = "Face ReSize (  w, h ) = ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(863, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = ",";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(622, 176);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(177, 19);
            this.label10.TabIndex = 0;
            this.label10.Text = "Face Array (  nh x nv ) = ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(877, 186);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = ",";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(971, 178);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 19);
            this.label12.TabIndex = 0;
            this.label12.Text = ", Total Face Pictures = ";
            // 
            // textTotalFaceNumber
            // 
            this.textTotalFaceNumber.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textTotalFaceNumber.Location = new System.Drawing.Point(1148, 174);
            this.textTotalFaceNumber.Name = "textTotalFaceNumber";
            this.textTotalFaceNumber.ReadOnly = true;
            this.textTotalFaceNumber.Size = new System.Drawing.Size(58, 27);
            this.textTotalFaceNumber.TabIndex = 3;
            this.textTotalFaceNumber.Text = "54";
            this.textTotalFaceNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(622, 233);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(157, 19);
            this.label13.TabIndex = 0;
            this.label13.Text = "Target Merged File  : ";
            // 
            // textFaceArrayNumVert
            // 
            this.textFaceArrayNumVert.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebFaceDownloader.Properties.Settings.Default, "textUsrFaceArrayNumVert", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textFaceArrayNumVert.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textFaceArrayNumVert.Location = new System.Drawing.Point(897, 174);
            this.textFaceArrayNumVert.Name = "textFaceArrayNumVert";
            this.textFaceArrayNumVert.Size = new System.Drawing.Size(58, 27);
            this.textFaceArrayNumVert.TabIndex = 3;
            this.textFaceArrayNumVert.Text = global::WebFaceDownloader.Properties.Settings.Default.textUsrFaceArrayNumVert;
            this.textFaceArrayNumVert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textFaceArrayNumVert.TextChanged += new System.EventHandler(this.textFaceArrayNumVert_TextChanged);
            // 
            // textFaceReSizeHeight
            // 
            this.textFaceReSizeHeight.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebFaceDownloader.Properties.Settings.Default, "textUsrFaceReSizeHeight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textFaceReSizeHeight.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textFaceReSizeHeight.Location = new System.Drawing.Point(883, 136);
            this.textFaceReSizeHeight.Name = "textFaceReSizeHeight";
            this.textFaceReSizeHeight.Size = new System.Drawing.Size(58, 27);
            this.textFaceReSizeHeight.TabIndex = 3;
            this.textFaceReSizeHeight.Text = global::WebFaceDownloader.Properties.Settings.Default.textUsrFaceReSizeHeight;
            this.textFaceReSizeHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textCropFaceHeight
            // 
            this.textCropFaceHeight.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebFaceDownloader.Properties.Settings.Default, "textUsrCropFaceHeight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textCropFaceHeight.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textCropFaceHeight.Location = new System.Drawing.Point(1075, 97);
            this.textCropFaceHeight.Name = "textCropFaceHeight";
            this.textCropFaceHeight.Size = new System.Drawing.Size(58, 27);
            this.textCropFaceHeight.TabIndex = 3;
            this.textCropFaceHeight.Text = global::WebFaceDownloader.Properties.Settings.Default.textUsrCropFaceHeight;
            this.textCropFaceHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textFaceArrayNumHeri
            // 
            this.textFaceArrayNumHeri.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebFaceDownloader.Properties.Settings.Default, "textUsrFaceArrayNumHeri", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textFaceArrayNumHeri.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textFaceArrayNumHeri.Location = new System.Drawing.Point(813, 174);
            this.textFaceArrayNumHeri.Name = "textFaceArrayNumHeri";
            this.textFaceArrayNumHeri.Size = new System.Drawing.Size(58, 27);
            this.textFaceArrayNumHeri.TabIndex = 3;
            this.textFaceArrayNumHeri.Text = global::WebFaceDownloader.Properties.Settings.Default.textUsrFaceArrayNumHeri;
            this.textFaceArrayNumHeri.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textFaceArrayNumHeri.TextChanged += new System.EventHandler(this.textFaceArrayNumHeri_TextChanged);
            // 
            // textFaceResizeWidth
            // 
            this.textFaceResizeWidth.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebFaceDownloader.Properties.Settings.Default, "textUsrFaceResizeWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textFaceResizeWidth.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textFaceResizeWidth.Location = new System.Drawing.Point(799, 136);
            this.textFaceResizeWidth.Name = "textFaceResizeWidth";
            this.textFaceResizeWidth.Size = new System.Drawing.Size(58, 27);
            this.textFaceResizeWidth.TabIndex = 3;
            this.textFaceResizeWidth.Text = global::WebFaceDownloader.Properties.Settings.Default.textUsrFaceResizeWidth;
            this.textFaceResizeWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textCropFaceWidth
            // 
            this.textCropFaceWidth.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebFaceDownloader.Properties.Settings.Default, "textUsrCropFaceWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textCropFaceWidth.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textCropFaceWidth.Location = new System.Drawing.Point(991, 97);
            this.textCropFaceWidth.Name = "textCropFaceWidth";
            this.textCropFaceWidth.Size = new System.Drawing.Size(58, 27);
            this.textCropFaceWidth.TabIndex = 3;
            this.textCropFaceWidth.Text = global::WebFaceDownloader.Properties.Settings.Default.textUsrCropFaceWidth;
            this.textCropFaceWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textCropFaceStartY
            // 
            this.textCropFaceStartY.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebFaceDownloader.Properties.Settings.Default, "textUsrCropFaceStartY", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textCropFaceStartY.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textCropFaceStartY.Location = new System.Drawing.Point(907, 97);
            this.textCropFaceStartY.Name = "textCropFaceStartY";
            this.textCropFaceStartY.Size = new System.Drawing.Size(58, 27);
            this.textCropFaceStartY.TabIndex = 3;
            this.textCropFaceStartY.Text = global::WebFaceDownloader.Properties.Settings.Default.textUsrCropFaceStartY;
            this.textCropFaceStartY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textCropFaceStartX
            // 
            this.textCropFaceStartX.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebFaceDownloader.Properties.Settings.Default, "textUsrCropFaceStartX", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textCropFaceStartX.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textCropFaceStartX.Location = new System.Drawing.Point(824, 97);
            this.textCropFaceStartX.Name = "textCropFaceStartX";
            this.textCropFaceStartX.Size = new System.Drawing.Size(58, 27);
            this.textCropFaceStartX.TabIndex = 3;
            this.textCropFaceStartX.Text = global::WebFaceDownloader.Properties.Settings.Default.textUsrCropFaceStartX;
            this.textCropFaceStartX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textTargetMergedFile
            // 
            this.textTargetMergedFile.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WebFaceDownloader.Properties.Settings.Default, "strUsrTargetFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textTargetMergedFile.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textTargetMergedFile.Location = new System.Drawing.Point(643, 263);
            this.textTargetMergedFile.Name = "textTargetMergedFile";
            this.textTargetMergedFile.Size = new System.Drawing.Size(580, 27);
            this.textTargetMergedFile.TabIndex = 1;
            this.textTargetMergedFile.Text = global::WebFaceDownloader.Properties.Settings.Default.strUsrTargetFile;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(622, 201);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 19);
            this.label14.TabIndex = 0;
            this.label14.Text = "Batch Count :";
            // 
            // textBatchCount
            // 
            this.textBatchCount.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBatchCount.Location = new System.Drawing.Point(731, 198);
            this.textBatchCount.Name = "textBatchCount";
            this.textBatchCount.Size = new System.Drawing.Size(54, 27);
            this.textBatchCount.TabIndex = 4;
            this.textBatchCount.Text = "1";
            this.textBatchCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboFileExt
            // 
            this.comboFileExt.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboFileExt.FormattingEnabled = true;
            this.comboFileExt.Items.AddRange(new object[] {
            "PNG",
            "JPG",
            "BMP"});
            this.comboFileExt.Location = new System.Drawing.Point(799, 230);
            this.comboFileExt.Name = "comboFileExt";
            this.comboFileExt.Size = new System.Drawing.Size(83, 27);
            this.comboFileExt.TabIndex = 5;
            this.comboFileExt.Text = "PNG";
            this.comboFileExt.SelectedIndexChanged += new System.EventHandler(this.comboFileExt_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConnect.Location = new System.Drawing.Point(14, 14);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(186, 44);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect Device ...";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(327, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 19);
            this.label15.TabIndex = 0;
            this.label15.Text = "Port :";
            // 
            // strWebPort
            // 
            this.strWebPort.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.strWebPort.Location = new System.Drawing.Point(378, 76);
            this.strWebPort.Name = "strWebPort";
            this.strWebPort.Size = new System.Drawing.Size(45, 27);
            this.strWebPort.TabIndex = 1;
            this.strWebPort.Text = "80";
            // 
            // textMsgBox
            // 
            this.textMsgBox.Location = new System.Drawing.Point(14, 240);
            this.textMsgBox.Multiline = true;
            this.textMsgBox.Name = "textMsgBox";
            this.textMsgBox.ReadOnly = true;
            this.textMsgBox.Size = new System.Drawing.Size(581, 144);
            this.textMsgBox.TabIndex = 7;
            // 
            // btnGetFBFaces
            // 
            this.btnGetFBFaces.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGetFBFaces.Location = new System.Drawing.Point(85, 390);
            this.btnGetFBFaces.Name = "btnGetFBFaces";
            this.btnGetFBFaces.Size = new System.Drawing.Size(186, 55);
            this.btnGetFBFaces.TabIndex = 8;
            this.btnGetFBFaces.Text = "脸书撷图";
            this.btnGetFBFaces.UseVisualStyleBackColor = true;
            this.btnGetFBFaces.Click += new System.EventHandler(this.btnGetFBFaces_Click);
            // 
            // bCapAndSave
            // 
            this.bCapAndSave.AutoSize = true;
            this.bCapAndSave.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bCapAndSave.Location = new System.Drawing.Point(27, 173);
            this.bCapAndSave.Name = "bCapAndSave";
            this.bCapAndSave.Size = new System.Drawing.Size(121, 23);
            this.bCapAndSave.TabIndex = 13;
            this.bCapAndSave.Text = "抓图存档到：";
            this.bCapAndSave.UseVisualStyleBackColor = true;
            // 
            // textBoxSavingPatch
            // 
            this.textBoxSavingPatch.Location = new System.Drawing.Point(50, 203);
            this.textBoxSavingPatch.Name = "textBoxSavingPatch";
            this.textBoxSavingPatch.Size = new System.Drawing.Size(545, 25);
            this.textBoxSavingPatch.TabIndex = 14;
            // 
            // btnUpdateDate
            // 
            this.btnUpdateDate.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnUpdateDate.Location = new System.Drawing.Point(42, 124);
            this.btnUpdateDate.Name = "btnUpdateDate";
            this.btnUpdateDate.Size = new System.Drawing.Size(121, 32);
            this.btnUpdateDate.TabIndex = 15;
            this.btnUpdateDate.Text = "Update Date";
            this.btnUpdateDate.UseVisualStyleBackColor = true;
            this.btnUpdateDate.Click += new System.EventHandler(this.btnUpdateDate_Click);
            // 
            // cbClearTheTempFiles
            // 
            this.cbClearTheTempFiles.AutoSize = true;
            this.cbClearTheTempFiles.Checked = true;
            this.cbClearTheTempFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbClearTheTempFiles.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbClearTheTempFiles.Location = new System.Drawing.Point(225, 173);
            this.cbClearTheTempFiles.Name = "cbClearTheTempFiles";
            this.cbClearTheTempFiles.Size = new System.Drawing.Size(165, 23);
            this.cbClearTheTempFiles.TabIndex = 16;
            this.cbClearTheTempFiles.Text = "Clear the temp files";
            this.cbClearTheTempFiles.UseVisualStyleBackColor = true;
            // 
            // cbDumpLog
            // 
            this.cbDumpLog.AutoSize = true;
            this.cbDumpLog.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbDumpLog.Location = new System.Drawing.Point(404, 173);
            this.cbDumpLog.Name = "cbDumpLog";
            this.cbDumpLog.Size = new System.Drawing.Size(109, 23);
            this.cbDumpLog.TabIndex = 17;
            this.cbDumpLog.Text = "Dump LOG";
            this.cbDumpLog.UseVisualStyleBackColor = true;
            // 
            // btnDelLog
            // 
            this.btnDelLog.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDelLog.Location = new System.Drawing.Point(519, 173);
            this.btnDelLog.Name = "btnDelLog";
            this.btnDelLog.Size = new System.Drawing.Size(76, 24);
            this.btnDelLog.TabIndex = 18;
            this.btnDelLog.Text = "Del LOG";
            this.btnDelLog.UseVisualStyleBackColor = true;
            this.btnDelLog.Click += new System.EventHandler(this.btnDelLog_Click);
            // 
            // WebFaceDownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 456);
            this.Controls.Add(this.btnDelLog);
            this.Controls.Add(this.cbDumpLog);
            this.Controls.Add(this.cbClearTheTempFiles);
            this.Controls.Add(this.btnUpdateDate);
            this.Controls.Add(this.textBoxSavingPatch);
            this.Controls.Add(this.bCapAndSave);
            this.Controls.Add(this.btnGetFBFaces);
            this.Controls.Add(this.textMsgBox);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.comboFileExt);
            this.Controls.Add(this.textBatchCount);
            this.Controls.Add(this.textTotalFaceNumber);
            this.Controls.Add(this.textFaceArrayNumVert);
            this.Controls.Add(this.textFaceReSizeHeight);
            this.Controls.Add(this.textCropFaceHeight);
            this.Controls.Add(this.textFaceArrayNumHeri);
            this.Controls.Add(this.textFaceResizeWidth);
            this.Controls.Add(this.textCropFaceWidth);
            this.Controls.Add(this.textCropFaceStartY);
            this.Controls.Add(this.TextSrcPicHeight);
            this.Controls.Add(this.textCropFaceStartX);
            this.Controls.Add(this.TextSrcPicWidth);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCreateFaceArrayPicture);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textTargetMergedFile);
            this.Controls.Add(this.strWebPort);
            this.Controls.Add(this.strWebURL);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebFaceDownForm";
            this.Text = "MipyWebFace Downloader v1.8";
            this.Load += new System.EventHandler(this.WebFaceDownForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox strWebURL;
        private System.Windows.Forms.Button btnCreateFaceArrayPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextSrcPicWidth;
        private System.Windows.Forms.TextBox TextSrcPicHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textCropFaceStartX;
        private System.Windows.Forms.TextBox textCropFaceStartY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textCropFaceWidth;
        private System.Windows.Forms.TextBox textCropFaceHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textFaceResizeWidth;
        private System.Windows.Forms.TextBox textFaceReSizeHeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textFaceArrayNumHeri;
        private System.Windows.Forms.TextBox textFaceArrayNumVert;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textTotalFaceNumber;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textTargetMergedFile;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBatchCount;
        private System.Windows.Forms.ComboBox comboFileExt;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox strWebPort;
        private System.Windows.Forms.TextBox textMsgBox;
        private System.Windows.Forms.Button btnGetFBFaces;
        private System.Windows.Forms.CheckBox bCapAndSave;
        private System.Windows.Forms.TextBox textBoxSavingPatch;
        private System.Windows.Forms.Button btnUpdateDate;
        private System.Windows.Forms.CheckBox cbClearTheTempFiles;
        private System.Windows.Forms.CheckBox cbDumpLog;
        private System.Windows.Forms.Button btnDelLog;
    }
}

