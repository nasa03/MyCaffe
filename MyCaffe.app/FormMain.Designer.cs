﻿namespace MyCaffe.app
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMNISTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCIFAR10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadVOC2007ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.getSQLExpressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runAutotestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.startAutotestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startWithResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abortAutotestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.createMyCaffeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingLeNETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingSiameseNETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingTripletNETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.destroyMyCaffeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainMNISTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testMNISTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runTestImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTestImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.specialTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alexNetCifarCUDA8BugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alexNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resNet56CifarAccuracyBugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testPythonInteropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.startCartPoleTrainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startAtariTrainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGymUiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.startNeuralStyleTransferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadTestDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDataDownloadTfTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDataDownloadTFTDataOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDataDownloadGPTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.v1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.v2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cultureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enUSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deDEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogAutoTests = new System.Windows.Forms.OpenFileDialog();
            this.m_bwLoadMnistDatabase = new System.ComponentModel.BackgroundWorker();
            this.m_bwProcess = new System.ComponentModel.BackgroundWorker();
            this.m_bwLoadCiFar10Database = new System.ComponentModel.BackgroundWorker();
            this.m_bwInit = new System.ComponentModel.BackgroundWorker();
            this.m_bwUrlCheck = new System.ComponentModel.BackgroundWorker();
            this.lvStatus = new MyCaffe.app.ListViewEx();
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblGpu = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblImgDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCudaPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerUI = new System.Windows.Forms.Timer(this.components);
            this.m_bwLoadVOCDatabase = new System.ComponentModel.BackgroundWorker();
            this.testDataDownloadLNNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.databaseToolStripMenuItem,
            this.testToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.gpuToolStripMenuItem,
            this.imageDBToolStripMenuItem,
            this.cultureToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(957, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDatabaseToolStripMenuItem,
            this.loadMNISTToolStripMenuItem,
            this.loadCIFAR10ToolStripMenuItem,
            this.loadVOC2007ToolStripMenuItem,
            this.toolStripSeparator6,
            this.getSQLExpressToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "&Database";
            // 
            // createDatabaseToolStripMenuItem
            // 
            this.createDatabaseToolStripMenuItem.Name = "createDatabaseToolStripMenuItem";
            this.createDatabaseToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.createDatabaseToolStripMenuItem.Text = "&Create Database";
            this.createDatabaseToolStripMenuItem.Click += new System.EventHandler(this.createDatabaseToolStripMenuItem_Click);
            // 
            // loadMNISTToolStripMenuItem
            // 
            this.loadMNISTToolStripMenuItem.Name = "loadMNISTToolStripMenuItem";
            this.loadMNISTToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.loadMNISTToolStripMenuItem.Text = "Load MNIST...";
            this.loadMNISTToolStripMenuItem.Click += new System.EventHandler(this.loadMNISTToolStripMenuItem_Click);
            // 
            // loadCIFAR10ToolStripMenuItem
            // 
            this.loadCIFAR10ToolStripMenuItem.Name = "loadCIFAR10ToolStripMenuItem";
            this.loadCIFAR10ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.loadCIFAR10ToolStripMenuItem.Text = "Load CIFAR-10...";
            this.loadCIFAR10ToolStripMenuItem.Click += new System.EventHandler(this.loadCIFAR10ToolStripMenuItem_Click);
            // 
            // loadVOC2007ToolStripMenuItem
            // 
            this.loadVOC2007ToolStripMenuItem.Name = "loadVOC2007ToolStripMenuItem";
            this.loadVOC2007ToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.loadVOC2007ToolStripMenuItem.Text = "Load VOC0712...";
            this.loadVOC2007ToolStripMenuItem.Click += new System.EventHandler(this.loadVOC2007ToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(164, 6);
            // 
            // getSQLExpressToolStripMenuItem
            // 
            this.getSQLExpressToolStripMenuItem.Name = "getSQLExpressToolStripMenuItem";
            this.getSQLExpressToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.getSQLExpressToolStripMenuItem.Text = "Get SQL Express...";
            this.getSQLExpressToolStripMenuItem.Click += new System.EventHandler(this.getSqlExpressMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runAutotestsToolStripMenuItem,
            this.toolStripSeparator2,
            this.startAutotestsToolStripMenuItem,
            this.abortAutotestsToolStripMenuItem,
            this.toolStripSeparator1,
            this.createMyCaffeToolStripMenuItem,
            this.deviceInformationToolStripMenuItem,
            this.destroyMyCaffeToolStripMenuItem,
            this.trainMNISTToolStripMenuItem,
            this.testMNISTToolStripMenuItem,
            this.runTestImageToolStripMenuItem,
            this.saveTestImagesToolStripMenuItem,
            this.abortToolStripMenuItem,
            this.cancelToolStripMenuItem,
            this.toolStripSeparator3,
            this.specialTestsToolStripMenuItem,
            this.toolStripSeparator7,
            this.downloadTestDataToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.testToolStripMenuItem.Text = "&Test";
            // 
            // runAutotestsToolStripMenuItem
            // 
            this.runAutotestsToolStripMenuItem.Name = "runAutotestsToolStripMenuItem";
            this.runAutotestsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.runAutotestsToolStripMenuItem.Text = "&Run Autotests UI";
            this.runAutotestsToolStripMenuItem.Click += new System.EventHandler(this.runAutotestsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(189, 6);
            // 
            // startAutotestsToolStripMenuItem
            // 
            this.startAutotestsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.startWithResetToolStripMenuItem});
            this.startAutotestsToolStripMenuItem.Name = "startAutotestsToolStripMenuItem";
            this.startAutotestsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.startAutotestsToolStripMenuItem.Text = "Start Server Autotests";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startServerAutoTestToolStripMenuItem_Click);
            // 
            // startWithResetToolStripMenuItem
            // 
            this.startWithResetToolStripMenuItem.Name = "startWithResetToolStripMenuItem";
            this.startWithResetToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.startWithResetToolStripMenuItem.Text = "Start with Reset";
            this.startWithResetToolStripMenuItem.Click += new System.EventHandler(this.startServerAutoTestWithResetToolStripMenuItem_Click);
            // 
            // abortAutotestsToolStripMenuItem
            // 
            this.abortAutotestsToolStripMenuItem.Enabled = false;
            this.abortAutotestsToolStripMenuItem.Name = "abortAutotestsToolStripMenuItem";
            this.abortAutotestsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.abortAutotestsToolStripMenuItem.Text = "Abort Server Autotests";
            this.abortAutotestsToolStripMenuItem.Click += new System.EventHandler(this.abortAutotestsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
            // 
            // createMyCaffeToolStripMenuItem
            // 
            this.createMyCaffeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usingLeNETToolStripMenuItem,
            this.usingSiameseNETToolStripMenuItem,
            this.usingTripletNETToolStripMenuItem});
            this.createMyCaffeToolStripMenuItem.Name = "createMyCaffeToolStripMenuItem";
            this.createMyCaffeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.createMyCaffeToolStripMenuItem.Text = "Create MyCaffe";
            // 
            // usingLeNETToolStripMenuItem
            // 
            this.usingLeNETToolStripMenuItem.Name = "usingLeNETToolStripMenuItem";
            this.usingLeNETToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.usingLeNETToolStripMenuItem.Text = "Using LeNET";
            this.usingLeNETToolStripMenuItem.Click += new System.EventHandler(this.createUsingLeNETToolStripMenuItem_Click);
            // 
            // usingSiameseNETToolStripMenuItem
            // 
            this.usingSiameseNETToolStripMenuItem.Name = "usingSiameseNETToolStripMenuItem";
            this.usingSiameseNETToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.usingSiameseNETToolStripMenuItem.Text = "Using SiameseNET";
            this.usingSiameseNETToolStripMenuItem.Click += new System.EventHandler(this.createUsingSiameseNETToolStripMenuItem_Click);
            // 
            // usingTripletNETToolStripMenuItem
            // 
            this.usingTripletNETToolStripMenuItem.Name = "usingTripletNETToolStripMenuItem";
            this.usingTripletNETToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.usingTripletNETToolStripMenuItem.Text = "Using TripletNET";
            this.usingTripletNETToolStripMenuItem.Click += new System.EventHandler(this.createUsingTripletNETToolStripMenuItem_Click);
            // 
            // deviceInformationToolStripMenuItem
            // 
            this.deviceInformationToolStripMenuItem.Enabled = false;
            this.deviceInformationToolStripMenuItem.Name = "deviceInformationToolStripMenuItem";
            this.deviceInformationToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.deviceInformationToolStripMenuItem.Text = "Device Information";
            this.deviceInformationToolStripMenuItem.Click += new System.EventHandler(this.deviceInformationToolStripMenuItem_Click);
            // 
            // destroyMyCaffeToolStripMenuItem
            // 
            this.destroyMyCaffeToolStripMenuItem.Enabled = false;
            this.destroyMyCaffeToolStripMenuItem.Name = "destroyMyCaffeToolStripMenuItem";
            this.destroyMyCaffeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.destroyMyCaffeToolStripMenuItem.Text = "Destroy MyCaffe";
            this.destroyMyCaffeToolStripMenuItem.Click += new System.EventHandler(this.destroyMyCaffeToolStripMenuItem_Click);
            // 
            // trainMNISTToolStripMenuItem
            // 
            this.trainMNISTToolStripMenuItem.Enabled = false;
            this.trainMNISTToolStripMenuItem.Name = "trainMNISTToolStripMenuItem";
            this.trainMNISTToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.trainMNISTToolStripMenuItem.Text = "Train MNIST";
            this.trainMNISTToolStripMenuItem.Click += new System.EventHandler(this.trainMNISTToolStripMenuItem_Click);
            // 
            // testMNISTToolStripMenuItem
            // 
            this.testMNISTToolStripMenuItem.Enabled = false;
            this.testMNISTToolStripMenuItem.Name = "testMNISTToolStripMenuItem";
            this.testMNISTToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.testMNISTToolStripMenuItem.Text = "Test MNIST";
            this.testMNISTToolStripMenuItem.Click += new System.EventHandler(this.testMNISTToolStripMenuItem_Click);
            // 
            // runTestImageToolStripMenuItem
            // 
            this.runTestImageToolStripMenuItem.Enabled = false;
            this.runTestImageToolStripMenuItem.Name = "runTestImageToolStripMenuItem";
            this.runTestImageToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.runTestImageToolStripMenuItem.Text = "Run Test Image";
            this.runTestImageToolStripMenuItem.Click += new System.EventHandler(this.runTestImageToolStripMenuItem_Click);
            // 
            // saveTestImagesToolStripMenuItem
            // 
            this.saveTestImagesToolStripMenuItem.CheckOnClick = true;
            this.saveTestImagesToolStripMenuItem.Name = "saveTestImagesToolStripMenuItem";
            this.saveTestImagesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveTestImagesToolStripMenuItem.Text = "Save Test Images...";
            this.saveTestImagesToolStripMenuItem.Click += new System.EventHandler(this.saveTestImagesToolStripMenuItem_Click);
            // 
            // abortToolStripMenuItem
            // 
            this.abortToolStripMenuItem.Enabled = false;
            this.abortToolStripMenuItem.Name = "abortToolStripMenuItem";
            this.abortToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.abortToolStripMenuItem.Text = "Abort";
            this.abortToolStripMenuItem.ToolTipText = "Abort the command thread.";
            this.abortToolStripMenuItem.Click += new System.EventHandler(this.abortToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Enabled = false;
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.ToolTipText = "Cancel the current operation.";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(189, 6);
            // 
            // specialTestsToolStripMenuItem
            // 
            this.specialTestsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alexNetCifarCUDA8BugToolStripMenuItem,
            this.alexNetToolStripMenuItem,
            this.resNet56CifarAccuracyBugToolStripMenuItem,
            this.testPythonInteropToolStripMenuItem,
            this.toolStripSeparator5,
            this.startCartPoleTrainerToolStripMenuItem,
            this.startAtariTrainerToolStripMenuItem,
            this.showGymUiToolStripMenuItem,
            this.toolStripSeparator4,
            this.startNeuralStyleTransferToolStripMenuItem});
            this.specialTestsToolStripMenuItem.Name = "specialTestsToolStripMenuItem";
            this.specialTestsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.specialTestsToolStripMenuItem.Text = "Special Tests";
            // 
            // alexNetCifarCUDA8BugToolStripMenuItem
            // 
            this.alexNetCifarCUDA8BugToolStripMenuItem.Name = "alexNetCifarCUDA8BugToolStripMenuItem";
            this.alexNetCifarCUDA8BugToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.alexNetCifarCUDA8BugToolStripMenuItem.Text = "AlexNet-Cifar CUDA 8 Bug";
            this.alexNetCifarCUDA8BugToolStripMenuItem.Click += new System.EventHandler(this.alexNetCifarCUDA8BugToolStripMenuItem_Click);
            // 
            // alexNetToolStripMenuItem
            // 
            this.alexNetToolStripMenuItem.Name = "alexNetToolStripMenuItem";
            this.alexNetToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.alexNetToolStripMenuItem.Text = "AlexNet-Cifar Load Storage Bug";
            this.alexNetToolStripMenuItem.Click += new System.EventHandler(this.alexNetToolStripMenuItem_Click);
            // 
            // resNet56CifarAccuracyBugToolStripMenuItem
            // 
            this.resNet56CifarAccuracyBugToolStripMenuItem.Name = "resNet56CifarAccuracyBugToolStripMenuItem";
            this.resNet56CifarAccuracyBugToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.resNet56CifarAccuracyBugToolStripMenuItem.Text = "ResNet56-Cifar Accuracy Bug";
            this.resNet56CifarAccuracyBugToolStripMenuItem.Click += new System.EventHandler(this.resNet56CifarAccuracyBugToolStripMenuItem_Click);
            // 
            // testPythonInteropToolStripMenuItem
            // 
            this.testPythonInteropToolStripMenuItem.Name = "testPythonInteropToolStripMenuItem";
            this.testPythonInteropToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.testPythonInteropToolStripMenuItem.Text = "Test Python Interop with GPT";
            this.testPythonInteropToolStripMenuItem.Click += new System.EventHandler(this.testPythonInteropToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(239, 6);
            // 
            // startCartPoleTrainerToolStripMenuItem
            // 
            this.startCartPoleTrainerToolStripMenuItem.Name = "startCartPoleTrainerToolStripMenuItem";
            this.startCartPoleTrainerToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.startCartPoleTrainerToolStripMenuItem.Text = "Start Cart-Pole Trainer";
            this.startCartPoleTrainerToolStripMenuItem.Click += new System.EventHandler(this.startCartPoleTrainerToolStripMenuItem_Click);
            // 
            // startAtariTrainerToolStripMenuItem
            // 
            this.startAtariTrainerToolStripMenuItem.Name = "startAtariTrainerToolStripMenuItem";
            this.startAtariTrainerToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.startAtariTrainerToolStripMenuItem.Text = "Start Atari Trainer";
            this.startAtariTrainerToolStripMenuItem.Click += new System.EventHandler(this.startAtariTrainerToolStripMenuItem_Click);
            // 
            // showGymUiToolStripMenuItem
            // 
            this.showGymUiToolStripMenuItem.Name = "showGymUiToolStripMenuItem";
            this.showGymUiToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.showGymUiToolStripMenuItem.Text = "Show Gym Ui";
            this.showGymUiToolStripMenuItem.Click += new System.EventHandler(this.showGymUiToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(239, 6);
            // 
            // startNeuralStyleTransferToolStripMenuItem
            // 
            this.startNeuralStyleTransferToolStripMenuItem.Name = "startNeuralStyleTransferToolStripMenuItem";
            this.startNeuralStyleTransferToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.startNeuralStyleTransferToolStripMenuItem.Text = "Start Neural Style Transfer";
            this.startNeuralStyleTransferToolStripMenuItem.Click += new System.EventHandler(this.startNeuralStyleTransferToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(189, 6);
            // 
            // downloadTestDataToolStripMenuItem
            // 
            this.downloadTestDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testDataDownloadTfTToolStripMenuItem,
            this.testDataDownloadTFTDataOnlyToolStripMenuItem,
            this.testDataDownloadGPTToolStripMenuItem,
            this.testDataDownloadLNNToolStripMenuItem});
            this.downloadTestDataToolStripMenuItem.Name = "downloadTestDataToolStripMenuItem";
            this.downloadTestDataToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.downloadTestDataToolStripMenuItem.Text = "Download Test Data";
            // 
            // testDataDownloadTfTToolStripMenuItem
            // 
            this.testDataDownloadTfTToolStripMenuItem.Name = "testDataDownloadTfTToolStripMenuItem";
            this.testDataDownloadTfTToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.testDataDownloadTfTToolStripMenuItem.Text = "TFT";
            this.testDataDownloadTfTToolStripMenuItem.Click += new System.EventHandler(this.testDownloadTestDataTftToolStripMenuItem_Click);
            // 
            // testDataDownloadTFTDataOnlyToolStripMenuItem
            // 
            this.testDataDownloadTFTDataOnlyToolStripMenuItem.Name = "testDataDownloadTFTDataOnlyToolStripMenuItem";
            this.testDataDownloadTFTDataOnlyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.testDataDownloadTFTDataOnlyToolStripMenuItem.Text = "TFT (Data Only)";
            this.testDataDownloadTFTDataOnlyToolStripMenuItem.Click += new System.EventHandler(this.testDownloadTestDataTftDataOnlyToolStripMenuItem_Click);
            // 
            // testDataDownloadGPTToolStripMenuItem
            // 
            this.testDataDownloadGPTToolStripMenuItem.Name = "testDataDownloadGPTToolStripMenuItem";
            this.testDataDownloadGPTToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.testDataDownloadGPTToolStripMenuItem.Text = "GPT";
            this.testDataDownloadGPTToolStripMenuItem.Click += new System.EventHandler(this.testDataDownloadGPTToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localHelpToolStripMenuItem,
            this.onlineHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // localHelpToolStripMenuItem
            // 
            this.localHelpToolStripMenuItem.Name = "localHelpToolStripMenuItem";
            this.localHelpToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.localHelpToolStripMenuItem.Text = "Local Help";
            this.localHelpToolStripMenuItem.Click += new System.EventHandler(this.localHelpToolStripMenuItem_Click);
            // 
            // onlineHelpToolStripMenuItem
            // 
            this.onlineHelpToolStripMenuItem.Enabled = false;
            this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
            this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.onlineHelpToolStripMenuItem.Text = "Online Help";
            this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.onlineHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // gpuToolStripMenuItem
            // 
            this.gpuToolStripMenuItem.Name = "gpuToolStripMenuItem";
            this.gpuToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.gpuToolStripMenuItem.Text = "GPU";
            // 
            // imageDBToolStripMenuItem
            // 
            this.imageDBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.v1ToolStripMenuItem,
            this.v2ToolStripMenuItem});
            this.imageDBToolStripMenuItem.Name = "imageDBToolStripMenuItem";
            this.imageDBToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.imageDBToolStripMenuItem.Text = "Image DB";
            // 
            // v1ToolStripMenuItem
            // 
            this.v1ToolStripMenuItem.Name = "v1ToolStripMenuItem";
            this.v1ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.v1ToolStripMenuItem.Text = "V1";
            this.v1ToolStripMenuItem.Click += new System.EventHandler(this.menuImgDb_Click);
            // 
            // v2ToolStripMenuItem
            // 
            this.v2ToolStripMenuItem.Name = "v2ToolStripMenuItem";
            this.v2ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.v2ToolStripMenuItem.Text = "V2";
            this.v2ToolStripMenuItem.Click += new System.EventHandler(this.menuImgDb_Click);
            // 
            // cultureToolStripMenuItem
            // 
            this.cultureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.enUSToolStripMenuItem,
            this.deDEToolStripMenuItem});
            this.cultureToolStripMenuItem.Name = "cultureToolStripMenuItem";
            this.cultureToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.cultureToolStripMenuItem.Text = "Culture";
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Checked = true;
            this.defaultToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // enUSToolStripMenuItem
            // 
            this.enUSToolStripMenuItem.Name = "enUSToolStripMenuItem";
            this.enUSToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.enUSToolStripMenuItem.Text = "en-US";
            this.enUSToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // deDEToolStripMenuItem
            // 
            this.deDEToolStripMenuItem.Name = "deDEToolStripMenuItem";
            this.deDEToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.deDEToolStripMenuItem.Text = "de-DE";
            this.deDEToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // openFileDialogAutoTests
            // 
            this.openFileDialogAutoTests.DefaultExt = "dll";
            this.openFileDialogAutoTests.Filter = "Auto Test Files (*.test.dll)|*.test.dll||";
            this.openFileDialogAutoTests.Title = "Select the MyCaffe.test.dll";
            // 
            // m_bwLoadMnistDatabase
            // 
            this.m_bwLoadMnistDatabase.WorkerReportsProgress = true;
            this.m_bwLoadMnistDatabase.WorkerSupportsCancellation = true;
            this.m_bwLoadMnistDatabase.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_bwLoadMnistDatabase_DoWork);
            this.m_bwLoadMnistDatabase.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.m_bw_ProgressChanged);
            this.m_bwLoadMnistDatabase.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.m_bw_RunWorkerCompleted);
            // 
            // m_bwProcess
            // 
            this.m_bwProcess.WorkerReportsProgress = true;
            this.m_bwProcess.WorkerSupportsCancellation = true;
            this.m_bwProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_bwProcess_DoWork);
            this.m_bwProcess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.m_bw_ProgressChanged);
            this.m_bwProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.m_bw_RunWorkerCompleted);
            // 
            // m_bwLoadCiFar10Database
            // 
            this.m_bwLoadCiFar10Database.WorkerReportsProgress = true;
            this.m_bwLoadCiFar10Database.WorkerSupportsCancellation = true;
            this.m_bwLoadCiFar10Database.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_bwLoadCiFar10Database_DoWork);
            this.m_bwLoadCiFar10Database.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.m_bw_ProgressChanged);
            this.m_bwLoadCiFar10Database.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.m_bw_RunWorkerCompleted);
            // 
            // m_bwInit
            // 
            this.m_bwInit.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_bwInit_DoWork);
            this.m_bwInit.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.m_bwInit_RunWorkerCompleted);
            // 
            // m_bwUrlCheck
            // 
            this.m_bwUrlCheck.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_bwUrlCheck_DoWork);
            this.m_bwUrlCheck.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.m_bwUrlCheck_RunWorkerCompleted);
            // 
            // lvStatus
            // 
            this.lvStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStatus});
            this.lvStatus.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvStatus.FullRowSelect = true;
            this.lvStatus.HideSelection = false;
            this.lvStatus.Location = new System.Drawing.Point(0, 27);
            this.lvStatus.Name = "lvStatus";
            this.lvStatus.RowHeight = 14;
            this.lvStatus.Size = new System.Drawing.Size(957, 403);
            this.lvStatus.TabIndex = 1;
            this.lvStatus.UseCompatibleStateImageBehavior = false;
            this.lvStatus.View = System.Windows.Forms.View.Details;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 934;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblGpu,
            this.lblImgDb,
            this.lblCudaPath});
            this.statusStrip1.Location = new System.Drawing.Point(0, 430);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(957, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblGpu
            // 
            this.lblGpu.Name = "lblGpu";
            this.lblGpu.Size = new System.Drawing.Size(0, 17);
            // 
            // lblImgDb
            // 
            this.lblImgDb.Name = "lblImgDb";
            this.lblImgDb.Size = new System.Drawing.Size(0, 17);
            // 
            // lblCudaPath
            // 
            this.lblCudaPath.Name = "lblCudaPath";
            this.lblCudaPath.Size = new System.Drawing.Size(0, 17);
            // 
            // timerUI
            // 
            this.timerUI.Enabled = true;
            this.timerUI.Interval = 1000;
            this.timerUI.Tick += new System.EventHandler(this.timerUI_Tick);
            // 
            // m_bwLoadVOCDatabase
            // 
            this.m_bwLoadVOCDatabase.WorkerReportsProgress = true;
            this.m_bwLoadVOCDatabase.WorkerSupportsCancellation = true;
            this.m_bwLoadVOCDatabase.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_bwLoadVOCDatabase_DoWork);
            this.m_bwLoadVOCDatabase.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.m_bw_ProgressChanged);
            this.m_bwLoadVOCDatabase.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.m_bw_RunWorkerCompleted);
            // 
            // testDataDownloadLNNToolStripMenuItem
            // 
            this.testDataDownloadLNNToolStripMenuItem.Name = "testDataDownloadLNNToolStripMenuItem";
            this.testDataDownloadLNNToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.testDataDownloadLNNToolStripMenuItem.Text = "LNN";
            this.testDataDownloadLNNToolStripMenuItem.Click += new System.EventHandler(this.testDataDownloadLNNToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 452);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lvStatus);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "MyCaffe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runAutotestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogAutoTests;
        private System.Windows.Forms.ToolStripMenuItem deviceInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMNISTToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker m_bwLoadMnistDatabase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem trainMNISTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testMNISTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abortToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker m_bwProcess;
        private System.Windows.Forms.ToolStripMenuItem createMyCaffeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem destroyMyCaffeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCIFAR10ToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker m_bwLoadCiFar10Database;
        private System.Windows.Forms.ToolStripMenuItem runTestImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAutotestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abortAutotestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startWithResetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gpuToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker m_bwInit;
        private System.ComponentModel.BackgroundWorker m_bwUrlCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem specialTestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alexNetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resNet56CifarAccuracyBugToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblGpu;
        private System.Windows.Forms.ToolStripStatusLabel lblImgDb;
        private System.Windows.Forms.Timer timerUI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem startCartPoleTrainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAtariTrainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem startNeuralStyleTransferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGymUiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadVOC2007ToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker m_bwLoadVOCDatabase;
        private System.Windows.Forms.ToolStripMenuItem usingLeNETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingSiameseNETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem v1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem v2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblCudaPath;
        private System.Windows.Forms.ToolStripMenuItem usingTripletNETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTestImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cultureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enUSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deDEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem getSQLExpressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testPythonInteropToolStripMenuItem;
        private ListViewEx lvStatus;
        private System.Windows.Forms.ToolStripMenuItem alexNetCifarCUDA8BugToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem downloadTestDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDataDownloadTfTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDataDownloadTFTDataOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDataDownloadGPTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDataDownloadLNNToolStripMenuItem;
    }
}

