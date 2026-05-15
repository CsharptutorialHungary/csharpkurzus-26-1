namespace TicTacToe.Core;

public class Field
{
    readonly int x, y;
    byte state; // 0 - empty, 1 - X, 2 - O
    bool isSelected = false;  // Indicates if the field is currently selected (^ character is displayed)
    public Field (int xpos, int ypos)
    {
        x = xpos;
        y = ypos;
        state = 0;
    }
    public int getState() => state;
    public bool setState(byte state)
    {
        if (this.state != (byte)0)
            return false;

        this.state = state;
        return true;
    }
    public bool getIsSelected() => isSelected;
    public void setIsSelected(bool isSelected)
    {
        this.isSelected = isSelected;
    }

    public int getX() => x;
    public int getY() => y;

    public override string ToString()
    {
        return state switch
        {
            0 => " ",
            1 => "X",
            2 => "O",
            _ => throw new InvalidOperationException("Invalid field state")
        };
    }
}