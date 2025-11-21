public class ConsoleBuffer
{
    public int width { get; }
    public int height { get; }
    private char[] buffer;

    public ConsoleBuffer()
    {
        width = Console.WindowWidth;
        height = Console.WindowHeight;

        buffer = new char[width * height];
    }

    public void ClearBuffer(char clearCharacter = ' ')
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                buffer[y * width + x] = clearCharacter;
            }
        }
    }

    public void WriteCharToBuffer(char Character, int x, int y)
    {
        buffer[y * width + x] = Character;
    }

    public void WriteTextToBuffer(string text, int x, int y)
    {
        int i = 0;
        foreach (char c in text)
        {
            buffer[y * width + x + i] = c;
            i++;
        }
    }

    public void Draw()
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(buffer);
    }
}