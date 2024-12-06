namespace EventVerse
{
    partial class ReportEventRevenue
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
            this.button3 = new System.Windows.Forms.Button();
            this.reportViewer3 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dataSet1 = new EventVerse.DataSet1();
            this.cATEGORYBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cATEGORYTableAdapter = new EventVerse.DataSet1TableAdapters.CATEGORYTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cATEGORYBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(128, 56);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 29);
            this.button3.TabIndex = 2;
            this.button3.Text = "back";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // reportViewer3
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.cATEGORYBindingSource;
            this.reportViewer3.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer3.LocalReport.ReportEmbeddedResource = "EventVerse.ReporteventRevenue.rdlc";
            this.reportViewer3.Location = new System.Drawing.Point(128, 189);
            this.reportViewer3.Name = "reportViewer3";
            this.reportViewer3.ServerReport.BearerToken = null;
            this.reportViewer3.Size = new System.Drawing.Size(753, 246);
            this.reportViewer3.TabIndex = 3;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cATEGORYBindingSource
            // 
            this.cATEGORYBindingSource.DataMember = "CATEGORY";
            this.cATEGORYBindingSource.DataSource = this.dataSet1;
            // 
            // cATEGORYTableAdapter
            // 
            this.cATEGORYTableAdapter.ClearBeforeFill = true;
            // 
            // ReportEventRevenue
            // 
            this.ClientSize = new System.Drawing.Size(989, 482);
            this.Controls.Add(this.reportViewer3);
            this.Controls.Add(this.button3);
            this.Name = "ReportEventRevenue";
            this.Load += new System.EventHandler(this.ReportEventRevenue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cATEGORYBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource cATEGORY1BindingSource;
        private System.Windows.Forms.Button button1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer3;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource cATEGORYBindingSource;
        private DataSet1TableAdapters.CATEGORYTableAdapter cATEGORYTableAdapter;
    }
}