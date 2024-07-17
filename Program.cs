using System;
using System.Text;

public class OldPhonePadConverter
{
    public static string OldPhonePad(string input)
    {
        // Keypad mapping
        var keypad = new string[] {
            "",     // 0
            "",     // 1
            "ABC",  // 2
            "DEF",  // 3
            "GHI",  // 4
            "JKL",  // 5
            "MNO",  // 6
            "PQRS", // 7
            "TUV",  // 8
            "WXYZ"  // 9
        };

        var result = new StringBuilder();
        int currentButton = -1;
        int pressCount = 0;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (c == '#') // Send button
            {
                break;
            }
            else if (c == '*') // Backspace button
            {
                if (result.Length > 0)
                {
                    result.Length--; // Remove last character
                }
            }
            else if (c == ' ') // Pause
            {
                if (currentButton != -1 && pressCount > 0)
                {
                    var letters = keypad[currentButton];
                    result.Append(letters[(pressCount - 1) % letters.Length]);
                    currentButton = -1;
                    pressCount = 0;
                }
            }
            else // Number button
            {
                int button = c - '0';

                if (currentButton == button)
                {
                    pressCount++;
                }
                else
                {
                    if (currentButton != -1 && pressCount > 0)
                    {
                        var letters = keypad[currentButton];
                        result.Append(letters[(pressCount - 1) % letters.Length]);
                    }
                    currentButton = button;
                    pressCount = 1;
                }
            }
        }

        // Process the last pending character
        if (currentButton != -1 && pressCount > 0)
        {
            var letters = keypad[currentButton];
            result.Append(letters[(pressCount - 1) % letters.Length]);
        }

        return result.ToString();
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(OldPhonePad("33#")); // E
        Console.WriteLine(OldPhonePad("227*#")); // P
        Console.WriteLine(OldPhonePad("4433555 555666#")); // HELLO
        Console.WriteLine(OldPhonePad("8 88777444666*664#")); // TURNG
    }
}
