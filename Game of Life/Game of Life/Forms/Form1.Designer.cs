
namespace Game_of_Life
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboBoxSelectCell = new System.Windows.Forms.ComboBox();
            this.buttonCreateNewCell = new System.Windows.Forms.Button();
            this.buttonOpenImage = new System.Windows.Forms.Button();
            this.comboBoxBrush = new System.Windows.Forms.ComboBox();
            this.comboBoxFigures = new System.Windows.Forms.ComboBox();
            this.checkBoxSort = new System.Windows.Forms.CheckBox();
            this.comboBoxStyle = new System.Windows.Forms.ComboBox();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.numericUpDownUpdateTime = new System.Windows.Forms.NumericUpDown();
            this.buttonStartEmpty = new System.Windows.Forms.Button();
            this.checkBoxBorder = new System.Windows.Forms.CheckBox();
            this.labelS = new System.Windows.Forms.Label();
            this.textBoxS = new System.Windows.Forms.TextBox();
            this.labelB = new System.Windows.Forms.Label();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonNextStep = new System.Windows.Forms.Button();
            this.buttonProceed = new System.Windows.Forms.Button();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.numericUpDownDensity = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownSize = new System.Windows.Forms.NumericUpDown();
            this.labelSize = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxSelectCell);
            this.splitContainer1.Panel1.Controls.Add(this.buttonCreateNewCell);
            this.splitContainer1.Panel1.Controls.Add(this.buttonOpenImage);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxBrush);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxFigures);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxSort);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxStyle);
            this.splitContainer1.Panel1.Controls.Add(this.buttonHelp);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownUpdateTime);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStartEmpty);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxBorder);
            this.splitContainer1.Panel1.Controls.Add(this.labelS);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxS);
            this.splitContainer1.Panel1.Controls.Add(this.labelB);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxB);
            this.splitContainer1.Panel1.Controls.Add(this.buttonClear);
            this.splitContainer1.Panel1.Controls.Add(this.buttonNextStep);
            this.splitContainer1.Panel1.Controls.Add(this.buttonProceed);
            this.splitContainer1.Panel1.Controls.Add(this.labelSpeed);
            this.splitContainer1.Panel1.Controls.Add(this.trackBar1);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStop);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStart);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownDensity);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownSize);
            this.splitContainer1.Panel1.Controls.Add(this.labelSize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1122, 1057);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;
            // 
            // comboBoxSelectCell
            // 
            this.comboBoxSelectCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSelectCell.FormattingEnabled = true;
            this.comboBoxSelectCell.Location = new System.Drawing.Point(18, 474);
            this.comboBoxSelectCell.Name = "comboBoxSelectCell";
            this.comboBoxSelectCell.Size = new System.Drawing.Size(150, 32);
            this.comboBoxSelectCell.TabIndex = 26;
            this.comboBoxSelectCell.Text = "Выбор клетки";
            this.comboBoxSelectCell.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectCell_SelectedIndexChanged);
            this.comboBoxSelectCell.SelectionChangeCommitted += new System.EventHandler(this.comboBoxSelectCell_SelectionChangeCommitted);
            this.comboBoxSelectCell.Click += new System.EventHandler(this.comboBoxSelectCell_Click);
            // 
            // buttonCreateNewCell
            // 
            this.buttonCreateNewCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCreateNewCell.Location = new System.Drawing.Point(18, 428);
            this.buttonCreateNewCell.Name = "buttonCreateNewCell";
            this.buttonCreateNewCell.Size = new System.Drawing.Size(150, 40);
            this.buttonCreateNewCell.TabIndex = 25;
            this.buttonCreateNewCell.Text = "Редактор клеток";
            this.buttonCreateNewCell.UseVisualStyleBackColor = true;
            this.buttonCreateNewCell.Click += new System.EventHandler(this.buttonCreateNewCell_Click);
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOpenImage.Location = new System.Drawing.Point(120, 958);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(75, 40);
            this.buttonOpenImage.TabIndex = 24;
            this.buttonOpenImage.Text = "Открыть";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.buttonOpenImage_Click);
            // 
            // comboBoxBrush
            // 
            this.comboBoxBrush.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxBrush.FormattingEnabled = true;
            this.comboBoxBrush.Items.AddRange(new object[] {
            "Стандартная",
            "Маленькая 2х2",
            "Средняя 3х3",
            "Большая 5х5",
            "Крест",
            "Линия В.",
            "Линия Г."});
            this.comboBoxBrush.Location = new System.Drawing.Point(18, 917);
            this.comboBoxBrush.Name = "comboBoxBrush";
            this.comboBoxBrush.Size = new System.Drawing.Size(150, 28);
            this.comboBoxBrush.TabIndex = 23;
            this.comboBoxBrush.Text = "Выбор кисти";
            // 
            // comboBoxFigures
            // 
            this.comboBoxFigures.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxFigures.FormattingEnabled = true;
            this.comboBoxFigures.Items.AddRange(new object[] {
            "Glider",
            "Pulsar",
            "Cube",
            "Triangle",
            "Figure 1",
            "LWSS",
            "Улей",
            "Каравай",
            "Блок",
            "Ящик",
            "Пруд",
            "Галактика Кока",
            "Мигалка",
            "Крест",
            "Пентадекатлон"});
            this.comboBoxFigures.Location = new System.Drawing.Point(18, 876);
            this.comboBoxFigures.Name = "comboBoxFigures";
            this.comboBoxFigures.Size = new System.Drawing.Size(150, 28);
            this.comboBoxFigures.TabIndex = 22;
            this.comboBoxFigures.Text = "Выбор фигуры";
            // 
            // checkBoxSort
            // 
            this.checkBoxSort.AutoSize = true;
            this.checkBoxSort.Checked = true;
            this.checkBoxSort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxSort.ForeColor = System.Drawing.Color.Black;
            this.checkBoxSort.Location = new System.Drawing.Point(18, 794);
            this.checkBoxSort.MaximumSize = new System.Drawing.Size(200, 0);
            this.checkBoxSort.Name = "checkBoxSort";
            this.checkBoxSort.Size = new System.Drawing.Size(149, 28);
            this.checkBoxSort.TabIndex = 14;
            this.checkBoxSort.Text = "Сортировать";
            this.checkBoxSort.UseVisualStyleBackColor = true;
            this.checkBoxSort.CheckedChanged += new System.EventHandler(this.checkBoxSort_CheckedChanged);
            // 
            // comboBoxStyle
            // 
            this.comboBoxStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStyle.FormattingEnabled = true;
            this.comboBoxStyle.Items.AddRange(new object[] {
            "Стандартный",
            "Профиль 1",
            "Профиль 2",
            "Профиль 3",
            "Профиль 4",
            "Профиль 5",
            "Профиль 6",
            "Профиль 7",
            "Профиль 8",
            "Профиль 9",
            "Профиль 10",
            "Профиль 11",
            "Профиль 12"});
            this.comboBoxStyle.Location = new System.Drawing.Point(18, 390);
            this.comboBoxStyle.Name = "comboBoxStyle";
            this.comboBoxStyle.Size = new System.Drawing.Size(150, 32);
            this.comboBoxStyle.TabIndex = 21;
            this.comboBoxStyle.Text = "Выбор цвета";
            // 
            // buttonHelp
            // 
            this.buttonHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonHelp.Location = new System.Drawing.Point(18, 958);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(102, 40);
            this.buttonHelp.TabIndex = 20;
            this.buttonHelp.Text = "Помощь";
            this.buttonHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // numericUpDownUpdateTime
            // 
            this.numericUpDownUpdateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownUpdateTime.Location = new System.Drawing.Point(140, 355);
            this.numericUpDownUpdateTime.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDownUpdateTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownUpdateTime.Name = "numericUpDownUpdateTime";
            this.numericUpDownUpdateTime.Size = new System.Drawing.Size(58, 29);
            this.numericUpDownUpdateTime.TabIndex = 19;
            this.numericUpDownUpdateTime.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownUpdateTime.ValueChanged += new System.EventHandler(this.numericUpDownUpdateTime_ValueChanged);
            // 
            // buttonStartEmpty
            // 
            this.buttonStartEmpty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartEmpty.Location = new System.Drawing.Point(91, 182);
            this.buttonStartEmpty.Name = "buttonStartEmpty";
            this.buttonStartEmpty.Size = new System.Drawing.Size(75, 40);
            this.buttonStartEmpty.TabIndex = 17;
            this.buttonStartEmpty.Text = "Пустой";
            this.buttonStartEmpty.UseVisualStyleBackColor = true;
            this.buttonStartEmpty.Click += new System.EventHandler(this.buttonStartEmpty_Click);
            // 
            // checkBoxBorder
            // 
            this.checkBoxBorder.AutoSize = true;
            this.checkBoxBorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxBorder.Location = new System.Drawing.Point(17, 835);
            this.checkBoxBorder.Name = "checkBoxBorder";
            this.checkBoxBorder.Size = new System.Drawing.Size(104, 28);
            this.checkBoxBorder.TabIndex = 16;
            this.checkBoxBorder.Text = "Граница";
            this.checkBoxBorder.UseVisualStyleBackColor = true;
            // 
            // labelS
            // 
            this.labelS.AutoSize = true;
            this.labelS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelS.Location = new System.Drawing.Point(14, 707);
            this.labelS.MaximumSize = new System.Drawing.Size(200, 0);
            this.labelS.Name = "labelS";
            this.labelS.Size = new System.Drawing.Size(188, 48);
            this.labelS.TabIndex = 15;
            this.labelS.Text = "Живет если  рядом есть:";
            // 
            // textBoxS
            // 
            this.textBoxS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxS.Location = new System.Drawing.Point(18, 758);
            this.textBoxS.MaxLength = 9;
            this.textBoxS.Name = "textBoxS";
            this.textBoxS.Size = new System.Drawing.Size(121, 29);
            this.textBoxS.TabIndex = 14;
            this.textBoxS.Text = "23";
            this.textBoxS.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelB.Location = new System.Drawing.Point(9, 619);
            this.labelB.MaximumSize = new System.Drawing.Size(200, 0);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(197, 48);
            this.labelB.TabIndex = 13;
            this.labelB.Text = " Рождается если         рядом есть:";
            // 
            // textBoxB
            // 
            this.textBoxB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxB.Location = new System.Drawing.Point(17, 674);
            this.textBoxB.MaxLength = 9;
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(121, 29);
            this.textBoxB.TabIndex = 12;
            this.textBoxB.Text = "3";
            this.textBoxB.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Enabled = false;
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClear.Location = new System.Drawing.Point(17, 558);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(150, 40);
            this.buttonClear.TabIndex = 11;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonNextStep
            // 
            this.buttonNextStep.Enabled = false;
            this.buttonNextStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNextStep.Location = new System.Drawing.Point(18, 512);
            this.buttonNextStep.Name = "buttonNextStep";
            this.buttonNextStep.Size = new System.Drawing.Size(150, 40);
            this.buttonNextStep.TabIndex = 10;
            this.buttonNextStep.Text = " Далее";
            this.buttonNextStep.UseVisualStyleBackColor = true;
            this.buttonNextStep.Click += new System.EventHandler(this.buttonNextStep_Click);
            // 
            // buttonProceed
            // 
            this.buttonProceed.Enabled = false;
            this.buttonProceed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonProceed.Location = new System.Drawing.Point(17, 274);
            this.buttonProceed.Name = "buttonProceed";
            this.buttonProceed.Size = new System.Drawing.Size(150, 40);
            this.buttonProceed.TabIndex = 9;
            this.buttonProceed.Text = "Продолжить";
            this.buttonProceed.UseVisualStyleBackColor = true;
            this.buttonProceed.Click += new System.EventHandler(this.buttonProceed_Click);
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSpeed.Location = new System.Drawing.Point(12, 328);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(190, 24);
            this.labelSpeed.TabIndex = 7;
            this.labelSpeed.Text = "Время обновления: ";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(17, 355);
            this.trackBar1.Maximum = 400;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(120, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Value = 50;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStop.Location = new System.Drawing.Point(17, 228);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(150, 40);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "Стоп";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.Location = new System.Drawing.Point(17, 182);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 40);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // numericUpDownDensity
            // 
            this.numericUpDownDensity.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownDensity.Location = new System.Drawing.Point(17, 128);
            this.numericUpDownDensity.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDensity.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownDensity.Name = "numericUpDownDensity";
            this.numericUpDownDensity.Size = new System.Drawing.Size(150, 29);
            this.numericUpDownDensity.TabIndex = 3;
            this.numericUpDownDensity.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(11, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Плотность";
            // 
            // numericUpDownSize
            // 
            this.numericUpDownSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownSize.Location = new System.Drawing.Point(17, 46);
            this.numericUpDownSize.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSize.Name = "numericUpDownSize";
            this.numericUpDownSize.Size = new System.Drawing.Size(150, 29);
            this.numericUpDownSize.TabIndex = 1;
            this.numericUpDownSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSize.ValueChanged += new System.EventHandler(this.numericUpDownSize_ValueChanged);
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSize.Location = new System.Drawing.Point(11, 11);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(84, 25);
            this.labelSize.TabIndex = 0;
            this.labelSize.Text = "Размер";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(914, 1053);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1122, 1057);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Game of Life";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.NumericUpDown numericUpDownSize;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.NumericUpDown numericUpDownDensity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Button buttonProceed;
        private System.Windows.Forms.Button buttonNextStep;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.Label labelS;
        private System.Windows.Forms.TextBox textBoxS;
        private System.Windows.Forms.CheckBox checkBoxBorder;
        private System.Windows.Forms.Button buttonStartEmpty;
        private System.Windows.Forms.NumericUpDown numericUpDownUpdateTime;
        private System.Windows.Forms.Button buttonHelp;
        public System.Windows.Forms.CheckBox checkBoxSort;
        private System.Windows.Forms.ComboBox comboBoxFigures;
        private System.Windows.Forms.ComboBox comboBoxBrush;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonOpenImage;
        private System.Windows.Forms.Button buttonCreateNewCell;
        private System.Windows.Forms.ComboBox comboBoxStyle;
        private System.Windows.Forms.ComboBox comboBoxSelectCell;
    }
}

