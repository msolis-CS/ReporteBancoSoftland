namespace DmRptBanco
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.directXFormContainerControl1 = new DevExpress.XtraEditors.DirectXFormContainerControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbNumeroNomina = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbNomina = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbEntidadFinanciera = new DevExpress.XtraEditors.LookUpEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.directXFormContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNumeroNomina.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNomina.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntidadFinanciera.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // directXFormContainerControl1
            // 
            this.directXFormContainerControl1.Controls.Add(this.simpleButton1);
            this.directXFormContainerControl1.Controls.Add(this.cmbEntidadFinanciera);
            this.directXFormContainerControl1.Controls.Add(this.labelControl3);
            this.directXFormContainerControl1.Controls.Add(this.labelControl2);
            this.directXFormContainerControl1.Controls.Add(this.labelControl1);
            this.directXFormContainerControl1.Controls.Add(this.cmbNumeroNomina);
            this.directXFormContainerControl1.Controls.Add(this.cmbNomina);
            this.directXFormContainerControl1.Location = new System.Drawing.Point(1, 31);
            this.directXFormContainerControl1.Name = "directXFormContainerControl1";
            this.directXFormContainerControl1.Size = new System.Drawing.Size(306, 141);
            this.directXFormContainerControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 46);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(75, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Número Nómina";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(61, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(35, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Nómina";
            // 
            // cmbNumeroNomina
            // 
            this.cmbNumeroNomina.Location = new System.Drawing.Point(105, 43);
            this.cmbNumeroNomina.Name = "cmbNumeroNomina";
            this.cmbNumeroNomina.Properties.Appearance.Options.UseTextOptions = true;
            this.cmbNumeroNomina.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbNumeroNomina.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cmbNumeroNomina.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbNumeroNomina.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNumeroNomina.Properties.NullText = "Selecciona Número Nómina";
            this.cmbNumeroNomina.Size = new System.Drawing.Size(174, 20);
            this.cmbNumeroNomina.TabIndex = 1;
            // 
            // cmbNomina
            // 
            this.cmbNomina.Location = new System.Drawing.Point(105, 12);
            this.cmbNomina.Name = "cmbNomina";
            this.cmbNomina.Properties.Appearance.Options.UseTextOptions = true;
            this.cmbNomina.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbNomina.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cmbNomina.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbNomina.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNomina.Properties.NullText = "Selecciona Nómina";
            this.cmbNomina.Size = new System.Drawing.Size(174, 20);
            this.cmbNomina.TabIndex = 0;
            this.cmbNomina.EditValueChanged += new System.EventHandler(this.cmbNomina_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 79);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(88, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Entidad Financiera";
            // 
            // cmbEntidadFinanciera
            // 
            this.cmbEntidadFinanciera.Location = new System.Drawing.Point(105, 76);
            this.cmbEntidadFinanciera.Name = "cmbEntidadFinanciera";
            this.cmbEntidadFinanciera.Properties.Appearance.Options.UseTextOptions = true;
            this.cmbEntidadFinanciera.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbEntidadFinanciera.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cmbEntidadFinanciera.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbEntidadFinanciera.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEntidadFinanciera.Properties.NullText = "Selecciona Entidad Financiera";
            this.cmbEntidadFinanciera.Size = new System.Drawing.Size(174, 20);
            this.cmbEntidadFinanciera.TabIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(204, 109);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Iniciar";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 173);
            this.Controls.Add(this.directXFormContainerControl1);
            this.DoubleBuffered = true;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("MainForm.IconOptions.Icon")));
            this.Name = "MainForm";
            this.directXFormContainerControl1.ResumeLayout(false);
            this.directXFormContainerControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNumeroNomina.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNomina.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntidadFinanciera.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DirectXFormContainerControl directXFormContainerControl1;
        private DevExpress.XtraEditors.LookUpEdit cmbNomina;
        private DevExpress.XtraEditors.LookUpEdit cmbNumeroNomina;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit cmbEntidadFinanciera;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}

