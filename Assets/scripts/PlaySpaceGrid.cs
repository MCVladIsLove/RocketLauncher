using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlaySpaceGrid
{
    public int VerticalCount { get; set; }
    public int HorizontalCount { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float LeftX { get; set; }
    public float BottomY { get; set; }
    public float MinVerticalSpaceBetweenCells { get; set; }
    public float MinHorizontalSpaceBetweenCells { get; set; }

    public Rect GetCellBorders(int row, int column)
    {
        Rect cell = new Rect();
        cell.x = LeftX + Width / HorizontalCount * column + MinHorizontalSpaceBetweenCells / 2;
        cell.y = BottomY + Height / VerticalCount * (VerticalCount - row - 1) + MinVerticalSpaceBetweenCells / 2;
        cell.width = Width / HorizontalCount - MinHorizontalSpaceBetweenCells;
        cell.height = Height / VerticalCount - MinVerticalSpaceBetweenCells;

        if (column == 0)
            cell.xMin = LeftX;
        if (column == HorizontalCount - 1)
            cell.xMax = LeftX + Width;

        if (row == 0)
            cell.yMax = BottomY + Height;
        if (row == VerticalCount - 1)
            cell.yMin = BottomY;
        
        return cell;
    }
    public Vector2 GetCellCenter(int row, int column)
    {
        Vector2 center = new Vector2();
        center.x = LeftX + Width / HorizontalCount * column + Width / HorizontalCount / 2;
        center.y = BottomY + Height / VerticalCount * (VerticalCount - row - 1) + Height / VerticalCount / 2;
        
        if (column == 0)
            center.x -= MinHorizontalSpaceBetweenCells / 4;
        if (column == HorizontalCount - 1)
            center.x += MinHorizontalSpaceBetweenCells / 4;

        if (row == 0)
            center.y += MinVerticalSpaceBetweenCells / 4;
        if (row == VerticalCount - 1)
            center.y -= MinVerticalSpaceBetweenCells / 4;
        
        return center;
    }
}