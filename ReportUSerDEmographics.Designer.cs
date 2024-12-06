namespace EventVerse
{
    partial class ReportUSerDEmographics
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
            this.button2 = new System.Windows.Forms.Button();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dataSet1 = new EventVerse.DataSet1();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataTable2TableAdapter1 = new EventVerse.DataSet1TableAdapters.DataTable2TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(106, 61);
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
            reportDataSource1.Value = this.bindingSource1;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "EventVerse.ReportuserDemo.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(106, 173);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ServerReport.BearerToken = null;
            this.reportViewer2.Size = new System.Drawing.Size(846, 246);
            this.reportViewer2.TabIndex = 3;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "DataTable2";
            this.bindingSource1.DataSource = this.dataSet1;
            // 
            // dataTable2TableAdapter1
            // 
            this.dataTable2TableAdapter1.ClearBeforeFill = true;
            // 
            // ReportUSerDEmographics
            // 
            this.ClientSize = new System.Drawing.Size(1038, 455);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.button2);
            this.Name = "ReportUSerDEmographics";
            this.Load += new System.EventHandler(this.ReportUSerDEmographics_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource dataTable2BindingSource;
        private System.Windows.Forms.Button button2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DataSet1TableAdapters.DataTable2TableAdapter dataTable2TableAdapter1;
    }
}