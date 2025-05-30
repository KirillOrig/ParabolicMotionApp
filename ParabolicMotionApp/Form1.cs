using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BaseFigureMotionApp
{
    public partial class Form1 : Form
    {
        // Таймер движения и обновления состояния анимации
        private Timer movementTimer;

        // Параметры движения по параболе
        private double progress;           // Параметр траектории от 0 до 1
        private int movementDirection;     // 1 – движение вперёд, -1 – обратное движение
        private double movementSpeed;      // Приращение progress за тик (задается пользователем)

        // Параметры вращения
        private double rotationAngle;      // Текущий угол вращения (в радианах)
        private double rotationSpeed;      // Приращение угла (в радианах за тик)

        // Масштабирование фигуры (1.0 – исходный масштаб)
        private double scaleFactor;

        // Параметры траектории (парабола)
        // Используется фиксированный базовый отступ trajMargin; effectiveMargin пересчитывается с учетом масштаба.
        private double trajMargin = 20;
        private double vertexY;   // Y-координата вершины параболы
        private double A;         // Коэффициент параболы
        private double midX;      // Центр области по X

        // Центр фигуры (вычисляется по траектории)
        private PointF figureCenter;

        // Параметр базовой фигуры (измерение "a")
        // По фото: внешний контур звезды – вписан в окружность радиуса 2a, центральный круг – радиуса a.
        private float a = 30; // базовая величина в пикселях

        public Form1()
        {
            InitializeComponent();
            InitializeSimulation();
        }

        // Пересчитывает параметры траектории с учетом текущего масштаба.
        private void RecalculateTrajectoryParameters()
        {
            // effectiveMargin гарантирует, что расстояние от центра фигуры до границ не меньше 2a*scaleFactor.
            double effectiveMargin = Math.Max(trajMargin, 2 * a * scaleFactor);
            double Ybottom = pictureBox1.Height - effectiveMargin;
            midX = pictureBox1.Width / 2.0;
            // Выбираем вершину так, чтобы не касаться верхней границы – примерно на 1/3 расстояния от верха до низа.
            vertexY = effectiveMargin + (Ybottom - effectiveMargin) / 3.0;
            double d = midX - effectiveMargin;
            A = (Ybottom - vertexY) / (d * d);
        }

        private void InitializeSimulation()
        {
            progress = 0.0;
            movementDirection = 1;
            rotationAngle = 0.0;
            // Значения скоростей берутся из TrackBar’ов
            movementSpeed = trackBarSpeed.Value / 1000.0;
            rotationSpeed = trackBarRotation.Value * Math.PI / 180.0;
            scaleFactor = trackBarScale.Value / 100.0; // 100 → масштаб 1.0

            // Пересчитываем параметры траектории с учетом текущего масштаба
            RecalculateTrajectoryParameters();

            // Начальная позиция фигуры — вычисляем по траектории при progress = 0.
            double effectiveMargin = Math.Max(trajMargin, 2 * a * scaleFactor);
            double xPos = effectiveMargin + progress * (pictureBox1.Width - 2 * effectiveMargin);
            double yPos = A * ((xPos - midX) * (xPos - midX)) + vertexY;
            figureCenter = new PointF((float)xPos, (float)yPos);

            // Настройка таймера движения (около 50 тиков в секунду)
            movementTimer = new Timer();
            movementTimer.Interval = 20;
            movementTimer.Tick += MovementTimer_Tick;
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            // Обновляем параметры скоростей (на случай изменений пользователем)
            movementSpeed = trackBarSpeed.Value / 1000.0;
            rotationSpeed = trackBarRotation.Value * Math.PI / 180.0;

            progress += movementSpeed * movementDirection;

            // Если достигнут конец траектории – сразу меняем направление движения и вращения
            if (progress >= 1.0)
            {
                progress = 1.0;
                movementDirection = -1;
            }
            else if (progress <= 0.0)
            {
                progress = 0.0;
                movementDirection = 1;
            }

            // Обновляем угол вращения (направление вращения синхронизировано с направлением движения)
            rotationAngle += rotationSpeed * movementDirection;

            // Пересчитываем параметры траектории с учетом масштаба
            double effectiveMargin = Math.Max(trajMargin, 2 * a * scaleFactor);
            double xPos = effectiveMargin + progress * (pictureBox1.Width - 2 * effectiveMargin);
            double yPos = A * ((xPos - midX) * (xPos - midX)) + vertexY;
            figureCenter = new PointF((float)xPos, (float)yPos);

            pictureBox1.Invalidate();
        }

        // Функция линейной интерполяции цвета по коэффициенту t (0 ≤ t ≤ 1)
        private Color InterpolateColor(Color start, Color end, double t)
        {
            int r = (int)(start.R + (end.R - start.R) * t);
            int g = (int)(start.G + (end.G - start.G) * t);
            int b = (int)(start.B + (end.B - start.B) * t);
            return Color.FromArgb(r, g, b);
        }

        // Вычисление локальных координат точек пятиконечной звезды, центрированной в (0, 0)
        private PointF[] GetStarPoints()
        {
            PointF[] points = new PointF[10];
            double angle = -Math.PI / 2; // начинаем с -90° (верхняя точка)
            double angleStep = Math.PI / 5; // 36° между точками
            double R = 2 * a;         // внешний радиус – 2a
            double r = 0.382 * (2 * a); // внутренний радиус (≈0.764a)
            for (int i = 0; i < 10; i++)
            {
                double radius = (i % 2 == 0) ? R : r;
                float x = (float)(radius * Math.Cos(angle));
                float y = (float)(radius * Math.Sin(angle));
                points[i] = new PointF(x, y);
                angle += angleStep;
            }
            return points;
        }

        // Применяем преобразование к точкам: вращение, масштабирование и перенос (translation) с учётом figureCenter
        private PointF[] TransformPoints(PointF[] pts)
        {
            PointF[] transformed = new PointF[pts.Length];
            float cos = (float)Math.Cos(rotationAngle);
            float sin = (float)Math.Sin(rotationAngle);
            for (int i = 0; i < pts.Length; i++)
            {
                // Вращение
                float xRot = pts[i].X * cos - pts[i].Y * sin;
                float yRot = pts[i].X * sin + pts[i].Y * cos;
                // Масштабирование
                xRot *= (float)scaleFactor;
                yRot *= (float)scaleFactor;
                // Перенос
                transformed[i] = new PointF(xRot + figureCenter.X, yRot + figureCenter.Y);
            }
            return transformed;
        }

        // Отрисовка фигуры: внешняя пятиконечная звезда и центральный круг.
        // Цвет звезды интерполируется от синего (progress = 0) до зелёного (progress = 1),
        // а цвет круга – от жёлтого до синего.
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color starColor = InterpolateColor(Color.Blue, Color.Green, progress);
            Color circleColor = InterpolateColor(Color.Yellow, Color.Blue, progress);

            // Рисуем звезду
            PointF[] starLocal = GetStarPoints();
            PointF[] starTransformed = TransformPoints(starLocal);
            using (SolidBrush brushStar = new SolidBrush(starColor))
            {
                g.FillPolygon(brushStar, starTransformed);
            }

            // Рисуем центральный круг
            float circleRadius = a * (float)scaleFactor; // радиус центрального круга = a * scaleFactor
            RectangleF circleRect = new RectangleF(
                figureCenter.X - circleRadius,
                figureCenter.Y - circleRadius,
                2 * circleRadius,
                2 * circleRadius);
            using (SolidBrush brushCircle = new SolidBrush(circleColor))
            {
                g.FillEllipse(brushCircle, circleRect);
            }
        }

        // Обработчики элементов управления:

        // Кнопка "Запуск" – старт анимации
        private void btnStart_Click(object sender, EventArgs e)
        {
            movementTimer.Start();
        }

        // Кнопка "Стоп" – остановка анимации
        private void btnStop_Click(object sender, EventArgs e)
        {
            movementTimer.Stop();
        }

        // Кнопка "Сброс" – возврат к исходному состоянию
        private void btnReset_Click(object sender, EventArgs e)
        {
            movementTimer.Stop();

            progress = 0.0;
            movementDirection = 1;
            rotationAngle = 0.0;
            scaleFactor = 1.0;
            trackBarScale.Value = 100;

            RecalculateTrajectoryParameters();
            double effectiveMargin = Math.Max(trajMargin, 2 * a * scaleFactor);
            double xPos = effectiveMargin + progress * (pictureBox1.Width - 2 * effectiveMargin);
            double yPos = A * ((xPos - midX) * (xPos - midX)) + vertexY;
            figureCenter = new PointF((float)xPos, (float)yPos);

            pictureBox1.Invalidate();
        }

        // Изменение масштаба через TrackBar: обновляем scaleFactor, пересчитываем траекторию и перерисовываем фигуру
        private void trackBarScale_Scroll(object sender, EventArgs e)
        {
            scaleFactor = trackBarScale.Value / 100.0;
            RecalculateTrajectoryParameters();
            double effectiveMargin = Math.Max(trajMargin, 2 * a * scaleFactor);
            double xPos = effectiveMargin + progress * (pictureBox1.Width - 2 * effectiveMargin);
            double yPos = A * ((xPos - midX) * (xPos - midX)) + vertexY;
            figureCenter = new PointF((float)xPos, (float)yPos);
            pictureBox1.Invalidate();
        }

        // При изменении скорости или вращения значения обновляются в каждом тике таймера
        private void trackBarSpeed_Scroll(object sender, EventArgs e) { }
        private void trackBarRotation_Scroll(object sender, EventArgs e) { }

        // При изменении размеров формы пересчитываем параметры траектории, чтобы фигура оставалась внутри рабочей области
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (pictureBox1 != null)
            {
                RecalculateTrajectoryParameters();
                double effectiveMargin = Math.Max(trajMargin, 2 * a * scaleFactor);
                double xPos = effectiveMargin + progress * (pictureBox1.Width - 2 * effectiveMargin);
                double yPos = A * ((xPos - midX) * (xPos - midX)) + vertexY;
                figureCenter = new PointF((float)xPos, (float)yPos);
                pictureBox1.Invalidate();
            }
        }
    }
}
