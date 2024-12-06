namespace EventVerse
{
    partial class ReportEventAttendence
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
            this.eVENTBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new EventVerse.DataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.eVENTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eVENTTableAdapter = new EventVerse.DataSet1TableAdapters.EVENTTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.eVENTBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.eVENTBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eVENTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eVENTBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // eVENTBindingSource1
            // 
            this.eVENTBindingSource1.DataMember = "EVENT";
            this.eVENTBindingSource1.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.eVENTBindingSource2;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "EventVerse.ReportEventattendence.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(64, 150);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(917, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // eVENTBindingSource
            // 
            this.eVENTBindingSource.DataMember = "EVENT";
            this.eVENTBindingSource.DataSource = this.dataSet1;
            // 
            // eVENTTableAdapter
            // 
            this.eVENTTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(64, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 54);
            this.button1.TabIndex = 2;
            this.button1.Text = "back";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // eVENTBindingSource2
            // 
            this.eVENTBindingSource2.DataMember = "EVENT";
            this.eVENTBindingSource2.DataSource = this.dataSet1;
            // 
            // ReportEventAttendence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportEventAttendence";
            this.Text = "ReportEventAttendence";
            this.Load += new System.EventHandler(this.ReportEventAttendence_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eVENTBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eVENTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eVENTBindingSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource eVENTBindingSource;
        private DataSet1TableAdapters.EVENTTableAdapter eVENTTableAdapter;
        private System.Windows.Forms.BindingSource eVENTBindingSource1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource eVENTBindingSource2;
    }
}