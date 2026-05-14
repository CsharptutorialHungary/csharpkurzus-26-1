namespace TicTacToe.Core;

public class Field
{
    int x, y;
    byte state; // 0 - empty, 1 - X, 2 - O
    public Field (int xpos, int ypos)
    {
        x = xpos;
        y = ypos;
        state = 0;
    }
    public int getState()
    {
        return state;
    }
    public void setState(byte state)
    {
        this.state = state;
    }
}