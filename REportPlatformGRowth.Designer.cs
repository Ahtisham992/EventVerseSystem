namespace EventVerse
{
    partial class REportPlatformGRowth
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.cATEGORY1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new EventVerse.DataSet1();
            this.button2 = new System.Windows.Forms.Button();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cATEGORY1TableAdapter = new EventVerse.DataSet1TableAdapters.CATEGORY1TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.cATEGORY1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // cATEGORY1BindingSource
            // 
            this.cATEGORY1BindingSource.DataMember = "CATEGORY1";
            this.cATEGORY1BindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(96, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 39);
            this.button2.TabIndex = 2;
            this.button2.Text = "back";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // reportViewer2
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.cATEGORY1BindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "EventVerse.Reportplatformgrowth.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(96, 138);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ServerReport.BearerToken = null;
            this.reportViewer2.Size = new System.Drawing.Size(890, 246);
            this.reportViewer2.TabIndex = 3;
            // 
            // cATEGORY1TableAdapter
            // 
            this.cATEGORY1TableAdapter.ClearBeforeFill = true;
            // 
            // REportPlatformGRowth
            // 
            this.ClientSize = new System.Drawing.Size(1053, 472);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.button2);
            this.Name = "REportPlatformGRowth";
            this.Load += new System.EventHandler(this.REportPlatformGRowth_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.cATEGORY1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource cATEGORYBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource cATEGORY1BindingSource;
        private DataSet1TableAdapters.CATEGORY1TableAdapter cATEGORY1TableAdapter;
    }
}