using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Example;
using TgcViewer;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;
using TgcViewer.Utils.Modifiers;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Input;
using Microsoft.DirectX.DirectInput;

namespace Examples.Tutorial
{
    /// <summary>
    /// </summary>
    public class Brazo1 : TgcExample
    {
        private const float VELOCIDAD_ANGULAR = 1.1f;
        TgcBox box;

        // medidas fijas
        float base_dx = 1.0f;
        float base_dy = 0.5f;
        float base_dz = 1.0f;
        float brazo_dx = 0.5f;
        float brazo_dy = 1f;
        float brazo_dz = 0.5f;
        float ante_brazo_dx = 0.75f;
        float ante_brazo_dy = 1.5f;
        float ante_brazo_dz = 0.75f;
        float mano_dx = 0.15f;
        float mano_dy = 0.5f;
        float mano_dz = 0.15f;
        float ang_pinza = 0.0f;
        float traslacion_pinza = 0.0f;
        float ang_brazo = 0.0f;
        float ang_ante_brazo = 0.0f;
        float ang_base = 0.0f;

        public override string getCategory()
        {
            return "Transformations";
        }

        public override string getName()
        {
            return "Brazo 1";
        }

        public override string getDescription()
        {
            return "Ejemplo brazo de robot. ARR/ABA->ang brazo, Der/Izq->ang antebrazo, A/S -> ang pinza";
        }


        public override void init()
        {
            Microsoft.DirectX.Direct3D.Device d3dDevice = GuiController.Instance.D3dDevice;
            TgcTexture texture = TgcTexture.createTexture(GuiController.Instance.ExamplesMediaDir + "MeshCreator\\Textures\\Metal\\floor1.jpg");
            Vector3 center = new Vector3(0, 0, 0);
            Vector3 size = new Vector3(1f, 1f, 1f);
            box = TgcBox.fromSize(center, size, texture);
            box.AutoTransformEnable = false;
            box.Transform = Matrix.Identity;
            GuiController.Instance.RotCamera.targetObject(box.BoundingBox);
            GuiController.Instance.RotCamera.CameraCenter = GuiController.Instance.RotCamera.CameraCenter + new Vector3(0f, 1.5f, 0f);
            GuiController.Instance.RotCamera.CameraDistance = GuiController.Instance.RotCamera.CameraDistance+ 2.5f;
        }

        public override void render(float elapsedTime)
        {
            Microsoft.DirectX.Direct3D.Device d3dDevice = GuiController.Instance.D3dDevice;
            box.AutoTransformEnable = false;

            TgcD3dInput input = GuiController.Instance.D3dInput;
            if (input.keyDown(Key.A))
            {
                ang_pinza += VELOCIDAD_ANGULAR * elapsedTime;
            }
            else if (input.keyDown(Key.S))
            {
                ang_pinza -= VELOCIDAD_ANGULAR * elapsedTime;
            }
            if (input.keyDown(Key.Q))
            {
                traslacion_pinza += VELOCIDAD_ANGULAR * elapsedTime;
            }
            else if (input.keyDown(Key.W))
            {
                traslacion_pinza -= VELOCIDAD_ANGULAR * elapsedTime;
            }

            if (input.keyDown(Key.Left))
            {
                ang_ante_brazo += VELOCIDAD_ANGULAR * elapsedTime;
            }
            else if (input.keyDown(Key.Right))
            {
                ang_ante_brazo -= VELOCIDAD_ANGULAR * elapsedTime;
            }

            if (input.keyDown(Key.Up))
            {
                ang_brazo += VELOCIDAD_ANGULAR * elapsedTime;
            }
            else if (input.keyDown(Key.Down))
            {
                ang_brazo -= VELOCIDAD_ANGULAR * elapsedTime;
            }


            // 1- Base del brazo
            // ajusto a la medida fija 
            Matrix Esc = Matrix.Scaling(base_dx, base_dy, base_dz);
            box.Transform = Esc;
            box.render();

            // 2- Brazo 
            // ajusto a la medida fija
            Esc = Matrix.Scaling(brazo_dx, brazo_dy, brazo_dz);
            // y lo traslado un poco para arriba, para que quede ubicado arriba de la base
            Matrix T = Matrix.Translation(0, brazo_dy / 2.0f + base_dy / 2.0f, 0);
            // Guardo el punto donde tiene que girar el brazo = en la parte de abajo del brazo
            // le aplico la misma transformacion que al brazo (sin tener en cuenta el escalado)
            Vector3 pivote_brazo = Vector3.TransformCoordinate(new Vector3(0, -brazo_dy / 2.0f, 0.0f), T);
            // Ahora giro el brazo sobre el pivote, para ello, primero traslado el centro del mesh al pivote,
            // ahi aplico la rotacion, y luego vuelvo a trasladar a la posicion original
            Matrix Rot = Matrix.RotationZ(ang_brazo);
            Matrix A = Matrix.Translation(-pivote_brazo.X, -pivote_brazo.Y, -pivote_brazo.Z);
            Matrix B = Matrix.Translation(pivote_brazo.X, pivote_brazo.Y, pivote_brazo.Z);
            Matrix Q1 = T * A * Rot * B;
            box.Transform = Esc * Q1;
            box.render();

            // 3- ante brazo
            // ajusto a la medida fija 
            Esc = Matrix.Scaling(ante_brazo_dx, ante_brazo_dy, ante_brazo_dz);
            T = Matrix.Translation(0, brazo_dy / 2 + ante_brazo_dy / 2.0f, 0) * Q1;
            // Guardo el punto donde tiene que girar el antebrazo
            Vector3 pivote_ante_brazo = Vector3.TransformCoordinate(new Vector3(0, -ante_brazo_dy / 2.0f, 0.0f), T);
            // orientacion del antebrazo
            Rot = Matrix.RotationZ(ang_ante_brazo);
            A = Matrix.Translation(-pivote_ante_brazo.X, -pivote_ante_brazo.Y, -pivote_ante_brazo.Z);
            B = Matrix.Translation(pivote_ante_brazo.X, pivote_ante_brazo.Y, pivote_ante_brazo.Z);
            Matrix Q2 = T * A * Rot * B;
            box.Transform = Esc * Q2;
            box.render();

            // 4- pinza: mano izquierda
            Esc = Matrix.Scaling(mano_dx, mano_dy, mano_dz);
            T = Matrix.Translation(mano_dx / 2 - ante_brazo_dx / 2, ante_brazo_dy / 2.0f + mano_dy / 2.0f, 0) * Q2;
            // Guardo el punto donde tiene que girar la pinza
            Vector3 pivote_pinza_izq = Vector3.TransformCoordinate(new Vector3(0, -mano_dy / 2.0f, 0.0f), T);
            // orientacion de la pinza
            Rot = Matrix.RotationZ(ang_pinza);
            A = Matrix.Translation(-pivote_pinza_izq.X, -pivote_pinza_izq.Y, -pivote_pinza_izq.Z);
            B = Matrix.Translation(pivote_pinza_izq.X, pivote_pinza_izq.Y, pivote_pinza_izq.Z);
            Matrix C = Matrix.Translation(traslacion_pinza, 0f, 0f);
            Matrix Q3 = C * T * A * Rot * B;
            box.Transform = Esc * Q3;
            box.render();

            // mano derecha
            Esc = Matrix.Scaling(mano_dx, mano_dy, mano_dz);
            box.Transform = Esc;
            T = Matrix.Translation(ante_brazo_dx / 2 - mano_dx / 2, ante_brazo_dy / 2 + mano_dy / 2.0f, 0) * Q2;
            // Guardo el punto donde tiene que girar la pinza
            Vector3 pivote_pinza_der = Vector3.TransformCoordinate(new Vector3(0, -mano_dy / 2.0f, 0.0f), T);
            // orientacion de la pinza
            Rot = Matrix.RotationZ(-ang_pinza);
            A = Matrix.Translation(-pivote_pinza_der.X, -pivote_pinza_der.Y, -pivote_pinza_der.Z);
            B = Matrix.Translation(pivote_pinza_der.X, pivote_pinza_der.Y, pivote_pinza_der.Z);
            C = Matrix.Translation(-traslacion_pinza, 0f, 0f);
            Matrix Q4 = C * T * A * Rot * B;
            box.Transform = Esc * Q4;
            box.render();

        }

        public override void close()
        {
            box.dispose();
        }

    }
}

