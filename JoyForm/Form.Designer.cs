namespace JoyForm
{
    partial class Form
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.listViewControllers = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnButtons = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAxes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonClear = new System.Windows.Forms.Button();
            this.textBoxControllerLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listViewControllers);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.buttonClear);
            this.splitContainer.Panel2.Controls.Add(this.textBoxControllerLog);
            this.splitContainer.Size = new System.Drawing.Size(1102, 592);
            this.splitContainer.SplitterDistance = 547;
            this.splitContainer.TabIndex = 0;
            // 
            // listViewControllers
            // 
            this.listViewControllers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewControllers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnType,
            this.columnButtons,
            this.columnHats,
            this.columnAxes});
            this.listViewControllers.Location = new System.Drawing.Point(3, 12);
            this.listViewControllers.Name = "listViewControllers";
            this.listViewControllers.Size = new System.Drawing.Size(541, 526);
            this.listViewControllers.TabIndex = 0;
            this.listViewControllers.UseCompatibleStateImageBehavior = false;
            this.listViewControllers.View = System.Windows.Forms.View.Details;
            this.listViewControllers.VirtualMode = true;
            this.listViewControllers.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListViewControllers_RetrieveVirtualItem);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 198;
            // 
            // columnType
            // 
            this.columnType.Text = "Type";
            this.columnType.Width = 72;
            // 
            // columnButtons
            // 
            this.columnButtons.Text = "Buttons";
            // 
            // columnHats
            // 
            this.columnHats.Text = "Hats";
            this.columnHats.Width = 40;
            // 
            // columnAxes
            // 
            this.columnAxes.Text = "Axes";
            this.columnAxes.Width = 40;
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClear.AutoSize = true;
            this.buttonClear.Location = new System.Drawing.Point(236, 544);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(101, 36);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "&Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // textBoxControllerLog
            // 
            this.textBoxControllerLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxControllerLog.Location = new System.Drawing.Point(3, 12);
            this.textBoxControllerLog.Multiline = true;
            this.textBoxControllerLog.Name = "textBoxControllerLog";
            this.textBoxControllerLog.ReadOnly = true;
            this.textBoxControllerLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxControllerLog.Size = new System.Drawing.Size(536, 526);
            this.textBoxControllerLog.TabIndex = 0;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 592);
            this.Controls.Add(this.splitContainer);
            this.Name = "Form";
            this.Text = "NerfDX Forms Example";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView listViewControllers;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox textBoxControllerLog;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ColumnHeader columnButtons;
        private System.Windows.Forms.ColumnHeader columnHats;
        private System.Windows.Forms.ColumnHeader columnAxes;
    }
}

