using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SGUI
{ 
    #region methods
    #region Others
    /// <summary>
    /// Change default GUI Content color for any GUI graphic
    /// </summary>
    /// <param name="color">Color to set</param>
    public static void ChangeGUIColor(Color color) { GUI.contentColor = color; }

    /// <summary>
    /// Generate a plain 1x1 texture with the parameter color.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Texture2D ColorTexture (Color color)
    {
        Texture2D plane = new Texture2D(1, 1);
        plane.SetPixel(1, 1, color);
        plane.Apply();
        return plane;
    }

    /// <summary>
    /// Get the Screen percentages calculated through the screen pixels
    /// </summary>
    /// <param name="x">pixels x position</param>
    /// <param name="y">pixels y position</param>
    /// <param name="height">pixels height</param>
    /// <param name="width">pixels width</param>
    /// <returns></returns>
    public static Rect PercentagesRect (int x, int y, int width, int height)
    {
        return new Rect(x / Screen.width * 100f, y / Screen.height * 100f, width / Screen.width * 100f, height / Screen.height * 100f);
    }

    /// <summary>
    /// Turn a Vector2 of pixels into a Vector2 of percentages.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public static Vector2 PointPercentages (Vector2 point)
    {
        return new Vector2(point.x / Screen.width * 100f, point.y / Screen.height * 100f);
    }

    /// <summary>
    /// Get the Screen Pixels calculated through the screen percentages
    /// </summary>
    /// <param name="x">Percentage x position</param>
    /// <param name="y">Percentage y position</param>
    /// <param name="height">Percentage height</param>
    /// <param name="width">Percentage width</param>
    /// <returns></returns>
    public static Rect PixelsRect(int px, int py, int pwidth, int pheight)
    {
        int xx, yy, ww, hh;

        if (pwidth > 0)
        {
            xx = (int)(px * Screen.width / 100f);
            ww = (int)(pwidth * Screen.width / 100f);
        }
        else
        {
            xx = (int)((px + pwidth) * Screen.width / 100f);
            ww = (int)(-pwidth * Screen.width / 100f);
        }

        if (pheight > 0)
        {
            yy = (int)(py * Screen.height / 100f);
            hh = (int)(pheight * Screen.height / 100f);
        }
        else
        {
            yy = (int)((py + pheight) * Screen.height / 100f);
            hh = (int)(-pheight * Screen.height / 100f);
        }


        return new Rect(xx, yy, ww, hh);
    }

    /// <summary>
    /// Calculates an int font size number based on the Screen Size
    /// </summary>
    /// <param name="percentage"></param>
    /// <returns></returns>
    public static int FontRelativeToScreen (float percentage)
    {
        float mean = (Screen.width + Screen.height) / 2;
        return (int)(mean * percentage / 100f);
    }

    /// <summary>
    /// Is a given pixel (x,y) inside of these percentages?
    /// </summary>
    /// <param name="point">Point 2D (pixels x,y)</param>
    /// <param name="percentages">Rect percentages (x,y,width,height)</param>
    /// <returns></returns>
    public static bool PixelInPercentages(Vector2 point, Rect percentages)
    {
        Vector2 pointP = new Vector2(point.x / Screen.width * 100f, 100 - (point.y / Screen.height * 100f));

        if (percentages.width > 0)
        {
            if (pointP.x < percentages.x) return false;
            if (pointP.x > percentages.x + percentages.width) return false;
        }
        else
        {
            if (pointP.x > percentages.x) return false;
            if (pointP.x < percentages.x + percentages.width) return false;
        }
        if (percentages.height > 0)
        {
            if (pointP.y < percentages.y) return false;
            if (pointP.y > percentages.y + percentages.height) return false;
        }
        else
        {
            if (pointP.y > percentages.y) return false;
            if (pointP.y < percentages.y + percentages.height) return false;
        }

        return true;
    }
    #endregion
    //-------------------------
    #region DrawRectangle
    /// <summary>
    /// Draw a full-screen rectangle with a plain color
    /// </summary>
    /// <param name="texture"></param>
    public static void DrawRectangle(Color color)
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), ColorTexture(color));
    }
    
    /// <summary>
    /// Draw a rectangle painted with a color within the given percentages of screen.
    /// </summary>
    /// <param name="Percentages"></param>
    /// <param name="color"></param>
    public static void DrawRectangle(Rect Percentages, Color color)
    {
        GUI.DrawTexture(new Rect(Percentages.x * Screen.width / 100f, Percentages.y * Screen.height / 100f, Percentages.width * Screen.width / 100f, Percentages.height * Screen.height / 100f), ColorTexture(color));
    }

    /// <summary>
    /// Draw a rectangle painted with a color within the given percentages of screen.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="texture"></param>
    public static void DrawRectangle(float x, float y, float width, float height, Color color)
    {
        GUI.DrawTexture(new Rect(x * Screen.width / 100f, y * Screen.height / 100f, width * Screen.width / 100f, height * Screen.height / 100f), ColorTexture(color));
    }
    
    /// <summary>
    /// Draw a rectangle with a plain color fullfilling an SLayer
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="texture"></param>
    public static void DrawRectangle(SLayer layer, Color color)
    {
        GUI.DrawTexture(new Rect(layer.XPerCent * Screen.width / 100f, layer.YPerCent * Screen.height / 100f, layer.WidthPerCent * Screen.width / 100f, layer.HeightPerCent * Screen.height / 100f), ColorTexture(color));
    }

    /// <summary>
    /// Draw a Rectangle with a plain Color within the percentages of a SLayer
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Percentages"></param>
    /// <param name="texture"></param>
    public static void DrawRectangle(SLayer layer, Rect Percentages, Color color)
    {
        float xx = layer.XPerCent + Percentages.x * layer.WidthPerCent / 100f;
        float yy = layer.YPerCent + Percentages.y * layer.HeightPerCent / 100f;
        float wwidth = Percentages.width * layer.WidthPerCent / 100f;
        float hheight = Percentages.height * layer.HeightPerCent / 100f;
        GUI.DrawTexture(new Rect(xx * Screen.width / 100f, yy * Screen.height / 100f, wwidth * Screen.width / 100f, hheight * Screen.height / 100f), ColorTexture(color));
    }

    /// <summary>
    /// Draw a Rectangle with a plain Color within the percentages of a SLayer
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="texture"></param>
    public static void DrawRectangle(SLayer layer, float x, float y, float width, float height, Color color)
    {
        float xx = layer.XPerCent + x * layer.WidthPerCent / 100f;
        float yy = layer.YPerCent + y * layer.HeightPerCent / 100f;
        float wwidth = width * layer.WidthPerCent / 100f;
        float hheight = height * layer.HeightPerCent / 100f;
        GUI.DrawTexture(new Rect(xx * Screen.width / 100f, yy * Screen.height / 100f, wwidth * Screen.width / 100f, hheight * Screen.height / 100f), ColorTexture(color));
    }
    #endregion
    //-------------------------
    #region Draw Texture
    /// <summary>
    /// Draw a full-screen texture.
    /// </summary>
    /// <param name="texture"></param>
    public static void DrawTexture(Texture2D texture)
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
	}

    /// <summary>
    /// Draw a texture within the screen percentages (position x, y, size w, h)
    /// </summary>
    /// <param name="Percentages"></param>
    /// <param name="texture"></param>
    public static void DrawTexture(Rect Percentages, Texture2D texture)
    {
        GUI.DrawTexture(new Rect(Percentages.x * Screen.width / 100f, Percentages.y * Screen.height / 100f, Percentages.width * Screen.width / 100f, Percentages.height * Screen.height / 100f), texture);
    }
    
    /// <summary>
    /// Draw a texture within the screen percentages (position x, y, size w, h)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="texture"></param>
    public static void DrawTexture(float x, float y, float width, float height, Texture2D texture)
    {
        GUI.DrawTexture(new Rect(x * Screen.width / 100f, y * Screen.height / 100f, width * Screen.width / 100f, height * Screen.height / 100f), texture);
    }
    
    /// <summary>
    /// Draw a texture fullfilling an SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="texture"></param>
    public static void DrawTexture(SLayer layer, Texture2D texture)
    {
		GUI.DrawTexture(new Rect(layer.XPerCent * Screen.width / 100f, layer.YPerCent * Screen.height / 100f, layer.WidthPerCent * Screen.width / 100f, layer.HeightPerCent * Screen.height / 100f), texture);
    }

    /// <summary>
    /// Draw a texture within the percentages of a SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Percentages"></param>
    /// <param name="texture"></param>
    public static void DrawTexture(SLayer layer, Rect Percentages, Texture2D texture)
    {
        float xx = layer.XPerCent + Percentages.x * layer.WidthPerCent / 100f;
        float yy = layer.YPerCent + Percentages.y * layer.HeightPerCent / 100f;
        float wwidth = Percentages.width * layer.WidthPerCent / 100f;
        float hheight = Percentages.height * layer.HeightPerCent / 100f;
        GUI.DrawTexture(new Rect(xx * Screen.width / 100f, yy * Screen.height / 100f, wwidth * Screen.width / 100f, hheight * Screen.height / 100f), texture);
    }

    /// <summary>
    /// Draw a texture within the percentages of a SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="texture"></param>
    public static void DrawTexture(SLayer layer, float x, float y, float width, float height, Texture2D texture)
    {
		float xx = layer.XPerCent + x * layer.WidthPerCent / 100f;
		float yy = layer.YPerCent + y * layer.HeightPerCent / 100f;
		float wwidth = width * layer.WidthPerCent / 100f;
		float hheight = height * layer.HeightPerCent / 100f;
        GUI.DrawTexture(new Rect(xx * Screen.width / 100f, yy * Screen.height / 100f, wwidth * Screen.width / 100f, hheight * Screen.height / 100f), texture);
    }
    #endregion
    //-------------------------
    #region Draw Texture Tileset
    /// <summary>
    /// Fill the screen with the same texture repeated.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="rows">repeitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    public static void DrawTileset(Texture2D texture, int rows, int columns)
	{
		float widthSpace = Screen.width / columns;
		float heightSpace = Screen.height / rows;
		for (int currentRow = 0; currentRow < rows; ++currentRow)
		{
			for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
				GUI.DrawTexture (new Rect (currentColumn * widthSpace, currentRow * heightSpace, widthSpace, heightSpace), texture);
		}
	}

    /// <summary>
    /// Fill the specified area with a repeated texture.
    /// </summary>
    /// <param name="Percentages">Area to contain the tileset</param>
    /// <param name="texture"></param>
    /// <param name="rows">repeitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    public static void DrawTileset(Rect Percentages, Texture2D texture, int rows, int columns)
	{
		float initialPosX = Screen.width * Percentages.x / 100f;
		float initialPosY = Screen.height * Percentages.y / 100f;
		float widthSpace = (Screen.width*Percentages.width/100f) / columns;
		float heightSpace = (Screen.height*Percentages.height/100f) / rows;

		for (int currentRow = 0; currentRow < rows; ++currentRow)
		{
			for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
				GUI.DrawTexture (new Rect (initialPosX + currentColumn * widthSpace, initialPosY + currentRow * heightSpace, widthSpace, heightSpace), texture);
		}
	}

    /// <summary>
    /// Fill the specified area with a repeated texture
    /// </summary>
    /// <param name="x">percentage X of Screen Width</param>
    /// <param name="y">percentage Y of Screen Height</param>
    /// <param name="width">percentage W of Screen Width</param>
    /// <param name="height">percentage H of Screen Height</param>
    /// <param name="texture"></param>
    /// <param name="rows">repeitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    public static void DrawTileset(float x, float y, float width, float height, Texture2D texture, int rows, int columns)
	{
		float initialPosX = Screen.width * x / 100f;
		float initialPosY = Screen.height * y / 100f;
		float widthSpace = (Screen.width*width/100f) / columns;
		float heightSpace = (Screen.height*height/100f) / rows;
		
		for (int currentRow = 0; currentRow < rows; ++currentRow)
		{
			for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
				GUI.DrawTexture (new Rect (initialPosX + currentColumn * widthSpace, initialPosY + currentRow * heightSpace, widthSpace, heightSpace), texture);
		}
	}

    /// <summary>
    /// Fill an SLayer with a repeated texture
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="texture"></param>
    /// <param name="rows">repeitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    public static void DrawTileset(SLayer layer, Texture2D texture, int rows, int columns)
	{
		float initialPosX = Screen.width * layer.XPerCent / 100f;
		float initialPosY = Screen.height * layer.YPerCent / 100f;
		float widthSpace = (Screen.width*layer.WidthPerCent/100f) / columns;
		float heightSpace = (Screen.height*layer.HeightPerCent/100f) / rows;
		
		for (int currentRow = 0; currentRow < rows; ++currentRow)
		{
			for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
				GUI.DrawTexture (new Rect (initialPosX + currentColumn * widthSpace, initialPosY + currentRow * heightSpace, widthSpace, heightSpace), texture);
		}
	}

    /// <summary>
    /// Fill an area inside a SLayer with a repeated texture.
	/// </summary>
	/// <param name="layer"></param>
	/// <param name="Percentages"></param>
	/// <param name="texture"></param>
    /// <param name="rows">repeitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    public static void DrawTileset(SLayer layer, Rect Percentages, Texture2D texture, int rows, int columns)
    {
        float xx = layer.XPerCent + Percentages.x * layer.WidthPerCent / 100f;
        float yy = layer.YPerCent + Percentages.y * layer.HeightPerCent / 100f;
        float wwidth = Percentages.width * layer.WidthPerCent / 100f;
        float hheight = Percentages.height * layer.HeightPerCent / 100f;

        float initialPosX = Screen.width * xx / 100f;
        float initialPosY = Screen.height * yy / 100f;
        float widthSpace = (Screen.width * wwidth / 100f) / columns;
        float heightSpace = (Screen.height * hheight / 100f) / rows;

        for (int currentRow = 0; currentRow < rows; ++currentRow)
        {
            for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
                GUI.DrawTexture(new Rect(initialPosX + currentColumn * widthSpace, initialPosY + currentRow * heightSpace, widthSpace, heightSpace), texture);
        }
    }

    /// <summary>
    /// Fill an area inside an SLayer with a repeated texture.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="texture"></param>
    /// <param name="rows">repeitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    public static void DrawTileset(SLayer layer, float x, float y, float width, float height, Texture2D texture, int rows, int columns)
	{
		float xx = layer.XPerCent + x * layer.WidthPerCent / 100f;
		float yy = layer.YPerCent + y * layer.HeightPerCent / 100f;
		float wwidth = width * layer.WidthPerCent / 100f;
		float hheight = height * layer.HeightPerCent / 100f;

		float initialPosX = Screen.width * xx / 100f;
		float initialPosY = Screen.height * yy / 100f;
		float widthSpace = (Screen.width*wwidth/100f) / columns;
		float heightSpace = (Screen.height*hheight/100f) / rows;
		
		for (int currentRow = 0; currentRow < rows; ++currentRow)
		{
			for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
				GUI.DrawTexture (new Rect (initialPosX + currentColumn * widthSpace, initialPosY + currentRow * heightSpace, widthSpace, heightSpace), texture);
		}
	}

    // WITH SPACING
    /// <summary>
    /// Fill the screen with a repeated texture.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="rows">repetitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    /// <param name="spaceX">Percentage of the area used for horizontal spacing</param>
    /// <param name="spaceY">Percentage of the area used for horizontal spacing</param>
    public static void DrawTileset(Texture2D texture, int rows, int columns, float spaceX, float spaceY)
    {
        float SpaceX = spaceX * Screen.width / 100f;
        float widthSpace = (Screen.width - SpaceX) / columns;
        SpaceX /= (columns-1);

        float SpaceY = spaceY * Screen.height / 100f;
        float heightSpace = (Screen.height - SpaceY) / rows;
        SpaceY /= (rows-1);
        
        for (int currentRow = 0; currentRow < rows; ++currentRow)
        {
            for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
                GUI.DrawTexture(new Rect(currentColumn * (widthSpace + SpaceX), currentRow * (heightSpace + SpaceY), widthSpace, heightSpace), texture);
        }
    }

    /// <summary>
    /// Fill an area of the screen with a repepated texture
    /// </summary>
    /// <param name="Percentages">Area of the screen</param>
    /// <param name="texture"></param>
    /// <param name="rows">repetitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    /// <param name="spaceX">Percentage of the area used for horizontal spacing</param>
    /// <param name="spaceY">Percentage of the area used for horizontal spacing</param>
    public static void DrawTileset(Rect Percentages, Texture2D texture, int rows, int columns, float spaceX, float spaceY)
    {
        float initialPosX = Screen.width * Percentages.x / 100f;
        float initialPosY = Screen.height * Percentages.y / 100f;

        float SizeX = Percentages.width * Screen.width / 100f;
        float SizeY = Percentages.height * Screen.height / 100f;

        float SpaceX = spaceX * SizeX / 100f;
        float widthSpace = (SizeX - SpaceX) / columns;
        SpaceX /= (columns - 1);

        float SpaceY = spaceY * SizeY / 100f;
        float heightSpace = (SizeY - SpaceY) / rows;
        SpaceY /= (rows - 1);

        for (int currentRow = 0; currentRow < rows; ++currentRow)
        {
            for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
                GUI.DrawTexture(new Rect(initialPosX + currentColumn * (widthSpace + SpaceX), initialPosY + currentRow * (heightSpace + SpaceY), widthSpace, heightSpace), texture);
        }

    }

    /// <summary>
    /// Fill an area of the screen with a repepated texture
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="texture"></param>
    /// <param name="rows">repetitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    /// <param name="spaceX">Percentage of the area used for horizontal spacing</param>
    /// <param name="spaceY">Percentage of the area used for horizontal spacing</param>
    public static void DrawTileset(float x, float y, float width, float height, Texture2D texture, int rows, int columns, float spaceX, float spaceY)
    {
        float initialPosX = Screen.width * x / 100f;
        float initialPosY = Screen.height * y / 100f;

        float SizeX = width * Screen.width / 100f;
        float SizeY = height * Screen.height / 100f;

        float SpaceX = spaceX * SizeX / 100f;
        float widthSpace = (SizeX - SpaceX) / columns;
        SpaceX /= (columns - 1);

        float SpaceY = spaceY * SizeY / 100f;
        float heightSpace = (SizeY - SpaceY) / rows;
        SpaceY /= (rows - 1);

        for (int currentRow = 0; currentRow < rows; ++currentRow)
        {
            for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
                GUI.DrawTexture(new Rect(initialPosX + currentColumn * (widthSpace + SpaceX), initialPosY + currentRow * (heightSpace + SpaceY), widthSpace, heightSpace), texture);
        }
    }

    /// <summary>
    /// Fill an SLayer with a repeated texture
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="texture"></param>
    /// <param name="rows">repetitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    /// <param name="spaceX">Percentage of the area used for horizontal spacing</param>
    /// <param name="spaceY">Percentage of the area used for horizontal spacing</param>
    public static void DrawTileset(SLayer layer, Texture2D texture, int rows, int columns, float spaceX, float spaceY)
    {

        float initialPosX = layer.Position.x;
        float initialPosY = layer.Position.y;

        float SizeX = layer.Size.x;
        float SizeY = layer.Size.y;

        float SpaceX = spaceX * Screen.width / 100f;
        float widthSpace = (SizeX - SpaceX) / columns;
        SpaceX /= (columns - 1);

        float SpaceY = spaceY * Screen.height / 100f;
        float heightSpace = (SizeY - SpaceY) / rows;
        SpaceY /= (rows - 1);

        for (int currentRow = 0; currentRow < rows; ++currentRow)
        {
            for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
                GUI.DrawTexture(new Rect(initialPosX + currentColumn * (widthSpace + SpaceX), initialPosY + currentRow * (heightSpace + SpaceY), widthSpace, heightSpace), texture);
        }
    }

    /// <summary>
    /// Fill an area inside of an SLayer with a repeated texture.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Percentages"></param>
    /// <param name="texture"></param>
    /// <param name="rows">repetitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    /// <param name="spaceX">Percentage of the area used for horizontal spacing</param>
    /// <param name="spaceY">Percentage of the area used for horizontal spacing</param>
    public static void DrawTileset(SLayer layer, Rect Percentages, Texture2D texture, int rows, int columns, float spaceX, float spaceY)
    {
        float initialPosX = layer.Position.x + layer.Size.x * Percentages.x / 100f;
        float initialPosY = layer.Position.y + layer.Size.y * Percentages.y / 100f;

        float SizeX = Percentages.width * layer.Size.x / 100f;
        float SizeY = Percentages.height * layer.Size.y / 100f;

        float SpaceX = spaceX * SizeX / 100f;
        float widthSpace = (SizeX - SpaceX) / columns;
        SpaceX /= (columns - 1);

        float SpaceY = spaceY * SizeY / 100f;
        float heightSpace = (SizeY - SpaceY) / rows;
        SpaceY /= (rows - 1);

        for (int currentRow = 0; currentRow < rows; ++currentRow)
        {
            for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
                GUI.DrawTexture(new Rect(initialPosX + currentColumn * (widthSpace + SpaceX), initialPosY + currentRow * (heightSpace + SpaceY), widthSpace, heightSpace), texture);
        }
    }

    /// <summary>
    /// Fill an area inside of an SLayer with a repeated texture
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="texture"></param>
    /// <param name="rows">repetitions vertically</param>
    /// <param name="columns">repetitions horizontally</param>
    /// <param name="spaceX">Percentage of the area used for horizontal spacing</param>
    /// <param name="spaceY">Percentage of the area used for horizontal spacing</param>
    public static void DrawTileset(SLayer layer, float x, float y, float width, float height, Texture2D texture, int rows, int columns, float spaceX, float spaceY)
    {
        float initialPosX = layer.Position.x + layer.Size.x * x / 100f;
        float initialPosY = layer.Position.y + layer.Size.y * y / 100f;

        float SizeX = width * layer.Size.x / 100f;
        float SizeY = height * layer.Size.y / 100f;

        float SpaceX = spaceX * Screen.width / 100f;
        float widthSpace = (SizeX - SpaceX) / columns;
        SpaceX /= (columns - 1);

        float SpaceY = spaceY * Screen.height / 100f;
        float heightSpace = (SizeY - SpaceY) / rows;
        SpaceY /= (rows - 1);

        for (int currentRow = 0; currentRow < rows; ++currentRow)
        {
            for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
                GUI.DrawTexture(new Rect(initialPosX + currentColumn * (widthSpace + SpaceX), initialPosY + currentRow * (heightSpace + SpaceY), widthSpace, heightSpace), texture);
        }
    }
    #endregion
    //-------------------------
    #region Draw Text
    /// <summary>
    /// Draw a text label.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    public static void DrawText(Vector2 position, string Text, int fontSize, Color textColor)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;

        GUI.skin.label.normal.textColor = textColor;
        GUI.skin.label.fontSize = fontSize;

        GUI.Label(new Rect(position.x * Screen.width / 100f, position.y * Screen.height / 100f, (100-position.x) * Screen.width / 100f, (100-position.y) * Screen.height / 100f), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
    }

    /// <summary>
    /// Write a Text Label in screen.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    public static void DrawText(float x, float y, string Text, int fontSize, Color textColor)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        
        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.normal.textColor = textColor;

        GUI.Label(new Rect(x * Screen.width / 100f, y * Screen.height / 100f, (100- x) * Screen.width / 100f, (100- y) * Screen.height / 100f), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
    }
    
    /// <summary>
    /// Write a Text Label inside an SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    public static void DrawText(SLayer layer, string Text, int fontSize, Color textColor)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;

        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.normal.textColor = textColor;

        GUI.Label(new Rect(layer.Position.x, layer.Position.y, layer.Size.x, layer.Size.y), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
    }

    /// <summary>
    /// Write a text inside an area of a SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="position"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    public static void DrawText(SLayer layer, Vector2 position, string Text, int fontSize, Color textColor)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;

        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.normal.textColor = textColor;

        float xx = layer.Position.x + position.x * layer.Size.x / 100f;
        float yy = layer.Position.y + position.y * layer.Size.y / 100f;
        float wwidth = (100 - position.x) * layer.Size.x / 100f;
        float hheight = (100 - position.y) * layer.Size.y / 100f;
        GUI.Label(new Rect(xx, yy, wwidth, hheight), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
    }

    /// <summary>
    /// Write a text inside an area of a SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    public static void DrawText(SLayer layer, float x, float y, string Text, int fontSize, Color textColor)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;

        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.normal.textColor = textColor;

        float xx = layer.Position.x + x * layer.Size.x / 100f;
		float yy = layer.Position.y + y * layer.Size.y / 100f;
        float wwidth = (100- x) * layer.Size.x / 100f;
        float hheight = (100-y) * layer.Size.y / 100f;
        GUI.Label(new Rect(xx, yy, wwidth, hheight), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
    }
    
    ///////////////////////////////////////////////////// WITH FONT
    /// <summary>
    /// Draw a text label.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    /// <param name="font"></param>
    public static void DrawText(Vector2 position, string Text, int fontSize, Color textColor, Font font)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.normal.textColor = textColor;
        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.font = font;

        GUI.Label(new Rect(position.x * Screen.width / 100f, position.y * Screen.height / 100f, (1 - position.x) * Screen.width / 100f, (1 - position.y) * Screen.height / 100f), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    /// <summary>
    /// Write a Text Label in screen.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    /// <param name="font"></param>
    public static void DrawText(float x, float y, string Text, int fontSize, Color textColor, Font font)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.normal.textColor = textColor;
        GUI.skin.label.font = font;

        GUI.Label(new Rect(x * Screen.width / 100f, y * Screen.height / 100f, (1 - x) * Screen.width / 100f, (1 - y) * Screen.height / 100f), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    /// <summary>
    /// Write a Text label inside of an SLayer
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    /// <param name="font"></param>
    public static void DrawText(SLayer layer, string Text, int fontSize, Color textColor, Font font)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.normal.textColor = textColor;
        GUI.skin.label.font = font;

        GUI.Label(new Rect(layer.Position.x, layer.Position.y, layer.Size.x, layer.Size.y), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    /// <summary>
    /// Write a text inside of an SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    /// <param name="font"></param>
    public static void DrawText(SLayer layer, float x, float y, string Text, int fontSize, Color textColor, Font font)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.normal.textColor = textColor;
        GUI.skin.label.font = font;

        float xx = layer.Position.x + x * layer.Size.x / 100f;
        float yy = layer.Position.y + y * layer.Size.y / 100f;
        float wwidth = (1 - x) * layer.Size.x / 100f;
        float hheight = (1 - y) * layer.Size.y / 100f;
        GUI.Label(new Rect(xx, yy, wwidth, hheight), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    /// <summary>
    /// Write a text inside of an SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="position"></param>
    /// <param name="Text"></param>
    /// <param name="fontSize"></param>
    /// <param name="textColor"></param>
    /// <param name="font"></param>
    public static void DrawText(SLayer layer, Vector2 position, string Text, int fontSize, Color textColor, Font font)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.fontSize = fontSize;
        GUI.skin.label.normal.textColor = textColor;
        GUI.skin.label.font = font;

        float xx = layer.Position.x + position.x * layer.Size.x / 100f;
        float yy = layer.Position.y + position.y * layer.Size.y / 100f;
        float wwidth = (1 - position.x) * layer.Size.x / 100f;
        float hheight = (1 - position.y) * layer.Size.y / 100f;
        GUI.Label(new Rect(xx, yy, wwidth, hheight), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    ////////////////////////////////////////////////////// WITH BRUSH
    /// <summary>
    /// Write a Text Label in screen.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="Text"></param>
    /// <param name="brush"></param>
    public static void DrawText(Vector2 position, string Text, TextBrush brush)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.normal.textColor = brush.TextColor;
        GUI.skin.label.fontSize = brush.FontSize;
        if (brush.TextFont != null)
            GUI.skin.label.font = brush.TextFont;

        GUI.Label(new Rect(position.x * Screen.width / 100f, position.y * Screen.height / 100f, (1-position.x) * Screen.width / 100f, (1-position.y) * Screen.height / 100f), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    /// <summary>
    /// Write a Text Label in screen.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="Text"></param>
    /// <param name="brush"></param>
    public static void DrawText(float x, float y, string Text, TextBrush brush)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.fontSize = brush.FontSize;
        GUI.skin.label.normal.textColor = brush.TextColor;
        if (brush.TextFont != null)
            GUI.skin.label.font = brush.TextFont;

        GUI.Label(new Rect(x * Screen.width / 100f, y * Screen.height / 100f, (1-x) * Screen.width / 100f, (1-y) * Screen.height / 100f), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    /// <summary>
    /// Write a Text Label inside of an SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Text"></param>
    /// <param name="brush"></param>
    public static void DrawText(SLayer layer, string Text, TextBrush brush)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.fontSize = brush.FontSize;
        GUI.skin.label.normal.textColor = brush.TextColor;
        if (brush.TextFont != null)
            GUI.skin.label.font = brush.TextFont;

        GUI.Label(new Rect(layer.Position.x, layer.Position.y, layer.Size.x, layer.Size.y), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    /// <summary>
    /// Write a text inside of an SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="position"></param>
    /// <param name="Text"></param>
    /// <param name="brush"></param>
    public static void DrawText(SLayer layer, Vector2 position, string Text, TextBrush brush)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.fontSize = brush.FontSize;
        GUI.skin.label.normal.textColor = brush.TextColor;
        if (brush.TextFont != null)
            GUI.skin.label.font = brush.TextFont;

        float xx = layer.Position.x + position.x * layer.Size.x / 100f;
        float yy = layer.Position.y + position.y * layer.Size.y / 100f;
        float wwidth = (1- position.x) * layer.Size.x / 100f;
        float hheight = (1 - position.y) * layer.Size.y / 100f;
        GUI.Label(new Rect(xx, yy, wwidth, hheight), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    /// <summary>
    /// Write a text inside of an SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="Text"></param>
    /// <param name="brush"></param>
    public static void DrawText(SLayer layer, float x, float y, string Text, TextBrush brush)
    {
        int size = GUI.skin.label.fontSize;
        Color color = GUI.skin.label.normal.textColor;
        Font f = GUI.skin.label.font;

        GUI.skin.label.fontSize = brush.FontSize;
        GUI.skin.label.normal.textColor = brush.TextColor;
        if (brush.TextFont != null)
            GUI.skin.label.font = brush.TextFont;

        float xx = layer.Position.x + x * layer.Size.x / 100f;
        float yy = layer.Position.y + y * layer.Size.y / 100f;
        float wwidth = (1-x) * layer.Size.x / 100f;
        float hheight = (1-y) * layer.Size.y / 100f;
        GUI.Label(new Rect(xx, yy, wwidth, hheight), Text);

        GUI.skin.label.fontSize = size;
        GUI.skin.label.normal.textColor = color;
        GUI.skin.label.font = f;
    }

    #endregion
    //-------------------------
    #region DrawButton
    /// <summary>
    /// Draw a button with text inside
    /// </summary>
    /// <param name="Percentages"></param>
    /// <param name="Text"></param>
    /// <returns>True if the button is hit</returns>
    public static bool DrawButton(Rect Percentages, string Text)
    {
        return GUI.Button(new Rect(Screen.width * Percentages.x / 100f, Screen.height * Percentages.y / 100f, Screen.width * Percentages.width / 100f, Screen.height * Percentages.height / 100f), Text);
    }

    /// <summary>
    /// Draw a button with text inside
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Text"></param>
    /// <returns>True if the button is hit</returns>
    public static bool DrawButton (float x, float y, float width, float height, string Text)
	{
		return GUI.Button (new Rect (Screen.width*x/100f, Screen.height*y/100f, Screen.width*width/100f, Screen.height*height/100f), Text);
	}

    /// <summary>
    /// Draw a button with a texture inside
    /// </summary>
    /// <param name="Percentages"></param>
    /// <param name="Texture"></param>
    /// <returns>True if the button is hit</returns>
    public static bool DrawButton(Rect Percentages, Texture2D Texture)
    {
        return GUI.Button(new Rect(Screen.width * Percentages.x / 100f, Screen.height * Percentages.y / 100f, Screen.width * Percentages.width / 100f, Screen.height * Percentages.height / 100f), Texture);
    }

    /// <summary>
    /// Draw a button with a texture inside
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Texture"></param>
    /// <returns>True if the button is hit</returns>
    public static bool DrawButton (float x, float y, float width, float height, Texture2D Texture)
	{
		return GUI.Button (new Rect (Screen.width*x/100f, Screen.height*y/100f, Screen.width*width/100f, Screen.height*height/100f), Texture);
	}
	
    /// <summary>
    /// Draw a GUI Button inside of an SLayer with a text.
	/// </summary>
	/// <param name="layer"></param>
	/// <param name="Text"></param>
    /// <returns>True if the button is hit</returns>
	public static bool DrawButton (SLayer layer, string Text)
	{
		return GUI.Button (new Rect (layer.Position.x, layer.Position.y, layer.Size.x, layer.Size.y), Text);
	}
	
    /// <summary>
    /// Draw a GUI Button inside of an SLayer with a texture.
	/// </summary>
	/// <param name="layer"></param>
	/// <param name="Texture"></param>
    /// <returns>True if the button is hit</returns>
	public static bool DrawButton (SLayer layer, Texture2D Texture)
	{
		return GUI.Button (new Rect (layer.Position.x, layer.Position.y, layer.Size.x, layer.Size.y), Texture);
	}

    /// <summary>
    /// Draw a GUI Button inside of an SLayer with a text.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Text"></param>
    /// <returns>True if button is pressed</returns>
    public static bool DrawButton (SLayer layer, float x, float y, float width, float height, string Text)
	{
        float xx = layer.Position.x + x * layer.Size.x / 100f;
		float yy = layer.Position.x + y * layer.Size.x / 100f;
        float ww = width * layer.Size.x / 100f;
		float hh = height * layer.Size.y / 100f;
        return GUI.Button (new Rect (xx, yy, ww, hh), Text);
	}
	
    /// <summary>
    /// Draw a GUI Button inside of an SLayer with a texture.
	/// </summary>
	/// <param name="layer"></param>
	/// <param name="percentageX"></param>
	/// <param name="percentageY"></param>
	/// <param name="percentageWith"></param>
	/// <param name="percentageHeight"></param>
	/// <param name="Texture"></param>
    /// <returns>True if the button is hit</returns>
	public static bool DrawButton (SLayer layer, float x, float y, float width, float height, Texture2D Texture)
	{
        float xx = layer.Position.x + x * layer.Size.x / 100f;
        float yy = layer.Position.x + y * layer.Size.x / 100f;
        float ww = width * layer.Size.x / 100f;
        float hh = height * layer.Size.y / 100f;
        return GUI.Button (new Rect (xx,yy,ww,hh), Texture);
	}
    
    /// <summary>
    /// Draw a GUI Button inside of an SLayer with a text.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Percentages"></param>
    /// <param name="Text"></param>
    /// <returns>True if the button is hit</returns>
    public static bool DrawButton (SLayer layer, Rect Percentages, string Text)
	{
		float xx = layer.Position.x + Percentages.x * layer.Size.x / 100f;
		float yy = layer.Position.y + Percentages.y * layer.Size.y / 100f;
        float wwidth = Percentages.width * layer.Size.x / 100f;
        float hheight = Percentages.height * layer.Size.y / 100f;

		return GUI.Button (new Rect(xx, yy, wwidth, hheight), Text);
	}
	
    /// <summary>
    /// Draw a GUI Button inside of an SLayer with a texture.
	/// </summary>
	/// <param name="layer"></param>
	/// <param name="Percentages"></param>
	/// <param name="Texture"></param>
    /// <returns>True if the button is hit</returns>
	public static bool DrawButton (SLayer layer, Rect Percentages, Texture2D Texture)
	{
        float xx = layer.Position.x + Percentages.x * layer.Size.x / 100f;
        float yy = layer.Position.y + Percentages.y * layer.Size.y / 100f;
        float wwidth = Percentages.width * layer.Size.x / 100f;
        float hheight = Percentages.height * layer.Size.y / 100f;

        return GUI.Button (new Rect(xx,yy,wwidth,hheight), Texture);
	}
    #endregion
    //--------------------------
    #region DrawBox
    /// <summary>
    /// Draw a GUI Box with a text.
    /// </summary>
    /// <param name="Text"></param>
    public static void DrawBox(string Text)
    {
        GUI.Box(new Rect(0,0,Screen.width, Screen.height), Text);
    }
    
    /// <summary>
    /// Draw a GUI Box with a text.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Text"></param>
    public static void DrawBox(float x, float y, float width, float height, string Text)
    {
        GUI.Box(new Rect(Screen.width * x / 100f, Screen.height * y / 100f, Screen.width * width / 100f, Screen.height * height / 100f), Text);
    }
    
    /// <summary>
    /// Draw a GUI Box with a texture.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Texture"></param>
	public static void DrawBox(float x, float y, float width, float height, Texture2D Texture)
    {
        GUI.Box(new Rect(Screen.width * x / 100f, Screen.height * y / 100f, Screen.width * width / 100f, Screen.height * height / 100f), Texture);
    }

    /// <summary>
    /// Draw a GUI Box inside of an SLayer with a text.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Text"></param>
	public static void DrawBox(SLayer layer, string Text)
    {
		GUI.Box(new Rect(layer.Position.x, layer.Position.y, layer.Size.x, layer.Size.y), Text);
    }

    /// <summary>
    /// Draw a GUI Box inside of an SLayer with a texture.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Texture"></param>
	public static void DrawBox(SLayer layer, Texture2D Texture)
    {
		GUI.Box(new Rect(layer.Position.x, layer.Position.y, layer.Size.x, layer.Size.y), Texture);
    }
    
    /// <summary>
    /// Draw a GUI Box inside of an SLayer with a text.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Text"></param>
	public static void DrawBox(SLayer layer, float x, float y, float width, float height, string Text)
    {
        float xx = layer.Position.x + layer.Size.x * x / 100f;
        float yy = layer.Position.y + layer.Size.y * y / 100f;
        float ww = layer.Size.x * width / 100f;
        float hh = layer.Size.y * height / 100f;
        GUI.Box(new Rect(xx, yy, ww, hh), Text);
    }

    /// <summary>
    /// Draw a GUI Box inside of an SLayer with a texture.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Texture"></param>
    public static void DrawBox(SLayer layer, float x, float y, float width, float height, Texture2D Texture)
    {
        float xx = layer.Position.x + layer.Size.x * x / 100f;
        float yy = layer.Position.y + layer.Size.y * y / 100f;
        float ww = layer.Size.x * width / 100f;
        float hh = layer.Size.y * height / 100f;
        GUI.Box(new Rect(xx, yy, ww, hh), Texture);
    }
    
    /// <summary>
    /// Draw a GUI Box with a text.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Percentages"></param>
    /// <param name="Text"></param>
    public static void DrawBox(Rect Percentages, string Text)
    {
        GUI.Box(new Rect(Percentages.x * Screen.width / 100f, Percentages.y * Screen.height / 100f, Percentages.width * Screen.width / 100f, Percentages.height * Screen.height / 100f), Text);
    }
    
    /// <summary>
    /// Draw a GUI Box with a texture.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Percentages"></param>
    /// <param name="Texture"></param>
    public static void DrawBox(Rect Percentages, Texture2D Texture)
    {
        GUI.Box(new Rect(Percentages.x * Screen.width / 100f, Percentages.y * Screen.height / 100f, Percentages.width * Screen.width / 100f, Percentages.height * Screen.height / 100f), Texture);
    }

    /// <summary>
    /// Draw a GUI Box inside of an SLayer with a text.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Percentages"></param>
    /// <param name="Text"></param>
	public static void DrawBox(SLayer layer, Rect Percentages, string Text)
    {
		float xx = layer.Position.x + Percentages.x * layer.Size.x / 100f;
		float yy = layer.Position.y + Percentages.y * layer.Size.y / 100f;
        float wwidth = Percentages.width * layer.Size.x / 100f;
        float hheight = Percentages.height * layer.Size.y / 100f;

        GUI.Box(new Rect(xx, yy, wwidth, hheight), Text);
    }
    
    /// <summary>
    /// Draw a GUI Box inside of an SLayer with a texture.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Percentages"></param>
    /// <param name="Texture"></param>
	public static void DrawBox(SLayer layer, Rect Percentages, Texture2D Texture)
    {
        float xx = layer.Position.x + Percentages.x * layer.Size.x / 100f;
        float yy = layer.Position.y + Percentages.y * layer.Size.y / 100f;
        float wwidth = Percentages.width * layer.Size.x / 100f;
        float hheight = Percentages.height * layer.Size.y / 100f;

        GUI.Box(new Rect(xx, yy, wwidth, hheight), Texture);
    }
    #endregion
    //--------------------------
    #region Draw Text Field
    /// <summary>
    /// Draw a Text Input Field.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextField(float x, float y, float width, float height, string Text, int maxLength)
    {
        return GUI.TextField(new Rect(Screen.width * x / 100f, Screen.height * y / 100f, Screen.width * width / 100f, Screen.height * height / 100f), Text, maxLength);
    }

    /// <summary>
    /// Draw a Text Input Field fullfilling an SLayer
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextField(SLayer layer, string Text, int maxLength)
    {
		return GUI.TextField(new Rect(layer.Position.x, layer.Position.y, layer.Size.x, layer.Size.y), Text, maxLength);
    }

    /// <summary>
    /// Draw a Text Input Field inside of an SLayer
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextField(SLayer layer, float x, float y, float width, float height, string Text, int maxLength)
    {
        float xx = layer.Position.x + x * layer.Size.x / 100f;
        float yy = layer.Position.y + y * layer.Size.y / 100f;
        float ww = width * layer.Size.x / 100f;
        float hh = height * layer.Size.y / 100f;
        return GUI.TextField(new Rect(xx, yy, ww, hh), Text, maxLength);
    }
    
    /// <summary>
    /// Draw a Text Input Field.
    /// </summary>
    /// <param name="percentages"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextField(Rect percentages, string Text, int maxLength)
    {
        return GUI.TextField(new Rect(Screen.width * percentages.x / 100f, Screen.height * percentages.y / 100f, Screen.width * percentages.width / 100f, Screen.height * percentages.height / 100f), Text, maxLength);
    }

    /// <summary>
    /// Draw a Text Input Field.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="percentages"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextField(SLayer layer, Rect percentages, string Text, int maxLength)
    {
        float xx = layer.Position.x + percentages.x * layer.Size.x / 100f;
        float yy = layer.Position.y + percentages.y * layer.Size.y / 100f;
        float ww = percentages.width * layer.Size.x / 100f;
        float hh = percentages.height * layer.Size.y / 100f;
        return GUI.TextField(new Rect(xx, yy, ww, hh), Text, maxLength);
    }
    #endregion
    //--------------------------
    #region Draw Text Area
    /// <summary>
    /// Draw a Text Area.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextArea(float x, float y, float width, float height, string Text, int maxLength)
    {
        return GUI.TextArea(new Rect(Screen.width * x / 100f, Screen.height * y / 100f, Screen.width * width / 100f, Screen.height * height / 100f), Text, maxLength);
    }

    /// <summary>
    /// Draw a Text Area.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextArea(SLayer layer, string Text, int maxLength)
    {
        return GUI.TextArea(new Rect(layer.Position.x, layer.Position.y, layer.Size.x, layer.Size.y), Text, maxLength);
    }

    /// <summary>
    /// Draw a Text Area inside of an SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="percentageX"></param>
    /// <param name="percentageY"></param>
    /// <param name="percentageWith"></param>
    /// <param name="percentageHeight"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextArea(SLayer layer, float x, float y, float width, float height, string Text, int maxLength)
    {
        float xx = layer.Position.x + x * layer.Size.x / 100f;
        float yy = layer.Position.y + y * layer.Size.y / 100f;
        float ww = width * layer.Size.x / 100f;
        float hh = height * layer.Size.y / 100f;
        return GUI.TextArea(new Rect(xx, yy, ww, hh), Text, maxLength);
    }

    /// <summary>
    /// Draw a Text Area.
    /// </summary>
    /// <param name="percentages"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextArea(Rect percentages, string Text, int maxLength)
    {
        return GUI.TextArea(new Rect(Screen.width * percentages.x / 100f, Screen.height * percentages.y / 100f, Screen.width * percentages.width / 100f, Screen.height * percentages.height / 100f), Text, maxLength);
    }

    /// <summary>
    /// Draw a Text Area inside of an SLayer.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="percentages"></param>
    /// <param name="Text">Text input</param>
    /// <param name="maxLength">Max length of the text</param>
    /// <returns>Text output</returns>
    public static string DrawTextArea(SLayer layer, Rect percentages, string Text, int maxLength)
    {
        float xx = layer.Position.x + percentages.x * layer.Size.x / 100f;
        float yy = layer.Position.y + percentages.y * layer.Size.y / 100f;
        float ww = percentages.width * layer.Size.x / 100f;
        float hh = percentages.height * layer.Size.y / 100f;
        return GUI.TextArea(new Rect(xx, yy, ww, hh), Text, maxLength);
    }
    #endregion
    //--------------------------
    #region DrawSlider
    // HORIZONTAL
    /// <summary>
    /// Draw an horizontal slider.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="value">Current value</param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float HorizontalSlider (float x, float y, float width, float value, float min, float max)
    {
        return GUI.HorizontalSlider(new Rect(x * Screen.width / 100f, y * Screen.height / 100f, width * Screen.width / 100f, 13), value, min, max);
    }

    /// <summary>
    /// Draw an horizontal slider.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="value">Current value</param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float HorizontalSlider(SLayer layer, float x, float y, float width, float value, float min, float max)
    {
        float xx = layer.Position.x + layer.Size.x * x / 100f;
        float yy = layer.Position.y + layer.Size.y * y / 100f;
        float w = layer.Size.x * width / 100f;
        return GUI.HorizontalSlider(new Rect(xx,yy,w,13), value, min, max);
    }
    
    // VERTICAL
    /// <summary>
    /// Draw a vertical slider.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="height"></param>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float VerticalSlider(float x, float y, float height, float value, float min, float max)
    {
        return GUI.VerticalSlider(new Rect(x * Screen.width / 100f, y * Screen.height / 100f, 13, height * Screen.height / 100f), value, min, max);
    }

    /// <summary>
    /// Draw a vertical slider inside of an SLayer
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="height"></param>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float VerticalSlider(SLayer layer, float x, float y, float height, float value, float min, float max)
    {
        float xx = layer.Position.x + layer.Size.x * x / 100f;
        float yy = layer.Position.y + layer.Size.y * y / 100f;
        float h = layer.Size.y * height / 100f;
        return GUI.VerticalSlider(new Rect(xx, yy, 13, h), value, min, max);
    }
    #endregion
    //--------------------------
    #region With Classes
    /// <summary>
    /// Detect if two SElelemts are colliding.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public static bool Collision(SElement A, SElement B)
    {
		Vector2 Apos = A.Position;
		Vector2 Asize = A.Size;
        if (Asize.x < 0)
        {
            Asize = new Vector2(-Asize.x, Asize.y);
            Apos = new Vector2(Apos.x - Asize.x, Asize.y);
        }
        if (Asize.y < 0)
        {
            Asize = new Vector2(Asize.x, -Asize.y);
            Apos = new Vector2(Apos.x, Asize.y - Asize.y);
        }

		Vector2 Bpos = B.Position;
		Vector2 Bsize = B.Size;
        if (Bsize.x < 0)
        {
            Bsize = new Vector2(-Bsize.x, Bsize.y);
            Bpos = new Vector2(Bpos.x - Bsize.x, Bsize.y);
        }
        if (Bsize.y < 0)
        {
            Bsize = new Vector2(Bsize.x, -Bsize.y);
            Bpos = new Vector2(Bpos.x, Bsize.y - Bsize.y);
        }

        if (Apos.x + Asize.x < Bpos.x) return false;
        if (Apos.x > Bpos.x + Bsize.x) return false;
        if (Apos.y + Asize.y < Bpos.y) return false;
        if (Apos.y > Bpos.y + Bsize.y) return false;

        return true;
    }

    /// <summary>
    /// Get the screen percentages of the intersection of A and B. If they do not intersect, the Rect is (0,0,0,0)
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns>Intersection of A and B</returns>
    public static Rect Intersection(SElement A, SElement B)
    {
        if (!Collision(A, B)) return new Rect(0, 0, 0, 0);

        float x;
        float y;
        float width;
        float height;

        Vector2 Apos = A.Position;
        Vector2 Asize = A.Size;

        if (Asize.x < 0)
        {
            Asize = new Vector2(-Asize.x, Asize.y);
            Apos = new Vector2(Apos.x - Asize.x, Apos.y);
        }
        if (Asize.y < 0)
        {
            Asize = new Vector2(Asize.x, -Asize.y);
            Apos = new Vector2(Apos.x, Apos.y - Asize.y);
        }

        Vector2 Bpos = B.Position;
        Vector2 Bsize = B.Size;

        if (Bsize.x < 0)
        {
            Bsize = new Vector2(-Bsize.x, Bsize.y);
            Bpos = new Vector2(Bpos.x - Bsize.x, Bpos.y);
        }
        if (Asize.y < 0)
        {
            Bsize = new Vector2(Bsize.x, -Bsize.y);
            Bpos = new Vector2(Bpos.x, Bpos.y - Bsize.y);
        }

        if (Apos.x > Bpos.x)
        {
            x = Apos.x;
            width = Bpos.x + Bsize.x - x;
        }
        else
        {
            x = Bpos.x;
            width = Apos.x + Asize.x - x;
        }

        if (Apos.y > Bpos.y)
        {
            y = Apos.y;
            height = Bpos.y + Bsize.y - y;
        }
        else
        {
            y = Bpos.y;
            height = Apos.y + Asize.y - y;
        }

        return new Rect(x / Screen.width * 100f, y / Screen.height * 100f, width / Screen.width * 100f, height / Screen.height * 100f);
    }

    /// <summary>
    /// The area containing A and B (in percentages)
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns>Area of A and B</returns>
    public static Rect Union(SElement A, SElement B)
    {
        float x;
        float y;
        float width;
        float height;

        Vector2 Apos = A.Position;
        Vector2 Asize = A.Size;

        if (Asize.x < 0)
        {
            Asize = new Vector2(-Asize.x, Asize.y);
            Apos = new Vector2(Apos.x - Asize.x, Apos.y);
        }
        if (Asize.y < 0)
        {
            Asize = new Vector2(Asize.x, -Asize.y);
            Apos = new Vector2(Apos.x, Apos.y - Asize.y);
        }

        Vector2 Bpos = B.Position;
        Vector2 Bsize = B.Size;

        if (Bsize.x < 0)
        {
            Bsize = new Vector2(-Bsize.x, Bsize.y);
            Bpos = new Vector2(Bpos.x - Bsize.x, Bpos.y);
        }
        if (Asize.y < 0)
        {
            Bsize = new Vector2(Bsize.x, -Bsize.y);
            Bpos = new Vector2(Bpos.x, Bpos.y - Bsize.y);
        }

        if (Apos.x < Bpos.x) x = Apos.x;
        else x = Bpos.x;

        if (Apos.y < Bpos.y) y = Apos.y;
        else y = Bpos.y;

        if (Apos.x + Asize.x > Bpos.x + Bsize.x) width = Apos.x + Asize.x - x;
        else width = Bpos.x + Bsize.x - x;

        if (Apos.y + Asize.y > Bpos.y + Bsize.y) height = Apos.y + Asize.y - y;
        else height = Bpos.y + Bsize.y - y;

        return new Rect(x/Screen.width*100f, y / Screen.height * 100f, width / Screen.width * 100f, height / Screen.height * 100f);
    }
    #endregion
    #endregion

    #region classes
    #region Structure Classes
    /// <summary>
    /// A layer in the screen for SGUI elements
    /// </summary>
    public class SLayer
    {
        #region Attributes
        private Rect percentages;
        public float XPerCent { get { return percentages.x; } }
        public float YPerCent { get { return percentages.y; } }
        public float WidthPerCent { get { return percentages.width; } }
        public float HeightPerCent { get { return percentages.height; } }

        private Vector2 size;
        /// <summary>
        /// Current size in pixels of the layer
        /// </summary>
        public Vector2 Size { get { return new Vector2(size.x, size.y); } }

        private Vector2 position;
        /// <summary>
        /// Current position in pixels of the layer
        /// </summary>
        public Vector2 Position { get { return new Vector2(position.x, position.y); } }

        #endregion

        #region Constructors
        public SLayer (float x,float y,float width, float height)
        {
            SetPercentages(new Rect(x, y, width, height));
        }
        public SLayer(SLayer layer, float x, float y, float width, float height)
        {
            float xx = layer.XPerCent + x * layer.WidthPerCent / 100f;
			float yy = layer.YPerCent + y * layer.HeightPerCent / 100f;
			float wwidth = width * layer.WidthPerCent / 100f;
			float hheight = height * layer.HeightPerCent / 100f;
            SetPercentages(new Rect(xx, yy, wwidth, hheight));
        }
        public SLayer(Rect Percentages)
        {
            float width = Percentages.width;
            float height = Percentages.height;
            SetPercentages(new Rect(Percentages.x, Percentages.y, width, height));
        }
        public SLayer(SLayer layer, Rect Percentages)
        {
			float xx = layer.XPerCent + Percentages.x * layer.WidthPerCent / 100f;
			float yy = layer.YPerCent + Percentages.y * layer.HeightPerCent / 100f;
			float wwidth = Percentages.width * layer.WidthPerCent / 100f;
			float hheight = Percentages.height * layer.HeightPerCent / 100f;
            SetPercentages(new Rect(xx, yy, wwidth, hheight));
        }
        #endregion

        #region Methods
        public void SetPercentages (Rect Percentages)
        {
            percentages = Percentages;
            size = new Vector2(Screen.width * percentages.width / 100f, Screen.height * percentages.height / 100f);
            position = new Vector2(Screen.width * percentages.x / 100f, Screen.height * percentages.y / 100f);
        }

        public void SetPercentages(float x, float y, float width, float height) 
        {
            SetPercentages(new Rect(x, y, width, height));
        }
        #endregion
    }

    /// <summary>
    /// A brush to write different texts with same color, size and font
    /// </summary>
    public class TextBrush
    {
        public Color TextColor;
        public int FontSize;
        public Font TextFont;

        public TextBrush(Color textColor, int fontSize)
        {
            TextColor = textColor;
            FontSize = fontSize;
            TextFont = null;
        }

        public TextBrush(Color textColor, int fontSize, Font font)
        {
            TextColor = textColor;
            FontSize = fontSize;
            TextFont = font;
        }

        private List<string> texts = new List<string>();
        private List<Rect> positions = new List<Rect>();

        public void AddText(Rect percentages, string Text)
        {
            texts.Add(Text);
            positions.Add(percentages);
        }

        public void Write()
        {
            if (TextFont == null) TextFont = GUI.skin.label.font;

            int size = GUI.skin.label.fontSize;
            Color color = GUI.skin.label.normal.textColor;
            Font f = GUI.skin.label.font;

            GUI.skin.label.normal.textColor = TextColor;
            GUI.skin.label.fontSize = FontSize;
            GUI.skin.label.font = TextFont;

            for (int i = 0; i<texts.Count; ++i)
            {
                Rect Percentages = positions[i];

                GUI.Label(new Rect(Percentages.x * Screen.width / 100f, Percentages.y * Screen.height / 100f, Percentages.width * Screen.width / 100f, Percentages.height * Screen.height / 100f), texts[i]);
            }


            GUI.skin.label.fontSize = size;
            GUI.skin.label.normal.textColor = color;
            GUI.skin.label.font = f;
        }
    }

    /// <summary>
    /// Create a gradual transition between two states
    /// </summary>
    public abstract class STransition
    {
        protected Rect initial;
        protected Rect difference;
        protected float initialTransparency;
        protected float differenceTransparency;
        protected float initialRotation;
        protected float differenceRotation;
        protected Vector3 initialColor;
        protected Vector3 differenceColor;
        protected float timer;
        protected float totalTime;

        internal STransition(Rect initial, Rect final, float Time)
        {
            this.initial = initial;
            totalTime = Time;
            timer = 0;
            initialTransparency = 1;
            differenceTransparency = 0;
            initialRotation = differenceRotation = 0;
            initialColor = differenceColor = new Vector3(1,1,1);

            difference = new Rect(final.x - initial.x, final.y - initial.y, final.width - initial.width, final.height - initial.height);
        }

        public virtual void SetTransparency(float initial, float final)
        {
            this.initialTransparency = initial;
            differenceTransparency = final - initial;
        }
        public virtual void SetRotation(float initial, float final)
        {
            this.initialRotation = initial;
            differenceRotation = final - initial;
        }
        public virtual void SetTint(Color initialColor, Color finalColor)
        {
            this.initialColor = new Vector3(initialColor.r, initialColor.g, initialColor.b);
            differenceColor = new Vector3(finalColor.r - initialColor.r, finalColor.g - initialColor.g, finalColor.b - initialColor.b);
        }


        public virtual void DrawPaused()
        {
            
        }

        public virtual bool Draw(float deltatime)
        {
            return true;
        }

        public virtual void Restart()
        {
            timer = 0;
        }

        public float Progress() { return (timer / totalTime); }

        public Rect PercentagesNow()
        {
            float p = Progress();
            float x = initial.x + difference.x * p;
            float y = initial.y + difference.y * p;
            float width = initial.width + difference.width * p;
            float height = initial.height + difference.height * p;

            return new Rect(x, y, width, height);
        }

        public float RotationNow()
        {
            return initialRotation + differenceRotation * Progress();
        }

        public float TransparencyNow()
        {
            float t = initialTransparency + differenceTransparency * Progress();
            while (t < 0) t += 1;
            while (t > 1) t -= 1;
            return t;
        }

        public Color TintNow()
        {
            float p = Progress();
            return new Color(initialColor.x + differenceColor.x * p, initialColor.y + differenceColor.y * p, initialColor.z + differenceColor.z * p);
        }
    }

    /// <summary>
    /// Contains and manage automatically the transitions. Just add transitions to it and draw.
    /// </summary>
    public class STransitionManager
    {
        class STManagerT
        {
            public STransition t;
            public Color tint;
            public float speed;
            public float rotation;
            public bool loop;
            public int repetitions = 0;
            public int Repetitions;

            public STManagerT(STransition T, Color Tint, float Speed, float Rotation, bool Loop, int Repetitions)
            {
                t = T;
                tint = Tint;
                speed = Speed;
                rotation = Rotation;
                loop = Loop;
                this.Repetitions = Repetitions;
            }
        }

        private List<STManagerT> transitions = new List<STManagerT>();

        /////////////////////////////////////////////////////////////// Add transition without loop
        public void AddTransition(STransition transition)
        {
            transitions.Add(new STManagerT(transition, Color.white, 1f, 0f, false, 1));
        }
        public void AddTransition(STransition transition, float speed)
        {
            transitions.Add(new STManagerT(transition, Color.white, speed, 0f, false, 1));
        }
        public void AddTransition(STransition transition, Color tint)
        {
            transitions.Add(new STManagerT(transition, tint, 1f, 0f, false, 1));
        }
        public void AddTransition(STransition transition, float speed, Color tint)
        {
            transitions.Add(new STManagerT(transition, tint, speed, 0f, false, 1));
        }
        public void AddTransition(STransition transition, float speed, float rotation)
        {
            transitions.Add(new STManagerT(transition, Color.white, speed, rotation, false, 1));
        }
        public void AddTransition(STransition transition, Color tint, float rotation)
        {
            transitions.Add(new STManagerT(transition, tint, 1f, rotation, false, 1));
        }
        public void AddTransition(STransition transition, float speed, Color tint, float rotation)
        {
            transitions.Add(new STManagerT(transition, tint, speed, rotation, false, 1));
        }

        /////////////////////////////////////////////////////////////////// With repetitions
        public void AddTransition(STransition transition, int repetitions)
        {
            transitions.Add(new STManagerT(transition, Color.white, 1f, 0f, false, repetitions));
        }
        public void AddTransition(STransition transition, float speed, int repetitions)
        {
            transitions.Add(new STManagerT(transition, Color.white, speed, 0f, false, repetitions));
        }
        public void AddTransition(STransition transition, Color tint, int repetitions)
        {
            transitions.Add(new STManagerT(transition, tint, 1f, 0f, false, repetitions));
        }
        public void AddTransition(STransition transition, float speed, Color tint, int repetitions)
        {
            transitions.Add(new STManagerT(transition, tint, speed, 0f, false, repetitions));
        }
        public void AddTransition(STransition transition, float speed, float rotation, int repetitions)
        {
            transitions.Add(new STManagerT(transition, Color.white, speed, rotation, false, repetitions));
        }
        public void AddTransition(STransition transition, Color tint, float rotation, int repetitions)
        {
            transitions.Add(new STManagerT(transition, tint, 1f, rotation, false, repetitions));
        }
        public void AddTransition(STransition transition, float speed, Color tint, float rotation, int repetitions)
        {
            transitions.Add(new STManagerT(transition, tint, speed, rotation, false, repetitions));
        }

        //////////////////////////////////////////////////////////////////// Transition with loop

        public void AddTransitionLoop(STransition transition)
        {
            transitions.Add(new STManagerT(transition, Color.white, 1f, 0f, true, 1));
        }
        public void AddTransitionLoop(STransition transition, float speed)
        {
            transitions.Add(new STManagerT(transition, Color.white, speed, 0f, true, 1));
        }
        public void AddTransitionLoop(STransition transition, Color tint)
        {
            transitions.Add(new STManagerT(transition, tint, 1f, 0f, true, 1));
        }
        public void AddTransitionLoop(STransition transition, float speed, Color tint)
        {
            transitions.Add(new STManagerT(transition, tint, speed, 0f, true, 1));
        }
        public void AddTransitionLoop(STransition transition, float speed, float rotation)
        {
            transitions.Add(new STManagerT(transition, Color.white, speed, rotation, true, 1));
        }
        public void AddTransitionLoop(STransition transition, Color tint, float rotation)
        {
            transitions.Add(new STManagerT(transition, tint, 1f, rotation, true, 1));
        }
        public void AddTransitionLoop(STransition transition, float speed, Color tint, float rotation)
        {
            transitions.Add(new STManagerT(transition, tint, speed, rotation, true, 1));
        }
        /////////////////////////////////////////////////////////////////////////////////////////////

        public void UpdateTimes(float deltatime)
        {
            for (int i = 0; i < transitions.Count; ++i)
            {
                STManagerT t = transitions[i];

                if (t.t.GetType() == typeof(STextureTransition))
                {
                    STextureTransition tt = t.t as STextureTransition;
                    if (tt.UpdateTime(deltatime*t.speed))
                    {
                        if (t.loop)
                            t.t.Restart();
                        else
                        {
                            transitions.RemoveAt(i);
                            --i;
                        }
                    }
                }
                else
                {
                    STextTransition tt = t.t as STextTransition;
                    if (tt.UpdateTime(deltatime * t.speed))
                    {
                        if (t.loop)
                            t.t.Restart();
                        else
                        {
                            transitions.RemoveAt(i);
                            --i;
                        }
                    }
                }
            }
        }

        public void DrawTransitions()
        {
            Color c = GUI.color;

            for (int i = 0; i < transitions.Count; ++i)
            {
                STManagerT t = transitions[i];
                GUI.color = t.tint;

                Rect P = t.t.PercentagesNow();
                Vector2 pivotPoint = new Vector2((P.x + P.width / 2f) * Screen.width / 100f, (P.y + P.height/2f) * Screen.height / 100f);
                GUIUtility.RotateAroundPivot(t.rotation, pivotPoint);
                t.t.DrawPaused();
                GUIUtility.RotateAroundPivot(-t.rotation, pivotPoint);
            }

            GUI.color = c;
        }

        public void DrawTransitions(float deltaTime)
        {
            Color c = GUI.color;

            for (int i = 0; i < transitions.Count; ++i)
            {
                STManagerT t = transitions[i];
                GUI.color = t.tint;

                Rect P = t.t.PercentagesNow();
                Vector2 pivotPoint = new Vector2((P.x + P.width / 2f) * Screen.width / 100f, (P.y + P.height / 2f) * Screen.height / 100f);
                GUIUtility.RotateAroundPivot(t.rotation, pivotPoint);

                if (t.t.Draw(deltaTime * t.speed))
                {
                    if (t.loop)
                        t.t.Restart();
                    else
                    {
                        ++t.repetitions;
                        if (t.repetitions >= t.Repetitions)
                        {
                            transitions.RemoveAt(i);
                            --i;
                        }
                        else
                            t.t.Restart();
                    }
                }

                GUIUtility.RotateAroundPivot(-t.rotation, pivotPoint);
            }

            GUI.color = c;
        }

        public void SetSpeedForTransition(STransition t, float speed)
        {
            foreach(STManagerT T in transitions)
            {
                if (T.t == t)
                {
                    T.speed = speed;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// A transition from an initial position, size and transparency to a final with a Texture
    /// </summary>
    public class STextureTransition : STransition
    {
        private Texture2D e;

        // ORIGINAL
        Rect orInitial;
        Rect orFinal;
        float orTime;
        Vector2 orTransparency;
        Vector2 orRotation;
        Vector3 orInitialTint;
        Vector3 orFinalTint;
        STextureTransition orNext;
        Texture2D orTex;
        //

        private STextureTransition nextTransition;

        public void SetNextTransition(STextureTransition Next)
        {
            nextTransition = Next;
        }

        public STextureTransition(Texture2D element, Rect initial, Rect final, float Time) : base(initial, final, Time)
        {
            e = element;
        }

        public STextureTransition(Texture2D element, Rect initial, Rect final, float Time, STextureTransition Next) : base(initial, final, Time)
        {
            e = element;
            nextTransition = Next;

            orInitial = initial;
            orFinal = final;
            orTime = Time;
            orTransparency = new Vector2(1, 1);
            orRotation = new Vector2(0, 0);
            orInitialTint = orFinalTint = new Vector3(1, 1, 1);
            orNext = Next;
            orTex = element;
        }

        public override void Restart()
        {
            base.Restart();
            if (orNext != null)
            {
                totalTime = orTime;
                this.initial = orInitial;
                this.initialTransparency = orTransparency.x;
                this.initialRotation = orRotation.x;
                this.initialColor = orInitialTint;
                this.difference = new Rect(orFinal.x - orInitial.x, orFinal.y - orInitial.y, orFinal.width - orInitial.width, orFinal.height - orInitial.height);
                this.differenceTransparency = orTransparency.y - orTransparency.x;
                this.differenceRotation = orRotation.y - orRotation.x;
                this.differenceColor = orFinalTint - orInitialTint;
                this.e = orTex;
                nextTransition = orNext;
            }
        }

        public override void SetRotation(float initial, float final)
        {
            base.SetRotation(initial, final);

            orRotation = new Vector2(initial, final);
        }

        public override void SetTransparency(float initial, float final)
        {
            base.SetTransparency(initial, final);

            orTransparency = new Vector2(initial, final);
        }

        public override void SetTint(Color initialColor, Color finalColor)
        {
            base.SetTint(initialColor, finalColor);

            orInitialTint = new Vector3(initialColor.r, initialColor.g, initialColor.b);
            orFinalTint = new Vector3(finalColor.r, finalColor.g, finalColor.b);
        }

        public override void DrawPaused()
        {
            Color c = GUI.color;

            Color tint = TintNow();
            GUI.color = new Color(tint.r, tint.g, tint.b, TransparencyNow());

            Rect P = PercentagesNow();

            Vector2 pivotPoint = new Vector2((P.x + P.width / 2f) * Screen.width / 100f, (P.y + P.height / 2f) * Screen.height / 100f);
            GUIUtility.RotateAroundPivot(RotationNow(), pivotPoint);

            DrawTexture(P, e);

            GUIUtility.RotateAroundPivot(-RotationNow(), pivotPoint);

            GUI.color = c;
        }

        public override bool Draw(float deltatime)
        {
            DrawPaused();

            return UpdateTime(deltatime);
        }

        public bool UpdateTime(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= totalTime)
            {
                timer = totalTime;
                if (nextTransition == null)
                    return true;

                LoadNextTransition();
            }
            return false;
        }

        public void LoadNextTransition()
        {
            timer = 0;
            totalTime = nextTransition.totalTime;
            this.initial = nextTransition.initial;
            this.initialTransparency = nextTransition.initialTransparency;
            this.initialRotation = nextTransition.initialRotation;
            this.initialColor = nextTransition.initialColor;
            this.difference = nextTransition.difference;
            this.differenceTransparency = nextTransition.differenceTransparency;
            this.differenceRotation = nextTransition.differenceRotation;
            this.differenceColor = nextTransition.differenceColor;
            this.e = (nextTransition as STextureTransition).e;
            nextTransition = nextTransition.nextTransition;
        }
    }

    /// <summary>
    /// A transition from an initial position, size and transparency to a final with a Text
    /// </summary>
    public class STextTransition : STransition
    {
        private string Text;

        // ORIGINAL
        string orText;
        Vector2 orInitial;
        Vector2 orFinal;
        float orTime;
        Vector2 orTransparency;
        Vector2 orRotation;
        Vector3 orInitialTint;
        Vector3 orFinalTint;
        STextTransition orNext;
        TextBrush orBrush;
        //

        private STextTransition nextTransition;

        private TextBrush brush = new TextBrush(Color.black, 32);
        public TextBrush Brush
        {
            get { return brush;}
            set
            {
                orBrush = new TextBrush(value.TextColor, value.FontSize, value.TextFont);
                brush = value;
            }
        }

        public void SetNextTransition(STextTransition Next)
        {
            nextTransition = Next;
        }

        // Only position
        public STextTransition(string text, Vector2 initial, Vector2 final, float Time) : base(new Rect(initial.x, initial.y, 100, 100), new Rect(final.x, final.y, 100, 100), Time)
        {
            Text = text;
        }

        public STextTransition(string text, Vector2 initial, Vector2 final, float Time, STextTransition Next) : base(new Rect(initial.x, initial.y, 100, 100), new Rect(final.x, final.y, 100, 100), Time)
        {
            Text = text;
            nextTransition = Next;

            orInitial = initial;
            orFinal = final;
            orTime = Time;
            orTransparency = new Vector2(1, 1);
            orRotation = new Vector2(0, 0);
            orInitialTint = orFinalTint = new Vector3(1, 1, 1);
            orNext = Next;
            orText = text;
        }

        public override void Restart()
        {
            base.Restart();
            if (orNext != null)
            {
                totalTime = orTime;
                this.Text = orText;
                this.initial = new Rect(orInitial.x, orInitial.y, 100, 100);
                this.initialTransparency = orTransparency.x;
                this.initialRotation = orRotation.x;
                this.initialColor = orInitialTint;
                this.difference = new Rect(orFinal.x - orInitial.x, orFinal.y - orInitial.y, 0, 0);
                this.differenceTransparency = orTransparency.y - orTransparency.x;
                this.differenceRotation = orRotation.y - orRotation.x;
                this.differenceColor = orFinalTint - orInitialTint;
                Brush = orBrush;
                nextTransition = orNext;
            }
        }

        public override void SetRotation(float initial, float final)
        {
            base.SetRotation(initial, final);

            orRotation = new Vector2(initial, final);
        }

        public override void SetTransparency(float initial, float final)
        {
            base.SetTransparency(initial, final);

            orTransparency = new Vector2(initial, final);
        }

        public override void SetTint(Color initialColor, Color finalColor)
        {
            base.SetTint(initialColor, finalColor);

            orInitialTint = new Vector3(initialColor.r, initialColor.g, initialColor.b);
            orFinalTint = new Vector3(finalColor.r, finalColor.g, finalColor.b);
        }

        public override void DrawPaused()
        {
            Color c = GUI.color;
            Color tc = GUI.skin.label.normal.textColor;

            Color ccc = new Color();
            ccc.r = tc.r;
            ccc.g = tc.g;
            ccc.b = tc.b;
            ccc.a = TransparencyNow();
            GUI.color = ccc;

            Rect P = PercentagesNow();

            Vector2 pivotPoint = new Vector2((P.x + P.width / 2f) * Screen.width / 100f, (P.y + P.height / 2f) * Screen.height / 100f);
            GUIUtility.RotateAroundPivot(RotationNow(), pivotPoint);

            if (Brush.TextFont == null)
                Brush = new TextBrush(Brush.TextColor, Brush.FontSize, GUI.skin.label.font);

            DrawText(P.x, P.y, Text, Brush.FontSize, Brush.TextColor, Brush.TextFont);

            GUIUtility.RotateAroundPivot(-RotationNow(), pivotPoint);

            GUI.color = c;
        }

        public override bool Draw(float deltatime)
        {
            DrawPaused();

            return UpdateTime(deltatime);
        }

        public bool UpdateTime(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= totalTime)
            {
                timer = totalTime;
                if (nextTransition == null)
                    return true;

                LoadNextTransition();
            }
            return false;
        }

        public void LoadNextTransition()
        {
            timer = 0;
            orBrush = new TextBrush(Brush.TextColor, Brush.FontSize, Brush.TextFont);
            totalTime = nextTransition.totalTime;
            this.initial = nextTransition.initial;
            this.initialTransparency = nextTransition.initialTransparency;
            this.initialRotation = nextTransition.initialRotation;
            this.initialColor = nextTransition.initialColor;
            this.difference = nextTransition.difference;
            this.differenceTransparency = nextTransition.differenceTransparency;
            this.differenceRotation = nextTransition.differenceRotation;
            this.differenceColor = nextTransition.differenceColor;
            this.Text = (nextTransition as STextTransition).Text;
            this.Brush = nextTransition.Brush;
            nextTransition = nextTransition.nextTransition;
        }
    }

    /// <summary>
    /// A transition from an initial position, size and transparency to a final with a SSprite
    /// </summary>
    public class SSpriteTransition : STransition
    {
        private SSprite e;

        // ORIGINAL
        Vector2 orInitial;
        Vector2 orFinal;
        float orTime;
        Vector2 orTransparency;
        Vector2 orRotation;
        Vector3 orInitialTint;
        Vector3 orFinalTint;
        SSpriteTransition orNext;
        SSprite orSprite;
        //

        private SSpriteTransition nextTransition;

        public void SetNextTransition(SSpriteTransition Next)
        {
            nextTransition = Next;
        }

        public SSpriteTransition(SSprite element, Rect initial, Rect final, float Time) : base(initial, final, Time)
        {
            e = element;
        }

        public SSpriteTransition(SSprite element, Rect initial, Rect final, float Time, SSpriteTransition Next) : base(initial, final, Time)
        {
            e = element;
            nextTransition = Next;
        }

        public override void Restart()
        {
            base.Restart();
            if (orNext != null)
            {
                totalTime = orTime;
                this.e = orSprite;
                this.initial = new Rect(orInitial.x, orInitial.y, 100, 100);
                this.initialTransparency = orTransparency.x;
                this.initialRotation = orRotation.x;
                this.initialColor = orInitialTint;
                this.difference = new Rect(orFinal.x - orInitial.x, orFinal.y - orInitial.y, 0, 0);
                this.differenceTransparency = orTransparency.y - orTransparency.x;
                this.differenceRotation = orRotation.y - orRotation.x;
                this.differenceColor = orFinalTint - orInitialTint;
                nextTransition = orNext;
            }
        }

        public override void SetRotation(float initial, float final)
        {
            base.SetRotation(initial, final);

            orRotation = new Vector2(initial, final);
        }

        public override void SetTransparency(float initial, float final)
        {
            base.SetTransparency(initial, final);

            orTransparency = new Vector2(initial, final);
        }

        public override void SetTint(Color initialColor, Color finalColor)
        {
            base.SetTint(initialColor, finalColor);

            orInitialTint = new Vector3(initialColor.r, initialColor.g, initialColor.b);
            orFinalTint = new Vector3(finalColor.r, finalColor.g, finalColor.b);
        }

        public override void DrawPaused()
        {
            Color c = GUI.color;

            Color tint = TintNow();
            GUI.color = new Color(tint.r, tint.g, tint.b, TransparencyNow());

            Rect P = PercentagesNow();

            Vector2 pivotPoint = new Vector2((P.x + P.width / 2f) * Screen.width / 100f, (P.y + P.height / 2f) * Screen.height / 100f);
            GUIUtility.RotateAroundPivot(RotationNow(), pivotPoint);

            e.Percentages = P;
            e.Draw(0);

            GUIUtility.RotateAroundPivot(-RotationNow(), pivotPoint);

            GUI.color = c;
        }

        public override bool Draw(float deltatime)
        {
            Color c = GUI.color;

            Color tint = TintNow();
            GUI.color = new Color(tint.r, tint.g, tint.b, TransparencyNow());

            Rect P = PercentagesNow();

            Vector2 pivotPoint = new Vector2((P.x + P.width / 2f) * Screen.width / 100f, (P.y + P.height / 2f) * Screen.height / 100f);
            GUIUtility.RotateAroundPivot(RotationNow(), pivotPoint);

            e.Percentages = P;
            e.Draw(Time.deltaTime);

            GUIUtility.RotateAroundPivot(-RotationNow(), pivotPoint);

            GUI.color = c;

            return UpdateTime(deltatime);
        }

        public bool UpdateTime(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= totalTime)
            {
                timer = totalTime;
                if (nextTransition == null)
                    return true;

                LoadNextTransition();
            }
            return false;
        }

        public void LoadNextTransition()
        {
            timer = 0;
            totalTime = nextTransition.totalTime;
            this.initial = nextTransition.initial;
            this.initialTransparency = nextTransition.initialTransparency;
            this.initialRotation = nextTransition.initialRotation;
            this.initialColor = nextTransition.initialColor;
            this.difference = nextTransition.difference;
            this.differenceTransparency = nextTransition.differenceTransparency;
            this.differenceRotation = nextTransition.differenceRotation;
            this.differenceColor = nextTransition.differenceColor;
            this.e = (nextTransition as SSpriteTransition).e;
            nextTransition = nextTransition.nextTransition;
        }
    }

    #endregion
    //----------------------------
    #region Draw Classes
    public abstract class SElement
    {
        #region Attributes

        public string Name = "";
        public string Tag = "";

        #region Percentages
        private Rect percentages = new Rect(0,0,0,0);
        public Rect Percentages {
            get { return new Rect(percentages.x, percentages.y, percentages.width, percentages.height); }
            set
            {
                percentages = value;
                position = new Vector2(percentages.x * Screen.width / 100f, percentages.y * Screen.height / 100f);
                size = new Vector2(percentages.width * Screen.width / 100f, percentages.height * Screen.height / 100f);
            }
        }
        public float XPerCent
        {
            get { return percentages.x; }
            set
            {
                percentages = new Rect(value, percentages.y, percentages.width, percentages.height);
                float xx = layer.Position.x + percentages.x * layer.WidthPerCent / 100f;
                float yy = layer.Position.y + percentages.y * layer.HeightPerCent / 100f;
                position = new Vector2(xx * Screen.width / 100f, yy * Screen.height / 100f);
            }
        }
        public float YPerCent
        {
            get { return percentages.y; }
            set
            {
                percentages = new Rect(percentages.x, value, percentages.width, percentages.height);
                float xx = layer.Position.x + percentages.x * layer.WidthPerCent / 100f;
                float yy = layer.Position.y + percentages.y * layer.HeightPerCent / 100f;
                position = new Vector2(xx * Screen.width / 100f, yy * Screen.height / 100f);
            }
        }
        public float WidthPerCent
        {
            get { return percentages.width; }
            set
            {
                percentages = new Rect(percentages.x, percentages.y, value, percentages.height);
                float wwidth = percentages.width * layer.WidthPerCent / 100f;
                float hheight = percentages.height * layer.HeightPerCent / 100f;
                size = new Vector2(wwidth * Screen.width / 100f, hheight * Screen.height / 100f);
            }
        }
        public float HeightPerCent
        {
            get { return percentages.height; }
            set
            {
                percentages = new Rect(percentages.x, percentages.y, percentages.width, value);
                float wwidth = percentages.width * layer.WidthPerCent / 100f;
                float hheight = percentages.height * layer.HeightPerCent / 100f;
                size = new Vector2(wwidth * Screen.width / 100f, hheight * Screen.height / 100f);
            }
        }
        #endregion

        #region Pixels
        private Vector2 position;
        /// <summary>
        /// Current position of the texture in pixels
        /// </summary>
        public Vector2 Position { get { return position; } }

        private Vector2 size;
        /// <summary>
        /// Current size of the texture in pixels
        /// </summary>
        public Vector2 Size { get { return size; } }
        #endregion

        private SLayer layer;
        public SLayer Layer { get { return layer; } }
        public void SetLayer(SLayer Layer) 
        {
            layer = Layer;
            Recalculate();
        }
        public void RemoveLayer()
        {
            layer = new SLayer(0, 0, 100, 100);
        }

        private bool over = false;
        public bool IsOver { get { return over; } }
        public void CancelOverTracking () { over = false; }

        public bool MouseOver()
        {
            Vector2 mousePosition = Event.current.mousePosition;

            if (mousePosition.x < Position.x)
            {
                over = false;
                return over;
            }
            if (mousePosition.y < Position.y)
            {
                over = false;
                return over;
            }
            if (mousePosition.x > Position.x + Size.x)
            {
                over = false;
                return over;
            }
            if (mousePosition.y > Position.y + Size.y)
            {
                over = false;
                return over;
            }

            over = true;
            return true;
        }

        public bool TappedOn
        {
            get
            {
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    Touch t = Input.GetTouch(i);
                    Rect area = new Rect(Position.x, Position.y, Position.x + Size.x, Position.y + Size.y);
                    if (t.position.x > area.x && t.position.x < area.width && t.position.y > area.y && t.position.y < area.height)
                        return true;
                }
                return false;
            }
        }

        public List<int> Touched
        {
            get
            {
                List<int> touches = new List<int>();

                if (Input.touchCount == 0) return touches;
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    Vector2 mp = Input.GetTouch(i).position;
                    mp = new Vector2(mp.x / Screen.width * 100f, (1 - mp.y / Screen.height) * 100f);
                    if (PixelInPercentages(mp, Percentages)) touches.Add(i);
                }
                return touches;
            }
        }
        #endregion

        #region Methods
        public virtual void Recalculate()
        {
            float xx = layer.Position.x + percentages.x * layer.WidthPerCent / 100f;
            float yy = layer.Position.y + percentages.y * layer.HeightPerCent / 100f;
            float wwidth = percentages.width * layer.WidthPerCent / 100f;
            float hheight = percentages.height * layer.HeightPerCent / 100f;

            position = new Vector2(xx * Screen.width / 100f, yy * Screen.height / 100f);
            size = new Vector2(wwidth * Screen.width / 100f, hheight * Screen.height / 100f);
        }
        #endregion

        #region Constructors
        internal SElement(float x, float y, float width, float height)
        {
            SetLayer(new SLayer(0, 0, 100, 100));
            percentages = new Rect(x, y, width, height);
            Recalculate();
        }
        internal SElement(Rect Percentages)
        {
            SetLayer(new SLayer(0, 0, 100, 100));
            percentages = Percentages;
            Recalculate();
        }
        internal SElement(SLayer Layer)
        {
            SetLayer(Layer);
            percentages = new Rect(0,0,100,100);
            Recalculate();
        }
        internal SElement(SLayer Layer, float x, float y, float width, float height)
        {
            SetLayer(Layer);
            percentages = new Rect(x, y, width, height);
            Recalculate();
        }
        internal SElement(SLayer Layer, Rect Percentages)
        {
            SetLayer(Layer);
            percentages = Percentages;
            Recalculate();
        }
        #endregion
    }

    public class STexture : SElement
    {
        #region Attributes

        public Texture2D Texture;
        private bool pushed = false;
        private bool hold = false;

        #endregion

        #region Constructors
        public STexture(float x, float y, float width, float height, Texture2D texture) : base(x,y,width, height)
        {
            Texture = texture;
        }
        public STexture(Rect Percentages, Texture2D texture) : base (Percentages)
        {
            Texture = texture;
        }
        public STexture(SLayer Layer, Texture2D texture) : base (Layer)
        {
            Texture = texture;
        }
        public STexture(SLayer Layer, float x, float y, float width, float height, Texture2D texture) : base (Layer, x, y, width, height)
        {
            Texture = texture;
        }
        public STexture(SLayer Layer, Rect Percentages, Texture2D texture) : base (Layer, Percentages)
        {
            Texture = texture;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draw texture.
        /// </summary>
        public void Draw()
        {
            GUI.DrawTexture(new Rect(Position.x, Position.y, Size.x, Size.y), Texture);
        }
        /// <summary>
        /// Draw the texture recalculating always position and size with percentages
        /// </summary>
        public void DrawUpdate()
        {
            DrawTexture(Layer, Percentages, Texture);
        }
        
        /// <summary>
        /// Draw texture and check if mouse is over it (OnGUI Function)
        /// </summary>
        /// <returns></returns>
        public bool DrawOver()
        {
            Draw();
            return MouseOver();
        }

        /// <summary>
        /// Draw texture and check if it's being clicked
        /// </summary>
        /// <returns></returns>
        public bool DrawPushed()
        {
            Draw();
            switch (pushed)
            {
                case true:
                    if (hold && (Input.GetKeyUp(KeyCode.Mouse0) || !Input.GetKey(KeyCode.Mouse0)))
                    {
                        hold = false;
                        pushed = false;
                    }
                    return pushed;
                case false:
                    if (MouseOver() && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        hold = true;
                        pushed = true;
                    }
                    return pushed;
            }
			return false;
        }
        
        /// <summary>
        /// Draw the texture and check if it's being clicked meanwhile
        /// </summary>
        /// <returns></returns>
        public bool DrawHold()
        {
            Draw();
            switch (hold)
            {
                case true:
                    if (Input.GetKeyUp(KeyCode.Mouse0) || !Input.GetKey(KeyCode.Mouse0))
                        hold = false;
                    return hold;
                case false:
                    if (MouseOver() && Input.GetKeyDown(KeyCode.Mouse0))
                        hold = true;
                    return hold;
            }
			return false;
        }

        /// <summary>
        /// Is the texture being tapped? (Mobile)
        /// </summary>
        /// <returns>True if tapped</returns>
        public bool DrawTapped()
        {
            Draw();
            return TappedOn;
        }

        /// <summary>
        /// Is the texture being touched? (Mobile)
        /// </summary>
        /// <returns>List of the touches indices touching the texture.</returns>
        public List<int> DrawTouched()
        {
            Draw();
            return Touched;
        }
        #endregion
    }
    //----------------------------
    public class STileset : SElement
    {
        #region Attributes
        private Texture2D Texture;

        private float spaceX, spaceY;
        private int rows, columns;
        private Vector2 fieldSize;
        public Vector2 FieldSize { get { return new Vector2(fieldSize.x, fieldSize.y); } }
        public Vector2 Dimensions { get { return new Vector2 (rows, columns);}}
        public Vector2 Spaces { get { return new Vector2(spaceX, spaceY); } }

        private bool pushed = false;
        private bool hold = false;

        /// <summary>
        /// Set rows and columns for the Tileset
        /// </summary>
        /// <param name="Rows"></param>
        /// <param name="Columns"></param>
        public void SetDimensions (int Rows, int Columns)
        {
            rows = Rows;
            columns = Columns;

            float width = (Size.x - (spaceX * (columns-1))) / columns;
            float height = (Size.y - (spaceY * (rows - 1))) / rows;
            fieldSize = new Vector2(width, height);
        }
        public void SetSpacesPercentages(float SpaceX, float SpaceY)
        {
            spaceX = SpaceX * Size.x / 100f;
            spaceY = SpaceY * Size.y / 100f;
            spaceX /= (rows - 1);
            spaceY /= (columns - 1);

            SetDimensions(rows, columns);
        }

        public override void Recalculate()
        {
            base.Recalculate();
            Vector2 D = Dimensions;
            SetDimensions((int)D.x, (int)D.y);
        }

        #endregion

        #region Constructors
        // Without spaces
        public STileset(float x, float y, float width, float height, Texture2D texture, int Rows, int Columns) : base (x,y,width,height)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;

            SetSpacesPercentages(0, 0);
        }
        public STileset(Rect Percentages, Texture2D texture, int Rows, int Columns) : base (Percentages)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;
            SetSpacesPercentages(0, 0);
        }
        public STileset(SLayer Layer, Texture2D texture, int Rows, int Columns) : base (Layer)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;
            SetSpacesPercentages(0, 0);
        }
        public STileset(SLayer Layer, float x, float y, float width, float height, Texture2D texture, int Rows, int Columns) : base (Layer, x, y, width, height)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;
            SetSpacesPercentages(0, 0);
        }
        public STileset(SLayer Layer, Rect Percentages, Texture2D texture, int Rows, int Columns) : base (Layer, Percentages)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;
            SetSpacesPercentages(0, 0);
        }
        // With spaces
        public STileset(float x, float y, float width, float height, Texture2D texture, int Rows, int Columns, float SpaceX, float SpaceY)
            : base(x, y, width, height)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;

            SetSpacesPercentages(SpaceX, SpaceY);
        }
        public STileset(Rect Percentages, Texture2D texture, int Rows, int Columns, float SpaceX, float SpaceY)
            : base(Percentages)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;
            SetSpacesPercentages(SpaceX, SpaceY);
        }
        public STileset(SLayer Layer, Texture2D texture, int Rows, int Columns, float SpaceX, float SpaceY)
            : base(Layer)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;
            SetSpacesPercentages(SpaceX, SpaceY);
        }
        public STileset(SLayer Layer, float x, float y, float width, float height, Texture2D texture, int Rows, int Columns, float SpaceX, float SpaceY)
            : base(Layer, x, y, width, height)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;
            SetSpacesPercentages(SpaceX, SpaceY);
        }
        public STileset(SLayer Layer, Rect Percentages, Texture2D texture, int Rows, int Columns, float SpaceX, float SpaceY)
            : base(Layer, Percentages)
        {
            if (Rows <= 0 || Columns <= 0) throw new UnityException("Wrong number of rows or columns in STileset");
            Texture = texture;
            rows = Rows;
            columns = Columns;
            SetSpacesPercentages(SpaceX, SpaceY);
        }
        #endregion

        #region Methods
        public void Draw()
        {
            for (int currentRow = 0; currentRow < rows; ++currentRow)
            {
                for (int currentColumn = 0; currentColumn < columns; ++currentColumn)
                    GUI.DrawTexture(new Rect(Position.x + currentColumn * (fieldSize.x + spaceX), Position.y + currentRow * (fieldSize.y + spaceY), fieldSize.x, fieldSize.y), Texture);
            }
        }

        public void DrawUpdate()
        {
            DrawTileset(Layer, Percentages, Texture, rows, columns);
        }

        /// <summary>
        /// Draw tileset and check if mouse is over it (OnGUI Function)
        /// </summary>
        /// <returns></returns>
        public bool DrawOver()
        {
            Draw();
            return MouseOver();
        }

        /// <summary>
        /// Draw tileset and check if it's being clicked
        /// </summary>
        /// <returns></returns>
        public bool DrawPushed()
        {
            Draw();
            switch (pushed)
            {
                case true:
                    if (hold && (Input.GetKeyUp(KeyCode.Mouse0) || !Input.GetKey(KeyCode.Mouse0)))
                    {
                        hold = false;
                        pushed = false;
                    }
                    return pushed;
                case false:
                    if (MouseOver() && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        hold = true;
                        pushed = true;
                    }
                    return pushed;
            }
            return false;
        }

        /// <summary>
        /// Draw the tileset and check if it's being clicked meanwhile
        /// </summary>
        /// <returns></returns>
        public bool DrawHold()
        {
            Draw();
            switch (hold)
            {
                case true:
                    if (Input.GetKeyUp(KeyCode.Mouse0) || !Input.GetKey(KeyCode.Mouse0))
                        hold = false;
                    return hold;
                case false:
                    if (MouseOver() && Input.GetKeyDown(KeyCode.Mouse0))
                        hold = true;
                    return hold;
            }
            return false;
        }

        /// <summary>
        /// Is the tileset being tapped? (Mobile)
        /// </summary>
        /// <returns>True if being tapped.</returns>
        public bool DrawTapped()
        {
            Draw();
            return TappedOn;
        }

        /// <summary>
        /// Is the tileset being touched? (Mobile)
        /// </summary>
        /// <returns>List of the touches indices touching the tileset.</returns>
        public List<int> DrawTouched()
        {
            Draw();
            return Touched;
        }
        #endregion
    }
    //----------------------------
    public class SText : SElement
    {
        #region Attributes
        public string Text;

        public static TextBrush DefaultBrush;
        public TextBrush Brush;

        #endregion

        #region Constructors
        public SText(float x, float y, string text) : base (x,y,100,100)
        {
            Text = text;
        }
        public SText(Vector2 position, string text) : base (new Rect(position.x, position.y, 100, 100))
        {
            Text = text;
        }
        public SText(SLayer Layer, string text) : base (Layer)
        {
            Text = text;
        }
        public SText(SLayer Layer, float x, float y, string text) : base (Layer, x, y, 100, 100)
        {
            Text = text;
        }
        public SText(SLayer Layer, Vector2 position, string text) : base (Layer, new Rect (position.x, position.y, 100, 100))
        {
            Text = text;
        }
        #endregion

        #region Methods
        public void Draw()
        {
            CheckStyle();

			Color currentColor = GUI.skin.label.normal.textColor;
			int currentFontSize = GUI.skin.label.fontSize;
			Font currentFont = GUI.skin.label.font;

			GUI.skin.label.normal.textColor = Brush.TextColor;
			GUI.skin.label.fontSize = Brush.FontSize;
			GUI.skin.label.font = Brush.TextFont;

            GUI.Label(new Rect(Position.x, Position.y, Size.x, Size.y), Text);

			GUI.skin.label.normal.textColor = currentColor;
			GUI.skin.label.fontSize = currentFontSize;
			GUI.skin.label.font = currentFont;
        }

        public void DrawUpdate()
        {
            ResetStyle();

            Font currentFont = GUI.skin.label.font;

            GUI.skin.label.font = Brush.TextFont;

            DrawText(Layer, new Vector2(Percentages.x, Percentages.y), Text, Brush.FontSize, Brush.TextColor);

            GUI.skin.label.font = currentFont;
        }

        public void ResetStyle()
        {
            Brush = null;
            CheckStyle();
        }

        private void CheckStyle()
        {
            if (Brush != null) return;

            if (DefaultBrush == null)
                DefaultBrush = new TextBrush(GUI.skin.label.normal.textColor, GUI.skin.label.fontSize, GUI.skin.label.font);
            if (DefaultBrush.TextFont == null)
                DefaultBrush.TextFont = GUI.skin.label.font;
            Brush = new TextBrush(DefaultBrush.TextColor, DefaultBrush.FontSize, DefaultBrush.TextFont);
        }
        #endregion
    }
    //----------------------------
    public class SButton : SElement
    {
        #region Attributes

        private string text = "";
        private Texture2D texture;
        public string Text {
            get{
                return text;
            }
            set{
                text = value;
                TextureText = false;
            }
        }
        public Texture2D Texture {
            get{
                return texture;
            }
            set{
                texture = value;
                TextureText = true;
            }
        }

        public TextBrush Brush;

        private bool TextureText = false;

        private bool pushed = false;
        public bool Pushed() { return pushed; }

        #endregion

        #region Constructors
        // WITH TEXT
		public SButton(float x, float y, float width, float height, string text) : base (x,y,width,height)
        {
            TextureText = false;
            this.text = text;
        }
        public SButton(Rect Percentages, string text) : base(Percentages)
        {
            TextureText = false;
            this.text = text;
        }
        public SButton(SLayer layer, string text) : base (layer)
        {
            TextureText = false;
            this.text = text;
        }
		public SButton(SLayer layer, float x, float y, float width, float height, string text) : base (layer, x, y, width, height)
        {
            TextureText = false;
            this.text = text;
        }
        public SButton(SLayer layer, Rect Percentages, string text) : base(layer, Percentages)
        {
            TextureText = false;
            this.text = text;
        }
        // WITH TEXTURE
		public SButton(float x, float y, float width, float height, Texture2D texture) : base (x,y,width,height)
        {
            TextureText = true;
            this.texture = texture;
        }
        public SButton(Rect Percentages, Texture2D texture) : base(Percentages)
        {
            TextureText = true;
            this.texture = texture;
        }
        public SButton(SLayer layer, Texture2D texture) : base (layer)
        {
            TextureText = true;
            this.texture = texture;
        }
		public SButton(SLayer layer, float x, float y, float width, float height, Texture2D texture) : base (layer, x,y,width,height)
        {
            TextureText = true;
            this.texture = texture;
        }
        public SButton(SLayer layer, Rect Percentages, Texture2D texture) : base(layer, Percentages)
        {
            TextureText = true;
            this.texture = texture;
        }
        #endregion

        #region Methods
        public void ResetStyle()
        {
            Brush = null;
            CheckStyle();
        }

        private void CheckStyle()
        {
            if (Brush == null)
                Brush = new TextBrush(GUI.skin.button.normal.textColor, GUI.skin.button.fontSize, GUI.skin.button.font);
        }

        public bool Draw()
        {
            if (!TextureText)
            {
                CheckStyle();

                Color currentColor = GUI.skin.button.normal.textColor;
                int currentFontSize = GUI.skin.button.fontSize;
                Font currentFont = GUI.skin.button.font;

                GUI.skin.button.fontSize = Brush.FontSize;
                GUI.skin.button.font = Brush.TextFont;
                GUI.skin.button.normal.textColor = Brush.TextColor;

                pushed = GUI.Button(new Rect(Position.x, Position.y, Size.x, Size.y), Text);

                GUI.skin.button.fontSize = currentFontSize;
                GUI.skin.button.font = currentFont;
                GUI.skin.button.normal.textColor = currentColor;

                return pushed;
            }
            else
            {
                pushed = GUI.Button(new Rect(Position.x, Position.y, Size.x, Size.y), texture);

                return pushed;
            }
        }

        public bool DrawUpdate()
        {
            if (!TextureText)
            {
                CheckStyle();

                Color currentColor = GUI.skin.button.normal.textColor;
                int currentFontSize = GUI.skin.button.fontSize;
                Font currentFont = GUI.skin.button.font;

                GUI.skin.button.fontSize = Brush.FontSize;
                GUI.skin.button.font = Brush.TextFont;
                GUI.skin.button.normal.textColor = Brush.TextColor;

                pushed = DrawButton(Layer, Percentages, text);

                GUI.skin.button.fontSize = currentFontSize;
                GUI.skin.button.font = currentFont;
                GUI.skin.button.normal.textColor = currentColor;

                return pushed;
            }
            else
            {
                pushed = DrawButton(Layer, Percentages, texture);

                return pushed;
            }
        }
        #endregion
    }
    //----------------------------
    public class SBox : SElement
    {
        #region Atributes
        private string text = "";
        private Texture2D texture;
        public string Text {
            get{
                return text;
            }
            set{
                text = value;
                TextureText = false;
            }
        }
        public Texture2D Texture {
            get{
                return texture;
            }
            set{
                texture = value;
                TextureText = true;
            }
        }

        public TextBrush Brush;

        private bool TextureText = false;

        private bool pushed = false;
        private bool hold = false;

        #endregion

        #region Constructors
        public SBox(string text) : base (0,0,100,100)
        {
            TextureText = false;
            this.text = text;
        }
        public SBox(float x, float y, float width, float height, string text) : base(x, y, width, height)
        {
            TextureText = false;
            this.text = text;
        }
        public SBox(Rect Percentages, string text) : base(Percentages)
        {
            TextureText = false;
            this.text = text;
        }
        public SBox(SLayer layer, string text) : base (layer)
        {
            TextureText = false;
            this.text = text;
        }
        public SBox(SLayer layer, float x, float y, float width, float height, string text) : base(layer, x, y, width, height)
        {
            TextureText = false;
            this.text = text;
        }
        public SBox(SLayer layer, Rect Percentages, string text) : base(layer, Percentages)
        {
            TextureText = false;
            this.text = text;
        }
        public SBox(Texture2D texture) : base (0,0,100,100)
        {
            TextureText = true;
            this.texture = texture;
        }
        public SBox(float x, float y, float width, float height, Texture2D texture) : base(x, y, width, height)
        {
            TextureText = true;
            this.texture = texture;
        }
        public SBox(SLayer layer, Texture2D texture) : base (layer)
        {
            TextureText = true;
            this.texture = texture;
        }
        public SBox(SLayer layer, float x, float y, float width, float height, Texture2D texture) : base(layer, x, y, width, height)
        {
            TextureText = true;
            this.texture = texture;
        }
        #endregion

        #region Methods
        public void ResetStyle()
        {
            Brush = null;
            CheckStyle();
        }

        private void CheckStyle()
        {
            if (Brush == null)
                Brush = new TextBrush(GUI.skin.box.normal.textColor, GUI.skin.box.fontSize, GUI.skin.box.font);
        }

        public void Draw()
        {
            if (!TextureText)
            {
                CheckStyle();

                Color currentColor = GUI.skin.box.normal.textColor;
                int currentFontSize = GUI.skin.box.fontSize;
                Font currentFont = GUI.skin.box.font;

                GUI.skin.box.fontSize = Brush.FontSize;
				GUI.skin.box.font = Brush.TextFont;
				GUI.skin.box.normal.textColor = currentColor;

                GUI.Box(new Rect(Percentages.x, Percentages.y, Percentages.width, Percentages.height), text);

				GUI.skin.box.fontSize = currentFontSize;
				GUI.skin.box.font = currentFont;
				GUI.skin.box.normal.textColor = currentColor;
            }
            else
            {
                GUI.Box(new Rect(Percentages.x, Percentages.y, Percentages.width, Percentages.height), texture);
            }
        }

        public void DrawUpdate()
        {
            if (!TextureText)
            {
                CheckStyle();

                Color currentColor = GUI.skin.box.normal.textColor;
                int currentFontSize = GUI.skin.box.fontSize;
                Font currentFont = GUI.skin.box.font;

                GUI.skin.box.fontSize = Brush.FontSize;
                GUI.skin.box.font = Brush.TextFont;
                GUI.skin.box.normal.textColor = currentColor;

                DrawBox(Layer, Percentages, text);

                GUI.skin.box.fontSize = currentFontSize;
                GUI.skin.box.font = currentFont;
                GUI.skin.box.normal.textColor = currentColor;
            }
            else
            {
                DrawBox(Layer, Percentages, texture);
            }
        }

        public bool DrawOver()
        {
            Draw();
            return MouseOver();
        }

        public bool DrawPushed()
        {
            Draw();
            switch (pushed)
            {
                case true:
                    if (hold && (Input.GetKeyUp(KeyCode.Mouse0) || !Input.GetKey(KeyCode.Mouse0)))
                    {
                        hold = false;
                        pushed = false;
                    }
                    return pushed;
                    break;
                case false:
                    if (MouseOver() && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        hold = true;
                        pushed = true;
                    }
                    return pushed;
                    break;
            }
            return false;
        }

        public bool DrawHold()
        {
            Draw();
            switch (hold)
            {
                case true:
                    if (Input.GetKeyUp(KeyCode.Mouse0) || !Input.GetKey(KeyCode.Mouse0))
                        hold = false;
                    return hold;
                    break;
                case false:
                    if (MouseOver() && Input.GetKeyDown(KeyCode.Mouse0))
                        hold = true;
                    return hold;
                    break;
            }
            return false;
        }

        public List<int> DrawTouched()
        {
            Draw();
            return Touched;
        }

        #endregion
    }
    //----------------------------
    public class SListBox : SElement
    {
        #region Attibutes
        private List<string> options = new List<string>();

        private Vector2 scrollPosition = Vector2.zero;
        private int selected = 0;
        public int Selected { get { return selected; } }

        private GUIStyle notSelectedStyle;
        private GUIStyle selectedStyle;
        private bool StyleSet = false;

        private Texture2D notSelectedTexture;
        public Texture2D NotSelectedTexture {
            get
            {
                return notSelectedTexture;
            }
            set
            {
                StyleSet = false;
                notSelectedTexture = value;
            }
        }
        private Texture2D selectedTexture;
        public Texture2D SelectedTexture
        {
            get
            {
                return selectedTexture;
            }
            set
            {
                StyleSet = false;
                selectedTexture = value;
            }
        }
        private TextBrush notSelectedBrush;
        private TextBrush selectedBrush;
        public TextBrush NotSelectedBrush
        {
            get { return notSelectedBrush; }
            set
            {
                StyleSet = false;
                notSelectedBrush = value;
            }
        }
        public TextBrush SelectedBrush
        {
            get { return selectedBrush; }
            set
            {
                StyleSet = false;
                selectedBrush = value;
            }
        }
        
        #endregion

        #region Constructors
        public SListBox(float x, float y, float width, float height, params string[] options) : base(x, y, width, height)
        {
            foreach (string s in options)
            {
                this.options.Add(s);
            }
        }

        public SListBox(Rect percentages, params string[] options) : base (percentages)
        {
            foreach (string s in options)
            {
                this.options.Add(s);
            }
        }

        public SListBox(SLayer layer, params string[] options) : base (layer)
        {
            foreach (string s in options)
            {
                this.options.Add(s);
            }
        }

        public SListBox(SLayer layer, Rect percentages, params string[] options)
            : base(layer, percentages)
        {
            foreach (string s in options)
            {
                this.options.Add(s);
            }
        }

        public SListBox(SLayer layer, float x, float y, float width, float height, params string[] options)
            : base(layer, x, y, width, height)
        {
            foreach (string s in options)
            {
                this.options.Add(s);
            }
        }

        #endregion

        #region Methods
        private void buildStyle()
        {
            selectedStyle = new GUIStyle();
            if (SelectedTexture == null)
            {
                Texture2D bg = ColorTexture(Color.green);
                selectedStyle.normal.background = bg;
            }
            else
                selectedStyle.normal.background = SelectedTexture;
            selectedStyle.alignment = TextAnchor.MiddleCenter;

            if (selectedBrush == null)
                selectedBrush = new TextBrush(Color.black, 12);
            selectedStyle.normal.textColor = selectedBrush.TextColor;
            selectedStyle.fontSize = selectedBrush.FontSize;
            if (selectedBrush.TextFont != null)
                selectedStyle.font = selectedBrush.TextFont;

            notSelectedStyle = new GUIStyle(GUI.skin.button);
            notSelectedStyle.alignment = TextAnchor.MiddleCenter;
            if (NotSelectedTexture != null)
            {
                notSelectedStyle.normal.background = NotSelectedTexture;
                notSelectedStyle.active.background = NotSelectedTexture;
                notSelectedStyle.hover.background = NotSelectedTexture;
                notSelectedStyle.focused.background = NotSelectedTexture;
            }

            if (notSelectedBrush == null)
                notSelectedBrush = new TextBrush(Color.black, 12);
            notSelectedStyle.normal.textColor = notSelectedBrush.TextColor;
            notSelectedStyle.fontSize = notSelectedBrush.FontSize;
            if (notSelectedBrush.TextFont != null)
                notSelectedStyle.font = notSelectedBrush.TextFont;
        }

        public void SetOptions(params string[] options)
        {
            this.options = new List<string>();
            foreach (string s in options)
                this.options.Add(s);
        }

        public void AddOption(string option) { options.Add(option); }

        public void Draw()
        {
            if (!StyleSet)
            {
                buildStyle();
                StyleSet = true;
            }

            float multiplierX = 1 - (options.Count * 2 / 100f);
            float multiplierY = options.Count / 2f;

            Rect Area = new Rect(Position.x, Position.y, Size.x, Size.y);
            scrollPosition = GUI.BeginScrollView(Area, scrollPosition, new Rect(0, 0, multiplierX*Area.width, multiplierY * Area.height));
            for (int i = 0; i < options.Count; ++i)
            {
                float height = Area.height / 2;
                if (selected != i)
                {
                    if (GUI.Button(new Rect(0, height * i, Area.width * 0.99f, height), options[i], notSelectedStyle)) selected = i;
                }
                else
                {
                    if (GUI.Button(new Rect(0, height * i, Area.width * 0.99f, height), options[i], selectedStyle)) selected = i;
                }
            }
            GUI.EndScrollView();
        }

        public void DrawUpdate()
        {
            if (!StyleSet)
            {
                buildStyle();
                StyleSet = true;
            }

            float multiplierX = 1 - (options.Count * 2 / 100f);
            float multiplierY = options.Count / 2f;

            Rect Area = new Rect(Screen.width * Percentages.x / 100f, Screen.height * Percentages.y / 100f, Screen.width * Percentages.width / 100f, Screen.height * Percentages.height / 100f);
            scrollPosition = GUI.BeginScrollView(Area, scrollPosition, new Rect(0, 0, multiplierX * Area.width, multiplierY * Area.height));
            for (int i = 0; i < options.Count; ++i)
            {
                float height = Area.height / 2;
                if (selected != i)
                {
                    if (GUI.Button(new Rect(0, height * i, Area.width * 0.99f, height), options[i])) selected = i;
                }
                else
                {
                    if (GUI.Button(new Rect(0, height * i, Area.width * 0.99f, height), options[i], selectedStyle)) selected = i;
                }
            }
            GUI.EndScrollView();
        }
        #endregion
    }
    //----------------------------
    public class STextField : SElement
    {
        #region Attributes
        public string Text = "";
        public TextBrush Brush;

        public int MaxLength = 0;
        #endregion

        #region Constructors
        public STextField(float x, float y, float width, float height, int maxLength) : base (x,y,width,height)
        {
            MaxLength = maxLength;
        }
        public STextField(Rect Percentages, int maxLength) : base (Percentages)
        {
            MaxLength = maxLength;
        }
        public STextField(SLayer Layer, int maxLength) : base (Layer)
        {
            MaxLength = maxLength;
        }
        public STextField(SLayer Layer, float x, float y, float width, float height, int maxLength) : base (Layer, x, y, width, height)
        {
            MaxLength = maxLength;
        }
        public STextField(SLayer Layer, Rect Percentages, int maxLength) : base (Layer, Percentages)
        {
            MaxLength = maxLength;
        }
        #endregion

        #region Methods
        private void CheckStyle()
        {
            if (Brush == null)
                Brush = new TextBrush(GUI.skin.textField.normal.textColor, GUI.skin.textField.fontSize, GUI.skin.textField.font);
        }

        public void ResetStyle()
        {
            Brush = null;
            CheckStyle(); 
        }
        
        public string Draw(string text)
        {
            this.Text = text;

            CheckStyle();

            Color currentColor = GUI.skin.textField.normal.textColor;
            int currentFontSize = GUI.skin.textField.fontSize;
            Font currentFont = GUI.skin.textField.font;

            GUI.skin.textField.normal.textColor = Brush.TextColor;
            GUI.skin.textField.fontSize = Brush.FontSize;
            GUI.skin.textField.font = Brush.TextFont;

            Text = GUI.TextField(new Rect(Position.x, Position.y, Size.x, Size.y), Text, MaxLength);

            GUI.skin.textField.normal.textColor = currentColor;
            GUI.skin.textField.fontSize = currentFontSize;
            GUI.skin.textField.font = currentFont;

            return Text;
        }

        public  string DrawUpdate(string text)
        {
            Text = text;

            CheckStyle();

            Color currentColor = GUI.skin.textField.normal.textColor;
            int currentFontSize = GUI.skin.textField.fontSize;
            Font currentFont = GUI.skin.textField.font;

            GUI.skin.textField.normal.textColor = Brush.TextColor;
            GUI.skin.textField.fontSize = Brush.FontSize;
            GUI.skin.textField.font = Brush.TextFont;

            Text = DrawTextField(Layer, Percentages, Text, MaxLength);

            GUI.skin.textField.normal.textColor = currentColor;
            GUI.skin.textField.fontSize = currentFontSize;
            GUI.skin.textField.font = currentFont;

            return Text;
        }
        #endregion
    }
    //----------------------------
    public class STextArea : SElement
    {
        #region Attributes

        public string Text = "";
        public TextBrush Brush;

        public int maxLength = 0;

        #endregion

        #region Constructors
        public STextArea(float x, float y, float width, float height, int MaxLength) : base(x,y,width,height)
        {
            maxLength = MaxLength;
        }
        public STextArea(Rect Percentages, int MaxLength) : base (Percentages)
        {
            maxLength = MaxLength;
        }
        public STextArea(SLayer Layer, int MaxLength) : base (Layer)
        {
            maxLength = MaxLength;
        }
        public STextArea(SLayer Layer, float x, float y, float width, float height, int MaxLength) : base (Layer, x, y, width, height)
        {
            maxLength = MaxLength;
        }
        public STextArea(SLayer Layer, Rect Percentages, int MaxLength) : base (Layer, Percentages)
        {
            maxLength = MaxLength;
        }
        //-----------------------------------
        public STextArea(float x, float y, float width, float height, string text, int MaxLength) : base (x,y,width, height)
        {
            Text = text;
            maxLength = MaxLength;
        }
        public STextArea(Rect Percentages, string text, int MaxLength) : base (Percentages)
        {
            Text = text;
            maxLength = MaxLength;
        }
        public STextArea(SLayer Layer, string text, int MaxLength) : base (Layer)
        {
            Text = text;
            maxLength = MaxLength;
        }
        public STextArea(SLayer Layer, float x, float y, float width, float height, string text, int MaxLength) : base (Layer, x, y, width, height)
        {
            Text = text;
            maxLength = MaxLength;
        }
        public STextArea(SLayer Layer, Rect Percentages, string text, int MaxLength) : base (Layer, Percentages)
        {
            Text = text;
            maxLength = MaxLength;
        }
        #endregion

        #region Methods
        private void CheckStyle()
        {
            if (Brush == null)
                Brush = new TextBrush(GUI.skin.textArea.normal.textColor, GUI.skin.textArea.fontSize, GUI.skin.textArea.font);
        }

        public void ResetStyle()
        {
            Brush = null;
            CheckStyle();
        }

        public string Draw(string text)
        {
            Text = text;

            CheckStyle();

            Color currentColor = GUI.skin.textArea.normal.textColor;
            int currentFontSize = GUI.skin.textArea.fontSize;
            Font currentFont = GUI.skin.textArea.font;

            GUI.skin.textArea.normal.textColor = Brush.TextColor;
            GUI.skin.textArea.fontSize = Brush.FontSize;
            GUI.skin.textArea.font = Brush.TextFont;

            GUI.TextArea(new Rect(Position.x, Position.y, Size.x, Size.y), Text, maxLength);

            GUI.skin.textArea.normal.textColor = currentColor;
            GUI.skin.textArea.fontSize = currentFontSize;
            GUI.skin.textArea.font = currentFont;

            return Text;
        }

        public string DrawUpdate(string text)
        {
            Text = text;

            CheckStyle();

            Color currentColor = GUI.skin.textArea.normal.textColor;
            int currentFontSize = GUI.skin.textArea.fontSize;
            Font currentFont = GUI.skin.textArea.font;

            GUI.skin.textArea.normal.textColor = Brush.TextColor;
            GUI.skin.textArea.fontSize = Brush.FontSize;
            GUI.skin.textArea.font = Brush.TextFont;

            Text = DrawTextArea(Layer, Percentages, Text, maxLength);

            GUI.skin.textArea.normal.textColor = currentColor;
            GUI.skin.textArea.fontSize = currentFontSize;
            GUI.skin.textArea.font = currentFont;

            return Text;
        }
        #endregion
    }
	//----------------------------
    public class SSlider : SElement
    {
        #region Attributes
        private bool horizontal = true;
        private float value;
        public float Value { get { return value; } }
        private float min;
        private float max;
        public Vector2 Range { get { return new Vector2(min, max); } }
        public void SetRange (float min, float max)
        {
            this.min = min;
            this.max = max;
        }
        #endregion

        #region Constructor
        public SSlider (float x, float y, float thickness, bool horizontal, float value, float min, float max) : base (x,y, thickness, thickness)
        {
            this.horizontal = horizontal;
            this.value = value;
            this.min = min;
            this.max = max;
        }
        public SSlider(SLayer layer, float x, float y, float thickness, bool horizontal, float value, float min, float max) : base(layer, x, y, thickness, thickness)
        {
            this.horizontal = horizontal;
            this.value = value;
            this.min = min;
            this.max = max;
        }
        #endregion

        #region Methods
        public float Draw(float v)
        {
            value = v;

            if (horizontal)
                return value = GUI.HorizontalSlider(new Rect(Position.x, Position.y, Size.x, 13), value, min, max);

            return value = GUI.VerticalSlider(new Rect(Position.x, Position.y, 13, Size.y), value, min, max);

        }
        public float DrawUpdate(float v)
        {
            value = v;

            if (horizontal)
                return HorizontalSlider(Percentages.x, Percentages.y, Percentages.width, value, min, max);
            return VerticalSlider(Percentages.x, Percentages.y, Percentages.height, value, min, max);
        }
        #endregion
    }

    #endregion
    //----------------------------
    #region SelfControls
    public class SelfButton : SElement
    {
        #region Attributes

        public Texture2D Texture;
        private bool pushed = false;
        public bool BeingPushed { get { return pushed; } }

        private Texture2D alternative;
        public Texture2D AlternativeTexture
        {
            get { return alternative; }
            set { alternative = value; }
        }

        private float XMultiplier;
        private float YMultiplier;
        public void SetMultipliers(float X, float Y)
        {
            XMultiplier = X;
            YMultiplier = Y;
            sizeDifference = new Vector2(
                Size.x * XMultiplier - Size.x,
                Size.y * YMultiplier - Size.y);
        }

        private Vector2 sizeDifference;

        public override void Recalculate()
        {
            base.Recalculate();
            SetMultipliers(XMultiplier, YMultiplier);
        }

        private bool mouseEntered;
        public bool MouseEntered { get { return mouseEntered; } }

        #endregion

        #region Constructors
        public SelfButton(float x, float y, float width, float height, Texture2D texture, float XMultiplier, float YMultiplier) : base(x, y, width, height)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
            alternative = texture;
        }
        public SelfButton(Rect Percentages, Texture2D texture, float XMultiplier, float YMultiplier) : base(Percentages)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
            alternative = texture;
        }
        public SelfButton(SLayer Layer, Texture2D texture, float XMultiplier, float YMultiplier) : base(Layer)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
            alternative = texture;
        }
        public SelfButton(SLayer Layer, float x, float y, float width, float height, Texture2D texture, float XMultiplier, float YMultiplier) : base(Layer, x, y, width, height)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
            alternative = texture;
        }
        public SelfButton(SLayer Layer, Rect Percentages, Texture2D texture, float XMultiplier, float YMultiplier) : base(Layer, Percentages)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
            alternative = texture;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draw texture on screen (OnGUI Function)
        /// </summary>
        public bool Draw()
        {
            if (pushed && Input.GetKeyUp(KeyCode.Mouse0))
                pushed = false;

            if (IsOver) mouseEntered = false;
            bool wasOver = IsOver;
            bool overr = MouseOver();
            if (!wasOver && overr) mouseEntered = true;
            if (!overr)
            {
                GUI.DrawTexture(new Rect(Position.x, Position.y, Size.x, Size.y), Texture);
                return false;
            }
            if (!pushed && Input.GetKeyDown(KeyCode.Mouse0))
                pushed = true;

            if (pushed && Input.GetKey(KeyCode.Mouse0))
                GUI.DrawTexture(new Rect(Position.x, Position.y, Size.x, Size.y), alternative);
            else
                GUI.DrawTexture(new Rect(Position.x - sizeDifference.x / 2f, Position.y - sizeDifference.y / 2f, Size.x * XMultiplier, Size.y * YMultiplier), alternative);

            return pushed;
        }
        
        /// <summary>
        /// Draw the texture recalculating always position and size with percentages
        /// </summary>
        public bool DrawUpdate()
        {
            if (pushed && Input.GetKeyUp(KeyCode.Mouse0))
                pushed = false;

            if (IsOver) mouseEntered = false;
            bool wasOver = IsOver;
            bool overr = MouseOver();
            if (!wasOver && overr) mouseEntered = true;
            if (!overr)
            {
                DrawTexture(Layer, Percentages, Texture);
                return false;
            }
            
            if (!pushed && Input.GetKeyDown(KeyCode.Mouse0))
                pushed = true;

            if (pushed && Input.GetKey(KeyCode.Mouse0))
                DrawTexture(Layer, Percentages, alternative);
            else
                DrawTexture(Layer, XPerCent - (WidthPerCent * XMultiplier - WidthPerCent) / 2f, YPerCent - (HeightPerCent * YMultiplier - HeightPerCent) / 2f, WidthPerCent * XMultiplier, HeightPerCent * YMultiplier, alternative);

            return pushed;
        }
        #endregion
    }
    //----------------------------
    public class SelfHorizontalSlider : SElement
    {
        #region Attributes
        public Texture2D Background;

        float value;
        public float Value { get { return value; } set { this.value = value; } }

        float minValue;
        float maxValue;

        SelfButton btn;
        public void SetButtonDimensions(float width, float height)
        {
            btn.WidthPerCent = width;
            btn.HeightPerCent = height;
        }

        public void SetButtonEffect(float multiplierX, float multiplierY)
        {
            btn.SetMultipliers(multiplierX, multiplierY);
        }

        public override void Recalculate()
        {
            base.Recalculate();
            if (btn != null)
                btn.Recalculate();
        }
        #endregion

        #region Constructor
        public SelfHorizontalSlider(float x, float y, float width, float height, Texture2D bar, float btnWidth, float btnHeight, Texture2D btn, float minValue, float maxValue) : base(x, y, width, height)
        {
            Background = bar;
            this.btn = new SelfButton(0, 0, btnWidth, btnHeight, btn, 1, 1);
            this.minValue = minValue;
            this.maxValue = maxValue;
            value = minValue;

            SetButtonDimensions(btnWidth, btnHeight);
        }

        public SelfHorizontalSlider(Rect Percentages, Texture2D bar, float btnWidth, float btnHeight, Texture2D btn, float minValue, float maxValue) : base(Percentages)
        {
            Background = bar;
            this.btn = new SelfButton(0, 0, btnWidth, btnHeight, btn, 1, 1);
            this.minValue = minValue;
            this.maxValue = maxValue;
            value = minValue;

            SetButtonDimensions(btnWidth, btnHeight);
        }
        #endregion

        #region Methods
        public float Draw(float value)
        {
            GUI.DrawTexture(new Rect(Position.x, Position.y, Size.x, Size.y), Background);

            if (btn.BeingPushed)
            {
                float mouseX = Event.current.mousePosition.x;
                if (mouseX <= Position.x) value = minValue;
                else if (mouseX >= Position.x + Size.x) value = maxValue;
                else
                {
                    float p2 = (mouseX - Position.x) / Size.x;
                    value = minValue + p2 * (maxValue - minValue);
                }
            }

            float p = (value - minValue) / (maxValue - minValue);

            btn.XPerCent = XPerCent + (WidthPerCent - btn.WidthPerCent) * p;
            btn.YPerCent = YPerCent + HeightPerCent / 2 - btn.HeightPerCent / 2;

            btn.Draw();

            this.value = value;
            return value;

        }
        public float DrawUpdate(float value)
        {
            Recalculate();
            this.value = Draw(value);
            return this.value;
        }
        #endregion
    }
    #endregion

    #region SelfControls Android
    public class SelfButtonTouch : SElement
    {
        #region Attributes

        public Texture2D Texture;
        private bool pressed = false;
        public bool BeingPressed { get { return pressed; } }

        private float XMultiplier;
        private float YMultiplier;
        public void SetMultipliers(float X, float Y)
        {
            XMultiplier = X;
            YMultiplier = Y;
            sizeDifference = new Vector2(
                Size.x * XMultiplier - Size.x,
                Size.y * YMultiplier - Size.y);
        }

        private Vector2 sizeDifference;

        public override void Recalculate()
        {
            base.Recalculate();
            SetMultipliers(XMultiplier, YMultiplier);
        }

        #endregion

        #region Constructors
        public SelfButtonTouch(float x, float y, float width, float height, Texture2D texture, float XMultiplier, float YMultiplier) : base(x, y, width, height)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
        }
        public SelfButtonTouch(Rect Percentages, Texture2D texture, float XMultiplier, float YMultiplier) : base(Percentages)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
        }
        public SelfButtonTouch(SLayer Layer, Texture2D texture, float XMultiplier, float YMultiplier) : base(Layer)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
        }
        public SelfButtonTouch(SLayer Layer, float x, float y, float width, float height, Texture2D texture, float XMultiplier, float YMultiplier) : base(Layer, x, y, width, height)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
        }
        public SelfButtonTouch(SLayer Layer, Rect Percentages, Texture2D texture, float XMultiplier, float YMultiplier) : base(Layer, Percentages)
        {
            Texture = texture;
            SetMultipliers(XMultiplier, YMultiplier);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draw texture on screen (OnGUI Function)
        /// </summary>
        public bool Draw()
        {
            pressed = false;
            for(int i = 0; i<Input.touchCount; ++i)
            {
                Touch t = Input.GetTouch(i);
                pressed = PixelInPercentages(t.position, Percentages);
                if (pressed) break;
            }
            if (pressed)
                GUI.DrawTexture(new Rect(Position.x, Position.y, Size.x, Size.y), Texture);
            else
                GUI.DrawTexture(new Rect(Position.x - sizeDifference.x / 2f, Position.y - sizeDifference.y / 2f, Size.x * XMultiplier, Size.y * YMultiplier), Texture);

            return pressed;
        }

        /// <summary>
        /// Draw the texture recalculating always position and size with percentages
        /// </summary>
        public bool DrawUpdate()
        {
            pressed = false;
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Touch t = Input.GetTouch(i);
                pressed = PixelInPercentages(t.position, Percentages);
                if (pressed) break;
            }
            if (pressed)
                DrawTexture(Layer, Percentages, Texture);
            else
                DrawTexture(Layer, XPerCent - (WidthPerCent * XMultiplier - WidthPerCent) / 2f, YPerCent - (HeightPerCent * YMultiplier - HeightPerCent) / 2f, WidthPerCent * XMultiplier, HeightPerCent * YMultiplier, Texture);

            return pressed;
        }
        #endregion
    }
    //----------------------------
    public class SelfHorizontalSliderTouch : SElement
    {
        #region Attributes
        public Texture2D Background;

        float value;
        public float Value { get { return value; } set { this.value = value; } }

        float minValue;
        float maxValue;

        SelfButtonTouch btn;
        public void SetButtonDimensions(float width, float height)
        {
            btn.WidthPerCent = width;
            btn.HeightPerCent = height;
        }

        public void SetButtonEffect(float multiplierX, float multiplierY)
        {
            btn.SetMultipliers(multiplierX, multiplierY);
        }

        public override void Recalculate()
        {
            base.Recalculate();
            if (btn != null)
                btn.Recalculate();
        }
        #endregion

        #region Constructor
        public SelfHorizontalSliderTouch(float x, float y, float width, float height, Texture2D bar, float btnWidth, float btnHeight, Texture2D btn, float minValue, float maxValue) : base(x, y, width, height)
        {
            Background = bar;
            this.btn = new SelfButtonTouch(0, 0, btnWidth, btnHeight, btn, 1, 1);
            this.minValue = minValue;
            this.maxValue = maxValue;
            value = minValue;

            SetButtonDimensions(btnWidth, btnHeight);
        }
        #endregion

        #region Methods
        public float Draw(float value)
        {
            GUI.DrawTexture(new Rect(Position.x, Position.y, Size.x, Size.y), Background);

            if (btn.BeingPressed)
            {
                float mouseX = Event.current.mousePosition.x;
                if (mouseX <= Position.x) value = minValue;
                else if (mouseX >= Position.x + Size.x) value = maxValue;
                else
                {
                    float p2 = (mouseX - Position.x) / Size.x;
                    value = minValue + p2 * (maxValue - minValue);
                }
            }

            float p = (value - minValue) / (maxValue - minValue);

            btn.XPerCent = XPerCent + (WidthPerCent - btn.WidthPerCent) * p;
            btn.YPerCent = YPerCent + HeightPerCent / 2 - btn.HeightPerCent / 2;

            btn.Draw();

            this.value = value;
            return value;

        }
        public float DrawUpdate(float value)
        {
            Recalculate();
            this.value = Draw(value);
            return this.value;
        }
        #endregion
    }
    #endregion
    //----------------------------
    #region SSprite
    public class SSprite : SElement
    {
        #region Attributes
        private Texture2D[] sprites;
        public void SetFrames (params Texture2D[] frames)
        {
            this.sprites = frames;
        }
        private float time;
        private float timer = 0;
        private int currentSprite = 0;
        private int rcounter = 0;
        public int Iterations { get { return rcounter; } }
        private bool backward = false;
        #endregion
        #region Constructor
        public SSprite (float x, float y, float width, float height, int fps) : base (x, y, width, height)
        {
            SetFPS(fps);
        }
        public SSprite(Rect Percentages, int fps) : base(Percentages)
        {
            SetFPS(fps);
        }
        #endregion
        #region Methods
        public void SetFPS (int fps)
        {
            if (fps == 0)
            {
                time = 0;
                return;
            }
            if (fps >= 0) backward = false;
            else
            {
                backward = true;
                fps *= -1;
            }
            time = 1f / (float)fps;
        }

        public void Restart()
        {
            rcounter = 0;
            timer = 0;
            currentSprite = 0;
        }

        public void Draw(float deltaTime)
        {
            if (sprites == null || sprites.Length == 0) return;

            GUI.DrawTexture(new Rect(Position.x, Position.y, Size.x, Size.y), sprites[currentSprite]);

            UpdateTime(deltaTime);
        }

        public void DrawUpdated(float deltaTime)
        {
            if (sprites == null || sprites.Length == 0) return;

            SGUI.DrawTexture(Percentages, sprites[currentSprite]);

            UpdateTime(deltaTime);
        }

        private void UpdateTime(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= time)
            {
                if (!backward)
                    ++currentSprite;
                else
                    --currentSprite;

                if (currentSprite >= sprites.Length)
                {
                    currentSprite = 0;
                    ++rcounter;
                }
                if (backward && currentSprite < 0)
                {
                    currentSprite = sprites.Length - 1;
                    ++rcounter;
                }
                while (timer >= time)
                    timer -= time;
            }
        }
        #endregion
    }
    #endregion
    #endregion
}
