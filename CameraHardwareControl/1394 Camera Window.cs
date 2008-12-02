using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using NationalInstruments;
using NationalInstruments.CWIMAQControls;
using System.IO;
using System.Xml.Serialization;


namespace CameraHardwareControl
{
	/// <summary>
	/// Snap using NI-IMAQ for IEEE 1394
	/// </summary>
	public class ControlWindow : System.Windows.Forms.Form
	{
        public Controller controller;

        private NationalInstruments.CWIMAQControls.AxCWIMAQViewer axCWIMAQViewer1;
		public System.Windows.Forms.TextBox InterfaceName;
		public System.Windows.Forms.Button StopButton;
		public System.Windows.Forms.Button SnapButton;
		public System.Windows.Forms.Button QuitButton;
		public System.Windows.Forms.Button GrabButton;
		private System.ComponentModel.IContainer components;
		public System.Windows.Forms.Label Label;
		public System.Windows.Forms.Timer Timer1;
		public NationalInstruments.CWIMAQControls.CWIMAQImageClass currentImage;
        public NationalInstruments.CWIMAQControls.CWIMAQImageClass tempImage;
     


        //This group of fields are all GUI independent.
        public Object[] imageArray;
        public Object[] tempImageArray;
        public ArrayList currentSequence = new ArrayList();
        public Object[] imageData;
        public ArrayList imageSequences = new ArrayList();
        public int scanCount = 0;
        public int frameCount = 0;
        public ArrayList tempScanParameters = new ArrayList();
        public ArrayList latestScanParameters = new ArrayList();
        public Object[] tempBuffer;
    

		public uint sid;	
		public int bufferNumber;
		public int errorCode;
        private HScrollBar ImageList;
        private Button AquireSequenceButton;


        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveImageSequenceToolStripMenuItem;
        private Label label2;
        private ToolStripMenuItem saveLatestImageSequenceToolStripMenuItem;
        private ToolStripMenuItem saveAllScanSequenceImagesToolStripMenuItem;
        private Button button2;
		public string errorMessage;



		public ControlWindow()
		{
				
			InitializeComponent();
				}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlWindow));
            this.axCWIMAQViewer1 = new NationalInstruments.CWIMAQControls.AxCWIMAQViewer();
            this.InterfaceName = new System.Windows.Forms.TextBox();
            this.Label = new System.Windows.Forms.Label();
            this.SnapButton = new System.Windows.Forms.Button();
            this.QuitButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.GrabButton = new System.Windows.Forms.Button();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.ImageList = new System.Windows.Forms.HScrollBar();
            this.AquireSequenceButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLatestImageSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllScanSequenceImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axCWIMAQViewer1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axCWIMAQViewer1
            // 
            this.axCWIMAQViewer1.Enabled = true;
            this.axCWIMAQViewer1.Location = new System.Drawing.Point(10, 30);
            this.axCWIMAQViewer1.Name = "axCWIMAQViewer1";
            this.axCWIMAQViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCWIMAQViewer1.OcxState")));
            this.axCWIMAQViewer1.Size = new System.Drawing.Size(848, 671);
            this.axCWIMAQViewer1.TabIndex = 0;
            this.axCWIMAQViewer1.ImageMouseMove += new NationalInstruments.CWIMAQControls._AxDCWIMAQViewerEvents_ImageMouseMoveEventHandler(this.axCWIMAQViewer1_ImageMouseMove);
            // 
            // InterfaceName
            // 
            this.InterfaceName.AcceptsReturn = true;
            this.InterfaceName.BackColor = System.Drawing.SystemColors.Window;
            this.InterfaceName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.InterfaceName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InterfaceName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.InterfaceName.Location = new System.Drawing.Point(10, 740);
            this.InterfaceName.MaxLength = 0;
            this.InterfaceName.Name = "InterfaceName";
            this.InterfaceName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.InterfaceName.Size = new System.Drawing.Size(117, 23);
            this.InterfaceName.TabIndex = 3;
            this.InterfaceName.Text = "cam0";
            this.InterfaceName.TextChanged += new System.EventHandler(this.InterfaceName_TextChanged);
            // 
            // Label
            // 
            this.Label.BackColor = System.Drawing.SystemColors.Control;
            this.Label.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label.Location = new System.Drawing.Point(12, 715);
            this.Label.Name = "Label";
            this.Label.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label.Size = new System.Drawing.Size(97, 20);
            this.Label.TabIndex = 5;
            this.Label.Text = "Camera Name";
            this.Label.Click += new System.EventHandler(this.Label_Click);
            // 
            // SnapButton
            // 
            this.SnapButton.BackColor = System.Drawing.SystemColors.Control;
            this.SnapButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.SnapButton.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SnapButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SnapButton.Location = new System.Drawing.Point(172, 718);
            this.SnapButton.Name = "SnapButton";
            this.SnapButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SnapButton.Size = new System.Drawing.Size(54, 38);
            this.SnapButton.TabIndex = 6;
            this.SnapButton.Text = "Snap";
            this.SnapButton.UseVisualStyleBackColor = false;
            this.SnapButton.Click += new System.EventHandler(this.SnapButton_Click);
            // 
            // QuitButton
            // 
            this.QuitButton.BackColor = System.Drawing.SystemColors.Control;
            this.QuitButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.QuitButton.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuitButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.QuitButton.Location = new System.Drawing.Point(763, 721);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.QuitButton.Size = new System.Drawing.Size(60, 39);
            this.QuitButton.TabIndex = 7;
            this.QuitButton.Text = "Quit";
            this.QuitButton.UseVisualStyleBackColor = false;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.BackColor = System.Drawing.SystemColors.Control;
            this.StopButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.StopButton.Enabled = false;
            this.StopButton.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StopButton.Location = new System.Drawing.Point(683, 721);
            this.StopButton.Name = "StopButton";
            this.StopButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StopButton.Size = new System.Drawing.Size(50, 39);
            this.StopButton.TabIndex = 9;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // GrabButton
            // 
            this.GrabButton.BackColor = System.Drawing.SystemColors.Control;
            this.GrabButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.GrabButton.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrabButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GrabButton.Location = new System.Drawing.Point(620, 721);
            this.GrabButton.Name = "GrabButton";
            this.GrabButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GrabButton.Size = new System.Drawing.Size(56, 39);
            this.GrabButton.TabIndex = 10;
            this.GrabButton.Text = "Grab";
            this.GrabButton.UseVisualStyleBackColor = false;
            this.GrabButton.Click += new System.EventHandler(this.GrabButton_Click);
            // 
            // Timer1
            // 
            this.Timer1.Interval = 5;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // ImageList
            // 
            this.ImageList.LargeChange = 1;
            this.ImageList.Location = new System.Drawing.Point(416, 766);
            this.ImageList.Maximum = 5;
            this.ImageList.Name = "ImageList";
            this.ImageList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ImageList.Size = new System.Drawing.Size(96, 19);
            this.ImageList.TabIndex = 8;
            this.ImageList.TabStop = true;
            this.ImageList.Scroll += new System.Windows.Forms.ScrollEventHandler(this.imageList_Scroll);
            // 
            // AquireSequenceButton
            // 
            this.AquireSequenceButton.Location = new System.Drawing.Point(272, 718);
            this.AquireSequenceButton.Name = "AquireSequenceButton";
            this.AquireSequenceButton.Size = new System.Drawing.Size(122, 31);
            this.AquireSequenceButton.TabIndex = 11;
            this.AquireSequenceButton.Text = "Start Sequence";
            this.AquireSequenceButton.UseVisualStyleBackColor = true;
            this.AquireSequenceButton.Click += new System.EventHandler(this.AquireSequenceButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(516, 770);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Scroll Frames";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(874, 26);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageSequenceToolStripMenuItem,
            this.saveLatestImageSequenceToolStripMenuItem,
            this.saveAllScanSequenceImagesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveImageSequenceToolStripMenuItem
            // 
            this.saveImageSequenceToolStripMenuItem.Name = "saveImageSequenceToolStripMenuItem";
            this.saveImageSequenceToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.saveImageSequenceToolStripMenuItem.Text = "Save Snap Image";
            this.saveImageSequenceToolStripMenuItem.Click += new System.EventHandler(this.saveImageSequenceToolStripMenuItem_Click);
            // 
            // saveLatestImageSequenceToolStripMenuItem
            // 
            this.saveLatestImageSequenceToolStripMenuItem.Name = "saveLatestImageSequenceToolStripMenuItem";
            this.saveLatestImageSequenceToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.saveLatestImageSequenceToolStripMenuItem.Text = "Save Latest Image Sequence";
            this.saveLatestImageSequenceToolStripMenuItem.Click += new System.EventHandler(this.saveLatestImageSequenceToolStripMenuItem_Click);
            // 
            // saveAllScanSequenceImagesToolStripMenuItem
            // 
            this.saveAllScanSequenceImagesToolStripMenuItem.Name = "saveAllScanSequenceImagesToolStripMenuItem";
            this.saveAllScanSequenceImagesToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.saveAllScanSequenceImagesToolStripMenuItem.Text = "Save All Scan Sequence Images";
            this.saveAllScanSequenceImagesToolStripMenuItem.Click += new System.EventHandler(this.saveAllScanSequenceImagesToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(413, 718);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 17;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(272, 756);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 29);
            this.button2.TabIndex = 19;
            this.button2.Text = "Stop Sequence";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // ControlWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(874, 806);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AquireSequenceButton);
            this.Controls.Add(this.ImageList);
            this.Controls.Add(this.GrabButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.SnapButton);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.InterfaceName);
            this.Controls.Add(this.axCWIMAQViewer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ControlWindow";
            this.Text = "Snap 1394";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axCWIMAQViewer1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{

        //    Application.Run(new ControlWindow());
        //}


		private void DisplayError(int x, out string y)
		{
			CWIMAQ1394.ShowErrorCW(x, out y);
			MessageBox.Show(y);
		}

        //Establishes a link to the camera based an intialises an array to collect N images
        public void OpenCameraLink(int numFrames) 
        {

           tempImage = new NationalInstruments.CWIMAQControls.CWIMAQImageClass();

           tempImageArray = new Object[numFrames];

            errorCode = CWIMAQ1394.CameraOpen2(InterfaceName.Text, CWIMAQ1394.CameraMode.IMG1394_CAMERA_MODE_CONTROLLER, out sid);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD)
                DisplayError(errorCode, out errorMessage);

            errorCode = CWIMAQ1394.TriggerConfigure(sid, CWIMAQ1394.TriggerPolarity.IMG1394_TRIG_POLAR_ACTIVEL, 60000, CWIMAQ1394.TriggerMode.IMG1394_TRIG_MODE1, 1);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) // camera trigger polarity needs to be falling edge - see page 153 of Marlin  manual. Also for explanation of trigger mode 1
                DisplayError(errorCode, out errorMessage);

           // errorCode = CWIMAQ1394.SetAttribute(sid, CWIMAQ1394.Attribute.IMG1394_ATTR_VIDEO_MODE, 5);
           // if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD)//colour processing is bypassed, giving out just raw binary data
            //    DisplayError(errorCode, out errorMessage);

            errorCode = CWIMAQ1394.SetAttribute(sid, CWIMAQ1394.Attribute.IMG1394_ATTR_TRIGGER_DELAY, 0);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) // sets internal delay between incoming external trigger and start of frame exposure to 0
                DisplayError(errorCode, out errorMessage);
           
            errorCode = CWIMAQ1394.SetAttribute(sid, CWIMAQ1394.Attribute.IMG1394_ATTR_AUTO_EXPOSURE, 0);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) // turn off auto exposure
                DisplayError(errorCode, out errorMessage);

            errorCode = CWIMAQ1394.SetAttribute(sid, CWIMAQ1394.Attribute.IMG1394_ATTR_SHUTTER, 1);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) // we set the camera shutter attribute to 1 in order to minimise the exposure time.
                DisplayError(errorCode, out errorMessage);


        }


        //This method is called by ArmAndWait() in the plugin and takes a single image when hardware triggered, displays it in the
        //GUI and adds it to the temporary image array. It  also keeps track of the number of frames taken.


        public void AquireSingleImage(double scanParameter)
        {
            tempScanParameters.Add(scanParameter);

            label2.Text = "Aquiring Scan " +scanCount.ToString()+ " Image "+frameCount.ToString();
            label2.Update();

            tempBuffer = new Object[1];
            tempBuffer[0] = new NationalInstruments.CWIMAQControls.CWIMAQImage();

             axCWIMAQViewer1.Attach((NationalInstruments.CWIMAQControls.CWIMAQImage)tempBuffer[0]);


            errorCode = CWIMAQ1394.SetupSequenceCW(sid, ref tempBuffer, 1, 0);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD)
                DisplayError(errorCode, out errorMessage);

            tempImageArray[frameCount] = tempBuffer[0];

            frameCount++;

            currentSequence.Add(tempImage);
        }


        //Called at the end of a scan, this method transfers the images to imageArray and displays them on 
        //a scroll bar
        internal void DumpAndDisplay()
        {
            imageArray = new Object[frameCount];

            for (int i = 0; i <= frameCount - 1; i++)
            {
                imageArray[i] = new NationalInstruments.CWIMAQControls.CWIMAQImage();
            }

            for (int i = 0; i <= frameCount - 1; i++)
            {
                imageArray[i] = tempImageArray[i];
            }

            imageSequences.Add(currentSequence);

            currentSequence.Clear();

            latestScanParameters.Clear();

            foreach (double d in tempScanParameters)
            {
                latestScanParameters.Add(d);
            }

            tempScanParameters.Clear();

            label2.Text = "Sequence Complete " +imageArray.Length.ToString() + " images";
            label2.Update();

            ImageList.Value = ImageList.Minimum;
            ImageList.Maximum = imageArray.Length - 1;

            ImageList_Change(0);

            scanCount++;

            frameCount = 0;


         CWIMAQ1394.Close(sid);

        }


        //Allows the camera link to be closed when scan master finishes.
        public void CloseCameraLink()
        {
            CWIMAQ1394.Close(sid);

            label2.Text = "Aquisition Stopped";
            label2.Update();
        }


        //A method to clear the collections and reset counters at the start of aquisition.
        public void clearSequences()
        {
            imageSequences.Clear();

            tempScanParameters.Clear();

            scanCount = 0;

            frameCount = 0;

        }


        //Acquires a sequence of N frames , given N triggers
        //This method is never implemented but illustrates how to take a sequence of images.
        public void aquireSequence(int numFrames)
        {

            label2.Text = "Aquiring Sequence";
            label2.Update();


            imageArray = new Object[numFrames];

            for (int i = 0; i <= numFrames-1; i++)
            {
               imageArray[i] = new NationalInstruments.CWIMAQControls.CWIMAQImage();
            }

            ImageList.Value = ImageList.Minimum;
            ImageList.Maximum = numFrames - 1;


            errorCode = CWIMAQ1394.CameraOpen2(InterfaceName.Text, CWIMAQ1394.CameraMode.IMG1394_CAMERA_MODE_CONTROLLER, out sid);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD)
                DisplayError(errorCode, out errorMessage);

            errorCode = CWIMAQ1394.TriggerConfigure(sid, CWIMAQ1394.TriggerPolarity.IMG1394_TRIG_POLAR_ACTIVEL, 60000, CWIMAQ1394.TriggerMode.IMG1394_TRIG_MODE1, 1);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD)
                DisplayError(errorCode, out errorMessage);

            errorCode = CWIMAQ1394.SetupSequenceCW(sid, ref imageArray, numFrames, 0);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD)
                DisplayError(errorCode, out errorMessage);

           
        }
        

        //Take a single image with a press of a button on the GUI
		private void SnapButton_Click(object sender, System.EventArgs e)
		{

            ImageList.Enabled = false;

			currentImage = new NationalInstruments.CWIMAQControls.CWIMAQImageClass();
            axCWIMAQViewer1.Attach(currentImage);

			errorCode = CWIMAQ1394.CameraOpen2(InterfaceName.Text, CWIMAQ1394.CameraMode.IMG1394_CAMERA_MODE_CONTROLLER, out sid);
			if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) 
				DisplayError(errorCode, out errorMessage);


            errorCode = CWIMAQ1394.SnapCW(sid, currentImage);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD)
                DisplayError(errorCode, out errorMessage);

         

             CWIMAQ1394.Close(sid);

		}

        //Attaches the appropriate image when the scoll br is updated
        private void ImageList_Change(int newScrollValue)
        {
            axCWIMAQViewer1.Attach((NationalInstruments.CWIMAQControls.CWIMAQImageClass)imageArray[newScrollValue]);
        }

        
        //Quit
		private void QuitButton_Click(object sender, System.EventArgs e)
		{
			if (Timer1.Enabled)
				StopButton_Click(StopButton, new System.EventArgs());

			Application.Exit();
		}

        //Asynchronous video mode
		private void GrabButton_Click(object sender, System.EventArgs e)
		{
			currentImage = new NationalInstruments.CWIMAQControls.CWIMAQImageClass();			
			axCWIMAQViewer1.Attach(currentImage);


            errorCode = CWIMAQ1394.CameraOpen2(InterfaceName.Text, CWIMAQ1394.CameraMode.IMG1394_CAMERA_MODE_CONTROLLER, out sid);
			if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) 
				DisplayError(errorCode, out errorMessage);
						
			errorCode = CWIMAQ1394.SetupGrabCW(sid);
			if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) 
				DisplayError(errorCode, out errorMessage);

			
			Timer1.Enabled = true;
        	GrabButton.Enabled = false;
			SnapButton.Enabled = false;
            StopButton.Enabled = true;
		}

        private void grabTriggeredImages(){

            currentImage = new NationalInstruments.CWIMAQControls.CWIMAQImageClass();

          

			errorCode = CWIMAQ1394.CameraOpen2(InterfaceName.Text, CWIMAQ1394.CameraMode.IMG1394_CAMERA_MODE_CONTROLLER, out sid);
			if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) 
				DisplayError(errorCode, out errorMessage);

            errorCode = CWIMAQ1394.TriggerConfigure(sid, CWIMAQ1394.TriggerPolarity.IMG1394_TRIG_POLAR_ACTIVEL, 60000, CWIMAQ1394.TriggerMode.IMG1394_TRIG_MODE1, 1);
            if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD)
                DisplayError(errorCode, out errorMessage);
						
			errorCode = CWIMAQ1394.SetupGrabCW(sid);
			if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) 
				DisplayError(errorCode, out errorMessage);

            imageSequences.Add(currentImage);
			
			Timer1.Enabled = true;
        	GrabButton.Enabled = false;
			SnapButton.Enabled = false;
            StopButton.Enabled = true;
        }

        public void SaveData()
        {
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save image data";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        SaveData(saveFileDialog1.FileName);
                    }

                }
            }
        }


        public void SaveDataSequence()
        {
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Title = "Save image data";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        Directory.CreateDirectory(saveFileDialog1.FileName);
                        SaveDataSequence(saveFileDialog1.FileName);
                    }

                }
            }
        }


        //Method used to get the directory name from a File Chooser
        public string GetSaveDialogFilename()
        {
            string file = "";
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save image data";
               
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        file = saveFileDialog1.FileName;
                    }
                }
            }
            return file;
        }


       
        //Saves all the scans recorded
        public void SaveScanSequenceImages()
        {
            string filepath = GetSaveDialogFilename();

            string filetext = Path.GetFileName(filepath);

            for(int i = 0; i<imageSequences.Count; i++)

            {
                string scanIndexedFilepath = filepath+"\\scan_" + i.ToString();
                Directory.CreateDirectory(scanIndexedFilepath);
                SaveDataSequence(scanIndexedFilepath);
            }
            
        }

       
        //Saves an indivdual frame
        public void SaveSnapImage()
        {
            string filename = GetSaveDialogFilename();
            SaveData(filename);

        }



        //Writes out a  binary image data to a set of files in a directory determined by the file chooser.
        public void SaveData(string filename)
        {
            string indexedFilename = filename+".dat";

            Byte[,] arrImage = (Byte[,])(currentImage.ImageToArray(0, 0, currentImage.Width, currentImage.Height));
            byte[,] arrayImage = (byte[,])(arrImage);
            byte[] flatImage = new byte[currentImage.Height * currentImage.Width];

            int byteIndex = 0;
            for (int i = 0; i < arrayImage.GetLength(1); i++)
            {
                for (int j = 0; j < arrayImage.GetLength(0); j++)
                {
                    flatImage[byteIndex] = arrayImage[j, i];
                    byteIndex++;
                }
            }
            File.WriteAllBytes(indexedFilename, flatImage);
        
        }

        
        //Writes all the images in the (latest) scan to a binary file and saves an xml copy of the scan parameters.
        public void SaveDataSequence(string filename)
        {
            string fileText = Path.GetFileName(filename);
            string parametersFilename = filename + "//scanparameters.txt";

            for (int k = 0; k <= imageArray.Length - 1; k++)
            {
               
           
                string indexedFilename = filename+"//"+fileText + "_image_" + k.ToString() + ".dat";
               

                currentImage = (NationalInstruments.CWIMAQControls.CWIMAQImageClass)(imageArray[k]);
                Byte[,] arrImage = (Byte[,])(currentImage.ImageToArray(0, 0, currentImage.Width, currentImage.Height));
                byte[,] arrayImage = (byte[,])(arrImage);
                byte[] flatImage = new byte[currentImage.Height * currentImage.Width];

                int byteIndex = 0;
                for (int i = 0; i < arrayImage.GetLength(1); i++)
                {
                    for (int j = 0; j < arrayImage.GetLength(0); j++)
                    {
                        flatImage[byteIndex] = arrayImage[j, i];
                        byteIndex++;
                    }
                }
                File.WriteAllBytes(indexedFilename, flatImage);
       
            }

            
            XmlSerializer s = new XmlSerializer(typeof(ArrayList));
            TextWriter w = new StreamWriter(parametersFilename);
            s.Serialize(w, latestScanParameters);
            w.Close();
         
        }

//Event driven methods 

		private void StopButton_Click(object sender, System.EventArgs e)
		{
			Timer1.Enabled = false;
			GrabButton.Enabled = true;
			SnapButton.Enabled = true;
			StopButton.Enabled = false;

			CWIMAQ1394.Close(sid);
		}

		private void Timer1_Tick(object sender, System.EventArgs e)
		{
			errorCode = CWIMAQ1394.Grab2CW(sid, 1, ref bufferNumber, currentImage);
			if (errorCode != CWIMAQ1394.ErrorCodes.IMG1394_ERR_GOOD) 
			{
				DisplayError(errorCode, out errorMessage);
				StopButton_Click(StopButton, new System.EventArgs());
			}
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void imageList_Scroll(object sender, ScrollEventArgs e)
        {
            ImageList_Change(e.NewValue);
        }

        private void AquireSequenceButton_Click(object sender, EventArgs e)
        {
           
            ImageList.Enabled = true;
            aquireSequence(3);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    indefiniteSynchrnousAquisition();
        //}



        private void saveImageSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
             SaveData();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void saveLatestImageSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveDataSequence();
        }

        private void saveAllScanSequenceImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveScanSequenceImages();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DumpAndDisplay();
        }

        private void Label_Click(object sender, EventArgs e)
        {

        }

        private void InterfaceName_TextChanged(object sender, EventArgs e)
        {

        }

        private void axCWIMAQViewer1_ImageMouseMove(object sender, _DCWIMAQViewerEvents_ImageMouseMoveEvent e)
        {

        }
    }
}
