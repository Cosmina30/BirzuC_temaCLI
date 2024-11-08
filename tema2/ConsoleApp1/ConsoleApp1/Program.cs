using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace ConsoleApp1
{
    class SimpleWindow : GameWindow
    {
        private float xPosition = 0.0f;   // Horizontal position of the object
        private float yPosition = 0.0f;   // Vertical position of the object
        private float moveSpeed = 0.05f;  // Movement speed for key controls

        // Constructor
        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
            MouseMove += Mouse_MouseMove;
        }

        // Handles key press events for controlling object horizontally
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Exit();
            }

            if (e.Key == Key.F11)
            {
                this.WindowState = this.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
            }

            // Move left with "A" key
            if (e.Key == Key.A)
            {
                xPosition -= moveSpeed;
            }

            // Move right with "D" key
            if (e.Key == Key.D)
            {
                xPosition += moveSpeed;
            }
        }

        // Handles mouse movement events for controlling object vertically
        void Mouse_MouseMove(object sender, MouseMoveEventArgs e)
        {
            // Map the mouse Y position to the object's Y position, scaled to window size
            yPosition = 2.0f * (e.Y - (Height / 2)) / Height;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Game logic can be updated here if needed
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Render a triangle with position controlled by keys and mouse
            GL.Begin(PrimitiveType.Triangles);

            // Apply current positions for the triangle vertices
            GL.Color3(Color.MidnightBlue);
            GL.Vertex2(xPosition - 0.1f, yPosition + 0.1f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex2(xPosition, yPosition - 0.1f);
            GL.Color3(Color.Ivory);
            GL.Vertex2(xPosition + 0.1f, yPosition + 0.1f);

            GL.End();

            this.SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
