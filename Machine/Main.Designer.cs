namespace Machine
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.but_close = new System.Windows.Forms.Button();
            this.combo_camera = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.text_sensitive = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.text_end = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.text_start = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.장치새로고침ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.시작ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.중지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.TrayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // but_close
            // 
            this.but_close.Location = new System.Drawing.Point(12, 174);
            this.but_close.Name = "but_close";
            this.but_close.Size = new System.Drawing.Size(260, 27);
            this.but_close.TabIndex = 0;
            this.but_close.Text = "종료";
            this.but_close.UseVisualStyleBackColor = true;
            this.but_close.Click += new System.EventHandler(this.but_close_Click);
            // 
            // combo_camera
            // 
            this.combo_camera.FormattingEnabled = true;
            this.combo_camera.Location = new System.Drawing.Point(73, 20);
            this.combo_camera.Name = "combo_camera";
            this.combo_camera.Size = new System.Drawing.Size(173, 20);
            this.combo_camera.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(16, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "카메라:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(15, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "마우스 감도:";
            // 
            // text_sensitive
            // 
            this.text_sensitive.Location = new System.Drawing.Point(103, 46);
            this.text_sensitive.Name = "text_sensitive";
            this.text_sensitive.Size = new System.Drawing.Size(143, 21);
            this.text_sensitive.TabIndex = 6;
            this.text_sensitive.Text = "3";
            this.text_sensitive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_sensitive_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(15, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "중지 단축키:";
            // 
            // text_end
            // 
            this.text_end.Location = new System.Drawing.Point(103, 101);
            this.text_end.Name = "text_end";
            this.text_end.ReadOnly = true;
            this.text_end.Size = new System.Drawing.Size(143, 21);
            this.text_end.TabIndex = 8;
            this.text_end.Text = "F2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(15, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "시작 단축키:";
            // 
            // text_start
            // 
            this.text_start.Location = new System.Drawing.Point(103, 74);
            this.text_start.Name = "text_start";
            this.text_start.ReadOnly = true;
            this.text_start.Size = new System.Drawing.Size(143, 21);
            this.text_start.TabIndex = 10;
            this.text_start.Text = "F1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.text_start);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.text_end);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.text_sensitive);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.combo_camera);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 131);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "설정";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.장치새로고침ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 장치새로고침ToolStripMenuItem
            // 
            this.장치새로고침ToolStripMenuItem.Name = "장치새로고침ToolStripMenuItem";
            this.장치새로고침ToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.장치새로고침ToolStripMenuItem.Text = "(&D)장치 새로고침";
            this.장치새로고침ToolStripMenuItem.Click += new System.EventHandler(this.장치새로고침ToolStripMenuItem_Click);
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenuStrip = this.TrayMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "핸드마우스";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseDoubleClick);
            // 
            // TrayMenu
            // 
            this.TrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.시작ToolStripMenuItem,
            this.중지ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.TrayMenu.Name = "TrayMenu";
            this.TrayMenu.Size = new System.Drawing.Size(181, 92);
            // 
            // 시작ToolStripMenuItem
            // 
            this.시작ToolStripMenuItem.Name = "시작ToolStripMenuItem";
            this.시작ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.시작ToolStripMenuItem.Text = "실행";
            this.시작ToolStripMenuItem.Click += new System.EventHandler(this.실행ToolStripMenuItem_Click);
            // 
            // 중지ToolStripMenuItem
            // 
            this.중지ToolStripMenuItem.Name = "중지ToolStripMenuItem";
            this.중지ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.중지ToolStripMenuItem.Text = "중지";
            this.중지ToolStripMenuItem.Click += new System.EventHandler(this.중지ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 209);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.but_close);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "핸드마우스";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.TrayMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button but_close;
        private System.Windows.Forms.ComboBox combo_camera;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_sensitive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox text_end;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox text_start;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 장치새로고침ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenuStrip TrayMenu;
        private System.Windows.Forms.ToolStripMenuItem 시작ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 중지ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
    }
}

