namespace midas_sise
{
    partial class Form1
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
            this.button_login = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.EB_rb_demo = new System.Windows.Forms.RadioButton();
            this.EB_rb_hts = new System.Windows.Forms.RadioButton();
            this.EB_rb_127 = new System.Windows.Forms.RadioButton();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.button_hide_console = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tb_shutdown = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_relogin = new System.Windows.Forms.CheckBox();
            this.btn_t8407 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(8, 66);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(170, 26);
            this.button_login.TabIndex = 338;
            this.button_login.Text = "login 및 실행";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EB_rb_demo);
            this.groupBox2.Controls.Add(this.EB_rb_hts);
            this.groupBox2.Controls.Add(this.EB_rb_127);
            this.groupBox2.Location = new System.Drawing.Point(8, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 33);
            this.groupBox2.TabIndex = 340;
            this.groupBox2.TabStop = false;
            // 
            // EB_rb_demo
            // 
            this.EB_rb_demo.AutoSize = true;
            this.EB_rb_demo.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this.EB_rb_demo.Location = new System.Drawing.Point(6, 12);
            this.EB_rb_demo.Name = "EB_rb_demo";
            this.EB_rb_demo.Size = new System.Drawing.Size(55, 16);
            this.EB_rb_demo.TabIndex = 425;
            this.EB_rb_demo.Text = "demo";
            this.EB_rb_demo.UseVisualStyleBackColor = true;
            // 
            // EB_rb_hts
            // 
            this.EB_rb_hts.AutoSize = true;
            this.EB_rb_hts.Checked = true;
            this.EB_rb_hts.Location = new System.Drawing.Point(65, 12);
            this.EB_rb_hts.Name = "EB_rb_hts";
            this.EB_rb_hts.Size = new System.Drawing.Size(40, 16);
            this.EB_rb_hts.TabIndex = 426;
            this.EB_rb_hts.TabStop = true;
            this.EB_rb_hts.Text = "hts";
            this.EB_rb_hts.UseVisualStyleBackColor = true;
            // 
            // EB_rb_127
            // 
            this.EB_rb_127.AutoSize = true;
            this.EB_rb_127.Location = new System.Drawing.Point(116, 12);
            this.EB_rb_127.Name = "EB_rb_127";
            this.EB_rb_127.Size = new System.Drawing.Size(41, 16);
            this.EB_rb_127.TabIndex = 427;
            this.EB_rb_127.Text = "127";
            this.EB_rb_127.UseVisualStyleBackColor = true;
            // 
            // tb_log
            // 
            this.tb_log.Font = new System.Drawing.Font("굴림", 8F);
            this.tb_log.Location = new System.Drawing.Point(7, 156);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_log.Size = new System.Drawing.Size(227, 247);
            this.tb_log.TabIndex = 393;
            // 
            // button_hide_console
            // 
            this.button_hide_console.Location = new System.Drawing.Point(7, 98);
            this.button_hide_console.Name = "button_hide_console";
            this.button_hide_console.Size = new System.Drawing.Size(81, 23);
            this.button_hide_console.TabIndex = 394;
            this.button_hide_console.Text = "콘솔 SHOW";
            this.button_hide_console.UseVisualStyleBackColor = true;
            this.button_hide_console.Click += new System.EventHandler(this.button_hide_console_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 395;
            this.button1.Text = "삼성전자";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_t1101_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 126);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 23);
            this.button2.TabIndex = 396;
            this.button2.Text = "서버시간 request";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_t0167_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(97, 126);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(81, 23);
            this.button4.TabIndex = 398;
            this.button4.Text = "타이머실행";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button_timer_Click);
            // 
            // tb_shutdown
            // 
            this.tb_shutdown.Font = new System.Drawing.Font("굴림", 8F);
            this.tb_shutdown.Location = new System.Drawing.Point(162, 438);
            this.tb_shutdown.Multiline = true;
            this.tb_shutdown.Name = "tb_shutdown";
            this.tb_shutdown.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_shutdown.Size = new System.Drawing.Size(69, 23);
            this.tb_shutdown.TabIndex = 399;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 12);
            this.label1.TabIndex = 400;
            this.label1.Text = "45. re-login checed";
            // 
            // cb_relogin
            // 
            this.cb_relogin.AutoSize = true;
            this.cb_relogin.Checked = true;
            this.cb_relogin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_relogin.Location = new System.Drawing.Point(165, 8);
            this.cb_relogin.Name = "cb_relogin";
            this.cb_relogin.Size = new System.Drawing.Size(68, 16);
            this.cb_relogin.TabIndex = 401;
            this.cb_relogin.Text = "re-login";
            this.cb_relogin.UseVisualStyleBackColor = true;
            // 
            // btn_t8407
            // 
            this.btn_t8407.Location = new System.Drawing.Point(8, 409);
            this.btn_t8407.Name = "btn_t8407";
            this.btn_t8407.Size = new System.Drawing.Size(226, 23);
            this.btn_t8407.TabIndex = 402;
            this.btn_t8407.Text = "장마감후 종가 upload t8407";
            this.btn_t8407.UseVisualStyleBackColor = true;
            this.btn_t8407.Click += new System.EventHandler(this.btn_t8407_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 467);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(227, 132);
            this.dataGridView1.TabIndex = 403;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 438);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 404;
            this.button3.Text = "테이블조회";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 611);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_t8407);
            this.Controls.Add(this.cb_relogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_shutdown);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_hide_console);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_login);
            this.Name = "Form1";
            this.Text = "2.2_view";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton EB_rb_demo;
        private System.Windows.Forms.RadioButton EB_rb_hts;
        private System.Windows.Forms.RadioButton EB_rb_127;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.Button button_hide_console;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox tb_shutdown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_relogin;
        private System.Windows.Forms.Button btn_t8407;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
    }
}

