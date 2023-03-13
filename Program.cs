using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//Realizado por: Mariana Alejandra Pimienta Hernandez
public class Program : MonoBehaviour
{
    Texture2D tex;
    float Height = 80, Width = 80;

    void Start()
    {
        Color BackColor = new Color(0,0,0,1);
        Setup (Height, Width, BackColor);
        Color Line1 = new Color(0,1,0,1);
   
        float l1 = Width / 8;
        float l2 = Width / 4;

        Circunferencia(Height / 2.0f, Width / 2.0f, l1, Line1);
        Parabola(Height / 2.0f, Width / 2.0f, l2, Line1);
        Elipse(Height / 2.0f, (Width - 45) / 2.0f, l2, l1, Line1);

        tex.Apply();
       
    }

    void Setup(float H, float W, Color Back){
        
        tex = new Texture2D((int)H, (int)W);
        tex.filterMode = FilterMode.Point;
        
        GetComponent<Renderer>().material.mainTexture = tex;
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                tex.SetPixel(x, y, Back); 
            }
        }
    }

    void ABasico(float x0, float y0, float x1, float y1, Color Line){

        float m = (y1 - y0) / (x1 - x0);
        float b = y0 - m * x0;

        float yb = y0;
        if (x0 == x1)
        {
            if (y0 < y1){
            for (int y = (int)y0; y < y1; y++)
            {
                tex.SetPixel((int)x0, y, Line);
            }
            }else{
            for (int y = (int)y1; y < y0; y++)
            {
                tex.SetPixel((int)x0, y, Line);
            }
            }
        }
        else
        {
            if (x1 > x0){
            for (int x = (int)x0; x < x1; x++)
            {
                float y = m * x + b;
                if (Mathf.Abs(m) >= 1 && x > x0){
                    ABasico(x, yb ,x ,y, Line);
                }
                tex.SetPixel(x, (int)y, Line);
                yb = y;
            }
            }else{
            for (int x = (int)x1; x < x0; x++)
            {
                float y = m * x + b;
                if (Mathf.Abs(m) >= 1 && x > x1){
                    ABasico(x, yb, x, y, Line);
                }
                tex.SetPixel(x, (int)y, Line);
                yb = y;
            }
            }
        }
        
    }

    void ADDA(float x0, float y0, float x1, float y1, Color Line){

        if (x0 == x1)
        {
            if (y0 < y1){
            for (int y = (int)y0; y < y1; y++)
            {
                tex.SetPixel((int)x0, y, Line);
            }
            }else{
            for (int y = (int)y1; y < y0; y++)
            {
                tex.SetPixel((int)x0, y, Line);
            }
            }
        }
        else
        {
            float Dx = (x1-x0);
            float Dy = (y1-y0);

            float m = (y1 - y0) / (x1 - x0);

            float k;
            if(Dx > Dy){ k = Dx;}
            else {k = Dy;}

            float y = y0;
            float x = x0;

            if (m <= 1){

            for (int i = 0; i <= k; i++)
            {
                y += m;
                x += 1;
                tex.SetPixel((int)x, (int)y, Line);
            }
            }else{
            for (int i = 0; i <= k; i++)
            {
                x += (1/m);
                y += 1;
                tex.SetPixel((int)x, (int)y, Line);
            }
            }
        }
        
    }

    void Abr(float x0, float y0, float x1, float y1, Color Line){   
        float w = x1-x0;
        float h = y1-y0;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1;else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1;else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1;else if (w > 0) dx2 = 1;
        int longest = Mathf.Abs((int)w);
        int shortest = Mathf.Abs((int)h);
        if (!(longest > shortest))
        {   
            longest = Mathf.Abs((int)h);
            shortest = Mathf.Abs((int)w);
            if (h < 0) dy2 = -1;else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        float x = x0;
        float y = y0;
        for (int i = 0; i <= longest; i++)
        {
            tex.SetPixel((int)x, (int)y, Line);
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }else{
                x += dx2;
                y += dy2;
            }
        }
        
    }

    void Circunferencia(float x0, float y0, float r, Color Line){

        float yb = 0, yc = 0;

        for(int x = 0; x <= r ; x++){
            
            float y = Mathf.Sqrt(Mathf.Pow(r,2) - Mathf.Pow(x,2));

            tex.SetPixel((int)(x0 + x), (int)(y0 + y), Line);

            tex.SetPixel((int)(x0 - x), (int)(y0 + y), Line);

            tex.SetPixel((int)(x0 + x), (int)(y0 - y), Line);

            tex.SetPixel((int)(x0 - x), (int)(y0 - y), Line);

            yc = y - yb;

            if(Mathf.Abs(yc) > 1 && x > 0){

                Abr((x0 + x), yb + y0, (x0 + x), y + y0, Line);

                Abr((x0 - x), yb + y0, (x0 - x), y + y0, Line);

                Abr((x0 + x), y0 - yb, (x0 + x), y0 - y, Line);

                Abr((x0 - x), y0 - yb, (x0 - x), y0 - y, Line);

            }

            yb = y; 
        }
    }

    void Elipse(float x0, float y0, float rx, float ry, Color Line){

        float xb = 0, yb = 0;
        for(int x = 0; x < 360; x++){
            xb = x0 + (rx * Mathf.Cos(x * Mathf.Deg2Rad));
            yb = y0 + (ry * Mathf.Sin(x * Mathf.Deg2Rad));

            tex.SetPixel((int)(xb), (int)(yb), Line);
        }

    }

    void Parabola(float x0, float y0, float r, Color Line){

        float yb = 0, yc = 0;

        for(int x = 0; x <= r ; x++){
            
            float y = Mathf.Sqrt(Mathf.Pow(r,2) - Mathf.Pow(x,2));

            tex.SetPixel((int)(x0 + x), (int)(y0 + y), Line);

            tex.SetPixel((int)(x0 - x), (int)(y0 + y), Line);

            yc = y - yb;

            if(Mathf.Abs(yc) > 1 && x > 0){

                Abr((x0 + x), yb + y0, (x0 + x), y + y0, Line);

                Abr((x0 - x), yb + y0, (x0 - x), y + y0, Line);

            }

            yb = y; 
        }
    }
}